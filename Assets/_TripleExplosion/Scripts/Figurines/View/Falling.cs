using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class Falling : MonoBehaviour
    {
        public event Action FallOver;

        private GameBoardHandler _board;
        private ReduceFigurine _reduceFigurine;
        private MovingFigurines _movingFigurines;
        private ChangingStartingFigures _changingStartingFigures;
        private FigurinesHandler _figurinesHandler;
        private readonly List<List<Transform>> _figurinesInColumn = new List<List<Transform>>();

        private SearchMatches _searchMatches;
        private List<Vector2> _movedFigures;
        private CheckingMatchNotFound _checkingMatchNotFound;

        [Inject]
        private void Construct(GameBoardHandler board,
                               ReduceFigurine reduceFigurine,
                               ChangingStartingFigures changingStartingFigures,
                               SearchMatches searchMatches,
                               CheckingMatchNotFound checkingMatchNotFound,
                               FigurinesHandler figurinesHandler)
        {
            _board = board;
            _reduceFigurine = reduceFigurine;
            _movingFigurines = new MovingFigurines(this);
            _changingStartingFigures = changingStartingFigures;

            _searchMatches = searchMatches;
            _checkingMatchNotFound = checkingMatchNotFound;
            _figurinesHandler = figurinesHandler;
        }

        private void Awake()
        {
            for (int i = 0; i < _board.GetLengthColumn; i++)
                _figurinesInColumn.Add(new List<Transform>());
        }

        private void OnSortList(List<Transform> figurines)
        {
            _movedFigures = new List<Vector2>();

            //Fill the _figurinesInColumn array by columns. 
            foreach (Transform figurine in figurines)
            {
                Vector2 coordinates = _board.GetÑoordinatesCell(figurine.parent);
                int x = (int)coordinates.x;
                _figurinesInColumn[x].Add(figurine);
                figurine.gameObject.SetActive(false);
            }

            //Sort rows in columns (top to bottom).
            for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                if (_figurinesInColumn[i].Count > 1)
                {
                    for (int t = 0; t < _figurinesInColumn[i].Count - 1; t++)
                    {
                        Transform maxFigurine = _figurinesInColumn[i][t];
                        Vector2 coordinates = _board.GetÑoordinatesCell(maxFigurine.parent);
                        int maxRow = (int)coordinates.y;

                        int maxIndex = t;

                        for (int k = t + 1; k < _figurinesInColumn[i].Count; k++)
                        {
                            Transform nextFigurine = _figurinesInColumn[i][k];
                            coordinates = _board.GetÑoordinatesCell(nextFigurine.parent);
                            int currentRow = (int)coordinates.y;

                            if (currentRow > maxRow)
                            {
                                maxRow = currentRow;
                                maxIndex = k;
                            }
                        }

                        (_figurinesInColumn[i][t], _figurinesInColumn[i][maxIndex]) =
                            (_figurinesInColumn[i][maxIndex], _figurinesInColumn[i][t]);
                    }
                }
            }

            /*for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                if (_figurinesInColumn[i].Count > 0)
                {
                    for (int t = 0; t < _figurinesInColumn[i].Count; t++)
                    {
                        Vector2 vector = _board.GetÑoordinatesCell(_figurinesInColumn[i][t].parent);
                        Debug.Log(vector);
                    }
                    Debug.Log("=====");
                }
            }
            Debug.Log("êîíåö");*/

            SwapFigurines();
            ChangeColor();
            OnFall();
        }

        private void SwapFigurines()
        {
            for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                for (int k = 0; k < _figurinesInColumn[i].Count; k++)
                {
                    Vector2 startCell = _board.GetÑoordinatesCell(_figurinesInColumn[i][k].parent);
                    int startRow = (int)startCell.y;

                    for (int t = 0; t < _board.GetLengthRow - startRow - 1; t++)
                    {
                        _figurinesHandler.SwipeFigurines(i, startRow + t, i, startRow + t + 1);
                    }
                }
            }
        }

        private void ChangeColor()
        {
            for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                for (int k = 0; k < _figurinesInColumn[i].Count; k++)
                {
                    _changingStartingFigures.SetRandomSprite(i, _board.GetLengthRow - 1 - k);
                }
            }
        }

        private void OnFall()
        {
            Debug.Log("OnFall");
            List<Transform> moveFigurines = new List<Transform>();

            for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                if (_figurinesInColumn[i].Count > 0)
                {
                    Transform currentFigurine = _figurinesInColumn[i][0];

                    Vector2 coordinates = _board.GetÑoordinatesCell(currentFigurine.parent);
                    int row = (int)coordinates.y;

                    SetParametersForFigurine(moveFigurines, currentFigurine, i, _board.GetLengthRow - 1);

                    currentFigurine.gameObject.SetActive(true);
                    currentFigurine.localPosition = Vector3.up;

                    _figurinesInColumn[i].RemoveAt(0);

                    for (int k = 0; k < _board.GetLengthRow - 1 - row; k++)
                    {
                        if (_board.GetCell(i, row + k + 1).childCount > 0)
                        {
                            Transform topFigurine = _board.GetCell(i, row + k + 1).GetChild(0);
                            SetParametersForFigurine(moveFigurines, topFigurine, i, row + k);
                        }
                    }
                }
            }

            if (moveFigurines.Count > 0)
            {
                _movingFigurines.StartMove(moveFigurines);
            }
            else
            {
                _checkingMatchNotFound.ResetFinded();

                foreach (var figure in _movedFigures)
                    _searchMatches.StartFind((int)figure.x, (int)figure.y);

                if (!_checkingMatchNotFound.IsFind)
                {
                    for (int column = 0; column < _board.GetLengthColumn; column++)
                    {
                        for (int row = 0; row < _board.GetLengthRow; row++)
                        {
                            Sprite sprite1 = _figurinesHandler.GetRender(column, row).sprite;
                            Sprite sprite2 = _board.GetCell(column, row).GetChild(0).GetComponent<SpriteRenderer>().sprite;

                            if (sprite1 != sprite2)
                            {
                                Debug.Log(column + " | " + row);
                                Debug.Log("ðåàëüíûé ñïðàéò: " + sprite2);
                                Debug.Log("ñïðàéò ÷åðåç ïîåáîòó: " + sprite1);
                            }
                        }
                    }

                    FallOver?.Invoke();
                }
            }
        }

        private void SetParametersForFigurine(List<Transform> moveFigurines,
                                              Transform figurine,
                                              int column,
                                              int row)
        {
            //Vector2 oldCoordinates = _board.GetÑoordinatesCell(figurine.parent);

            figurine.parent = _board.GetCell(column, row);
            //_figurinesHandler.SwipeFigurines(column, row, column, (int)oldCoordinates.y);
            /*Debug.Log(column + " | " + row);
            Debug.Log((int)oldCoordinates.x + " | " + (int)oldCoordinates.y);
            Debug.Log("====");*/
            
            _figurinesHandler.GetSwap(column, row).SetCoordinates();

            //figurine.GetComponent<SwapParent>().SetCoordinates();

            moveFigurines.Add(figurine);

            _movedFigures.Add(new Vector2(column, row));
        }

        private void OnEnable()
        {
            _reduceFigurine.Reduced += OnSortList;
            _movingFigurines.MovementOver += OnFall;
        }

        private void OnDisable()
        {
            _reduceFigurine.Reduced -= OnSortList;
            _movingFigurines.MovementOver -= OnFall;
        }
    }
}

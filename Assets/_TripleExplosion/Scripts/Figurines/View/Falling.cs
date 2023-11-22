using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class Falling : MonoBehaviour
    {
        private GameBoardHandler _board;
        private ReduceFigurine _reduceFigurine;
        private ChangingStartingFigures _changingStartingFigures;
        private FigurinesHandler _figurinesHandler;
        private MovingFigurines _movingFigurines;
        private RemovingMatches _removingMatches;
        private FixingNoMoves _fixingNoMoves;
        private readonly List<List<Transform>> _figurinesInColumn = new List<List<Transform>>();

        private SearchMatches _searchMatches;
        private List<Vector2> _movedFigures;

        [Inject]
        private void Construct(GameBoardHandler board,
                               ReduceFigurine reduceFigurine,
                               ChangingStartingFigures changingStartingFigures,
                               SearchMatches searchMatches,
                               FigurinesHandler figurinesHandler,
                               RemovingMatches removingMatches,
                               FixingNoMoves fixingNoMoves)
        {
            _board = board;
            _reduceFigurine = reduceFigurine;
            _changingStartingFigures = changingStartingFigures;
            _movingFigurines = new MovingFigurines(this);
            _searchMatches = searchMatches;
            _figurinesHandler = figurinesHandler;
            _removingMatches = removingMatches;
            _fixingNoMoves = fixingNoMoves;
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
                Vector2 coordinates = _board.GetCoordinatesCell(figurine.parent);
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
                        Vector2 coordinates = _board.GetCoordinatesCell(maxFigurine.parent);
                        int maxRow = (int)coordinates.y;

                        int maxIndex = t;

                        for (int k = t + 1; k < _figurinesInColumn[i].Count; k++)
                        {
                            Transform nextFigurine = _figurinesInColumn[i][k];
                            coordinates = _board.GetCoordinatesCell(nextFigurine.parent);
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
                    Vector2 startCell = _board.GetCoordinatesCell(_figurinesInColumn[i][k].parent);
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
            List<Transform> moveFigurines = new List<Transform>();

            for (int i = 0; i < _figurinesInColumn.Count; i++)
            {
                if (_figurinesInColumn[i].Count > 0)
                {
                    Transform currentFigurine = _figurinesInColumn[i][0];

                    Vector2 coordinates = _board.GetCoordinatesCell(currentFigurine.parent);
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
                _removingMatches.Clear();

                foreach (var figure in _movedFigures)
                    _searchMatches.StartFind((int)figure.x, (int)figure.y);

                if (_removingMatches.IsNoMath)
                    _fixingNoMoves.OnCheckAndFix();
                else
                    _removingMatches.RemoveFigurines();
            }
        }

        private void SetParametersForFigurine(List<Transform> moveFigurines,
                                              Transform figurine,
                                              int column,
                                              int row)
        {
            figurine.parent = _board.GetCell(column, row);
            _figurinesHandler.GetSwap(column, row).SetCoordinates();
            moveFigurines.Add(figurine);

            _movedFigures.Add(new Vector2(column, row));
        }

        private void OnEnable()
        {
            _reduceFigurine.ReducedOver += OnSortList;
            _movingFigurines.MovementOver += OnFall;
        }

        private void OnDisable()
        {
            _reduceFigurine.ReducedOver -= OnSortList;
            _movingFigurines.MovementOver -= OnFall;
        }
    }
}

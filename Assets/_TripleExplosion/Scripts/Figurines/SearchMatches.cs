using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TripleExplosion
{
    public class SearchMatches
    {
        public event Action<List<Transform>> MatchFounded;

        private readonly GameBoardHandler _board;
        private readonly FigurinesHandler _figurinesHandler;
        private readonly int _minNumberCoincidencesWithoutMain = 2;

        private Transform _mainTransform;
        private Sprite _mainSprite;
        private List<Transform> _horizontalFigurines;
        private List<Transform> _verticalFigurines;

        public SearchMatches(GameBoardHandler board,
                             FigurinesHandler figurinesHandler)
        {
            _board = board;
            _figurinesHandler = figurinesHandler;
        }

        private int GetLengthHorizontal { get => _horizontalFigurines.Count; }
        private int GetLengthVertical { get => _verticalFigurines.Count; }

        public Transform GetTransformInHorizontal(int index)
        {
            if (index >= 0 && index < GetLengthHorizontal)
                return _horizontalFigurines[index];
            else
                throw new ArgumentOutOfRangeException();
        }

        public Transform GetTransformInVertical(int index)
        {
            if (index >= 0 && index < GetLengthVertical)
                return _verticalFigurines[index];
            else
                throw new ArgumentOutOfRangeException();
        }

        public void StartFind(Transform figurine)
        {
            Vector2 cell = _board.GetCoordinatesCell(figurine.parent);
            StartFind((int)cell.x, (int)cell.y);
        }

        public void StartFind(int column, int row)
        {
            _horizontalFigurines = new List<Transform>();
            _verticalFigurines = new List<Transform>();

            FindMatches(column, row);
            if (IsMatch())
                AddMatch();
        }

        private void FindMatches(int column, int row)
        {
            _mainTransform = _board.GetCell(column, row).GetChild(0);
            _mainSprite = _figurinesHandler.GetRender(column, row).sprite;

            FindMatchesUp(column, row);
            FindMatchesDown(column, row);
            FindMatchesRight(column, row);
            FindMatchesLeft(column, row);
        }

        private bool IsMatch()
        {
            if (_horizontalFigurines.Count >= _minNumberCoincidencesWithoutMain ||
                _verticalFigurines.Count >= _minNumberCoincidencesWithoutMain)
                return true;
            else
                return false;
        }

        private void AddMatch()
        {
            List<Transform> figurines = new List<Transform>();

            int lengthHorizontal = GetLengthHorizontal;
            if (lengthHorizontal >= _minNumberCoincidencesWithoutMain)
                for (int i = 0; i < lengthHorizontal; i++)
                    figurines.Add(GetTransformInHorizontal(i));

            int lengthVertical = GetLengthVertical;
            if (lengthVertical >= _minNumberCoincidencesWithoutMain)
                for (int i = 0; i < lengthVertical; i++)
                    figurines.Add(GetTransformInVertical(i));

            figurines.Add(_mainTransform);

            MatchFounded?.Invoke(figurines);
        }

        private void FindMatchesRight(int column, int row)
        {
            column++;

            if (column < _board.GetLengthColumn)
            {
                Transform figurine = _board.GetCell(column, row).GetChild(0);
                if (IsMatched(figurine))
                {
                    _horizontalFigurines.Add(figurine);
                    FindMatchesRight(column, row);
                }
            }
        }

        private void FindMatchesLeft(int column, int row)
        {
            column--;

            if (column >= 0)
            {
                Transform figurine = _board.GetCell(column, row).GetChild(0);
                if (IsMatched(figurine))
                {
                    _horizontalFigurines.Add(figurine);
                    FindMatchesLeft(column, row);
                }
            }
        }

        private void FindMatchesUp(int column, int row)
        {
            row++;

            if (row < _board.GetLengthRow)
            {
                Transform figurine = _board.GetCell(column, row).GetChild(0);
                if (IsMatched(figurine))
                {
                    _verticalFigurines.Add(figurine);
                    FindMatchesUp(column, row);
                }
            }
        }

        private void FindMatchesDown(int column, int row)
        {
            row--;

            if (row >= 0)
            {
                Transform figurine = _board.GetCell(column, row).GetChild(0);
                if (IsMatched(figurine))
                {
                    _verticalFigurines.Add(figurine);
                    FindMatchesDown(column, row);
                }
            }
        }

        private bool IsMatched(Transform figurine)
        {
            if (figurine.GetComponent<SpriteRenderer>().sprite == _mainSprite)
                return true;
            else
                return false;
        }
    }
}

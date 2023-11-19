using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TripleExplosion
{
    public class SearchMatches
    {
        public event Action MatchFounded;

        private readonly GameBoardHandler _board;
        private readonly CheckingMatchNotFound _checkingMatchNotFound;
        private readonly FigurinesHandler _figurinesHandler;
        private readonly int _minNumberCoincidencesWithoutMain = 2;

        private Transform _mainTransform;
        private Sprite _mainSprite;
        private List<Transform> _horizontalFigurines;
        private List<Transform> _verticalFigurines;

        public SearchMatches(GameBoardHandler board,
                             CheckingMatchNotFound checkingMatchNotFound,
                             FigurinesHandler figurinesHandler)
        {
            _checkingMatchNotFound = checkingMatchNotFound;
            _board = board;
            _figurinesHandler = figurinesHandler;
        }

        public int GetMinNumberCoincidencesWithoutMain { get => _minNumberCoincidencesWithoutMain; }
        public int GetLengthHorizontal { get => _horizontalFigurines.Count; }
        public int GetLengthVertical { get => _verticalFigurines.Count; }
        public Transform GetMainFigurine { get => _mainTransform; }

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

        public void StartFind(int column, int row)
        {
            _horizontalFigurines = new List<Transform>();
            _verticalFigurines = new List<Transform>();

            FindMatches(column, row);
            CheckMatches();
        }

        private void FindMatches(int column, int row)
        {
            _mainTransform = _board.GetCell(column, row).GetChild(0);
            //_mainSprite = _mainTransform.GetComponent<SpriteRenderer>().sprite;
            _mainSprite = _figurinesHandler.GetRender(column, row).sprite;

            FindMatchesUp(column, row);
            FindMatchesDown(column, row);
            FindMatchesRight(column, row);
            FindMatchesLeft(column, row);
        }

        private void CheckMatches()
        {
            _horizontalFigurines.Distinct();
            _verticalFigurines.Distinct();

            if (_horizontalFigurines.Count >= _minNumberCoincidencesWithoutMain ||
                _verticalFigurines.Count >= _minNumberCoincidencesWithoutMain)
            {
                _checkingMatchNotFound.Finded();
                MatchFounded?.Invoke();
            }
            else
                _checkingMatchNotFound.MatchNotFounded();
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

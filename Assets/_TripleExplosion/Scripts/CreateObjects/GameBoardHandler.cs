using System;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class GameBoardHandler : IInitializable
    {
        private readonly Transform _parent;
        private readonly int _numRows;
        private readonly int _numColumns;
        private readonly Transform _cellPrefab;
        [Inject] private readonly DiContainer _diContainer;

        private Transform[,] _cells;
        private bool _isBoardAcrive = true;

        public GameBoardHandler(GameBoardConfig config, Transform parent)
        {
            _parent = parent;
            _numRows = config.NumbersRows;
            _numColumns = config.NumbersColumns;
            _cellPrefab = config.CellPrefab;
        }

        public int GetLengthColumn { get => _numColumns; }
        public int GetLengthRow { get => _numRows; }
        public bool IsBoardAcrive { get => _isBoardAcrive; }

        public Transform GetCell(int column, int row)
        {
            if (column >= 0 && column < GetLengthColumn &&
                row >= 0 && row < GetLengthRow)
                return _cells[column, row];
            else
                throw new ArgumentOutOfRangeException();
        }

        public Vector2 GetÑoordinatesCell(Transform cell)
        {
            for (int column = 0; column < _numColumns; column++)
            {
                for (int row = 0; row < _numRows; row++)
                {
                    if (GetCell(column, row) == cell)
                        return new Vector2(column, row);
                }
            }

            throw new NotImplementedException();
        }

        public void DisableActiveBoarde() => _isBoardAcrive = false;

        public void EnableActiveBoarde() => _isBoardAcrive = true;

        public void Initialize() => CreateGameBoard();

        private void CreateGameBoard()
        {
            _cells = new Transform[_numColumns, _numRows];

            for (int column = 0; column < _numColumns; column++)
            {
                for (int row = 0; row < _numRows; row++)
                {
                    Transform cell =
                        _diContainer.InstantiatePrefabForComponent<Transform>(_cellPrefab,
                                                                              _parent.position,
                                                                              Quaternion.identity,
                                                                              _parent);
                    cell.localScale = _cellPrefab.lossyScale;
                    cell.localPosition = new Vector2(column, row);
                    cell.name = $"{column}, {row}";

                    _cells[column, row] = cell;
                }
            }
        }
    }
}

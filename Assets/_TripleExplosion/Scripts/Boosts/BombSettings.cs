using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class BombSettings : IInitializable, IBoost
    {
        public event Action<bool> BoostActivitySet;
        public event Action<List<Transform>> ListFilled;
        public event Action BombUsed;

        private readonly GameBoardHandler _board;
        private readonly ReduceFigurine _reduceFigurine;

        private int _explosionWidth = 3;
        private int _explosionHeight = 3;
        private bool _isActive = false;

        private int _maxWidth;
        private int _maxHeight;

        public BombSettings(GameBoardHandler board,
                            ReduceFigurine reduceFigurine)
        {
            _board = board;
            _reduceFigurine = reduceFigurine;
        }

        public void Initialize()
        {
            _maxWidth = _board.GetLengthColumn;
            _maxHeight = _board.GetLengthRow;

            if (_explosionWidth % 2 != 1)
                _explosionWidth--;
            if (_explosionHeight % 2 != 1)
                _explosionHeight--;
        }

        public bool GetActiveBoost { get => _isActive; }

        public void SetActiveBoost(bool value)
        {
            _isActive = value;
            BoostActivitySet?.Invoke(value);
        }

        public void TryUsingBoost(int column, int row)
        {
            if (_isActive && _board.IsBoardAcrive)
            {
                SetActiveBoost(false);
                _board.SetActiveBoarde(false);
                List<Transform> figurines = new List<Transform>();
                FillList(figurines, column, row);
                ListFilled?.Invoke(figurines);
                _reduceFigurine.StartReduce(figurines);
                BombUsed?.Invoke();
            }
        }

        private void FillList(List<Transform> figurines, int column, int row)
        {
            FillRow(figurines, column, row);

            for (int i = 1; i <= (_explosionHeight - 1) / 2; i++)
            {
                if (row + i < _maxHeight)
                    FillRow(figurines, column, row + i);
                if (row - i >= 0)
                    FillRow(figurines, column, row - i);
            }
        }

        private void FillRow(List<Transform> figurines, int column, int row)
        {
            figurines.Add(_board.GetCell(column, row).GetChild(0));

            for (int i = 1; i <= (_explosionWidth - 1) / 2; i++)
            {
                if (column + i < _maxWidth)
                    figurines.Add(_board.GetCell(column + i, row).GetChild(0));
                if (column - i >= 0)
                    figurines.Add(_board.GetCell(column - i, row).GetChild(0));
            }
        }
    }
}

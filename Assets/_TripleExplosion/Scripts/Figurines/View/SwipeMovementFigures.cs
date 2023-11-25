using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class SwipeMovementFigures : MonoBehaviour
    {
        public event Action ReverseSwipeUsed;

        private MovingFigurines _movingFigurines;
        private GameBoardHandler _board;
        private SearchMatches _searchMatches;
        private RemovingMatches _removingMatches;
        private FixingNoMoves _fixingNoMoves;

        private readonly List<Transform> _figurines = new List<Transform>();
        private readonly float _straightAngle = 180f;

        private bool _needSearch = true;
        private bool _needReverseSwipe = true;
        private SwapParent _swapFigurine;
        private float _swipeAngle;

        [Inject]
        private void Construct(GameBoardHandler board,
                               SearchMatches searchMatches,
                               RemovingMatches removingMatches,
                               FixingNoMoves fixingNoMoves)
        {
            _movingFigurines = new MovingFigurines(this);
            _board = board;
            _searchMatches = searchMatches;
            _removingMatches = removingMatches;
            _fixingNoMoves = fixingNoMoves;
        }

        public void SetReverseSwipe(bool value) => _needReverseSwipe = value;

        public void SetParameters(SwapParent figurine, float swipeAngle)
        {
            _swapFigurine = figurine;

            if (swipeAngle > 0)
                _swipeAngle = swipeAngle - _straightAngle;
            else
                _swipeAngle = swipeAngle + _straightAngle;
        }

        public void AddFigurine(Transform figurine) => _figurines.Add(figurine);

        public void CallMove()
        {
            if (_figurines.Count > 0)
                _movingFigurines.StartMove(_figurines);
        }

        private void ClearList() => _figurines.Clear();

        private void Search()
        {
            if (_needSearch)
            {
                foreach (Transform figurine in _figurines)
                    _searchMatches.StartFind(figurine);

                ClearList();

                if (_removingMatches.IsNoMath)
                {
                    if (_needReverseSwipe)
                    {
                        _needSearch = false;
                        _swapFigurine.ChangeParant(_swipeAngle);
                    }
                    else
                    {
                        _fixingNoMoves.OnCheckAndFix();
                    }
                }
                else
                {
                    _removingMatches.RemoveFigurines();
                }

                if (!_needReverseSwipe)
                {
                    SetReverseSwipe(true);
                    ReverseSwipeUsed?.Invoke();
                }
            }
            else
            {
                ClearList();
                _needSearch = true;
                _board.SetActiveBoarde(true);
            }
        }

        private void OnEnable()
            => _movingFigurines.MovementOver += Search;

        private void OnDisable()
            => _movingFigurines.MovementOver -= Search;
    }
}

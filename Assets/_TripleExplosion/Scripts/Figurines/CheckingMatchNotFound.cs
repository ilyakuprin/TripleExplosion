using System;

namespace TripleExplosion
{
    public class CheckingMatchNotFound 
    {
        public event Action ParametersSet;

        private readonly float _straightAngle = 180f;
        private readonly GameBoardHandler _board;

        private SwapParent _figurine;
        private float _swipeAngle;
        private bool _isFirstCheck = true;
        private bool _isFind = false;
        private bool _isReverseSwipe = false;
        private bool _isChangeParante = true;

        public CheckingMatchNotFound(GameBoardHandler board)
            => _board = board;

        public bool IsFind { get => _isFind; }

        public void SetParameters(SwapParent figurine, float swipeAngle)
        {
            _figurine = figurine;
            _swipeAngle = swipeAngle;
            SetFirstCheck();
            _isFind = false;
            _isReverseSwipe = true;
            _isChangeParante = true;

            ParametersSet?.Invoke();
        }

        public void SetFirstCheck() => _isFirstCheck = true;

        public void SetNoReverse() => _isChangeParante = false;

        public void Finded()
        {
            if (!_isFind)
                _isFind = true;
            if (_isReverseSwipe)
                _isReverseSwipe = false;
        }

        public void ResetFinded()
        {
            if (_isFind)
                _isFind = false;
        }

        public void MatchNotFounded()
        {
            if (_isReverseSwipe)
            {
                if (!_isFirstCheck)
                {
                    if (_isChangeParante)
                    {
                        if (_swipeAngle > 0)
                            _swipeAngle -= _straightAngle;
                        else
                            _swipeAngle += _straightAngle;

                        _figurine.ChangeParant(_swipeAngle, false);
                    }
                    else
                        _board.EnableActiveBoarde();
                }

                _isFirstCheck = false;
            }
        }
    }
}

using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class SwapParent : MonoBehaviour
    {
        private FigurinesHandler _figurinesHandler;
        private SwipeMovementFigures _swipeMovementFigures;
        private GameBoardHandler _board;
        private SwapParent _secondFigure;
        private int _column;
        private int _row;
        private readonly float _halfRightAngle = 45f;
        private readonly float _straightAngleWithoutHalfRight = 135f;

        public int GetColumn { get => _column; }
        public int GetRow { get => _row; }

        [Inject]
        public void Construct(GameBoardHandler board,
                              FigurinesHandler figurinesHandler,
                              SwipeMovementFigures swipeMovementFigures)
        {
            _board = board;
            _swipeMovementFigures = swipeMovementFigures;
            _figurinesHandler = figurinesHandler;
        }

        public void SetCoordinates()
        {
            _column = (int)transform.parent.localPosition.x;
            _row = (int)transform.parent.localPosition.y;
        }

        private void Awake() => SetCoordinates();

        public void ChangeParant(float swipeAngle)
        {
            if (swipeAngle > -_halfRightAngle &&
                swipeAngle <= _halfRightAngle &&
                _column < _board.GetLengthColumn - 1)
                RightSwipe();
            else if (swipeAngle > _halfRightAngle &&
                     swipeAngle <= _straightAngleWithoutHalfRight &&
                     _row < _board.GetLengthRow - 1)
                UpSwipe();
            else if ((swipeAngle > _straightAngleWithoutHalfRight ||
                    swipeAngle <= -_straightAngleWithoutHalfRight) &&
                    _column > 0)
                LeftSwipe();
            else if ((swipeAngle < -_halfRightAngle &&
                      swipeAngle >= -_straightAngleWithoutHalfRight) &&
                      _row > 0)
                DownSwipe();
            else
            {
                _board.EnableActiveBoarde();
                return;
            }
            
            _figurinesHandler.SwipeFigurines(GetColumn, GetRow,
                                             _secondFigure.GetColumn, _secondFigure.GetRow);

            _swipeMovementFigures.AddFigurine(transform);
            _swipeMovementFigures.AddFigurine(_secondFigure.transform);

            _swipeMovementFigures.CallMove();
        }

        private void RightSwipe()
        {
            IncreaseColumn();
            SetSecondFigure();
            _secondFigure.DecreaseColumn();
        }

        private void UpSwipe()
        {
            IncreaseRow();
            SetSecondFigure();
            _secondFigure.DecreaseRow();
        }

        private void LeftSwipe()
        {
            DecreaseColumn();
            SetSecondFigure();
            _secondFigure.IncreaseColumn();
        }

        private void DownSwipe()
        {
            DecreaseRow();
            SetSecondFigure();
            _secondFigure.IncreaseRow();
        }

        private void SetSecondFigure()
            => _secondFigure = _figurinesHandler.GetSwap(_column, _row);

        public void IncreaseColumn()
        {
            if (_column < _board.GetLengthColumn)
            {
                _column++;
                SetNewParant();
            }
        }

        public void DecreaseColumn()
        {
            if (_column >= 0)
            {
                _column--;
                SetNewParant();
            }
        }

        public void IncreaseRow()
        {
            if (_row < _board.GetLengthRow)
            {
                _row++;
                SetNewParant();
            }
        }

        public void DecreaseRow()
        {
            if (_row >= 0)
            {
                _row--;
                SetNewParant();
            }
        }

        private void SetNewParant()
            => transform.parent = _board.GetCell(_column, _row);
    }
}

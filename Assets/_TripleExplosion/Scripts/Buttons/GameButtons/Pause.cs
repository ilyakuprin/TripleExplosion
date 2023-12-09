using UnityEngine;

namespace TripleExplosion
{
    public class Pause
    {
        private readonly Falling _falling;
        private readonly SwipeMovementFigures _swipeMovementFigures;
        private readonly ReduceFigurine _reduceFigurine;
        private readonly TimerCounter _timerCounter;
        private readonly GameBoardHandler _board;
        private bool _isDisabledBoard;

        public Pause(Falling falling,
                     SwipeMovementFigures swipeMovementFigures,
                     ReduceFigurine reduceFigurine,
                     TimerCounter timerCounter,
                     GameBoardHandler board)
        {
            _falling = falling;
            _swipeMovementFigures = swipeMovementFigures;
            _reduceFigurine = reduceFigurine;
            _timerCounter = timerCounter;
            _board = board;
        }

        public void OnSetPause(bool value)
        {
            _falling.GetMovingFigurines.Pause(value);
            _swipeMovementFigures.GetMovingFigurines.Pause(value);
            _reduceFigurine.Pause(value);
            _timerCounter.Pause(value);

            if (value && _board.IsBoardAcrive)
            {
                _board.SetActiveBoarde(false);
                _isDisabledBoard = true;
            }
            else if (!value && _isDisabledBoard)
            {
                _board.SetActiveBoarde(true);
                _isDisabledBoard = false;
            }
        }
    }
}

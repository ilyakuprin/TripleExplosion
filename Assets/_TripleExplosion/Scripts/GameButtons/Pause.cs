using Zenject;

namespace TripleExplosion
{
    public class Pause
    {
        [Inject] private readonly Falling _falling;
        [Inject] private readonly SwipeMovementFigures _swipeMovementFigures;
        [Inject] private readonly ReduceFigurine _reduceFigurine;
        [Inject] private readonly TimerCounter _timerCounter;
        [Inject] private readonly GameBoardHandler _board;

        private bool _isDisabledBoard;

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

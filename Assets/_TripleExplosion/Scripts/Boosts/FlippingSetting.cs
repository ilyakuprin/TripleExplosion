namespace TripleExplosion
{
    public class FlippingSetting : IBoost
    {
        private readonly SwipeMovementFigures _swipeMovementFigures;
        private bool _isActive = false;

        public FlippingSetting(SwipeMovementFigures swipeMovementFigures)
            => _swipeMovementFigures = swipeMovementFigures;

        public bool GetActiveBoost { get => _isActive; }

        public void SetActiveBoost(bool value)
            => _isActive = value;

        public void ChangeActiveBoost()
            => _isActive = !_isActive;

        public void EnableBoost()
            => _swipeMovementFigures.SetReverseSwipe(true);

        public void DisableBoost()
            => _swipeMovementFigures.SetReverseSwipe(false);
    }
}

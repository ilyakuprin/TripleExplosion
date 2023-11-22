namespace TripleExplosion
{
    public class FlippingSetting
    {
        private readonly SwipeMovementFigures _swipeMovementFigures;

        public FlippingSetting(SwipeMovementFigures swipeMovementFigures)
            => _swipeMovementFigures = swipeMovementFigures;

        public void OnActivateBoost()
            => _swipeMovementFigures.DisableReverseSwipe();
    }
}

namespace TripleExplosion
{
    public class NumberFlipView : NumberBoostsView
    {
        private void OnEnable()
            => GetBoostCounter.SwipeChanged += ChangeValue;

        private void OnDisable()
            => GetBoostCounter.SwipeChanged += ChangeValue;
    }
}
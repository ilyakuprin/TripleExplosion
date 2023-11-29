namespace TripleExplosion
{
    public class NumberMixView : NumberBoostsView
    {
        private void OnEnable()
            => GetBoostCounter.MixChanged += ChangeValue;

        private void OnDisable()
            => GetBoostCounter.MixChanged += ChangeValue;
    }
}
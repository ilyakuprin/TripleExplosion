namespace TripleExplosion
{
    public class NumberPaintView : NumberBoostsView
     {
        private void OnEnable()
            => GetBoostCounter.PaintChanged += ChangeValue;

        private void OnDisable()
            => GetBoostCounter.PaintChanged += ChangeValue;
    }
}
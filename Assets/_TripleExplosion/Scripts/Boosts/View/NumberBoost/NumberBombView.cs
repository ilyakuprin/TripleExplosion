namespace TripleExplosion
{
    public class NumberBombView : NumberBoostsView
     {
        private void OnEnable()
            => GetBoostCounter.BombChanged += ChangeValue;

        private void OnDisable()
            => GetBoostCounter.BombChanged += ChangeValue;
     }
}
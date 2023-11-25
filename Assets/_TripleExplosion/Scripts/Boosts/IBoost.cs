namespace TripleExplosion
{
    public interface IBoost
    {
        public bool GetActiveBoost { get; }

        public void SetActiveBoost(bool value);
    }
}

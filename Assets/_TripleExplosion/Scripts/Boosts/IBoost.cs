using System;

namespace TripleExplosion
{
    public interface IBoost
    {
        public event Action<bool> BoostActivitySet;

        public bool GetActiveBoost { get; }

        public void SetActiveBoost(bool value);
    }
}

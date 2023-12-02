using System;

namespace TripleExplosion
{
    public interface IPointCounter
     {
        public event Action<int> PointAdded;

        public int TotalCounter { get; }

        public void Add(int value);
    }
}
using UnityEngine;

namespace TripleExplosion
{
    public interface IPointCounter
     {
        public int TotalCounter { get; }

        public void Add(int value);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounter : IInitializable, IDisposable
    {
        public event Action PointAdded;

        private int _counter;
        private readonly ReduceFigurine _reduceFigurine;
        private readonly int _rewardForFigurine = 1;

        public PointCounter(ReduceFigurine reduceFigurine)
            => _reduceFigurine = reduceFigurine;

        public int GetCounter { get => _counter; }

        private void AddPoint(List<Transform> figurenes)
        {
            _counter += figurenes.Count * _rewardForFigurine;
            PointAdded?.Invoke();
        }

        public void Initialize()
            => _reduceFigurine.ReducedOver += AddPoint;

        public void Dispose()
            => _reduceFigurine.ReducedOver -= AddPoint;
    }
}

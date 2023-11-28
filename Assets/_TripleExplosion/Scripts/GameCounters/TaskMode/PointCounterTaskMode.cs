using System;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounterTaskMode
    {
        public event Action<int> PointAdded;

        private float _modifier;
        private int _totalCounter;

        [Inject]
        private void Construct(TasksModeConfig tasksModeConfig)
            => _modifier = tasksModeConfig.PointForShip;

        public void AddPoint(int value)
        {
            _totalCounter += Mathf.RoundToInt(value * _modifier);
            PointAdded?.Invoke(_totalCounter);
        }
    }
}

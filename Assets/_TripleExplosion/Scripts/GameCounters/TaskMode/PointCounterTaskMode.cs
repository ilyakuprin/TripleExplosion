using System;
using UnityEngine;

namespace TripleExplosion
{
    public class PointCounterTaskMode : IPointCounter
    {
        public event Action<int> PointAdded;

        private readonly float _modifier;
        private int _totalCounter;

        public int TotalCounter { get => _totalCounter; }

        public PointCounterTaskMode(TasksModeConfig tasksModeConfig)
            => _modifier = tasksModeConfig.PointForShip;

        public void AddPoint(int value)
        {
            Add(Mathf.RoundToInt(value * _modifier));
            PointAdded?.Invoke(TotalCounter);
        }

        public void Add(int value)
        {
            _totalCounter += value;
        }
    }
}

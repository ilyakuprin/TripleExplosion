using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounterEndlessMode : IPointCounter, IInitializable, IDisposable
    {
        public event Action<int> PointAdded;

        [Inject] private readonly ReduceFigurine _reduceFigurine;
        [Inject] private readonly RemovingMatches _removingMatches;
        [Inject] private readonly BombSettings _bombSettings;
        private readonly int _rewardForFigurine = 1;

        private int _scoredPoints;
        private int _totalCounter;

        public int TotalCounter { get => _totalCounter; }

        private void CalculatePoints(List<Transform> figurines)
        {
            _scoredPoints += figurines.Count * _rewardForFigurine;
        }

        private void AddScoredPoint(List<Transform> _)
        {
            Add(_scoredPoints);
            _scoredPoints = 0;
            PointAdded?.Invoke(TotalCounter);
        }

        public void Add(int value)
        {
            _totalCounter += value;
        }

        public void Initialize()
        {
            _bombSettings.ListFilled += CalculatePoints;
            _removingMatches.MatchAdded += CalculatePoints;
            _reduceFigurine.ReducedOver += AddScoredPoint;
        }

        public void Dispose()
        {
            _bombSettings.ListFilled -= CalculatePoints;
            _removingMatches.MatchAdded -= CalculatePoints;
            _reduceFigurine.ReducedOver -= AddScoredPoint;
        }
    }
}

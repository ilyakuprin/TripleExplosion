using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounter : IInitializable, IDisposable
    {
        public event Action PointAdded;

        private readonly ReduceFigurine _reduceFigurine;
        private readonly RemovingMatches _removingMatches;
        private readonly BombSettings _bombSettings;
        private readonly int _rewardForFigurine = 1;

        private int _scoredPoints;
        private int _totalCounter;

        public PointCounter(ReduceFigurine reduceFigurine,
                            RemovingMatches removingMatches,
                            BombSettings bombSettings)
        {
            _reduceFigurine = reduceFigurine;
            _removingMatches = removingMatches;
            _bombSettings = bombSettings;
        }

        public int GetCounter { get => _totalCounter; }

        private void CalculatePoints(int count)
        {
            _scoredPoints += count * _rewardForFigurine;
        }

        private void AddPoint(List<Transform> _)
        {
            _totalCounter += _scoredPoints;
            _scoredPoints = 0;
            PointAdded?.Invoke();
        }

        public void Initialize()
        {
            _bombSettings.ListFilled += CalculatePoints;
            _removingMatches.MatchAdded += CalculatePoints;
            _reduceFigurine.ReducedOver += AddPoint;
        }

        public void Dispose()
        {
            _bombSettings.ListFilled -= CalculatePoints;
            _removingMatches.MatchAdded -= CalculatePoints;
            _reduceFigurine.ReducedOver -= AddPoint;
        }
    }
}

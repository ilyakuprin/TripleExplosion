using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounterEndlessMode : IInitializable, IDisposable
    {
        public event Action<int> PointAdded;

        private readonly ReduceFigurine _reduceFigurine;
        private readonly RemovingMatches _removingMatches;
        private readonly BombSettings _bombSettings;
        private readonly int _rewardForFigurine = 1;

        private int _totalCounter;
        private int _scoredPoints;

        public PointCounterEndlessMode(ReduceFigurine reduceFigurine,
                                       RemovingMatches removingMatches,
                                       BombSettings bombSettings)
        {
            _reduceFigurine = reduceFigurine;
            _removingMatches = removingMatches;
            _bombSettings = bombSettings;
        }

        private void CalculatePoints(List<Transform> figurines)
        {
            _scoredPoints += figurines.Count * _rewardForFigurine;
        }

        private void AddScoredPoint(List<Transform> _)
        {
            _totalCounter += _scoredPoints;
            _scoredPoints = 0;
            PointAdded?.Invoke(_totalCounter);
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

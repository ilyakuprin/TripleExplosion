using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class TimerCounterEndlessMode : TimerCounter
    {
        private RemovingMatches _removingMatches;
        private ReduceFigurine _reduceFigurine;
        private BombSettings _bombSettings;
        private int _counterExtraTime;

        private readonly Dictionary<int, int> _extraTime = new Dictionary<int, int>()
        {
            { 3, 1 },
            { 4, 1 },
            { 5, 3 },
            { 6, 4 },
            { 7, 10 },
            { 9, 9 }
        };

        [Inject]
        private void Construct(RemovingMatches removingMatches,
                               ReduceFigurine reduceFigurine,
                               BombSettings bombSettings)
        {
            _removingMatches = removingMatches;
            _reduceFigurine = reduceFigurine;
            _bombSettings = bombSettings;
        }

        private void CalculateExtraTime(List<Transform> figurines)
        {
            int countFigurines = figurines.Count;

            if (_extraTime.ContainsKey(countFigurines))
                _counterExtraTime += _extraTime[countFigurines];
        }

        private void AddExtraTime(List<Transform> _)
        {
            Timer.AddTime(_counterExtraTime);
            _counterExtraTime = 0;
        }

        private void OnEnable()
        {
            _bombSettings.ListFilled += CalculateExtraTime;
            _removingMatches.MatchAdded += CalculateExtraTime;
            _reduceFigurine.ReducedOver += AddExtraTime;
            Timer.TimeUpdated += ChangeTimerUi;
            Timer.TimeOver += EnabledTimerOver;
        }

        private void OnDisable()
        {
            _bombSettings.ListFilled -= CalculateExtraTime;
            _removingMatches.MatchAdded -= CalculateExtraTime;
            _reduceFigurine.ReducedOver -= AddExtraTime;
            Timer.TimeUpdated -= ChangeTimerUi;
            Timer.TimeOver -= EnabledTimerOver;
        }
    }
}

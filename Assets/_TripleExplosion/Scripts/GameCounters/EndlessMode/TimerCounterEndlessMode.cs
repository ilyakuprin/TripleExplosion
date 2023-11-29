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
        private float _counterExtraTime;

        private readonly Dictionary<int, float> _extraTime = new Dictionary<int, float>();

        [Inject]
        private void Construct(RemovingMatches removingMatches,
                               ReduceFigurine reduceFigurine,
                               BombSettings bombSettings,
                               EndlessModeConfig endlessModeConfig)
        {
            _removingMatches = removingMatches;
            _reduceFigurine = reduceFigurine;
            _bombSettings = bombSettings;

            DictionaryForInspector[] dictionary = endlessModeConfig.GetDictionary();
            for (int i = 0; i < dictionary.Length; i++)
            {
                _extraTime.Add(dictionary[i].Key, dictionary[i].Value);
            }
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

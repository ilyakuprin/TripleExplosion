using System;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class TimerCounter : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasTimerOver;
        [SerializeField] private TextMeshProUGUI _textUi;
        [SerializeField] private float _startTime;
        private Timer _timer;
        private RemovingMatches _removingMatches;
        private ReduceFigurine _reduceFigurine;
        private int _counterExtraTime;

        private readonly int _minutesInHour = 60;

        private readonly Dictionary<int, int> _extraTime = new Dictionary<int, int>()
        {
            { 3, 5 },
            { 4, 6 },
            { 5, 10 },
            { 6, 12 },
            { 7, 15 },
            { 9, 9 },
        };

        [Inject]
        private void Construct(RemovingMatches removingMatches,
                               ReduceFigurine reduceFigurine)
        {
            _removingMatches = removingMatches;
            _reduceFigurine = reduceFigurine;
        }

        private void Awake()
        {
            _timer = new Timer(this);
            _timer.SetTime(_startTime);
        }

        private void Start()
            => _timer.StartTimer();

        private void CalculateExtraTime(int countFigurines)
        {
            if (_extraTime.ContainsKey(countFigurines))
                _counterExtraTime += _extraTime[countFigurines];
        }

        private void AddExtraTime(List<Transform> _)
        {
            _timer.AddTime(_counterExtraTime);
            _counterExtraTime = 0;
        }

        private void ChangeTimerUi(float time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);

            if (ts.Minutes < _minutesInHour)
                _textUi.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            else
                _textUi.text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void EnabledTimerOver()
            => _canvasTimerOver.SetActive(true);

        private void OnEnable()
        {
            _removingMatches.MatchAdded += CalculateExtraTime;
            _reduceFigurine.ReducedOver += AddExtraTime;
            _timer.TimeUpdated += ChangeTimerUi;
            _timer.TimeOver += EnabledTimerOver;
        }

        private void OnDisable()
        {
            _removingMatches.MatchAdded -= CalculateExtraTime;
            _reduceFigurine.ReducedOver -= AddExtraTime;
            _timer.TimeUpdated -= ChangeTimerUi;
            _timer.TimeOver -= EnabledTimerOver;
        }
    }
}

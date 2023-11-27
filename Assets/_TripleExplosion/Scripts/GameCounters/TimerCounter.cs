using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class TimerCounter : MonoBehaviour, IPause
    {
        [SerializeField] private GameObject _canvasTimerOver;
        [SerializeField] private TextMeshProUGUI _textUi;
        [SerializeField] private float _startTime;
        private Timer _timer;
        private RemovingMatches _removingMatches;
        private ReduceFigurine _reduceFigurine;
        private BombSettings _bombSettings;
        private int _counterExtraTime;

        private readonly int _minutesInHour = 60;

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

        private void Awake()
        {
            _timer = new Timer(this);
            _timer.SetTime(_startTime);
        }

        private void Start()
            => _timer.SetEnableTimer(true);

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

        public void Pause(bool value)
            => _timer.SetEnableTimer(!value);

        private void OnEnable()
        {
            _bombSettings.ListFilled += CalculateExtraTime;
            _removingMatches.MatchAdded += CalculateExtraTime;
            _reduceFigurine.ReducedOver += AddExtraTime;
            _timer.TimeUpdated += ChangeTimerUi;
            _timer.TimeOver += EnabledTimerOver;
        }

        private void OnDisable()
        {
            _bombSettings.ListFilled -= CalculateExtraTime;
            _removingMatches.MatchAdded -= CalculateExtraTime;
            _reduceFigurine.ReducedOver -= AddExtraTime;
            _timer.TimeUpdated -= ChangeTimerUi;
            _timer.TimeOver -= EnabledTimerOver;
        }
    }
}

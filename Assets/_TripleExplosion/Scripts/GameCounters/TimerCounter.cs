using System;
using TMPro;
using UnityEngine;

namespace TripleExplosion
{
    public class TimerCounter : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasTimerOver;
        [SerializeField] private TextMeshProUGUI _textUi;
        [SerializeField] private float _startTime;
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(this);
            _timer.SetTime(_startTime);
        }

        private void Start()
            => _timer.StartTimer();

        private void ChangeTimerUi(float time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);
            _textUi.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        }

        private void EnabledTimerOver()
            => _canvasTimerOver.SetActive(true);

        private void OnEnable()
        {
            _timer.TimeUpdated += ChangeTimerUi;
            _timer.TimeOver += EnabledTimerOver;
        }

        private void OnDisable()
        {
            _timer.TimeUpdated -= ChangeTimerUi;
            _timer.TimeOver -= EnabledTimerOver;
        }
    }
}

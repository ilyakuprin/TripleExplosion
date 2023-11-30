using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public abstract class TimerCounter : MonoBehaviour, IPause
    {
        [SerializeField] private GameObject _canvasTimerOver;
        [SerializeField] private TextMeshProUGUI _textUi;
        [SerializeField] private float _startTime;
        [Inject] private readonly GameBoardHandler _board;

        private readonly int _minutesInHour = 60;

        protected Timer Timer { get; private set; }

        private void Awake()
        {
            Timer = new Timer(this);
            Timer.SetTime(_startTime);
        }

        private void Start()
            => Timer.SetEnableTimer(true);

        protected void ChangeTimerUi(float time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);

            if (ts.Minutes < _minutesInHour)
                _textUi.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            else
                _textUi.text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        public void Pause(bool value)
            => Timer.SetEnableTimer(!value);

        protected void EnabledTimerOver()
        {
            _canvasTimerOver.SetActive(true);
            _board.SetActiveBoarde(false);
        }
    }
}

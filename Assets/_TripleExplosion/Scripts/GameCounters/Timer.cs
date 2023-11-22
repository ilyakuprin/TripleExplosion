using System;
using System.Collections;
using UnityEngine;

namespace TripleExplosion
{
    public class Timer
    {
        public event Action<float> TimeUpdated;
        public event Action TimeOver;

        public MonoBehaviour _monoBeh;
        private Coroutine _coroutineTimer;
        private float _time = 0;

        public Timer(MonoBehaviour monoBeh)
            => _monoBeh = monoBeh;

        public void SetTime(float startTime)
        {
            if (startTime > 0)
                _time = startTime;
            else
                throw new NotImplementedException("Time must be positive");
        }

        public void AddTime(float time)
        {
            if (time > 0)
                _time += time;
        }

        public void StartTimer()
            => _coroutineTimer = _monoBeh.StartCoroutine(UpdateTimer());

        public void StopTimer()
        {
            if (_coroutineTimer != null)
                _monoBeh.StopCoroutine(_coroutineTimer);
        }

        private IEnumerator UpdateTimer()
        {
            while (_time > 0)
            {
                _time -= Time.deltaTime;
                TimeUpdated?.Invoke(_time);
                yield return null;
            }

            TimeOver?.Invoke();
        }
    }
}

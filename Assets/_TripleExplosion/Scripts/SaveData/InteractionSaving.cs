using System;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class InteractionSaving : IInitializable, IDisposable
    {
        public event Action SaveDataReceived;

        private readonly TimerCounter _timer;

        public InteractionSaving (TimerCounter timer)
        {
            _timer = timer;
        }

        public void Initialize()
        {
            OnDataReceived();
            YandexGame.GetDataEvent += OnDataReceived;
            _timer.Timer.TimeOver += OnSave;
        }

        public void OnSave()
        {
            YandexGame.SaveProgress();
            OnDataReceived();
        }

        private void OnDataReceived()
        {
            SaveDataReceived?.Invoke();
        }

        public void Dispose()
        {
            YandexGame.GetDataEvent -= OnDataReceived;
            _timer.Timer.TimeOver -= OnSave;
        }
    }
}

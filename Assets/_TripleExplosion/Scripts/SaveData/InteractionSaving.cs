using System;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class InteractionSaving : IInitializable, IDisposable
    {
        public event Action SaveDataReceived;

        public void Initialize()
        {
            OnDataReceived();
            YandexGame.GetDataEvent += OnDataReceived;
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
        }
    }
}

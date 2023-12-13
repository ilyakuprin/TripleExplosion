using System;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class OpeningGame : IInitializable, IDisposable
    {
        private readonly YandexGame _yandexGame;

        public OpeningGame(YandexGame yandexGame)
            => _yandexGame = yandexGame;

        private void ResetTimerAd()
            => _yandexGame.ResetTimerFullAd();

        public void Initialize()
            => YandexGame.GetDataEvent += ResetTimerAd;

        public void Dispose()
            => YandexGame.GetDataEvent -= ResetTimerAd;
    }
}

using System;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class RewardAd : IInitializable, IDisposable
    {
        private readonly GettingExtraTime _gettingExtraTime;

        public RewardAd(GettingExtraTime gettingExtraTime)
        {
            _gettingExtraTime = gettingExtraTime;
        }

        private void Rewarded(int id)
        {
            if (id == 0)
            {
                _gettingExtraTime.ContinueGame();
            }
        }

        public void Initialize()
            => YandexGame.RewardVideoEvent += Rewarded;

        public void Dispose()
            => YandexGame.RewardVideoEvent -= Rewarded;
    }
}
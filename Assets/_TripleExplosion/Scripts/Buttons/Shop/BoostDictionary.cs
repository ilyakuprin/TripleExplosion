using System;
using System.Collections.Generic;
using YG;
using Zenject;

namespace TripleExplosion
{
    public enum Boost
    {
        Swap,
        Bomb,
        Paint,
        Mix
    }

    public class BoostDictionary : IInitializable
    {
        private GameParametersConfig _config;
        private Dictionary<Boost, int> _boostsCost;

        public BoostDictionary(GameParametersConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            _boostsCost = new Dictionary<Boost, int>()
            {
                { Boost.Swap, _config.SwapCost },
                { Boost.Bomb, _config.BombCost },
                { Boost.Paint, _config.PaintCost },
                { Boost.Mix, _config.MixCost }
            }; 
        }

        public int GetCost(Boost boost)
        {
            if (_boostsCost.ContainsKey(boost))
            {
                return _boostsCost[boost];
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        public void AddBoost(Boost boost, bool isSeveral)
        {
            if (boost == Boost.Swap)
            {
                if (isSeveral)
                    YandexGame.savesData.CountSwipe += _config.BuySeveral;
                else
                    YandexGame.savesData.CountSwipe++;
            }
            else if (boost == Boost.Bomb)
            {
                if (isSeveral)
                    YandexGame.savesData.CountBomb += _config.BuySeveral;
                else
                    YandexGame.savesData.CountBomb++;
            }
            else if (boost == Boost.Paint)
            {
                if (isSeveral)
                    YandexGame.savesData.CountPaint += _config.BuySeveral;
                else
                    YandexGame.savesData.CountPaint++;
            }
            else if (boost == Boost.Mix)
            {
                if (isSeveral)
                    YandexGame.savesData.CountMix += _config.BuySeveral;
                else
                    YandexGame.savesData.CountMix++;
            }
        }
    }
}
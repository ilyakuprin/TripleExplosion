using System;
using UnityEngine;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class ConversionPoints : IInitializable, IDisposable
     {
        public event Action MoneyAdded;

        private readonly IPointCounter _pointCounter;
        private readonly GameParametersConfig _parameters;
        private readonly TimerCounter _timer;

        public ConversionPoints(IPointCounter pointCounter,
                                GameParametersConfig parameters,
                                TimerCounter timer)
        {
            _pointCounter = pointCounter;
            _parameters = parameters;
            _timer = timer;
        }

        private void Convert()
        {
            int money = Mathf.RoundToInt(_pointCounter.TotalCounter * _parameters.MultiplyModifierMoney);
            YandexGame.savesData.Money += money;
            MoneyAdded?.Invoke();
        }

        public void Initialize()
        {
            _timer.Timer.TimeOver += Convert;
        }

        public void Dispose()
        {
            _timer.Timer.TimeOver -= Convert;
        }
    }
}
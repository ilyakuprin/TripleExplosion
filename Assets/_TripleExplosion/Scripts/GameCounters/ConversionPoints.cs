using System;
using UnityEngine;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class ConversionPoints : IInitializable, IDisposable
     {
        private readonly IPointCounter _pointCounter;
        private readonly GameParametersConfig _parameters;
        private readonly TimerCounter _timer;
        private readonly InteractionSaving _saving;

        public ConversionPoints(IPointCounter pointCounter,
                                GameParametersConfig parameters,
                                TimerCounter timer,
                                InteractionSaving saving)
        {
            _pointCounter = pointCounter;
            _parameters = parameters;
            _timer = timer;
            _saving = saving;
        }

        public void Convert()
        {
            int money = Mathf.RoundToInt(_pointCounter.TotalCounter * _parameters.MultiplyModifierMoney);
            YandexGame.savesData.Money += money;
            _saving.OnSave();
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
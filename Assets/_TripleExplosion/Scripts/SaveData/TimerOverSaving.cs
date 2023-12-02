using System;
using Zenject;

namespace TripleExplosion
{
    public class TimerOverSaving : IInitializable, IDisposable
    {
        private readonly TimerCounter _timer;
        private readonly InteractionSaving _saving;

        public TimerOverSaving(TimerCounter timer,
                               InteractionSaving saving)
        {
            _timer = timer;
            _saving = saving;
        }

        public void Initialize()
        {
            _timer.Timer.TimeOver += _saving.OnSave;
        }

        public void Dispose()
        {
            _timer.Timer.TimeOver -= _saving.OnSave;
        }
    }
}
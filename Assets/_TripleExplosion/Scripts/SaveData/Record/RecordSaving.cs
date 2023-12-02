using System;
using Zenject;

namespace TripleExplosion
{
    public abstract class RecordSaving : IInitializable, IDisposable
    {
        private readonly IPointCounter _pointCounter;
        private readonly TimerCounter _timer;
        private readonly InteractionSaving _saving;

        public RecordSaving(IPointCounter pointCounter,
                            TimerCounter timer,
                            InteractionSaving saving)
        {
            _pointCounter = pointCounter;
            _timer = timer;
            _saving = saving;
        }

        protected float TotalTime { get => _timer.Timer.TotalTime; }

        protected abstract void ChangeRecord(int totalValue);

        protected void Save()
            => _saving.OnSave();

        public void Initialize()
            => _pointCounter.PointAdded += ChangeRecord;

        public void Dispose()
            => _pointCounter.PointAdded -= ChangeRecord;
    }
}
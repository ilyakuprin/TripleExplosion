using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public abstract class BestRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bestRecord;
        [Inject] private readonly InteractionSaving _saving;
        [Inject] private readonly IPointCounter _counter;

        protected int GetCurrentValue { get => _counter.TotalCounter; }

        private int _maxValue;

        protected int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                if (value >= 0)
                    _maxValue = value;
            }
        }

        protected void SetText(int value)
            => _bestRecord.text = string.Format("{0:0000}", value);

        protected abstract void View();

        protected void ChangeBestRecord(int totalValue)
        {
            if (totalValue > _maxValue)
                SetText(totalValue);
        }

        private void OnEnable()
        {
            _saving.SaveDataReceived += View;
            _counter.PointAdded += ChangeBestRecord;
        }

        private void OnDisable()
        {
            _saving.SaveDataReceived -= View;
            _counter.PointAdded -= ChangeBestRecord;
        }
    }
}
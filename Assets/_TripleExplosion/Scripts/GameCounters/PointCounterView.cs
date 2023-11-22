using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterUi;
        private PointCounter _pointCounter;

        [Inject]
        private void Construct(PointCounter pointCounter)
            => _pointCounter = pointCounter;

        private void ChangeCounter()
            => _counterUi.text = string.Format("{0:0000}", _pointCounter.GetCounter);

        private void OnEnable()
            => _pointCounter.PointAdded += ChangeCounter;

        private void OnDisable()
            => _pointCounter.PointAdded -= ChangeCounter;
    }
}

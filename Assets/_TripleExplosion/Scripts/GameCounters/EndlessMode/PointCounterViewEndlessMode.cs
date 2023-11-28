using TMPro;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class PointCounterViewEndlessMode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterUi;
        [Inject] private readonly PointCounterEndlessMode _pointCounter;

        private void ChangeCounter(int value)
            => _counterUi.text = string.Format("{0:0000}", value);

        private void OnEnable()
            => _pointCounter.PointAdded += ChangeCounter;

        private void OnDisable()
            => _pointCounter.PointAdded -= ChangeCounter;
    }
}

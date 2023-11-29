using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class NumberBoostsView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _buy;
        [SerializeField] private TextMeshProUGUI _counter;
        [Inject] private readonly BoostCounter _boostCounter;

        protected BoostCounter GetBoostCounter { get => _boostCounter; }

        protected void ChangeValue(int value)
        {
            if (value > 0)
            {
                if (_buy.activeInHierarchy)
                    SetActiveBoost(true);

                _counter.text = value.ToString();
            }
            else if (!_buy.activeInHierarchy)
                SetActiveBoost(false);
        }

        private void SetActiveBoost(bool value)
        {
            _button.interactable = value;
            _buy.SetActive(!value);
        }
    }
}

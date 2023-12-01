using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class BuyingBoost : MonoBehaviour
    {
        [SerializeField] private Boost _boost;
        [SerializeField] private Button _button;
        [SerializeField] private bool _isSeveral;
        [Inject] private readonly InteractionSaving _saving;
        [Inject] private readonly BoostDictionary _dictionaryCost;
        [Inject] private readonly GameParametersConfig _config;
        [Inject] private readonly ShopInteractingButtons _shopInteractingButtons;

        private void Buy()
        {
            int cost = _dictionaryCost.GetCost(_boost);

            if (_isSeveral)
                cost *= _config.BuySeveral;

            YandexGame.savesData.Money -= cost;

            _dictionaryCost.AddBoost(_boost, _isSeveral);

            _shopInteractingButtons.SetActiveButtons();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => Buy());
            _button.onClick.AddListener(() => _saving.OnSave());
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => Buy());
            _button.onClick.RemoveListener(() => _saving.OnSave());
        }

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }
    }
}
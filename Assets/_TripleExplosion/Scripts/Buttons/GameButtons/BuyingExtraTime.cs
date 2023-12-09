using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class BuyingExtraTime : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        private GameParametersConfig _config;
        private GettingExtraTime _gettingExtraTime;

        [Inject]
        private void Construct(GameParametersConfig config,
                               GettingExtraTime gettingExtraTime)
        {
            _config = config;
            _gettingExtraTime = gettingExtraTime;
        }

        private void Buy()
        {
            if (YandexGame.savesData.Money >= _config.BuyExtraTime)
            {
                YandexGame.savesData.Money -= _config.BuyExtraTime;
                _gettingExtraTime.ContinueGame();
            }
        }

        private void SetActive()
        {
            if (YandexGame.savesData.Money >= _config.BuyExtraTime)
                _buyButton.interactable = true;
            else
                _buyButton.interactable = false;
        }

        private void OnEnable()
        {
            SetActive();
            _buyButton.onClick.AddListener(() => Buy());
        }

        private void OnDisable()
            => _buyButton.onClick.RemoveAllListeners();

        private void OnValidate()
            => _buyButton ??= GetComponent<Button>();
    }
}
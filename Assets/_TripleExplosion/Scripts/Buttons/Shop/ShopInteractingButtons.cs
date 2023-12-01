using UnityEngine;
using Zenject;
using YG;

namespace TripleExplosion
{
    public class ShopInteractingButtons : MonoBehaviour
    {
        [SerializeField] private ShopButtons _swipe;
        [SerializeField] private ShopButtons _paint;
        [SerializeField] private ShopButtons _bomb;
        [SerializeField] private ShopButtons _mix;

        [Inject] private readonly GameParametersConfig _config;
        [Inject] private readonly InteractionSaving _saving;

        public void SetActiveButtons()
        {
            if (YandexGame.savesData.Money < _config.SwapCost)
            {
                _swipe.BuyOne.interactable = false;
                _swipe.BuySeveral.interactable = false;
            }
            if (YandexGame.savesData.Money < _config.SwapCost * _config.BuySeveral)
                _swipe.BuySeveral.interactable = false;

            if (YandexGame.savesData.Money < _config.PaintCost)
            {
                _paint.BuyOne.interactable = false;
                _paint.BuySeveral.interactable = false;
            }
            if(YandexGame.savesData.Money < _config.PaintCost * _config.BuySeveral)
                _paint.BuySeveral.interactable = false;

            if (YandexGame.savesData.Money < _config.BombCost)
            {
                _bomb.BuyOne.interactable = false;
                _bomb.BuySeveral.interactable = false;
            }
            if (YandexGame.savesData.Money < _config.BombCost * _config.BuySeveral)
                _bomb.BuySeveral.interactable = false;

            if (YandexGame.savesData.Money < _config.MixCost)
            {
                _mix.BuyOne.interactable = false;
                _mix.BuySeveral.interactable = false;
            }
            if(YandexGame.savesData.Money < _config.MixCost * _config.BuySeveral)
                _mix.BuySeveral.interactable = false;
        }

        private void OnEnable()
        {
            _saving.SaveDataReceived += SetActiveButtons;
        }

        private void OnDisable()
        {
            _saving.SaveDataReceived -= SetActiveButtons;
        }
    }
}
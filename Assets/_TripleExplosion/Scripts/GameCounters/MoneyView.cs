using TMPro;
using UnityEngine;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;
        [Inject] private readonly ConversionPoints _conversionPoints;
        [Inject] private readonly InteractionSaving _saving;

        private void View()
            => _money.text = string.Format("{0:0000000}", YandexGame.savesData.Money);

        private void OnEnable()
        {
            _conversionPoints.MoneyAdded += View;
            _saving.SaveDataReceived += View;
        }

        private void OnDisable()
        {
            _conversionPoints.MoneyAdded -= View;
            _saving.SaveDataReceived -= View;
        }
    }
}
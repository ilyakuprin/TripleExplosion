using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class MixingButton : MonoBehaviour
    {
        public event Action MixUsed;

        [SerializeField] private Button _button;
        private MixingSettings _mixingSettings;
        private GameBoardHandler _board;
        private DisablingOtherBoosts _boostsButtonsHandler;

        [Inject]
        private void Construct(MixingSettings mixingSettings,
                               GameBoardHandler board,
                               DisablingOtherBoosts boostsButtonsHandler)
        {
            _mixingSettings = mixingSettings;
            _board = board;
            _boostsButtonsHandler = boostsButtonsHandler;
        }

        private void OnTryMix()
        {
            if (_board.IsBoardAcrive)
            {
                _boostsButtonsHandler.DisableOtherBoosts();
                _mixingSettings.Mix();
                MixUsed?.Invoke();
            }
        }

        private void OnEnable()
            => _button.onClick.AddListener(OnTryMix);

        private void OnDisable()
            => _button.onClick.RemoveListener(OnTryMix);

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

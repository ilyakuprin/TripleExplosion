using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class MixingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private MixingSettings _mixingSettings;
        private GameBoardHandler _board;

        [Inject]
        private void Construct(MixingSettings mixingSettings,
                               GameBoardHandler board)
        {
            _mixingSettings = mixingSettings;
            _board = board;
        }

        private void OnTryMix()
        {
            if (_board.IsBoardAcrive)
                _mixingSettings.Mix();
        }

        private void OnEnable()
            => _button.onClick.AddListener(OnTryMix);

        private void OnDisable()
            => _button.onClick.RemoveListener(OnTryMix);

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

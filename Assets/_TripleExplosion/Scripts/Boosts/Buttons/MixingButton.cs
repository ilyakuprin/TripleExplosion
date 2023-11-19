using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class MixingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private MixingSettings _mixingSettings;

        [Inject]
        private void Construct(MixingSettings mixingSettings)
            => _mixingSettings = mixingSettings;

        private void OnEnable()
            => _button.onClick.AddListener(_mixingSettings.OnMix);

        private void OnDisable()
            => _button.onClick.RemoveListener(_mixingSettings.OnMix);

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

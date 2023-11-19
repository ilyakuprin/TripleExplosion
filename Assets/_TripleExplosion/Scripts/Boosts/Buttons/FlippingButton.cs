using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class FlippingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private FlippingSetting _flipping;

        [Inject]
        private void Construct(FlippingSetting flipping)
            => _flipping = flipping;

        private void OnEnable()
            => _button.onClick.AddListener(_flipping.OnActivateBoost);

        private void OnDisable()
            => _button.onClick.RemoveListener(_flipping.OnActivateBoost);

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

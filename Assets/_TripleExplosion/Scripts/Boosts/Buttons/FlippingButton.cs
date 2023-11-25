using Zenject;

namespace TripleExplosion
{
    public class FlippingButton : BoostButton
    {
        [Inject] private readonly FlippingSetting _flipping;

        private void Awake() => SetBoost(_flipping);

        private void OnEnable()
            => GetButton.onClick.AddListener(() => GetBoost.SetActiveBoost(!_flipping.GetActiveBoost));

        private void OnDisable()
            => GetButton.onClick.RemoveListener(() => GetBoost.SetActiveBoost(!_flipping.GetActiveBoost));
    }
}

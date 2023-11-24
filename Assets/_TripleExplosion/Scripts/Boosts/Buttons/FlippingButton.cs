using Zenject;

namespace TripleExplosion
{
    public class FlippingButton : BoostButton
    {
        [Inject] private readonly FlippingSetting _flipping;

        private void Awake() => SetBoost(_flipping);
    }
}

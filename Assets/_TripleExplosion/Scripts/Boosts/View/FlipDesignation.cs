using Zenject;

namespace TripleExplosion
{
    public class FlipDesignation : ActiveBoostDesignation
    {
        [Inject] private readonly FlippingSetting _flip;

        private void Awake() => SetBoost(_flip);
    }
}

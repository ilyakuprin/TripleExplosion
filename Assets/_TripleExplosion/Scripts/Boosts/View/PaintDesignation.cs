using Zenject;

namespace TripleExplosion
{
    public class PaintDesignation : ActiveBoostDesignation
    {
        [Inject] private readonly ColorChangingSetting _paint;

        private void Awake() => SetBoost(_paint);
    }
}

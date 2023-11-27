using Zenject;

namespace TripleExplosion
{
    public class BombDesignation : ActiveBoostDesignation
    {
        [Inject] private readonly BombSettings _bomb;

        private void Awake() => SetBoost(_bomb);
    }
}

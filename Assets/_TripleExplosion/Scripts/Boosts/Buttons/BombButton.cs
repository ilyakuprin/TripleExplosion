using Zenject;

namespace TripleExplosion
{
    public class BombButton : BoostButton
    {
        [Inject] private readonly BombSettings _bombSettings;

        private void Awake() => SetBoost(_bombSettings);
    }
}

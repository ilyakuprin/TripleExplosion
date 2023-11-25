using Zenject;

namespace TripleExplosion
{
    public class BombButton : BoostButton
    {
        [Inject] private readonly BombSettings _bombSettings;

        private void Awake() => SetBoost(_bombSettings);

        private void OnEnable()
            => GetButton.onClick.AddListener(() => GetBoost.SetActiveBoost(!_bombSettings.GetActiveBoost));

        private void OnDisable()
            => GetButton.onClick.RemoveListener(() => GetBoost.SetActiveBoost(!_bombSettings.GetActiveBoost));
    }
}

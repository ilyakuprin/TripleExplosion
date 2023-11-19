using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class BombButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private BombSettings _bombSettings;

        [Inject]
        private void Construct(BombSettings bombSettings)
            => _bombSettings = bombSettings;

        private void OnEnable()
            => _button.onClick.AddListener(_bombSettings.OnEnableBoost);

        private void OnDisable()
            => _button.onClick.RemoveListener(_bombSettings.OnEnableBoost);

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

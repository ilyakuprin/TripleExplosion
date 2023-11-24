using UnityEngine;
using UnityEngine.UI;

namespace TripleExplosion
{
    public abstract class BoostButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IBoost _boost;

        public Button GetButton { get => _button; }
        public IBoost GetBoost { get => _boost; }

        protected void SetBoost(IBoost boost)
            => _boost = boost;

        private void OnEnable()
            => _button.onClick.AddListener(() => _boost.ChangeActiveBoost());

        private void OnDisable()
            => _button.onClick.RemoveListener(() => _boost.ChangeActiveBoost());

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

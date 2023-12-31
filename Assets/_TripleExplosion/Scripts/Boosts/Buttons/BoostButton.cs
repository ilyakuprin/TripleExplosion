using UnityEngine;
using UnityEngine.UI;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public abstract class BoostButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IBoost _boost;

        public Button GetButton { get => _button; }
        public IBoost GetBoost { get => _boost; }

        protected void SetBoost(IBoost boost)
            => _boost = boost;

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

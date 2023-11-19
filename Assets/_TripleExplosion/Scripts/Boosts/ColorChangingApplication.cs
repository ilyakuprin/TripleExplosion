using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class ColorChangingApplication : MonoBehaviour
    {
        [SerializeField] private SwapParent _swapParent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ColorChangingSetting _colorSetting;

        [Inject]
        private void Construct(ColorChangingSetting colorSetting)
            => _colorSetting = colorSetting;

        private void OnMouseDown()
            => _colorSetting.TryUsingBoost(_spriteRenderer,
                                           _swapParent.GetColumn,
                                           _swapParent.GetRow);

        private void OnEnable()
        {
            _swapParent ??= GetComponent<SwapParent>();
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }
    }
}

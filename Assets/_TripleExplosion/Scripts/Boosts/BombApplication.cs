using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class BombApplication : MonoBehaviour
    {
        [SerializeField] private SwapParent _swapParent;
        private BombSettings _bombSettings;

        [Inject]
        private void Construct(BombSettings bombSettings)
            => _bombSettings = bombSettings;

        private void OnMouseDown()
            => _bombSettings.TryUsingBoost(_swapParent.GetColumn,
                                           _swapParent.GetRow);

        private void OnEnable()
            => _swapParent ??= GetComponent<SwapParent>();
    }
}

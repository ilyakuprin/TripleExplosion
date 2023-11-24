using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Collider2D), typeof(SwapParent))]
    public class SwipeHandler : MonoBehaviour
    {
        [SerializeField] private SwapParent _swapParent;
        private GameBoardHandler _board;
        private SwipeMovementFigures _swipeMovementFigures;
        private Vector2 _touchDownPosition;
        private Vector2 _touchUpPosition;

        private readonly float _straightAngle = 180f;
        private readonly float _swipeError = 0.1f;

        [Inject]
        public void Construct(GameBoardHandler board,
                              SwipeMovementFigures swipeMovementFigures)
        {
            _board = board;
            _swipeMovementFigures = swipeMovementFigures;
        }

        private void OnMouseDown()
        {
            if (_board.IsBoardAcrive)
                _touchDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            if (_board.IsBoardAcrive)
            {
                _touchUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Mathf.Abs(Vector3.Distance(_touchDownPosition, _touchUpPosition)) > _swipeError)
                {
                    _board.SetActiveBoarde(false);

                    float swipeAngle = CalculeteAngleSwipe();
                    _swipeMovementFigures.SetParameters(_swapParent, swipeAngle);
                    _swapParent.ChangeParant(swipeAngle);
                }
            }
        }

        private float CalculeteAngleSwipe()
            => Mathf.Atan2(_touchUpPosition.y - _touchDownPosition.y,
                            _touchUpPosition.x - _touchDownPosition.x)
                            * _straightAngle / Mathf.PI;

        private void OnValidate()
            => _swapParent ??= GetComponent<SwapParent>();
    }
}

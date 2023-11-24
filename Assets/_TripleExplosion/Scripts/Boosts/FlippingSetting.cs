using System;
using Zenject;

namespace TripleExplosion
{
    public class FlippingSetting : IInitializable, IDisposable, IBoost
    {
        private readonly SwipeMovementFigures _swipeMovementFigures;
        private bool _isActive = false;

        public FlippingSetting(SwipeMovementFigures swipeMovementFigures)
            => _swipeMovementFigures = swipeMovementFigures;

        public bool GetActiveBoost { get => _isActive; }

        public void SetActiveBoost(bool value)
        {
            _isActive = value;
            _swipeMovementFigures.SetReverseSwipe(!value);
        }

        public void ChangeActiveBoost()
            => SetActiveBoost(!_isActive);

        private void DisableBoost()
            => SetActiveBoost(false);

        public void Initialize()
            => _swipeMovementFigures.ReverseSwipeUsed += DisableBoost;

        public void Dispose()
            => _swipeMovementFigures.ReverseSwipeUsed -= DisableBoost;
    }
}

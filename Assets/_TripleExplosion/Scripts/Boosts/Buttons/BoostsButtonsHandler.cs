using System;
using Zenject;

namespace TripleExplosion
{
    public class BoostsButtonsHandler : IInitializable, IDisposable
    {
        private readonly BoostButton[] _buttons;

        public BoostsButtonsHandler(BoostButton[] boostButtons)
            => _buttons = boostButtons;

        private void DisableOtherBoosts(IBoost boostButton)
        {
            foreach (BoostButton button in _buttons)
            {
                if (button.GetBoost != boostButton)
                    button.GetBoost.SetActiveBoost(false);
            }
        }

        public void Initialize()
        {
            foreach (BoostButton button in _buttons)
                button.GetButton.onClick.AddListener(()
                    => DisableOtherBoosts(button.GetBoost));

            UnityEngine.Debug.Log(_buttons.Length);
        }

        public void Dispose()
        {
            foreach (BoostButton button in _buttons)
                button.GetButton.onClick.RemoveListener(()
                    => DisableOtherBoosts(button.GetBoost));
        }
    }
}

using System;
using Zenject;

namespace TripleExplosion
{
    public class BoostCounter : IInitializable, IDisposable
    {
        private readonly InteractionSaving _saving;
        private readonly MixingButton _mixing;
        private readonly BombSettings _bomb;
        private readonly SwipeMovementFigures _swipe;
        private readonly ColorChangingSetting _colorChanging;

        public BoostCounter(InteractionSaving saving,
                            MixingButton mixing,
                            BombSettings bomb,
                            SwipeMovementFigures swipe,
                            ColorChangingSetting colorChanging)
        {
            _saving = saving;
            _mixing = mixing;
            _bomb = bomb;
            _swipe = swipe;
            _colorChanging = colorChanging;
        }

        private void SubtractMix()
        {
            _saving.SaveData.CountMix--;
            _saving.Save();
        }

        private void SubtractBomb()
        {
            _saving.SaveData.CountBomb--;
            _saving.Save();
        }

        private void SubtractSwipe()
        {
            _saving.SaveData.CountSwipe--;
            _saving.Save();
        }

        private void SubtractColorChanging()
        {
            _saving.SaveData.CountPaint--;
            _saving.Save();
        }

        public void Initialize()
        {
            _mixing.MixUsed += SubtractMix;
            _bomb.BombUsed += SubtractBomb;
            _swipe.ReverseSwipeUsed += SubtractSwipe;
            _colorChanging.ColorChangeUsed += SubtractColorChanging;
        }

        public void Dispose()
        {
            _mixing.MixUsed -= SubtractMix;
            _bomb.BombUsed -= SubtractBomb;
            _swipe.ReverseSwipeUsed -= SubtractSwipe;
            _colorChanging.ColorChangeUsed -= SubtractColorChanging;
        }
    }
}

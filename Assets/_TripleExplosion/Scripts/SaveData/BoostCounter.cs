using System;
using Zenject;

namespace TripleExplosion
{
    public class BoostCounter : IInitializable, IDisposable
    {
        public event Action<int> MixChanged, SwipeChanged, BombChanged, PaintChanged;

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
            MixChanged?.Invoke(_saving.SaveData.CountMix);
        }

        private void SubtractBomb()
        {
            _saving.SaveData.CountBomb--;
            _saving.Save();
            BombChanged?.Invoke(_saving.SaveData.CountBomb);
        }

        private void SubtractSwipe()
        {
            _saving.SaveData.CountSwipe--;
            _saving.Save();
            SwipeChanged?.Invoke(_saving.SaveData.CountSwipe);
        }

        private void SubtractPaint()
        {
            _saving.SaveData.CountPaint--;
            _saving.Save();
            PaintChanged?.Invoke(_saving.SaveData.CountPaint);
        }

        private void InitializeSaveData()
        {
            MixChanged?.Invoke(_saving.SaveData.CountMix);
            BombChanged?.Invoke(_saving.SaveData.CountBomb);
            SwipeChanged?.Invoke(_saving.SaveData.CountSwipe);
            PaintChanged?.Invoke(_saving.SaveData.CountPaint);
        }

        public void Initialize()
        {
            _saving.SaveDataReceived += InitializeSaveData;

            _mixing.MixUsed += SubtractMix;
            _bomb.BombUsed += SubtractBomb;
            _swipe.ReverseSwipeUsed += SubtractSwipe;
            _colorChanging.ColorChangeUsed += SubtractPaint;
        }

        public void Dispose()
        {
            _saving.SaveDataReceived -= InitializeSaveData;

            _mixing.MixUsed -= SubtractMix;
            _bomb.BombUsed -= SubtractBomb;
            _swipe.ReverseSwipeUsed -= SubtractSwipe;
            _colorChanging.ColorChangeUsed -= SubtractPaint;
        }
    }
}

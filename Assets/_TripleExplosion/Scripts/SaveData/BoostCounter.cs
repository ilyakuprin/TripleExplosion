using System;
using Zenject;
using YG;

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
            YandexGame.savesData.CountMix--;
            MixChanged?.Invoke(YandexGame.savesData.CountMix);
        }

        private void SubtractBomb()
        {
            YandexGame.savesData.CountBomb--;
            BombChanged?.Invoke(YandexGame.savesData.CountBomb);
        }

        private void SubtractSwipe()
        {
            YandexGame.savesData.CountSwipe--;
            SwipeChanged?.Invoke(YandexGame.savesData.CountSwipe);
        }

        private void SubtractPaint()
        {
            YandexGame.savesData.CountPaint--;
            PaintChanged?.Invoke(YandexGame.savesData.CountPaint);
        }

        private void InitializeSaveData()
        {
            MixChanged?.Invoke(YandexGame.savesData.CountMix);
            BombChanged?.Invoke(YandexGame.savesData.CountBomb);
            SwipeChanged?.Invoke(YandexGame.savesData.CountSwipe);
            PaintChanged?.Invoke(YandexGame.savesData.CountPaint);
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

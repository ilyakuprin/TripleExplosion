using Zenject;
using YG;

namespace TripleExplosion
{
    public class MainMenuRecordEndlessModeView : MainMenuRecordView
    {
        [Inject] private readonly InteractionSaving _saving;

        private void SendView()
            => View(YandexGame.savesData.RecordEndlessMode, YandexGame.savesData.TimeEndlessMode);

        private void OnEnable()
            => _saving.SaveDataReceived += SendView;

        private void OnDisable()
            => _saving.SaveDataReceived -= SendView;
    }
}
using Zenject;
using YG;

namespace TripleExplosion
{
    public class MainMenuRecordTaskModeView : MainMenuRecordView
    {
        [Inject] private readonly InteractionSaving _saving;

        private void SendView()
            => View(YandexGame.savesData.RecordTaskMode, YandexGame.savesData.TimeTaskMode);

        private void OnEnable()
            => _saving.SaveDataReceived += SendView;

        private void OnDisable()
            => _saving.SaveDataReceived -= SendView;
    }
}
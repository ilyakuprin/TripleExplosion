using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class GameSceneLoader : MonoBehaviour
    {
        private InteractionSaving _saving;
        private ConversionPoints _conversionPoints;
        private SavingResultLb _savingResultLb;

        [Inject]
        private void Construct(InteractionSaving saving,
                               ConversionPoints conversionPoints,
                               SavingResultLb savingResultLb)
        {
            _saving = saving;
            _conversionPoints = conversionPoints;
            _savingResultLb = savingResultLb;
        }

        private void Start()
        {
            YandexGame.FullscreenShow();
        }

        public void OnLoadMenu()
        {
            _conversionPoints.Convert();
            _saving.OnSave();
            _savingResultLb.UpdateLb();
            SceneManager.LoadScene(0);
        }

        private void OnApplicationQuit()
        {
            _savingResultLb.UpdateLb();
            _conversionPoints.Convert();
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class GameSceneLoader : MonoBehaviour
    {
        [Inject] private readonly InteractionSaving _saving;

        private void Awake()
        {
            YandexGame.FullscreenShow();
        }

        public void OnLoadMenu()
        {
            _saving.OnSave();
            SceneManager.LoadScene(0);
        }

        //The save will take effect after the timeout screen appears.
        public void OnReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripleExplosion
{
    public class SceneLoader : MonoBehaviour
    {
        public void OnLoadMenu()
            => SceneManager.LoadScene(0);

        public void OnLoadEndlessMode()
            => SceneManager.LoadScene(1);

        public void OnLoadTaskMode()
            => SceneManager.LoadScene(2);

        public void OnReloadCurrentScene()
            => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

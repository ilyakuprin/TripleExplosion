using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripleExplosion
{
    public class MainMenuSceneLoader : MonoBehaviour
    {
        public void OnLoadEndlessMode()
            => SceneManager.LoadScene(1);

        public void OnLoadTaskMode()
            => SceneManager.LoadScene(2);
    }
}
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasPause;
        [SerializeField] private Button _button;
        [Inject] private readonly Pause _pause;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => _canvasPause.SetActive(true));
            _button.onClick.AddListener(() => _pause.OnSetPause(true));
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => _canvasPause.SetActive(true));
            _button.onClick.RemoveListener(() => _pause.OnSetPause(true));
        }

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class ReturnToGame : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Button _button;
        [Inject] private readonly Pause _pause;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => _canvas.SetActive(false));
            _button.onClick.AddListener(() => _pause.OnSetPause(false));
        }

        private void OnDisable()
        {
            /*_button.onClick.RemoveListener(() => _canvas.SetActive(false));
            _button.onClick.RemoveListener(() => _pause.OnSetPause(false));*/

            _button.onClick.RemoveAllListeners();
        }

        private void OnValidate()
            => _button ??= GetComponent<Button>();

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

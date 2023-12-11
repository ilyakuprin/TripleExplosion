using UnityEngine;
using UnityEngine.UI;

namespace TripleExplosion
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonsAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button[] _buttons;

        private void PlayAudio()
            => _audioSource.Play();

        private void OnEnable()
        {
            foreach (Button b in _buttons)
                b.onClick.AddListener(PlayAudio);
        }

        private void OnDisable()
        {
            foreach (Button b in _buttons)
                b.onClick.RemoveListener(PlayAudio);
        }

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
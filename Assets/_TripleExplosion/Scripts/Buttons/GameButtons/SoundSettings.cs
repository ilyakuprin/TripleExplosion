using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TripleExplosion
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _slider;
        private readonly string _parameterMusic = "Music";
        private readonly float _minValue = 0.001f;
        private readonly float _unmuteValue = 0.5f;
        private bool _mute;

        public void OnSetValue(float value)
        {
            if (value > _minValue)
            {
                _audioMixer.SetFloat(_parameterMusic, Mathf.Log10(value) * 20);
                _mute = false;
            }
            else
            {
                _audioMixer.SetFloat(_parameterMusic, Mathf.Log10(_minValue) * 20);
                _mute = true;
            }
        }

        public void OnPressIconSound()
        {
            if (_mute)
            {
                _slider.value = _unmuteValue;
                OnSetValue(_unmuteValue);
            }
            else
            {
                _slider.value = _slider.minValue;
                OnSetValue(_minValue);
            }
        }
    }
}

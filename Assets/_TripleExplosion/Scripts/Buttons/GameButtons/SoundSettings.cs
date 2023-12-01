using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class SoundSettings : MonoBehaviour
    {
        [Inject] private readonly InteractionSaving _save;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _slider;
        private readonly string _parameterMusic = "Music";
        private readonly float _minValue = 0.001f;
        private readonly float _unmuteValue = 0.5f;
        private bool _mute;

        public void OnSetValue(float value)
        {
            float volume;

            if (value > _minValue)
            {
                volume = Mathf.Log10(value) * 20;
                _audioMixer.SetFloat(_parameterMusic, volume);
                _mute = false;
            }
            else
            {
                volume = Mathf.Log10(_minValue) * 20;
                _audioMixer.SetFloat(_parameterMusic, volume);
                _mute = true;
            }

            YandexGame.savesData.Volume = value;
        }

        private void View()
        {
            OnSetValue(YandexGame.savesData.Volume);
            _slider.value = YandexGame.savesData.Volume;
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

        private void OnEnable()
        {
            _save.SaveDataReceived += View;
        }

        private void OnDisable()
        {
            _save.SaveDataReceived -= View;
        }
    }
}

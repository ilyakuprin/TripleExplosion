using UnityEngine;

namespace TripleExplosion
{
    [RequireComponent(typeof(AudioSource))]
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _clips;

        private Timer _timer;
        private readonly System.Random _random = new System.Random();

        private void Awake()
            => _timer = new Timer(this);

        private void Start()
            => ChangeSound();

        private void ChangeSound()
        {
            SetRandomClip();

            _timer.SetTime(_source.clip.length);
            _timer.SetEnableTimer(false);

            _source.Play();
            _timer.SetEnableTimer(true);
        }

        private void SetRandomClip()
        {
            int index = _random.Next(_clips.Length);

            if (_clips[index] == _source.clip)
                index = (index + 1) / _clips.Length;

            _source.clip = _clips[index];
        }

        private void OnEnable()
            => _timer.TimeOver += ChangeSound;

        private void OnDisable()
            => _timer.TimeOver -= ChangeSound;

        private void OnValidate()
            => _source ??= GetComponent<AudioSource>();
    }
}

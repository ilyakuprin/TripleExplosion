using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(AudioSource))]
    public class MatchAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private ReduceFigurine _reduceFigurine;

        [Inject]
        private void Construct(ReduceFigurine reduceFigurine)
            => _reduceFigurine = reduceFigurine;

        private void PlayAudio()
            => _audioSource.Play();

        private void OnEnable()
            => _reduceFigurine.ReducedStarted += PlayAudio;

        private void OnDisable()
            => _reduceFigurine.ReducedStarted -= PlayAudio;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
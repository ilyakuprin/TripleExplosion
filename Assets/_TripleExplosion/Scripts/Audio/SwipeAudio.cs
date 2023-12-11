using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(AudioSource))]
    public class SwipeAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private SwipeMovementFigures _movingFigurines;

        [Inject]
        private void Construct(SwipeMovementFigures movingFigurines)
            => _movingFigurines = movingFigurines;

        private void PlayAudio()
            => _audioSource.Play();

        private void OnEnable()
            => _movingFigurines.GetMovingFigurines.MovementStarted += PlayAudio;

        private void OnDisable()
            => _movingFigurines.GetMovingFigurines.MovementStarted -= PlayAudio;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
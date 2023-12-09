using System.Collections;
using UnityEngine;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class WaitingLoadAd : MonoBehaviour
    {
        [Tooltip("Объект, который будет активироваться перед открытием рекламы. И деактивироваться при открытии.")]
        [SerializeField] private GameObject _notificationObj;
        [Min(1), Tooltip("Максимальное время показа объекта нотификации. Если реклама так и не будет показана, то объект скроется через указанное в данном параметре время.")]
        [SerializeField] private float _waitingForAds = 2;

        private Coroutine _closeNotification;
        private Pause _pause;

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;
        }

        private void Awake()
        {
            _notificationObj.SetActive(false);
        }

        private void OnEnable()
        {
            YandexGame.onAdNotification += OnAdNotification;
            YandexGame.OpenFullAdEvent += OnOpenAd;
            YandexGame.OpenVideoEvent += OnOpenAd;
        }

        private void OnDisable()
        {
            YandexGame.onAdNotification -= OnAdNotification;
            YandexGame.OpenFullAdEvent -= OnOpenAd;
            YandexGame.OpenVideoEvent -= OnOpenAd;
        }

        private void OnAdNotification()
        {
            YandexGame.OpenFullAdEvent?.Invoke();
            _notificationObj.SetActive(true);
            _pause.OnSetPause(true);
            _closeNotification = StartCoroutine(CloseNotification());
        }

        private IEnumerator CloseNotification()
        {
            yield return new WaitForSecondsRealtime(_waitingForAds);
            _notificationObj.SetActive(false);
            _pause.OnSetPause(false);
            YandexGame.CloseFullAdEvent?.Invoke();
        }

        private void OnOpenAd()
        {
            _notificationObj.SetActive(false);
            _pause.OnSetPause(false);
            if (_closeNotification != null)
                StopCoroutine(_closeNotification);
        }
    }
}
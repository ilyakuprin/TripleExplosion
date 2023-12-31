using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class GettingExtraTime : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasTimerOver;
        [Inject] private readonly GameParametersConfig _config;
        [Inject] private readonly TimerCounter _timer;
        [Inject] private readonly Pause _pause;
        [Inject] private readonly InteractionSaving _saving;
        [Inject] private readonly GameBoardHandler _board;

        public void ContinueGame()
        {
            _timer.Timer.AddTime(_config.ExtraTimeSeconds);
            _pause.OnSetPause(false);
            _board.SetActiveBoarde(true);
            _canvasTimerOver.SetActive(false);
            _saving.OnSave();
        }
    }
}
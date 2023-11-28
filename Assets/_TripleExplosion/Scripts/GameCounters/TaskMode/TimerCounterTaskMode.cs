using Zenject;

namespace TripleExplosion
{
    public class TimerCounterTaskMode : TimerCounter
    {
        private float _modifier;

        [Inject]
        private void Construct(TasksModeConfig tasksModeConfig)
            => _modifier = tasksModeConfig.TimeForShip;

        public void AddExtraTime(int value)
            => Timer.AddTime(value * _modifier);

        private void OnEnable()
        {
            Timer.TimeUpdated += ChangeTimerUi;
            Timer.TimeOver += EnabledTimerOver;
        }

        private void OnDisable()
        {
            Timer.TimeUpdated -= ChangeTimerUi;
            Timer.TimeOver -= EnabledTimerOver;
        }
    }
}

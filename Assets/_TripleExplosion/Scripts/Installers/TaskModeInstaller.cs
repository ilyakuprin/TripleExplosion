using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class TaskModeInstaller : MonoInstaller
    {
        [SerializeField] private TimerCounterTaskMode _timer;
        [SerializeField] private TasksModeConfig _tasksModeConfig;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            Container.Bind<TasksModeConfig>().FromInstance(_tasksModeConfig).AsSingle();
            Container.Bind<TimerCounterTaskMode>().FromInstance(_timer).AsSingle();
            Container.Bind<TimerCounter>().FromInstance(_timer).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<FormationTasks>().AsSingle();
            Container.BindInterfacesAndSelfTo<PointCounterTaskMode>().AsSingle();
        }
    }
}

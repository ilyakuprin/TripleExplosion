using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class EndlessModeInstaller : MonoInstaller
    {
        [SerializeField] private EndlessModeConfig _config;
        [SerializeField] private TimerCounterEndlessMode _timer;

        public override void InstallBindings()
        {
            Container.Bind<EndlessModeConfig>().FromInstance(_config).AsSingle();
            Container.Bind<TimerCounter>().FromInstance(_timer).AsSingle();
            Container.BindInterfacesAndSelfTo<PointCounterEndlessMode>().AsSingle();
            Container.BindInterfacesAndSelfTo<RecordSavingEndlessMode>().AsSingle();
        }
    }
}

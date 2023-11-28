using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class EndlessModeInstaller : MonoInstaller
    {
        [SerializeField] private TimerCounterEndlessMode _timer;

        public override void InstallBindings()
        {
            Container.Bind<TimerCounter>().FromInstance(_timer).AsSingle();
            Container.BindInterfacesAndSelfTo<PointCounterEndlessMode>().AsSingle();
        }
    }
}

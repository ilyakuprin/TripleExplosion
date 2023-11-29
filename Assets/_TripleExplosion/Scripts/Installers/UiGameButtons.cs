using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class UiGameButtons : MonoInstaller
    {
        [SerializeField] private PauseButton[] _pause;
        [SerializeField] private TimerCounter _timer;
        [SerializeField] private ReturnToGame[] _returnToGames;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            foreach (PauseButton pause in _pause)
                Container.Bind<PauseButton>().FromInstance(pause).AsTransient();

            foreach (ReturnToGame toGame in _returnToGames)
                Container.Bind<ReturnToGame>().FromInstance(toGame).AsTransient();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<Pause>().AsSingle();
        }
    }
}

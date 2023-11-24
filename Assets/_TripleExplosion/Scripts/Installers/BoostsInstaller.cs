using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class BoostsInstaller : MonoInstaller
    {
        [SerializeField] private BoostButton[] _boostButton;

        public override void InstallBindings()
        {
            BindInterfaces();

            foreach (BoostButton boost in _boostButton)
                Container.Bind<BoostButton>().FromInstance(boost).AsSingle();

            Container.Bind<BoostButton[]>().FromInstance(_boostButton).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<FlippingSetting>().AsSingle();
            Container.BindInterfacesAndSelfTo<ColorChangingSetting>().AsSingle();
            Container.BindInterfacesAndSelfTo<BombSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MixingSettings>().AsSingle();
        }
    }
}

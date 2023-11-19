using Zenject;

namespace TripleExplosion
{
    public class BoostsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInterfaces();
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

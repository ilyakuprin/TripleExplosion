using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class BoostsInstaller : MonoInstaller
    {
        [SerializeField] private BoostButton[] _boostButton;
        [SerializeField] private MixingButton _mixingButton;
        [SerializeField] private ActiveBoostDesignation[] _boostsDesignation;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            foreach (BoostButton boost in _boostButton)
                Container.Bind<BoostButton>().FromInstance(boost).AsTransient();

            Container.Bind<MixingButton>().FromInstance(_mixingButton).AsSingle();

            foreach (ActiveBoostDesignation boost in _boostsDesignation)
                Container.Bind<ActiveBoostDesignation>().FromInstance(boost).AsTransient();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<FlippingSetting>().AsSingle();
            Container.BindInterfacesAndSelfTo<ColorChangingSetting>().AsSingle();
            Container.BindInterfacesAndSelfTo<BombSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MixingSettings>().AsSingle();

            Container.BindInterfacesAndSelfTo<DisablingOtherBoosts>().AsSingle();
        }
    }
}

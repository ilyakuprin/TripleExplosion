using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameParametersConfig _parameters;
        [SerializeField] private ShopInteractingButtons _shopInteractingButtons;

        public override void InstallBindings()
        {
            Container.Bind<GameParametersConfig>().FromInstance(_parameters).AsSingle();
            Container.Bind<ShopInteractingButtons>().FromInstance(_shopInteractingButtons).AsSingle();
            Container.BindInterfacesAndSelfTo<InteractionSaving>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoostDictionary>().AsSingle();
        }
    }
}
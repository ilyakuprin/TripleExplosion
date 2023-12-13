using UnityEngine;
using YG;
using Zenject;

namespace TripleExplosion
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameParametersConfig _parameters;
        [SerializeField] private ShopInteractingButtons _shopInteractingButtons;
        [SerializeField] private YandexGame _yandexGame;

        public override void InstallBindings()
        {
            Container.Bind<GameParametersConfig>().FromInstance(_parameters).AsSingle();
            Container.Bind<ShopInteractingButtons>().FromInstance(_shopInteractingButtons).AsSingle();
            Container.Bind<YandexGame>().FromInstance(_yandexGame).AsSingle();
            Container.BindInterfacesAndSelfTo<InteractionSaving>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoostDictionary>().AsSingle();
            Container.BindInterfacesAndSelfTo<OpeningGame>().AsSingle();
        }
    }
}
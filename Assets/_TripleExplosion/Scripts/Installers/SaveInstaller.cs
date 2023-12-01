using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class SaveInstaller : MonoInstaller
    {
        [SerializeField] private ShopInteractingButtons _shopInteractingButtons;

        public override void InstallBindings()
        {
            Container.Bind<ShopInteractingButtons>().FromInstance(_shopInteractingButtons).AsSingle();

            BindInterfaces();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<BoostCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<InteractionSaving>().AsSingle();
        }
    }
}

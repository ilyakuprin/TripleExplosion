using Zenject;
using UnityEngine;

namespace TripleExplosion
{
    public class SaveInstaller : MonoInstaller
    {
        [SerializeField] private InteractionSaving _interactionSaving;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            Container.Bind<InteractionSaving>().FromInstance(_interactionSaving).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<BoostCounter>().AsSingle();
        }
    }
}

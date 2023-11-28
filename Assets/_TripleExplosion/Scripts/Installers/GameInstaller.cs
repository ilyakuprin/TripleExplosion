using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameBoardConfig _gameBoardConfig;
        [SerializeField] private FigurinesConfig _figurinesConfig;
        [SerializeField] private Transform _parent;
        [SerializeField] private ReduceFigurine _reduceFigurine;
        [SerializeField] private Falling _falling;
        [SerializeField] private SwipeMovementFigures _swipeMovementFigures;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
            AssignInitializationOrder();
        }

        private void BindSerializeField()
        {
            Container.Bind<GameBoardConfig>().FromInstance(_gameBoardConfig).AsSingle();
            Container.Bind<FigurinesConfig>().FromInstance(_figurinesConfig).AsSingle();
            Container.Bind<Transform>().FromInstance(_parent).AsSingle();
            Container.Bind<ReduceFigurine>().FromInstance(_reduceFigurine).AsSingle();
            Container.Bind<Falling>().FromInstance(_falling).AsSingle();
            Container.Bind<SwipeMovementFigures>().FromInstance(_swipeMovementFigures).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<GameBoardHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawningFigures>().AsSingle();
            Container.BindInterfacesAndSelfTo<SearchMatches>().AsSingle();
            Container.BindInterfacesAndSelfTo<RemovingMatches>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckingAllMoves>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChangingStartingFigures>().AsSingle();
            Container.BindInterfacesAndSelfTo<FixingNoMoves>().AsSingle();
            Container.BindInterfacesAndSelfTo<FigurinesHandler>().AsSingle();
        }

        private void AssignInitializationOrder()
        {
            Container.BindInitializableExecutionOrder<GameBoardHandler>(-10);
            Container.BindInitializableExecutionOrder<FigurinesHandler>(-9);
            Container.BindInitializableExecutionOrder<SpawningFigures>(-8);
            Container.BindInitializableExecutionOrder<ChangingStartingFigures>(-7);
        }
    }
}

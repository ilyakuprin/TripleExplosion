using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class SpawningFigures : IInitializable
    {
        private readonly SpriteRenderer _prefab;
        private readonly GameBoardHandler _board;
        private readonly FigurinesHandler _figurinesHandler;

        [Inject] private readonly DiContainer _diContainer;

        public SpawningFigures(GameBoardHandler gameBoard,
                               FigurinesConfig config,
                               FigurinesHandler figurinesHandler)
        {
            _board = gameBoard;
            _prefab = config.FigurinePrefab;
            _figurinesHandler = figurinesHandler;
        }

        public void Initialize() => CreateFigurines();

        private void CreateFigurines()
        {
            int lengthColumn = _board.GetLengthColumn;
            int lengthRow = _board.GetLengthRow;

            for (int column = 0; column < lengthColumn; column++)
            {
                for (int row = 0; row < lengthRow; row++)
                {
                    SpriteRenderer newObject = GetCreatingObject(column, row);
                    newObject.transform.localScale = _prefab.transform.lossyScale;

                    _figurinesHandler.SetArrays(column, row, newObject);
                }
            }
        }

        private SpriteRenderer GetCreatingObject(int column, int row)
        {
            Transform parant = _board.GetCell(column, row);

            SpriteRenderer newObject =
                _diContainer.InstantiatePrefabForComponent<SpriteRenderer>(_prefab,
                                                                           parant.position,
                                                                           Quaternion.identity,
                                                                           parant);
            return newObject;
        }
    }
}

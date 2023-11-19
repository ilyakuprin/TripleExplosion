using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class ChangingStartingFigures : IInitializable
    {
        private readonly GameBoardHandler _board;
        private readonly CheckingAllMoves _checkingAllMoves;
        private readonly Sprite[] _sprites;
        private readonly FigurinesHandler _figurinesHandler;

        public ChangingStartingFigures(GameBoardHandler board,
                                       FigurinesConfig config,
                                       CheckingAllMoves checkingAllMoves,
                                       FigurinesHandler figurinesHandler)
        {
            _board = board;
            _sprites = config.GetCopySprites();
            _checkingAllMoves = checkingAllMoves;
            _figurinesHandler = figurinesHandler;
        }

        public void Initialize() => SetColorFugurines();

        private void SetColorFugurines()
        {
            int lengthColumn = _board.GetLengthColumn;
            int lengthRow = _board.GetLengthRow;

            Sprite[] allSprites = new Sprite[lengthColumn * lengthRow];

            for (int column = 0; column < lengthColumn; column++)
            {
                for (int row = 0; row < lengthRow; row++)
                {
                    int currentCount = column * lengthColumn + row;

                    List<Sprite> possibleSprites = GetPossibleSprites(allSprites, column, row);

                    //Transform figurine = _board.GetCell(column, row).GetChild(0);

                    Sprite newSprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
                    _figurinesHandler.GetRender(column, row).sprite = newSprite;
                    //figurine.GetComponent<SpriteRenderer>().sprite = newSprite;

                    allSprites[currentCount] = newSprite;
                }
            }

            if (!_checkingAllMoves.CheckMoves())
            {
                SetColorFugurines();
            }
        }

        private List<Sprite> GetPossibleSprites(Sprite[] allSprites, int column, int row)
        {
            List<Sprite> possibleSprites = new List<Sprite>();
            possibleSprites.AddRange(_sprites);

            int lengthColumn = _board.GetLengthColumn;
            int lengthRow = _board.GetLengthRow;
            int currentCount = column * lengthColumn + row;

            if (row >= 2)
                if (allSprites[currentCount - 1] == allSprites[currentCount - 2])
                    possibleSprites.Remove(allSprites[currentCount - 1]);

            if (column >= 2)
                if (allSprites[currentCount - lengthRow] == allSprites[currentCount - lengthRow * 2])
                    possibleSprites.Remove(allSprites[currentCount - lengthRow]);

            return possibleSprites;
        }

        public void SetRandomSprite(int column, int row)
            /*=> figurine.GetComponent<SpriteRenderer>().sprite =
               _sprites[Random.Range(0, _sprites.Length)];*/
            => _figurinesHandler.GetRender(column, row).sprite =
               _sprites[Random.Range(0, _sprites.Length)];
    }
}

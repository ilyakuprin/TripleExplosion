using UnityEngine;

namespace TripleExplosion
{
    public class CheckingAllMoves
    {
        private readonly GameBoardHandler _board;
        private readonly FigurinesHandler _figurines;

        public CheckingAllMoves(GameBoardHandler board,
                                FigurinesHandler figurines)
        {
            _board = board;
            _figurines = figurines;
        }

        public bool CheckMoves()
        {
            for (int column = 0; column < _board.GetLengthColumn - 2; column++)
            {
                for (int row = 0; row < _board.GetLengthRow - 2; row++)
                {
                    if (CheckHorizontal(column, row) || CheckVertical(column, row))
                        return true;
                }
            }

            return false;
        }

        private bool CheckHorizontal(int column, int row)
        {
            Sprite currentSprite = _figurines.GetRender(column, row).sprite;

            if (column + 1 < _board.GetLengthColumn)
            {
                Sprite rightSprite = _figurines.GetRender(column + 1, row).sprite;
                if (currentSprite == rightSprite)
                {
                    if (column + 3 < _board.GetLengthColumn)
                    {
                        Sprite rightRightSprite = _figurines.GetRender(column + 3, row).sprite;
                        if (currentSprite == rightRightSprite)
                            return true;
                    }

                    if (column + 2 < _board.GetLengthColumn && row + 1 < _board.GetLengthRow)
                    {
                        Sprite rightUpSprite = _figurines.GetRender(column + 2, row + 1).sprite;
                        if (currentSprite == rightUpSprite)
                            return true;
                    }

                    if (column + 2 < _board.GetLengthColumn && row - 1 >= 0)
                    {
                        Sprite rightDownSprite = _figurines.GetRender(column + 2, row - 1).sprite;
                        if (currentSprite == rightDownSprite)
                            return true;
                    }

                    if (column - 2 >= 0)
                    {
                        Sprite leftLeftSprite = _figurines.GetRender(column - 2, row).sprite;
                        if (currentSprite == leftLeftSprite)
                            return true;
                    }

                    if (column - 1 >= 0 && row + 1 < _board.GetLengthRow)
                    {
                        Sprite leftUpSprite = _figurines.GetRender(column - 1, row + 1).sprite;
                        if (currentSprite == leftUpSprite)
                            return true;
                    }

                    if (column - 1 >= 0 && row - 1 >= 0)
                    {
                        Sprite leftDownSprite = _figurines.GetRender(column - 1, row - 1).sprite;
                        if (currentSprite == leftDownSprite)
                            return true;
                    }
                }
            }

            if (column + 2 < _board.GetLengthColumn)
            {
                Sprite rightSprite = _figurines.GetRender(column + 2, row).sprite;
                if (currentSprite == rightSprite)
                {
                    if (row + 1 < _board.GetLengthRow)
                    {
                        Sprite upSprite = _figurines.GetRender(column + 1, row + 1).sprite;
                        if (currentSprite == upSprite)
                            return true;
                    }
                    if (row - 1 >= 0)
                    {
                        Sprite downSprite = _figurines.GetRender(column + 1, row - 1).sprite;
                        if (currentSprite == downSprite)
                            return true;
                    }
                }
            }

            return false;
        }

        private bool CheckVertical(int column, int row)
        {
            Sprite currentSprite = _figurines.GetRender(column, row).sprite;

            if (row + 1 < _board.GetLengthRow)
            {
                Sprite upSprite = _figurines.GetRender(column, row + 1).sprite;
                if (currentSprite == upSprite)
                {
                    if (row + 3 < _board.GetLengthRow)
                    {
                        Sprite upUpSprite = _figurines.GetRender(column, row + 3).sprite;
                        if (currentSprite == upUpSprite)
                            return true;
                    }

                    if (row + 2 < _board.GetLengthRow && column + 1 < _board.GetLengthColumn)
                    {
                        Sprite upRightSprite = _figurines.GetRender(column + 1, row + 2).sprite;
                        if (currentSprite == upRightSprite)
                            return true;
                    }

                    if (row + 2 < _board.GetLengthRow && column - 1 >= 0)
                    {
                        Sprite upLeftSprite = _figurines.GetRender(column - 1, row + 2).sprite;
                        if (currentSprite == upLeftSprite)
                            return true;
                    }

                    if (row - 2 >= 0)
                    {
                        Sprite downDownSprite = _figurines.GetRender(column, row - 2).sprite;
                        if (currentSprite == downDownSprite)
                            return true;
                    }

                    if (row - 1 >= 0 && column + 1 < _board.GetLengthColumn)
                    {
                        Sprite downRightSprite = _figurines.GetRender(column + 1, row - 1).sprite;
                        if (currentSprite == downRightSprite)
                            return true;
                    }

                    if (row - 1 >= 0 && column - 1 >= 0)
                    {
                        Sprite downLeftSprite = _figurines.GetRender(column - 1, row - 1).sprite;
                        if (currentSprite == downLeftSprite)
                            return true;
                    }
                }
            }

            if (row + 2 < _board.GetLengthRow)
            {
                Sprite upSprite = _figurines.GetRender(column, row + 2).sprite;
                if (currentSprite == upSprite)
                {
                    if (column + 1 < _board.GetLengthColumn)
                    {
                        Sprite leftSprite = _figurines.GetRender(column + 1, row + 1).sprite;
                        if (currentSprite == leftSprite)
                            return true;
                    }

                    if (column - 1 >= 0)
                    {
                        Sprite rightSprite = _figurines.GetRender(column - 1, row + 1).sprite;
                        if (currentSprite == rightSprite)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}

using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class FigurinesHandler : IInitializable
    {
        private readonly GameBoardHandler _board;
        private SpriteRenderer[,] _renders;
        private SwapParent[,] _swap;

        public FigurinesHandler(GameBoardHandler board)
            => _board = board;

        public SpriteRenderer GetRender(int column, int row) => _renders[column, row];
        public SwapParent GetSwap(int column, int row) => _swap[column, row];

        public void SwipeFigurines(int column1, int row1, int column2, int row2)
        {
            (_renders[column1, row1], _renders[column2, row2]) = (_renders[column2, row2], _renders[column1, row1]);
            (_swap[column1, row1], _swap[column2, row2]) = (_swap[column2, row2], _swap[column1, row1]);
        }

        public void Initialize()
            => ArrayInitialization(_board.GetLengthColumn, _board.GetLengthRow);

        private void ArrayInitialization(int column, int row)
        {
            _renders = new SpriteRenderer[column, row];
            _swap = new SwapParent[column, row];
        }

        public void SetArrays(int column, int row, SpriteRenderer spriteRenderer)
        {
            _renders[column, row] = spriteRenderer;
            _swap[column, row] = spriteRenderer.GetComponent<SwapParent>();
        }
    }
}

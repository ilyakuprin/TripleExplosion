using UnityEngine;

namespace TripleExplosion
{
    public class ColorChangingSetting
    {
        private Sprite _newSprite;
        private bool _isActive = false;

        private readonly GameBoardHandler _board;
        private readonly SearchMatches _searchMatches;
        private readonly RemovingMatches _removingMatches;
        private readonly FixingNoMoves _fixingNoMoves;

        public ColorChangingSetting(GameBoardHandler board,
                                    SearchMatches searchMatches,
                                    RemovingMatches removingMatches,
                                    FixingNoMoves fixingNoMoves)
        {
            _board = board;
            _searchMatches = searchMatches;
            _removingMatches = removingMatches;
            _fixingNoMoves = fixingNoMoves;
        }

        public void TryUsingBoost(SpriteRenderer spriteRenderer, int column, int row)
        {
            if (_isActive && _board.IsBoardAcrive)
            {
                if (spriteRenderer.sprite != _newSprite)
                {
                    _board.DisableActiveBoarde();
                    spriteRenderer.sprite = _newSprite;
                    _isActive = false;
                    _searchMatches.StartFind(column, row);

                    if (!_removingMatches.IsNoMath)
                        _removingMatches.RemoveFigurines();
                    else
                        _fixingNoMoves.OnCheckAndFix();
                }
            }
        }

        public void OnSetSprite(Sprite sprite)
        {
            _isActive = true;
            _newSprite = sprite;
        }
    }
}

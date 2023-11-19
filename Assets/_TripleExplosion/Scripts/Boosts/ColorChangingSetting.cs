using UnityEngine;

namespace TripleExplosion
{
    public class ColorChangingSetting
    {
        private Sprite _newSprite;
        private bool _isActive = false;

        private readonly GameBoardHandler _board;
        private readonly SearchMatches _searchMatches;
        private readonly CheckingMatchNotFound _checkingMatchNotFound;

        public ColorChangingSetting(GameBoardHandler board,
                                    SearchMatches searchMatches,
                                    CheckingMatchNotFound checkingMatchNotFound)
        {
            _board = board;
            _searchMatches = searchMatches;
            _checkingMatchNotFound = checkingMatchNotFound;
        }

        public void TryUsingBoost(SpriteRenderer spriteRenderer, int column, int row)
        {
            if (_isActive && _board.IsBoardAcrive)
            {
                if (spriteRenderer.sprite != _newSprite)
                {
                    spriteRenderer.sprite = _newSprite;
                    _isActive = false;
                    _checkingMatchNotFound.SetFirstCheck();
                    _searchMatches.StartFind(column, row);
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

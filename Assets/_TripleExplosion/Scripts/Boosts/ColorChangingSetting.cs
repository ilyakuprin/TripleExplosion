using System;
using UnityEngine;

namespace TripleExplosion
{
    public class ColorChangingSetting : IBoost
    {
        public event Action<bool> BoostActivitySet;
        public event Action ColorChangeUsed;

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

        public bool GetActiveBoost { get => _isActive; }

        public void OnSetSprite(Sprite sprite)
        {
            SetActiveBoost(true);
            _newSprite = sprite;
        }

        public void SetActiveBoost(bool value)
        {
            _isActive = value;
            BoostActivitySet?.Invoke(value);
        }

        public void TryUsingBoost(SpriteRenderer spriteRenderer, int column, int row)
        {
            if (_isActive && _board.IsBoardAcrive)
            {
                if (spriteRenderer.sprite != _newSprite)
                {
                    _board.SetActiveBoarde(false);
                    spriteRenderer.sprite = _newSprite;
                    SetActiveBoost(false);
                    _searchMatches.StartFind(column, row);

                    if (!_removingMatches.IsNoMath)
                        _removingMatches.RemoveFigurines();
                    else
                        _fixingNoMoves.OnCheckAndFix();

                    ColorChangeUsed?.Invoke();
                }
            }
        }
    }
}

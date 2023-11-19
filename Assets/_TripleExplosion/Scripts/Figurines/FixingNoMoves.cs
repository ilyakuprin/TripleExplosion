using System;
using Zenject;

namespace TripleExplosion
{
    public class FixingNoMoves : IInitializable, IDisposable
    {
        private readonly Falling _falling;
        private readonly CheckingAllMoves _checkingAllMoves;
        private readonly MixingSettings _mixingSettings;
        private readonly GameBoardHandler _board;

        public FixingNoMoves(Falling falling,
                             CheckingAllMoves checkingAllMoves,
                             MixingSettings mixingSettings,
                             GameBoardHandler board)
        {
            _falling = falling;
            _checkingAllMoves = checkingAllMoves;
            _mixingSettings = mixingSettings;
            _board = board;
        }

        public void Initialize()
            => _falling.FallOver += Fix;

        private void Fix()
        {
            if (!_checkingAllMoves.CheckMoves())
            {
                _board.EnableActiveBoarde();
                _mixingSettings.OnMix();
            }
            else
            {
                _board.EnableActiveBoarde();
            }
        }

        public void Dispose()
            => _falling.FallOver -= Fix;
    }
}

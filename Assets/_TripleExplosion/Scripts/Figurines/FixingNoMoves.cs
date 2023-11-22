namespace TripleExplosion
{
    public class FixingNoMoves
    {
        private readonly CheckingAllMoves _checkingAllMoves;
        private readonly MixingSettings _mixingSettings;
        private readonly GameBoardHandler _board;

        public FixingNoMoves(CheckingAllMoves checkingAllMoves,
                             MixingSettings mixingSettings,
                             GameBoardHandler board)
        {
            _checkingAllMoves = checkingAllMoves;
            _mixingSettings = mixingSettings;
            _board = board;
        }

        public void OnCheckAndFix()
        {
            if (!_checkingAllMoves.CheckMoves())
            {
                _mixingSettings.Mix();
            }
            else
            {
                _board.EnableActiveBoarde();
            }
        }
    }
}

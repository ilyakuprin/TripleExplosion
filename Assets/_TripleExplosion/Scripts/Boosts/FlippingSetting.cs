namespace TripleExplosion
{
    public class FlippingSetting
    {
        private readonly CheckingMatchNotFound _checkingMatchNotFound;

        public FlippingSetting(CheckingMatchNotFound checkingMatchNotFound)
            => _checkingMatchNotFound = checkingMatchNotFound;

        private void OnFlipp()
        {
            _checkingMatchNotFound.SetNoReverse();
            _checkingMatchNotFound.ParametersSet -= OnFlipp;
        }

        public void OnActivateBoost()
            => _checkingMatchNotFound.ParametersSet += OnFlipp;
    }
}

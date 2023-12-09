using System.Linq;
using YG;

namespace TripleExplosion
{
    public class SavingResultLb 
    {
        private readonly string _endlessMode = "EndlessMode";
        private readonly string _taskMode = "TaskMode";

        private void UpdateEndlessMode()
        {
            int _maxValue = YandexGame.savesData.RecordEndlessMode.Max();
            YandexGame.NewLeaderboardScores(_endlessMode, _maxValue);
        }

        private void UpdateTaskMode()
        {
            int _maxValue = YandexGame.savesData.RecordTaskMode.Max();
            YandexGame.NewLeaderboardScores(_taskMode, _maxValue);
        }

        public void UpdateLb()
        {
            UpdateEndlessMode();
            UpdateTaskMode();
        }
    }
}
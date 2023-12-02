using System.Linq;
using YG;

namespace TripleExplosion
{
    public class RecordSavingTaskMode : RecordSaving
    {
        public RecordSavingTaskMode(IPointCounter pointCounter,
                                    TimerCounter timer,
                                    InteractionSaving saving) : base(pointCounter,
                                                                     timer,
                                                                     saving)
        {
        }

        protected override void ChangeRecord(int totalValue)
        {
            int minRecord = YandexGame.savesData.RecordTaskMode.Min();

            if (totalValue > minRecord)
            {
                for (int i = 0; i < YandexGame.savesData.RecordTaskMode.Length; i++)
                {
                    if (YandexGame.savesData.RecordTaskMode[i] == minRecord)
                    {
                        YandexGame.savesData.RecordTaskMode[i] = totalValue;
                        YandexGame.savesData.TimeTaskMode[i] = TotalTime;
                        Save();
                        break;
                    }
                }
            }
        }
    }
}
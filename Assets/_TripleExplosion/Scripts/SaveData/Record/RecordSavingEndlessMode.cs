using System.Linq;
using YG;

namespace TripleExplosion
{
    public class RecordSavingEndlessMode : RecordSaving
    {
        public RecordSavingEndlessMode(IPointCounter pointCounter,
                                       TimerCounter timer,
                                       InteractionSaving saving) : base(pointCounter,
                                                                        timer,
                                                                        saving)
        {
        }

        protected override void ChangeRecord(int totalValue)
        {
            int minRecord = YandexGame.savesData.RecordEndlessMode.Min();

            if (totalValue > minRecord)
            {
                for (int i = 0; i < YandexGame.savesData.RecordEndlessMode.Length; i++)
                {
                    if (YandexGame.savesData.RecordEndlessMode[i] == minRecord)
                    {
                        YandexGame.savesData.RecordEndlessMode[i] = totalValue;
                        YandexGame.savesData.TimeEndlessMode[i] = TotalTime;
                        Save();
                        break;
                    }
                }
            }
        }
    }
}
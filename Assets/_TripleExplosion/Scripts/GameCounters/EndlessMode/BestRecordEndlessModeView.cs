using YG;
using System.Linq;

namespace TripleExplosion
{
    public class BestRecordEndlessModeView : BestRecordView
    {
        protected override void View()
        {
            if (GetCurrentValue <= MaxValue)
            {
                MaxValue = YandexGame.savesData.RecordEndlessMode.Max();
                SetText(MaxValue);
            }
        }
    }
}
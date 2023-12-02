using YG;
using System.Linq;

namespace TripleExplosion
{
    public class BestRecordTaskModeView : BestRecordView
    {
        protected override void View()
        {
            if (GetCurrentValue <= MaxValue)
            {
                MaxValue = YandexGame.savesData.RecordTaskMode.Max();
                SetText(MaxValue);
            }
        }
    }
}
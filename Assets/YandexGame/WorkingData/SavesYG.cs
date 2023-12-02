
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        //

        public int CountBomb = 5;
        public int CountSwipe = 5;
        public int CountMix = 5;
        public int CountPaint = 5;

        public int Money = 0;
        public float Volume = 0.5f;

        public int[] RecordEndlessMode = new int[3]; 
        public int[] RecordTaskMode = new int[3];
        public float[] TimeEndlessMode = new float[3];
        public float[] TimeTaskMode = new float[3];

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
        }
    }
}

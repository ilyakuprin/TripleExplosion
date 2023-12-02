using System;
using TMPro;
using UnityEngine;

namespace TripleExplosion
{
    [Serializable]
    public struct Record
    {
        public TextMeshProUGUI Points;
        public TextMeshProUGUI Time;
    }

    public abstract class MainMenuRecordView : MonoBehaviour
    {
        [SerializeField] private Record[] _records; 

        private readonly int _minutesInHour = 60;

        protected void View(int[] record, float[] time)
        {
            Array.Sort(record);
            Array.Reverse(record);

            Array.Sort(time);
            Array.Reverse(time);

            for (int i = 0; i < _records.Length; i++)
            {
                _records[i].Points.text = GetStringPoints(record[i]);
                _records[i].Time.text = GetStringTime(time[i]);
            }
        }

        private string GetStringPoints(int points)
            => string.Format("{0:0000000}", points);

        private string GetStringTime(float time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);

            if (ts.Minutes < _minutesInHour)
                return string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            else
                return string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }
    }
}
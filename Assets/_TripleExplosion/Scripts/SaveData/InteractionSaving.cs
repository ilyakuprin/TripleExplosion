using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace TripleExplosion
{
    public class InteractionSaving : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public event Action SaveDataReceived;

        public SaveData SaveData;

        [DllImport("__Internal")]
        private static extern void SaveEntern(string date);

        [DllImport("__Internal")]
        private static extern void LoadEntern();

        private void Start()
        {
            if (!Application.isEditor)
                LoadEntern();
            else
                SaveDataReceived?.Invoke();
        }

        public void Save()
        {
            if (!Application.isEditor)
            {
                string jsonString = JsonUtility.ToJson(SaveData);
                SaveEntern(jsonString);
            }
        }

        //Call from JS.
        public void SetPlayerInfo(string value)
        {
            SaveData = JsonUtility.FromJson<SaveData>(value);
            SaveDataReceived?.Invoke();
            _text.text = SaveData.CountBomb + " | " + SaveData.CountMix + " | " + SaveData.CountPaint + " | " + SaveData.CountSwipe;
        }
    }
}

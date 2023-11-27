using System.Runtime.InteropServices;
using UnityEngine;

namespace TripleExplosion
{
    public class InteractionSaving : MonoBehaviour
    {
        [HideInInspector] public SaveData SaveData;

        [DllImport("__Internal")]
        private static extern void SaveEntern(string date);

        [DllImport("__Internal")]
        private static extern void LoadEntern();

        private void Awake()
        {
            //LoadEntern();
        }

        public void Save()
        {
            string jsonString = JsonUtility.ToJson(SaveData);
            //SaveEntern(jsonString);
        }

        public void SetPlayerInfo(string value)
        {
            SaveData = JsonUtility.FromJson<SaveData>(value);
        }
    }
}

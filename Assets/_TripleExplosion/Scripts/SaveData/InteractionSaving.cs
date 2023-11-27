using System.Runtime.InteropServices;
using UnityEngine;

namespace TripleExplosion
{
    public class InteractionSaving : MonoBehaviour
    {
        [SerializeField] private SaveData _saveData;

        [DllImport("__Internal")]
        private static extern void SaveEntern(string date);

        [DllImport("__Internal")]
        private static extern void LoadEntern();

        private void Awake()
        {
#if UNITY_WEBGL
            LoadEntern();
#endif
        }

        public void Save()
        {
#if UNITY_WEBGL
            string jsonString = JsonUtility.ToJson(_saveData);
            SaveEntern(jsonString);
#endif
        }

        public void SetPlayerInfo(string value)
        {
            _saveData = JsonUtility.FromJson<SaveData>(value);
            Debug.Log(_saveData.CountBomb);
            Debug.Log(_saveData.CountFlip);
            Debug.Log(_saveData.CountMix);
            Debug.Log(_saveData.CountPaint);
        }
    }
}

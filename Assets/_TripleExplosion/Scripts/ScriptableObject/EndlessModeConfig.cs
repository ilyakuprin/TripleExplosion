using UnityEngine;

namespace TripleExplosion
{
    [CreateAssetMenu(fileName = "EndlessModeConfig", menuName = "Configs/EndlessModeConfig")]
    public class EndlessModeConfig : ScriptableObject
    {
        [SerializeField] private DictionaryForInspector[] _cstomDictionary;

        public DictionaryForInspector[] GetDictionary()
        {
            DictionaryForInspector[] copyArray = new DictionaryForInspector[_cstomDictionary.Length];
            _cstomDictionary.CopyTo(copyArray, 0);
            return copyArray;
        }

        private void OnValidate()
        {
            for (int i = 0; i < _cstomDictionary.Length - 1; i++)
            {
                if (_cstomDictionary[i].Key < 0)
                    _cstomDictionary[i].Key = 0;

                if (_cstomDictionary[i].Value < 0)
                    _cstomDictionary[i].Value = 0;

                for (int k = i + 1; k < _cstomDictionary.Length; k++)
                {
                    if (_cstomDictionary[i].Key == _cstomDictionary[k].Key)
                        Debug.LogError("Keys match " + k + " and " + i);
                }
            }
        }
    }
}

using UnityEngine;

namespace TripleExplosion
{
    [CreateAssetMenu(fileName = "GameBoardConfig", menuName = "Configs/GameBoardConfig")]
    public class GameBoardConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 50)] public int NumbersRows { get; private set; }
        [field: SerializeField, Range(1, 50)] public int NumbersColumns { get; private set; }
        [field: SerializeField] public Transform CellPrefab { get; private set; }
    }
}

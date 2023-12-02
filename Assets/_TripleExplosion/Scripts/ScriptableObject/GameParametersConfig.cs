using UnityEngine;

namespace TripleExplosion
{
    [CreateAssetMenu(fileName = "GameParametersConfig", menuName = "Configs/GameParametersConfig")]
    public class GameParametersConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 2)] public float MultiplyModifierMoney { get; private set; }

        [field: SerializeField, Range(1, 1000)] public int SwapCost { get; private set; }
        [field: SerializeField, Range(1, 1000)] public int BombCost { get; private set; }
        [field: SerializeField, Range(1, 1000)] public int PaintCost { get; private set; }
        [field: SerializeField, Range(1, 1000)] public int MixCost { get; private set; }
        [field: SerializeField, Range(2, 20)] public int BuySeveral { get; private set; }

        [field: SerializeField, Range(1, 1000)] public int BuyExtraTime { get; private set; }
        [field: SerializeField, Range(1, 1000)] public int ExtraTimeSeconds { get; private set; }
    }
}
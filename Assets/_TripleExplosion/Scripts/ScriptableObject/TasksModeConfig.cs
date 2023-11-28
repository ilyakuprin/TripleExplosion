using UnityEngine;

namespace TripleExplosion
{
    [CreateAssetMenu(fileName = "TasksModeConfig", menuName = "Configs/TasksModeConfig")]
    public class TasksModeConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 100)] public int MinCountInTask { get; private set; }
        [field: SerializeField, Range(1, 100)] public int MaxCountInTask { get; private set; }
        [field: SerializeField, Range(0, 10)] public float TimeForShip { get; private set; }
        [field: SerializeField, Range(0, 10)] public float PointForShip { get; private set; }

        private void OnValidate()
        {
            if (MinCountInTask > MaxCountInTask)
                MinCountInTask = MaxCountInTask;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace TripleExplosion
{
    public class FormationTasks
    {
        private readonly int _minCountInTask;
        private readonly int _maxCountInTask;
        private readonly List<Sprite> _availableSprites;

        public FormationTasks(FigurinesConfig figurinesConfig,
                              TasksModeConfig tasksModeConfig)
        {
            Sprite[] sprites = figurinesConfig.GetCopySprites();
            _availableSprites = new List<Sprite>(sprites);

            _minCountInTask = tasksModeConfig.MinCountInTask;
            _maxCountInTask = tasksModeConfig.MaxCountInTask;
        }

        public Sprite GetShip()
        {
            Sprite ship = null;

            if (_availableSprites.Count > 0)
            {
                int index = Random.Range(0, _availableSprites.Count);
                ship = _availableSprites[index];
                _availableSprites.RemoveAt(index);
            }

            return ship;
        }

        public int GetValue()
            => Random.Range(_minCountInTask, _maxCountInTask);

        public void AddShip(Sprite ship)
        {
            foreach(Sprite availableShip in _availableSprites)
            {
                if (availableShip == ship)
                    return;
            }

            _availableSprites.Add(ship);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripleExplosion
{
    public class MovingFigurines : IPause
    {
        public event Action MovementStarted;
        public event Action MovementOver;

        private readonly MonoBehaviour _monoBeh;
        private readonly float _time = 0.2f;
        private bool _pause = false;

        public MovingFigurines(MonoBehaviour monoBeh)
            => _monoBeh = monoBeh;

        public void StartMove(List<Transform> figurines)
            => _monoBeh.StartCoroutine(Move(figurines));

        private IEnumerator Move(List<Transform> figurines)
        {
            MovementStarted?.Invoke();

            float speed = 1 / _time;
            float currentTime = 0;

            while (currentTime < _time)
            {
                if (!_pause)
                {
                    currentTime += Time.deltaTime;

                    foreach (Transform figurine in figurines)
                        figurine.localPosition = Vector3.MoveTowards(figurine.localPosition,
                                                                    Vector3.zero,
                                                                    speed * Time.deltaTime);
                }

                yield return null;
            }

            foreach (Transform figurine in figurines)
                figurine.localPosition = Vector3.zero;

            MovementOver?.Invoke();
        }

        public void Pause(bool value)
            => _pause = value;
    }
}

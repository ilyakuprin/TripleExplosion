using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripleExplosion
{
    public class MovingFigurines
    {
        public event Action MovementOver;

        private readonly MonoBehaviour _monoBeh;
        private readonly float _time = 0.4f;

        public MovingFigurines(MonoBehaviour monoBeh)
            => _monoBeh = monoBeh;

        public void StartMove(Transform figurine)
        {
            List<Transform> figurines = new List<Transform>
            {
                figurine
            };

            _monoBeh.StartCoroutine(Move(figurines));
        }

        public void StartMove(List<Transform> figurines)
            => _monoBeh.StartCoroutine(Move(figurines));

        private IEnumerator Move(List<Transform> figurines)
        {
            Vector3 speed = figurines[0].localPosition / _time;
            float currentTime = 0;

            while (currentTime < _time)
            {
                currentTime += Time.deltaTime;

                foreach(Transform figurine in figurines)
                    figurine.localPosition -= speed * Time.deltaTime;

                yield return null;
            }

            foreach (Transform figurine in figurines)
                figurine.localPosition = Vector3.zero;

            MovementOver?.Invoke();
        }
    }
}

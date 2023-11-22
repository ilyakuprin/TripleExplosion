using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripleExplosion
{
    public class ReduceFigurine : MonoBehaviour
    {
        public event Action<List<Transform>> ReducedOver;

        private readonly float _timeReduce = 0.2f;

        public void StartReduce(List<Transform> figurenes)
            => StartCoroutine(Reduce(figurenes));

        private IEnumerator Reduce(List<Transform> figurenes)
        {
            Vector3 currentScale = figurenes[0].localScale;

            float currentTime = 0;
            float speed = currentScale.x / _timeReduce;

            while (currentTime < _timeReduce)
            {
                currentTime += Time.deltaTime;

                foreach(var figurene in figurenes)
                    figurene.localScale -= Vector3.one * speed * Time.deltaTime;

                yield return null;
            }

            foreach (var figurene in figurenes)
                figurene.localScale = currentScale;

            ReducedOver?.Invoke(figurenes);
        }
    }
}

using System;
using UnityEngine;

namespace TripleExplosion
{
    [Serializable]
    public class BoostCounter
    {
        [SerializeField] private int _countBomb;
        [SerializeField] private int _countFlip;
        [SerializeField] private int _countMix;
        [SerializeField] private int _countPaint;
    }
}

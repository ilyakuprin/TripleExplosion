using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class CallingMovementFigures : IInitializable, IDisposable
    {
        private readonly MovingFigurines _movingFigurines;
        private readonly List<Transform> _figurines = new List<Transform>();

        public CallingMovementFigures(MovingFigurines movingFigurines)
            => _movingFigurines = movingFigurines;

        public void Initialize() => _movingFigurines.MovementOver += ClearList;

        public void AddFigurine(Transform figurine) => _figurines.Add(figurine);

        public void CallMove() => _movingFigurines.StartMove(_figurines);

        private void ClearList() => _figurines.Clear();

        public void Dispose() => _movingFigurines.MovementOver -= ClearList;
    }
}

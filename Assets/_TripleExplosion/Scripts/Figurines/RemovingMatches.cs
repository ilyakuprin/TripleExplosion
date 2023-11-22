using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class RemovingMatches : IInitializable, IDisposable
    {
        public event Action<int> MatchAdded;

        private readonly SearchMatches _searchMatches;
        private readonly ReduceFigurine _reduceFigurine;

        private readonly List<Transform> _redusedFigurines = new List<Transform>();

        public RemovingMatches(SearchMatches searchMatches,
                               ReduceFigurine reduceFigurine)
        {
            _searchMatches = searchMatches;
            _reduceFigurine = reduceFigurine;
        }

        public bool IsNoMath
        {
            get
            {
                if (_redusedFigurines.Count > 0)
                    return false;
                else
                    return true;
            }
        }

        private void DistinctAndAdd(List<Transform> figurines)
        {
            for (int k = 0; k < _redusedFigurines.Count; k++)
            {
                for (int i = 0; i < figurines.Count; i++)
                {
                    if (figurines[i] == _redusedFigurines[k])
                    {
                        figurines.RemoveAt(i);
                    }
                }
            }

            if (figurines.Count > 0)
            {
                _redusedFigurines.AddRange(figurines);
                MatchAdded?.Invoke(figurines.Count);
            }
        }

        public void RemoveFigurines()
        {
            if (!IsNoMath)
                _reduceFigurine.StartReduce(_redusedFigurines);
        }

        public void Clear()
            => _redusedFigurines.Clear();

        public void Initialize()
            => _searchMatches.MatchFounded += DistinctAndAdd;

        public void Dispose()
            => _searchMatches.MatchFounded -= DistinctAndAdd;
    }
}

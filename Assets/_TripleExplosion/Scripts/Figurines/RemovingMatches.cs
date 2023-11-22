using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripleExplosion
{
    public class RemovingMatches : IInitializable, IDisposable
    {
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

        private void OnRemoveMatch()
        {
            List<Transform> figurines = new List<Transform>();

            int lengthHorizontal = _searchMatches.GetLengthHorizontal;
            if (lengthHorizontal >= _searchMatches.GetMinNumberCoincidencesWithoutMain)
                for (int i = 0; i < lengthHorizontal; i++)
                    figurines.Add(_searchMatches.GetTransformInHorizontal(i));

            int lengthVertical = _searchMatches.GetLengthVertical;
            if (lengthVertical >= _searchMatches.GetMinNumberCoincidencesWithoutMain)
                for (int i = 0; i < lengthVertical; i++)
                    figurines.Add(_searchMatches.GetTransformInVertical(i));

            figurines.Add(_searchMatches.GetMainFigurine);

            CheckAndAdd(figurines);
        }

        private void CheckAndAdd(List<Transform> figurines)
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
                _redusedFigurines.AddRange(figurines);
        }

        public void RemoveFigurines()
        {
            if (_redusedFigurines.Count > 0)
                _reduceFigurine.StartReduce(_redusedFigurines);
        }

        public void Clear() => _redusedFigurines.Clear();

        public void Initialize()
            => _searchMatches.MatchFounded += OnRemoveMatch;

        public void Dispose()
            => _searchMatches.MatchFounded -= OnRemoveMatch;
    }
}

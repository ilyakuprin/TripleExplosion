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

            CheckAndReduce(figurines);
        }

        private void CheckAndReduce(List<Transform> figurines)
        {
            for (int k = 0; k < _redusedFigurines.Count; k++)
            {
                for (int i = 0; i < figurines.Count; i++)
                {
                    if (figurines[i] == _redusedFigurines[k])
                    {
                        figurines.Remove(figurines[i]);
                    }
                }
            }

            if (figurines.Count > 0)
            {
                _redusedFigurines.AddRange(figurines);
                _reduceFigurine.StartReduce(figurines);
            }
        }

        private void RemoveFromListRedused(List<Transform> figurines)
        {
            foreach (var figurine1 in figurines)
            {
                foreach (var figurine2 in _redusedFigurines)
                {
                    if (figurine1 == figurine2)
                    {
                        _redusedFigurines.Remove(figurine2);
                        break;
                    }
                }
            }
        }

        public void Initialize()
        {
            _searchMatches.MatchFounded += OnRemoveMatch;
            _reduceFigurine.Reduced += RemoveFromListRedused;
        }

        public void Dispose()
        {
            _searchMatches.MatchFounded -= OnRemoveMatch;
            _reduceFigurine.Reduced -= RemoveFromListRedused;
        }
    }
}

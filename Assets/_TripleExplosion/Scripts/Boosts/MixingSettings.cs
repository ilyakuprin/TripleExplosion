using System.Collections.Generic;
using UnityEngine;

namespace TripleExplosion
{
    public class MixingSettings
    {
        private readonly GameBoardHandler _board;
        private readonly ReduceFigurine _reduceFigurine;

        public MixingSettings(GameBoardHandler board,
                              ReduceFigurine reduceFigurine)
        {
            _board = board;
            _reduceFigurine = reduceFigurine;
        }

        public void Mix()
        {
            _board.SetActiveBoarde(false);
            List<Transform> figurines = new List<Transform>();

            for (int column = 0; column < _board.GetLengthColumn; column++)
            {
                for (int row = 0; row < _board.GetLengthRow; row++)
                {
                    Transform figurune = _board.GetCell(column, row).GetChild(0);
                    figurines.Add(figurune);
                }
            }

            _reduceFigurine.StartReduce(figurines);
        }
    }
}

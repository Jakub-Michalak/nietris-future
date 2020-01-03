using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class TPiece : Tetromino
    {
        public override char? PieceSymbol() { return 't'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(3, -1), new Vector2(4, -1), new Vector2(5, -1), new Vector2(4, -2) }; }

        public override Vector2 getCenter(List<Vector2> current)
        {
            return current[1];
        }
    }
}

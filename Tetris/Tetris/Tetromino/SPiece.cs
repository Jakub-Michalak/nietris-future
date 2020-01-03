using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class SPiece : Tetromino
    {
        public override char? PieceSymbol() { return 's'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(3, -1), new Vector2(4, -1), new Vector2(4, -2), new Vector2(5, -2) }; }
        public override Vector2 getCenter(List<Vector2> current)
        {
            return current[1];
        }
    }
}

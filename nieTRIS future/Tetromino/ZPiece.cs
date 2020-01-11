using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class ZPiece : Tetromino
    {
        public ZPiece() : base() { }
        public override char? PieceSymbol() { return 'z'; }

        public override Vector2 getCenter(List<Vector2> current)
        {
            return current[0];
        }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(4, -1), new Vector2(5, -1), new Vector2(3, -2), new Vector2(4, -2) }; }
    }
}

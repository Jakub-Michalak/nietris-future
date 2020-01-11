using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class OPiece : Tetromino
    {
        public OPiece()
        {
            this.x = 5;
            this.y = 10;
            this.position = 0;
        }
        public override char? PieceSymbol() { return 'o'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(4, -1), new Vector2(5, -1), new Vector2(4, -2), new Vector2(5, -2) }; }
        public override Vector2 getCenter(List<Vector2> current)
        {
            return current[2];
        }
        public override List<Vector2> Rotate(List<Vector2> current, rotations currentRotation, rotationDirection direction, ref char?[,] array)
        {
            return current;
        }

    }
}

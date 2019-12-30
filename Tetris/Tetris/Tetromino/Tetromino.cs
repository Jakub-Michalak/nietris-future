using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetromino
    {
        public int x = 5;
        public int y = 10;
        public virtual List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(1, 1), new Vector2(1, 2), new Vector2(2, 2), new Vector2(2, 1) }; }

        public enum rotationDirection
        {
            clockwise,
            counterclockwise
        }

        public enum rotations
        {
            rotation1,
            rotation2,
            rotation3,
            rotation4
        }

        public virtual List<Vector2> Rotate(List<Vector2> current, rotations currentRotation, rotationDirection direction, ref char?[,] array)
        {
            return current;

        }

       

        public virtual char? PieceSymbol() { return null; }
        public virtual int[,] rotation1()
        {
            return null;
        }
        public virtual int[,] rotation2()
        {
            return null;
        }
        public virtual int[,] rotation3()
        {
            return null;
        }
        public virtual int[,] rotation4()
        {
            return null;
        }



    
    }
}

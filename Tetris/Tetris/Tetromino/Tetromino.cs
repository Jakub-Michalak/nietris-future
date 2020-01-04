using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    abstract class Tetromino
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

        public abstract Vector2 getCenter(List<Vector2> current);

        public virtual List<Vector2> Rotate(List<Vector2> current, rotations currentRotation, rotationDirection direction, ref char?[,] array)
        {
            Vector2 center = getCenter(current);

            if (direction == rotationDirection.clockwise)
            {
                List<Vector2> newcurrent = new List<Vector2>();
                foreach (Vector2 v in current)
                {
                    newcurrent.Add(RotatePointClockwise(v, center));
                }
                return newcurrent;
            }

            if (direction == rotationDirection.counterclockwise)
            {
                List<Vector2> newcurrent = new List<Vector2>();
                foreach (Vector2 v in current)
                {
                    newcurrent.Add(RotatePointCounterClockwise(v, center));
                }
                return newcurrent;
            }

            return current;

        }

       

        public virtual char? PieceSymbol() { return null; }


        public Vector2 RotatePointClockwise(Vector2 current, Vector2 center)
        {
            return new Vector2(-current.Y + center.Y + center.X, current.X - center.X + center.Y);
        }

        public Vector2 RotatePointCounterClockwise(Vector2 current, Vector2 center)
        {
            return new Vector2(current.Y - center.Y + center.X, -current.X + center.X + center.Y);
        }

        
    }
}

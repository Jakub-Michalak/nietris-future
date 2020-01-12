using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    abstract class Tetromino
    {
        public int x;
        public int y;
        public int position;
        public Vector2[][][] kicks;

        public Tetromino()
        {
            this.x = 5;
            this.y = 10;
            this.position = 0;
            this.kicks = new Vector2[4][][];
            this.kicks[0] = new Vector2[4][];
            kicks[0][1] = new Vector2[] { new Vector2(0,0), new Vector2(-1,0), new Vector2(-1,1), new Vector2(0,-2), new Vector2(-1,-2)};
            kicks[0][3] = new Vector2[] { new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,-2), new Vector2(1,-2)};
            this.kicks[1] = new Vector2[3][];
            kicks[1][0] = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, 2), new Vector2(1, 2) };
            kicks[1][2] = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -2), new Vector2(1, 2) };
            this.kicks[2] = new Vector2[4][];
            kicks[2][1] = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, -2), new Vector2(-1, -2) };
            kicks[2][3] = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, -2), new Vector2(1, -2) };
            this.kicks[3] = new Vector2[3][];
            kicks[3][0] = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, 2), new Vector2(-1, 2) };
            kicks[3][2] = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, 2), new Vector2(-1, 2) };
        }

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

        public bool IsLegalPosition(List<Vector2> maybecurrent, ref char?[,] array) {
            foreach (Vector2 v in maybecurrent)
            {
                if (v.X < 0 || v.X > 9) return false;
                if (v.Y +20 > 39) return false;
                try
                {
                    if (array[(int)v.X, (int)v.Y+20] != null) return false;
                }
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Vector2> MovePiece(List<Vector2> current, Vector2 offset)
        {
            List<Vector2> newcurrent = new List<Vector2>();
            foreach (Vector2 v in current)
            {
                newcurrent.Add(v+offset);
            }
            return newcurrent;
        }

        public virtual List<Vector2> Rotate(List<Vector2> current, rotations currentRotation, rotationDirection direction, ref char?[,] array)
        {
            int oldposition = this.position;
            List<Vector2> afterrotation = TryRotate(current, currentRotation, direction);
            List<Vector2> mayberotated;
            foreach(Vector2 offset in this.kicks[oldposition][this.position])
            {
                mayberotated = MovePiece(afterrotation, offset);
                if (IsLegalPosition(mayberotated, ref array)) return mayberotated;
            }
            this.position = oldposition;
            return current;
        }

        public virtual List<Vector2> TryRotate(List<Vector2> current, rotations currentRotation, rotationDirection direction)
        {
            Vector2 center = getCenter(current);

            if (direction == rotationDirection.clockwise)
            {

                List<Vector2> newcurrent = new List<Vector2>();
                foreach (Vector2 v in current)
                {
                    newcurrent.Add(RotatePointClockwise(v, center));
                }
                this.position = (this.position + 1) % 4;
                return newcurrent;
            }

            if (direction == rotationDirection.counterclockwise)
            {
                List<Vector2> newcurrent = new List<Vector2>();
                foreach (Vector2 v in current)
                {
                    newcurrent.Add(RotatePointCounterClockwise(v, center));
                }
                this.position = (this.position + 3) % 4;
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

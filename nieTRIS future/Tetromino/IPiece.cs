using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class IPiece : Tetromino
    {
        public IPiece()
        {
            this.x = 5;
            this.y = 10;
            this.position = 0;
            this.kicks = new Vector2[4][][];
            this.kicks[0] = new Vector2[4][];
            kicks[0][1] = new Vector2[] { new Vector2(0, 0), new Vector2(-2, 0), new Vector2(1, 0), new Vector2(-2, -1), new Vector2(1, 2) };
            kicks[0][3] = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(2, 0), new Vector2(-1, 2), new Vector2(2, -1) };
            this.kicks[1] = new Vector2[3][];
            kicks[1][0] = new Vector2[] { new Vector2(0, 0), new Vector2(2, 0), new Vector2(-1, 0), new Vector2(2, 1), new Vector2(-1, -2) };
            kicks[1][2] = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(2, 0), new Vector2(-1, 2), new Vector2(2, -1) };
            this.kicks[2] = new Vector2[4][];
            kicks[2][1] = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-2, 0), new Vector2(1, -2), new Vector2(-2, 1) };
            kicks[2][3] = new Vector2[] { new Vector2(0, 0), new Vector2(2, 0), new Vector2(-1, 0), new Vector2(2, 1), new Vector2(-1, -2) };
            this.kicks[3] = new Vector2[3][];
            kicks[3][0] = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-2, 0), new Vector2(1, -2), new Vector2(-2, 1) };
            kicks[3][2] = new Vector2[] { new Vector2(0, 0), new Vector2(-2, 0), new Vector2(1, 0), new Vector2(-2, -1), new Vector2(1, 2) };
        }
        public override char? PieceSymbol() { return 'i'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(3, -1), new Vector2(4, -1), new Vector2(5, -1), new Vector2(6, -1) }; }

        public override Vector2 getCenter(List<Vector2> current)
        {
            return current[2];
        }

        public override List<Vector2> TryRotate(List<Vector2> current, rotations currentRotation, rotationDirection direction)
        {
            if(direction == rotationDirection.clockwise) this.position = (this.position + 1) % 4;
            else this.position = (this.position + 3) % 4;

            int maxX = 0;
            int minX = 10;
            float centerX;
            int maxY = -5;
            int minY = 20;
            float centerY;
            foreach (Vector2 v in current)
            {
                if (v.X > maxX) maxX = (int)v.X;
                if (v.X < minX) minX = (int)v.X;
                if (v.Y > maxY) maxY = (int)v.Y;
                if (v.Y < minY) minY = (int)v.Y;
            }
            centerX = (minX + maxX) / 2;
            centerY = (minY + maxY) / 2;

            if(currentRotation == rotations.rotation1 && direction == rotationDirection.clockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 1.5f, centerY + 2), new Vector2(centerX + 1.5f, centerY + 1), new Vector2(centerX + 1.5f, centerY), new Vector2(centerX + 1.5f, centerY - 1) };
            }
            if (currentRotation == rotations.rotation1 && direction == rotationDirection.counterclockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 0.5f, centerY + 2), new Vector2(centerX + 0.5f, centerY + 1), new Vector2(centerX + 0.5f, centerY), new Vector2(centerX + 0.5f, centerY - 1) };
            }

            if (currentRotation == rotations.rotation2 && direction == rotationDirection.clockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 1, centerY + 1.5f), new Vector2(centerX , centerY + 1.5f), new Vector2(centerX -1, centerY + 1.5f), new Vector2(centerX - 2, centerY + 1.5f) };
            }
            if (currentRotation == rotations.rotation2 && direction == rotationDirection.counterclockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 1, centerY + 0.5f), new Vector2(centerX, centerY + 0.5f), new Vector2(centerX - 1, centerY + 0.5f), new Vector2(centerX - 2, centerY + 0.5f) };
            }

            if (currentRotation == rotations.rotation3 && direction == rotationDirection.clockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 0.5f, centerY + 1), new Vector2(centerX + 0.5f, centerY), new Vector2(centerX + 0.5f, centerY - 1), new Vector2(centerX + 0.5f, centerY - 2) };
            }
            if (currentRotation == rotations.rotation3 && direction == rotationDirection.counterclockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 1.5f, centerY + 1), new Vector2(centerX + 1.5f, centerY), new Vector2(centerX + 1.5f, centerY - 1), new Vector2(centerX + 1.5f, centerY - 2) };
            }

            if (currentRotation == rotations.rotation4 && direction == rotationDirection.clockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 2, centerY + 0.5f), new Vector2(centerX + 1, centerY + 0.5f), new Vector2(centerX, centerY + 0.5f), new Vector2(centerX - 1, centerY + 0.5f) };
            }
            if (currentRotation == rotations.rotation4 && direction == rotationDirection.counterclockwise)
            {
                return new List<Vector2> { new Vector2(centerX + 2, centerY + 1.5f), new Vector2(centerX + 1, centerY + 1.5f), new Vector2(centerX, centerY + 1.5f), new Vector2(centerX - 1, centerY + 1.5f) };
            }

            return current;

        }
    }
}

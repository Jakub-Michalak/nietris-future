using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class LPiece : Tetromino
    {
        public override char? PieceSymbol() { return 'l'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(3, -1), new Vector2(4, -1), new Vector2(5, -1), new Vector2(5, -2) }; }
        public override int[,] rotation1()
        {
            return new int[3, 3]{
            { 0,0,1 },
            { 1,1,1 },
            { 0,0,0 } };
        }
        public override int[,] rotation2()
        {
            return new int[3, 3]{
            { 0,1,0 },
            { 0,1,0 },
            { 0,1,1 } };
        }
        public override int[,] rotation3()
        {
            return new int[3, 3]{
            { 0,0,0 },
            { 1,1,1 },
            { 1,0,0 } };
        }
        public override int[,] rotation4()
        {
            return new int[3, 3]{
            { 1,1,0 },
            { 0,1,0 },
            { 0,1,0 } };
        }
    }
}

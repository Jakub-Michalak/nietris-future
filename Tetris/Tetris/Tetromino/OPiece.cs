using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class OPiece : Tetromino
    {
        public override char? PieceSymbol() { return 'o'; }
        public override List<Vector2> StartingPosition() { return new List<Vector2> { new Vector2(4, -1), new Vector2(5, -1), new Vector2(4, -2), new Vector2(5, -2) }; }
        public override int[,] rotation1()
        {
            return new int[3, 4]{
            { 0,1,1,0 },
            { 0,1,1,0 },
            { 0,0,0,0 } };
        }
        public override int[,] rotation2()
        {
            return new int[3, 4]{
            { 0,1,1,0 },
            { 0,1,1,0 },
            { 0,0,0,0 } };
        }
        public override int[,] rotation3()
        {
            return new int[3, 4]{
            { 0,1,1,0 },
            { 0,1,1,0 },
            { 0,0,0,0 } };
        }
        public override int[,] rotation4()
        {
            return new int[3, 4]{
            { 0,1,1,0 },
            { 0,1,1,0 },
            { 0,0,0,0 } };
        }

    }
}

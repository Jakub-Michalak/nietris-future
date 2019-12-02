using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class OPiece : Tetromino
    {
        public override string PieceSymbol()
        {
            return "O";
        }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ZPiece : Tetromino
    {
        new public char PieceSymbol = 'z';
        public override int[,] rotation1()
        {
            return new int[3, 3]{
            { 1,1,0 },
            { 0,1,1 },
            { 0,0,0 } };
        }
        public override int[,] rotation2()
        {
            return new int[3, 3]{
            { 0,0,1 },
            { 0,1,1 },
            { 0,1,0 } };
        }
        public override int[,] rotation3()
        {
            return new int[3, 3]{
            { 0,0,0 },
            { 1,1,0 },
            { 0,1,1 } };
        }
        public override int[,] rotation4()
        {
            return new int[3, 3]{
            { 0,1,0 },
            { 1,1,0 },
            { 1,0,0 } };
        }
    }
}

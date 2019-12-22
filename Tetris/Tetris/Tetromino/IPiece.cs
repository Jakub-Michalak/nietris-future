using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class IPiece : Tetromino
    {
        new public char PieceSymbol = 'i';

        public override int[,] rotation1()
        {
            return new int[4, 4]{
            { 0,0,0,0 },
            { 1,1,1,1 },
            { 0,0,0,0 },
            { 0,0,0,0 } };
        
        }
        public override int[,] rotation2()
        {
            return new int[4, 4]{
            { 0,0,1,0 },
            { 0,0,1,0 },
            { 0,0,1,0 },
            { 0,0,1,0 } };

        }
        public override int[,] rotation3()
        {
            return new int[4, 4]{
            { 0,0,0,0 },
            { 0,0,0,0 },
            { 1,1,1,1 },
            { 0,0,0,0 } };

        }
        public override int[,] rotation4()
        {
            return new int[4, 4]{
            { 0,1,0,0 },
            { 0,1,0,0 },
            { 0,1,0,0 },
            { 0,1,0,0 } };

        }
    }
}

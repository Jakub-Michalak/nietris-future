using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetromino
    {
        public virtual string PieceSymbol()
        {
            return "-";
        }
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

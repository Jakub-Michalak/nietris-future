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
        public void move(string direction)
        {
            switch (direction) 
            {
                case "left":
                    x = x - 1;
                    break;
                case "right":
                    x = x + 1;
                    break;
                case "down":
                    y = y - 1;
                    break;
            }
               

        }


    
    }
}

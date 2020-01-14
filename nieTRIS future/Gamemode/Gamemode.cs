using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    public abstract class Gamemode
    {

        //WARUNEK WYGRANEJ
        public virtual bool CheckWinCondition(int clearedLine)
        {
            return false;
        }

        //WYPISYWANIE DANYCH KONIEC



        //WYPISYWANIE DANYCH W TRAKCIE
    }
}

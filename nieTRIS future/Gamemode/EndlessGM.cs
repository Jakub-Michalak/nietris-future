using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class EndlessGM : Gamemode
    {

        public override bool CheckWinCondition(int clearedLine)
        {
            return false;
        }

        public override string getName()
        {
            return "Endless";
        }

    }
}

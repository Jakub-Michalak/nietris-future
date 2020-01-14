using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class SprintGM : Gamemode
    {
  

        public override bool CheckWinCondition(int clearedLine)
        {
            if (clearedLine >= 40) return true;
            else return false;
        }

        public override string getName()
        {
            return "Sprint";
        }
    }
}

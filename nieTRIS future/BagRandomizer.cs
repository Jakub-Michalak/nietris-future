using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nieTRIS_future
{
    class BagRandomizer
    {
        public static List<Tetromino> GetNewBag()
        {
            List<Tetromino> AvailableTetromino = new List<Tetromino>();

            AvailableTetromino.Add(new IPiece());
            AvailableTetromino.Add(new OPiece());
            AvailableTetromino.Add(new TPiece());
            AvailableTetromino.Add(new SPiece());
            AvailableTetromino.Add(new ZPiece());
            AvailableTetromino.Add(new JPiece());
            AvailableTetromino.Add(new LPiece());

            List<Tetromino> tempList = new List<Tetromino>();

            Random rng = new Random();
            for (int i = 0; i < 7; i++)
            {
                int num = rng.Next(0, AvailableTetromino.Count());
                tempList.Add(AvailableTetromino.ElementAt(num));
                AvailableTetromino.RemoveAt(num);
            }


            return tempList;
        }

        public static void Test()
        {
            Debug.WriteLine("Next Blocks");
            List<Tetromino> TestList = GetNewBag();
            for (int i = 0; i < 7; i++)
            {
                Debug.WriteLine(TestList.ElementAt(i).PieceSymbol());
            }
        }

    }
}

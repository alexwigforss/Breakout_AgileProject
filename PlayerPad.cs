using static System.Console;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class PlayerPad
    {
        const int leftWall = 0;
        const int rightWall = 80;
        static string board = "▀▀▀▀██▀▀▀▀";
        private static int currentFirstXPosition = 35;
        public static int CurrentFirstXPosition { get => currentFirstXPosition; set => currentFirstXPosition = value; }
        public static string Board { get => board; }

        public void PrintBoard()
        {
            SetCursorPosition(currentFirstXPosition, 35);
            Write(board);
        }

       // check for walls
        public bool CheckForWalls(int x)
        {
            if (x == -1 && currentFirstXPosition == leftWall)
                return true;
            if (x==1 && currentFirstXPosition + 10 >= rightWall)
                return true;
            else return false;
        }

        //clear trail
        public void ClearTrail(int x)
        {
            SetCursorPosition(currentFirstXPosition + x, 35);
            Write(" ");
        }

        //förflytta vid knapptryckning 
        public void MoveLeft() 
        {
            if (CheckForWalls(-1))
                return;
            currentFirstXPosition--;
            PrintBoard();
            ClearTrail(10);
        } 
        public void MoveRight() 
        {
            if (CheckForWalls(1))
                return;
            currentFirstXPosition++;
            PrintBoard();
            ClearTrail(-1);
        }

    }
}

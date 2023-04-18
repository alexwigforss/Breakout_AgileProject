using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Obstacles
    {
        //poängvärde
        int scorePoints = 100;
        //träffar för att ta sönder
        int hitPoints = 1;
        //färgkod
        public static string colourCode = "u001b[31m";
        //storlek
        public static string visual="_______\n" +
                                   "|_______|";

        Obstacles[] hinder = new Obstacles[10];

        //printobstacle
        
        //minska träffantal vid träff (metod)
        public void ballHit() 
        { 
            hitPoints--; 
            checkHitPoints(); 
        }
        
        //om träffantal == 0, ta bort hinder
        public void checkHitPoints() 
        { 
            if (hitPoints > 0) return;
            else if (hitPoints == 0) CheckObstacleEvent();    //NYI
        }

        //metod, om träffantal == 0 ge +1 liv
        public static void CheckObstacleEvent()
        {
            if(colourCode== "u001b[31m")
            {
                Program.lives++;
            }
        }
        public static void PrintObstacles()
        {
            SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x, GameBoard.TopLeftBrickZoneCorner.y); Write(visual);
            SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x+2, GameBoard.TopLeftBrickZoneCorner.y); Write(visual);
            SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x+4, GameBoard.TopLeftBrickZoneCorner.y); Write(visual);
        }
    }
}

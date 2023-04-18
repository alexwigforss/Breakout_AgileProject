using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Breakout
{
    public class Obstacles
    {
        //poängvärde
        int scorePoints = 100;
        //träffar för att ta sönder
        int hitPoints = 1;
        //färgkod
        public string colour;
        //storlek
        public static string visual = "▓▓▓▓▓▓";
        

        public Obstacles(int scorePoints, int hitPoints, string colour = "green")
        {
            this.scorePoints = scorePoints;
            this.hitPoints = hitPoints;
            this.colour = colour;
        }
        static Obstacles[] hinder = new Obstacles[] { new(100, 1), new(500, 3, "blue"), new(100, 1), new(500, 3, "red"), new(100, 1), new(500, 3, "blue"), new(100, 1), new(500, 3, "blue"), new(100, 1), new(500, 3, "red")};

        //printobstacle
        void Visualize()
        {
            switch (colour) 
            { 
            case "blue":
                ForegroundColor = ConsoleColor.Blue;
                break;
            case "red":
                ForegroundColor = ConsoleColor.Red;
                break;
            case "green":
                ForegroundColor = ConsoleColor.Green;
                break;
            default: 
                    break;
            }
            Write(visual);
            ForegroundColor = ConsoleColor.White;
        }

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
        public void CheckObstacleEvent()
        {
            if(colour== "red")
            {
                Program.lives++;
            }
        }
        public static void PlaceObstacles()
        {
                SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x , GameBoard.TopLeftBrickZoneCorner.y);
            foreach (Obstacles o in hinder)
                o.Visualize();
            
            
        }
    }
}

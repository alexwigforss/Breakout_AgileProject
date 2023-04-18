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
        int scorePoints;
        // koordinater
        int xPosition;
        int yPosition;
        //träffar för att ta sönder
        int hitPoints = 1;
        //färgkod
        public string colour="white";
        //storlek
        public static string visual = "▓▓▓▓▓▓"; //alt+178
        public static string visualBroken = "▒▒▒▒▒▒"; //177
        public static string visualFull = "██████"; //219


        public Obstacles(int scorePoints, int hitPoints, int x, int y, string colour = "green")
        {
            this.scorePoints = scorePoints;
            this.hitPoints = hitPoints;
            this.colour = colour;
            this.xPosition = x;
            this.yPosition = y;
        }

        public Obstacles(int type, int x, int y)
        {
            if (type == 1)
            {
                this.scorePoints = 100;
                this.hitPoints = 1;
                this.colour = "green";
            }
            if (type == 2)
            {
                this.scorePoints = 300;
                this.hitPoints = 2;
                this.colour = "blue";
            }
            if (type == 3)
            {
                this.scorePoints = 500;
                this.hitPoints = 3;
                this.colour = "red";
            }
            this.xPosition = x;
            this.yPosition = y;
        }

        static public List<Obstacles> hinder = new();
        static Random random = new Random();
        public static void MakeObstacles()
        {
            int x = 1; int y = 5;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    int type = random.Next(1, 4);

                    Obstacles o = new Obstacles(type, x, y);
                    hinder.Add(o);
                    x += 6;
                }
                y++;
                x = 1;
            }
        }

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
            if (hitPoints == 3)
            {
                visual = visualFull;
            }
            if (hitPoints==2)
            {
                visual = visual;
            }
            else if (hitPoints == 1)
            {
                visual = visualBroken;
                return;
            }
            else if (hitPoints == 0) CheckObstacleEvent();    //NYI
        }

        //metod, om träffantal == 0 ge +1 liv
        public void CheckObstacleEvent()
        {
            if (colour == "red")
            {
                Program.lives++;
            }
        }
        public static void PlaceObstacles()
        {
            foreach (Obstacles o in hinder)
            {
                SetCursorPosition(o.xPosition, o.yPosition);
                o.Visualize();
            }
        }
    }
}

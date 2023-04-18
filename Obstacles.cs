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
        //utseende
        string VisualHealthState;
        public static string dead = "      "; // borta 0 hp
        public static string visualDamage = "▓▓▓▓▓▓"; //alt+178 2hp
        public static string visualBroken = "▒▒▒▒▒▒"; //177 1 hp
        public static string visualFull = "██████"; //219 3hp

        //old constructor
        public Obstacles(string VisualHealtState, int scorePoints, int hitPoints, int x, int y, string colour = "green")
        {
            this.VisualHealthState = VisualHealtState;
            this.scorePoints = scorePoints;
            this.hitPoints = hitPoints;
            this.colour = colour;
            this.xPosition = x;
            this.yPosition = y;
        }
        //new constructor
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
            //Alla börjar med full health oavsett hitpoints
            this.VisualHealthState = visualFull;
        }

        static public List<Obstacles> hinder = new();
        static Random random = new Random();
        public static void MakeObstacles()
        {
            int x = 1; int y = 5;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 11; i++)
                {
                    int type = random.Next(1, 4);

                    Obstacles o = new Obstacles(type, x, y);
                    hinder.Add(o);
                    x += 7;
                }
                y = y + 2;
                x = 1;
            }
        }

        //Skriv ut hinder
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
            Write(VisualHealthState);
            ForegroundColor = ConsoleColor.White;
        }

        //När bollen träffar hindret
        public void ballHit()
        {
            hitPoints--;
            checkHitPoints();
        }

        //Hindret går sönder/försvinner
        public void checkHitPoints()
        {
            //om den hade 3 hp, blir träffad en gång
            if (hitPoints == 2)
            {
                VisualHealthState = visualDamage;
            }
            else if (hitPoints == 1)
            {
                VisualHealthState = visualBroken;
            }
            else if (hitPoints == 0)
            {
                VisualHealthState = dead;
                CheckObstacleEvent(); //NYI
            }    
        }

        //Röda block ger 1 up
        public void CheckObstacleEvent()
        {
            if (colour == "red")
            {
                Program.lives++;
            }
            //TODO Lägg till fler events
        }
        //Positionera hinder
        public static void PlaceObstacles()
        {
            foreach (Obstacles o in hinder)
            {
                SetCursorPosition(o.xPosition, o.yPosition);
               /* if(o.hitPoints == 0) {
                    visual = "      ";
                }*/
                o.Visualize();
            }
        }
    }
}

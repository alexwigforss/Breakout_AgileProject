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
        public static int numberOfRows = 3; 
      
        public static int Solids = 0;       
      
        private static int checkLimmit = 9;

        public static int CheckLimmit { get => checkLimmit; set => checkLimmit = value; }

        private string visualHealthState;
        // Poängvärde
        private int scorePoints;
        // Koordinater
        private int xPosition;
        private int yPosition;
        // Träffar för att ta sönder
        private int hitPoints = 1;
        // Mängd hinder
        private static int active = 0;
        private static decimal procent;

        // Konstruktor
        public Obstacles(int type, int x, int y)
        {
            if (type <= 40)
            {
                this.scorePoints = 100;
                this.hitPoints = 1;
                this.colour = "green";
            }
            else if (type > 40 && type < 90)
            {
                this.scorePoints = 300;
                this.hitPoints = 2;
                this.colour = "blue";
            }
            else if (type >= 90)
            {
                this.scorePoints = 500;
                this.hitPoints = 3;
                this.colour = "red";
            }

            this.xPosition = x;
            this.yPosition = y;
            //Alla börjar med full health oavsett hitpoints
            this.visualHealthState = visualFull;
            active++;
        }

        // Utseende
        private const string dead = "      "; // 0 hp, blanksteg
        private const string visualFull = "██████"; // 3 hp, tecken 219
        private const string visualDamage = "▓▓▓▓▓▓"; // 2 hp, tecken 178
        private const string visualBroken = "▒▒▒▒▒▒"; // 1 hp, tecken177
        private string colour = "white";

        static public List<Obstacles> hinder = new();

        public static void MakeObstacles()
        {
            Random random = new();
            int x = 2; 
            int y = 5;
            hinder.Clear();

            // Sätt hindrets position på y-axel
            for (int j = 0; j < numberOfRows; j++)
            {
                // Sätt hindrets position på x-axeln
                for (int i = 0; i < 11; i++)
                {
                    int type = random.Next(0, 100);
                    Obstacles o = new(type, x, y);
                    hinder.Add(o);
                    x += 7;
                }

                // Ny rad

                y += 2;
                x = 2;
            }
        }

        public static void CountProcent()
        {
            Procent = ((hinder.Count - (decimal)Active) / hinder.Count) * 100;           
        }

        public static int CountObstacles()
        {
            Active = hinder.Count;
            return hinder.Count;
        }
        //Skriv ut hinder


        // När bollen träffar hindret
        public void BallHit()
        {
            if (visualHealthState== dead) return;
            hitPoints--;
            CheckHitPoints();
        }

        // Ändra hindrets status
        public void CheckHitPoints()
        {
            if (hitPoints == 2)
            {
                visualHealthState = visualDamage;
            }
            else if (hitPoints == 1)
            {
                visualHealthState = visualBroken;
            }
            else if (hitPoints == 0)
            {
                visualHealthState = dead;
                active--;
                CountProcent();
                CheckObstacleEvent();
            }
        }

        // Röda block ger 1 up
        public void CheckObstacleEvent()
        {
            if (colour == "red")
            {
                Program.Lives++;
            }
            Program.Score += scorePoints;
           
            //TODO Lägg till fler events
        }

        // Positionera hinder
        public static void PlaceObstacles()
        {
            foreach (Obstacles obs in hinder)
            {
                SetCursorPosition(obs.xPosition, obs.yPosition);
                obs.Visualize();
            }
        }

        // Skriv ut hinder

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
            Write(visualHealthState);
            ForegroundColor = ConsoleColor.White;
        }

        // Get/set
        public int YPosition { get => yPosition; set => yPosition = value; }
        public int XPosition { get => xPosition; set => xPosition = value; }
        public string VisualHealthState { get => visualHealthState; set => visualHealthState = value; }
        public static int Active { get => active; set => active = value; }
        public static string Dead { get => dead; }
        public int HitPoints { get => hitPoints; set => hitPoints = value; }
        public static decimal Procent { get => procent; set => procent = value; }
        public string Colour { get => colour; set => colour = value; }
    }
}

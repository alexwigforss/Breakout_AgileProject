using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Breakout
{
    public class Ball
    {
        private static int hitPadIndex = 0;

        V2 xyPosition;
        V2 xyPrevPosition;
        V2 speed;
        public V2 XyPosition { get => xyPosition; set => xyPosition = value; }

        // Konstruktor
        public Ball(V2 position, V2 riktning)
        {
            xyPosition = position;
            xyPrevPosition = new V2(-1, -1); // Börjar på minus så den inte suddar sig själv i första bildrutan
            speed = riktning;
        }

        public bool CheckWalls()
        {
            if (xyPosition.X + speed.X >= Program.windowWidth || xyPosition.X + speed.X < 0) // Om den träffat någon vägg
                speed.X *= -1;

            if (xyPosition.Y + speed.Y < 0) // Om träffat taket
                speed.Y *= -1;
            // Om träffat brädan
            else if ((xyPosition.Y + speed.Y == Program.windowHeight - 5) && ((xyPosition.X >= PlayerPad.CurrentFirstXPosition) && (xyPosition.X <= PlayerPad.CurrentFirstXPosition + PlayerPad.Board.Length)))
            {
                hitPadIndex = xyPosition.X - PlayerPad.CurrentFirstXPosition;
                switch (hitPadIndex)
                {
                    case 0:
                    case 1:
                        speed = new V2(-2, -1);
                        break;
                    case 2:
                    case 3:
                        speed = new V2(-1, -1);
                        break;
                    case 4:
                    case 5:
                        speed = new V2(0, -1);
                        break;
                    case 6:
                    case 7:
                        speed = new V2(1, -1);
                        break;
                    case 8:
                    case 9:
                        speed = new V2(2, -1);
                        break;
                    default:
                        hitPadIndex = 0;
                        break;
                }
                return false;
            }
            // Om "Träffat" botten
            else if (xyPosition.Y + speed.Y >= Program.windowHeight)
            {
                return true;
            }
            return false;
        }
        public void CheckObstacles(ref int ahead)
        {
            foreach (Obstacles obs in Obstacles.hinder)
            {
                if (xyPosition.Y + ahead == obs.YPosition && ((xyPosition.X >= obs.XPosition) && (xyPosition.X < obs.XPosition + 6)))
                {
                    if (obs.VisualHealthState == Obstacles.Dead)
                    {
                        return;
                    }
                    else
                    {
                        obs.BallHit();
                        speed.Y *= -1;
                        break;
                    }
                }
            }
        }

        public bool Move()
        {
            int ahead = (speed.Y < 0) ? -1 : 1;
            if (xyPosition.Y + ahead <= Obstacles.CheckLimmit)
            {
                CheckObstacles(ref ahead);
            }
            bool died = CheckWalls();
            if (!died)
            {
                xyPrevPosition = xyPosition;
                xyPosition.X += speed.X;
                xyPosition.Y += speed.Y;
            }
            return died;
        }

        public void PrintSelf()
        {
            SetCursorPosition(xyPosition.X, xyPosition.Y);
            Write("O");
        }
        public void PrintSelfClearTrail()
        {
            PrintSelf();
            ClearTrail();
        }
        public void ClearTrail()
        {
            SetCursorPosition(xyPrevPosition.X, xyPrevPosition.Y);
            Write(" ");
        }
    }
}

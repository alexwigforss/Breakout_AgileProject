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
        public static int hitPadIndex = 0;
        // public static int hitObstaclesIndex;
        static int width = 80;
        static int height = 40;
        V2 xy;
        V2 xyPrev;
        V2 speed;
        int xyDiff, diffCount;
        public V2 Xy { get => xy; set => xy = value; }

        /// <summary>
        /// Boll Constructor
        /// </summary>
        /// <param name="xy">position cordinates</param>
        /// <param name="speed">movement force</param>
        public Ball(V2 XY, V2 SPEED)
        {
            xy = XY;
            xyPrev = new V2(-1, -1); // börjar på minus så den inte suddar säg själv i första bildrutan.
            speed = SPEED;

        }
        public string XyToString()
        {
            return xy.x.ToString() + xy.y.ToString();
        }

        public bool CheckWalls()
        {
            if (xy.x + speed.x >= width || xy.x + speed.x < 0) // Om den träffat någon vägg
                speed.x *= -1;

            if (xy.y + speed.y < 0) // Om träffat taket
                speed.y *= -1;
            // Om träffat brädan
            else if ((xy.y + speed.y == height - 5) && ((xy.x >= PlayerPad.CurrentFirstXPosition) && (xy.x <= PlayerPad.CurrentFirstXPosition + PlayerPad.Board.Length)))
            {
                hitPadIndex = xy.x - PlayerPad.CurrentFirstXPosition;
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
            else if (xy.y + speed.y >= height)
            {
                return true;
            }
            return false;
        }
        public void CheckObstacles(ref int ahead)
        {

            foreach (Obstacles obs in Obstacles.hinder)
            {
                if (xy.y + ahead == obs.YPosition && ((xy.x >= obs.XPosition) && (xy.x < obs.XPosition + 6)))
                {
                    if (obs.VisualHealthState == Obstacles.dead)
                    {
                        return;
                    }
                    else
                    {
                        obs.ballHit();
                        speed.y *= -1;
                        break;
                    }
                    //om hindret är dead, studsa inte bollen
                }
            }
        }

        public bool Move()
        {
            int ahead = (speed.y < 0) ? -1 : 1;
            if (xy.y + ahead <= Obstacles.CheckLimmit)
            {
                CheckObstacles(ref ahead);
            }
            bool died = CheckWalls();
            if (!died)
            {
                xyPrev = xy;
                xy.x += speed.x;
                xy.y += speed.y;
            }
            return died;
        }

        public void PrintSelf()
        {
            SetCursorPosition(xy.x, xy.y);
            Write("O");
        }
        public void PrintSelfClearTrail()
        {
            PrintSelf();
            ClearTrail();
        }
        public void ClearTrail()
        {
            SetCursorPosition(xyPrev.x, xyPrev.y);
            Write(" ");
        }
    }
}

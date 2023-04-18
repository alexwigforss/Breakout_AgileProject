using System;
using System.Reflection.Emit;
using System.Xml;
using static System.Console;
namespace Breakout
{
    // Integral vektor för position och hastighet i x & y-led
    public struct V2
    {
        public int x;
        public int y;
        public V2(int x, int y) { this.x = x; this.y = y; }
        public V2(V2 xy) { this.x = xy.x; this.y = xy.y; }
    }

    public class Program
    {
        public static int lives = 3;
        public static int score = 0;
        static int steps_scince_start;
        public static System.Timers.Timer timestep;
        static void NotYetImplemented()
        {
            WriteLine("Not yet implemented, try another command");
        }
        static void Main(string[] args)
        {
            GameMenu();

            static void GameMenu()
            {
                SetupConsole();
                Clear();
                SetCursorPosition(0, 0);
                WriteLine("============= BREAKDOWN ==============\n");
                WriteLine("(P)lay the game\n\n" +
                    "(Q)uit the program\n\n" +
                    "(H)elp\n\n" +
                    "High(S)core\n");
                string input = "";
                do
                {
                    input = ReadLine();
                    if (input.ToLower() == "p")
                    {
                        PlayerPad bräda = new PlayerPad();

                        Game(bräda);
                    }
                    else if (input.ToLower() == "q")
                        System.Environment.Exit(0);
                    else if (input.ToLower() == "h")
                        NotYetImplemented();

                    else if (input.ToLower() == "s")
                        NotYetImplemented();

                } while (true);
            }

            // GAME

            static void Game(PlayerPad p)
            {
                Ball b;
                b = new Ball(new V2(5, 5), new V2(1, 1));
                Obstacles.MakeObstacles();

                timestep = new System.Timers.Timer();
                timestep.Interval = 150;
                timestep.Elapsed += TimerEventStep;
                timestep.Start();

                bool running = true;
                //SetupConsole();
                Clear();

                int sec = steps_scince_start;
                b.PrintSelf();

                // GAMELOOP
                while (running)
                {
                    //läser knapptryckningar för brädet
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                p.MoveLeft();
                                break;
                            case ConsoleKey.RightArrow:
                                p.MoveRight();
                                break;
                            case ConsoleKey.Escape:
                                ReInit();
                                GameMenu();
                                break;
                            default:
                                break;
                        }
                    }

                    SetCursorPosition(75, 1);
                    Write(steps_scince_start);

                    SetCursorPosition(75, 3);
                    Write(PlayerPad.CurrentFirstXPosition);

                    p.PrintBoard();
                    GameBoard.DrawFrame();
                    GameBoard.DrawBrickZoneCorners();
                    GameBoard.DrawStats(lives, score);

                    if (steps_scince_start > sec)
                    {
                        bool died = b.Move();
                        if (died)
                        {
                            b = new Ball(new V2(5, 5), new V2(1, 1));
                            if (lives > 0)
                            {
                                lives--;

                                int rowToClear = 39;
                                SetCursorPosition(0, rowToClear);
                                Write(new string(' ', WindowWidth));
                            }
                            else
                            {
                                ReInit();
                                //System.Timers.Timer timestep;
                                GameMenu();
                            }
                        }
                        else if (!died)
                        {
                            sec = steps_scince_start;
                            b.PrintSelfClearTrail();

                        }

                    }
                }

                static void ReInit()
                {
                    score = 0;
                    lives = 3;
                    steps_scince_start = 0;
                    timestep.Stop();
                }
            }
        }

        static int windowWidth = 80;
        static int windowHeight = 40;

        private static void SetupConsole()
        {
            SetWindowSize(1, 1);
            SetBufferSize(windowWidth + GameBoard.margin, windowHeight);
            SetWindowSize(windowWidth + GameBoard.margin, windowHeight);
            CursorVisible = false;
        }
        public static void TimerEventStep(Object source, System.Timers.ElapsedEventArgs e)
        {
            steps_scince_start++;
        }
    }
}
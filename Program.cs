using System;
using System.Reflection.Emit;
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
                        PlayerBoard bräda = new PlayerBoard();

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
            
             static void Game(PlayerBoard p)
            {
                Ball b;
                b = new Ball(new V2(5, 5), new V2(1, 1));

                timestep = new System.Timers.Timer();
                timestep.Interval = 100;
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
                                GameMenu();
                                break;
                            default:
                                break;
                        }
                    }

                    SetCursorPosition(75, 1);
                    Write(steps_scince_start);

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
                            }
                            else
                            {
                                GameMenu();
                            }
                        }
                        else if(!died)
                        {
                        sec = steps_scince_start;
                        b.PrintSelfClearTrail();

                        }

                    }
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
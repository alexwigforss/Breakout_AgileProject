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
        public static int previousScore = 0;
        public static bool gameover = false;
        static int steps_scince_start;
        public static System.Timers.Timer timestep;
        static void Main(string[] args)
        {
            GameMenu();

            static void GameMenu()
            {
                SetupConsole();
                Clear();
                SetCursorPosition(0, 0);
                string input = "";
                Welcome();
                do
                {
                    Write("> ");
                    input = ReadLine();
                    if (input.ToLower() == "p")
                    {
                        PlayerPad bräda = new PlayerPad();
                        Game(bräda);
                    }
                    else if (input.ToLower() == "q")
                        Environment.Exit(0);
                    else if (input.ToLower() == "h")
                        HelpMenu();
                    else if (input.ToLower() == "s")
                        WriteLine("Not yet implemented, try another command");

                } while (true);
            }

            // GAME

            static void Game(PlayerPad p)
            {
                Ball b;
                Obstacles.MakeObstacles();
                Obstacles.CountNotDead();



                b = new Ball(new V2(5, 25), new V2(1, -1));

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
                            b = new Ball(new V2(5, 2), new V2(1, 1));
                            if (lives > 0)
                            {
                                lives--;
                                int rowToClear = 39;
                                SetCursorPosition(0, rowToClear);
                                Write(new string(' ', WindowWidth));
                                gameover = false;
                            }
                            else
                            {
                                gameover = true;
                                previousScore= score;
                                ReInit();
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
                    PlayerPad.CurrentFirstXPosition = 35;
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

        static void Welcome()
        {
            if (gameover)
            {
                Write("=============== BREAKDOWN ===============\n\n" +
                    " ___   _         ____  ___   ___   _     ___   ___    __    ___   ____ \r\n| | \\ | | |     | |_  / / \\ | |_) | |   / / \\ | |_)  / /\\  | | \\ | |_  \r\n|_|_/ \\_\\_/     |_|   \\_\\_/ |_| \\ |_|__ \\_\\_/ |_| \\ /_/--\\ |_|_/ |_|__\n\n" +
                    $"Du klarade {(int)Obstacles.procent}%\n\n"+
                    $"Du fick {previousScore} poäng\n\n" +
                    "(P)lay\n\n" +
                          "(Q)uit\n\n" +
                          "(H)elp\n\n" +
                          "High(S)core\n");
            }
            else
            {
                WriteLine("=============== BREAKDOWN ===============\n\n" +
                          "(P)lay\n\n" +
                          "(Q)uit\n\n" +
                          "(H)elp\n\n" +
                          "High(S)core\n");
            }
        }

        private static void HelpMenu()
        {
            Clear();
            WriteLine("\n======== NAVIGATING THE MENU ============\n\n" +
                "To execute the menu commands, type the corresponding letter and press 'enter':\n" +
                "Type 'P' and 'enter' to play the game\n" +
                "Type 'S' and 'enter' to see the highscore list\n" +
                "Type 'Q' and 'enter' to quit the program\n" +
                "Type 'H' and 'enter' to view this information\n\n" +
                "=============== GAMEPLAY ================\n\n" +
                "Type 'p' and press 'enter' to start the game. \n" +
                "   Control the paddle att the bottom of the screen with left and right arrow. \n" +
                "   Use the ball to break the obstacles to score points and clear the level. \n" +
                "You start with three balls, if you miss a ball and it goes past the paddle you lose the ball.\n" +
                "   Some obstacles give you an extra ball when they break.\n" +
                "   The game ends when you lose your last ball.\n" +
                "Press 'esc' to end the game and return to Start Menu. \n\n" +
                "=============== HIGHSCORE ===============\n\n" +
                "Type 's' and press 'enter' to view the Highscore list. \n" +
                "   The players with the three best scores can be seen on the screen.\n" +
                "You score points by breaking obstacles and collecting items.\n" +
                "   Some obstacles are worth more points than others.\n" +
                "If you score higher than previous players you can add your name to the list.\n\n" +
                "Press any key to return to the Start Menu");
            ReadKey();
            Clear();
            Welcome();
        }
    }
}
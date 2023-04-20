using System;
using System.Reflection.Emit;
using System.Xml;
using static System.Console;
namespace Breakout
{
    // Integral vektor för position och hastighet i x & y-led
    public struct V2
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public V2(int x, int y) { this.x = x; this.y = y; }
        public V2(V2 xy) { this.x = xy.X; this.y = xy.Y; }
    }

    public class Program
    {
        private static int lives = 3;
        private static int score = 0;
        private static int previousScore = 0;
        private static bool gameover = false;
        private static int steps_since_start;
        private static System.Timers.Timer timestep = new();
        public const int windowWidth = 80;
        public const int windowHeight = 40;
        public const int margin = 20;


        public static bool newgame = true;

        public static PlayerPad bräda = new();

        public static int Lives { get => lives; set => lives = value; }
        public static int Score { get => score; set => score = value; }
        public static int PreviousScore { get => previousScore; set => previousScore = value; }
        public static bool Gameover { get => gameover; set => gameover = value; }
        public static int Steps_since_start { get => steps_since_start; set => steps_since_start = value; }
        public static System.Timers.Timer Timestep { get => timestep; set => timestep = value; }

        static void Main(string[] args)
        {

            GameMenu();
        }

        static void GameMenu()
        {
            SetupConsole();
            Clear();
            SetCursorPosition(0, 0);
            string? input;

            Welcome();
            // Meny-loop
            do
            {
                Write("> ");
                input = ReadLine();
                if (input.ToLower() == "p")
                {
                    bräda = new();
                    Game(bräda);
                }
                else if (input.ToLower() == "q")
                    Environment.Exit(0);
                else if (input.ToLower() == "h")
                    HelpMenu();
                else if (input.ToLower() == "s")
                    WriteLine("Not yet implemented, try another command");

            }
            while (true);
        }
        static void NextLevel()
        {
            bräda = new();
            Game(bräda);
        }

        // Startar nytt spel
        static void Game(PlayerPad pad)
        {

            bool running = true;
            int sec = Steps_since_start;

            // Startar ny tidtagning
            Timestep = new();
            Timestep.Interval = 100;
            Timestep.Elapsed += TimerEventStep;
            Timestep.Start();

            Clear();
            Ball ball = new(new V2(40, 10), new V2(0, 1));
            Obstacles.MakeObstacles();
            Obstacles.CountObstacles();
            GameBoard.DrawFrame();
            ball.PrintSelf();

            // Spel-loop
            while (running)
            {
                Obstacles.PlaceObstacles();

                // Läser knapptryckningar för brädet
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            pad.MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            pad.MoveRight();
                            break;
                        case ConsoleKey.Escape:
                            ReInit();
                            GameMenu();
                            break;
                        default:
                            break;
                    }
                }

                pad.PrintBoard();
                GameBoard.DrawStats(Lives, Score);

                // Avslutar banan när 9 hinder är borta
                if (Obstacles.Active <= 30)
                {
                    NewLevelInit();
                    running = false;
                }

                if (Steps_since_start > sec)
                {
                    bool died = ball.Move();
                    if (died)
                    {
                        ball = new(new V2(40, 10), new V2(0, 1));
                        if (Lives > 0)
                        {
                            Lives--;
                            int rowToClear = 39;
                            SetCursorPosition(0, rowToClear);
                            Write(new string(' ', WindowWidth));
                            Gameover = false;
                        }
                        else if (Lives == 0)
                        {
                            Gameover = true;
                            PreviousScore = Score;

                            // TODO Print game over ( not success ) screen
                            ReInit();
                            GameMenu();
                        }
                    }
                    else if (!died)
                    {
                        sec = Steps_since_start;
                        ball.PrintSelfClearTrail();

                    }

                }
            }

            PrintSuccesScreen();
            NextLevel();

            static void ReInit()
            {
                PlayerPad.CurrentFirstXPosition = 35;
                Score = 0;
                Lives = 3;
                steps_since_start = 0;
                Timestep.Stop();
                newgame = true;
            }
            static void NewLevelInit()
            {
                PlayerPad.CurrentFirstXPosition = 35;
                steps_since_start = 0;
                Timestep.Stop();
                Obstacles.numberOfRows++;
                Obstacles.Procent = 0;
                Obstacles.Active = 0;
                Obstacles.CheckLimmit += 2;
                newgame = false;
            }

            static void PrintSuccesScreen()
            {
                Clear();
                SetCursorPosition(0, 0);

                WriteLine(@"  ________              __    __  .__        ");
                WriteLine(@" /  _________________ _/  |__/  |_|__| ______");
                WriteLine(@"/   \  __\_  __ \__  \\   __\   __|  |/  ___/");
                WriteLine(@"\    \_\  |  | \// __ \|  |  |  | |  |\___ \ ");
                WriteLine(@" \______  |__|  (____  |__|  |__| |__/____  >");
                WriteLine();
                WriteLine("Du har klarat banan.");
                WriteLine("Tryck valfri knapp för nästa!");
                ReadKey();

            }

            static void PrintWinnerScreen()
            {
                Clear();
                SetCursorPosition(0, 0);


                WriteLine(@"  ________              __    __  .__        ");
                WriteLine(@" /  _________________ _/  |__/  |_|__| ______");
                WriteLine(@"/   \  __\_  __ \__  \\   __\   __|  |/  ___/");
                WriteLine(@"\    \_\  |  | \// __ \|  |  |  | |  |\___ \ ");
                WriteLine(@" \______  |__|  (____  |__|  |__| |__/____  >");
                WriteLine(@"    .___\/           \/                 ._./ ");
                WriteLine(@"  __| ___ __  ___  ______    ____   ____| |  ");
                WriteLine(@" / __ |  |  \ \  \/ \__  \  /    \ /    | |  ");
                WriteLine(@"/ /_/ |  |  /  \   / / __ \|   |  |   |  \|  ");
                WriteLine(@"\____ |____/    \_/ (____  |___|  |___|  __  ");
                WriteLine(@"     \/                  \/     \/     \/\/  ");
                WriteLine("Tryck valfri knapp för omstart!");
                ReadKey();
                ReInit();
            }
        }

        private static void SetupConsole()
        {
            SetWindowSize(1, 1);
            SetBufferSize(windowWidth + margin, windowHeight);
            SetWindowSize(windowWidth + margin, windowHeight);
            CursorVisible = false;
        }
        public static void TimerEventStep(Object source, System.Timers.ElapsedEventArgs e)
        {
            Steps_since_start++;
        }

        static void Welcome()
        {
            if (Gameover)
            {
                Write("=============== BREAKDOWN ===============\n\n" +
                    " ___   _         ____  ___   ___   _     ___   ___    __    ___   ____ \r\n| | \\ | | |     | |_  / / \\ | |_) | |   / / \\ | |_)  / /\\  | | \\ | |_  \r\n|_|_/ \\_\\_/     |_|   \\_\\_/ |_| \\ |_|__ \\_\\_/ |_| \\ /_/--\\ |_|_/ |_|__\n\n" +
                    $"Du klarade {(int)Obstacles.Procent}%\n\n" +
                    $"Du fick {PreviousScore} poäng\n\n" +
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
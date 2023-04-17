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

    internal class Program
    {
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
                        //break;
                    else if (input.ToLower() == "h")
                        NotYetImplemented();

                    else if (input.ToLower() == "s")
                        NotYetImplemented();

                } while (true);
            }

            static void Game(PlayerBoard p)
            {
                int lives = 3;
                int score = 10000;
                bool running = true;
                //SetupConsole();
                Clear();

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

                    p.PrintBoard();
                    GameBoard.DrawFrame();
                    GameBoard.DrawBrickZoneCorners();
                    GameBoard.DrawStats(lives, score);
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
    }
}
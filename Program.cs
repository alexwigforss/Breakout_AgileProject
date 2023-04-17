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
        static int margin = 20;
        static int windowWidth = 80;
        static int windowHeight = 40;
        static void NotYetImplemented()
        {
            WriteLine("Not yet implemented, try another command");
        }
        static void Main(string[] args)
        {
            SetWindowSize(80, 40);
            SetBufferSize(80, 40);
            CursorVisible = false;

            WriteLine("============= BREAKDOWN ==============\n");
            WriteLine("(P)lay the game\n\n" +
                "(Q)uit the program\n\n" +
                "(H)elp\n\n" +
                "High(S)core\n");
            string input="";    
            do
            {
                input=ReadLine();
                if (input.ToLower() == "p")
                {
                    PlayerBoard bräda = new PlayerBoard();
                    bräda.PrintBoard();
                    Game(bräda);
                }
                else if (input.ToLower() == "q")
                    break;
                else if (input.ToLower() == "h")
                    NotYetImplemented();

                else if (input.ToLower() == "s")
                    NotYetImplemented();

            } while (true);
        }
        //läser knapptryckningar för brädet
        static void Game(PlayerBoard p)
        {
            while (true)
            {
                if (KeyAvailable)
                {
                    if (ReadKey().Key == ConsoleKey.RightArrow)
                    {
                        p.MoveRight();
                    }
                }
                else if (KeyAvailable)
                {
                    if (ReadKey().Key == ConsoleKey.LeftArrow)
                    {
                        p.MoveLeft();
                    }
                }
            }
            int lives = 3;
            int score = 10000;

            SetupConsole();

            while (true)
            {
                DrawFrame();
                DrawBrickZoneCorners();
                DrawStats(lives, score);
            }
        }

        private static void DrawStats(int lives, int score)
        {
            SetCursorPosition(GameBoard.BottomRightCorner.x + 3, 2);
            Write($"LIVES: {lives}");

            SetCursorPosition(GameBoard.BottomRightCorner.x + 3, 4);
            Write($"SCORE: {score}");
        }

        private static void DrawBrickZoneCorners()
        {
            SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x, GameBoard.TopLeftBrickZoneCorner.y);
            Write("X");

            SetCursorPosition(GameBoard.BottomRightBrickZoneCorner.x, GameBoard.BottomRightBrickZoneCorner.y);
            Write("X");
        }

        private static void DrawFrame()
        {
            SetCursorPosition(0, 0);
            Write("┌");

            SetCursorPosition(GameBoard.BottomRightCorner.x + 1, 0);
            Write("┬");

            SetCursorPosition(GameBoard.BottomRightCorner.x + margin, 0);
            Write("┐");

            SetCursorPosition(GameBoard.BottomRightCorner.x + 1, GameBoard.BottomRightCorner.y);
            Write("└");

            SetCursorPosition(GameBoard.BottomRightCorner.x + margin, GameBoard.BottomRightCorner.y);
            Write("┘");

            for (int i = 1; i < GameBoard.BottomRightCorner.y; i++)
            {
                SetCursorPosition(GameBoard.BottomRightCorner.x + 1, i);
                Write("│");
                SetCursorPosition(0, i);
                Write("│");
                SetCursorPosition(GameBoard.BottomRightCorner.x + margin, i);
                Write("│");
            }
            for (int i = 1; i <= GameBoard.BottomRightCorner.x; i++)
            {
                SetCursorPosition(i, 0);
                Write("─");
            }
            for (int i = GameBoard.BottomRightCorner.x + 2; i < GameBoard.BottomRightCorner.x + margin; i++)
            {
                SetCursorPosition(i, 0);
                Write("─");
                SetCursorPosition(i, GameBoard.BottomRightCorner.y);
                Write("─");
            }
        }

        private static void SetupConsole()
        {
            SetWindowSize(1, 1);
            SetBufferSize(windowWidth + margin, windowHeight);
            SetWindowSize(windowWidth + margin, windowHeight);
            CursorVisible = false;
        }
    }
}
/*
┌───────────────────┬─────┐
│                   │     │
│                   │     │
│                   │     │
│                   │     │
│                   │     │
│                   │     │
│                   └─────┘
 */
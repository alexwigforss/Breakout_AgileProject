using static System.Console;
namespace Breakout
{
    public static class GameBoard
    {
        public static int margin = 20;
        public static int width = 80;
        public static int height = 40;

        static V2 topLeftCorner = new V2(1, 1);
        static V2 bottomRightCorner = new V2(79, 39);

        static V2 topLeftBrickZoneCorner = new V2(1, 5);
        static V2 bottomRightBrickZoneCorner = new V2(79, 19);

        public static int Width { get => width; set => width = value; }
        public static int Height { get => height; set => height = value; }
        public static V2 TopLeftCorner { get => topLeftCorner; set => topLeftCorner = value; }
        public static V2 BottomRightCorner { get => bottomRightCorner; set => bottomRightCorner = value; }
        public static V2 TopLeftBrickZoneCorner { get => topLeftBrickZoneCorner; set => topLeftBrickZoneCorner = value; }
        public static V2 BottomRightBrickZoneCorner { get => bottomRightBrickZoneCorner; set => bottomRightBrickZoneCorner = value; }

        public static void DrawStats(int lives, int score)
        {
            SetCursorPosition(GameBoard.BottomRightCorner.x + 3, 2);
            Write($"LIVES: {lives}");

            SetCursorPosition(GameBoard.BottomRightCorner.x + 3, 4);
            Write($"SCORE: {score}");
        }

        public static void DrawBrickZoneCorners()
        {
            SetCursorPosition(GameBoard.TopLeftBrickZoneCorner.x, GameBoard.TopLeftBrickZoneCorner.y);
            Write("X");

            SetCursorPosition(GameBoard.BottomRightBrickZoneCorner.x, GameBoard.BottomRightBrickZoneCorner.y);
            Write("X");
        }

        public static void DrawFrame()
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
    }
}

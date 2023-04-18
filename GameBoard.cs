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
            SetCursorPosition(BottomRightCorner.x + 3, 2);
            Write($"LIVES: {lives}");

            SetCursorPosition(BottomRightCorner.x + 3, 4);
            Write($"SCORE: {score}");
        }

        public static void DrawBrickZoneCorners()
        {
            SetCursorPosition(TopLeftBrickZoneCorner.x, TopLeftBrickZoneCorner.y);
            Write("X");

            SetCursorPosition(BottomRightBrickZoneCorner.x, BottomRightBrickZoneCorner.y);
            Write("X");
        }

        public static void DrawFrame()
        {
            //SetCursorPosition(0, 0);
            //Write("┌");

            SetCursorPosition(BottomRightCorner.x + 1, 0);
            Write("┬");

            SetCursorPosition(BottomRightCorner.x + margin, 0);
            Write("┐");

            SetCursorPosition(BottomRightCorner.x + 1, BottomRightCorner.y);
            Write("└");

            SetCursorPosition(BottomRightCorner.x + margin, BottomRightCorner.y);
            Write("┘");

            for (int i = 1; i < BottomRightCorner.y; i++)
            {
                SetCursorPosition(BottomRightCorner.x + 1, i);
                Write("│");
                //SetCursorPosition(0, i);
                //Write("│");
                SetCursorPosition(BottomRightCorner.x + margin, i);
                Write("│");
            }
            for (int i = 1; i <= BottomRightCorner.x; i++)
            {
                //SetCursorPosition(i, 0);
                //Write("─");
            }
            for (int i = BottomRightCorner.x + 2; i < BottomRightCorner.x + margin; i++)
            {
                SetCursorPosition(i, 0);
                Write("─");
                SetCursorPosition(i, BottomRightCorner.y);
                Write("─");
            }
        }
    }
}

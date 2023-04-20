using static System.Console;
namespace Breakout
{
    public static class GameBoard
    {

        private static V2 topLeftCorner = new(1, 1);
        private static V2 bottomRightCorner = new(79, 39);
        public static V2 TopLeftCorner { get => topLeftCorner; }
        public static V2 BottomRightCorner { get => bottomRightCorner;}

        // Skriver statistik bredvid spelplanen
        public static void DrawStats(int lives, int score)
        {
            SetCursorPosition(BottomRightCorner.X + 3, 2);
            Write($"LIVES: {lives}");

            SetCursorPosition(BottomRightCorner.X + 3, 4);
            Write($"SCORE: {score}");

            SetCursorPosition(BottomRightCorner.X + 3, 6);
            Write($"PROGRESS: {(int)Obstacles.Procent}%"); 

            SetCursorPosition(BottomRightCorner.X + 3, 8);
            Write($"OBSTACLES: {Obstacles.Active}/{Obstacles.hinder.Count}");
        }

        public static void DrawFrame()
        {
            SetCursorPosition(BottomRightCorner.X + 1, 0);
            Write("┬");

            SetCursorPosition(BottomRightCorner.X + Program.margin, 0);
            Write("┐");

            SetCursorPosition(BottomRightCorner.X + 1, BottomRightCorner.Y);
            Write("└");

            SetCursorPosition(BottomRightCorner.X + Program.margin, BottomRightCorner.Y);
            Write("┘");

            for (int i = 1; i < BottomRightCorner.Y; i++)
            {
                SetCursorPosition(BottomRightCorner.X + 1, i);
                Write("│");
                SetCursorPosition(BottomRightCorner.X + Program.margin, i);
                Write("│");
            }
            for (int i = BottomRightCorner.X + 2; i < BottomRightCorner.X + Program.margin; i++)
            {
                SetCursorPosition(i, 0);
                Write("─");
                SetCursorPosition(i, BottomRightCorner.Y);
                Write("─");
            }
        }
    }
}

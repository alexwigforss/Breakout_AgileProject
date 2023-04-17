namespace Breakout
{
    public static class GameBoard
    {
        static int width = 80;
        static int height = 40;
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

    }
}

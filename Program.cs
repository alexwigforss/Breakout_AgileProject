using static System.Console;

namespace Breakout
{
    internal class Program
    {
        static void NotYetImplemented()
        {
            WriteLine("Not yet implemented, try another command");
        }
        static void Main(string[] args)
        {
            SetWindowSize(80, 40);
            SetBufferSize(80, 40);
            
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
        }
    }
}
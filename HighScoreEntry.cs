using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Breakout
{
    public class HighScoreEntry
    {

        public string Name;
        public int HSScore;
        public HighScoreEntry(string lineFromFile)
        {
            string[] field = lineFromFile.Split(",");
            Name = field[0];
            HSScore = Convert.ToInt32(field[1]);
        }
        public static List<HighScoreEntry> highScoreEntries = new List<HighScoreEntry>();
        public static void ReadHighScoreFromFile()
        {

            using (StreamReader file = new StreamReader("HSlist.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                    highScoreEntries.Add(new HighScoreEntry(line));
            }

        }
        public static void AddHighScoreToFile(string inpName, int newHScore)
        {
            using (StreamWriter file = new StreamWriter("HSlist.txt", true))
            {

                file.WriteLine(inpName + "," + newHScore);
            }
        }
        public static void PrintList()
        {
            highScoreEntries.Sort((x, y) => y.HSScore.CompareTo(x.HSScore));
            foreach (HighScoreEntry highScoreEntry in highScoreEntries)
            {
                WriteLine($"{highScoreEntry.Name}, {highScoreEntry.HSScore} ");
            }
            highScoreEntries.Clear();
        }
    }

}


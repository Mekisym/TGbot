using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TGbot
{
    
    class game
    {
        private static string wordsPath = AppDomain.CurrentDomain.BaseDirectory + "Words.txt";

        public static string getWord()
        {
            Random random = new Random();

            List<string> myList = new List<string>();

            using (FileStream file = new FileStream(wordsPath, FileMode.Open))
            {
                using (StreamReader stream = new StreamReader(file))
                {
                    for (int i = 0; i < 465; i++)
                    {
                        string line = stream.ReadLine();
                        if (line != null)
                        {
                            myList.Add(line);
                        }
                    }
                }
            }

            return myList[random.Next(0, myList.Count)];

        }

        public static string shifrW(string word)
        {
            var wordArr = word.ToArray();
            var shifr = Regex.Replace(word, @".", "-").ToArray();

            shifr[0] = wordArr[0];
            shifr[shifr.Length - 1] = wordArr[wordArr.Length - 1];

            word = "";

            for (int i = 0; i < wordArr.Length; i++)
            {
                word += shifr[i];
            }

            return word;
        }

        public string gaming(char letter, string word, string shifredW)
        {
            if ((int)letter == 63)
            {
                letter = (char)1110;
            }

            var shifredArr = shifredW.ToArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    shifredArr[i] = letter;
                }
            }
            shifredW = "";
            for (int i = 0; i < shifredArr.Length; i++)
            {
                shifredW += shifredArr[i];
            }

            return shifredW;
        }

        public string textPicture(int number)
        {
            string picture;
            switch (number)
            {
                case 1:
                    picture = "|---------\n" + "|\n" + "|\n" + @"|      | " + "\n|" + "\n|";
                    break;
                case 2:
                    picture = "|---------\n" + "|\n" + "|\n" + @"|     /| " + "\n|" + "\n|";
                    break;
                case 3:
                    picture = "|---------\n" + "|\n" + "|\n" + @"|     /|\" + "\n|" + "\n|";
                    break;
                case 4:
                    picture = "|---------\n" + "|\n" + "|\n" + @"|     /|\" + "\n|       \\" + "\n|";
                    break;
                case 5:
                    picture = "|---------\n" + "|\n" + "|\n" + @"|     /|\" + "\n|     / \\" + "\n|";
                    break;
                case 6:
                    picture = "|---------\n" + "|      |\n" + "|     ( )\n" + @"|     /|\" + "\n|     / \\" + "\n|";
                    break;
                default:
                    picture = @"\(0_0)/";
                    break;
            }
            return picture;
        }
    }
}

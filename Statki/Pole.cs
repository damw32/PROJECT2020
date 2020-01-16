using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Statki
{
    class Pole
    {
        public const String Zestrzelony = " ";
        public const String Pudlo = "O";
        public const String Lodz = "$";
        public const String Sukces = "*";

        private PunktyPola[,] _Points = new PunktyPola[10, 10];

        private static readonly String[] _Letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        
        public void Ustaw(Point point, PunktyPola pointValue)
        {
            if (_Points[point.X, point.Y] != PunktyPola.Zestrzelony && _Points[point.X, point.Y] != PunktyPola.Lodz)
                throw new Exception("Ten punkt został już wykorzystany");

            _Points[point.X, point.Y] = pointValue;
        }

        
        public void RysPole(Boolean playerView)
        {
            Console.Write(" |");

            for (Int32 X = 1; X < 11; X++)
                Console.Write("{0}|", X);

            Console.WriteLine();
            Console.WriteLine("----------------------");

            for (Int32 Y = 0; Y < 10; Y++)
            {
                Console.Write("{0}|", _Letters[Y]);

                for (Int32 X = 1; X < 11; X++)
                {
                    PunktyPola Point = _Points[X - 1, Y];
                    Console.ForegroundColor = ConsoleColor.Green;

                    switch (Point)
                    {
                        case PunktyPola.Sukces:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(Sukces);
                            break;
                        case PunktyPola.Pudlo:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(Pudlo);
                            break;
                        case PunktyPola.Lodz:
                            Console.ForegroundColor = playerView ? ConsoleColor.Yellow : ConsoleColor.Green;
                            Console.Write(playerView ? Lodz : Zestrzelony);
                            break;
                        default: //unpinned
                            Console.Write(Zestrzelony);
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");
        }

        
        public static Int32 Wartoscliter(String letter)
        {
            if (String.IsNullOrEmpty(letter))
                throw new ArgumentException("litera");

            Int32 Index = Array.IndexOf(_Letters, letter.ToUpper());

            if (-1 != Index)
                return Index;

            throw new Exception("Musisz wpisać literę od A do J");
        }
    }
}

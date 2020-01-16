using System;
using System.Collections.Generic;
using System.Text;
using Statki.Statki;
using System.Drawing;

namespace Statki
{
    public class Gra
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            DisplayStart();

            Gracz[] Players = new Gracz[] { new Gracz(), new Gracz() };

            SetupPlayer(Players[0], 1); //Gracz1
            SetupPlayer(Players[1], 2); //Gracz2

            RunGameLoop(Players);
            Console.ReadLine();
        }

        private void RunGameLoop(Gracz[] players)
        {
            Int32 CurrentPlayer = 0;
            Gracz PlayerWhoWon = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("{0} twoja kolej", players[CurrentPlayer].Nazwa);
                    Console.WriteLine();
                    Console.WriteLine("Przeciwnik wyzej\r\n");
                    players[CurrentPlayer == 0 ? 1 : 0].RysPole(false);
                    Console.WriteLine();
                    Console.WriteLine("Ty (Dolny)\r\n");
                    players[CurrentPlayer].RysPole(true);

                    Console.WriteLine();

                    String Point = null;

                    while (String.IsNullOrEmpty(Point))
                    {
                        Console.Write("Wprowadz lokalizacje (Np : A5): ");
                        Point = Console.ReadLine();
                    }

                    Console.WriteLine();

                    Point CheckedPoint = CheckPoint(Point);

                    if (players[CurrentPlayer == 0 ? 1 : 0].JestSukces(CheckedPoint))
                    {
                        if (players[CurrentPlayer == 0 ? 1 : 0].Allzatopione)
                        {
                            PlayerWhoWon = players[CurrentPlayer];
                            break;
                        }
                    }

                    Console.WriteLine("Wcisnij Enter by kontynuowac.");
                    Console.ReadLine();
                    CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Wcisnij Enter by kontynuowac.");
                    Console.ReadLine();
                }

                Console.Clear();
            }

            Console.Clear();
            Console.WriteLine("{0} wygral! Koniec.", PlayerWhoWon.Nazwa);
        }

        private void SetupPlayer(Gracz player, Int32 playerNumber)
        {
            Console.WriteLine("*************Gracz {0}*************\r\n\r\n", playerNumber);

            while (String.IsNullOrEmpty(player.Nazwa))
            {
                Console.Write("Wprowadz imie : ");
                player.Nazwa = Console.ReadLine();
            }

            Console.WriteLine();

            SetupBoats(player);

            Console.WriteLine();
            Console.Clear();
        }

        private void SetupBoats(Gracz player)
        {
            Statek[] Boats =
            {
                new Lotniskowiec(),
                new OkretWojenny(),
                new Niszczyciel(),
                new Patrolowiec(),
                new Podwodny()
            };

            Console.WriteLine("Teraz wprowadz punkty dla roznych lodzi..\r\n\r\n" +
                "Gdy zostaniesz poproszony o punkty, wpisz je w ten sposób: A5;A6;A7\r\n\r\n\r\n");

            foreach (Statek Boat in Boats)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Wprowadz {0} punktow dla {1} : ", Boat.Trafienia, Boat.Nazwa);
                        String PointsRead = Console.ReadLine().Trim().Replace(" ", "");

                        if (String.IsNullOrEmpty(PointsRead))
                            continue;

                        String[] Points = PointsRead.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        if (0 == Points.Length)
                            continue;

                        Boat.Points = new List<Trafienie>(Boat.Trafienia);

                        Int32 X = -1, Y = -1;

                        for (Int32 J = 0; J < Points.Length; J++)//(String Point in Points)
                        {
                            Point CheckedPoint = CheckPoint(Points[J]);

                            if (X != -1)
                            {
                                if (Math.Abs(X - CheckedPoint.X) > 1 || Math.Abs(Y - CheckedPoint.Y) > 1)
                                    throw new Exception("Punkty lokalizacji lodzi musza byc wprowadzane w kolejnosci sekwencyjnej i nie mogą zawierac przerw");
                            }

                            X = CheckedPoint.X;
                            Y = CheckedPoint.Y;

                            Boat.Points.Add(new Trafienie { Lokalizacja = CheckedPoint });
                        }

                        player.Dodaj(Boat);

                        break;

                    }
                    catch (Exception Ex)
                    {
                        Boat.Points = null;
                        Console.WriteLine(Ex.Message);
                        continue;
                    };
                }
            }
        }

        private Point CheckPoint(String point)
        {
            if (point.Length < 2 || point.Length > 3)
                throw new Exception(String.Format("Musi miec litere i cyfre dla kazdego punktu. Zobacz punkt {0}.", point));

            Int32 Y = Pole.Wartoscliter(point[0].ToString());
            Int32 X = Convert.ToInt32(point.Substring(1));

            if (X < 1 || X > 10)
                throw new Exception("Nie mozna wprowadzic wartosci liczbowej mniejszej niż 1 lub wiekszej niz 10");

            return new Point(X - 1, Y);
        }

        private void DisplayStart()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("<-------Statki------>");
            Console.WriteLine("<-------Autor------>");
            Console.WriteLine("Damian Wyszynski\r\n\r\n\r");
            Console.WriteLine("Powieksz okno!");
            Console.WriteLine();
            Console.WriteLine("Legenda:\r\n\r\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Lodz  : {0}", Pole.Lodz);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Pudlo  : {0}", Pole.Pudlo);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sukces   : {0}", Pole.Sukces);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Wcisnij 'Enter' aby zaczac!");
            Console.ReadLine();
            Console.Clear();
        }

        //public void Stats()
        //{
        //    int TotalWins;
        //    int TotalLosses;
        //    double WinLossRatio;
        //}
    }
}

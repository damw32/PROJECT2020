using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Statki
{
    class Gracz
    {
        private List<Statek> Statki_ = new List<Statek>(5);

        private Pole Pole_ = new Pole();

        public String Nazwa { get; set; }

        
        public Boolean Allzatopione { get { return Statki_.All((boat) => boat.Zatopiony); } }

        
        public void Dodaj(Statek boat)
        {
            Sprawdz(boat);
            Statki_.Add(boat);

            foreach (Trafienie Point in boat.Points)
                Pole_.Ustaw(Point.Lokalizacja, PunktyPola.Lodz);
        }

        private void Sprawdz(Statek boat)
        {
            if (null == boat)
                throw new ArgumentNullException("lodz");

            if (Statki_.Count == 5)
                throw new Exception("Pole nie moze zawierac wiecej niz 5 lodzi");

            if (boat.Trafienia != boat.Points.Count)
                throw new Exception(String.Format("Nie ustawiles odpowiedniej liczby punktow dla tej lodzi. Ta lodz wymaga {0} punktow.", boat.Trafienia));

            if (boat.Points.Count != boat.Points.Distinct().Count())
                throw new Exception(String.Format("Co najmniej jeden z ustalonych punktow jest taki sam dla {0}.", boat.Nazwa));

            Boolean XAllSame = boat.Points.TrueForAll((point) => point.Lokalizacja.X.Equals(boat.Points[0].Lokalizacja.X));

            if (XAllSame)
                return;

            Boolean YAllSame = boat.Points.TrueForAll((point) => point.Lokalizacja.Y.Equals(boat.Points[0].Lokalizacja.Y));

            if (!YAllSame)
                throw new Exception(String.Format("{0} nie moze byc przekatna", boat.Nazwa));

            Statek OverlapBoat = Statki_.Find((aBoat) => aBoat.PointsOverlap(boat));

            if (OverlapBoat != null)
                throw new Exception(String.Format("{0} pokrywa się z innym {1}.", boat.Nazwa, OverlapBoat.Nazwa));
        }

        
        public void RysPole(Boolean playerView)
        {
            Pole_.RysPole(playerView);
        }

      
        public Boolean JestSukces(Point point)
        {
            foreach (Statek Boat in Statki_)
            {
                if (Boat.JestSukces(point))
                {
                    Pole_.Ustaw(point, PunktyPola.Sukces);

                    if (Boat.Zatopiony)
                        Console.WriteLine("Zatopiles {0}!", Boat.Nazwa);
                    else
                        Console.WriteLine("Sukces!");

                    Console.Beep(1500, 1000);
                    return true;
                }
            }

            Console.WriteLine("Pudlo!");
            Console.Beep(500, 1000);
            Pole_.Ustaw(point, PunktyPola.Pudlo);
            return false;
        }
    }
}

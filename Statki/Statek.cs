using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Statki
{
    class Statek 
    {
      
        public virtual String Nazwa { get { return null; } }

        
        public virtual Int32 Trafienia { get { return 0; } }

       
        public Boolean Zatopiony { get { return Points.All((point) => point.Hit); } }

        
        public List<Trafienie> Points { get; set; }

        public Boolean PointsOverlap(Statek otherBoat)
        {
            return this.Points.Any((point) => otherBoat.Points.Any((otherPoint) => otherPoint.Equals(point)));
        }

       
        public Boolean JestSukces(Point point)
        {
            if (null == Points)
                throw new Exception("Boat is not setup.");

            Trafienie HitPoint = Points.FirstOrDefault((aPoint) => { return (aPoint.Lokalizacja.X == point.X && aPoint.Lokalizacja.Y == point.Y); });

            if (HitPoint != null)
            {
                if (HitPoint.Hit)
                    throw new Exception("Point was already hit.");

                HitPoint.Hit = true;
                return true;
            }

            return false;
        }
    }

}

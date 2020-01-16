using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Statki
{
    public class Trafienie
    {
        
        public Point Lokalizacja { get; set; }

        
        public Boolean Hit { get; set; }

        public override Boolean Equals(Object punkt)
        {
            if (null == punkt || !(punkt is Trafienie))
                return false;

            Trafienie innyp = punkt as Trafienie;
            return innyp.Lokalizacja.Equals(Lokalizacja);
        }

        public override Int32 GetHashCode()
        {
            return Lokalizacja.GetHashCode();
        }
    }
}

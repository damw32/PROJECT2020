using System;
using System.Collections.Generic;
using System.Text;

namespace Statki.Statki
{
    class Podwodny : Statek
    {
        public override String Nazwa { get { return "Statek podwodny"; } }
        public override Int32 Trafienia { get { return 3; } }
    }
}

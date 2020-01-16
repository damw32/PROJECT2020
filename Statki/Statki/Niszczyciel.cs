using System;
using System.Collections.Generic;
using System.Text;

namespace Statki.Statki
{
    class Niszczyciel : Statek
    {
        public override String Nazwa { get { return "Niszczyciel"; } }
        public override Int32 Trafienia { get { return 3; } }
    }
}

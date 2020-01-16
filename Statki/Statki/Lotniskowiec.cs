using System;
using System.Collections.Generic;
using System.Text;

namespace Statki.Statki
{
    class Lotniskowiec : Statek
    {
        public override String Nazwa { get { return "Lotniskowiec"; } }

        public override Int32 Trafienia { get { return 5; } }
    }
}

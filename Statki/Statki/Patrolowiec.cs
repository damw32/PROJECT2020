using System;
using System.Collections.Generic;
using System.Text;

namespace Statki.Statki
{
    class Patrolowiec : Statek
    {
        public override String Nazwa { get { return "Patrolowiec"; } }
        public override Int32 Trafienia { get { return 2; } }
    }
}

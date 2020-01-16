using System;
using System.Collections.Generic;
using System.Text;

namespace Statki.Statki
{
    class OkretWojenny : Statek
    {
        public override String Nazwa { get { return "Okret wojenny"; } }
        public override Int32 Trafienia { get { return 4; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag
{
    public abstract class Parser : Import
    {
        public abstract void Import(Data d);
    }
}

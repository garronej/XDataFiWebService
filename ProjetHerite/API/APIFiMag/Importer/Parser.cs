using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag
{   
    /// <summary>
    /// Classe abstraite, Parser servant dans le cas des imports depuis des formats spécifiques
    /// </summary>
    public abstract class Parser : Import
    {
        public abstract void Import(Data d);
    }
}

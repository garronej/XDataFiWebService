using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.Import
{
    /// <summary>
    /// Classe abstraite, Parser servant dans le cas des imports depuis des formats spécifiques
    /// </summary>
    public abstract class Parser : Import
    {
        public abstract void Import(Data.Data d);
    }
}

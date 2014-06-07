using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.Import
{
    /// <summary>
    /// Interface d'export des données
    /// Dépend du type de données à importer, et du type de fichier source
    /// </summary>
    public interface Import
    {
        void Import(Data.Data d);
    }
}

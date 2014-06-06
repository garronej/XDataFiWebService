using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag
{   
    /// <summary>
    /// Interface d'export des données
    /// Dépend du type de données à exporter, et du type de fichier cible
    /// </summary>
    public interface Export
    {
        void Export(Data d);
    }
}

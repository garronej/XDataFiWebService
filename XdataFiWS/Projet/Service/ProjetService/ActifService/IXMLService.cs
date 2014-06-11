using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IXML" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IXMLService
    {
        [OperationContract]
        WcfLibrary.Data.DataXML getXML(string s, string sSchema);
    }
}

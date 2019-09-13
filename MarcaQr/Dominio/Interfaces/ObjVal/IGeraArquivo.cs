using Dominio.ObjVal;
using System.Web;

namespace Dominio.Interfaces.ObjVal
{
    public interface IGeraArquivo
    {
        Arquivo GeraLink(HttpRequest httpRequest);
    }
}

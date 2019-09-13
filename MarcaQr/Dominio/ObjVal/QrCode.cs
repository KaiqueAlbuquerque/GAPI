using Dominio.Entidades;
using Dominio.Interfaces.ObjVal;
using Persits.PDF;
using System.Configuration;
using System.Web;

namespace Dominio.ObjVal
{
    public class QrCode : IGeraArquivo
    {
        public string NomeArquivo { get; set; }
        public string Link { get; set; }
        public int XFolhaEmPe { get; set; }
        public int YFolhaEmPe { get; set; }
        public int XFolhaDeitada { get; set; }
        public int YFolhaDeitada { get; set; }
        public int Tamanho { get; set; }

        public Arquivo GeraLink(HttpRequest httpRequest)
        {
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("/Arquivos/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    Log log = GeraQrCode(httpRequest, postedFile.FileName);

                    Arquivo arquivo = new Arquivo();

                    arquivo.LinkDownload = ConfigurationManager.AppSettings["linkDownload"] + "qrCode/" + postedFile.FileName;
                    arquivo.Log = log;

                    return arquivo;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        private Log GeraQrCode(HttpRequest httpRequest, string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            Link = httpRequest.Params["link"];
            XFolhaEmPe = int.Parse(httpRequest.Params["xfolhaEmPe"]);
            XFolhaDeitada = int.Parse(httpRequest.Params["xfolhaDeitada"]);
            YFolhaEmPe = int.Parse(httpRequest.Params["yfolhaEmPe"]);
            YFolhaDeitada = int.Parse(httpRequest.Params["yfolhaDeitada"]);
            Tamanho = int.Parse(httpRequest.Params["tamanho"]);

            int contadorDePaginas = 0;

            // Cria instancia de PDF manager.
            PdfManager objPDF = new PdfManager();

            // Chave licença
            objPDF.RegKey = ConfigurationManager.AppSettings["chavePdf"];

            //Abre PDF
            PdfDocument objDoc = objPDF.OpenDocument(HttpContext.Current.Server.MapPath("/Arquivos/" + nomeArquivo));

            foreach (PdfPage page in objDoc.Pages)
            {
                contadorDePaginas++;

                if (page.Rotate.Equals(0) || page.Rotate.Equals(180))
                {
                    page.Canvas.DrawBarcode2D(Link, "Type=3; X=" + XFolhaEmPe + "; Y=" + YFolhaEmPe + "; Version=" + Tamanho);
                }
                else
                {
                    page.Canvas.DrawBarcode2D(Link, "Type=3; X=" + XFolhaDeitada + "; Y=" + YFolhaDeitada + "; Version=" + Tamanho);
                }
            }

            objDoc.Save(HttpContext.Current.Server.MapPath("/Arquivos/qrCode/" + nomeArquivo));
            objDoc.Close();

            Log log = new Log();
            log.NomeArquivo = nomeArquivo;
            log.QuatindadePaginas = contadorDePaginas;
            log.TipoAlteracao = 2;

            return log;
        }
    }
}

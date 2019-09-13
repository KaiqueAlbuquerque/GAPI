using Dominio.Interfaces.ObjVal;
using System.Configuration;
using System.Web;
using Persits.PDF;
using Dominio.Entidades;

namespace Dominio.ObjVal
{
    public class MarcaDAgua : IGeraArquivo
    {
        public string NomeArquivo { get; set; }
        public double Transparencia { get; set; }
        public string Texto { get; set; }
        public int XFolhaEmPe { get; set; }
        public int YFolhaEmPe { get; set; }
        public int XFolhaDeitada { get; set; }
        public int YFolhaDeitada { get; set; }
        public int Angulo { get; set; }
        public int Tamanho { get; set; }
        public string Cor { get; set; }
        public string Fonte { get; set; }

        public Arquivo GeraLink(HttpRequest httpRequest)
        {
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("/Arquivos/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    Log log = GerarMarcadAgua(httpRequest, postedFile.FileName);

                    Arquivo arquivo = new Arquivo();

                    arquivo.LinkDownload = ConfigurationManager.AppSettings["linkDownload"] + "marcadAgua/" + postedFile.FileName;
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

        private Log GerarMarcadAgua(HttpRequest httpRequest, string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            Transparencia = double.Parse(httpRequest.Params["transparencia"]);
            Texto = httpRequest.Params["texto"];
            XFolhaEmPe = int.Parse(httpRequest.Params["xfolhaEmPe"]);
            XFolhaDeitada = int.Parse(httpRequest.Params["xfolhaDeitada"]);
            YFolhaEmPe = int.Parse(httpRequest.Params["yfolhaEmPe"]);
            YFolhaDeitada = int.Parse(httpRequest.Params["yfolhaDeitada"]);
            Angulo = int.Parse(httpRequest.Params["angulo"]);
            Tamanho = int.Parse(httpRequest.Params["tamanho"]);
            Cor = httpRequest.Params["cor"];
            Fonte = httpRequest.Params["fonte"];

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

                var GState = objDoc.CreateGState("BlendMode=1; Alpha=" + Transparencia.ToString().Replace(',', '.') + "; FillAlpha=" + Transparencia.ToString().Replace(',', '.'));
                page.Canvas.SetGState(GState);

                if (page.Rotate.Equals(0) || page.Rotate.Equals(180))
                {
                    page.Canvas.DrawText(Texto, "x=" + XFolhaEmPe + "; y=" + YFolhaEmPe + "; angle=" + Angulo + "; size=" + Tamanho + "; color=&" + Cor, objDoc.Fonts[Fonte]);
                }
                else
                {
                    page.Canvas.DrawText(Texto, "x=" + XFolhaDeitada + "; y=" + YFolhaDeitada + "; angle=" + Angulo + "; size=" + Tamanho + "; color=&" + Cor, objDoc.Fonts[Fonte]);
                }
            }

            objDoc.Save(HttpContext.Current.Server.MapPath("/Arquivos/marcadAgua/" + nomeArquivo));
            objDoc.Close();

            Log log = new Log();
            log.NomeArquivo = nomeArquivo;
            log.QuatindadePaginas = contadorDePaginas;
            log.TipoAlteracao = 1;

            return log;
        }
    }
}

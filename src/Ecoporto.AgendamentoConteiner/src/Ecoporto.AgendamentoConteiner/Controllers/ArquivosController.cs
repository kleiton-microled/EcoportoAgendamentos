using Ecoporto.AgendamentoConteiner.Dados.Interfaces;
using System;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoConteiner.Controllers
{
    public class ArquivosController : Controller
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IUploadRepositorio _uploadRepositorio;

        public ArquivosController(IAgendamentoRepositorio agendamentoRepositorio, IUploadRepositorio uploadRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _uploadRepositorio = uploadRepositorio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public FileResult Download(int id)
        {
            WsSharepoint.WsIccSharepoint ws = new WsSharepoint.WsIccSharepoint();

            try
            {
                var upload = _uploadRepositorio.ObterUploadPorId(id);

                if (upload != null)
                {
                    var arquivoSharepoint = ws.ObterImagemDocAverbacaoPorAutonum(id);

                    var contentType = MimeMapping.GetMimeMapping("a" + upload.Extensao);

                    return File(arquivoSharepoint, contentType, upload.Arquivo);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }
    }
}
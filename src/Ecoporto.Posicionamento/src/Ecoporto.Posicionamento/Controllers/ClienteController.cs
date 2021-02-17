using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Extensions;
using System.Web.Mvc;

namespace Ecoporto.Posicionamento.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IClientesRepositorio _clientesRepositorio;

        public ClienteController(IClientesRepositorio clientesRepositorio)
        {
            _clientesRepositorio = clientesRepositorio;
        }

        [HttpGet]
        public ActionResult ObterCliente(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return RetornarErro("Informe o CPF ou CNPJ do Cliente");

            var dados = _clientesRepositorio.ObterClientePorDocumento(documento.ApenasNumeros());

            if (dados == null)
                return RetornarErro($"Nenhum Cliente encontrado com o documento {documento}");

            return Json(dados, JsonRequestBehavior.AllowGet);
        }
    }
}
using Ecoporto.AgendamentoConteiner.Dados.Repositorios;
using Ecoporto.AgendamentoConteiner.Extensions;
using System;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoConteiner.Filtros
{
    public class ValidaTransportadoraAgendamentoFilter : ActionFilterAttribute
    {
        private readonly AgendamentoRepositorio _agendamentoRepositorio = new AgendamentoRepositorio();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var agendamentoId = context.Controller.ValueProvider.GetValue("id").AttemptedValue.ToInt();

            if (agendamentoId > 0)
            {
                var agendamentoBusca = _agendamentoRepositorio
                    .ObterAgendamentoPorId(agendamentoId);

                if (agendamentoBusca == null)
                    throw new Exception("Agendamento não encontrado ou excluído");

                if (context.HttpContext.User.ObterTransportadoraId() != agendamentoBusca.TransportadoraId)                
                    throw new Exception("Agendamento não pertence a Transportadora logada");                
            }            
        }
    }
}
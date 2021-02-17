using Ecoporto.AgendamentoDEPOT.Dados.Repositorios;
using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Extensions;
using System;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoDEPOT.Filtros
{
    public class ValidaTransportadoraAgendamentoFilter : ActionFilterAttribute
    {
        private readonly AgendamentoRepositorio _agendamentoRepositorio = new AgendamentoRepositorio();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var agendamentoId = context.Controller.ValueProvider.GetValue("id").AttemptedValue.ToInt();
            var tipoAgendamento = context.Controller.ValueProvider.GetValue("tipoAgendamento").AttemptedValue;
            
            if (agendamentoId > 0)
            {
                var tipoAgendamentoEnum = (TipoAgendamento)Enum.Parse(typeof(TipoAgendamento), tipoAgendamento, true);

                var agendamentoBusca = _agendamentoRepositorio
                    .ObterAgendamentoPorId(agendamentoId, tipoAgendamentoEnum);

                if (agendamentoBusca == null)
                    throw new Exception("Agendamento não encontrado ou excluído");

                if (context.HttpContext.User.ObterTransportadoraId() != agendamentoBusca.TransportadoraId)                
                    throw new Exception("Agendamento não pertence a Transportadora logada");                
            }            
        }
    }
}
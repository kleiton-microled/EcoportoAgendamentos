using Ecoporto.Posicionamento.Controllers;
using Ecoporto.Posicionamento.Dados.Repositorios;
using Ecoporto.Posicionamento.Extensions;
using Ecoporto.Posicionamento.Models;
using System;
using System.Web.Mvc;

namespace Ecoporto.Posicionamento.Filtros
{
    public class ValidaDespachanteAgendamentoFilter : ActionFilterAttribute
    {
        private readonly AgendamentoConteinerRepositorio _agendamentoConteinerRepositorio = new AgendamentoConteinerRepositorio();
        private readonly AgendamentoCargaSoltaRepositorio _agendamentoCargaSoltaRepositorio = new AgendamentoCargaSoltaRepositorio();
        private readonly AgendamentoVeiculosRepositorio _agendamentoVeiculosRepositorio = new AgendamentoVeiculosRepositorio();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerType.Name;
            var agendamentoId = context.Controller.ValueProvider.GetValue("id").AttemptedValue.ToInt();                   

            if (agendamentoId > 0)
            {
                Agendamento agendamentoBusca = new Agendamento();

                if (controllerName == nameof(AgendamentoCargaSoltaController))
                {
                    agendamentoBusca = _agendamentoCargaSoltaRepositorio
                        .ObterPosicionamentoPorId(agendamentoId);
                }
                else if (controllerName == nameof(AgendamentoConteinerController))
                {
                    agendamentoBusca = _agendamentoConteinerRepositorio
                        .ObterPosicionamentoPorId(agendamentoId);
                }
                else
                {
                    agendamentoBusca = _agendamentoVeiculosRepositorio
                        .ObterPosicionamentoPorId(agendamentoId);
                }

                if (agendamentoBusca == null)
                    throw new Exception("Agendamento não encontrado ou excluído");

                if (context.HttpContext.User.ObterDespachanteId() != agendamentoBusca.DespachanteId)
                    throw new Exception("Agendamento não pertence a Transportadora logada");
            }
        }
    }
}
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Dados.Repositorios;
using System;

using Unity;

namespace Ecoporto.Posicionamento
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAgendamentoConteinerRepositorio, AgendamentoConteinerRepositorio>();
            container.RegisterType<IAgendamentoCargaSoltaRepositorio, AgendamentoCargaSoltaRepositorio>();
            container.RegisterType<IAgendamentoVeiculosRepositorio, AgendamentoVeiculosRepositorio>();
            container.RegisterType<IEmpresaRepositorio, EmpresaRepositorio>();
            container.RegisterType<IMotivosRepositorio, MotivosRepositorio>();
            container.RegisterType<IPeriodosRepositorio, PeriodosRepositorio>();
            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            container.RegisterType<IViagensRepositorio, ViagensRepositorio>();
            container.RegisterType<IClientesRepositorio, ClientesRepositorio>();
        }
    }
}
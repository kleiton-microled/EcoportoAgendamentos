using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using Ecoporto.AgendamentoTRA.Dados.Repositorios;
using System;
using Unity;
using Unity.Injection;

namespace Ecoporto.AgendamentoTRA
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

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            container.RegisterType<ITransportadoraRepositorio, TransportadoraRepositorio>();
            container.RegisterType<IRecintoRepositorio, RecintoRepositorio>();

            container.RegisterType<IVinculoTRARepositorio, VinculoRepositorio>(new InjectionConstructor(TipoVinculo.TRA));
            container.RegisterType<IVinculoDEPOTRepositorio, VinculoRepositorio>(new InjectionConstructor(TipoVinculo.DEPOT));
        }
    }
}
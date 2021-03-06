using Ecoporto.AgendamentoConteiner.Dados.Interfaces;
using Ecoporto.AgendamentoConteiner.Dados.Repositorios;
using System;
using Unity;

namespace Ecoporto.AgendamentoConteiner
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
            container.RegisterType<IMotoristaRepositorio, MotoristaRepositorio>();
            container.RegisterType<IVeiculoRepositorio, VeiculoRepositorio>();
            container.RegisterType<IAgendamentoRepositorio, AgendamentoRepositorio>();
            container.RegisterType<IReservaRepositorio, ReservaRepositorio>();
            container.RegisterType<IEmpresaRepositorio, EmpresaRepositorio>();
            container.RegisterType<IUploadRepositorio, UploadRepositorio>();
        }
    }
}
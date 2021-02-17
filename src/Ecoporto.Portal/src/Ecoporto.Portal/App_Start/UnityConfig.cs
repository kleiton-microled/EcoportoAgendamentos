using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Dados.Repositorios;
using Ecoporto.Portal.Services;
using Ecoporto.Portal.Services.Interfaces;
using System;
using Unity;

namespace Ecoporto.Portal
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
            // Repositórios

            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            container.RegisterType<IMotoristaRepositorio, MotoristaRepositorio>();
            container.RegisterType<IVeiculoRepositorio, VeiculoRepositorio>();
            container.RegisterType<IEmpresaRepositorio, EmpresaRepositorio>();
            container.RegisterType<IMenuRepositorio, MenuRepositorio>();

            // Services
            container.RegisterType<INavegacaoService, NavegacaoService>();
            container.RegisterType<IMenuService, MenuService>();
        }
    }
}
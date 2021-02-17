using Dapper;
using Ecoporto.Portal.Config;
using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.Portal.Dados.Repositorios
{
    public class MenuRepositorio : IMenuRepositorio
    {
        public IEnumerable<Menu> ObterMenus()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<Menu>(@"SELECT Id, Descricao, Detalhes, URL, Icone, Ativo, Parametros, Cor FROM SGIPA.TB_PORTAL_MENUS WHERE Ativo = 1 ORDER BY Id");
            }
        }
        
        public Menu ObterMenuPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Menu>(@"SELECT Id, Descricao, Detalhes, URL, Icone, Ativo, Parametros, Cor FROM SGIPA.TB_PORTAL_MENUS WHERE Id = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<SubMenu> ObterSubMenus(int menuId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "MenuId", value: menuId, direction: ParameterDirection.Input);

                return con.Query<SubMenu>(@"SELECT Id, MenuId, Descricao, URL, Ativo, Parametros FROM SGIPA.TB_PORTAL_SUB_MENUS WHERE MenuId = :MenuId AND Ativo = 1 ORDER BY Id", parametros);
            }
        }

        public Menu ObterSubMenuPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Menu>(@"SELECT Id, MenuId, Descricao, URL, Ativo, Parametros FROM SGIPA.TB_PORTAL_SUB_MENUS WHERE Id = :Id", parametros).FirstOrDefault();
            }
        }
    }
}
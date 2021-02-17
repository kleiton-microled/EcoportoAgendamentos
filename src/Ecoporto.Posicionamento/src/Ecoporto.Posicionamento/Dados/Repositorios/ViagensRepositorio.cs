using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class ViagensRepositorio : IViagensRepositorio
    {
        public IEnumerable<Viagem> ObterViagensEmOperacao()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<Viagem>($@"
                    SELECT 
                        AUTONUMVIAGEM As Id,
                        NAVIO || ' ' || VIAGEM AS Descricao
                    FROM 
                        OPERADOR.VW_NAVIO_VIAGEM 
                    WHERE 
                        AUTONUMVIAGEM IN 
                            (
                                SELECT 
                                    DISTINCT 
                                        AUTONUM 
                                FROM 
                                    OPERADOR.TB_VIAGENS 
                                WHERE 
                                    OPERANDO = 1 
                                AND 
                                    NVL(FLAG_FEC_OP, 0) = 0 
                            )  
                    ORDER BY 
                        NAVIO,
                        VIAGEM ");
            }
        }      
    }
}
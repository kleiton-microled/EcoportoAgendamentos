using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class UploadRepositorio : IUploadRepositorio
    {        
        public bool DocumentoJaExistente(int agendamentoId, int tipoDocumentoUpload)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoDocumentoUpload", value: tipoDocumentoUpload, direction: ParameterDirection.Input);

                return con.Query<Upload>($@"
                    SELECT
                        AUTONUM As Id,
                        DECODE(AUTONUM_AGENDAMENTO_DOC, 1, 'Packing List', 2, 'Desenho Técnico', 3, 'Imagem') As TipoDocumento,
                        NOME_IMG As Arquivo,
                        DT_INCLUSAO As DataEnvio
                    FROM
                        SGIPA.TB_AV_IMAGEM
                    WHERE
                        TIPO_DOC_AGENDAMENTO = 'CSOP'
                    AND
                        AUTONUM_AGENDAMENTO = :AgendamentoId
                    AND
                        AUTONUM_AGENDAMENTO_DOC = :TipoDocumentoUpload", parametros).Any();
            }
        }

        public IEnumerable<Upload> ObterUploads(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                // LOTE As BookingCsItemId,
                // Perdão pela "gambiarra". O Web Service da Ecoporto permite a consulta do arquivo apenas pelo Lote ou Lote + Autonum da Imagem
                // Como não tem "Lote", estou usando a propriedade para armazenar o BookingCsItemId.

                return con.Query<Upload>($@"
                    SELECT
                        AUTONUM As Id,
                        AUTONUM_AGENDAMENTO AS AgendamentoId,                        
                        DECODE(AUTONUM_AGENDAMENTO_DOC, 1, 'Packing List', 2, 'Desenho Técnico', 3, 'Imagem') As TipoDocumento,
                        AUTONUM_AGENDAMENTO_DOC As TipoDocumentoId,
                        NOME_IMG As Arquivo,
                        LOTE As BookingCsItemId,
                        DT_INCLUSAO As DataEnvio,
                        1 As Sharepoint
                    FROM
                        SGIPA.TB_AV_IMAGEM
                    WHERE
                        TIPO_DOC_AGENDAMENTO = 'CSOP'
                    AND
                        AUTONUM_AGENDAMENTO = :AgendamentoId", parametros);
            }
        }

        public Upload ObterUploadPorId(int arquivoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "ArquivoId", value: arquivoId, direction: ParameterDirection.Input);

                return con.Query<Upload>($@"
                    SELECT
                        AUTONUM As Id,
                        DECODE(AUTONUM_AGENDAMENTO_DOC, 1, 'Packing List', 2, 'Desenho Técnico', 3, 'Imagem') As TipoDocumento,
                        NOME_IMG As Arquivo,
                        LOTE As BookingCsItemId,
                        DT_INCLUSAO As DataEnvio
                    FROM
                        SGIPA.TB_AV_IMAGEM
                    WHERE
                        TIPO_DOC_AGENDAMENTO = 'CSOP'
                    AND
                        AUTONUM = :ArquivoId", parametros).FirstOrDefault();
            }
        }

        public void ExcluirDocumentosAgendamento(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                con.Execute($@"DELETE FROM SGIPA.TB_AV_IMAGEM WHERE AUTONUM_AGENDAMENTO = :AgendamentoId AND TIPO_DOC_AGENDAMENTO = 'CSOP'", parametros);
            }
        }
    }
}
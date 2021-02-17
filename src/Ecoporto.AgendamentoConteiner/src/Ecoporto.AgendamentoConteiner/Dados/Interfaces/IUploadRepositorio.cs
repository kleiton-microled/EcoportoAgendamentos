using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Dados.Interfaces
{
    public interface IUploadRepositorio
    {
        bool DocumentoJaExistente(int agendamentoId, int tipoDocumentoUpload);
        IEnumerable<Upload> ObterUploads(int agendamentoId);
        Upload ObterUploadPorId(int arquivoId);
        void ExcluirDocumentosAgendamento(int agendamentoId);
        void ExcluirDocumentosPorId(int id);
    }
}

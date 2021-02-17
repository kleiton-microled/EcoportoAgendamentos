using Ecoporto.AgendamentoCS.Enums;
using Ecoporto.AgendamentoCS.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IUploadRepositorio
    {
        bool DocumentoJaExistente(int agendamentoId, int tipoDocumentoUpload);
        IEnumerable<Upload> ObterUploads(int agendamentoId);
        Upload ObterUploadPorId(int arquivoId);
        void ExcluirDocumentosAgendamento(int agendamentoId);
    }
}

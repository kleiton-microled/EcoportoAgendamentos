using Ecoporto.Posicionamento.Models;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IMotivosRepositorio
    {
        IEnumerable<Motivo> ObterMotivos();
        Motivo ObterMotivoPorId(int id);
        bool ExigeViagem(int motivoId);
    }
}

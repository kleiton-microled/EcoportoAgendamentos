using Ecoporto.Posicionamento.Models;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IViagensRepositorio
    {
        IEnumerable<Viagem> ObterViagensEmOperacao();
    }
}
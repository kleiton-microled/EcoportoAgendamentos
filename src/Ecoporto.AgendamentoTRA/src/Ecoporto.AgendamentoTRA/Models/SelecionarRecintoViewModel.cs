using System.Collections.Generic;

namespace Ecoporto.AgendamentoTRA.Models
{
    public class SelecionarRecintoViewModel
    {
        public int? RecintoSelecionadoId { get; set; }

        public List<Recinto> Recintos { get; set; } = new List<Recinto>();
    }
}
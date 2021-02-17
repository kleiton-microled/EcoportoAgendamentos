using Ecoporto.AgendamentoDEPOT.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public interface IVeiculoRepositorio
    {
        int Cadastrar(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Excluir(int id);
        IEnumerable<Veiculo> ObterVeiculos(int transportadoraId);
        IEnumerable<Veiculo> ObterVeiculosPorPlaca(string descricao, int transportadoraId);
        Veiculo ObterVeiculoPorId(int id);
        IEnumerable<TipoVeiculo> ObterTiposVeiculos();
        IEnumerable<Veiculo> ObterUltimos5VeiculosAgendados(int transportadoraId);
    }
}
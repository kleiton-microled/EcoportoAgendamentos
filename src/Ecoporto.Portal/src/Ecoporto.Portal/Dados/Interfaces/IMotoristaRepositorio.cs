using Ecoporto.Portal.Models;
using System.Collections.Generic;

namespace Ecoporto.Portal.Dados.Interfaces
{
    public interface IMotoristaRepositorio
    {
        int Cadastrar(Motorista motorista);
        void Atualizar(Motorista motorista);
        void Excluir(int id);
        IEnumerable<Motorista> ObterMotoristas(int transportadoraId);
        IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId);
        Motorista ObterMotoristaPorId(int id);
        Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId);
        Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId);
        IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId);
        Motorista ObterMotoristaChronosPorCNH(string cnh);
    }
}
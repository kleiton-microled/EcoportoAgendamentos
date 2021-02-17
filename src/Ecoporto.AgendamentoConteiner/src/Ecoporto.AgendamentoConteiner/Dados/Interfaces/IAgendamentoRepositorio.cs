using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Models;
using Ecoporto.AgendamentoConteiner.Models.DTO;
using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Dados.Interfaces
{
    public interface IAgendamentoRepositorio
    {
        int Cadastrar(Agendamento agendamento);
        void Atualizar(Agendamento agendamento);
        int CadastrarConteiner(Conteiner conteiner);
        int CadastrarConteinerVazio(ConteinerVazio conteiner);
        void AtualizarConteinerVazio(ConteinerVazio conteiner);
        void AtualizarConteiner(Conteiner conteiner);
        int CadastrarDanfe(NotaFiscal nf);
        void Excluir(int id);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int transportadoraId);
        IEnumerable<AgendamentoDTO> ObterAgendamentosPorPeriodoEVeiculo(int transportadoraId, int periodoId, int veiculoId);
        Agendamento ObterAgendamentoPorId(int id);
        AgendamentoDTO ObterDetalhesAgendamento(int id);
        IEnumerable<Janela> ObterPeriodos(DateTime inicio, DateTime fim, TipoAgendamentoConteiner ef);
        IEnumerable<Reserva> ObterItensAgendamento(int id);
        IEnumerable<Conteiner> ObterConteineresAgendamento(int id);
        Conteiner ObterConteinerAgendamentoPorId(int id);
        void AtualizarProtocoloImpresso(int id);
        Janela ObterPeriodoPorId(int id);
        IEnumerable<NotaFiscal> ObterNotasFiscaisPorDanfe(string danfe);
        IEnumerable<NotaFiscal> ObterNotasFiscaisPorItemAgendamento(int agendamentoId, int itemId);
        IEnumerable<CFOP> ObterCFOP();
        IEnumerable<NotaFiscal> ObterNotasFiscaisAgendamento(int agendamentoId);
        IEnumerable<Reserva> ObterReservasAgendamento(int id);
        IEnumerable<Reserva> ObterPeriodosReservas(string[] reservas);
        IEnumerable<TipoConteiner> ObterTiposConteiner();
        AgendamentoDTO ObterDadosProtocolo(int id);
        IEnumerable<DocumentoTransito> ObterTiposDocumentoTransito();
        Conteiner ObterConteinerPorId(int id);
        void ExcluirConteiner(int id);
        NotaFiscal ObterDanfePorId(int id);
        void ExcluirDanfe(int id);
        void AtualizarMotoristaAgendamento(int agendamentoId, int motoristaId);
        void AtualizarVeiculoAgendamento(int agendamentoId, int veiculoId);
        Reserva ObterConteinerPorISO(string iso);
        bool ExisteConteinerLateArrival(string sigla, string reserva, int viagem);
        bool ConteinerExigeNF(string reserva, int viagem);
        Conteiner ObterInformacoesConteinerReserva(string hash);
        void AtualizarFlagDueDesembaracada(int agendamentoId, bool check);
    }
}

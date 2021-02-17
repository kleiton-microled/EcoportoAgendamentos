using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Extensions;
using Ecoporto.AgendamentoConteiner.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecoporto.AgendamentoConteiner.Models
{
    public class Agendamento : Entidade<Agendamento>
    {
        public Agendamento()
        {

        }

        public Agendamento(
            int motoristaId,
            int veiculoId,
            string cte,
            bool cegonha,
            int transportadoraId,
            TipoOperacao tipoOperacao,
            int usuarioId)
        {
            MotoristaId = motoristaId;
            VeiculoId = veiculoId;
            CTE = cte;
            Cegonha = cegonha;
            TransportadoraId = transportadoraId;
            TipoOperacao = tipoOperacao;
            UsuarioId = usuarioId;
        }

        public Agendamento(
            int id,
            int motoristaId,
            int veiculoId,
            string cte,
            bool cegonha,
            int transportadoraId,
            int periodoId,
            string emailRegistro,
            TipoOperacao tipoOperacao,
            bool dueDesembaracada,
            int usuarioId)
        {
            Id = id;
            MotoristaId = motoristaId;
            VeiculoId = veiculoId;
            CTE = cte;
            Cegonha = cegonha;
            TransportadoraId = transportadoraId;
            PeriodoId = periodoId;
            EmailRegistro = emailRegistro;
            TipoOperacao = tipoOperacao;
            DueDesembaracada = dueDesembaracada;
            UsuarioId = usuarioId;
        }

        public int MotoristaId { get; set; }

        public int VeiculoId { get; set; }

        public int TransportadoraId { get; set; }

        public int PeriodoId { get; set; }

        public bool Cegonha { get; set; }

        public int UsuarioId { get; set; }

        public string Protocolo { get; set; }

        public bool Impresso { get; set; }

        public string CTE { get; set; }

        public string EmailRegistro { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public bool DueDesembaracada { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataEntrada { get; set; }

        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public List<Upload> Uploads { get; set; } = new List<Upload>();

        public List<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public void AdicionarConteineres(List<Conteiner> conteineres)
        {
            if (conteineres != null)
                Conteineres.AddRange(conteineres);
        }

        public void AdicionarUploads(List<Upload> uploads)
        {
            if (uploads != null)
                Uploads.AddRange(uploads);
        }

        public void AdicionarNotasFiscais(List<NotaFiscal> notasFiscais)
        {
            if (notasFiscais != null)
                NotasFiscais.AddRange(notasFiscais);
        }

        public void Alterar(Agendamento agendamento)
        {
            MotoristaId = agendamento.MotoristaId;
            VeiculoId = agendamento.VeiculoId;
            CTE = agendamento.CTE;
            PeriodoId = agendamento.PeriodoId;
            Cegonha = agendamento.Cegonha;
            EmailRegistro = agendamento.EmailRegistro;
        }

        public override void Validar()
        {
            RuleFor(c => c.MotoristaId)
                .GreaterThan(0)
                .WithMessage("Motorista não informado");

            RuleFor(c => c.VeiculoId)
                .GreaterThan(0)
                .WithMessage("Veículo não informado");

            RuleFor(c => c.CTE)
                .NotEmpty()
                .WithMessage("O campo CTE é obrigatório");

            RuleFor(c => c.TransportadoraId)
                .GreaterThan(0)
                .WithMessage("Transportadora não informada");

            RuleFor(c => c.UsuarioId)
                .GreaterThan(0)
                .WithMessage("Usuário não informado");

            RuleFor(c => c.PeriodoId)
                .GreaterThan(0)
                .When(c => c.Id > 0)
                .WithMessage("Período não informado");

            RuleFor(c => c.Conteineres)
                .NotEmpty()
                .When(c => c.Id > 0)
                .WithMessage("Nenhum Contêiner Adicionado");

            RuleFor(c => c.EmailRegistro)
                .Must(Validation.EmailValido)
                .When(c => c.Id > 0)
                .When(c => !string.IsNullOrEmpty(c.EmailRegistro))
                .WithMessage("Email inválido");

            foreach (var conteiner in Conteineres)
            {
                if (conteiner.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                {
                    var notas = NotasFiscais
                        .Where(c => c.SiglaConteiner == conteiner.Sigla).ToList();

                    if (notas.Count == 0)
                    {
                        if (conteiner.ExigeNF)
                        {
                            if (conteiner.Reserva.Bagagem == false)
                                AdicionarNotificacao($"Nenhuma DANFE informada para o Contêiner {conteiner.Sigla} / Reserva {conteiner.Reserva.Descricao}");
                        }
                    }
                    else
                        conteiner.NotasFiscais.AddRange(notas);

                    // Esta é uma funcionalidade que vai existir futuramente por decisão da área eles irão amadurecer a ideia para implementar isso no site para o cliente.

                    //if (conteiner.Bagagem)
                    //{
                    //    if (!Uploads.Any(c => c.TipoDocumento == TipoDocumentoUpload.AUTORIZACAO_BAGAGEM))
                    //        AdicionarNotificacao($"O Contêiner {conteiner.Sigla} exige upload de {TipoDocumentoUpload.AUTORIZACAO_BAGAGEM.ToName()}");

                    //    if (!Uploads.Any(c => c.TipoDocumento == TipoDocumentoUpload.PACKING_LIST))
                    //        AdicionarNotificacao($"O Contêiner {conteiner.Sigla} exige upload de {TipoDocumentoUpload.PACKING_LIST.ToName()}");
                    //}
                }
            }

            var notificacoes = ValidationResult.Errors;

            ValidationResult = Validate(this);

            AdicionarNotificacoes(notificacoes);
        }
    }
}
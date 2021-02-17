$(document).ready(function () {

    $('main')
        .addClass('container')
        .removeClass('container-fluid');

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {

        if (stepPosition === 'first') {
            $("#prev-btn").addClass('disabled');
        } else if (stepPosition === 'final') {
            $("#next-btn").addClass('disabled');
        } else {
            $("#prev-btn").removeClass('disabled');
            $("#next-btn").removeClass('disabled');
        }
    });

    $("#smartwizard").on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {

        var motoristaId = $('#MotoristaId').val();
        var veiculoId = $('#VeiculoId').val();
        var cte = $('#CTE').val();

        limpaErros();

        $('#btnConcluirAgendamento')
            .addClass('invisivel');        

        if (stepDirection === 'forward') {

            if (stepNumber == 0 && motoristaId === '') {

                mostrarErro('Selecione o Motorista');

                return false;
            }

            if (stepNumber == 1 && veiculoId === '') {

                mostrarErro('Selecione o Veículo');

                return false;
            }

            if (stepNumber == 1 && cte === '') {

                mostrarErro('Informe o número do CTE');

                return false;
            }   

            if (stepNumber == 2) {

                $('#btnConcluirAgendamento')
                    .removeClass('invisivel');
            }            
        }
    });

    $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        transitionEffect: 'fade',
        showStepURLhash: true,
        enableAllSteps: true,
        keyNavigation: false,
        toolbarSettings: {
            toolbarExtraButtons: [
                $('<button type="submit" id="btnConcluirAgendamento" onclick="Spinner(this)" class="ml-2 invisivel">Concluir</button>').text('Concluir')
                    .addClass('btn btn-info')
                    .on('click', function () {

                    })
            ],
            toolbarPosition: 'bottom',
            toolbarButtonPosition: 'end'
        },
        lang: {
            next: 'Próximo',
            previous: 'Anterior'
        }
    });

    if (parseInt($('#Id').val()) > 0) {

        $('#smartwizard > ul > li').addClass('done');

        $("#smartwizard > ul > li:first")
            .removeClass('done')
            .addClass('active');
    }

    corrigirSelects();
});

$("#btnPesquisarMotorista").click(function () {

    event.preventDefault();

    $('#pesquisa-modal-motoristas')
        .data('toggle', 'MotoristaId')
        .modal('show');
});

function selecionarMotorista(id, descricao) {

    var toggle = $('#pesquisa-modal-motoristas').data('toggle');

    $('#pesquisa-modal-motoristas').modal('hide');

    identificaMudancaMotorista($('#MotoristaId').val(), id, descricao);

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesMotorista($('#MotoristaId').val());

    $("#ListaMotoristas").empty();
}

function identificaMudancaMotorista(motoristaAnterior, novoMotorista, novoMotoristaDescricao) {

    if (parseInt(motoristaAnterior) !== parseInt(novoMotorista)) {

        $('#pnlAtualizarMotorista')
            .removeClass('invisivel');

        $('#pnlNovoMotorista')
            .html(novoMotoristaDescricao);

        var tipo = $('#TipoAgendamento').val();

        $('#lnkAtualizarMotorista')
            .attr('onclick', 'atualizarMotorista(' + $('#Id').val() + ',' + novoMotorista + ', "' + tipo + '")');
    }
}

function atualizarMotorista(agendamentoId, motoristaId, tipo) {

    var obj = {
        AgendamentoId: agendamentoId,
        MotoristaId: motoristaId,
        TipoAgendamento: tipo
    };

    $.post(urlBase + 'Agendamento/AtualizarMotorista', obj, function (resultado) {

        $('#pnlAtualizarMotorista')
            .addClass('invisivel');

        $('#pnlNovoMotorista')
            .html('');

        $('#lnkAtualizarMotorista')
            .attr('onclick', '');

        $('#pnlMotoristaAtualizado')
            .removeClass('invisivel');

    }).fail(function (data) {

        mostrarErrosResponse(data);
    });
}

function obterDetalhesMotorista(motoristaId) {

    $('#pnlDetalhesMotorista').html('');

    $.get(urlBase + 'Agendamento/ObterDetalhesMotorista?motoristaId=' + motoristaId, function (resultado) {

        if (resultado) {
            $('#pnlDetalhesMotorista').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#MotoristaId').change(function () {

    obterDetalhesMotorista($(this).val());
});

$("#btnPesquisarVeiculo").click(function () {

    event.preventDefault();

    $('#pesquisa-modal-veiculos')
        .data('toggle', 'VeiculoId')
        .modal('show');
});

function selecionarVeiculo(id, descricao) {

    var toggle = $('#pesquisa-modal-veiculos').data('toggle');

    $('#pesquisa-modal-veiculos').modal('hide');

    identificaMudancaVeiculo($('#VeiculoId').val(), id, descricao);

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesVeiculo($('#VeiculoId').val());

    $("#ListaVeiculos").empty();
}

function identificaMudancaVeiculo(veiculoAnterior, novoVeiculo, novoVeiculoDescricao) {

    if (parseInt(veiculoAnterior) !== parseInt(novoVeiculo)) {

        $('#pnlAtualizarVeiculo')
            .removeClass('invisivel');

        $('#pnlNovoVeiculo')
            .html(novoVeiculoDescricao);

        var tipo = $('#TipoAgendamento').val();

        $('#lnkAtualizarVeiculo')
            .attr('onclick', 'atualizarVeiculo(' + $('#Id').val() + ',' + novoVeiculo + ',"' + tipo +'")');
    }
}

function atualizarVeiculo(agendamentoId, veiculoId, tipo) {

    var obj = {
        AgendamentoId: agendamentoId,
        VeiculoId: veiculoId,
        TipoAgendamento: tipo
    };

    $.post(urlBase + 'Agendamento/AtualizarVeiculo', obj, function (resultado) {

        $('#pnlAtualizarVeiculo')
            .addClass('invisivel');

        $('#pnlNovoVeiculo')
            .html('');

        $('#lnkAtualizarVeiculo')
            .attr('onclick', '');

        $('#pnlVeiculoAtualizado')
            .removeClass('invisivel');

    }).fail(function (data) {

        mostrarErrosResponse(data);
    });
}

function obterDetalhesVeiculo(veiculoId) {

    $('#pnlDetalhesVeiculo').html('');

    $.get(urlBase + 'Agendamento/ObterDetalhesVeiculo?veiculoId=' + veiculoId, function (resultado) {

        if (resultado) {

            $('#pnlDetalhesVeiculo').html(resultado);
            $('#pnlCTE').removeClass('invisivel');

            if ($('#Carreta').val() === '' || $('#Carreta').val() === '___-____') {

                $('#lnkAtualizarPlacas').attr('href', urlBase + 'Veiculos/Atualizar/' + $('#VeiculoId').val());

                $("#smartwizard :input").prop("disabled", true);

                $('#modal-veiculo-invalido')
                    .modal('show');
            } else {
                $("#smartwizard :input:not(#Temp,#Ventilacao,#Umidade,#Escala,#Bagagem)").prop("disabled", false);
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#VeiculoId').change(function () {

    obterDetalhesVeiculo($(this).val());
});

function mostrarErro(mensagem) {

    $('#msgErro')
        .html(mensagem)
        .removeClass('invisivel');
}

function mostrarErrosResponse(data) {

    var retorno = data.responseJSON;

    if (retorno.erros != null) {

        $('#msgErro')
            .html('');

        if (retorno.erros.length === 1) {

            var erro = retorno.erros[0];

            $('#msgErro')
                .removeClass('invisivel')
                .html(erro.ErrorMessage);
        }

        if (retorno.erros.length > 1) {

            retorno.erros.map(function (erro) {
                $('#msgErro').append('<li>' + erro.ErrorMessage + '</li>');
            });
        }

        $('#msgErro')
            .removeClass('invisivel');
    }
}

function limpaErros() {

    $('#msgErro')
        .addClass('invisivel')
        .html('');
}

function obterPeriodos(tipoAgendamento) {

    $('#pnlPeriodos').html('');

    $.get(urlBase + 'Agendamento/ObterPeriodos?tipoAgendamento=' + tipoAgendamento, function (resultado) {

        if (resultado) {

            $('#pnlPeriodos').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function SelecionarPeriodo(id) {

    if (parseInt(id) > 0)
        $('#PeriodoId').val(id);
}

var agendamentoMensagemSucesso = function () {

    toastr.success('Agendamento cadastrado com sucesso!', 'Agendamento');

};

var agendamentoMensagemErro = function (xhr, status) {

    $('#btnConcluirAgendamento')
        .html('Concluir')
        .removeClass('disabled');

    mostrarErrosResponse(xhr);
};

function ValidaTipoAgendamento() {

    var tipoAgendamento = parseInt($('#TipoAgendamento:checked').val());

    if (tipoAgendamento === 1) {

        $('#pnlTipoConteiner').removeClass('invisivel');
        $('#pnlRecintoDEPOT').addClass('invisivel');
        $('#pnlRecintoTRA').removeClass('invisivel');

        $('#lblTituloAgendamento').text('TRA');
    }

    if (tipoAgendamento === 2) {

        $('#pnlTipoConteiner').addClass('invisivel');
        $('#pnlRecintoDEPOT').removeClass('invisivel');
        $('#pnlRecintoTRA').addClass('invisivel');

        $('#lblTituloAgendamento').text('DEPOT');
    }

    ExibirTotais();    
}

function ExibirTotais() {

    var tipoAgendamento = parseInt($('#TipoAgendamento:checked').val());
    var recinto = '';

    if (!isNumero(tipoAgendamento))
        return;

    if (tipoAgendamento === 1) {
        recinto = $('#RecintoSelecionadoTRA').val();
    }

    if (tipoAgendamento === 2) {
        recinto = $('#RecintoSelecionadoDEPOT').val();
    }

    $.get(urlBase + 'Agendamento/ObterTotais?recinto=' + recinto + '&tipoAgendamento=' + tipoAgendamento, function (resultado) {

        if (resultado) {
            
            $('#lblTotal').text(resultado.Total);
            $('#lblAgendados').text(resultado.TotalAgendado);
            $('#lblDisponiveis').text(resultado.Disponiveis);

            obterPeriodos(tipoAgendamento);
        }

    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$(window).resize(function () {

    corrigirSelects();
});

function corrigirSelects() {

    $('#RecintoSelecionadoId')
        .select2({ width: '100%' });    
}
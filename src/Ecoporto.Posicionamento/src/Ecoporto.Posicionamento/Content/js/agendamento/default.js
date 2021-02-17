$(document).ready(function () {

    $('main')
        .addClass('container')
        .removeClass('container-fluid');

    obterViagensEmOperacao();
});

$(window).resize(function () {

    corrigirSelects();
});

function corrigirSelects() {

    $('#MotivoId')
        .select2({ width: 'resolve' });

    $('#NovaViagemId')
        .select2({ width: 'resolve' });
}

function corrigirLayout() {

    $('#pnlMenuLateral')
        .removeClass('invisivel');

    $('#pnlFormAgendamento')
        .removeClass('col-xl-12')
        .addClass('col-xl-9');

    $('main')
        .addClass('container-fluid')
        .removeClass('container');
}

function ObterCliente() {

    var cpfCnpj = $('#CpfCnpj').val();

    limpaErros();

    $('#RazaoSocial')
        .val('Carregando...');

    $.get(urlBase + 'Cliente/ObterCliente?documento=' + cpfCnpj, function (resultado) {

        if (resultado) {

            $('#RazaoSocial').val(resultado.Razao);
        }
    }).fail(function (data) {

        mostrarErro(data);

        $('#RazaoSocial')
            .val('');
    });
}

$('#CpfCnpj').keyup(function () {

    var tamanho = $(this).val().length;

    if (tamanho === 11 || tamanho === 14 || tamanho === 18) {
        ObterCliente();
    } else {
        limpaErros();
        $('#RazaoSocial')
            .val('');
    }
});

function VerificaExigenciaViagem() {

    var motivoId = $('#MotivoId').val();

    $.get(urlBase + 'AgendamentoCargaSolta/ExigeViagem?motivoId=' + motivoId, function (resultado) {

        if (resultado.toLowerCase() === 'true') {

            $('#pnlMotivo').removeClass('col-md-9');
            $('#pnlMotivo').addClass('col-md-6');

            $('#pnlViagem').removeClass('invisivel');
        } else {

            $('#pnlMotivo').removeClass('col-md-6');
            $('#pnlMotivo').addClass('col-md-9');

            $('#pnlViagem').addClass('invisivel');
        }

        corrigirSelects();

    }).fail(function (data) {

        mostrarErro(data);
    });
}

function mostrarErro(data) {

    var response = data.responseJSON;
    var erro = response.erros[0];

    $('#msgErro')
        .removeClass('invisivel')
        .html(erro.ErrorMessage);
}

function limpaErros() {

    $('#msgErro')
        .addClass('invisivel')
        .html('');
}

$('#btnConcluir').click(function () {

    $($(this))
        .html('<i class="fa fa-spinner fa-spin"></i> aguarde...')
        .addClass('disabled');
});

function obterViagensEmOperacao() {

    $.get(urlBase + 'Viagem/ObterViagensEmOperacao', function (resultado) {

        if (resultado) {

            $('#NovaViagemId').append($('<option>', {
                value: '',
                text: ''
            }));

            resultado.forEach(function (item) {
                $('#NovaViagemId').append($('<option>', {
                    value: item.Id,
                    text: item.Descricao
                }));
            });

            corrigirSelects();
        }
    }).fail(function (data) {

        mostrarErrosResponse(data);
    });
}

function agendamentoMensagemSucesso() {

}

var agendamentoMensagemErro = function (xhr, status) {

    if (xhr !== null && xhr.responseText !== '') {

        toastr.error('Falha ao cadastrar o Agendamento. Verifique mensagens.', 'Agendamento');

        var msg = $('#msgErro');

        msg.html('');
        msg.removeClass('invisivel');

        var resultado = JSON.parse(xhr.responseText);

        var mensagens = resultado.erros.map(function (erro) {
            return '<li>' + erro.ErrorMessage + '</li>';
        });

        msg.html(mensagens);

    } else {
        toastr.error(xhr.statusText, 'Agendamento');
    }

    $('#btnConcluir')
        .html('Concluir')
        .removeClass('disabled');
};
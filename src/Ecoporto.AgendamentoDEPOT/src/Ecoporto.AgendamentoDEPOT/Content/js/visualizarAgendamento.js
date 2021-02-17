$(document).ready(function () {

    $('main')
        .addClass('container')
        .removeClass('container-fluid');

    $('input').prop('disabled', true);

    $('#tblConteineres .btn-excluir-conteiner').remove();
    $('#tblConteineres tbody td').removeClass('col-acoes-conteiner');  

    $('#TipoDocTransitoId')
        .empty()
        .append($('<option>', {
            value: '',
            text: ''
        }));

    $.get(urlBase + 'Agendamento/ObterDocsTransito', function (resultado) {

        resultado.forEach(function (item) {
            $('#TipoDocTransitoId').append($('<option>', {
                value: item.Id,
                text: item.Descricao
            }));
        });

    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
});

function selecionarDanfe(danfe, cfop) {

    event.preventDefault();

    $('#Danfe').val(danfe);
    $('#CFOP').val(cfop);
}

function vincularDanfes(bookingId, sigla, id) {

    event.preventDefault();

    limparCamposConteiner();

    $('#pnlDadosConteiner').hide('slow');

    $('#BookingId').val(bookingId);
    $('#Sigla').val(sigla);
    $('#UploadSiglaConteiner').val(sigla);

    $('#tblConteineres tr').removeClass("linha-selecionada");
    $('#linha-conteiner-' + id).addClass('linha-selecionada');

    obterDanfesPorConteiner(sigla);
    obterUploadsPorConteiner(sigla);

    $('#pnlDanfes').removeClass('invisivel');
    $('#pnlUploads').removeClass('invisivel');

    $('#Danfe').focus();
}

function visualizarConteiner(id) {

    event.preventDefault();

    $('#tblConteineres tr').removeClass("linha-selecionada");
    $('#linha-conteiner-' + id).addClass('linha-selecionada');

    $.get(urlBase + 'Agendamento/ObterDadosConteiner?&id=' + id, function (resultado) {

        if (resultado) {

            $('#BookingId').val(resultado.BookingId);
            $('#ConteinerId').val(resultado.Id);
            $('#Sigla').val(resultado.Sigla);
            $('#Volumes').val(resultado.Volumes);
            $('#Tamanho').val(resultado.Tamanho);
            $('#TipoBasico').val(resultado.TipoBasico);
            $('#Tara').val(resultado.Tara);
            $('#Bruto').val(resultado.Bruto);
            $('#ONU').val(resultado.ONU);
            $('#IMO').val(resultado.IMO);
            $('#Temp').val(resultado.Temp);
            $('#Escala').val(resultado.Escala);
            $('#Umidade').val(resultado.Umidade);
            $('#Ventilacao').val(resultado.Ventilacao);
            $('#Comprimento').val(resultado.Comprimento);
            $('#Altura').val(resultado.Altura);
            $('#LateralDireita').val(resultado.LateralDireita);
            $('#LateralEsquerda').val(resultado.LateralEsquerda);
            $('#LacreArmador1').val(resultado.LacreArmador1);
            $('#LacreArmador2').val(resultado.LacreArmador2);
            $('#OutrosLacres1').val(resultado.OutrosLacres1);
            $('#OutrosLacres2').val(resultado.OutrosLacres2);
            $('#OutrosLacres3').val(resultado.OutrosLacres3);
            $('#OutrosLacres4').val(resultado.OutrosLacres4);
            $('#LacreExportador').val(resultado.LacreExportador);
            $('#LacreSIF').val(resultado.LacreSIF);
            $('#Observacoes').val(resultado.Observacoes);
            $('#TipoDocTransitoId').val(resultado.TipoDocTransitoId);
            $('#NumDocTransitoDUE').val(resultado.NumDocTransitoDUE);
            $('#DataDocTransitoDUE').val(resultado.DataDocTransitoDUE);

            $('#IMO')
                .empty()
                .append($('<option>', {
                    value: resultado.IMO,
                    text: resultado.IMO
                }));

            $('#ONU')
                .empty()
                .append($('<option>', {
                    value: resultado.ONU,
                    text: resultado.ONU
                }));

            $('#pnlDadosConteiner').show('slow');
        }
    });
}

function limparCamposConteiner() {

    $('#ConteinerId').val('');
    $('#BookingId').val('');
    $('#Tamanho').val('');
    $('#Reserva').val('');
    $('#ItemReservaId').val('');
    $('#Sigla').val('');
    $('#Volumes').val('');
    $('#Tamanho').val('');
    $('#TipoBasico').val('');
    $('#Tara').val('');
    $('#Bruto').val('');
    $('#Temp').val('');
    $('#Escala').val('');
    $('#Umidade').val('');
    $('#Ventilacao').val('');
    $('#Comprimento').val('');
    $('#Altura').val('');
    $('#LateralDireita').val('');
    $('#LateralEsquerda').val('');
    $('#LacreArmador1').val('');
    $('#LacreArmador2').val('');
    $('#OutrosLacres1').val('');
    $('#OutrosLacres2').val('');
    $('#OutrosLacres3').val('');
    $('#OutrosLacres4').val('');
    $('#LacreExportador').val('');
    $('#LacreSIF').val('');
    $('#DAT').val('');
    $('#Observacoes').val('');
}

function obterDanfesPorConteiner(sigla) {

    $.get(urlBase + 'Agendamento/ObterDanfesPorConteiner?sigla=' + sigla, function (resultado) {

        if (resultado) {

            $('#pnlDanfes').html(resultado);
            $('#tblDanfes .btn-excluir-danfe').remove();
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function obterUploadsPorConteiner(sigla) {

    $.get(urlBase + 'Agendamento/ObterUploadsConteiner?sigla=' + sigla, function (resultado) {

        if (resultado) {

            $('#_VincularDocumentosConsulta').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}
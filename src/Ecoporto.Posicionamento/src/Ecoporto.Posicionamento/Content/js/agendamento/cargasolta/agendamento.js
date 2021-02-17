function PesquisarDetalhesCargaSolta() {

    var reserva = $('#Reserva').val();

    limpaErros();

    $('#pnlConteudo')
        .addClass('invisivel');

    $('#btnPesquisarItensCargaSolta')
        .html('<i class="fa fa-spinner fa-spin"></i> aguarde...')
        .addClass('disabled');

    $.get(urlBase + 'AgendamentoCargaSolta/ObterDetalhes?reserva=' + reserva, function (resultado) {

        if (resultado) {

            $('#pnlDetalhesReserva')
                .empty()
                .html(resultado);            

            $('#pnlConteudo')
                .removeClass('invisivel');            

            $('#Reserva')
                .removeClass('pulse');            

            corrigirLayout();
            corrigirSelects();
        }
    }).fail(function (data) {

        mostrarErro(data);
    }).always(function () {
        $('#btnPesquisarItensCargaSolta')
            .html('<i class="fa fa-search"></i> Pesquisar')
            .removeClass('disabled');
    });
}

function selecionarTodosItensCargaSolta() {

    event.preventDefault();
    
    $("input:checkbox").each(function () {
        this.checked = !this.checked;
    });
}

function habilitarCampoQuantidade(id) {

    if ($('#chkItemCs-' + id).is(':checked')) {

        $('#txtQuantidade-' + id)
            .prop('disabled', false);
    } else {

        $('#txtQuantidade-' + id)
            .val('')
            .prop('disabled', true);
    }    
}
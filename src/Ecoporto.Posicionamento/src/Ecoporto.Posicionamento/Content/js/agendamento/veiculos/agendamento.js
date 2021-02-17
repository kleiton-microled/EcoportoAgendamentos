function PesquisarDetalhesVeiculos() {

    var reserva = $('#Reserva').val();

    limpaErros();

    $('#pnlConteudo')
        .addClass('invisivel');

    $('#btnPesquisarVeiculos')
        .html('<i class="fa fa-spinner fa-spin"></i> aguarde...')
        .addClass('disabled');

    $.get(urlBase + 'AgendamentoVeiculos/ObterDetalhes?reserva=' + reserva, function (resultado) {

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
        $('#btnPesquisarVeiculos')
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
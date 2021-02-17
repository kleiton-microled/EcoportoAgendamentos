$(document).ready(function () {

    ocultarBoxLoading();

    configurarTextBox();

    exibirBoxLoading();

    exibirMensagemErro();

    exibirMensagemErroLogin();

    exibirModalPersonalizarColunas();

    personalizarBoxFiltro();

    personalizarRodape();

    personalizarTabelas();

    $(".calendario").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });

});

//
// Configurar validações de conteudo de textboxes
//
function configurarTextBox() {
    // Aceitar somente letras
   // $(".Alpha").alpha();

    // Aceitar somente letras e números
    //$(".AlphaNumeric").alphanumeric();

    // Mascara para CNPJ
    $(".Cnpj").mask("99.999.999/9999-99");

    // Mascara para Container
    $(".Container").mask("aaaa999999-9");

    // Mascara para CPF
    $(".Cpf").mask("999.999.999-99");

    // Formato de data
    //$(".Date").datepicker();
    $(".Date").mask("99/99/9999");

    $(".Placa").mask("SSS-9A99");

    $(".DateHHMM").mask("99/99/9999 99:99");

    // Aceitar somente números
    //$(".Numeric").numeric();
    //$(".Decimal").decimal();

    $(".BL").mask("9999999999/9");    

    $('.Telefone').focusout(function () {
        var phone, element;
        element = $(this);
        element.unmask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    }).trigger('focusout');

    //Formato de Mes/Ano referencia - Inicialmente para tela de indicadores
    $(".MesAnoRefer").mask("99/9999");

 //Mascara para Tipo de Documento DI - Averbação Online
    $(".DI").mask("9999/9999999-9");

    //Mascara para Tipo de Documento DSM - DSI Manual - Averbação Online
    $(".DSI").mask("99/999999-9");

    //Mascara para Tipo de Documento DTA - DTAS - Averbação Online
    $(".DTAeDTAS").mask("99999999/9999");
}

//
// Exibir a box carregando quando o usuário clicar em algum link de ação
//
function exibirBoxLoading() {
    $(".ActionButton").click(function () {
        if (window.Page_IsValid != undefined && !window.Page_IsValid) {
            window.Page_IsValid = true;
            return false;
        }

        var mask = $('#ModalPopUpMask');
        mask.css({ 'width': $(window).width(), 'height': $(document).height() });
        mask.fadeTo(100, 0.1);

        $("#BoxLoading").fadeIn(300);
    });
}

//
// Exibir mensagem de erro dinâmica
//
function exibirMensagemErro() {
    var msg = $('.MessageContainer');

    if (msg.children("p").length > 0) {
        window.scrollTo(0, 0);

        $(".ErrorMessageLogin").attr("src", "auto");
        msg.slideDown(600);

        msg.click(function () {
            window.clearTimeout(alerttimer);
            msg.slideUp(600);
        });

        var alerttimer = window.setTimeout(function () {
            msg.click();
        }, 5000);
    }
}

//
// Exibir mensagem de erro no login
//
function exibirMensagemErroLogin() {
    var msg = $('#pnlLoginError');

    window.scrollTo(0, 0);

    $(".ErrorMessageLogin").attr("src", "auto");
    msg.slideDown(600);

    msg.click(function () {
        window.clearTimeout(alerttimer);
        msg.slideUp(600);
    });

    var alerttimer = window.setTimeout(function () {
        msg.click();
    }, 5000);
}

//
// Exibir ModalPopUp de personalizar colunas
//
function exibirModalPersonalizarColunas() {
    if ($("#pnlPersonalizarColunas").length > 0) {
        showModalPopUp("#pnlPersonalizarColunas");
    }
}

//
// Ocultar box carregando
//
function ocultarBoxLoading() {
    $("#BoxLoading:visible").hide();
    $("#ModalPopUpMask:visible").hide();
}

//
// Exibir e ocultar o conteudo dos boxes
//
function personalizarBoxFiltro() {
    $("div.TitleFilter").toggle(
        function () {
            var btFilterClosed = $(this).children("img.BtFilterClosed");

            $(this).parent().children("div.FilterParameters").slideUp(400);
            $(btFilterClosed).attr("src", $(btFilterClosed).attr("src").replace("BtFilterClosed.png", "BtFilterOpen.png"));
        },
        function () {
            var btFilterClosed = $(this).children("img.BtFilterClosed");

            $(this).parent().children("div.FilterParameters").slideDown(400);
            $(btFilterClosed).attr("src", $(btFilterClosed).attr("src").replace("BtFilterOpen.png", "BtFilterClosed.png"));
        }
    );
}

//
// Personalizar o rodapé, mantendo sempre na parte inferior do viewport se o conteudo for muito pequeno
//
function personalizarRodape() {
    // Não funciona no IE7
    if ($('body').is('.lt-ie9 *')) {
        $(".ContentPlaceHolder").css("min-height", ($(window).height() - 198) + "px");// IE8, 7, 6 ...
    }
    
}

//
// Personalizar todas as tabelas
//
function personalizarTabelas() {
    $('table.DefaultGrid tr').click(function () {
        $(this).toggleClass('select');
    });
}

//
// Adicionar scroll vertical e horizontal no GridView com o header fixo
//
function scrollGridView() {
    var $divGrid = $(".ContentGridExtend");
    var $gridHeader = $("<table></table>");
    var $grid = $(".DefaultGrid");

    $divGrid.before("<div class='GridHeader'></div>");
    $divGrid.css("height", "400px");
    $divGrid.css("overflow", "scroll");

    var $divHeader = $("div.GridHeader");

    $divHeader.append($gridHeader);

    $gridHeader.append($(".DefaultGrid tr:nth(0)").clone());
    $(".DefaultGrid tr:nth(0)").remove();
    $gridHeader.css("width", $grid.css("width"));
    $gridHeader.addClass("DefaultGrid");

    $divGrid.scroll(function (e) {
        $divHeader.scrollLeft($(this).scrollLeft());
    });
}

//
// Exibir uma div como modal popup
//
function showModalPopUp(id) {
    var modal = $(id);
    var mask = $('#ModalPopUpMask');
    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    mask.css({ 'width': maskWidth, 'height': maskHeight });
    mask.show();
    mask.fadeTo(100, 0.8);

    var winH = $(window).height();
    var winW = $(window).width();

    modal.css('top', winH / 2 - $(id).height() / 2);
    modal.css('left', winW / 2 - $(id).width() / 2);

    modal.show();

    $('.ModalPopUp .CloseModalPopUp').click(function (e) {
        e.preventDefault();

        mask.hide();
        $('.ModalPopUp').hide();
    });
}

//
// File Upload
//
function checkFile(fup, txtArquivo) {
    $('#' + fup).change(function () {
        var file = $('#' + fup)[0].value;
        var ext = file;
        var valor = file;
        if (file != "" && file != null) {
            var aux = file.split("\\");
            if (aux.length > 0) {
                valor = aux[aux.length - 1];
                if (valor == null || valor == undefined) {
                    valor = file;
                }
            }
        }
        $('#' + txtArquivo).val(valor);
    });
    $('#' + txtArquivo).keydown(function () {
        return false;
    });
    $('#' + txtArquivo).keypress(function () {
        return false;
    });
}
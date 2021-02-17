var links = $('.navbar ul li a');

$.each(links, function (key, va) {
    if (va.href == document.URL) {
        $(this).addClass('active');
    }
});

$(".moeda").mask('#.##0,00', {
    reverse: true,
    allowNegative: false,
    thousands: '.',
    decimal: ',',
    affixesStay: false,
    clearIfNotMatch: true
});

$(".data").mask('00/00/0000');
$('.telefone').mask('(00) 0000-0000');
$('.celular').mask('(00) 00000-0000');
$('.cep').mask('00000-000');
$('.cpf').mask('000.000.000-00');
$('.cnpj').mask('00.000.000/0000-00');
$(".placa").mask("SSS-9A99");

$(".inteiro").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^\d].+/, ""));
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

var isNumero = function (numero) {
    return !isNaN(numero - parseFloat(numero));
}

var isInteiro = function (numero) {
    return /^\d+$/.test(numero);
}

var isMoeda = function (numero) {

    var valor = numero
        .replace(/\./g, '')
        .replace(',', '.');

    return !isNaN(valor - parseFloat(valor));
}

var formataMoedaCalculo = function (numero) {

    return numero
        .replace(/\./g, '')
        .replace(',', '.');
}

var formataMoedaPtBr = function (numero) {

    var numero = numero.toFixed(2).split('.');
    numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$("input[type=text]").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$('select').keyup(function (e) {

    if (e.keyCode == 46) {
        $(this).empty();
    }
});

$(document).on('keyup keypress', 'form input[type="text"]', function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

var Strings = {};

Strings.orEmpty = function (entity) {
    return entity || "";
};

String.prototype.padding = function (n, c) {
    var val = this.valueOf();
    if (Math.abs(n) <= val.length) {
        return val;
    }
    var m = Math.max((Math.abs(n) - this.length) || 0, 0);
    var pad = Array(m + 1).join(String(c || '\xa0').charAt(0));
    //      var pad = String(c || ' ').charAt(0).repeat(Math.abs(n) - this.length);
    return (n < 0) ? pad + val : val + pad;
    //      return (n < 0) ? val + pad : pad + val;
};

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}

function NavegarMenu(url) {

    var iframe = $('#frmConteudo', window.parent.document).modal('show');
    console.log(iframe);
    //var iframe = window.parent.document.getElementById('frmConteudo');
    //iframe.src = url;
}

function NavegarMenuExterno(menuId, subMenuId) {
    console.log('menu extenro');
    modalCarregando();

    var obj = {
        menuId: menuId,
        subMenuId: subMenuId
    };

    $.post(urlBase + 'Menu/Navegar', obj, function (resultado) {

        var iframe = window.parent.document.getElementById('frmConteudo');
        iframe.src = resultado;

    }).fail(function (data) {

        mostrarErrosResponse(data);

    }).always(function () {

        $('#btnAdicionarDanfe')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');
    });
}

function mostrarErrosResponse(data) {

    var retorno = data.responseJSON;

    if (retorno.erros != null) {

        if (retorno.erros.length === 1) {

            var erro = retorno.erros[0];

            toastr.error(erro.ErrorMessage, 'Agendamento');
        }

        if (retorno.erros.length > 1) {

            retorno.erros.map(function (erro) {
                toastr.error(erro.ErrorMessage, 'Agendamento');
            });
        }

        $(window).scrollTop(0);
    }
}

//function modalCarregando() {

//    //$('#pnlModalCarregando').modal('show');
//    var modal = window.parent.document.getElementById('pnlModalCarregando');
//    modal.style.display = 'block';
//}

//try {

//    var comecouCarregamento = function () {

//        var modal = window.parent.document.getElementById('pnlModalCarregando');
//        modal.style.display = 'none';
//        //$('#pnlModalCarregando').modal('hide');
//    };

//    var finalizouCarregamento = function (iframe) {
//        iframe.contentWindow.onunload = comecouCarregamento;
//    };

//} catch (e) {

//}  
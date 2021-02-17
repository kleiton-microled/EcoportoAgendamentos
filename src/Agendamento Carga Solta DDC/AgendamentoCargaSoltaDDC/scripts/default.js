function CorrigirRodape(){
    $(".ContentPlaceHolder").css("height", ($(window).height() - 176) + "px");
}

function AutoLoad(){
    $(document).ready(function () {
        CorrigirRodape();
        GridScroll();
        ConfigurarTextBox();
        DataHora();
    });
}

function GridScroll() {
    if (!jQuery.browser.msie || (jQuery.browser.msie && jQuery.browser.version > 7)) {
        $("#Grid-Scroll").css("height", ($(window).height() - 248) + "px");
    }
}

$(function () {
    $(".Calendario").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });
    $(".Calendario").attr('readOnly', 'true');
});

function ConfigurarTextBox() {

    // Aceitar somente letras
    $(".Alpha").alpha();

    // Aceitar somente letras e números
    $(".AlphaNumeric").alphanumeric();

    // Mascara para CNPJ
    $(".CNPJ").mask("99.999.999/9999-99");

    // Mascara para Container
    $(".Container").mask("aaaa999999-9");
       
    //Placa
    $(".Placa").mask("SSS-9A99");

    // Mascara para CPF
    $(".CPF").mask("999.999.999-99");

    $(".DateHHMM").mask("99/99/9999 99:99");

    // Aceitar somente números
    $(".Numeric").numeric();
    $(".Decimal").decimal();

    // Mascara para Telefone
    $(".Telefone").mask("(99) 9999-9999");

    $(".Moeda").maskMoney({ decimal: ",", thousands: "." });

}

$(document).ready(function () {
    $('input:text').focus(
    function () {
        $(this).css({ 'background-color': '#FFFFEE' });
    });

    $('input:text').blur(
    function () {
        $(this).css({ 'background-color': '#FFFFFF' });
    });
});

function DataHora() {

    var dayarray = new Array("Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado")
    var montharray = new Array("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12")

    var mydate = new Date()
    var year = mydate.getYear()
    if (year < 1900)
        year += 1900
    var day = mydate.getDay()
    var month = mydate.getMonth()
    var daym = mydate.getDate()


    if (daym < 10)
        daym = "0" + daym
    var hours = mydate.getHours()
    var minutes = mydate.getMinutes()
    var seconds = mydate.getSeconds()
    var dn = "AM"
    if (hours >= 24)
        dn = "PM"
    if (hours > 24) {
        hours = hours - 24
    }
    if (hours == 0)
        hours = 24
    if (minutes <= 9)
        minutes = "0" + minutes
    if (seconds <= 9)
        seconds = "0" + seconds
    //change font size here
    var cdate = "<br />" + daym + "/" + montharray[month] + "/" + year + " " + hours + ":" + minutes;
    if (document.all)
        document.all.lblData.innerHTML = cdate
    else if (document.getElementById)
        document.getElementById("lblData").innerHTML = cdate
    else
        document.write(cdate)
}
if (!document.all && !document.getElementById)
    getthedate()

function Relogio() {
    if (document.all || document.getElementById)
        setInterval("getthedate()", 1000)

} 
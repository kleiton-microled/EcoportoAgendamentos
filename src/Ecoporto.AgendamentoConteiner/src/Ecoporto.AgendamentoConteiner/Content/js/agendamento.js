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

            if (stepNumber == 1 && cte !== '') {

                if (parseInt($('#Id').val()) === 0) {

                    $('.btn.btn-secondary.sw-btn-next')
                        .html('<i class="fa fa-spinner fa-spin"></i>&nbsp;Aguarde...')
                        .addClass('disabled');

                    $("#frmCadastroAgendamento").submit();

                    return false;
                }
            }

            if (stepNumber == 2) {

                validarConteineresNotasFiscais(parseInt($('#Id').val()));

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

        if (parseInt($('#PeriodoId').val()) > 0) {

            $('#smartwizard > ul > li').addClass('done');

            $("#smartwizard > ul > li:first")
                .removeClass('done')
                .addClass('active');
        }
    }

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

        $('#lnkAtualizarMotorista')
            .attr('onclick', 'atualizarMotorista(' + $('#Id').val() + ',' + novoMotorista + ')');
    }
}

function atualizarMotorista(agendamentoId, motoristaId) {

    var obj = {
        AgendamentoId: agendamentoId,
        MotoristaId: motoristaId
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

        $('#lnkAtualizarVeiculo')
            .attr('onclick', 'atualizarVeiculo(' + $('#Id').val() + ',' + novoVeiculo + ')');
    }
}

function atualizarVeiculo(agendamentoId, veiculoId) {

    var obj = {
        AgendamentoId: agendamentoId,
        VeiculoId: veiculoId
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

$('#Reserva').on('keydown', function (e) {

    if (e.which == 9 || e.which == 13) {
        consultarReserva($(this).val());
    }
});

function consultarReserva(reserva) {

    $('#validaReserva')
        .html('')
        .addClass('invisivel');

    limpar();
    limpaErros();

    $('#pnlItensReserva').html('');

    if (reserva === '') {

        $('#validaReserva').removeClass('invisivel');

        return;
    }

    $.get(urlBase + 'Agendamento/ObterDetalhesReserva?reserva=' + reserva, function (resultado) {

        if (resultado) {

            $('#BookingId').val(resultado.BookingId);
            $('#NavioViagem').val(resultado.NavioViagem);
            $('#ViagemId').val(resultado.ViagemId);
            $('#Abertura').val(resultado.Abertura);
            $('#Fechamento').val(resultado.Fechamento);
            $('#Exportador').val(resultado.Exportador);

            $('#validaReserva').addClass('invisivel');

            obterItensReserva(reserva.trim(), resultado.ViagemId);

            desabilitarCamposDanfe();

            $('#pnlReserva').removeClass('invisivel');

            $('#Reserva').removeClass('pulse');

            validarAgendamentoSemSaldo(reserva.trim(), resultado.ViagemId, $('#Id').val());

            if (parseInt(resultado.EF) === 1) {

                $('#pnlDadosConteiner, #pnlDadosDanfes, #pnlDanfes, #pnlUploads')
                    .addClass('invisivel');
            }

            //$('#IMO')
            //    .empty()
            //    .append($('<option>', {
            //        value: '',
            //        text: ''
            //    }));

            //resultado.IMOS.forEach(function (item) {

            //    $('#IMO').append($('<option>', {
            //        value: item,
            //        text: item
            //    }));
            //});

            $('#IMO1').val(resultado.IMO1);
            $('#IMO2').val(resultado.IMO1);
            $('#IMO3').val(resultado.IMO1);
            $('#IMO4').val(resultado.IMO1);

            //$('#ONU')
            //    .empty()
            //    .append($('<option>', {
            //        value: '',
            //        text: ''
            //    }));

            //resultado.ONUS.forEach(function (item) {
            //    $('#ONU').append($('<option>', {
            //        value: item,
            //        text: item
            //    }));
            //});

            $('#ONU1').val(resultado.ONU1);
            $('#ONU2').val(resultado.ONU2);
            $('#ONU3').val(resultado.ONU3);
            $('#ONU4').val(resultado.ONU4);

            obterPeriodos();
        }
    }).fail(function (data) {

        mostrarErrosResponse(data);

        limpar();
    });

    $('#btnConcluir')
        .prop('disabled', false);
}

function atualizarSaldoBooking(bookingId, agendamentoId) {

    $.get(urlBase + 'Agendamento/ObterSaldoItemBooking?bookingId=' + bookingId + '&agendamentoId=' + agendamentoId, function (resultado) {

        if (resultado) {

            var saldo = parseInt(resultado);

            $('#txtSaldoBooking_' + bookingId).html(saldo);

            if (saldo === 0)
                $('#btnCadastrarConteineres_' + bookingId).addClass('disabled');
            else
                $('#btnCadastrarConteineres_' + bookingId).removeClass('disabled');
        }
    }).fail(function (data) {

        mostrarErrosResponse(data);

        limpar();
    });
}

function validarConteineresNotasFiscais(agendamentoId) {

    $.get(urlBase + 'Agendamento/ValidarConteineresNotasFiscais?agendamentoId=' + agendamentoId)
        .done(function () {
            return true;
        })
        .fail(function (data) {

            mostrarErrosResponse(data);

            return false;
        });
}

function validarAgendamentoSemSaldo(reserva, viagemId, agendamentoId) {

    var obj = {
        Reserva: reserva,
        ViagemId: viagemId,
        AgendamentoId: agendamentoId
    };

    $.get(urlBase + 'Agendamento/ObterSaldoDaReserva?' + $.param(obj), function (resultado) {

        if (resultado != null) {

            if (parseInt(resultado) <= 0) {

                $('#Reserva').val('');

                $('#lnkIrParaAgendamentos').attr('href', urlBase + 'Agendamento/Cadastrar');

                $('#modal-reserva-sem-saldo')
                    .modal({ backdrop: 'static', keyboard: false });

                $("input, button").prop("disabled", true);
                $("a:not(#lnkIrParaAgendamentos)").addClass("disabled");
            }
        }
    }).fail(function (data) {

        mostrarErrosResponse(data);

        limpar();
    });
}

function mostrarErro(mensagem) {

    $('#msgErro')
        .html(mensagem)
        .removeClass('invisivel');

    $(window).scrollTop(0);
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

        $(window).scrollTop(0);
    }
}

function limpaErros() {

    $('#msgErro')
        .addClass('invisivel')
        .html('');
}

function limpar() {

    limparCamposDanfe();
    limparCamposItem();

    $('#pnlDadosConteiner').hide();
}

function limparCamposDanfe() {

    $('#Danfe').val('');
    $('#CFOP').val('');
}

function limparCamposItem() {

    $('#QuantidadeItemReserva').val('');
    $('#Chassis').val('');
}

function obterItensReserva(reserva, viagemId) {

    $('#ItemReservaId').empty();

    $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + reserva + '&viagemId=' + viagemId, function (resultado) {

        if (resultado) {

            $('#tblItensReserva').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#ItemReservaId').change(function () {

    limparCamposItem();
});

function validarQuantidade(id, target) {

    $('#pnlAlertaQuantidade').addClass('invisivel');

    if (parseInt(id) > 0) {

        if (target.value != null) {

            var quantidadeReserva = parseInt($('#quantidade-reserva-' + id).text());
            var quantidadeInformada = parseInt(target.value);

            if (quantidadeInformada > 0) {
                if (quantidadeInformada > quantidadeReserva) {

                    $('#pnlAlertaQuantidade').removeClass('invisivel');
                    target.focus();
                }
            }
        }
    }
}

function obterPeriodos() {

    $('#pnlPeriodos').html('');

    $.get(urlBase + 'Agendamento/ObterPeriodos?reserva=' + $('#Reserva').val(), function (resultado) {

        if (resultado) {

            $('#pnlPeriodos').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function validarDanfe(campo) {

    $('#pnlAlertaDanfe')
        .text('').hide();

    var texto = campo;
    var retorno = '';

    if (texto.length !== 44) {
        retorno = 'A chave da Danfe informada é inválida';
    }

    if (texto.length === 44) {

        if (!validarCNPJ(texto.substring(6, 20))) {
            retorno = 'DANFE inválida';
        }

        if (texto.substring(20, 22) !== '55') {
            retorno = 'DANFE inválida';
        }

        if (texto.substring(25, 34) === '000000000') {
            retorno = 'DANFE inválida';
        }
    }

    return retorno;
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

function selecionarItemReserva(bookingId, tamanho, bagagem, hash) {

    event.preventDefault();

    limparCamposConteiner();
    limparCamposDanfe();
    limpaErros();

    $("#tblDanfes > tbody").html("");

    $('#BookingId').val(bookingId);
    $('#Tamanho').val(tamanho);

    $('#pnlDadosConteiner').show('slow');

    $('#tblItensReserva tr').removeClass("linha-selecionada");
    $('#linha-item-reserva-' + hash).addClass('linha-selecionada');

    if (toBoolean(bagagem)) {
        $('#Bagagem').val('Sim');
    }
    else {
        $('#Bagagem').val('Não');
    }

    $('#pnlDadosDanfes').addClass('invisivel');
    $('#pnlUploads').addClass('invisivel');

    //$.get(urlBase + 'Agendamento/ObterDadosConteinerReserva?hash=' + hash, function (resultado) {

    //    if (resultado) {

    //        $('#Comprimento').val(resultado.Comprimento);
    //        $('#Altura').val(resultado.Altura);
    //        $('#LateralDireita').val(resultado.LateralDireita);
    //        $('#LateralEsquerda').val(resultado.LateralEsquerda);
    //    }
    //}).fail(function (data) {
    //    toastr.error(data.statusText, 'Agendamento');
    //});

    $('#Sigla').focus();
}

function obterDanfesPorConteiner(sigla, agendamentoId) {

    $.get(urlBase + 'Agendamento/ObterDanfesPorConteiner?sigla=' + sigla + '&agendamentoId=' + agendamentoId, function (resultado) {

        if (resultado) {

            $('#pnlDanfes').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function habilitarCamposDanfe() {

    $('#Danfe').prop('disabled', false);
    $('#CFOP').prop('disabled', false);
    $('#btnAdicionarDanfe').removeClass('disabled');
}

function desabilitarCamposDanfe() {

    $('#Danfe').prop('disabled', true);
    $('#CFOP').prop('disabled', true);
    $('#btnAdicionarDanfe').addClass('disabled');
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

$('#btnAdicionarDanfe').click(function () {
    alert("teste");
    event.preventDefault();

    limpaErros();

    if ($('#ItemReservaId').val() === '') {

        mostrarErro('Selecione um item');

        return;
    }

    var retorno = validarDanfe($('#Danfe').val());

    if (retorno !== '') {

        mostrarErro(retorno);

        $('#Danfe').focus();

        return;
    }

    $('#btnAdicionarDanfe')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        AgendamentoId: $('#Id').val(),
        ConteinerId: $('#ConteinerId').val(),
        Danfe: $('#Danfe').val(),
        Reserva: $('#Reserva').val(),
        CFOP: $('#CFOP').val(),
        BookingId: $('#BookingId').val(),
        SiglaConteiner: $('#Sigla').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarDanfes', obj, function (resultado) {

        $('#pnlDanfes').html(resultado);

        limparCamposDanfe();

    }).fail(function (data) {

        mostrarErrosResponse(data);

    }).always(function () {

        $('#btnAdicionarDanfe')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');
    });
});
function loadFile() {
    var input, file, fr;

    if (typeof window.FileReader !== 'function') {
        bodyAppend("p", "The file API isn't supported on this browser yet.");
        return;
    }

    input = document.getElementById('fileinput');
    if (!input) {
        bodyAppend("p", "Um, couldn't find the fileinput element.");
    }
    else if (!input.files) {
        bodyAppend("p", "This browser doesn't seem to support the `files` property of file inputs.");
    }
    else if (!input.files[0]) {
        bodyAppend("p", "Please select a file before clicking 'Load'");
    }
    else {
        file = input.files[0];
        fr = new FileReader();
        fr.onload = receivedText;
        fr.readAsText(file);
    }

    function receivedText() {
        //console.log(fr.result);
        var doc = parser.parseFromString(fr.result, "text/xml");
        document.getElementById("xml").innerHTML = fr.result;
        
        shownode(doc.childNodes[0]);
    }
}

function shownode(node) {
    var chidren = node.childNodes[1];
    console.log(chidren.childNodes[0]);
    console.log(chidren.childNodes[0].childNodes[2]);
    var nfe = chidren.childNodes[0].childNodes[2].innerHTML;
    document.getElementById("Danfe").innerText = nfe;
    console.log("nfe:" + nfe)
    console.log(typeof chidren);
    
    
    //
    //let list = node.childNodes[0].nodeName;

    //document.getElementById("danfe").innerHTML = node.childNodes[4];
    //for (var i = 0; i < node.childNodes.length; i++)
    //    shownode(node.childNodes[i]);
}

//function showResult(fr, label) {
//    var markup, result, n, aByte, byteStr;

//    markup = [];
//    result = fr.result;
//    for (n = 0; n < result.length; ++n) {
//        aByte = result.charCodeAt(n);
//        byteStr = aByte.toString(16);
//        if (byteStr.length < 2) {
//            byteStr = "0" + byteStr;
//        }
//        markup.push(byteStr);
//    }
//    bodyAppend("p", label + " (" + result.length + "):");
//    bodyAppend("pre", markup.join(" "));
//}

//function bodyAppend(tagName, innerHTML) {
//    var elm;

//    elm = document.createElement(tagName);
//    elm.innerHTML = innerHTML;
//    document.body.appendChild(elm);
//}


if (typeof (DOMParser) == 'undefined') {
    DOMParser = function () { }
    DOMParser.prototype.parseFromString = function (str, contentType) {
        if (typeof (ActiveXObject) != 'undefined') {
            var xmldata = new ActiveXObject('MSXML.DomDocument');
            xmldata.async = false;
            xmldata.loadXML(str);
            return xmldata;
        } else if (typeof (XMLHttpRequest) != 'undefined') {
            var xmldata = new XMLHttpRequest;
            if (!contentType) {
                contentType = 'application/xml';
            }
            xmldata.open('GET', 'data:' + contentType + ';charset=utf-8,' + encodeURIComponent(str), false);
            if (xmldata.overrideMimeType) {
                xmldata.overrideMimeType(contentType);
            }
            xmldata.send(null);
            return xmldata.responseXML;
        }
    }
}

var xmlString = "<root><thing attr='val'/></root>";
var parser = new DOMParser();
var doc = parser.parseFromString(xmlString, "text/xml");
//console.log(doc.childNodes.length);
//console.log(doc.nodeName["serie"]);
//upload danfe
//$('#btnUploadDanfe').on("click", function () {
//    alert("ok");
//    $("input").trigger("click")
//    var selectedFile = document.getElementById('input').files[0];
//    console.log(selectedFile);
//    //var has_selected_file = $('#upld').filter(function () {
//    //    return $.trim(this.value) != ''
//    //}).length > 0;
//    //if (has_selected_file) {
//    //    $('#frm').submit();
//    //}
//    //else {
//    //    alert('Selecione um arquivo!');
//    //}
//});

function selecionarDanfe(danfe, cfop) {

    event.preventDefault();

    limpaErros();

    $('#Danfe').val(danfe);
    $('#CFOP').val(cfop);
}

function excluirDanfe(id, sigla) {

    $('#modal-mensagem-danfe').text('Confirma a exclusão da Danfe?');

    $('#del-modal-danfe')
        .data('id', id);

    $('#del-modal-danfe')
        .data('sigla', sigla);

    $('#del-modal-danfe')
        .modal('show');
}

function confirmarExclusaoDanfe(id) {

    var _url = urlBase + 'Agendamento/ExcluirDanfe';

    var _id = $('#del-modal-danfe')
        .data('id');

    var _sigla = $('#del-modal-danfe')
        .data('sigla');

    var _agendamentoId = $('#Id').val();

    $.post(_url, { id: _id, sigla: _sigla, agendamentoId: _agendamentoId }, function (resultado) {

        $('#pnlDanfes').html(resultado);

        toastr.success('Nota Fiscal excluída com sucesso!', 'Agendamento');

    }).fail(function (response) {

        mostrarErrosResponse(response);
    }).always(function () {

        $('#del-modal-danfe')
            .data('id', '0')
            .modal('hide');
    });
}

function excluirUpload(id) {

    $('#modal-mensagem-upload').text('Confirma a exclusão do Upload?');

    $('#del-modal-upload')
        .data('id', id);

    $('#del-modal-upload')
        .modal('show');
}

function confirmarExclusaoUpload(id) {

    event.preventDefault();

    var _url = urlBase + 'Agendamento/ExcluirUpload';

    var _id = $('#del-modal-upload')
        .data('id');

    var _agendamentoId = $('#Id').val();

    $.post(_url, { id: _id, agendamentoId: _agendamentoId }, function (resultado) {

        toastr.success('Documento excluído com sucesso!', 'Agendamento');

        $('#tblDocumentos').html(resultado);

    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir o Upload', 'Agendamento');
        }
    }).always(function () {

        $('#del-modal-upload')
            .data('id', '0')
            .modal('hide');
    });
}

$('#btnAdicionarUpload').click(function (e) {

    limpaErros();

    var form = $('#frmCadastrarDocumentos');

    var formData = new FormData();
    var totalFiles = document.getElementById("Upload").files.length;

    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("Upload").files[i];
        formData.append("Upload", file);
        formData.append("TiposDocumentos", $('#TiposDocumentos').val());
        formData.append("BookingId", $('#BookingId').val());
        formData.append("AgendamentoId", $('#Id').val());
        formData.append("UploadSiglaConteiner", $('#Sigla').val());
    }

    e.preventDefault();
    e.stopImmediatePropagation();

    $('#btnAdicionarUpload')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var xhr = new XMLHttpRequest();
    xhr.open('POST', urlBase + 'Agendamento/CadastrarDocumento');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {

                $('#tblDocumentos').html(xhr.responseText);

                $("#Upload").val('');

                $('#btnAdicionarUpload')
                    .html('<i class="fas fa-plus"></i>')
                    .removeClass('disabled');

            } else if (xhr.status == 400) {

                var retorno = JSON.parse(xhr.response);

                if (retorno != null) {
                    if (retorno.erros != null) {

                        var msg = retorno.erros[0].ErrorMessage;

                        $('#msgErro').html(msg).removeClass('invisivel');

                    }
                } else {
                    if (xhr.statusText) {
                        toastr.error(data.statusText, 'Agendamento');
                    } else {
                        toastr.error('Falha ao incluir a Danfe', 'Agendamento');
                    }
                }

                $('#btnAdicionarUpload')
                    .html('<i class="fas fa-plus"></i>')
                    .removeClass('disabled');
            }
        }
    };
    xhr.send(formData);
});

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
    $('#PesoLiquido').val('');
    $('#Bruto').val('');
    $('#Temp').val('');
    $('#ISO').val('');
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
    $('#Observacoes').val('');
    $('#QuantidadeVazios').val('');
    $('#IMO1').val('');
    $('#IMO2').val('');
    $('#IMO3').val('');
    $('#IMO4').val('');
    $('#ONU1').val('');
    $('#ONU2').val('');
    $('#ONU3').val('');
    $('#ONU4').val('');
    $('#TipoDocTransitoId').prop('selectedIndex', 0);
}

function obterDadosConteiner() {

    return {
        Reserva: {
            BookingId: $('#BookingId').val(),
            Tamanho: $('#Tamanho').val()
        },
        Id: $('#ConteinerId').val(),
        AgendamentoId: $('#Id').val(),
        Sigla: $('#Sigla').val(),
        Volumes: $('#Volumes').val(),
        Tamanho: $('#Tamanho').val(),
        TipoBasico: $('#TipoBasico').val(),
        Tara: $('#Tara').val(),
        Bruto: $('#Bruto').val(),
        PesoLiquido: $('#PesoLiquido').val(),
        ONU1: $('#ONU1').val(),
        ONU2: $('#ONU2').val(),
        ONU3: $('#ONU3').val(),
        ONU4: $('#ONU4').val(),
        IMO1: $('#IMO1').val(),
        IMO2: $('#IMO2').val(),
        IMO3: $('#IMO3').val(),
        IMO4: $('#IMO4').val(),
        Temp: $('#Temp').val(),
        Escala: $('#Escala').val(),
        ISO: $('#ISO').val(),
        Umidade: $('#Umidade').val(),
        Ventilacao: $('#Ventilacao').val(),
        Comprimento: $('#Comprimento').val(),
        Altura: $('#Altura').val(),
        LateralDireita: $('#LateralDireita').val(),
        LateralEsquerda: $('#LateralEsquerda').val(),
        LacreArmador1: $('#LacreArmador1').val(),
        LacreArmador2: $('#LacreArmador2').val(),
        OutrosLacres1: $('#OutrosLacres1').val(),
        OutrosLacres2: $('#OutrosLacres2').val(),
        OutrosLacres3: $('#OutrosLacres3').val(),
        OutrosLacres4: $('#OutrosLacres4').val(),
        LacreExportador: $('#LacreExportador').val(),
        LacreSIF: $('#LacreSIF').val(),
        DAT: $('#DAT').val(),
        Observacoes: $('#Observacoes').val(),
        TipoDocTransitoId: $('#TipoDocTransitoId').val(),
        NumDocTransitoDUE: $('#NumDocTransitoDUE').val(),
        DataDocTransitoDUE: $('#DataDocTransitoDUE').val(),
        ReeferLigado: $('#ReeferLigado').is(':checked')
    };
}

function confirmarCadastroConteiner() {

    var obj = obterDadosConteiner();

    $('#btnAdicionarConteiner')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    $.post(urlBase + 'Agendamento/CadastrarConteiner', obj, function (resultado) {

        $('#pnlConteineres').html(resultado);

        atualizarSaldoBooking(obj.Reserva.BookingId, obj.AgendamentoId);

        $('#pnlDadosConteiner').fadeOut(500, function () {
            $(this)
                .data('aberto', '0')
                .data('bookingId', '')
                .addClass('invisivel');
        });

        limparCamposConteiner();

    }).fail(function (data) {

        mostrarErrosResponse(data);

    }).always(function () {

        $('#modal-conteiner-reefer')
            .modal('hide');

        $('#btnAdicionarConteiner')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');
    });

}

function CadastrarConteinerCheio() {

    event.preventDefault();

    limpaErros();

    var obj = obterDadosConteiner();
    console.log(obj);

    $.post(urlBase + 'Agendamento/ValidarConteiner', obj, function (validou) {

        if (validou) {

            if ($('#ReeferLigado').is(':checked')) {

                var ventilacao = parseInt($('#Ventilacao').val()) === 0 ? 'CLOSED' : $('#Ventilacao').val();
                var umidade = parseInt($('#Umidade').val()) === 0 ? 'OFF' : $('#Umidade').val();

                $('#lblModalReefer').html('Ligado');
                $('#lblModalReeferTemp').html($('#Temp').val());
                $('#lblModalReeferVent').html(ventilacao);
                $('#lblModalReeferUmidade').html(umidade);
                $('#lblModalReeferEscala').html($('#Escala option:selected').text());

                $('#modal-conteiner-reefer')
                    .modal('show');

                return false;
            }

            confirmarCadastroConteiner(obj);
        }

    }).fail(function (data) {

        mostrarErrosResponse(data);
    });
}

function cadastrarConteinerVazio(bookingId, tipo, tamanho) {

    event.preventDefault();

    limpaErros();

    $('#salvarConteinerVazio')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Reserva: {
            BookingId: bookingId,
            Tamanho: tamanho,
            Tipo: tipo
        },
        Id: $('#ConteinerId').val(),
        AgendamentoId: $('#Id').val(),
        QuantidadeVazios: $('#QuantidadeVazios').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarConteinerVazio', obj, function (resultado) {

        $('#pnlConteineres').html(resultado);

        atualizarSaldoBooking(obj.Reserva.BookingId, obj.AgendamentoId);

        limparCamposConteiner();

    }).fail(function (data) {

        mostrarErrosResponse(data);

    }).always(function () {

        $('#btnAdicionarConteiner')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');
    });
}

function excluirConteiner(id, sigla, bookingId) {

    $('#del-modal-conteiner')
        .data('id', id);

    $('#del-modal-conteiner')
        .data('sigla', sigla);

    $('#del-modal-conteiner')
        .data('bookingId', bookingId);

    $('#del-modal-conteiner')
        .modal('show');
}

function confirmarExclusaoConteiner() {

    var _url = urlBase + 'Agendamento/ExcluirConteiner';

    var _id = $('#del-modal-conteiner')
        .data('id');

    var _sigla = $('#del-modal-conteiner')
        .data('sigla');

    var _bookingId = $('#del-modal-conteiner')
        .data('bookingId');

    var _agendamentoId = $('#Id').val();

    $.post(_url, { id: _id, agendamentoId: _agendamentoId }, function (resultado) {

        $('#pnlConteineres').html(resultado);

        obterDanfesPorConteiner(_sigla, _agendamentoId);

        toastr.success('Contêiner excluído com sucesso!', 'Agendamento');

        atualizarSaldoBooking(_bookingId, _agendamentoId);

    }).fail(function (response) {

        mostrarErrosResponse(response);
    }).always(function () {

        $('#del-modal-conteiner')
            .data('id', '0')
            .modal('hide');

        $('#del-modal-conteiner')
            .data('bookingCsItemId', '0')
            .modal('hide');
    });
}

function vincularDanfes(bookingId, sigla, id) {

    event.preventDefault();

    limpaErros();
    limparCamposConteiner();

    $('#pnlDadosConteiner').hide('slow');

    $('#BookingId').val(bookingId);
    $('#Sigla').val(sigla);
    $('#ConteinerId').val(id);
    $('#UploadSiglaConteiner').val(sigla);

    habilitarCamposDanfe();

    $('#tblConteineres tr').removeClass("linha-selecionada");
    $('#linha-conteiner-' + id).addClass('linha-selecionada');

    obterDanfesPorConteiner(sigla, $('#Id').val());
    //obterUploadsPorConteiner(sigla);

    $('#pnlDadosDanfes').removeClass('invisivel');
    $('#pnlUploads').removeClass('invisivel');

    $('#Danfe').focus();
}

function visualizarConteiner(id) {

    event.preventDefault();

    limpaErros();

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
            $('#PesoLiquido').val(resultado.PesoLiquido);
            $('#ONU1').val(resultado.ONU1);
            $('#ONU2').val(resultado.ONU2);
            $('#ONU3').val(resultado.ONU3);
            $('#ONU4').val(resultado.ONU4);
            $('#IMO1').val(resultado.IMO1);
            $('#IMO2').val(resultado.IMO2);
            $('#IMO3').val(resultado.IMO3);
            $('#IMO4').val(resultado.IMO4);
            $('#Escala').val(resultado.Escala);
            $('#ISO').val(resultado.ISO);
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

            if (resultado.ReeferLigado) {
                $('#ReeferLigado').bootstrapToggle('on');
            } else {
                $('#ReeferLigado').bootstrapToggle('off');
            }

            $('#Temp').val(resultado.Temp);
            $('#Ventilacao').val(resultado.Ventilacao);
            $('#Umidade').val(resultado.Umidade);

            //$('#IMO')
            //    .empty()
            //    .append($('<option>', {
            //        value: resultado.IMO,
            //        text: resultado.IMO
            //    }));

            //$('#ONU')
            //    .empty()
            //    .append($('<option>', {
            //        value: resultado.ONU,
            //        text: resultado.ONU
            //    }));

            if (resultado.Bagagem) {
                $('#Bagagem').val('Sim');
            }
            else {
                $('#Bagagem').val('Não');
            }

            $('#pnlDadosConteiner').show('slow');
        }
    });
}

function ValidarCamposReefer() {

    var desligado = !$('#ReeferLigado').is(':checked');

    $('#Temp')
        .val('')
        .prop('disabled', desligado);

    $('#Ventilacao')
        .val('')
        .prop('disabled', desligado);

    $('#Umidade')
        .val('')
        .prop('disabled', desligado);

    $('#Escala')
        .prop('selectedIndex', 0)
        .prop('disabled', desligado);

    if (desligado)
        $('#Escala').prop('selectedIndex', -1);
}

function AtualizarFlagDue() {

    var check = $('#DueDesembaracada').is(':checked');

    var obj = {
        agendamentoId: $('#Id').val(),
        check
    };

    $.post(urlBase + 'Agendamento/AtualizarDueDesembaracada', obj, function (resultado) {



    }).fail(function (data) {

        mostrarErrosResponse(data);
    });
}
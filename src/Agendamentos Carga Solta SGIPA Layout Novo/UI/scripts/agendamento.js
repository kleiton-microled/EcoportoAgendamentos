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
        var bookingCsId = $('#BookingCsId').val();

        $('#msgErro').html('').addClass('invisivel');

        if (stepDirection === 'forward') {

            if (stepNumber == 0 && motoristaId === '') {

                $('#msgErro')
                    .html('Selecione o Motorista')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 1 && veiculoId === '') {

                $('#msgErro')
                    .html('Selecione o Veículo')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 1 && cte === '') {

                $('#msgErro')
                    .html('Informe o número do CTE')
                    .removeClass('invisivel');
                return false;
            }

            if (stepNumber == 2) {

                $('#btnConcluir').show();
            }
        }
    });

    $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        transitionEffect: 'fade',
        showStepURLhash: true,
        enableAllSteps: true,
        toolbarSettings: {
            toolbarPosition: 'bottom',
            toolbarButtonPosition: 'end'
        },
        lang: {
            next: 'Próximo',
            previous: 'Anterior'
        }
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

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesMotorista($('#MotoristaId').val());

    $("#ListaMotoristas").empty();
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

    $("#" + toggle)
        .empty()
        .append($('<option>', {
            value: id,
            text: descricao
        })).focus();

    obterDetalhesVeiculo($('#VeiculoId').val());

    $("#ListaVeiculos").empty();
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
                $("#smartwizard :input").prop("disabled", false);
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#VeiculoId').change(function () {

    obterDetalhesVeiculo($(this).val());
});

$('#Reserva').on('keypress', function (e) {

    if (e.which == 13) {
        consultarReserva($(this).val());
    }
});

$('#Reserva').on('keydown', function (e) {

    if (e.which == 9) {
        consultarReserva($(this).val());
    }
});

function consultarReserva(reserva) {

    $('#validaReserva')
        .html('')
        .addClass('invisivel');

    limpar();

    $('#pnlItensReserva').html('');

    if (reserva === '') {

        $('#validaReserva').removeClass('invisivel');

        return;
    }

    $.get(urlBase + 'Agendamento/ObterDetalhesReserva?reserva=' + reserva, function (resultado) {

        if (resultado) {

            $('#BookingCsId').val(resultado.BookingCsId);
            $('#Navio').val(resultado.Navio);
            $('#ViagemId').val(resultado.ViagemId);
            $('#Viagem').val(resultado.Viagem);
            $('#Abertura').val(resultado.Abertura);
            $('#Fechamento').val(resultado.Fechamento);
            $('#Exportador').val(resultado.Exportador);

            $('#validaReserva').addClass('invisivel');

            obterItensReserva($('#Reserva').val(), $('#ViagemId').val());
            validarAgendamentoSemSaldo($('#Reserva').val(), $('#ViagemId').val());

            desabilitarCamposDanfe();

            $('#pnlReserva').removeClass('invisivel');
        }
    }).fail(function (data) {

        $('#validaReserva')
            .html(data.statusText)
            .removeClass('invisivel');

        limpar();
    });

    $('#btnConcluir')
        .prop('disabled', false);
}

function limpar() {

    limparCamposDanfe();
    limparCamposItem();
}

function limparCamposDanfe() {

    $('#Danfe').val('');
    $('#CFOP').val('');

    desabilitarCamposDanfe();
}

function limparCamposItem() {

    $('#QuantidadeItemReserva').val('');
    $('#Chassis').val('');
}

function obterItensReserva(reserva, viagemId) {

    $('#ItemReservaId').empty();

    $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + reserva + '&viagemId=' + viagemId, function (resultado) {

        if (resultado) {

            resultado.forEach(function (item) {
                $('#ItemReservaId').append($('<option>', {
                    value: item.BookingCsItemId,
                    text: item.Descricao
                }));
            });
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    }).done(function () {
        obterDetalhesItem();
    });
}

function validarAgendamentoSemSaldo(reserva, viagemId) {

    $.get(urlBase + 'Agendamento/ObterSaldoItensReserva?reserva=' + reserva + '&viagemId=' + viagemId, function (resultado) {

        if (resultado != null) {

            if (parseInt(resultado) === 0) {

                $('#Reserva').val('');

                $('#lnkIrParaAgendamentos').attr('href', urlBase + 'Agendamento/Cadastrar');

                $('#modal-reserva-sem-saldo')
                    .modal('show');
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#ItemReservaId').change(function () {

    obterDetalhesItem();
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

    $.get(urlBase + 'Agendamento/ObterPeriodos', function (resultado) {

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

var agendamentoMensagemSucesso = function (data) {

    toastr.success('Agendamento cadastrado com sucesso!', 'Agendamento');

};

var agendamentoMensagemErro = function (xhr, status) {

    $('#btnConcluirAgendamento')
        .html('Concluir')
        .removeClass('disabled');

    if (xhr !== null && xhr.responseText !== '') {

        toastr.error('Falha ao cadastrar o Agendamento. Verifique mensagens.', 'Agendamento');

        var msg = $('#msgErro');

        msg.html('');
        msg.removeClass('invisivel');

        var resultado = JSON.parse(xhr.responseText);

        var mensagens = resultado.erros.map(function (erro) {
            return '<li>' + erro.ErrorMessage + '</li>'
        })

        msg.html(mensagens);

    } else {
        toastr.error(xhr.statusText, 'Agendamento');
    }
};

$('#btnAdicionarItemReserva').click(function () {

    event.preventDefault();

    $('#msgErro').html('').addClass('invisivel');

    if ($('#QuantidadeItemReserva').val() === '') {

        $('#msgErro')
            .html('Informe a Quantidade do item')
            .removeClass('invisivel');
        return;
    }

    $('#btnAdicionarItemReserva')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Reserva: $('#Reserva').val(),
        BookingCsItemId: $('#ItemReservaId').val(),
        Qtde: $('#QuantidadeItemReserva').val(),
        Chassis: $('#Chassis').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarItemReserva', obj, function (resultado) {

        $('#tblItensReserva').html(resultado);

        obterDanfesPorItem(obj.BookingCsItemId);
        obterUploadsPorItem(obj.BookingCsItemId);

        obterPeriodos();

        limparCamposItem();

    }).fail(function (data) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            var msg = retorno.erros[0].ErrorMessage;

            $('#msgErro').html(msg).removeClass('invisivel');

        } else {
            if (data.statusText) {
                toastr.error(data.statusText, 'Agendamento');
            } else {
                toastr.error('Falha ao incluir a Danfe', 'Agendamento');
            }
        }

    }).always(function () {

        $('#btnAdicionarItemReserva')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');

    });
});

function excluirItemReserva(id, bookingCsItemId) {

    $('#modal-mensagem-item').text('Confirma a exclusão do item?');

    $('#del-modal-item')
        .data('id', id);

    $('#del-modal-item')
        .data('bookingCsItemId', bookingCsItemId);

    $('#del-modal-item')
        .modal('show');
}

function confirmarExclusaoItemReserva() {

    var _url = urlBase + 'Agendamento/ExcluirItemReserva';

    var _id = $('#del-modal-item')
        .data('id');

    var _bookingCsItemId = $('#del-modal-item')
        .data('bookingCsItemId');

    console.log(_id);
    console.log(_bookingCsItemId);

    $.post(_url, { id: _id }, function (resultado) {

        $('#tblItensReserva').html(resultado);

        obterDanfesPorItem(_bookingCsItemId);
        obterUploadsPorItem(_bookingCsItemId);

    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir o item', 'Agendamento');
        }
    }).always(function () {

        $('#del-modal-item')
            .data('id', '0')
            .modal('hide');
    });
}

function selecionarItem(bookingCsItemId, qtde, chassis) {

    event.preventDefault();

    limparCamposItem();

    $.get(urlBase + 'Agendamento/ObterItemReservaPorId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#Reserva').val(resultado.Reserva);
            $('#ViagemId').val(resultado.ViagemId);

            $('#ItemReservaId').empty();

            $.get(urlBase + 'Agendamento/ObterItensReserva?reserva=' + resultado.Reserva + '&viagemId=' + resultado.ViagemId, function (resultadoItens) {

                if (resultadoItens) {

                    resultadoItens.forEach(function (item) {

                        $('#ItemReservaId').append($('<option>', {
                            value: item.BookingCsItemId,
                            text: item.Descricao
                        }));
                    });
                }

                if ($("#ItemReservaId option[value='" + bookingCsItemId + "']").val() === undefined) {
                    $('#ItemReservaId').append($('<option>', {
                        value: resultado.BookingCsItemId,
                        text: resultado.Descricao
                    }));
                }

                $('#ItemReservaId').val(bookingCsItemId);

                $('#QuantidadeItemReserva').val(qtde);
                $('#Chassis').val(chassis);

                $('#TiposDocumentos').empty();

                if (resultado.PackingList) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 1,
                        text: 'Packing List'
                    }));
                }

                if (resultado.DesenhoTecnico) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 2,
                        text: 'Desenho Técnico'
                    }));
                }

                if (resultado.ImagemCarga) {

                    $('#TiposDocumentos').append($('<option>', {
                        value: 3,
                        text: 'Imagem Carga'
                    }));
                }

                obterDanfesPorItem(bookingCsItemId);
                obterUploadsPorItem(bookingCsItemId);
                habilitarCamposDanfe();

                $('#Danfe').focus();

            }).fail(function (data) {
                toastr.error(data.statusText, 'Agendamento');
            });
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

function obterDanfesPorItem(bookingCsItemId) {

    $.get(urlBase + 'Agendamento/ObterDanfesPorItemId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#tblDanfes').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

function obterUploadsPorItem(bookingCsItemId) {

    $.get(urlBase + 'Agendamento/ObterUploadsPorItemId?bookingCsItemId=' + bookingCsItemId, function (resultado) {

        if (resultado) {

            $('#tblDocumentos').html(resultado);
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#btnAdicionarDanfe').click(function () {

    event.preventDefault();

    $('#msgErro')
        .html('')
        .addClass('invisivel');

    if ($('#ItemReservaId').val() === '') {

        $('#msgErro')
            .html('Selecione um item')
            .removeClass('invisivel');
        return;
    }

    var retorno = validarDanfe($('#Danfe').val());

    if (retorno !== '') {

        $('#msgErro')
            .html(retorno)
            .removeClass('invisivel');

        $('#Danfe').focus();

        return;
    }

    if ($('#CFOP').val() === '') {

        $('#msgErro')
            .html('O CFOP é obrigatório')
            .removeClass('invisivel');

        return;
    }

    $('#btnAdicionarDanfe')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    var obj = {
        Danfe: $('#Danfe').val(),
        Reserva: $('#Reserva').val(),
        CFOP: $('#CFOP').val(),
        CTE: $('#CTE').val(),
        BookingCsItemId: $('#ItemReservaId').val()
    };

    $.post(urlBase + 'Agendamento/CadastrarDanfes', obj, function (resultado) {

        $('#tblDanfes').html(resultado);

        limparCamposDanfe();

    }).fail(function (data) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            var msg = retorno.erros[0].ErrorMessage;

            $('#msgErro').html(msg).removeClass('invisivel');

        } else {
            if (data.statusText) {
                toastr.error(data.statusText, 'Agendamento');
            } else {
                toastr.error('Falha ao incluir a Danfe', 'Agendamento');
            }
        }
    }).always(function () {

        $('#btnAdicionarDanfe')
            .html('<i class="fas fa-save"></i>')
            .removeClass('disabled');
    });
});

function selecionarDanfe(danfe, cfop) {

    event.preventDefault();

    $('#Danfe').val(danfe);
    $('#CFOP').val(cfop);
}

function excluirDanfe(id, bookingCsItemId) {

    $('#modal-mensagem-danfe').text('Confirma a exclusão da Danfe?');

    $('#del-modal-danfe')
        .data('id', id);

    $('#del-modal-danfe')
        .data('bookingCsItemId', bookingCsItemId);

    $('#del-modal-danfe')
        .modal('show');
}

function confirmarExclusaoDanfe(id) {

    var _url = urlBase + 'Agendamento/ExcluirDanfe';

    var _id = $('#del-modal-danfe')
        .data('id');

    var _bookingCsItemId = $('#del-modal-danfe')
        .data('bookingCsItemId');

    $.post(_url, { id: _id, bookingCsItemId: _bookingCsItemId }, function (resultado) {
        $('#tblDanfes').html(resultado);
    }).fail(function (data) {
        if (data.statusText) {
            toastr.error(data.statusText, 'Agendamento');
        } else {
            toastr.error('Falha ao excluir a Danfe', 'Agendamento');
        }
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

    var _url = urlBase + 'Agendamento/ExcluirUpload';

    var _id = $('#del-modal-upload')
        .data('id');

    $.post(_url, { id: _id }, function (resultado) {
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

function obterDetalhesItem() {

    var bookingCsItemId = $('#ItemReservaId').val();
    var reserva = $('#Reserva').val();

    $('#Chassis').prop('readonly', true);
    $('#TiposDocumentos').empty();

    if (!isNumero(bookingCsItemId)) {
        toastr.error('Nenhum item encontrado para a reserva ' + reserva, 'Agendamento');
        return;
    }

    $.get(urlBase + 'Agendamento/ObterDetalhesItem?bookingCsItemId=' + bookingCsItemId + '&reserva=' + reserva, function (resultado) {

        if (resultado) {

            if (resultado.Veiculo) {
                $('#Chassis').prop('readonly', false);
            }
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'Agendamento');
    });
}

$('#btnAdicionarUpload').click(function (e) {

    var form = $('#frmCadastrarDocumentos');

    var formData = new FormData();
    var totalFiles = document.getElementById("Upload").files.length;

    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("Upload").files[i];
        formData.append("Upload", file);
        formData.append("TiposDocumentos", $('#TiposDocumentos').val());
        formData.append("BookingCsItemId", $('#ItemReservaId').val());
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
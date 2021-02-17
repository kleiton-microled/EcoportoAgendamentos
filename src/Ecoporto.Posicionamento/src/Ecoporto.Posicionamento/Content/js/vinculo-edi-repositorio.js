function Remover(event) {
    debugger
    if (event.target.tagName == "A") {
        event.target.parentNode.parentNode.classList.add("fadeOut");

        setTimeout(function () {
            event.target.parentNode.parentNode.remove();
        }, 500);
    } else if (event.tagName == "A") {
        event.parentNode.parentNode.classList.add("fadeOut");

        setTimeout(function () {
            event.parentNode.parentNode.remove();
        }, 500);
    }

}


$(function () {
    var operacao = "A"; //"A"=Adição; "E"=Edição
    var indice_selecionado = -1; //Índice do item selecionado na lista
    var tbNotificacoes = localStorage.getItem("tbNotificacoes");// Recupera os dados armazenados
    tbNotificacoes = JSON.parse(tbNotificacoes); // Converte string para objeto
    if (tbNotificacoes == null) // Caso não haja conteúdo, iniciamos um vetor vazio
        tbNotificacoes = [];
});


function Adicionar() {

    var tipoNotificacaoSelect = document.getElementById("TipoNotificacaoId");
    var tipoNotificacaoText = tipoNotificacaoSelect.options[tipoNotificacaoSelect.selectedIndex].text;

    var notificacaoSelect = document.getElementById("NotificacaoId");
    var notificacaoText = notificacaoSelect.options[notificacaoSelect.selectedIndex].text;

    var modeloSelect = document.getElementById("ModeloArquivoId");
    var modeloText = modeloSelect.options[modeloSelect.selectedIndex].text;

    var notificacao = JSON.stringify({
        Tipo: tipoNotificacaoText,
        IdTipo: $('#TipoNotificacaoId').val(),
        Notificacao: notificacaoText,
        NotificacaoId: $('#NotificacaoId').val(),
        ModeloId: $('#ModeloArquivoId').val(),
        Modelo: modeloText
    });
    tbNotificacoes.push(notificacao);
    localStorage.setItem("tbNotificacoes", JSON.stringify(tbNotificacoes));
    console.log("Registro adicionado.");
    return true;
}


function Excluir() {
    tbNotificacoes.splice(indice_selecionado, 1);
    localStorage.setItem("tbNotificacoes", JSON.stringify(tbNotificacoes));
    console.log("Registro excluído.");
}
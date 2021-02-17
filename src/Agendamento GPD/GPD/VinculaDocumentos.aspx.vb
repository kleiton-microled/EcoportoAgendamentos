Imports System.IO
Imports System.Net

Public Class VinculaDocumentos
    Inherits System.Web.UI.Page

    Private Lote As Integer = 0
    Private Doct As String = ""

    Private Function ObterLote() As Integer
        If Request.QueryString("lote") IsNot Nothing Then
            Return Val(Request.QueryString("lote").ToString())
        End If
    End Function

    Private Function ObterIdAgendamento() As Integer
        If Request.QueryString("id") IsNot Nothing Then
            Return Val(Request.QueryString("id").ToString())
        End If
    End Function

    Private Function ObterMotoristaAgendamento() As Integer
        If Request.QueryString("motorista") IsNot Nothing Then
            Return Val(Request.QueryString("motorista").ToString())
        End If
    End Function

    Private Function ObterVeiculoAgendamento() As Integer
        If Request.QueryString("veiculo") IsNot Nothing Then
            Return Val(Request.QueryString("veiculo").ToString())
        End If
    End Function

    Private Function ObterDocumentoDoLote() As Integer
        Return Documento.ObterTipoDocumento(ObterLote())
    End Function

    Private Function EhEdicao() As Boolean
        If Request.QueryString("edit") IsNot Nothing Then
            Return Convert.ToBoolean(Val(Request.QueryString("edit").ToString()))
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            CarregarTiposDocumentos()
            CarregarDocumentosAnexados()

            Me.chkAceite.Checked = Val(Banco.Conexao.Execute("SELECT FLAG_DECLARA_DOCS_AGENDAMENTO FROM TB_CNTR_BL WHERE AUTONUM = " & ObterIdAgendamento()).Fields(0).Value)

        End If

    End Sub

    Private Sub CarregarDocumentosAnexados()

        Me.gdvDocumentos.DataSource = Documento.Consultar(ObterIdAgendamento(), ObterMotoristaAgendamento(), ObterVeiculoAgendamento(), Session("SIS_ID").ToString())
        Me.gdvDocumentos.DataBind()

    End Sub

    Private Sub CarregarTiposDocumentos()

        Dim ds As New DataTable
        ds = Documento.ObterDocumentoPorTipoDocumento(ObterDocumentoDoLote())

        If ds IsNot Nothing Then
            Me.cbDocumentos.Items.Add(New ListItem("-- Selecione --", "0"))
            For Each Linha As DataRow In ds.Rows
                Me.cbDocumentos.Items.Add(New ListItem(Linha("DESCRICAO").ToString(), Linha("AUTONUM").ToString()))
            Next
        End If

    End Sub

    Protected Sub gdvDocumentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvDocumentos.RowCommand

        Dim Index As Integer = e.CommandArgument

        If e.CommandName.Equals("DEL") Then

            Dim IDAvImagem As String = Me.gdvDocumentos.DataKeys(Index)("AUTONUM_AV_IMAGEM").ToString()
            Dim Liberado As String = Banco.Conexao.Execute("SELECT NVL(FLAG_LIBERADO,0) FLAG_LIBERADO FROM TB_CNTR_BL WHERE AUTONUM = " & ObterIdAgendamento()).Fields(0).Value.ToString()
            Dim Bloqueio As String = Banco.Conexao.Execute("SELECT NVL(A.FLAG_BLOQUEIO,0) FLAG_BLOQUEIO FROM TB_AGENDAMENTO_DOC A INNER JOIN TB_AV_IMAGEM B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC WHERE B.AUTONUM = " & IDAvImagem).Fields(0).Value.ToString()

            Dim Status As String = String.Empty

            Status = Banco.Conexao.Execute("SELECT NVL(IMPRESSO,0) IMPRESSO FROM TB_CNTR_BL WHERE AUTONUM = " & ObterIdAgendamento()).Fields(0).Value.ToString()

            If Val(Status) = 1 Or Val(Liberado) Then
                If Val(Bloqueio) = 1 Then
                    Me.lblErro.Text = "Documento já analisado, não permitindo a exclusão"
                    Me.lblErro.Visible = True
                    Exit Sub
                End If
            End If

            Try
                Dim ws As New WsSharepoint.WsIccSharepointSoapClient()
                ws.ExcluirImagemDocAverbacaoPorLoteEautonum(ObterLote(), IDAvImagem)

                CarregarDocumentosAnexados()
            Catch ex As Exception
                Console.Write("")
            End Try


        End If

    End Sub

    Protected Sub btnAnexar_Click(sender As Object, e As EventArgs) Handles btnAnexar.Click

        Me.lblErro.Visible = False
        Me.lblSucesso.Visible = False

        If Me.cbDocumentos.SelectedIndex = 0 Then
            Me.lblErro.Text = "Selecione o tipo de arquivo"
            Me.lblErro.Visible = True
            Exit Sub
        End If

        If Not txtUpload.HasFile Then
            Me.lblErro.Text = "Selecione um arquivo"
            Me.lblErro.Visible = True
            Exit Sub
        End If

        'PDF - GIF - JPEG - PNG
        If Not txtUpload.FileName.ToUpper().EndsWith("PDF") And
            Not txtUpload.FileName.ToUpper().EndsWith("JPG") And
            Not txtUpload.FileName.ToUpper().EndsWith("JPEG") And
            Not txtUpload.FileName.ToUpper().EndsWith("GIF") And
            Not txtUpload.FileName.ToUpper().EndsWith("PNG") Then
            Me.lblErro.Text = "Tipo de arquivo não permitido"
            Me.lblErro.Visible = True
            Exit Sub
        End If

        Dim NomeArquivo As String = String.Empty
        Dim TamanhoArquivo As Integer = 0

        If txtUpload.HasFile Then

            NomeArquivo = Path.GetFileName(Me.txtUpload.PostedFile.FileName)
            TamanhoArquivo = Me.txtUpload.PostedFile.ContentLength

            If TamanhoArquivo > 5242880 Then
                Me.lblErro.Text = "O tamanho do arquivo ultrapassa 5MB"
                Me.lblErro.Visible = True
                Exit Sub
            End If

            Try
                'If Not Documento.DocumentoJaCadastrado(ObterIdAgendamento(), Me.cbDocumentos.SelectedValue) Then

                Using binario = New BinaryReader(Me.txtUpload.PostedFile.InputStream)

                        Dim dados As Byte() = Nothing
                        dados = binario.ReadBytes(Me.txtUpload.PostedFile.ContentLength)

                        Dim dto As New DocumentoDto
                        dto = Documento.ObterInformacoesLote(ObterLote())

                        If dto IsNot Nothing Then

                            Dim img As New WsSharepoint.ImagemAverbacao With {
                                .NomeImagem = Path.GetFileName(Me.txtUpload.PostedFile.FileName),
                                .IdTipoDocUpload = Convert.ToInt32(Me.cbDocumentos.SelectedValue),
                                .Lote = Convert.ToInt64(dto.Lote),
                                .TipoDocumento = dto.TipoDocumento,
                                ._byteArrayImagem = dados,
                                .CaminhoImagem = " ",
                                .NumeroDocumento = dto.NumeroDocumento,
                                .DataInclusao = DateTime.Now,
                                .AutonumAgendamento = ObterIdAgendamento()
                            }

                        Dim ws As New WsSharepoint.WsIccSharepointSoapClient()
                        ws.EnviarImagemDocAverbacao(img)

                        End If

                        'Documento.GravarDocumento(ObterLote(), Me.txtUpload.FileName, Me.cbDocumentos.SelectedValue, ObterIdAgendamento(), dados)

                        If EhEdicao() Then
                            Session("DOC_ALTERADO") = Session("DOC_ALTERADO") & ";" & Me.cbDocumentos.SelectedItem.Text
                        End If

                    End Using

                    Me.lblSucesso.Visible = True

                'Else
                '    Me.lblErro.Text = "Este documento já foi anexado"
                '    Me.lblErro.Visible = True
                'End If
            Catch ex As Exception
                Me.lblErro.Text = "Erro ao anexar o arquivo."
                Me.lblErro.Visible = True
            Finally
                CarregarDocumentosAnexados()
            End Try

        End If

    End Sub

    Protected Sub chkAceite_CheckedChanged(sender As Object, e As EventArgs) Handles chkAceite.CheckedChanged
        Banco.Conexao.Execute("UPDATE TB_CNTR_BL SET FLAG_DECLARA_DOCS_AGENDAMENTO = " & Convert.ToInt32(chkAceite.Checked) & " WHERE AUTONUM = " & ObterIdAgendamento())
    End Sub
End Class

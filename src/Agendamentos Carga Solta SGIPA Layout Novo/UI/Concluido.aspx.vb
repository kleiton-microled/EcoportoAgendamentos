Imports System.Data.OleDb

Public Class Concluido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("id") IsNot Nothing Then

                Me.lblEmail.Text = Transportadora.ObterEmailAgendamento(Session("SIS_ID").ToString())

                Dim SQL As String = "SELECT A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, A.LOTE, B.NUMERO AS BL FROM TB_AG_CS A INNER JOIN TB_BL B ON A.LOTE = B.AUTONUM WHERE A.AUTONUM= " & Val(Request.QueryString("id").ToString()) & " AND A.AUTONUM_TRANSPORTADORA = " & Val(Session("SIS_ID").ToString())

                Dim Dt As New DataTable

                Using Connection As New OleDbConnection(Banco.StringConexaoOracle())
                    Using Adap As New OleDbDataAdapter(New OleDbCommand(SQL, Connection))

                        Adap.Fill(Dt)

                        If Dt.Rows.Count > 0 Then

                            Dim Protocolo As String = Dt.Rows(0)("PROTOCOLO").ToString()
                            Dim BL As String = Dt.Rows(0)("BL").ToString()
                            Dim Lote As String = Dt.Rows(0)("LoTE").ToString()
                            Dim Assunto As String = String.Empty

                            Dim Para As String = Banco.Conexao.Execute(" SELECT NVL(EMAIL_REGISTRO, ' ') EMAIL_REGISTRO FROM TB_PARAMETROS_SISTEMA").Fields(0).Value

                            If Request.QueryString("edit") IsNot Nothing Then
                                Assunto = "Agendamento atualizado: " & Protocolo & " Lote: " & Lote
                            Else
                                Assunto = "Envio de Documento: Agendamento " & Protocolo & " Lote: " & Lote
                            End If

                            Dt = Documento.Consultar(Request.QueryString("motorista").ToString(), Request.QueryString("veiculo").ToString(), Request.QueryString("id").ToString(), Lote, Session("SIS_ID").ToString())

                            Dim Mensagem As String = String.Empty
                            Dim Documentos As String = String.Empty

                            If Dt.Rows.Count > 0 Then

                                Dim ListaDocumentos As New List(Of String)

                                If Dt IsNot Nothing Then

                                    Mensagem = "<p>Informo que foram anexados os seguintes os documentos:<br/>"

                                    For Each Doc As DataRow In Dt.Rows
                                        ListaDocumentos.Add(Doc("DESCRICAO").ToString())
                                    Next

                                    If Session("DOC_ALTERADO") IsNot Nothing Then

                                        'ListaDocumentos.Clear()
                                        Dim DocsAlterados() As String = Session("DOC_ALTERADO").ToString().Split(New Char() {";"c})

                                        If DocsAlterados.Count() > 0 Then
                                            For Each Doc In DocsAlterados
                                                If Doc <> String.Empty Then
                                                    ListaDocumentos.Add(Doc)
                                                End If
                                            Next
                                        End If

                                        Session("DOC_ALTERADO") = Nothing

                                    End If

                                    Documentos = String.Join("<br/>", ListaDocumentos.Distinct())

                                    Mensagem = Mensagem & "<br/><strong>" & Documentos & "</strong>"

                                End If

                                Dim msg = Email.EnviarEmail(
                                  "registroimportacao@ecoportosantos.com.br",
                                  Para,
                                  Assunto,
                                  "",
                                  "",
                                  Mensagem & "<br/><br/>Protocolo: " & Protocolo & " <br/>BL: " & BL & "<br/>Lote: " & Lote & "</p><br/>
                               <p>E-mail do cliente: " & Me.lblEmail.Text & "</p>")

                                Banco.Conexao.Execute("UPDATE TB_AG_CS SET MOTIVO_AGENDAMENTO_RECUSADO = NULL WHERE AUTONUM = " & Val(Request.QueryString("id").ToString()) & " AND AUTONUM_TRANSPORTADORA = " & Val(Session("SIS_ID").ToString()))
                            End If

                        End If

                    End Using
                End Using

            End If
        End If

    End Sub

End Class
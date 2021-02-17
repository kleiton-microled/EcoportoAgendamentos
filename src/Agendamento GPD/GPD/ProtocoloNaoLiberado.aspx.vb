Imports System.Data.OleDb

Public Class ProtocoloNaoLiberado
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("protocolo") IsNot Nothing Then

            Dim IDAgendamento As String = String.Empty

            Try
                IDAgendamento = Banco.Conexao.Execute("SELECT AUTONUM FROM TB_CNTR_BL WHERE NUM_PROTOCOLO || ANO_PROTOCOLO = '" & Request.QueryString("protocolo").ToString() & "'").Fields(0).Value.ToString()
            Catch ex As Exception

            End Try

            Dim agendamento As New Importacao
            agendamento = agendamento.ConsultarDadosAgendamento(Session("SIS_ID").ToString(), IDAgendamento)

            If agendamento IsNot Nothing Then

                Dim Doct As Integer = Documento.ObterTipoDocumentoPorConteiner(IDAgendamento)
                Dim QtdeRequerida As Integer = Documento.ObterQuantidadeRequerida(Doct)
                Dim QtdeVinculada As Integer = Documento.ObterQuantidadeVinculada(Val(IDAgendamento))

                Dim dsDocs As New DataTable
                dsDocs = Documento.Consultar(IDAgendamento, agendamento.AutonumMotorista, agendamento.AutonumVeiculo, Session("SIS_ID").ToString())
                QtdeVinculada = dsDocs.Rows.Count

                If agendamento.Placa_Cavalo = agendamento.Placa_carreta Then
                    QtdeRequerida = QtdeRequerida - 1
                End If

                If QtdeVinculada < QtdeRequerida Then

                    Dim dsAux As New DataTable
                    dsAux = Documento.DocumentosQueFaltam(IDAgendamento, Doct)
                    Dim ausentes As New List(Of String)

                    If dsAux IsNot Nothing Then
                        If dsAux.Rows.Count > 0 Then

                            For Each Doc In dsAux.Rows
                                If Val(Doc("AUTONUM").ToString()) = 8 Or Val(Doc("AUTONUM").ToString()) = 9 Then
                                    If agendamento.Placa_Cavalo <> agendamento.Placa_carreta Then
                                        ausentes.Add(Doc("DESCRICAO").ToString())
                                    End If
                                Else
                                    ausentes.Add(Doc("DESCRICAO").ToString())
                                End If
                            Next

                        End If
                    End If

                    Me.lblMsg.Text = "Protocolo não liberado. Pendente anexar os seguintes documentos: <br/> <b>" & String.Join(",", ausentes) & "</b> <br/>para posterior analise pelo setor responsável."
                Else
                    Me.lblMsg.Text = "<b>Agendamento não liberado para impressão. Aguarde enquanto a documentação está sendo analisada.</b>"
                End If

            End If

        End If

    End Sub
End Class
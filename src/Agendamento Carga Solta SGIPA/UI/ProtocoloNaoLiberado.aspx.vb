Public Class ProtocoloNaoLiberado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("id") IsNot Nothing Then

            Dim ds As New DataTable

            Dim agendamento As New Agendamento
            ds = agendamento.ConsultarDadosAgendamento(Request.QueryString("id").ToString())

            If ds IsNot Nothing Then
                If ds.Rows.Count > 0 Then

                    Dim Doct As Integer = Documento.ObterTipoDocumento(ds.Rows(0)("LOTE").ToString())
                    Dim QtdeRequerida As Integer = Documento.ObterQuantidadeRequerida(Doct)
                    Dim QtdeVinculada As Integer = Documento.ObterQuantidadeVinculada(Val(Request.QueryString("id").ToString()))

                    Dim dsDocs As New DataTable
                    dsDocs = agendamento.ConsultarDadosAgendamento(Request.QueryString("id").ToString())

                    If dsDocs IsNot Nothing Then
                        If dsDocs.Rows.Count > 0 Then
                            dsDocs = Documento.Consultar(dsDocs.Rows(0)("AUTONUM_MOTORISTA").ToString(), dsDocs.Rows(0)("AUTONUM_VEICULO").ToString(), dsDocs.Rows(0)("AUTONUM").ToString(), dsDocs.Rows(0)("LOTE").ToString(), Session("SIS_ID").ToString())
                            QtdeVinculada = dsDocs.Rows.Count
                        End If
                    End If

                    If ds.Rows(0)("PLACA_CAVALO").ToString() = ds.Rows(0)("PLACA_CARRETA").ToString() Then
                        QtdeRequerida = QtdeRequerida - 1
                    End If

                    If QtdeVinculada < QtdeRequerida Then

                        Dim dsAux As New DataTable
                        dsAux = Documento.DocumentosQueFaltam(Request.QueryString("id").ToString(), Doct)
                        Dim ausentes As New List(Of String)

                        If dsAux IsNot Nothing Then
                            If dsAux.Rows.Count > 0 Then

                                For Each Doc In dsAux.Rows
                                    If Val(Doc("AUTONUM").ToString()) = 8 Or Val(Doc("AUTONUM").ToString()) = 9 Then
                                        If ds.Rows(0)("PLACA_CAVALO").ToString() <> ds.Rows(0)("PLACA_CARRETA").ToString() Then
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

        End If

    End Sub

End Class
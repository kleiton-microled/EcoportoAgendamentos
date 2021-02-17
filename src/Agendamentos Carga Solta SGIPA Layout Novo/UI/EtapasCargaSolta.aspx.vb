Imports System.Drawing
Imports System.IO

Public Class EtapasCargaSolta
    Inherits System.Web.UI.Page

    Dim LoteEtapa As String

    Const AVERBOU = "SIM"
    Const SEFAZ = "SIM"
    Const DESEMBARAÇO = "SIM"
    Const MAPA_MADEIRA = "SIM"
    Const CERTIFICADO = "0"
    Const GR_PAGA = "SIM"
    Const GR_PAGA_FAT = "FAT"
    Const FORMA_PAGAMENTO = "3"
    Const SISCARGA = "SIM"
    Const BLOQUEIO_BL = "NAO"
    Const BLOQUEIO_CNTR = "NAO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not Page.IsPostBack Then
            If Not Request.QueryString("Pend") Is Nothing Then
                If Not Request.QueryString("Pend").ToString() = String.Empty Then
                    If Request.QueryString("Pend").ToString() = "SIM" Then
                        Label1.Visible = True
                        Label1.Text = "Gentileza , contactar o representante legal da carga / Despachante para sanar pendências"
                    Else
                        Label1.Visible = False
                        Label1.Text = ""
                    End If
                End If
            End If
            If Not Request.QueryString("Lote") Is Nothing Then
                If Not Request.QueryString("Lote").ToString() = String.Empty Then
                    LoteEtapa = Request.QueryString("Lote").ToString()
                    Consultar()
                End If
            End If
        End If

        DgEtapasCs.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        DgEtapasCs.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar()


        DsConsulta.ConnectionString = Banco.StringConexaoOracle()
        DsConsulta.ProviderName = "System.Data.OleDb"

        DsConsulta.SelectCommand = "SELECT PATIO,LOTE,NUMERO,TIPO_DOCUMENTO,NUM_DOCUMENTO,DATA_FREE_TIME,AVERBOU,DI_ICMS_SEFAZ,DI_DESEMBARACADA,MAPA_DE_MADEIRA,GR_PAGA,SISCARGA,BLOQUEIO_BL,TIPO_DOC,FLAG_CERTIFICADO,FORMA_PAGAMENTO FROM  VW_CONSULTA_TRANSP_ETAPA  WHERE Lote = " & LoteEtapa.ToString() & " ORDER BY DATA_FREE_TIME DESC"

        DsConsulta.DataBind()
        DgEtapasCs.DataBind()


    End Sub

    Protected Sub DgEtapasCs_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DgEtapasCs.RowDataBound

        Dim TipoDoc As String = String.Empty
        Dim FlagCertificado As String = String.Empty
        Dim FormaPagamento As String = String.Empty

        If e.Row.RowType = DataControlRowType.DataRow Then

            TipoDoc = DgEtapasCs.DataKeys(e.Row.RowIndex)("TIPO_DOC").ToString()
            FlagCertificado = DgEtapasCs.DataKeys(e.Row.RowIndex)("FLAG_CERTIFICADO").ToString()
            FormaPagamento = DgEtapasCs.DataKeys(e.Row.RowIndex)("FORMA_PAGAMENTO").ToString()

            For index = 0 To e.Row.Cells.Count - 1

                Dim HeaderText As String = DgEtapasCs.Columns(index).HeaderText.ToUpper()

                If Convert.ToInt32(TipoDoc) < 7 Or Convert.ToInt32(TipoDoc) > 13 Then

                    If HeaderText = "AVERBOU" Then
                        If e.Row.Cells(index).Text.ToUpper() <> AVERBOU Then
                            e.Row.Cells(index).BackColor = System.Drawing.Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "ICMS SEFAZ" Then
                        If e.Row.Cells(index).Text.ToUpper() <> SEFAZ Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "DESEMBARAÇADA" Then
                        If e.Row.Cells(index).Text.ToUpper() <> DESEMBARAÇO Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "MAPA MADEIRA" Then
                        If e.Row.Cells(index).Text.ToUpper() <> MAPA_MADEIRA Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "GR PAGA" Then
                        If (e.Row.Cells(index).Text.ToUpper() <> GR_PAGA And e.Row.Cells(index).Text.ToUpper() <> GR_PAGA_FAT) And FormaPagamento <> FORMA_PAGAMENTO Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "SISCARGA" Then
                        If e.Row.Cells(index).Text.ToUpper() <> SISCARGA Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "BLOQUEIO BL" Then
                        If e.Row.Cells(index).Text.ToUpper() <> BLOQUEIO_BL Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If


                Else

                    If HeaderText = "AVERBOU" Then
                        If e.Row.Cells(index).Text.ToUpper() <> AVERBOU Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "BLOQUEIO BL" Then
                        If e.Row.Cells(index).Text.ToUpper() <> BLOQUEIO_BL Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If


                    If HeaderText = "GR PAGA" Then
                        If (e.Row.Cells(index).Text.ToUpper() <> GR_PAGA And e.Row.Cells(index).Text.ToUpper() <> GR_PAGA_FAT) And FormaPagamento <> FORMA_PAGAMENTO Then
                            e.Row.Cells(index).BackColor = Color.Red
                            e.Row.Cells(index).ForeColor = Color.White
                        Else
                            e.Row.Cells(index).BackColor = Color.LightGreen
                        End If
                    End If

                    If HeaderText = "ICMS SEFAZ" Then
                        e.Row.Cells(index).BackColor = Color.LightGreen
                    End If

                    If HeaderText = "DESEMBARAÇADA" Then
                        e.Row.Cells(index).BackColor = Color.LightGreen
                    End If

                    If HeaderText = "MAPA MADEIRA" Then
                        e.Row.Cells(index).BackColor = Color.LightGreen
                    End If

                    If HeaderText = "SISCARGA" Then
                        e.Row.Cells(index).BackColor = Color.LightGreen
                    End If

                End If

            Next

        End If

    End Sub

    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Response.Redirect("ConsultarLotesImportacao.aspx")
    End Sub
End Class
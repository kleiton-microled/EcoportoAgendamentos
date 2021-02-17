Imports System.Drawing
Imports System.Drawing.Imaging

Public Class ProtocoloCS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then



                If Convert.ToInt32(Banco.ExecuteScalar("SELECT NVL(AUTONUM_GD_RESERVA,0) AUTONUM_GD_RESERVA FROM TB_AGENDAMENTO_WEB_CS WHERE NUM_PROTOCOLO || '/' || ANO_PROTOCOLO = '" & Request.QueryString("protocolo").ToString() & "'")) > 0 Then
                    Dim Sb As StringBuilder = GerarProtocolo(Request.QueryString("protocolo").ToString(), "", "1")
                    If Sb IsNot Nothing Then
                        conteudo.InnerHtml = "<center>" & Sb.ToString() & "</center>"
                        Banco.BeginTransaction("UPDATE REDEX.TB_AGENDAMENTO_WEB_CS SET STATUS = 'IM' WHERE NUM_PROTOCOLO = " & Request.QueryString("protocolo").ToString())
                    End If
                Else
                    Response.Redirect("ConsultarAgendamentosCargaSolta.aspx?erro=1")
                End If
            End If
        End If

    End Sub

    Public Function GerarProtocolo(ByVal ID As String, ByVal CorPadrao As String, ByVal Empresa As String) As StringBuilder

        Dim Rst1 As New DataTable
        Dim Rst2 As New DataTable
        Dim Rst3 As New DataTable
        Dim Rst4 As New DataTable

        Dim RstItens As New DataTable

        Dim SQL As New StringBuilder

        Dim Estrutura As New StringBuilder
        Dim Tabela1 As New StringBuilder
        Dim Tabela2 As New StringBuilder
        Dim Header As New StringBuilder
        Dim TabelaItem As New StringBuilder

        Dim Protocolos As String() = ID.Split(",")

        Dim ID_Conteiner As String = String.Empty

        Dim Contador As Integer = 0

        Dim Umidade As String = String.Empty
        Dim Ventilacao As String = String.Empty

        For Each Item In Protocolos

            SQL.Append("SELECT DISTINCT ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    TRANSPORTADORA, ")
            SQL.Append("    PLACA_CAVALO, ")
            SQL.Append("    PLACA_CARRETA, ")
            SQL.Append("    MODELO, ")
            SQL.Append("    NOME, ")
            SQL.Append("    CNH, ")
            SQL.Append("    RG, ")
            SQL.Append("    NEXTEL, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    DT_DEAD_LINE, ")
            SQL.Append("    PORTO_DESTINO, ")
            SQL.Append("    NAVIO_VIAGEM, ")
            SQL.Append("    PROTOCOLO, ")
            SQL.Append("    PERIODO, ")
            SQL.Append("    PATIO ")
            SQL.Append("FROM ")
            SQL.Append("  REDEX.VW_AGENDAMENTO_WEB_CS_PROT ")
            SQL.Append("WHERE ")
            SQL.Append("    PROTOCOLO = '{0}' AND AUTONUM_TRANSPORTADORA = {1}")

            Rst1 = Banco.List(String.Format(SQL.ToString(), Item, Session("SIS_AUTONUM_TRANSPORTADORA")))

            For Each Rst1Item As DataRow In Rst1.Rows

                Header.Append("<table id=cabecalho>")
                Header.Append("	<tr>")
                Header.Append("		<td align=left width=180px>")

                If Empresa = "1" Then
                    Header.Append("			<img src=css/img/LogoTop.png />")
                Else
                    Header.Append("			<img src=css/img/LogoTop.png />")
                End If

                Header.Append("		</td>")
                Header.Append("		<td>")
                Header.Append("		<font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE CARGA SOLTA</font>")
                Header.Append("		<br/>")
                Header.Append("        <font face=Arial size=5>Nº: " & Rst1Item("PROTOCOLO").ToString() & "</font> ")

                If Empresa = "1" Then
                    Header.Append("		<br/><br/>Período previsto de chegada no terminal ECOPORTO:<br/>")
                Else
                    Header.Append("		<br/><br/>Período previsto de chegada no terminal ECOPORTO ALFANDEGADO:<br/>")
                End If

                Header.Append("        " & Rst1Item("PERIODO").ToString())

                Header.Append("		</td>")
                Header.Append("	</tr>")
                Header.Append("</table>")
                Header.Append("<br/>")

                If Not Rst1.Rows.Count = 0 Then

                    Tabela1.Append("<table>")
                    Tabela1.Append("<caption>Dados do Transporte</caption>")
                    Tabela1.Append("    <thead bgcolor=#B3C63C>")
                    Tabela1.Append("        <td>TRANSPORTADORA</td>")
                    Tabela1.Append("        <td>PLACA CAVALO</td>")
                    Tabela1.Append("        <td>PLACA CARRETA</td>")
                    Tabela1.Append("        <td>MODELO</td>")
                    Tabela1.Append("    </thead>")
                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Rst1Item("TRANSPORTADORA").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("PLACA_CAVALO").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("PLACA_CARRETA").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("MODELO").ToString() & "</td>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("    <thead bgcolor=#B3C63C>")
                    Tabela1.Append("        <td>MOTORISTA</td>")
                    Tabela1.Append("        <td>CNH</td>")
                    Tabela1.Append("        <td>RG</td>")
                    Tabela1.Append("        <td>NEXTEL</td>")
                    Tabela1.Append("    </thead>")
                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Rst1Item("NOME").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("CNH").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("RG").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("NEXTEL").ToString() & "</td>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("</table>")

                    Tabela1.Append("<br/>")
                    Tabela1.Append("<table>")
                    Tabela1.Append("<caption>Identificação da Reserva</caption>")
                    Tabela1.Append("    <thead bgcolor=#B3C63C>")
                    Tabela1.Append("        <td>RESERVA</td>")
                    Tabela1.Append("        <td>DEAD LINE</td>")
                    Tabela1.Append("        <td>PORTO DESTINO</td>")
                    Tabela1.Append("        <td>NAVIO / VIAGEM</td>")
                    Tabela1.Append("    </thead>")
                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Rst1Item("RESERVA").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("DT_DEAD_LINE").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("PORTO_DESTINO").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1Item("NAVIO_VIAGEM").ToString() & "</td>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("</table>")
                    Tabela1.Append("<br/>")

                    SQL.Clear()
                    SQL.Append("SELECT ")
                    SQL.Append("    A.DANFE, ")
                    SQL.Append("    A.NUMERO, ")
                    SQL.Append("    A.SERIE, ")
                    SQL.Append("    A.QTDE, ")
                    SQL.Append("    A.VALOR, ")
                    SQL.Append("    A.PESO_BRUTO, ")
                    SQL.Append("    A.M3 ")
                    SQL.Append("FROM ")
                    SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CS_NF A ")
                    SQL.Append("WHERE ")
                    SQL.Append("    A.AUTONUM_AGENDAMENTO = " & Rst1Item("AUTONUM").ToString())

                    Rst2 = Banco.List(SQL.ToString())

                    If Rst2 IsNot Nothing Then
                        If Rst2.Rows.Count > 0 Then
                            Tabela1.Append("<table>")
                            Tabela1.Append("<caption>Identificação da Carga</caption>")
                            Tabela1.Append("    <thead bgcolor=#B3C63C>")
                            Tabela1.Append("       <td>DOCUMENTO</td>")
                            Tabela1.Append("       <td>M3</td>")
                            Tabela1.Append("       <td>PESO BRUTO</td>")
                            Tabela1.Append("       <td>QTDE</td>")
                            Tabela1.Append("       <td>VALOR</td>")
                            Tabela1.Append("       <td>DANFE</td>")
                            Tabela1.Append("    </thead>")

                            For Each LinhaNF As DataRow In Rst2.Rows
                                Tabela1.Append("    <tbody>")
                                Tabela1.Append("       <td>NF " & LinhaNF("NUMERO").ToString() & " " & LinhaNF("SERIE").ToString() & "</td>")
                                Tabela1.Append("       <td>" & LinhaNF("M3").ToString() & "</td>")
                                Tabela1.Append("       <td>" & LinhaNF("PESO_BRUTO").ToString() & "</td>")
                                Tabela1.Append("       <td>" & LinhaNF("QTDE").ToString() & "</td>")
                                Tabela1.Append("       <td>" & LinhaNF("VALOR").ToString() & "</td>")
                                Tabela1.Append("       <td>" & LinhaNF("DANFE").ToString() & "</td>")
                                Tabela1.Append("    </tbody>")
                            Next

                            Tabela1.Append("</table>")
                            Tabela1.Append("<br/>")

                        End If
                    End If

                    Tabela1.Append("<table>")
                    Tabela1.Append("    <thead bgcolor=#B3C63C>")
                    Tabela1.Append("        <td></td>")
                    Tabela1.Append("        <td>CHEGADA NO TERMINAL</td>")
                    Tabela1.Append("        <td>SAÍDA DO TERMINAL</td>")
                    Tabela1.Append("    </thead>")
                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("    <td align=""center""><img src=""CodigoBarra.aspx?protocolo=" & Rst1Item("PROTOCOLO").ToString() & "&modo=E"" /></td> ")
                    Tabela1.Append("        <td class=assinatura><br/> ")
                    Tabela1.Append("            ___/___/___ ___:___:___ ")
                    Tabela1.Append("    <br/> <br/> <br/> <br/> ")
                    Tabela1.Append("            _______________________ ")
                    Tabela1.Append("        </td>")
                    Tabela1.Append("        <td class=assinatura><br/> ")
                    Tabela1.Append("            ___/___/___ ___:___:___ ")
                    Tabela1.Append("    <br/> <br/> <br/> <br/> ")
                    Tabela1.Append("            _______________________ ")
                    Tabela1.Append("        </td>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("</table>")
                    Tabela1.Append("<br />")

                    Tabela1.Append(" <table>")
                    Tabela1.Append("    <tr>")
                    Tabela1.Append("    <td colspan=""3"" align=""center"" style=""font-size:14px;"">")

                    'If Val(Rst1Item("PATIO").ToString()) = 3 Then
                    '    Tabela1.Append("Pátio 3 - Endereço: Av. Engenheiro Antônio Alves Freire s/n  - Cais do Saboó")
                    'ElseIf Val(Rst1Item("PATIO").ToString()) = 5 Then
                    '    Tabela1.Append("Pátio 5 - Cais do Saboó s/n")
                    'ElseIf Val(Rst1Item("PATIO").ToString()) = 2 Then
                    '    Tabela1.Append("Pátio 2 - Endereço: Av. Engenheiro Antonio Alves Freire, s/n Cais do Saboó – Santos/SP")
                    'End If

                    Tabela1.Append(EnderecoProtocolo.ObterEnderecoProtocolo(Val(Rst1Item("PATIO").ToString())))

                    Tabela1.Append("    </td>")
                    Tabela1.Append("    </tr>")
                    Tabela1.Append(" </table>")


                End If

                Estrutura.Append(Header.ToString())
                Estrutura.Append(Tabela1.ToString())
                Estrutura.Append(Tabela2.ToString())
                Estrutura.Append("<div class=folha></div>")

                Header.Clear()
                Tabela1.Clear()
                Tabela2.Clear()
                SQL.Clear()

                'AlteraStatusImpressao(Item)

            Next
        Next

        Return Estrutura

    End Function


End Class
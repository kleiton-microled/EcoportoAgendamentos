Imports System.Drawing
Imports System.Drawing.Imaging

Public Class ProtocoloCNTRCarregamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then
                If Convert.ToInt32(Banco.ExecuteScalar("SELECT NVL(AUTONUM_GD_RESERVA,0) AUTONUM_GD_RESERVA FROM TB_AGENDAMENTO_WEB_CNTR_CA WHERE AUTONUM = " & Request.QueryString("protocolo").ToString())) > 0 Then
                    Dim Sb As StringBuilder = GerarProtocolo(Request.QueryString("protocolo").ToString(), "", "1")
                    If Sb IsNot Nothing Then
                        conteudo.InnerHtml = "<center>" & Sb.ToString() & "</center>"
                        Banco.BeginTransaction("UPDATE REDEX.TB_AGENDAMENTO_WEB_CNTR_CA SET STATUS = 'IM' WHERE AUTONUM = " & Request.QueryString("protocolo").ToString())
                    End If
                Else
                    Response.Redirect("ConsultarAgendamentosCNTRCarregamento.aspx?erro=1")
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

            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    AUTONUM_MOTORISTA, ")
            SQL.Append("    AUTONUM_VEICULO, ")
            SQL.Append("    NOME, ")
            SQL.Append("    PLACA_CAVALO, ")
            SQL.Append("    PLACA_CARRETA, ")
            SQL.Append("    NUM_PROTOCOLO, ")
            SQL.Append("    ANO_PROTOCOLO, ")
            SQL.Append("    AUTONUM_TRANSPORTADORA, ")
            SQL.Append("    CNH, ")
            SQL.Append("    VEICULO, ")
            SQL.Append("    NAVIO_VIAGEM, ")
            SQL.Append("    DEAD_LINE, ")
            SQL.Append("    ID_CONTEINER, ")
            SQL.Append("    PROTOCOLO, ")
            SQL.Append("    STATUS, ")
            SQL.Append("    PERIODO, ")
            SQL.Append("    TRANSPORTADORA, ")
            SQL.Append("    MODELO, ")
            SQL.Append("    NEXTEL, ")
            SQL.Append("    RG, ")
            SQL.Append("    PORTO_DESTINO, ")
            SQL.Append("    PORTO_ORIGEM, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    PATIO ")
            SQL.Append("FROM ")
            SQL.Append("   REDEX.VW_AGENDAMENTO_WEB_CN_CA_PROT ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = {0} ")

            Rst1 = Banco.List(String.Format(SQL.ToString(), Item))

            If Rst1 Is Nothing Then
                If Rst1.Rows.Count = 0 Then
                    Return Nothing
                End If
            End If

            ID_Conteiner = Rst1.Rows(0)("ID_CONTEINER").ToString()

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
            Header.Append("		<font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE CONTÊINER (EXPORTAÇÃO)</font>")
            Header.Append("		<br/>")
            Header.Append("        <font face=Arial size=5>Nº: " & Rst1.Rows(0)("NUM_PROTOCOLO").ToString() & "/" & Rst1.Rows(0)("ANO_PROTOCOLO").ToString() & "</font> ")


            If Empresa = "1" Then
                Header.Append("		<br/><br/>Período previsto de chegada no terminal ECOPORTO:<br/>")
            Else
                Header.Append("		<br/><br/>Período previsto de chegada no terminal ECOPORTO ALFANDEGADO:<br/>")
            End If

            Header.Append("        " & Rst1.Rows(0)("PERIODO").ToString())

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
                Tabela1.Append("        <td>" & Rst1.Rows(0)("TRANSPORTADORA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("PLACA_CAVALO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("PLACA_CARRETA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("MODELO").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>MOTORISTA</td>")
                Tabela1.Append("        <td>CNH</td>")
                Tabela1.Append("        <td>RG</td>")
                Tabela1.Append("        <td>NEXTEL</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("NOME").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("CNH").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("RG").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("NEXTEL").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")


                Tabela1.Append("<br/>")
                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Identificação da Reserva</caption>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>RESERVA</td>")
                Tabela1.Append("        <td>DEAD LINE</td>")
                Tabela1.Append("        <td>PORTO DESTINO</td>")
                Tabela1.Append("        <td>EXPORTADOR</td>")
                Tabela1.Append("        <td>NAVIO / VIAGEM</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("RESERVA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("DEAD_LINE").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("PORTO_DESTINO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("EXPORTADOR").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("NAVIO_VIAGEM").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br/>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Contêineres</caption>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>CONTÊINER</td>")                
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ID_CONTEINER").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br/>")


                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td></td>")
                Tabela1.Append("        <td>CHEGADA NO TERMINAL</td>")
                Tabela1.Append("        <td>SAÍDA DO TERMINAL</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("    <td align=""center""><img src=""CodigoBarra.aspx?protocolo=" & Rst1.Rows(0)("NUM_PROTOCOLO").ToString() & "/" & Rst1.Rows(0)("ANO_PROTOCOLO").ToString() & "&modo=E"" /></td> ")
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

                'If Val(Rst1.Rows(0)("PATIO").ToString()) = 3 Then
                '    Tabela1.Append("Pátio 3 - Endereço: Av. Engenheiro Antônio Alves Freire s/n  - Cais do Saboó")
                'ElseIf Val(Rst1.Rows(0)("PATIO").ToString()) = 5 Then
                '    Tabela1.Append("Pátio 5 - Cais do Saboó s/n")
                'ElseIf Val(Rst1.Rows(0)("PATIO").ToString()) = 2 Then
                '    Tabela1.Append("Pátio 2 - Endereço: Av. Engenheiro Antonio Alves Freire, s/n Cais do Saboó – Santos/SP")
                'End If

                Tabela1.Append(EnderecoProtocolo.ObterEnderecoProtocolo(Val(Rst1.Rows(0)("PATIO").ToString())))

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

        Return Estrutura

    End Function


End Class
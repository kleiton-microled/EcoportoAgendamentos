Imports System.Drawing
Imports System.Drawing.Imaging

Public Class ProtocoloCNTR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then
                If Convert.ToInt32(Banco.ExecuteScalar("SELECT NVL(AUTONUM_GD_RESERVA,0) AUTONUM_GD_RESERVA FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & Request.QueryString("protocolo").ToString())) > 0 Then
                    Dim Sb As StringBuilder = GerarProtocolo(Request.QueryString("protocolo").ToString(), "", "1")
                    If Sb IsNot Nothing Then
                        conteudo.InnerHtml = "<center>" & Sb.ToString() & "</center>"
                        Banco.BeginTransaction("UPDATE REDEX.TB_GD_CONTEINER SET STATUS = 'IM' WHERE AUTONUM_GD_CNTR = " & Request.QueryString("protocolo").ToString())
                    End If
                Else
                    Response.Redirect("ConsultarAgendamentosCNTR.aspx?erro=1")
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
            SQL.Append("    VEICULO,")
            SQL.Append("    NAVIO_VIAGEM,")
            SQL.Append("    DEAD_LINE,")
            SQL.Append("    CONTEINER,")
            SQL.Append("    PROTOCOLO,")
            SQL.Append("    STATUS,")
            SQL.Append("    PERIODO,")
            SQL.Append("    AUTONUM_GD_CNTR,")
            SQL.Append("    REFERENCE,")
            SQL.Append("    AUTONUM_GD_MOTORISTA,")
            SQL.Append("    AUTONUM_VEICULO,")
            SQL.Append("    AUTONUMVIAGEM,")
            SQL.Append("    ID_CONTEINER,")
            SQL.Append("    TARA,")
            SQL.Append("    BRUTO,")
            SQL.Append("    LACRE1,")
            SQL.Append("    LACRE2,")
            SQL.Append("    LACRE3,")
            SQL.Append("    LACRE4,")
            SQL.Append("    LACRE5,")
            SQL.Append("    LACRE6,")
            SQL.Append("    LACRE7,")
            SQL.Append("    LACRE_SIF,")
            SQL.Append("    VENTILACAO,")
            SQL.Append("    UMIDADE,")
            SQL.Append("    VOLUMES,")
            SQL.Append("    TEMPERATURA,")
            SQL.Append("    ESCALA,")
            SQL.Append("    ALTURA,")
            SQL.Append("    LATERAL_DIREITA,")
            SQL.Append("    LATERAL_ESQUERDA,")
            SQL.Append("    COMPRIMENTO,")
            SQL.Append("    TAMANHO,")
            SQL.Append("    TIPOBASICO,")
            SQL.Append("    AUTONUM_RESERVA,")
            SQL.Append("    IMO1,")
            SQL.Append("    IMO2,")
            SQL.Append("    IMO3,")
            SQL.Append("    IMO4,")
            SQL.Append("    ONU1,")
            SQL.Append("    ONU2,")
            SQL.Append("    ONU3,")
            SQL.Append("    ONU4,")
            SQL.Append("    NOME,")
            SQL.Append("    PLACA_CAVALO,")
            SQL.Append("    PLACA_CARRETA,")
            SQL.Append("    NUM_PROTOCOLO,")
            SQL.Append("    ANO_PROTOCOLO,")
            SQL.Append("    ID_TRANSPORTADORA,")
            SQL.Append("    TRANSPORTADORA,")
            SQL.Append("    CNH, ")
            SQL.Append("    POD, ")
            SQL.Append("    FDES, ")
            SQL.Append("    MODELO, ")
            SQL.Append("    RG, ")
            SQL.Append("    LINE, ")
            SQL.Append("    NEXTEL, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    COD_ISO, ")
            SQL.Append("    PATIO ")
            SQL.Append("FROM ")
            SQL.Append("   REDEX.VW_AGENDAMENTO_WEB_CN_PROT ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_GD_CNTR = {0} ")

            Rst1 = Banco.List(String.Format(SQL.ToString(), Item))

            If Rst1 IsNot Nothing Then
                If Rst1.Rows.Count > 0 Then

                    ID_Conteiner = Rst1.Rows(0)("CONTEINER").ToString()

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

                End If
            End If

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
                Tabela1.Append("        <td>" & Rst1.Rows(0)("REFERENCE").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("DEAD_LINE").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("POD").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("EXPORTADOR").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("NAVIO_VIAGEM").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br/>")


                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Identificação da Carga</caption>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>CONTÊINER</td>")
                Tabela1.Append("        <td>ISO</td>")
                Tabela1.Append("        <td>PESO BRUTO (Kg)</td>")
                Tabela1.Append("        <td>TARA (Kg)</td>")
                Tabela1.Append("        <td>VOLUMES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ID_CONTEINER").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("COD_ISO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("BRUTO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("TARA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("VOLUMES").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>LACRE ARMADOR 1</td>")
                Tabela1.Append("        <td>LACRE ARMADOR 2</td>")
                Tabela1.Append("        <td>LACRE EXPORTADOR</td>")
                Tabela1.Append("        <td>LACRE SIF</td>")
                Tabela1.Append("        <td>OUTROS LACRES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LACRE1").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LACRE2").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LACRE7").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LACRE_SIF").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LACRE3").ToString() & "/" & Rst1.Rows(0)("LACRE4").ToString() & "/" & Rst1.Rows(0)("LACRE5").ToString() & "/" & Rst1.Rows(0)("LACRE6").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<br/>")
                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Carga Perigosa / Refrigeração / Excessos (cm) </caption>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>IMO1</td>")
                Tabela1.Append("        <td>IMO2</td>")
                Tabela1.Append("        <td>IMO3</td>")
                Tabela1.Append("        <td>IMO4</td>")
                Tabela1.Append("        <td>TEMPERATURA</td>")
                Tabela1.Append("        <td>ESCALA</td>")
                Tabela1.Append("        <td>ALTURA</td>")
                Tabela1.Append("        <td>LATERAL DIREITA</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("IMO1").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("IMO2").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("IMO3").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("IMO4").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("TEMPERATURA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ESCALA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ALTURA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LATERAL_DIREITA").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=#B3C63C>")
                Tabela1.Append("        <td>ONU1</td>")
                Tabela1.Append("        <td>ONU2</td>")
                Tabela1.Append("        <td>ONU3</td>")
                Tabela1.Append("        <td>ONU4</td>")
                Tabela1.Append("        <td>UMIDADE</td>")
                Tabela1.Append("        <td>VENTILAÇÃO</td>")
                Tabela1.Append("        <td>LATERAL ESQUERDA</td>")
                Tabela1.Append("        <td>COMPRIMENTO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ONU1").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ONU2").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ONU3").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("ONU4").ToString() & "</td>")
                Tabela1.Append("        <td>" & Umidade & "</td>")
                Tabela1.Append("        <td>" & Ventilacao & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("LATERAL_ESQUERDA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Rows(0)("COMPRIMENTO").ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")


                SQL.Clear()
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.AUTONUM_AGENDAMENTO, ")
                SQL.Append("    A.DANFE, ")
                SQL.Append("    A.NUMERO, ")
                SQL.Append("    A.SERIE, ")
                SQL.Append("    A.EMISSOR, ")
                SQL.Append("    A.EMISSAO, ")
                SQL.Append("    A.VALOR, ")
                SQL.Append("    A.PESO_BRUTO, ")
                SQL.Append("    B.REFERENCE ")
                SQL.Append("FROM ")
                SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CNTR_NF A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    REDEX.TB_GD_CONTEINER B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM_GD_CNTR ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM_AGENDAMENTO = " & Rst1.Rows(0)("AUTONUM_GD_CNTR").ToString())

                Rst2 = Banco.List(SQL.ToString())

                If Not Rst2.Rows.Count = 0 Then
                    Tabela1.Append("<td style=""font-family:Arial;font-size:11px;font-weight: bold;padding-top:8px;text-align:left;border:0px;margin:10px;""><tr><td>Notas Fiscais do Contêiner: " & Rst1.Rows(0)("CONTEINER").ToString() & "</td>")
                End If

                For Each Linha As DataRow In Rst2.Rows

                    Tabela1.Append("<table>")
                    Tabela1.Append("    <thead bgcolor=#B3C63C>")
                    Tabela1.Append("        <td>Reserva</td>")
                    Tabela1.Append("        <td>DANFE</td>")
                    Tabela1.Append("        <td>Número</td>")
                    Tabela1.Append("        <td>Série</td>")
                    Tabela1.Append("        <td>Emissão</td>")
                    Tabela1.Append("        <td>Emissor</td>")
                    Tabela1.Append("        <td>Valor</td>")
                    Tabela1.Append("        <td>Peso Bruto</td>")
                    Tabela1.Append("    </thead>")

                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Linha("REFERENCE").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("DANFE").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("NUMERO").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("SERIE").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("EMISSAO").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("EMISSOR").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("VALOR").ToString() & "</td>")
                    Tabela1.Append("        <td>" & Linha("PESO_BRUTO").ToString() & "</td>")

                    Tabela1.Append("</table>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("</table>")
                    Tabela1.Append("</table>")
                    Tabela1.Append("<br />")

                Next

                Tabela1.Append("<br />")


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
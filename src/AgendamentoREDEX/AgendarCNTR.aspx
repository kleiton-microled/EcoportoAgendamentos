<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AgendarCNTR.aspx.vb" Inherits="AgendamentoREDEX.AgendarCNTR" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        $(function () {
            var activeIndex = parseInt($('#<%=AccordionIndex.ClientID %>').val());

            $("#accordion").accordion({
                heightStyle: 'panel',
                navigation: true,
                active: activeIndex,
                change: function (event, ui) {
                    var index = $(this).children('h3').index(ui.newHeader);
                    $('#<%=AccordionIndex.ClientID %>').val(index);
                }
            });

        });
    </script>

    <center>
        <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
            bgcolor="#F8FFDE" width="1000px">
            <tr>
                <td colspan="2" style="border-style: solid; border-width: 1px; border-color: Black;
                    color: White; font-family: tahoma; font-size: 13px; font-weight: bold;" 
                    bgcolor="#B3C63C">
                    Consulta de Reserva
                </td>
                <td style="padding: 0px; margin: 0px; border-color: Black; color: White; font-family: tahoma;
                    font-size: 13px; font-weight: bold; border-left-style: solid; border-left-width: 1px;
                    padding-left: 4px;" align="left" rowspan="2" colspan="2">
                    <asp:Button ID="btnSalvar" runat="server" Height="44px" Text="Salvar" Width="78px"
                        CssClass="Botao" Visible="false" />
                    <asp:Button ID="btnCancelar" runat="server" Height="44px" Text="Excluir" Width="78px"
                        CssClass="Botao" OnClientClick="return confirm('Confirma a exclusão do Agendamento?');"
                        Visible="false" />
                    <asp:Button ID="btnSair" runat="server" Height="44px" Text="Sair" Width="78px" CssClass="Botao" />
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px">
                    Informe a Reserva:
                </td>
                <td style="border-style: solid; border-width: 1px; width: 406px;">
                    <asp:TextBox ID="txtReserva" runat="server" AutoPostBack="True" Width="99%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div id="accordion" style="width: 1000px; text-align: left; display: none;">
            <h3>
                1. Informações da Reserva</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Informações da Reserva
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Navio:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblNavio" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Viagem:
                        </td>
                        <td style="border-style: solid; border-width: 1px" align="left">
                            <asp:Label ID="lblViagem" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Exportador:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblExportador" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Dead Line:
                        </td>
                        <td style="border-style: solid; border-width: 1px" align="left">
                            <asp:Label ID="lblDeadLine" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            NVOCC:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblNVOCC" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            POD:
                        </td>
                        <td align="left" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblPortoDescarga" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Informações da Carga
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Tipo:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTipo" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Total:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTotal" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Status:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblStatus" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            M3:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblM3" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Utilizados:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblUtilizados" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Peso Bruto:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblPesoBruto" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Disponíveis:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblDisponiveis" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Volumes:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblVolumes" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                2. Motorista / Veículo</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Motorista / Veículo
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Nome Motorista / CNH:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="txtMotorista" runat="server" Width="300px" AutoPostBack="True" Font-Size="11px"></asp:TextBox>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Cavalo:
                        </td>
                        <td style="border-style: solid; border-width: 1px; width: 90px;">
                            <asp:DropDownList ID="cbCavalo" runat="server" Width="90px" DataTextField="PLACA_CAVALO"
                                DataValueField="PLACA_CAVALO" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Transportadora:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTransportadora" runat="server"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Carreta:
                        </td>
                        <td style="border-style: solid; border-width: 1px; width: 90px;">
                            <asp:DropDownList ID="cbCarreta" runat="server" Width="90px" DataTextField="PLACA_CARRETA"
                                DataValueField="PLACA_CARRETA">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                3. Informações do Contêiner</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4">
                            <table border="1" style="font-family: tahoma; font-size: 11px; outline-width: 0;
                                outline-style: none; outline-color: invert;" width="100%">
                                <tr>
                                    <td bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        Contêiner
                                    </td>
                                    <td colspan="2" bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        ONU
                                    </td>
                                    <td bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        IMO
                                    </td>
                                    <td bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        Refrigeração
                                    </td>
                                    <td bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        Excessos CM
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White" rowspan="3" valign="top">
                                        <table cellpadding="2" cellspacing="2" class="style2" style="outline: 0;">
                                            <tr>
                                                <td width="58px">
                                                    Contêiner:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtConteiner" runat="server" Width="96px" Enabled="False" AutoPostBack="true"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="txtConteiner_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="AAAA999999-9" TargetControlID="txtConteiner">
                </asp:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Volumes:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVolumesCntr" runat="server" Width="96px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Tamanho:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cbTamanho" runat="server" Width="96px" DataTextField="PLACA_CAVALO"
                                                        DataValueField="PLACA_CAVALO" Enabled="False">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Tipo:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cbTipo" runat="server" Width="96px" DataTextField="CODIGO"
                                                        DataValueField="CODIGO" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Tara:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTara" runat="server" Width="96px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Peso Bruto:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPesoBrutoCntr" runat="server" Width="96px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td bgcolor="White" align="center" valign="middle">
                                        <table border="0" cellpadding="2" cellspacing="2" width="80px" style="outline: 0;">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtUn1" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUn3" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtUn2" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUn4" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2" bgcolor="White" align="center" valign="middle">
                                        <table border="0" cellpadding="2" cellspacing="2" width="80px" style="outline: 0;">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtIMO1" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIMO3" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtIMO2" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIMO4" runat="server" Width="30px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle" bgcolor="White" align="center">
                                        <table border="0" cellpadding="2" cellspacing="2" style="outline: 0;">
                                            <tr>
                                                <td align="right" width="40px">
                                                    Temp:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTemp" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Escala:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEscala" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="40px">
                                                    Umid.:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUmidade" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Vent:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVent" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle" bgcolor="White" align="center">
                                        <table border="0" cellpadding="2" cellspacing="2" style="outline: 0;">
                                            <tr>
                                                <td align="right">
                                                    Compr.:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtComprimento" runat="server" Width="42px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Altura:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAltura" runat="server" Width="42px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Lat Esq:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLatEsq" runat="server" Width="42px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Lat.Dir:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLatDir" runat="server" Width="42px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#B3C63C" colspan="5" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                        color: #FFFFFF">
                                        Lacres
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White" align="center" colspan="3" valign="middle">
                                        <table cellpadding="2" cellspacing="2" style="outline: 0;">
                                            <tr>
                                                <td width="58px">
                                                    Armador1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre1" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Armador2:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre2" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle" bgcolor="White" align="center">
                                        <table cellpadding="2" cellspacing="2" style="outline: 0;">
                                            <tr>
                                                <td>
                                                    Outros 1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre3" runat="server" Width="28px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Outros 3:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre5" runat="server" Width="28px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Outros 2:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre4" runat="server" Width="28px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Outros 4:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre6" runat="server" Width="28px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle" bgcolor="White" align="center">
                                        <table border="0" cellpadding="2" cellspacing="2" style="outline: 0;">
                                            <tr>
                                                <td align="right" width="70px">
                                                    Exportador:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLacre7" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="70px">
                                                    SIF:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLacre8" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                4. Informações das Notas Fiscais</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                     <tr>
                        <td colspan="4">
                            <asp:Label ID="msgErroNF" runat="server" ForeColor="Red" Visible="false" Font-Size="14px" Font-Bold="true"
                                Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMsgNF" runat="server" Text="As Notas Fiscais só poderão ser vinculadas após a conclusão do Agendamento. Clique no botão Salvar para continuar."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Notas Fiscais
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-style: solid; border-width: 1px">
                            <table cellpadding="2" cellspacing="2" width="950px">

                                <tr>
                                    <td colspan="6">Arquivo Xml Danfe</td>
                                </tr>
                                
                                 <tr>
                                    <td colspan="6">
                                        <asp:Label ID="msgSucessoDanfe" runat="server" ForeColor="#00CC66" Visible="False" Font-Size="14px" Font-Bold="True"></asp:Label>
                                     </td>                                    
                                </tr>

                                 <tr>
                                    <td colspan="6">
                                        <asp:FileUpload ID="txtArquivoDanfe" runat="server"></asp:FileUpload>
                                         <asp:Button ID="btnUploadNota" runat="server" Text="Upload" />
                                        <asp:TextBox ID="txtXml" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                    </td>                                    
                                </tr>

                                <tr bgcolor="White">
                                     <td colspan="6">
                                        DANFE:
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="6">
                                         <asp:TextBox ID="txtDANFE" runat="server" Enabled="False" Width="99%" MaxLength="44"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr bgcolor="White">
                                    <%--<td >
                                        DANFE:
                                    </td>--%>
                                    <td>
                                        Número:
                                    </td>
                                    <td>
                                        Série:
                                    </td>
                                    <td colspan="2">
                                        Emissor:
                                    </td>
                                </tr>
                                <tr bgcolor="White">                                   
                                   
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSerie" runat="server"  Enabled="False"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtEmissor" runat="server" Width="99%"  Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td>
                                        Emissão:
                                    </td>                                    
                                    <td>
                                        Valor:
                                    </td>
                                    <td>
                                        Peso Bruto:
                                    </td>
                                    
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td>
                                        <asp:TextBox ID="txtEmissao" runat="server" Enabled="False" CssClass="calendario"></asp:TextBox>
                                    </td>                                    
                                    <td>
                                        <asp:TextBox ID="txtValor" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPesoBruto" runat="server"  Enabled="False"></asp:TextBox>
                                    </td>
                                    
                                    <td>
                                        <asp:Button ID="btnSalvarNF" runat="server" Text="Salvar / Alterar" Enabled="False" />
                                        <asp:Button ID="btnCancelarNF" runat="server" Text="Cancelar" Enabled="False" />
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td colspan="6">
                                        <asp:GridView ID="dgConsultaNF" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            EmptyDataText="Nenhuma NF encontrada." ForeColor="#333333" ShowHeaderWhenEmpty="True"
                                            Width="100%" Font-Size="11px" DataKeyNames="AUTONUM">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField HeaderText="DANFE" DataField="DANFE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Número" DataField="NUMERO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Série" DataField="SERIE">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Emissor" DataField="EMISSOR" />
                                                <asp:BoundField HeaderText="Emissão" DataField="EMISSAO">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField HeaderText="Valor" DataField="VALOR" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Peso Bruto" DataField="PESO_BRUTO" DataFormatString="{0:N}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField> 
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink 
                                                            ID="HyperLink1" 
                                                            runat="server" 
                                                            Target="_blank"
                                                            Visible='<%# IIf(Eval("ARQUIVO_DANFE") <> "X", True, False)  %>'
                                                            NavigateUrl='<%# "~/VisualizaDanfe.aspx?id=" & Eval("AUTONUM") & "&tipo=2" %>'>

                                                            <img src='<%= Page.ResolveUrl("~/imagens/nfe.jpg") %>' width="50px" height="50px"/>
                                                        </asp:HyperLink>
                                                         </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDITAR"
                                                            CommandArgument="<%# Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Confirma a exclusão da NF?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#B3C63C" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                5. Escolha do Período</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnGrid" runat="server" Height="150px" ScrollBars="Vertical" BorderStyle="None">
                                <asp:GridView ID="dgConsultaPeriodos" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="None" EmptyDataText="Nenhum período encontrado." ForeColor="#333333"
                                    ShowHeaderWhenEmpty="True" Width="100%" Font-Size="11px" DataKeyNames="AUTONUM_GD_RESERVA">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField HeaderText="Período Inicial" DataField="PERIODO_INICIAL">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Período Final" DataField="PERIODO_FINAL">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Saldo" DataField="LIMITE_CAMINHOES">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="cmdSelecionar" ImageUrl="~/imagens/check.png"
                                                    CommandName="SEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#B3C63C" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>     
            
             
        </div>
       
         <div id="pnlAlertaCTE" style="width:1000px; background-color:coral; color: white; height:20px;line-height:20px;display: none;">
            <p>Atenção: É necessário apresentar 03 vias do CTE e Danfe no Gate!</p>
        </div>

    </center>
    <asp:Label ID="lblCodigoAgendamento" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoBooking" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoMotorista" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoVeiculo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoProtocolo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoNF" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoViagem" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPatio" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="AccordionIndex" runat="server" Value="0" />
</asp:Content>

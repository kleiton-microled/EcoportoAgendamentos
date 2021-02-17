<%@ Page Title="" Language="vb" Debug="true" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="CadastrarConteineresExportacao.aspx.vb" Inherits="GPD.CadastrarConteineresExportacao"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        *
        {
            padding: 0px;
            margin: 0px;
        }
        
        .Upper
        {
            text-transform:uppercase;
        }
        
        .Numeric
        {
            text-align:right;
        }
        
        </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">


    <center>

    <table align="center" cellspacing="4" 
        style="border-style: solid; border-width: 1px; border-color: #999999" 
        bgcolor="White">
        <tr>
            <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" colspan="4" height="18px" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                &nbsp;Cadastro de Contêineres</td>
        </tr>
        <tr>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" 
                            width="250px">
                            &nbsp;Reserva 
                            / Booking</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        Reserva (Informe e pressione TAB):</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:TextBox ID="txtReserva" runat="server" Width="98%" AutoPostBack="True" 
                                            CssClass="Upper AlphaNumeric"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" 
                            width="250px">
                            &nbsp;Tipo / Tamanho</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        Tam.:</td>
                                    <td align="left">
                                        Tipo:</td>
                                    <td align="left">
                                        Remarks:</td>
                                    <td align="center">
                                        EF:</td>
                                </tr>
                                <tr>
                                    <td align="left" width="40px">
                                        <asp:DropDownList ID="cbTamanho" runat="server" AutoPostBack="True" 
                                            Font-Names="Verdana" Font-Size="12px" Width="40px" Height="20px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cbTipo" runat="server" 
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="20px" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" width="60px">
                                        <asp:DropDownList ID="cbRemarks" runat="server" AutoPostBack="True" 
                                            Font-Names="Verdana" Font-Size="12px" Width="80px" Height="20px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="20px">
                                        <asp:Label ID="lblEF" runat="server" Font-Bold="True">N/A</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    cellpadding="0" cellspacing="1"
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Sigla Contêiner</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        Sigla:</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtSiglaConteiner" runat="server" Width="110px" 
                                            CssClass="Container"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    cellpadding="0" cellspacing="1"
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Transporte</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left" width="80px">
                                        Tipo:</td>
                                    <asp:Panel ID="PanelVagao1" runat="server" Visible="False">                                   
                                        <td align="left" width="80px">Vagão</td>
                                    </asp:Panel>
                                    </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList ID="cbTipoTransporte" runat="server" AutoPostBack="True" 
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="20px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="R">Rodoviário</asp:ListItem>
                                            <asp:ListItem Value="F">Ferroviário</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                     <asp:Panel ID="PanelVagao2" runat="server" Visible="False">   
                                        <td align="center">
                                            <asp:TextBox ID="txtVagao" runat="server" Width="98%"></asp:TextBox>
                                        </td>
                                    </asp:Panel>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table bgcolor="GhostWhite" 
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%" cellpadding="0" cellspacing="1">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Informações</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" bgcolor="#CCCCCC" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="center" bgcolor="GhostWhite">
                                        Porto de Descarga:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        Navio / Viagem:</td>
                                    <td align="center" bgcolor="GhostWhite" width="128px">
                                        Data Dead Line:</td>
                                    <td align="center" bgcolor="GhostWhite" width="128px">
                                        Porto Destino:</td>
                                    <td align="center" bgcolor="GhostWhite" width="74px">
                                        Tara</td>
                                    <td align="center" bgcolor="GhostWhite" width="140px">
                                        P. Bruto (Tara+Carga):</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        Volumes:</td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:Label ID="lblPortoDescarga" runat="server" Font-Bold="True">N/A</asp:Label>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:Label ID="lblNavioViagem" runat="server" Font-Bold="True">N/A</asp:Label>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:Label ID="lblDataDeadLine" runat="server" Font-Bold="True">N/A</asp:Label>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:Label ID="lblPortoDestino" runat="server" Font-Bold="True">N/A</asp:Label>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:TextBox ID="txtTara" runat="server" Width="65px" CssClass="Numeric" 
                                            MaxLength="22" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:TextBox ID="txtPesoBruto" runat="server" Width="130px" CssClass="Numeric" 
                                            MaxLength="22" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite" height="22px">
                                        <asp:TextBox ID="txtVolumes" runat="server" Width="50px" CssClass="Numeric" 
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                          &nbsp;Carga Perigosa ONU</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        ONU1:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOnu1" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        ONU2:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOnu2" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        ONU3:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOnu3" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        ONU4:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOnu4" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Carga Perigosa IMO</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite">
                                        IMO1:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtIMO1" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        IMO2:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtIMO2" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="GhostWhite">
                                        IMO3:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtIMO3" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        IMO4:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtIMO4" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2">
                <table bgcolor="GhostWhite" 
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%" cellpadding="0" cellspacing="1">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                          &nbsp;Refrigeração</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite">
                                        Temperatura:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtTemperatura" runat="server" Width="50px" Enabled="False" 
                                            CssClass="Moeda" MaxLength="5"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Escala:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtEscala" runat="server" Width="50px" Enabled="False" 
                                            MaxLength="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="GhostWhite">
                                        Umidade:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtUmidade" runat="server" Width="50px" Enabled="False" 
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Ventilação:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtVentilacao" runat="server" Width="50px" Enabled="False" 
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Excessos CM</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        Altura:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtAltura" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Comp.:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtComp" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        Lat. Dir:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtLatDir" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Lat. Esq.:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtLatEsq" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Lacres Armador</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="center" bgcolor="GhostWhite" height="20px">
                                        Lacre Armador 1:</td>
                                    <td align="center" bgcolor="GhostWhite">
                                        Lacre 
                                        Armador 2:</td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtLacreArmador1" runat="server" Width="95%" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtLacreArmador2" runat="server" Width="95%" MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%" cellpadding="0" cellspacing="1">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Lacre Exportador</td>
                    </tr>
                    <tr>
                        <td bgcolor="GhostWhite">
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="center" bgcolor="GhostWhite" height="20px">
                                        Lacre:</td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtLacreExportador" runat="server" Width="110px" 
                                            MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table bgcolor="GhostWhite" 
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%" cellpadding="0" cellspacing="1">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Lacre SIF</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="center" bgcolor="GhostWhite" height="20px">
                                        Lacre:</td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="GhostWhite" width="100%">
                                        <asp:TextBox ID="txtLacreSIF" runat="server" Width="98%" MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" 
                            width="250px">
                            &nbsp;Outros Lacres</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        Lacre 1:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOutrosLacres1" runat="server" Width="60px" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Lacre 2:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOutrosLacres2" runat="server" Width="60px" MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" width="60px">
                                        Lacre 3:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOutrosLacres3" runat="server" Width="60px" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="GhostWhite">
                                        Lacre 4:</td>
                                    <td align="center" bgcolor="GhostWhite" width="50px">
                                        <asp:TextBox ID="txtOutrosLacres4" runat="server" Width="60px" MaxLength="15"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="3">
                <table bgcolor="GhostWhite" 
                    
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" 
                            width="250px">
                            &nbsp;Exportador</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td align="left" bgcolor="GhostWhite" height="20px">
                                        Razão Social:</td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="GhostWhite">
                                        <asp:TextBox ID="txtExportador" runat="server" Width="99%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <table bgcolor="GhostWhite" 
                    style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;" 
                    width="100%" cellpadding="0" cellspacing="1">
                    <tr>
                        <td align="left" bgcolor="<%= Session("SIS_COR_PADRAO") %>" 
                            style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                            &nbsp;Observações</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtObs" runat="server" Rows="6" 
                                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" 
                    Height="18px" Text="Retornar" Width="80px" />
                <asp:Button ID="btNovo" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" 
                    Height="18px" Text="Novo" Width="80px" />
                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" 
                    Height="18px" 
                    onclientclick="return confirm('Confirma a exclusão do Contêiner?');" 
                    Text="Excluir" Width="80px" />
                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" 
                    Height="18px" 
                    Text="Salvar" Width="80px" />
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblFlagLate" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTamanhoCntrSel" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>        
    </table>

    </center>

    </form>
</asp:Content>

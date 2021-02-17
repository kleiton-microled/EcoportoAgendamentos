<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="CadastrarAgendamentos.aspx.vb" Inherits="AgendamentoCargaSoltaDDC.CadastrarAgendamentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        * {
            padding: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }

        .QtdItens {
            text-align: center;
        }

        .PromptCSS {
            color: black;
            font-weight: bold;
            background-color: #EEE8AA;
            font-family: Arial;
            font-size: 10pt;
            border: solid 1px black;
        }
    </style>

    <script type="text/javascript">
        javascript: window.history.forward(1);

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelForm" runat="server" ScrollBars="Vertical"
        Height="100%">
        <form id="form2" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <center>
                <table align="center">
                    <tr>
                        <td valign="top">
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Informações Veículo / Motorista
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                            <tr>
                                                <td width="110px" align="left">Nome Motorista:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomeMotorista" runat="server" Width="266px" AutoPostBack="True"
                                                        Font-Names="Verdana" Font-Size="12px" Enabled="False" Style="height: 18px"></asp:TextBox>
                                                </td>
                                                <td align="right">CNH:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="cbCNH" runat="server" Width="100%" Font-Names="Verdana" Font-Size="12px"
                                                        AutoPostBack="True" Enabled="False" Style="height: 18px">
                                                    </asp:DropDownList>
                                                    <asp:ListSearchExtender ID="cbCNH_ListSearchExtender" runat="server"
                                                        Enabled="True" PromptText="Digite" TargetControlID="cbCNH"
                                                        PromptCssClass="PromptCSS">
                                                    </asp:ListSearchExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="110px">Placa Cavalo:
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" style="font-family: verdana; font-size: 11px">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="cbCavalo" runat="server" Width="90px" Enabled="False" Font-Names="Verdana"
                                                                    Font-Size="12px" AutoPostBack="True" Style="height: 18px">
                                                                </asp:DropDownList>
                                                                <asp:ListSearchExtender ID="cbCavalo_ListSearchExtender" runat="server"
                                                                    Enabled="true" PromptText="Digite" TargetControlID="cbCavalo"
                                                                    PromptCssClass="PromptCSS">
                                                                </asp:ListSearchExtender>
                                                            </td>
                                                            <td align="right" width="90px">Placa Carreta:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cbCarreta" runat="server" Width="90px" Enabled="False" Font-Names="Verdana"
                                                                    Font-Size="12px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:ListSearchExtender ID="cbCarreta_ListSearchExtender" runat="server"
                                                                    Enabled="True" PromptText="Digite" TargetControlID="cbCarreta"
                                                                    PromptCssClass="PromptCSS">
                                                                </asp:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">Tara:
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" style="font-family: verdana; font-size: 11px"
                                                        width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtTara" runat="server" Width="40px" Enabled="False"
                                                                    Font-Names="Verdana" Font-Size="12px" BackColor="White"></asp:TextBox>
                                                            </td>
                                                            <td align="right">Chassi:
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtChassi" runat="server" Width="74px" CssClass="AlphaNumeric" Enabled="False"
                                                                    Font-Names="Verdana" Font-Size="12px" BackColor="White"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">&nbsp;</td>
                                                <td align="left">
                                                    <asp:CheckBox ID="CheckAutonomo" runat="server" Width="264px" Enabled="false" AutoPostBack="True" Text="Autônomo" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" align="left">
                            <table bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                width="350px">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Transportadora
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="left" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left" width="80px">Razão Social:
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTransportadora" runat="server" Width="99%" CssClass="Numeric"
                                                                    Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left" width="80px" height="22px">&nbsp;</td>
                                                            <td align="left">&nbsp;</td>
                                                            <td align="right">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="80px">Nº Protocolo: </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblProtocolo" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px">Não Gerado</asp:Label>
                                                                <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px" Visible="False"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btNovo" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Novo" Width="60px" />
                                                                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Salvar" Width="60px" />
                                                                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="10px" Height="18px" OnClientClick="return confirm('Confirma a exclusão do agendamento?');" Text="Excluir" Width="60px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Informações Documentação</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                            <tr>
                                                <td width="110px" align="left">Tipo Doc. Saída:</td>
                                                <td align="left" width="220px">
                                                    <asp:DropDownList ID="cbTipoDocSaida" runat="server" Width="100%" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="CR">Conhecimento Rodoviário (CR)</asp:ListItem>
                                                        <asp:ListItem Value="MN">Minuta (MN)</asp:ListItem>
                                                        <asp:ListItem Value="OC">Ordem de Coleta (OC)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">Série Doc. Saída:</td>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSerieDocSaida" runat="server" Width="100%" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" MaxLength="15"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" align="left">Nro. Doc. Saída:</td>
                                                <td align="left" width="220px">
                                                    <asp:TextBox ID="txtNrDocSaida" runat="server" Width="100%" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" CssClass="Numeric"></asp:TextBox>
                                                </td>
                                                <td align="right">Emissão Doc. Saída:
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox ID="txtEmissaoDocSaida" runat="server" Width="100%"
                                                        CssClass="Calendario" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="110px" align="left">Contêiner:
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="cbConteiner" runat="server" Width="100%" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" DataTextField="ID_CONTEINER"
                                                        DataValueField="AUTONUM" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="2" valign="top">
                            <table bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                width="350px">
                                <tr>
                                    <td align="center" bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                                        <table cellspacing="0" width="100%" align="center">
                                            <tr>
                                                <td align="center" width="150px">Período Inicial
                                                </td>
                                                <td align="center" width="150px">Período Final
                                                </td>
                                                <td align="center" width="50px">Saldo
                                                </td>
                                                <td width="50px">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" style="height: auto;">
                                        <asp:Panel ID="PanelScrollPeriodos" runat="server" ScrollBars="Vertical"
                                            Height="330px">
                                            <asp:GridView ID="dgPeriodos" runat="server"
                                                AutoGenerateColumns="False" CellPadding="0" CellSpacing="1"
                                                DataKeyNames="AUTONUM_GD_RESERVA,PERIODO_INICIAL,PERIODO_FINAL,SALDO,FLAG_DTA"
                                                EmptyDataText="Nenhum período disponível." Font-Names="Verdana"
                                                Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                Width="310px">
                                                <AlternatingRowStyle BackColor="White"
                                                    ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="AUTONUM_GD_RESERVA"
                                                        HeaderText="AUTONUM" Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODO_INICIAL"
                                                        HeaderText="PERÍODO INICIAL">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODO_FINAL"
                                                        HeaderText="PERÍODO FINAL">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SALDO" HeaderText="SALDO">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FLAG_DTA"
                                                        HeaderText="FLAG_DTA" Visible="False" />
                                                    <asp:ButtonField ButtonType="Image"
                                                        ImageUrl="~/imagens/unchecked.png" Text="Button">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <EmptyDataRowStyle BackColor="GhostWhite"
                                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True"
                                                    ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True"
                                                    ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White"
                                                    HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"
                                                    ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </asp:Panel>

                                        <asp:Label ID="lblPeriodo" runat="server" Text="Nenhum período foi selecionado."
                                            Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                        <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Nota Fiscal
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                            <tr>
                                                <td align="right">Nota Fiscal:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNotaFiscal" runat="server" Width="80px" CssClass="AlphaNumeric"
                                                        Font-Names="Verdana" Font-Size="12px" Enabled="False" MaxLength="10"></asp:TextBox>
                                                </td>
                                                <td align="right">Série:
                                                </td>
                                                <td align="right" width="120">
                                                    <asp:TextBox ID="txtSerie" runat="server" Width="120px" CssClass="AlphaNumeric" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" MaxLength="4"></asp:TextBox>
                                                </td>
                                                <td align="right" height="22px">&nbsp;Emissão:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDataEmissao" runat="server" Width="80px"
                                                        CssClass="Calendario" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" MaxLength="10"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">Carga Solta:
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="cbItensCS" runat="server" AutoPostBack="True" Enabled="False" Font-Names="Verdana" Font-Size="11px" Width="100%" DataTextField="DISPLAY" DataValueField="AUTONUM">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" height="22px">Quantidade:</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtQtde" runat="server" BackColor="White" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <table cellpadding="1" cellspacing="1">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnAdicProduto" runat="server" Text="Adicionar Nota Fiscal" BorderColor="#999999"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9px"
                                                                    Width="115px" Height="18px" CommandName="ADICIONAR" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:Button ID="btnExcluirProduto" runat="server" Text="Excluir Notas Fiscais" BorderColor="#999999"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9px"
                                                                    Width="115px" Height="18px" Visible="false" OnClientClick="return confirm('Deseja excluir as Notas Fiscais deste agendamento?');" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <asp:Panel ID="PanelPatioInvalido" runat="server" Visible="false">
                                                <tr>
                                                    <td align="left" colspan="7">
                                                        <asp:Label ID="lblPatioInvalido" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </asp:Panel>


                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:Panel ID="PanelScrollNotas" runat="server" ScrollBars="Vertical" Height="164px"
                                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgNotas" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            CellSpacing="1" EmptyDataText="Nenhuma Nota Fiscal com Carga Solta foram vinculados." Font-Names="Verdana"
                                                            Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            Width="100%" DataKeyNames="AUTONUM,AUTONUM_PRODUTO,LOTE,NOTAFISCAL,TIPO,SERIE,EMISSAO,QTDE"
                                                            BackColor="#999999" BorderColor="GhostWhite"
                                                            BorderStyle="Solid">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="AUTONUMAGENDAMENTO" HeaderText="AUTONUMAGENDAMENTO"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False"></asp:BoundField>
                                                                <asp:BoundField DataField="NOTAFISCAL" HeaderText="NOTA FISCAL">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SERIE" HeaderText="SÉRIE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMISSAO" HeaderText="EMISSÃO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TIPO" HeaderText="DOCUMENTO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LOTE" HeaderText="LOTE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PATIO" HeaderText="PATIO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PRODUTO" HeaderText="PRODUTO" HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMBALAGEM" HeaderText="EMBALAGEM" HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QTDE" HeaderText="QTDE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AUTONUM_PRODUTO" HeaderText="AUTONUM_PRODUTO"
                                                                    HtmlEncode="False" Visible="False" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif"
                                                                            CommandName="EDITAR" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <EmptyDataRowStyle BackColor="GhostWhite" HorizontalAlign="Center"
                                                                VerticalAlign="Middle" />
                                                            <FooterStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
                                                            <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                            <div style="text-align: left">
                                <asp:Panel ID="pnLegenda" runat="server" Visible="False">
                                    <asp:Label ID="Label2" runat="server" Text="CARGA DISPONÍVEL NO LOTE/BL:" Font-Size="10px"
                                        Font-Bold="True" Font-Names="Verdana"></asp:Label>
                                    <asp:Label ID="lblConteinerSelecionado" runat="server" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="11px" ForeColor="#CC0000"></asp:Label>
                                    <asp:Label ID="lblPatio" runat="server" Visible="False"></asp:Label>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>

                </table>
            </center>
        </form>
    </asp:Panel>
</asp:Content>

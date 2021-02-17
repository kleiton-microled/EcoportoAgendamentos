<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="CadastrarAgendamentos.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.CadastrarAgendamentos" %>

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

        .pnl-erro {
            padding: 6px;
            margin-bottom: 4px;
            width: 1080px;
            background-color: #F08080;
            color: white;
            font-weight: bold;
            border: 1px solid black;
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

                <asp:Panel ID="pnlMsgErro" runat="server" Visible="false" CssClass="pnl-erro">
                    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
                </asp:Panel>

                <table align="center" style="width: 1100px;">
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
                                                <td width="110px" align="right">Nome Motorista:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNomeMotorista" runat="server" Width="100%" AutoPostBack="True"
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
                                                <td align="right" width="110px">Placa Cavalo:
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" style="font-family: verdana; font-size: 11px" width="100%">
                                                        <tr>
                                                            <td align="left">
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
                                                            <td align="right">
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
                                                            <td align="right" width="80px">Razão Social:
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTransportadora" runat="server" Width="98%" CssClass="Numeric"
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
                                                            <td align="right" width="80px" height="22px">Período:</td>
                                                            <td align="right" colspan="2">&nbsp;
                                                                <asp:Label ID="lblPeriodo" runat="server" Font-Bold="True" ForeColor="Blue" Text="Nenhum período foi selecionado." Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="80px">Nº Protocolo: </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProtocolo" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px">Não Gerado</asp:Label>
                                                                <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px" Visible="False"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btNovo" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"  Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Novo" Width="50px" />
                                                                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Salvar" Width="50px" />
                                                                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="10px" Height="18px" OnClientClick="return confirm('Confirma a exclusão do agendamento?');" Text="Excluir" Width="50px" />
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
                            
                            <table bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px" width="100%">
                                <tr>
                                    <td align="left" bgcolor='<%= Session("SIS_COR_PADRAO") %>' style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">Informações Documentação </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                            <tr>
                                                <td align="right" width="110px">Tipo Doc. Saída:</td>
                                                <td align="left">
                                                    <asp:DropDownList ID="cbTipoDocSaida" runat="server" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="220px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="CR">Conhecimento Rodoviário (CR)</asp:ListItem>
                                                        <asp:ListItem Value="MN">Minuta (MN)</asp:ListItem>
                                                        <asp:ListItem Value="OC">Ordem de Coleta (OC)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" height="22px">Série Doc. Saída:</td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtSerieDocSaida" runat="server" Enabled="False" Font-Names="Verdana" Font-Size="12px" MaxLength="15" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="110px">Nro. Doc. Saída:</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNrDocSaida" runat="server" CssClass="Numeric" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="216px"></asp:TextBox>
                                                </td>
                                                <td align="right" height="22px">Emissão Doc. Saída:</td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtEmissaoDocSaida" runat="server" CssClass="Calendario" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor='<%= Session("SIS_COR_PADRAO") %>' colspan="6" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" width="110px">Nota Fiscal </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="110px">Lote/BL: </td>
                                                <td align="left" colspan="5">
                                                    <asp:DropDownList ID="cbLote" runat="server" AutoPostBack="True" DataTextField="DESCR" DataValueField="LOTE" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="110px">Nota Fiscal: </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="AlphaNumeric" Enabled="False" Font-Names="Verdana" Font-Size="12px" MaxLength="10" Width="218px"></asp:TextBox>
                                                    &nbsp;</td>
                                                <td align="right" height="22px">Série:&nbsp; </td>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSerie" runat="server" CssClass="AlphaNumeric" Enabled="False" Font-Names="Verdana" Font-Size="12px" MaxLength="4" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="right">Emissão:</td>
                                                <td align="right">
                                                    <asp:TextBox ID="txtDataEmissao" runat="server" CssClass="Calendario" Enabled="False" Font-Names="Verdana" Font-Size="12px" MaxLength="10" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">Carga Solta:</td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="cbItensCS" runat="server" AutoPostBack="True" Enabled="False" Font-Names="Verdana" Font-Size="11px" Width="99%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">Qtde:</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtQtde" runat="server" BackColor="White" Enabled="False" Font-Names="Verdana" Font-Size="12px" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">Packing List:</td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtPackingList" runat="server" Enabled="False" Font-Names="Verdana" Font-Size="12px" MaxLength="150" Width="98%"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnExcluirProduto" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9px" Height="18px" OnClientClick="return confirm('Deseja excluir as Notas Fiscais deste agendamento?');" Text="Exclur" Visible="false" Width="100%" />
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnAdicProduto" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CommandName="ADICIONAR" Font-Names="Verdana" Font-Size="9px" Height="18px" Text="Adicionar" Width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">&nbsp;</td>
                                                <td align="left" colspan="5">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <table cellpadding="1" cellspacing="1">
                                                        <tr>
                                                            <td align="center">&nbsp;</td>
                                                            <td align="center">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center">&nbsp;</td>
                                                <td align="center">&nbsp;</td>
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
                                                    <asp:Panel ID="PanelScrollNotas" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Height="113px" ScrollBars="Vertical">
                                                        <asp:GridView ID="dgNotas" runat="server" AutoGenerateColumns="False" BackColor="#999999" BorderColor="GhostWhite" BorderStyle="Solid" CellPadding="4" CellSpacing="1" DataKeyNames="AUTONUM,AUTONUM_PRODUTO,LOTE,NOTAFISCAL,TIPO,SERIE,EMISSAO,QTDE" EmptyDataText="Nenhuma Nota Fiscal com Carga Solta foram vinculados." Font-Names="Verdana" Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="AUTONUMAGENDAMENTO" HeaderText="AUTONUMAGENDAMENTO" Visible="False" />
                                                                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False" />
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
                                                                <asp:BoundField DataField="AUTONUM_PRODUTO" HeaderText="AUTONUM_PRODUTO" HtmlEncode="False" Visible="False" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="cmdEditar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="EDITAR" ImageUrl="~/imagens/editar.gif" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="cmdExcluir" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="DEL" ImageUrl="~/imagens/excluir.png" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <EmptyDataRowStyle BackColor="GhostWhite" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle CssClass="CorPadrao" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle CssClass="CorPadrao" Font-Bold="True" ForeColor="White" />
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
                        <td valign="top">
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
                                            Height="304px">
                                            <asp:GridView ID="dgPeriodos" runat="server"
                                                AutoGenerateColumns="False" CellPadding="0" CellSpacing="1"
                                                DataKeyNames="AUTONUM_GD_RESERVA,PERIODO_INICIAL,PERIODO_FINAL,SALDO,FLAG_DTA"
                                                EmptyDataText="Nenhum período disponível." Font-Names="Verdana"
                                                Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                Width="305px">
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
                                            <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
                                        </asp:Panel>



                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                            <iframe id="frameDocumentos" runat="server" height="350px" scrolling="auto" visible="false" width="100%"></iframe>
                        </td>
                        <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                            <iframe id="frameEmails" runat="server" height="350px" scrolling="auto" visible="false" width="98%"></iframe>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2">
                            <div style="text-align: left">
                                <asp:Panel ID="pnLegenda" runat="server" Visible="False">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px" Text="CARGA DISPONÍVEL NO LOTE/BL:"></asp:Label>
                                    <asp:Label ID="lblLoteSelecionado1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#CC0000"></asp:Label>
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

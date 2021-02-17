<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master"
    CodeBehind="AgendamentoImportacao.aspx.vb" Inherits="GPD.AgendamentoImportacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        * {
            padding: 0px;
            margin: 0px;
        }


        .QtdItens {
            text-align: center;
        }

        .style1 {
            width: 100%;
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
            width: 922px;
            background-color: #F08080;
            color: white;
            font-weight: bold;
            border: 1px solid black;
        }
    </style>



    <script type="text/javascript">
        (function ($) {
            $.fn.vAlign = function () {
                return this.each(function (i) {
                    var h = $(this).height();
                    var oh = $(this).outerHeight();
                    var mt = (h + (oh - h)) / 2;
                    $(this).css("margin-top", "-" + mt + "px");
                    $(this).css("top", "50%");
                    $(this).css("position", "absolute");
                });
            };
        })(jQuery);

        (function ($) {
            $.fn.hAlign = function () {
                return this.each(function (i) {
                    var w = $(this).width();
                    var ow = $(this).outerWidth();
                    var ml = (w + (ow - w)) / 2;
                    $(this).css("margin-left", "-" + ml + "px");
                    $(this).css("left", "50%");
                    $(this).css("position", "absolute");
                });
            };
        })(jQuery);

        $(document).ready(function () {

            $("#tbcad").vAlign();
            $("#tbcad").hAlign();

        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <center>

            <asp:Panel ID="pnlMsgErro" runat="server" Visible="false" CssClass="pnl-erro">
                <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
            </asp:Panel>

            <table>
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
                                            <td align="left">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNomeMotorista" runat="server" Width="170px"
                                                                Font-Names="Verdana" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="chAutonomo" runat="server" Text="   Autônomo" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">CNH:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cbCNH" runat="server" Width="100%" Font-Names="Verdana"
                                                    AutoPostBack="True">
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
                                            <td align="left">
                                                <table cellpadding="0" cellspacing="0"
                                                    style="font-family: verdana; font-size: 11px" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="cbCavalo" runat="server" Width="80px" Enabled="False"
                                                                Font-Names="Verdana" AutoPostBack="True" DataTextField="PLACA_CAVALO"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                            <asp:ListSearchExtender ID="cbCavalo_ListSearchExtender" runat="server"
                                                                Enabled="True" PromptText="Digite" TargetControlID="cbCavalo"
                                                                PromptCssClass="PromptCSS">
                                                            </asp:ListSearchExtender>
                                                        </td>
                                                        <td align="right" width="90px">Placa Carreta:
                                                        </td>
                                                        <td align="right">
                                                            <asp:DropDownList ID="cbCarreta" runat="server" Width="80px" Enabled="False"
                                                                Font-Names="Verdana" DataTextField="PLACA_CARRETA" Font-Size="11px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:ListSearchExtender ID="cbCarreta_ListSearchExtender" runat="server"
                                                                Enabled="True" PromptText="Digite"
                                                                TargetControlID="cbCarreta" PromptCssClass="PromptCSS">
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
                                                                Font-Names="Verdana" BackColor="White"></asp:TextBox>
                                                        </td>
                                                        <td align="right">Chassi:
                                                        </td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtChassi" runat="server" Width="74px" CssClass="AlphaNumeric" Enabled="False"
                                                                Font-Names="Verdana" BackColor="White"></asp:TextBox>
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
                                                                Font-Names="Verdana"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left" width="80px">Nº Protocolo:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProtocolo" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                Font-Size="10px">Não Gerado</asp:Label>
                                                            <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px"
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Text="Salvar" Width="60px"
                                                                Height="20px" Enabled="False" />
                                                            <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Text="Excluir" Width="60px"
                                                                Height="20px" OnClientClick="return confirm('Deseja realmente excluir o agendamento');"
                                                                Enabled="False" />
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
                                            <td width="110px" align="left">Tipo Doc. Saída:
                                            </td>
                                            <td align="left" width="220px">
                                                <asp:DropDownList ID="cbTipoDocSaida" runat="server" Width="220px"
                                                    Font-Names="Verdana">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="CR">Conhecimento Rodoviário (CR)</asp:ListItem>
                                                    <asp:ListItem Value="MN">Minuta (MN)</asp:ListItem>
                                                    <asp:ListItem Value="OC">Ordem de Coleta (OC)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">Série Doc. Saída:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtSerieDocSaida" runat="server" Width="110px"
                                                    Font-Names="Verdana" MaxLength="15" CssClass="AlphaNumeric"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="110px" align="left">Nro. Doc. Saída:
                                            </td>
                                            <td align="left" width="220px">
                                                <asp:TextBox ID="txtNrDocSaida" runat="server" Width="216px"
                                                    Font-Names="Verdana" CssClass="Numeric" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td align="right">Emissão Doc. Saída:
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtEmissaoDocSaida" runat="server" Width="110px" CssClass="Calendario"
                                                    Font-Names="Verdana"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="110px" align="left">Contêiner:
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:DropDownList ID="cbConteineres" runat="server" Width="100%"
                                                    Font-Names="Verdana" DataTextField="ID_CONTEINER" DataValueField="AUTONUM"
                                                    AutoPostBack="True" Enabled="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
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
                                            <td align="center" width="10px">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="height: auto;">
                                    <asp:Panel ID="PanelScrollPeriodos" runat="server" ScrollBars="Vertical" Height="187px">
                                        <asp:GridView ID="dgPeriodos" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                            EmptyDataText="Nenhum período disponível." Font-Names="Verdana" Font-Size="10px"
                                            ForeColor="#333333" GridLines="None" Width="310px" ShowHeader="False" CellSpacing="1"
                                            DataKeyNames="AUTONUM_GD_RESERVA,PERIODO_INICIAL,PERIODO_FINAL,SALDO,FLAG_DTA">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="AUTONUM" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PERIODO_INICIAL" HeaderText="PERÍODO INICIAL">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PERIODO_FINAL" HeaderText="PERÍODO FINAL">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SALDO" HeaderText="SALDO">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FLAG_DTA" HeaderText="FLAG_DTA" Visible="False" />
                                                <asp:ButtonField ButtonType="Image" Text="Button"
                                                    ImageUrl="~/imagens/unchecked.png">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <EmptyDataRowStyle BackColor="GhostWhite" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Label ID="lblPeriodo" runat="server" Text="Nenhum período foi selecionado."
                                        Font-Bold="True" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblFreeTime" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblLote" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblTipoDoc" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblPatio" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblVip" runat="server" Visible="False"></asp:Label>
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
                                            <td width="110px" align="left">Número NF:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNotaFiscal" runat="server" Width="80px" CssClass="Numeric"
                                                    Font-Names="Verdana" MaxLength="10"></asp:TextBox>
                                            </td>
                                            <td align="right">Série:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSerie" runat="server" Width="80px" CssClass="AlphaNumeric"
                                                    Font-Names="Verdana" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td align="right" height="22px">&nbsp;Emissão:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDataEmissao" runat="server" Width="80px" CssClass="Calendario"
                                                    Font-Names="Verdana" MaxLength="10"></asp:TextBox>
                                            </td>
                                            <td align="right" width="110px">
                                                <asp:Button ID="btVincular" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="9px" Text="Vincular Contêiner"
                                                    Width="100%" Height="18px" />
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
                                            <td align="left" colspan="7">
                                                <asp:Panel ID="PanelScrollNotas" runat="server" ScrollBars="Vertical" Height="80px"
                                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                    <asp:GridView ID="dgNotas" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        CellSpacing="1" EmptyDataText="Nenhum lote foi vinculado." Font-Names="Verdana"
                                                        Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        Width="97%" DataKeyNames="ID" BackColor="#999999" BorderColor="GhostWhite"
                                                        BorderStyle="Solid">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>

                                                            <asp:TemplateField Visible="false">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="Server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />


                                                            <asp:BoundField DataField="ID_CONTEINER" HeaderText="CÓD. CONTEINER">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOTAFISCAL" HeaderText="NOTA FISCAL">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CONTEINER" HeaderText="CONTÊINER">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SERIE" HeaderText="SÉRIE">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EMISSAO" HeaderText="EMISSÃO">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="LOTE" HeaderText="LOTE">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                                        CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <EmptyDataRowStyle BackColor="GhostWhite" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                    <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                        <iframe id="frameDocumentos" runat="server" height="320px" scrolling="auto" visible="false" width="100%"></iframe>
                    </td>
                    <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                        <iframe id="frameEmails" runat="server" height="320px" scrolling="auto" visible="false" width="98%"></iframe>
                    </td>
                </tr>
            </table>
        </center>
    </form>
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="ConsultarTRA.aspx.vb" Inherits="GPD.ConsultarTRA" %>

<%@ Register Assembly="AjaxControlToolKit" namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .PromptCSS
        {
            color: black;
            font-weight: bold;
            background-color: #EEE8AA;
            font-family: Arial;
            font-size: 10pt;
            border: solid 1px black;
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

    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div id="Div1" style="margin-left: 20px; margin-right: 20px;">
            <asp:Panel ID="pnlBarra" runat="server">
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" style="padding-right: 20px;">
                            <asp:Button ID="btAbrirPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                        <td width="40px">
                            Ação:
                        </td>
                        <td width="160px" align="right">
                            <asp:DropDownList ID="cbModoImpressao" runat="server" Font-Names="Verdana" Font-Size="11px"
                                Width="160px">
                                <asp:ListItem Value="0">Imprimir Selecionados</asp:ListItem>
                                <asp:ListItem Value="1">Imprimir Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="35px" align="right">
                            <asp:Button ID="btImprimir" runat="server" Font-Names="Verdana" Font-Size="10px"
                                OnClientClick="return confirm('Confirma a impressão do(s) Protocolo(s)?');" Text="OK"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" style="font-family: verdana; font-size: 10px; font-weight: bold">
                            HISTÓRICO DE AGENDAMENTOS
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPesquisa" runat="server" Visible="false">
                <table id="tbPesq" align="center" width="500px" bgcolor="#999999" border="0" cellpadding="1"
                    cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #999999;">
                    <tr>
                        <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold;
                            color: #FFFFFF;" bgcolor="<%= Session("SIS_COR_PADRAO") %>">
                            Pesquisar Agendamento
                        </td>
                    </tr>
                    <tr>
                        <td width="125px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            Navio/ Viagem:
                        </td>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbFiltroNavio" runat="server" Width="142px" Font-Names="Verdana"
                                Font-Size="11px">
                                <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                <asp:ListItem Value="1">Contém</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtNavio" runat="server" Style="margin-left: 0px" Width="250px"
                                Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                        </td>                    
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            Nome do Motorista:    
                        </td>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbFiltroMotorista" runat="server" Width="142px" Font-Names="Verdana"
                                Font-Size="11px">
                                <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                <asp:ListItem Value="1">Contém</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtMotorista" runat="server" Style="margin-left: 0px" Width="250px"
                            Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            CNH:
                        </td>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbFiltroCNH" runat="server" Width="142px" Font-Names="Verdana"
                                Font-Size="11px">
                                <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                <asp:ListItem Value="1">Contém</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtCNH" runat="server" Style="margin-left: 0px" Width="250px"
                            Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            Placa Cavalo:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtPlacaCV" runat="server" Width="140px" Font-Names="Verdana"
                                Font-Size="11px" MaxLength="8"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtPlacaCV_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="AAA-9999" TargetControlID="txtPlacaCV">
                            </asp:MaskedEditExtender>
                        </td>                        
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            Placa Carreta:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                            border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                            border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtPlacaCR" runat="server" Width="140px" Font-Names="Verdana"
                                Font-Size="11px" MaxLength="8"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtPlacaCR_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="AAA-9999" TargetControlID="txtPlacaCR">
                            </asp:MaskedEditExtender>
                        </td>                    
                    </tr>
                    <tr>
                        <td align="center" bgcolor="White" style="border-right: 1px dotted #999999; border-bottom: 1px dotted #999999;"
                            colspan="3">
                            <asp:Button ID="btRetornar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Retornar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Button ID="btLimpar" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Limpar"
                                Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>                    
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <div id="Grid-Scroll" style="overflow: auto; margin-left: 20px; margin-right: 20px;">
            <asp:GridView ID="dgConsulta" runat="server" AutoGenerateColumns="False" CellPadding="2"
                ForeColor="#333333" GridLines="None" CellSpacing="1" Font-Names="Verdana" Width="100%"
                Font-Size="10px" BackColor="#999999" ShowHeaderWhenEmpty="True" EmptyDataText="Nenhum registro encontrado."
                PageSize="15" DataKeyNames="AUTONUM,NUM_PROTOCOLO,ANO_PROTOCOLO" AllowPaging="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>                                     

                    <asp:ButtonField ButtonType="Image" CommandName="EDITAR" Text="Editar" ImageUrl="~/imagens/editar.gif">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:ButtonField>
                  
                  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Excluir" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False" SortExpression="AUTONUM" />

                    <asp:BoundField DataField="NAVIO_VIAGEM" HeaderText="NAVIO VIAGEM" SortExpression="VIAGEM" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NOME" HeaderText="MOTORISTA" SortExpression="NOME" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CNH" HeaderText="CNH" SortExpression="CNH" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PLACA CAVALO &lt;br/&gt; PLACA CARRETA" DataField="VEICULO"
                        HtmlEncode="False" SortExpression="VEICULO" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PERÍODO" DataField="PERIODO" HtmlEncode="False" SortExpression="PERIODO"
                        NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="NUM_PROTOCOLO,ANO_PROTOCOLO" DataNavigateUrlFormatString="Protocolo_RetiradaTRA.aspx?protocolo={0}{1}"
                        DataTextField="PROTOCOLO" HeaderText="PROTOCOLO / IMPRESSÃO" SortExpression="PROTOCOLO"
                        Target="_blank">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="155px" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText = "PÁTIO" DataField="PATIO" SortExpression="PATIO" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText = "IMPRESSO" DataField="IMP" SortExpression="IMP" NullDisplayText = "-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckProtocolo" runat="server" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Imprimir" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ANO_PROTOCOLO" HeaderText="ANO_PROTOCOLO" Visible="False"
                        NullDisplayText="-" SortExpression="ANO_PROTOCOLO" />
                    <asp:BoundField DataField="NUM_PROTOCOLO" HeaderText="NUM_PROTOCOLO" Visible="False"
                        NullDisplayText="-" SortExpression="NUM_PROTOCOLO" />
                    <asp:BoundField DataField="PATIO" HeaderText="PATIO" Visible="False" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#E9E9E9" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedDescendingCellStyle BackColor="#E9E7E2" />
            </asp:GridView>
        </div>

    </form>
</asp:Content>

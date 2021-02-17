<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master"
    CodeBehind="ConsultarAgendamentosImportacao.aspx.vb" Inherits="GPD.ConsultarAgendamentosImportacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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

            $("#tbpes").vAlign();
            $("#tbpes").hAlign();

        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">




    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div id="Div1" style="margin-left: 20px; margin-right: 20px;">
            <asp:Panel ID="pnBarra" runat="server">
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" style="padding-right: 20px;">
                            <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                        <td width="40px">Ação:
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
                        <td colspan="4" align="center" style="font-family: verdana; font-size: 10px; font-weight: bold">HISTÓRICO DE AGENDAMENTOS
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnPesquisa" runat="server" Visible="false">
                <table id="tbpes" align="center" width="500px" bgcolor="#999999" border="0" cellpadding="1"
                    cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #999999">
                    <tr>
                        <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF;"
                            bgcolor="<%= Session("SIS_COR_PADRAO") %>">Pesquisar Agendamento
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Contêiner:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtReserva" runat="server" Style="margin-left: 0px" Width="140px"
                                Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Tipo:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbTipo" runat="server" Width="144px" DataTextField="TIPOBASICO"
                                DataValueField="TIPOBASICO" Font-Names="Verdana" Font-Size="11px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Tamanho:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbTamanho" runat="server" Width="144px" Font-Names="Verdana"
                                Font-Size="11px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Placa Cavalo:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
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
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Placa Carreta:
                        </td>
                        <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
                            <asp:TextBox ID="txtPlacaCR" runat="server" Font-Names="Verdana"
                                Font-Size="11px" MaxLength="8" Width="140px"></asp:TextBox>
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
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">Motorista:
                        </td>
                        <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px; border-right-color: #999999">
                            <asp:DropDownList ID="cbFiltro1" runat="server" Width="142px" Font-Names="Verdana"
                                Font-Size="11px">
                                <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                <asp:ListItem Value="1">Contém</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="240px" align="left" bgcolor="White" style="border-right: 1px dotted #999999; border-bottom: 1px dotted #999999; margin-left: 80px;">
                            <asp:TextBox ID="txtMotorista" runat="server" Style="margin-left: 0px" Width="250px"
                                Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="White" style="border-right: 1px dotted #999999; border-bottom: 1px dotted #999999;"
                            colspan="3">
                            <asp:Button ID="btRetornar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Retornar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Button ID="btLimpar" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Limpar"
                                Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Button ID="btPesquisar2" runat="server" Font-Names="Verdana" Font-Size="10px"
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
                DataSourceID="DsConsulta" PageSize="20"
                DataKeyNames="AUTONUM,NUM_PROTOCOLO,ANO_PROTOCOLO,PATIO,DATA_FREE_TIME,VIP,DT_CHEGADA_VEI"
                AllowPaging="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:ButtonField ButtonType="Image" CommandName="EDIT" Text="Editar" ImageUrl="~/imagens/editar.gif">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:ButtonField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False" SortExpression="AUTONUM" />
                    <asp:BoundField HeaderText="CONTÊINER" DataField="ID_CONTEINER" SortExpression="ID_CONTEINER"
                        NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NOME" HeaderText="MOTORISTA" SortExpression="NOME" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CAVALO/CARRETA" DataField="VEICULO"
                        HtmlEncode="False" SortExpression="VEICULO" NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PERÍODO" DataField="PERIODO" HtmlEncode="False" SortExpression="PERIODO"
                        NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="TRANSPORTE" DataField="TIPO_TRANSPORTE" SortExpression="TIPO_TRANSPORTE"
                        NullDisplayText="-">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" SortExpression="DOCUMENTO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATA_FREE_TIME" HeaderText="DATA FREE TIME" SortExpression="DATA_FREE_TIME">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="REMOCAO" HeaderText="REMOÇÃO" NullDisplayText="-" SortExpression="REMOCAO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IMO" HeaderText="IMO" NullDisplayText="-" SortExpression="IMO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ONU" HeaderText="ONU" NullDisplayText="-" SortExpression="ONU">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="NUM_PROTOCOLO,ANO_PROTOCOLO" DataNavigateUrlFormatString="Protocolo_Imp.aspx?protocolo={0}{1}"
                        DataTextField="PROTOCOLO" HeaderText="PROTOCOLO" SortExpression="PROTOCOLO"
                        Target="_blank">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="IMPRESSO" HeaderText="IMPRESSO" SortExpression="IMPRESSO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_CHEGADA_VEI" HeaderText="CHEGADA" NullDisplayText="-"
                        SortExpression="DT_CHEGADA_VEI">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckProtocolo" runat="server" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ANO_PROTOCOLO" HeaderText="ANO_PROTOCOLO" Visible="False"
                        NullDisplayText="-" SortExpression="ANO_PROTOCOLO" />
                    <asp:BoundField DataField="NUM_PROTOCOLO" HeaderText="NUM_PROTOCOLO" Visible="False"
                        NullDisplayText="-" SortExpression="NUM_PROTOCOLO" />
                    <asp:BoundField DataField="PATIO" HeaderText="PATIO" Visible="False" />
                    <asp:BoundField DataField="VIP" HeaderText="VIP" Visible="False" />
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
            <asp:SqlDataSource ID="DsConsulta" runat="server"></asp:SqlDataSource>
        </div>
    </form>
    </div>
</asp:Content>

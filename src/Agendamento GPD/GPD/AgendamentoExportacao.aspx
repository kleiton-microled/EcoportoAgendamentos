<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master"
    CodeBehind="AgendamentoExportacao.aspx.vb" Inherits="GPD.AgendamentoExportacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <form id="form1" runat="server">
    &nbsp;&nbsp;&nbsp;
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="500px" bgcolor="#CCCCCC">
        <tr>
            <td align="center" class="CorPadrao" colspan="2" bgcolor="<%= Session("SIS_COR_PADRAO") %>"
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                Agendamento Exportação
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                Transportadora:
            </td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="right">
                <asp:TextBox ID="txtIDTransportadora" runat="server" MaxLength="8" Width="1px" Font-Names="Verdana"
                    Font-Size="11px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtTransportadora" runat="server" MaxLength="8" Width="370px" BackColor="White"
                    BorderStyle="None" Enabled="False" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                Veículo:
            </td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:DropDownList ID="cbVeiculos" runat="server" Width="280px" DataTextField="PLACAS"
                    DataValueField="AUTONUM" AutoPostBack="True">
                </asp:DropDownList>
                <asp:ListSearchExtender ID="cbVeiculos_ListSearchExtender" runat="server" Enabled="True"
                    PromptText="Digite para Pesquisar..." TargetControlID="cbVeiculos" 
                    PromptCssClass="PromptCSS">
                </asp:ListSearchExtender>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                Motorista:
            </td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:DropDownList ID="cbMotoristas" runat="server" Width="280px" DataTextField="NOME"
                                DataValueField="AUTONUM" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:ListSearchExtender ID="cbMotoristas_ListSearchExtender" runat="server" Enabled="True"
                                PromptText="Digite para Pesquisar..." TargetControlID="cbMotoristas" 
                                PromptCssClass="PromptCSS">
                            </asp:ListSearchExtender>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="chAutonomo" runat="server" Text="Autônomo" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                Contêiner:
            </td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
                <asp:DropDownList ID="cbConteiner" runat="server" Width="370px" DataTextField="ID_CONTEINER"
                    DataValueField="AUTONUM_GD_CNTR" AutoPostBack="True">
                </asp:DropDownList>
                <asp:ListSearchExtender ID="cbConteiner_ListSearchExtender" runat="server" Enabled="True"
                    PromptText="Digite para Pesquisar..." TargetControlID="cbConteiner" 
                    PromptCssClass="PromptCSS">
                </asp:ListSearchExtender>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White" colspan="2" align="center">
                <asp:Panel ID="PanelScrollPeriodos" runat="server" ScrollBars="Vertical" Height="200px"
                    Width="500px">
                    <asp:GridView ID="dgConsulta" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CellPadding="2" CellSpacing="1" EmptyDataText="Nenhum Período Disponível para o Contêiner selecionado."
                        Font-Names="Verdana" Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                        Width="480px" BorderColor="#999999" BackColor="#999999" DataKeyNames="AUTONUM_GD_RESERVA,PERIODO_INICIAL,PERIODO_FINAL,SALDO">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField HeaderText="AUTONUM_GD_RESERVA" DataField="AUTONUM_GD_RESERVA" Visible="False">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PERIODO_INICIAL" HeaderText="Período Inicial">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PERIODO_FINAL" HeaderText="Período Final">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagens/unchecked.png" Text="Button"
                                CommandName="CHECK">
                                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:ButtonField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataRowStyle BackColor="#EEEEEA" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedDescendingCellStyle BackColor="#E9E7E2" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnPeriodo" runat="server" Visible="False">
                    <table align="center" cellpadding="8" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblIDPeriodo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                                <asp:Label ID="lblIDAgendamento" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" bgcolor="White"
                height="30px">
                <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" Text="Retornar" Width="80px"
                    CausesValidation="False" />
                <asp:Button ID="btAgendar" runat="server" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" Text="Agendar" Width="80px"
                    Enabled="False" />
                <asp:Panel ID="PanelMsg" runat="server" Visible="false">
                    <br />
                    <asp:Label ID="lblMsgOK" runat="server" ForeColor="Green" Font-Bold="False"></asp:Label>
                    <asp:Label ID="lblMsgErro" runat="server" ForeColor="Red" Font-Bold="False"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>

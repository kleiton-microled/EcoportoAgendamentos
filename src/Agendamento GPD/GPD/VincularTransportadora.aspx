<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="VincularTransportadora.aspx.vb" Inherits="GPD.VincularTransportadora" %>
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

            $("#tbcad").vAlign();
            $("#tbcad").hAlign();

        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    
        <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="660px" bgcolor="#CCCCCC">
            <tr>
                <td align="center" class="CorPadrao" colspan="2" bgcolor="<%= Session("SIS_COR_PADRAO") %>"
                    style="font-family: Verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                    Vincular Transportadora com Recinto:
                </td>
            </tr>
            <tr>
                <td style="font-family: Verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                    Transportadora:
                </td>
                <td style="font-family: Verdana; font-size: 11px" bgcolor="White" align="right">
                    <asp:DropDownList ID="cbTransportadoras" runat="server" Width="380px">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rfvTransportadoras" runat="server"
                        ControlToValidate="cbTransportadoras" ErrorMessage="RequiredFieldValidator"
                        Font-Names="Verdana" ForeColor="Red" InitialValue="0">* Campo Obrigatório</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="font-family: Verdana; font-size: 11px" width="130px" bgcolor="White" align="right">
                    Recinto:
                </td>
                <td style="font-family: Verdana; font-size: 11px" bgcolor="White" align="right">
                    <asp:DropDownList ID="cbRecintos" runat="server" Width="380px"></asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rfvRecintos" runat="server" ControlToValidate="cbRecintos"
                        ErrorMessage="RequiredFieldValidator" Font-Names="Verdana" ForeColor="Red" 
                        InitialValue="0">* Campo Obrigatório</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" 
                    bgcolor="White" height="30px">
                    <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" Text="Retornar" Width="80px"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" CausesValidation="false" />
                    <asp:Button ID="btSalvar" runat="server" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px"
                        Text="Vincular" Width="80px" />
                </td>
            </tr>

            <tr>
                <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" 
                    bgcolor="White">
                    <asp:Panel ID="pnlMensg" runat="server" Visible="false">
                    <br />
                    <asp:Label ID="lblMsgOK" runat="server" ForeColor="Green" Font-Bold="false"></asp:Label>
                    <asp:Label ID="lblMsgErro" runat="server" ForeColor="Red" Font-Bold="false"></asp:Label>
                </asp:Panel>
                </td>
            </tr>
        </table>
    
    </form>

</asp:Content>
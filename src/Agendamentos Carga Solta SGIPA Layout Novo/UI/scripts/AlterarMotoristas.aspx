<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="AlterarMotoristas.aspx.vb" Inherits="GPD.AlterarMotoristas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="450px" 
        bgcolor="#CCCCCC">
        <tr>
            <td align="center" class="CorPadrao" colspan="2" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" bgcolor="<%= Session("SIS_COR_PADRAO") %>">
                Alterar Motoristas</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Transportadora</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtID" runat="server" MaxLength="8" Width="20px" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtIDTransportadora" runat="server" MaxLength="8" Width="20px" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtTransportadora" runat="server" MaxLength="8" Width="300px" 
                    BackColor="White" BorderStyle="None" Enabled="False" Font-Names="Verdana" 
                    Font-Size="11px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                CNH</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtCNH" runat="server" MaxLength="11" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric" Enabled="False"></asp:TextBox>
&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCNH" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Validade CNH</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtValidadeCNH" runat="server" MaxLength="10" Width="120px" Font-Names="Verdana"
                    Font-Size="11px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtValidadeCNH_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtValidadeCNH">
                </asp:MaskedEditExtender>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtValidadeCNH" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtValidadeCNH" Display="Dynamic" 
                    ErrorMessage="RegularExpressionValidator" ForeColor="Red" 
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">* Data Inválida</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Nome</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" Enabled="False"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtNome" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                RG</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtRG" runat="server" MaxLength="12" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtRG" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                CPF</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtCPF" runat="server" MaxLength="10" Width="120px" Font-Names="Verdana"
                    Font-Size="11px" Height="19px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtCPF_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="999,999,999-99" TargetControlID="txtCPF">
                </asp:MaskedEditExtender>
&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="txtCPF" ErrorMessage="CompareValidator" ForeColor="Red" 
                    Operator="NotEqual" ValueToCompare="___.___.___-__">* Campo Obrigatório</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Celular</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtCelular" runat="server" MaxLength="10" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtCelular_MaskedEditExtender" runat="server" 
                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="(99) 9999-9999" TargetControlID="txtCelular">
                </asp:MaskedEditExtender>
            &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="txtCelular" ErrorMessage="CompareValidator" ForeColor="Red" 
                    Operator="NotEqual" ValueToCompare="(__) ____-____">* Campo Obrigatório</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Nextel ID</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtNextel" runat="server" MaxLength="10" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtNextel" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Número MOP</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtNumeroMOP" runat="server" MaxLength="10" Width="120px" Font-Names="Verdana"
                    Font-Size="11px" CssClass="Numeric"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" 
                bgcolor="White" height="30px">
                <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Retornar" Width="80px" CausesValidation="False" />
                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Excluir" Width="80px" CausesValidation="False" 
                    onclientclick="return confirm('Confirma a exclusão do registro?');" />
                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Salvar" Width="80px" />
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

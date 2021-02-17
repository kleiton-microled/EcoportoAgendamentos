<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master" CodeBehind="AlterarVeiculos.aspx.vb" Inherits="AgendamentoCargaSoltaDDC.AlterarVeiculos" %>
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="450px" 
        bgcolor="#CCCCCC">
        <tr>
            <td align="center" class="CorPadrao" colspan="2" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF" bgcolor="<%= Session("SIS_COR_PADRAO") %>">
                Alterar Veículos</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Transportadora</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtID" runat="server" MaxLength="8" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False" Width="20px"></asp:TextBox>
                <asp:TextBox ID="txtIDTransportadora" runat="server" MaxLength="8" 
                    Font-Names="Verdana" Font-Size="11px" Visible="False" Width="20px"></asp:TextBox>
                <asp:TextBox ID="txtTransportadora" runat="server" MaxLength="8" Width="300px" 
                    BackColor="White" BorderStyle="None" Enabled="False" Font-Names="Verdana" 
                    Font-Size="11px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Placa-Cavalo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtPlacaCavalo" runat="server" MaxLength="8" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" Enabled="True" CssClass="Placa"></asp:TextBox>                
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPlacaCavalo" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Placa-Carreta</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtPlacaCarreta" runat="server" MaxLength="8" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" Enabled="True" CssClass="Placa"></asp:TextBox>                
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Tara Veículo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtTaraVeiculo" runat="server" MaxLength="6" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Modelo</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtModelo" runat="server" MaxLength="25" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMod" runat="server"
                ControlToValidate="txtModelo" ErrorMessage="RequiredFieldValidator"
                Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Cor</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtCor" runat="server" MaxLength="20" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCor" runat="server" 
                    ControlToValidate="txtCor" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Chassi</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtChassi" runat="server" MaxLength="17" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Renavam</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:TextBox ID="txtRenavam" runat="server" MaxLength="10" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtRenavam" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
                Tipo de Carreta</td>
            <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                <asp:DropDownList ID="cbTipoCarreta" runat="server" Width="205px" 
                    Font-Names="Verdana" Font-Size="11px">
                </asp:DropDownList>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="cbTipoCarreta" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red">* Campo Obrigatório</asp:RequiredFieldValidator>
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


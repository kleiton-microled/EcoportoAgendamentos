<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="LembrarSenha.aspx.vb" Inherits="GPD.LembrarSenha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
<table align="center" cellpadding="2" cellspacing="1" width="450px" 
        bgcolor="#CCCCCC">
    <tr>
        <td align="center" class="CorPadrao" colspan="2" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
            Lembrar Senha</td>
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
            CPF</td>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White">
            <asp:TextBox ID="txtCPF" runat="server" MaxLength="12" Width="200px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="Numeric" 
                Enabled="False"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCPF" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White" 
            align="center" colspan="2" height="26px">
            * O Email será enviado para o email cadastrado para este usuário.</td>
    </tr>
    <tr>
        <td align="center" colspan="2" style="font-family: verdana; font-size: 11px" 
                bgcolor="White" height="30px">
            <asp:Button ID="btRetornar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Retornar" Width="80px" CausesValidation="False" />
            <asp:Button ID="btLimpar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Limpar" Width="80px" CausesValidation="False" 
                    onclientclick="return confirm('Confirma a exclusão do registro?');" />
            <asp:Button ID="btEnviar" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                    Text="Enviar" Width="80px" />
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

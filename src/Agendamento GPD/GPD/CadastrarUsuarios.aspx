<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="CadastrarUsuarios.aspx.vb" Inherits="GPD.CadastrarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
<table align="center" cellpadding="2" cellspacing="1" width="450px" 
        bgcolor="#CCCCCC">
    <tr>
        <td align="center" class="CorPadrao" colspan="2" 
                style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
            Cadastrar Usuários</td>
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
            <asp:TextBox ID="txtCPF" runat="server" MaxLength="12" Width="120px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="CPF" Enabled="False"></asp:TextBox>
&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCPF" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
            Ativo</td>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td width="130px">
                        <asp:RadioButtonList ID="rbAtivo" runat="server" RepeatDirection="Horizontal" 
                            Width="100px">
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                            <asp:ListItem Value="0">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="rbAtivo" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Selecione uma opção</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
            Administrador</td>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White" align="left">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td width="130px">
                        <asp:RadioButtonList ID="rbAdmin" runat="server" RepeatDirection="Horizontal" 
                            Width="100px">
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                            <asp:ListItem Value="0">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="rbAdmin" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Selecione uma opção</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
            Nome</td>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White">
            <asp:TextBox ID="txtNome" runat="server" MaxLength="17" Width="210px" 
                    Font-Names="Verdana" Font-Size="11px" CssClass="AlphaNumeric"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNome" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="font-family: verdana; font-size: 11px" width="100px" bgcolor="White">
            Email</td>
        <td style="font-family: verdana; font-size: 11px" bgcolor="White">
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="10" Width="210px" 
                    Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="RequiredFieldValidator" 
                    Font-Names="Verdana" ForeColor="Red" Display="Dynamic">* Campo Obrigatório</asp:RequiredFieldValidator>
        </td>
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

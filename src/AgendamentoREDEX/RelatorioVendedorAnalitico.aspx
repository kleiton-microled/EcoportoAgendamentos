<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RelatorioVendedorAnalitico.aspx.vb"
    Inherits="AgendamentoREDEX.RelatorioVendedorAnalitico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 200px;">
                    <img alt="" src="imagens/LogoTop.png" />
                </td>
                <td align="center" style="font-family: arial, Helvetica, sans-serif; font-size: 18px;
                    font-weight: bold">
                    Relatório de Cálculo Analítico de Vendedores<br />
                    Mês de referência:
                </td>
                <td style="width: 200px;" align="right">
                    Data:
                    <asp:Label ID="lblData" runat="server"></asp:Label>
                    <br />
                    Hora:
                    <asp:Label ID="lblHora" runat="server"></asp:Label>
                    &nbsp;<br />
                    Usuário:
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

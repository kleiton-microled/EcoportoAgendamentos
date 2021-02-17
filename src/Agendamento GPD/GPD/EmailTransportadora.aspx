<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmailTransportadora.aspx.vb" Inherits="GPD.EmailTransportadora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <form id="form1" runat="server">
        <p style="font-family:Arial;font-size:12px;color:red;font-weight:bold;padding:0;margin:0;">Por favor, informar um e-mail para contato em caso de divergência na documentação:</p>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                Por favor, Aguarde....
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%; border: 1px solid silver; border-collapse: collapse; font-family: Tahoma; font-size: 12px;">
                        <tr>
                            <td style="background-color: #b3c63c"><strong>Informe o email de contato:</strong></td>
                        </tr>
                        <tr>
                            <td>

                                <table style="width: 100%;">
                                    <tr>
                                        <td rowspan="4">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="240px" Rows="14" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnAnexar" runat="server" OnClick="btnAnexar_Click" OnClientClick="MostrarProgresso();" Text="Salvar" Width="60px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblErro" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                            <asp:Label ID="lblSucesso" runat="server" Font-Bold="True" ForeColor="#00CC66" Visible="False">Email cadastrado com sucesso!</asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>Para inserir mais de um e-mail colocar ; (ponto e vírgula)</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAnexar" runat="server" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>

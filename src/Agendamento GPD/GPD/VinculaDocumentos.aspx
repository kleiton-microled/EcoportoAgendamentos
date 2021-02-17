<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VinculaDocumentos.aspx.vb" Inherits="GPD.VinculaDocumentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p style="font-family: Arial; font-size: 12px; color: red; font-weight: bold; padding: 0; margin: 0;">Atenção! A fim de melhorar o processo de chegada do motorista ao Terminal, solicitamos anexar às documentações necessárias para carregamento da carga.</p>
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
                            <td style="background-color: #b3c63c"><strong>Envio de Documentos:</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3">São permitidos PDF - GIF - JPEG - PNG com tamanho máximo de 5MB<br /><br />
                                            <asp:CheckBox ID="chkAceite" runat="server" AutoPostBack="true" Text="   Declaro, sob as penas da lei, que os documentos anexados são autênticos e equivalem aos originais que estão em meu poder." />
                                            <br />
                                            <br />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td width="150px">
                                            <asp:DropDownList ID="cbDocumentos" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="txtUpload" runat="server" accept=".png, .jpg, .jpeg, .gif, .pdf" Width="260px" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnAnexar" runat="server" Text="Enviar Arquivo" OnClick="btnAnexar_Click" OnClientClick="MostrarProgresso();" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblErro" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                            <asp:Label ID="lblSucesso" runat="server" Font-Bold="True" ForeColor="#00CC66" Visible="False">Arquivo enviado com sucesso!</asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #b3c63c"><strong>Documentos Anexados:</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvDocumentos" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="AUTONUM_AV_IMAGEM" EmptyDataText="Nenhum documento anexado" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="AUTONUM_AV_IMAGEM" HeaderText="Autonum" Visible="False" />
                                        <asp:BoundField DataField="DESCRICAO" HeaderText="Documento" />
                                        <asp:HyperLinkField
                                            DataNavigateUrlFields="AUTONUM_AV_IMAGEM,LOTE"
                                            DataNavigateUrlFormatString="VisualizaDocumentos.aspx?id={0}&lote={1}"
                                            DataTextField="NOME_IMG" HeaderText="Anexo"
                                            Target="_blank"></asp:HyperLinkField>
                                        <asp:BoundField DataField="DT_INCLUSAO" HeaderText="Data Envio">
                                            <ItemStyle Width="160px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="cmdExcluir" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="DEL" ImageUrl="~/imagens/excluir.png" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#b3c63c" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
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

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EscolheReserva.aspx.vb" Inherits="AgendamentoREDEX.EscolheReserva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="font-family: tahoma; font-size: 12px">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    Escolha a Reserva:<br />
    
    <asp:GridView ID="dgBooking" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="None" 
        EmptyDataText="Nenhuma NF encontrada." ForeColor="#333333"
                                    ShowHeaderWhenEmpty="True" Width="100%" 
        Font-Size="11px" DataKeyNames="AUTONUM_BOO" Font-Names="Tahoma">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="AUTONUM_BOO" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField HeaderText="Reserva" DataField="REFERENCE">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Navio/Viagem" DataField="NAVIO_VIAGEM">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="NVOCC" DataField="NVOCC">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXPORTADOR" HeaderText="Exportador" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="cmdSelecionar" ImageUrl="~/imagens/check.png"
                                                    CommandName="SEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#B3C63C" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

    </form>
</body>
</html>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ConsultarAgendamentosCNTR.aspx.vb" Inherits="AgendamentoREDEX.ConsultarAgendamentosCNTR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <asp:Panel ID="pnFiltro" runat="server" Visible="false">
            <table id="tbpes" align="center" width="500px" bgcolor="#999999" border="0" cellpadding="1"
                cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #999999">
                <tr>
                    <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold;
                        color: #FFFFFF;" bgcolor="#B3C63C">
                        Pesquisar Agendamento
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Reserva:
                    </td>
                    <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:TextBox ID="txtReserva" runat="server" Style="margin-left: 0px" Width="140px"
                            Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                        border-bottom-color: #999999; border-right-style: dotted; border-right-width: 1px;
                        border-right-color: #999999" width="100px">
                        Contêiner:
                    </td>
                    <td align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999" width="100px">
                        <asp:TextBox ID="txtConteiner" runat="server" Font-Names="Verdana" Font-Size="11px"
                            MaxLength="12" Width="140px"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Tipo:
                    </td>
                    <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:DropDownList ID="cbTipo" runat="server" Width="144px" DataTextField="TIPOBASICO"
                            DataValueField="TIPOBASICO" Font-Names="Verdana" Font-Size="11px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Tamanho:
                    </td>
                    <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:DropDownList ID="cbTamanho" runat="server" Width="144px" Font-Names="Verdana"
                            Font-Size="11px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Placa Cavalo:
                    </td>
                    <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:TextBox ID="txtPlacaCV" runat="server" Width="140px" Font-Names="Verdana" Font-Size="11px"
                            MaxLength="8"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Placa Carreta:
                    </td>
                    <td width="100px" align="left" bgcolor="White" colspan="2" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:TextBox ID="txtPlacaCR" runat="server" Width="140px" Font-Names="Verdana" Font-Size="11px"
                            MaxLength="8"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Navio:
                    </td>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:DropDownList ID="cbFiltro1" runat="server" Width="142px" Font-Names="Verdana"
                            Font-Size="11px">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="240px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:TextBox ID="txtNavio" runat="server" Style="margin-left: 0px" Width="250px"
                            Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        Motorista:
                    </td>
                    <td width="100px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                        border-bottom-width: 1px; border-bottom-color: #999999; border-right-style: dotted;
                        border-right-width: 1px; border-right-color: #999999">
                        <asp:DropDownList ID="cbFiltro2" runat="server" Width="142px" Font-Names="Verdana"
                            Font-Size="11px">
                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                            <asp:ListItem Value="1">Contém</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="240px" align="left" bgcolor="White" style="border-right: 1px dotted #999999;
                        border-bottom: 1px dotted #999999; margin-left: 80px;">
                        <asp:TextBox ID="txtMotorista" runat="server" Style="margin-left: 0px" Width="250px"
                            Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="White" style="border-right: 1px dotted #999999; border-bottom: 1px dotted #999999;"
                        colspan="3">
                        <asp:Button ID="btRetornar" runat="server" Font-Names="Verdana" Font-Size="10px"
                            Text="Retornar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        <asp:Button ID="btLimpar" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Limpar"
                            Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        <asp:Button ID="btPesquisar2" runat="server" Font-Names="Verdana" Font-Size="10px"
                            Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table style="width: 98%">
            <tr>
                <td align="left">
                    <asp:Button ID="btnAgendar" runat="server" Text="Agendar Contêiner" />
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgConsulta" runat="server" AutoGenerateColumns="False" CellPadding="4"
            EmptyDataText="Nenhum registro encontrado." ForeColor="#333333" ShowHeaderWhenEmpty="True"
            Font-Size="11px" DataKeyNames="AUTONUM_GD_CNTR" Width="98%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <%--<asp:BoundField HeaderText="Exportador" DataField="EXPORTADOR" />--%>
                <%--<asp:BoundField HeaderText="POD" DataField="PORTO_ORIGEM" />
                <asp:BoundField HeaderText="FDES" DataField="PORTO_DESTINO" />                          --%>
                <asp:BoundField HeaderText="Reserva" DataField="RESERVA" />
                <asp:BoundField HeaderText="Navio/Viagem" DataField="NAVIO_VIAGEM" />
                <asp:BoundField HeaderText="Contêiner" DataField="CONTEINER" />
               <%-- <asp:HyperLinkField DataNavigateUrlFields="AUTONUM_GD_CNTR" DataNavigateUrlFormatString="ProtocoloCNTR.aspx?protocolo={0}"
                    DataTextField="PROTOCOLO" HeaderText="Protocolo" Target="_blank">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>--%>
                 <asp:TemplateField HeaderText="Protocolo">
                    <ItemTemplate>
                        <%--<asp:ImageButton runat="server" ID="cmdProtocolo"  CommandName="EDITAR"
                            CommandArgument="<%# Container.DataItemIndex %>" />--%>
                        
                        <asp:button text='<%# Eval("PROTOCOLO").ToString() %>' runat="server" CommandName="PROTOCOLO" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Período" DataField="PERIODO" />
                <asp:BoundField HeaderText="Motorista" DataField="MOTORISTA" />
                <asp:BoundField HeaderText="CNH" DataField="CNH" />
                <asp:BoundField HeaderText="Veículo" DataField="VEICULO" />
                <asp:BoundField HeaderText="Dead Line" DataField="DEAD_LINE" />
                <asp:BoundField HeaderText="Status" DataField="STATUS" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDITAR"
                            CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Confirma a exclusão do Agendamento?');" />
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
    </center>
</asp:Content>

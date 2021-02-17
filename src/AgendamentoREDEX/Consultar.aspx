<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Consultar.aspx.vb" Inherits="AgendamentoREDEX.Consultar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <asp:GridView ID="dgConsulta" runat="server" AutoGenerateColumns="False" CellPadding="4"
            EmptyDataText="Nenhum registro encontrado." ForeColor="#333333" ShowHeaderWhenEmpty="True"
            Font-Size="11px" DataKeyNames="AUTONUM">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField HeaderText="AUTONUM" DataField="AUTONUM" Visible="False" />
                <asp:BoundField HeaderText="Motorista" DataField="NOME" />
                <asp:BoundField HeaderText="CNH" DataField="CNH" />
                <asp:BoundField HeaderText="Data" DataField="DATA_AGENDAMENTO" />
                <asp:BoundField HeaderText="Veículo" DataField="VEICULO" />
                <asp:BoundField HeaderText="Protocolo" DataField="PROTOCOLO" />
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

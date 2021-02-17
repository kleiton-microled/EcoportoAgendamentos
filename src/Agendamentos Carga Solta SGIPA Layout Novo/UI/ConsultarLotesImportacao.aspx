<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="ConsultarLotesImportacao.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.ConsultarLotesImportacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div id="Geral" style="margin: 0px 20px 0px 20px;">




            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" />
            <table cellpadding="4" cellspacing="0" style="padding-top: 4px; padding-bottom: 4px;"
                width="100%">
                <tr>
                    <td align="center" width="50%">
                        <asp:GridView ID="dgConsulta" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="2" CellSpacing="1" EmptyDataText="Nenhum Lote Disponível."
                            Font-Names="Verdana" Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                            Width="100%" BorderColor="#999999" BackColor="#999999"
                             AllowPaging="True" DataSourceID="DsConsulta"
                            PageSize="20">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <% If Eval("AUTONUM") = "NAO" Then  %>
                                        <asp:HyperLink ID="lnkAgendar" runat="server" NavigateUrl='<%# Eval("AUTONUM", "CadastrarAgendamentos.aspx?conteiner={0}&free_time={1}&doc={2}&patio={3}&vip={4}&lote={5}") %>' Text="Agendar"></asp:HyperLink>
                                        <% End If %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="Patio" HeaderText="Patio" />
                                <asp:BoundField DataField="LOTE" HeaderText="Lote">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_DOCUMENTO" HeaderText="Documento">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NUM_DOCUMENTO" HeaderText="Num.Documento" />
                                <asp:BoundField DataField="Numero" HeaderText="BL" />
                                <asp:BoundField DataField="QUANTIDADE_LOTE" HeaderText="Quantidade BL" />
                                <asp:BoundField DataField="QUANTIDADE_AGENDA" HeaderText="Qtde. Agendado" />
                                <asp:BoundField DataField="Saldo" HeaderText="Saldo" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAgendar" runat="server" PostBackUrl='<%# "CadastrarAgendamentos.aspx?lote=" & Eval("LOTE") & "" %>' Text="Agendar"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pendências" SortExpression="Agendar" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendencias" runat="server" Text='<%# Bind("Agendar") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Agendar" HeaderText="Pendências" />

                                <asp:HyperLinkField DataNavigateUrlFields="LOTE,AGENDAR" DataNavigateUrlFormatString="EtapasCargaSolta.aspx?lote={0}&amp;Pend={1}" DataTextFormatString="Visualizar" Text="Visualizar" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <EmptyDataRowStyle BackColor="#EEEEEA" HorizontalAlign="Center" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedDescendingCellStyle BackColor="#E9E7E2" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DsConsulta" runat="server"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

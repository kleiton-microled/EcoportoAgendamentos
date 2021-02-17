<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master"
    CodeBehind="ConsultarConteineresImportacao.aspx.vb" Inherits="GPD.ConsultarConteineresImportacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Geral" style="margin: 0px 20px 0px 20px;">
        <form id="form1" runat="server">
            <table cellpadding="4" cellspacing="0" style="padding-top: 4px; padding-bottom: 4px;"
                width="100%">
                <tr>
                    <td align="center" width="50%">
                        <asp:GridView ID="dgConsulta" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="2" CellSpacing="1" EmptyDataText="Nenhum Contêiner Disponível."
                            Font-Names="Verdana" Font-Size="10px" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                            Width="100%" BorderColor="#999999" BackColor="#999999"
                            DataKeyNames="AUTONUM" AllowPaging="True" DataSourceID="DsConsulta"
                            PageSize="20">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AUTONUM_GD_CNTR" HeaderText="AUTONUM_GD_CNTR" Visible="False" />
                                <asp:BoundField DataField="ID_CONTEINER" HeaderText="CONTÊINER">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TAMANHO" HeaderText="TAMANHO">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPOCONTEINER" HeaderText="TIPO">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LOTE" HeaderText="LOTE">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_DOCUMENTO" HeaderText="TIPO DOC.">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOCUMENTO" HeaderText="NUM. DOC.">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PESO_APURADO" HeaderText="PESO APURADO">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IMO" HeaderText="IMO">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ONU" HeaderText="ONU">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATA_FREE_TIME" HeaderText="DATA FREE TIME">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="REMOCAO" HeaderText="REMOÇÃO">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAgendar" runat="server" PostBackUrl='<%# "AgendamentoImportacao.aspx?conteiner=" & Eval("AUTONUM") & "&free_time=" & Eval("DATA_FREE_TIME") & "&doc=" & Eval("TIPO_DOCUMENTO") & "&patio=" & Eval("PATIO") & "&vip=" & Eval("VIP") & "&lote=" & Eval("LOTE") %>' Text="Agendar"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM_CNTR" Visible="False" />
                                <asp:BoundField DataField="PATIO" HeaderText="PATIO" Visible="False" />
                                <asp:BoundField DataField="VIP" HeaderText="VIP" Visible="False" />
                                <asp:TemplateField HeaderText="Pendências" SortExpression="Agendar" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendencias" runat="server" Text='<%# Bind("Agendar") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AGENDAR" HeaderText="Pendências" />
                                <asp:HyperLinkField DataNavigateUrlFields="LOTE,AGENDAR" DataNavigateUrlFormatString="EtapasConteiner.aspx?lote={0}&amp;Pend={1}" DataTextFormatString="Visualizar" Text="Visualizar" />
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
        </form>
    </div>
</asp:Content>

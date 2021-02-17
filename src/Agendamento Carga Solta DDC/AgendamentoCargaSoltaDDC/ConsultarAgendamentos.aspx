<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="ConsultarAgendamentos.aspx.vb" Inherits="AgendamentoCargaSoltaDDC.ConsultarAgendamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div id="ConteudoGeral" style="margin-left: 20px; margin-right: 20px;">

            <table align="center" cellpadding="2" cellspacing="0" width="100%">
                <asp:Panel ID="PanelBarra" runat="server">
                    <tr>

                        <td align="left" style="padding-right: 20px;">
                            <asp:Button ID="btNovo" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Novo"
                                Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px"
                                Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                        <td width="40px">Ação:
                        </td>
                        <td width="160px">
                            <asp:DropDownList ID="cbModoImpressao" runat="server" Font-Names="Verdana" Font-Size="11px"
                                Width="160px">
                                <asp:ListItem Value="0">Imprimir Selecionados</asp:ListItem>
                                <asp:ListItem Value="1">Imprimir Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="35px" align="right">
                            <asp:Button ID="btImprimir" runat="server" Font-Names="Verdana" Font-Size="10px"
                                OnClientClick="return confirm('Confirma a impressão do(s) Protocolo(s)?');" Text="OK"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </td>
                    </tr>

                </asp:Panel>
                <tr>
                    <td>


                        <asp:Panel ID="PanelPesquisa" runat="server" Visible="False">

                            <table align="center" bgcolor="#999999" border="0" cellpadding="1" cellspacing="0"
                                style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;">
                                <tr>
                                    <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF; border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999;"
                                        bgcolor="<%= Session("SIS_COR_PADRAO") %>">Pesquisa
                                    </td>
                                </tr>
                                <tr>
                                    <td width="70px" align="left" bgcolor="White" style="border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999">Protocolo:
                                    </td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999"
                                        width="100px">

                                        <asp:DropDownList ID="cbFiltro1" runat="server"
                                            Font-Names="Verdana" Font-Size="11px" Enabled="True">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td width="150px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999">
                                        <asp:TextBox ID="txtProtocolo" runat="server" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px"
                                            Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="70px" align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;">Cavalo:
                                    </td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="100px">
                                        <asp:DropDownList ID="cbFiltro2" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="150px" align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999">
                                        <asp:TextBox ID="txtCavalo" runat="server" CssClass="Placa" Font-Names="Verdana"
                                            Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="70px">Carreta:
                                    </td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="100px">
                                        <asp:DropDownList ID="cbFiltro3" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999"
                                        width="150px">
                                        <asp:TextBox ID="txtCarreta" runat="server" CssClass="Placa" Font-Names="Verdana"
                                            Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="70px">CNH:
                                    </td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="100px">
                                        <asp:DropDownList ID="cbFiltro4" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999"
                                        width="150px">
                                        <asp:TextBox ID="txtCNH" runat="server" CssClass="Numeric" Font-Names="Verdana"
                                            Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="70px">Motorista:</td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="100px">
                                        <asp:DropDownList ID="cbFiltro5" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999"
                                        width="150px">
                                        <asp:TextBox ID="txtMotorista" runat="server" CssClass="AlphaNumeric"
                                            Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="70px">Documento:</td>
                                    <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px; border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999;"
                                        width="100px">
                                        <asp:DropDownList ID="cbFiltro6" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #999999"
                                        width="150px">
                                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="Numeric"
                                            Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px"
                                            Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="White"
                                        style="border-right: 1px dotted #999999;" colspan="3">

                                        <asp:Button ID="btLimpar" runat="server" BackColor="WhiteSmoke"
                                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="11px" Text="Limpar" Width="75px" />
                                        <asp:Button ID="btFiltrar" runat="server" BackColor="WhiteSmoke"
                                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="11px" Text="Pesquisar" Width="75px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>





                    </td>
                </tr>
            </table>
            <asp:GridView ID="DgAgendamentos" runat="server" AutoGenerateColumns="False" CellPadding="1"
                ForeColor="#333333" GridLines="None" CellSpacing="1" Width="100%" EmptyDataText="Nenhum registro encontrado."
                ShowHeaderWhenEmpty="True" BackColor="#999999" Font-Names="Verdana" Font-Size="10px"
                DataKeyNames="AUTONUM_AG_CS, CNTR" AllowPaging="true" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDIT"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="AUTONUM_AG_CS" HeaderText="AUTONUM_AG_CS" Visible="False" SortExpression="AUTONUM_AG_CS">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MOTORISTA" DataField="NOME_MOTORISTA" SortExpression="NOME_MOTORISTA">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CNH" DataField="CNH" SortExpression="CNH">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="CAVALO/CARRETA">
                        <ItemTemplate>
                            <%# Eval("PLACA_CAVALO") + " " + Eval("PLACA_CARRETA")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PERIODO" HeaderText="PERÍODO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="AUTONUM_AG_CS" DataNavigateUrlFormatString="Protocolo.aspx?id={0}"
                        DataTextField="PROTOCOLO" HeaderText="PROTOCOLO"
                        Target="_blank">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="DOC. SAÍDA" DataField="NUM_DOC_SAIDA" SortExpression="NUM_DOC_SAIDA">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CONTÊINER" DataField="ID_CONTEINER" SortExpression="ID_CONTEINER">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="NÚMERO BL" DataField="NUMERO_BL" SortExpression="NUMERO_BL">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="DOCUMENTO">
                        <ItemTemplate>
                            <%# Eval("DESCR_DOCUMENTO") + " " + Eval("NUM_DOCUMENTO")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="DT_FREE_TIME" HeaderText="FREE TIME" SortExpression="DT_FREE_TIME">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_CHEGADA" HeaderText="CHEGADA" SortExpression="DT_CHEGADA">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" Visible="False">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="IMPRESSO" DataField="IMPRESSO" SortExpression="IMP">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckProtocolo" runat="server" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataRowStyle BackColor="#E9E9E9" HorizontalAlign="Center" VerticalAlign="Middle" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="CorPadrao" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </form>
</asp:Content>

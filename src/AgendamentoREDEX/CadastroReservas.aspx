<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CadastroReservas.aspx.vb" Inherits="AgendamentoREDEX.CadastroReservas"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <center>
        <br />
        <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
            bgcolor="#F8FFDE">
            <tr>
                <td colspan="2" style="border-color: Black; border-style: solid; border-width: 1px;
                    font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                    Cadastro da Reserva
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Reserva:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:TextBox ID="txtReserva" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Tipo:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbTipo" runat="server" DataTextField="DESCRICAO" DataValueField="AUTONUM_ARM"
                        Width="172px" AutoPostBack="True">
                        <asp:ListItem Value="0">-- SELECIONE --</asp:ListItem>
                        <asp:ListItem Value="1">Carga Solta</asp:ListItem>
                        <asp:ListItem Value="2">Contêiner</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <asp:Panel ID="pnConteiner" runat="server" Visible="false">
                <tr bgcolor="White">
                    <td style="border-style: solid; border-width: 1px" align="right">
                        Qtde Contêineres:
                    </td>
                    <td style="border-style: solid; border-width: 1px" align="left">
                        <asp:TextBox ID="txtQtdeConteineres" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnCargaSolta" runat="server" Visible="false">
                <tr bgcolor="White">
                    <td style="border-style: solid; border-width: 1px" align="right">
                        Quantidade:
                    </td>
                    <td style="border-style: solid; border-width: 1px" align="left">
                        <asp:TextBox ID="txtQuantidade" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="White">
                    <td style="border-style: solid; border-width: 1px" align="right">
                        Peso:
                    </td>
                    <td style="border-style: solid; border-width: 1px" align="left">
                        <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="White">
                    <td style="border-style: solid; border-width: 1px" align="right">
                        Volumes:
                    </td>
                    <td style="border-style: solid; border-width: 1px" align="left">
                        <asp:TextBox ID="txtVolumes" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="White">
                    <td style="border-style: solid; border-width: 1px" align="right">
                        Metragem Cúbica:
                    </td>
                    <td style="border-style: solid; border-width: 1px" align="left">
                        <asp:TextBox ID="txtM3" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Armador:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbArmador" runat="server" DataTextField="DESCRICAO" DataValueField="AUTONUM_ARM"
                        Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Viagem:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbViagem" runat="server" DataTextField="NUM_VIAGEM" DataValueField="AUTONUM_VIA"
                        Width="500px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Navio:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbNavio" runat="server" DataTextField="DESCRICAO_NAV" DataValueField="AUTONUM_NAV"
                        Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Exportador:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:TextBox ID="txtExportador" runat="server" Width="496px"></asp:TextBox>
                </td>
                <asp:AutoCompleteExtender ID="AutoCompleteExportador" ServiceMethod="ListarExportadores"
                    MinimumPrefixLength="1" CompletionInterval="500" EnableCaching="true" TargetControlID="txtExportador"
                    runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" />
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Agente/NVOCC:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:TextBox ID="txtNVOCC" runat="server" Width="496px"></asp:TextBox>
                </td>
                <asp:AutoCompleteExtender ID="AutoCompleteNVOCC" ServiceMethod="ListarNVOCC" MinimumPrefixLength="1"
                    CompletionInterval="500" EnableCaching="true" TargetControlID="txtNVOCC" runat="server"
                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" />
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Porto Origem:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbPortoOrigem" runat="server" DataTextField="DESCRICAO_POR"
                        DataValueField="AUTONUM_POR" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Porto Destino:
                </td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:DropDownList ID="cbPortoDestino" runat="server" DataTextField="DESCRICAO_POR"
                        DataValueField="AUTONUM_POR" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px" align="right">
                    Confirmação de E-Mail:</td>
                <td style="border-style: solid; border-width: 1px" align="left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="496px"></asp:TextBox>              
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-width: 1px 1px 0px 1px; border-top-style: solid; border-right-style: solid;
                    border-left-style: solid;" colspan="2">
                    <asp:Panel ID="pnConsulta" runat="server" Height="140px" ScrollBars="Vertical">
                        <asp:GridView ID="dgConsulta" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            EmptyDataText="Nenhum registro encontrado." ForeColor="#333333" ShowHeaderWhenEmpty="True"
                            Font-Size="11px" DataKeyNames="AUTONUM" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <%--<asp:BoundField HeaderText="Exportador" DataField="EXPORTADOR" />--%>
                                <%--<asp:BoundField HeaderText="POD" DataField="PORTO_ORIGEM" />
                <asp:BoundField HeaderText="FDES" DataField="PORTO_DESTINO" />                          --%>
                                <asp:BoundField DataField="AUTONUM" HeaderText="Código" Visible="False" />
                                <asp:BoundField HeaderText="Reserva" DataField="RESERVA" />
                                <asp:BoundField HeaderText="Qtde" DataField="QUANTIDADE" />
                                <asp:BoundField HeaderText="Qtde. Cntr" DataField="QUANTIDADE_CNTR" />
                                <asp:BoundField HeaderText="Peso" DataField="PESO" />
                                <asp:BoundField HeaderText="M3" DataField="METRAGEM_CUBICA" />
                                <asp:BoundField DataField="VOLUMES" HeaderText="Volumes" />
                                <asp:BoundField DataField="NAVIO" HeaderText="Navio" />
                                <asp:BoundField DataField="NUM_VIAGEM" HeaderText="Viagem" />
                                <asp:BoundField DataField="EXPORTADOR" HeaderText="Exportador" />
                                <asp:BoundField DataField="NVOCC" HeaderText="Agente/NVOCC" />
                                <asp:BoundField DataField="ARMADOR" HeaderText="Armador" />
                                <asp:BoundField DataField="PORTO_ORIGEM" HeaderText="Origem" />
                                <asp:BoundField DataField="PORTO_DESTINO" HeaderText="Destino" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="cmdSelecionar" ImageUrl="~/imagens/check.png"
                                            CommandName="SELECIONAR" CommandArgument="<%# Container.DataItemIndex %>" />
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
                    </asp:Panel>
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-top-style: solid; border-top-width: 1px;" colspan="2" align="center">
                    <asp:Button ID="btnSalvar" runat="server" Height="24px" Text="Salvar" Width="78px"
                        CssClass="Botao" />
                    &nbsp;<asp:Button ID="btnExcluir" runat="server" Height="24px" Text="Excluir" Width="78px"
                        CssClass="Botao" />
                    &nbsp;<asp:Button ID="btnSair" runat="server" Height="24px" Text="Sair" Width="78px"
                        CssClass="Botao" />
                    <asp:Label ID="lblAutonum" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

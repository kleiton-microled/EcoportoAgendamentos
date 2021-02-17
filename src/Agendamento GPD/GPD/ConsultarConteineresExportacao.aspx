<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="ConsultarConteineresExportacao.aspx.vb" Inherits="GPD.ConsultarConteineresExportacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


<script type="text/javascript">
    (function ($) {
        $.fn.vAlign = function () {
            return this.each(function (i) {
                var h = $(this).height();
                var oh = $(this).outerHeight();
                var mt = (h + (oh - h)) / 2;
                $(this).css("margin-top", "-" + mt + "px");
                $(this).css("top", "50%");
                $(this).css("position", "absolute");
            });
        };
    })(jQuery);

    (function ($) {
        $.fn.hAlign = function () {
            return this.each(function (i) {
                var w = $(this).width();
                var ow = $(this).outerWidth();
                var ml = (w + (ow - w)) / 2;
                $(this).css("margin-left", "-" + ml + "px");
                $(this).css("left", "50%");
                $(this).css("position", "absolute");
            });
        };
    })(jQuery);

    $(document).ready(function () {

        $("#tbcad").vAlign();
        $("#tbcad").hAlign();

    });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">


 <table cellpadding="4" cellspacing="0" 
                            style="padding-top: 4px; padding-bottom: 4px;" width="100%">
                    <tr>
                        <td align="center" width="50%">
                           
                            <asp:Panel ID="pnBarra" runat="server">
                           
                            <asp:Button ID="btPEsquisar" runat="server" BackColor="WhiteSmoke" 
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Enabled="true" 
                                        Font-Names="Verdana" Font-Size="11px" Text="Pesquisar" Width="80px" />
                            <asp:Button ID="btNovo" runat="server" BackColor="WhiteSmoke" 
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Enabled="true" 
                                        Font-Names="Verdana" Font-Size="11px" Text="Novo" 
                                Width="80px" />
                             </asp:Panel>
                    
                    <asp:Panel ID="PnPesquisa" runat="server" Visible="False">
                        <br />
                        <table align="center" bgcolor="#999999" border="0" cellpadding="1" cellspacing="0"
                            
                            style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;">
                            <tr>
                                <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold;
                                    color: #FFFFFF; border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px;
                                    border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999;"
                                    bgcolor="<%= Session("SIS_COR_PADRAO") %>">
                                    Pesquisar Contêineres
                                </td>
                            </tr>
                            <tr>
                                <td width="70px" align="left" bgcolor="White" style="border-right-style: dotted;
                                    border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px;
                                    border-right-color: #999999; border-bottom-color: #999999">
                                    Reserva:
                                </td>
                                <td align="left" bgcolor="White" style="border-right-style: dotted;
                                    border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px;
                                    border-right-color: #999999; border-bottom-color: #999999" width="100px">
                                    
                                     <asp:DropDownList ID="cbFiltro1" runat="server" 
                    Font-Names="Verdana" Font-Size="11px">
                    <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                    <asp:ListItem Value="1">Contém</asp:ListItem>
                </asp:DropDownList>
                                    
                                    </td>
                                <td width="150px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                                    border-bottom-width: 1px; border-bottom-color: #999999">
                                    <asp:TextBox ID="txtReserva" runat="server" 
                                        Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="70px">
                                    Contêiner:
                                </td>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="100px">
                                    <asp:DropDownList ID="cbFiltro2" runat="server" Font-Names="Verdana" 
                                        Font-Size="11px" Enabled="False">
                                        <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                        <asp:ListItem Value="1">Contém</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999" width="150px">
                                    <asp:TextBox ID="txtConteiner" runat="server" CssClass="Container" Font-Names="Verdana"
                                        Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
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
                    <tr>
                        <td align="center" width="50%">
	  
<asp:GridView ID="dgConsulta" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="2" CellSpacing="1" EmptyDataText="Nenhum Contêiner Cadastrado."
            Font-Names="Verdana" Font-Size="10px" ForeColor="#333333" 
        GridLines="None" ShowHeaderWhenEmpty="True"
            Width="500px" BorderColor="#999999" BackColor="#999999" 
            DataKeyNames="AUTONUM_GD_CNTR,ID_CONTEINER" AllowPaging="True" DataSourceID="dsConsulta" 
                                PageSize="15">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>             
                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagens/editar.gif" 
                    Text="Button" CommandName="EDIT">
                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:ButtonField>


               <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:TemplateField>


                <asp:BoundField DataField="AUTONUM_GD_CNTR" HeaderText="AUTONUM_GD_CNTR" 
                    Visible="False" />
                <asp:BoundField DataField="REFERENCE" HeaderText="REFERENCE">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ID_CONTEINER" HeaderText="CONTÊINER">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="AUTONUM_GD_CNTR,REFERENCE" 
                    DataNavigateUrlFormatString="AgendamentoExportacao.aspx?conteiner={0}&amp;reserva={1}&amp;action=edit" 
                    DataTextFormatString="Agendar" Text="Agendar">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="TIPO_TRANSPORTE" HeaderText="TIPO TRANSPORTE" Visible="false" />
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

                            <asp:SqlDataSource ID="dsConsulta" runat="server"></asp:SqlDataSource>

                        </td>
                    </tr>
                </table>

</form>

</asp:Content>

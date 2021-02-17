<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="ConsultarRecTransp.aspx.vb" Inherits="GPD.ConsultarRecTransp" %>
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
    <form id="frmConsRecintos" runat="server" style="margin-left: 15%; margin-right: 15%;">
        <asp:Panel ID="pnlBarra" runat="server">
            <table align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding-right: 20px;" height="22px" colspan="2">
                        <asp:Button ID="btNovo" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Nova Vinculação"
                            Width="100px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Selecione a opção de consulta:</td>
                    <td align="center" style="padding-right: 20px;" height="22px">
                        <asp:DropDownList ID="cbOpcao" runat="server" Width="250px" AutoPostBack = "true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlConsultas" runat="server">
            <asp:GridView ID="dgTranspRec" runat="server" AutoGenerateColumns="false" CellPadding="1"
                ForeColor="#333333" GridLines="None" CellSpacing="1" Width="100%" EmptyDataText="Nenhum registro encontrado."
                ShowHeaderWhenEmpty="true" BackColor="#999999" Font-Names="Verdana" Font-Size="10px"
                DataKeyNames="AUTONUM" AllowPaging="true" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                
                <Columns>
                    <asp:BoundField DataField="AUTONUM" Visible="false" />
                    <asp:BoundField HeaderText="DESCRIÇÃO" DataField="DESCR"> 
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="450px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DESCRIÇÃO RESUMIDA" DataField="DESCR_RESUMIDA">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DESCRIÇÃO RECINTO" DataField="DESCR_RECINTO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ATIVO" DataField="ATIVO">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>

                    <asp:ButtonField ButtonType="Image" CommandName="EDIT" ImageUrl="~/imagens/editar.gif">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:ButtonField>
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>">
                            </asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" VerticalAlign="middle" Width="20px" />
                    </asp:TemplateField>

                </Columns>
                
                <EditRowStyle BackColor="#999999" />
                <EmptyDataRowStyle BackColor="#E9E9E9" HorizontalAlign="Center" VerticalAlign="Middle" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="CorPadrao" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" /> 
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

            </asp:GridView>
        </asp:Panel>
    </form>
</asp:Content>

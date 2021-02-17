<%@ Page Title="" Language="vb" enableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/master/Site.Master"
    CodeBehind="EtapasConteiner.aspx.vb" Inherits="GPD.EtapasConteiner" %>

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
    <form id="form1" runat="server" style="margin-top:20px;">
         


   <div id="Grid-Scroll" style="overflow: auto; margin-left: 20px; margin-right: 20px;">
         <asp:Button ID="btnVoltar" runat="server" Text="Voltar" />
            <br />
              <asp:Label ID="Label1" runat="server" Font-size-="12" Font-Bold="true"  > </asp:Label> 
       <br />
        <asp:GridView ID="DgEtapasConteiner" runat="server" AutoGenerateColumns="False" CellPadding="1"
            ForeColor="#333333" GridLines="None" CellSpacing="1" Width="100%" EmptyDataText="Nenhum registro encontrado."
            ShowHeaderWhenEmpty="True" BackColor="#999999" Font-Names="Verdana" Font-Size="10px"
             DataSourceID="DsConsulta"  DataKeyNames="TIPO_DOC,FLAG_CERTIFICADO,FORMA_PAGAMENTO"
           >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>


                <asp:BoundField DataField="Patio" HeaderText="Patio">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>   
                <asp:BoundField HeaderText="Lote" DataField="Lote">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Tipo_Documento" HeaderText="Documento">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Num Doc." DataField="Num_Documento">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="BL" DataField="Numero">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Data_Free_Time" HeaderText="Free Time">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Averbou" HeaderText="Averbou" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MAPA_DE_MADEIRA" HeaderText="Mapa Madeira" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DI_DESEMBARACADA" HeaderText="Desembaraçada" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DI_ICMS_SEFAZ" HeaderText="ICMS SEFAZ" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SISCARGA" HeaderText="SisCarga" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GR_PAGA" HeaderText="GR Paga" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BLOQUEIO_BL" HeaderText="Bloqueio BL" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BLOQUEIO_CNTR" HeaderText="Bloqueio CNTR" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField> 
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataRowStyle BackColor="#E9E9E9" HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
       <asp:SqlDataSource ID="DsConsulta" runat="server"></asp:SqlDataSource>
    </div>

    </form>
</asp:Content>

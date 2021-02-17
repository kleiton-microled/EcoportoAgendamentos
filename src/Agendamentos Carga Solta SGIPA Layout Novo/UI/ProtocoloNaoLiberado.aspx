<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master" CodeBehind="ProtocoloNaoLiberado.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.ProtocoloNaoLiberado" %>

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

    <table id="tbcad" width="600px" style="background-color: #ffffff; border: 1px solid black;">
        <tr>
            <td>
                <center>
                    <br />
                    <asp:Label ID="lblMsg" Font-Size="14px" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:HyperLink ID="lnkAgendamentos" runat="server" NavigateUrl="~/ConsultarAgendamentos.aspx">Ir para Agendamentos</asp:HyperLink>
                    <br />
                    <br />
                </center>
            </td>
        </tr>
    </table>

</asp:Content>

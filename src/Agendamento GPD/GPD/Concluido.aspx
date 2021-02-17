<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Site.Master" CodeBehind="Concluido.aspx.vb" Inherits="GPD.Concluido" %>

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
                    <p style="font-size: 14px; font-weight: bold;">Aguarde.</p>
                    <br />
                    <p style="font-size: 14px; font-weight: bold;">As documentações anexadas serão analisadas pelo setor responsável para que se possa fazer a impressão do protocolo.</p>
                    <br />
                    <p style="font-size: 12px;"">Caso tenha divergência será enviado um e-mail para o endereço informado abaixo:</p>
                    <br />
                    <asp:Label ID="lblEmail" runat="server" Text="" Font-Size="14px" ForeColor="#990000" Font-Bold="true"></asp:Label>
                     <br />
                    <br />
                    <asp:HyperLink ID="lnkAgendamentos" runat="server" NavigateUrl="~/ConsultarAgendamentosImportacao.aspx">Ir para Agendamentos</asp:HyperLink>
                     <br />
                    <br />
                </center>
            </td>
        </tr>
    </table>

</asp:Content>

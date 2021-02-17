<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="AgendamentoRecusado.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.AgendamentoRecusado" %>

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

    <style type="text/css">
        .auto-style1 {
            font-family: verdana;
            font-weight: bold;
            font-size: 11px;
            color: #FFFFFF;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">

        <table id="tbcad" align="center" cellpadding="2" cellspacing="1" width="500px" bgcolor="#CCCCCC">
            <tr>
                <td align="center" class="auto-style1" bgcolor="<%= Session("SIS_COR_PADRAO") %>">Agendamento Recusado</td>
            </tr>
            <tr>
                <td style="font-family: verdana; font-size: 11px" bgcolor="White">
                    <br />
                    <br />
                    Mensagem:
                    <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                    <br />
                    <br />
                    <center>
                        <asp:LinkButton ID="lnkVoltar" runat="server" PostBackUrl="~/ConsultarAgendamentos.aspx">Voltar</asp:LinkButton>
                    </center>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </form>
</asp:Content>

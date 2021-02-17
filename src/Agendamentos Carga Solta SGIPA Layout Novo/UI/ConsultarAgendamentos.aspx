<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false"  MasterPageFile="~/master/Consultas.Master" Inherits="AgendamentoCargaSoltaSGIPA.ConsultarAgendamentos" CodeBehind="ConsultarAgendamentos.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/estilos.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <link href="Content/fontawesome-all.css" rel="stylesheet" />
    <link href="Content/toastr.css" rel="stylesheet" />
    <link href="Content/select2.css" rel="stylesheet" />
    <link href="Content/jquery.dataTables.min.css" rel="stylesheet" />


    <link href="Content/plugins/smartWizard/css/smart_wizard.min.css" rel="stylesheet" />
    <link href="Content/plugins/smartWizard/css/smart_wizard_theme_dots.min.css" rel="stylesheet" />
    <link href="Content/plugins/smartWizard/css/smart_wizard_theme_arrows.min.css" rel="stylesheet" />
    <link href="Content/plugins/smartWizard/css/smart_wizard_theme_circles.min.css" rel="stylesheet" />
    <link href="Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="row mt-3">
            <div class="col-lg-10 offset-lg-1">
                <div class="card">
                    <div class="card-header">
                        <i class="fa fa-table fa-lg"></i>
                        Agendamentos

                        <div class="float-right ">
                            <a href="CadastrarAgendamentos.aspx"  class="btn btn-info btn-sm btn-block btn-as-block"><i class="fa fa-edit"></i>&nbsp;Cadastrar Agendamento</a>
                            <%--<a href="#"  class="btn btn-info btn-sm btn-novo"><i class="fa fa-edit"></i>&nbsp;Cadastrar Agendamento</a>--%>
                        </div>
                    </div>

                    <div class="card-body">
                        <asp:Panel ID="PanelBarra" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label for="cbControl">Ação</label>
                                        <asp:DropDownList ID="cbModoImpressao" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Imprimir Selecionados</asp:ListItem>
                                            <asp:ListItem Value="1">Imprimir Todos</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <asp:Button ID="btImprimir" runat="server" OnClientClick="return confirm('Confirma a impressão do(s) Protocolo(s)?');" Text="OK" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelPesquisa" runat="server" Visible="False">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label for="cbFiltro1">Protocolo:</label>
                                </div>
                                  <div class="form-group col-md-3">
                                      <asp:DropDownList ID="cbFiltro1" runat="server"
                                            Font-Names="Verdana" Font-Size="11px" Enabled="True">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                                  <div class="form-group col-md-3">
                                    <label for="txtProtocolo">Protocolo:</label>
                                      <asp:TextBox ID="txtProtocolo" runat="server" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                  </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label for="cbFiltro2">Cavalo:</label>
                                    <asp:DropDownList ID="cbFiltro2" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                    
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:TextBox ID="txtCavalo" runat="server" CssClass="Placa" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label for="cbFiltro3">Carreta</label>
                                    <asp:DropDownList ID="cbFiltro3" runat="server" Font-Names="Verdana" Font-Size="11px">
                                        <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                        <asp:ListItem Value="1">Contém</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                     <asp:TextBox ID="txtCarreta" runat="server" CssClass="Placa" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label for="">CNH:</label>
                                    <asp:DropDownList ID="cbFiltro4" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                            <div class="form-group col-md-3">    
                                <asp:TextBox ID="txtCNH" runat="server" CssClass="Numeric" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                            </div>

                            </div>

                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>Motorista:</label>
                                    <asp:DropDownList ID="cbFiltro5" runat="server" Font-Names="Verdana"
                                        Font-Size="11px">
                                        <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                        <asp:ListItem Value="1">Contém</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:TextBox ID="txtMotorista" runat="server" CssClass="AlphaNumeric" Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>Documento:</label>
                                    <asp:DropDownList ID="cbFiltro6" runat="server" Font-Names="Verdana"
                                            Font-Size="11px">
                                            <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                            <asp:ListItem Value="1">Contém</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="Numeric"
                                            Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px"
                                            Width="200px"></asp:TextBox>
                                </div>                            
                            </div>

                            <div class="row">
                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <asp:LinkButton id="btLimpar" runat="server" CssClass="btn btn-warning btn-block" Enable="false"><i class="fa fa-eraser"></i>&nbsp;&nbsp;Limpar</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btFiltrar"  Width="100%" runat="server" CssClass="btn btn-primary"><span aria-hidden="true" class="fa fa-search"></span>&nbsp;&nbsp;Pesquisar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-3">
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="form-group col-md-3">
                                </div>
                            </div>

                      
                        </asp:Panel>
                        <div class="row ">
                            <div class="col-sm-12 mt-1">
                                <div class="table-responsive">
                            <%--<asp:GridView ID="DgAgendamentos" ClientIDMode="Static" runat="server" DataKeyNames="AUTONUM_AG_CS,DT_CHEGADA"
                                CssClass="table table-hover table-sm grdViewTable data-table-grid" GridLines="None" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" EmptyDataText="Nenhum registro encontrado."
                                ShowHeaderWhenEmpty="True">--%>
                                <asp:GridView ID="DgAgendamentos" DataKeyNames="AUTONUM_AG_CS,DT_CHEGADA" CssClass="table table-hover table-sm grdViewTable data-table-grid" GridLines="None" CellSpacing="-1" runat="server" OnRowCommand="DgAgendamentos_RowCommand" OnRowDataBound="DgAgendamentos_RowDataBound" AutoGenerateColumns="false">
                                <Columns>
<%--                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDIT"
                                                CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="AUTONUM_AG_CS" HeaderText="AUTONUM_AG_CS" Visible="False" SortExpression="AUTONUM_AG_CS">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Motorista" DataField="NOME_MOTORISTA" SortExpression="NOME_MOTORISTA"/>
                                       
                                    <asp:BoundField HeaderText="CNH" DataField="CNH" SortExpression="CNH"/>
                                    <asp:TemplateField HeaderText="Placa Cavalo Carreta">
                                        <ItemTemplate>
                                            <%# Eval("PLACA_CAVALO") + " " + Eval("PLACA_CARRETA")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PERIODO" HeaderText="Período" />
                                    <asp:BoundField HeaderText="Nº Doc. Saída" DataField="NUM_DOC_SAIDA" SortExpression="NUM_DOC_SAIDA"/>
                                    <asp:BoundField HeaderText="Lote" DataField="LOTE" SortExpression="LOTE"/>
                                    <asp:BoundField HeaderText="Nº BL" DataField="NUMERO_BL" SortExpression="NUMERO_BL" />
                                    <asp:TemplateField HeaderText="Documento">
                                        <ItemTemplate>
                                            <%# Eval("DESCR_DOCUMENTO") + " " + Eval("NUM_DOCUMENTO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DT_FREE_TIME" HeaderText="Data Free Time" SortExpression="DT_FREE_TIME" />
                                    <asp:HyperLinkField DataNavigateUrlFields="AUTONUM_AG_CS" DataNavigateUrlFormatString="Protocolo.aspx?id={0}"
                                        DataTextField="PROTOCOLO" HeaderText="Protocolo/Impressão"
                                        Target="_blank" />
                                    <asp:BoundField HeaderText="Impresso" DataField="IMPRESSO" SortExpression="IMP" />
                                    <asp:BoundField DataField="DT_CHEGADA" HeaderText="Chegada" SortExpression="DT_CHEGADA" />
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" Visible="False" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckProtocolo" runat="server" CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="cmdEditar" CssClass="btn btn-sm btn-info btn-block" CommandArgument="<%# Container.DataItemIndex %>" CommandName="EDIT">
                                                                    <i class='fa fa-edit'></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="campo-acao" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <%--<a href="#" class="btn btn-danger btn-sm" onclick="excluirItem('<%# Eval("AUTONUM_TI") %>',this)" data-toggle="modal" role="button" data-target="#modal-exclusao" title="Excluir"><i class="fa fa-trash"></i></a>--%>
                                            <asp:LinkButton runat="server" ID="cmdExcluir" class="btn btn-danger btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="DEL">
                                                                    <i class='fa fa-edit'></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                            </div>
                        </div>
                    

                        
                    </div>
                </div>
            </div>

        </div>
       
    </form>
    <script type="text/javascript" src="scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="scripts/toastr.js"></script>
    <script type="text/javascript" src="scripts/select2.min.js"></script>
    <%--<script type="text/javascript" src="Content/plugins/smartWizard/js/jquery.smartWizard.min.js"></script>
    <script type="text/javascript" src="Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.js"></script>--%>
    <script type="text/javascript" src="scripts/jquery.dataTables.min.js"></script>

    
     <script type="text/javascript">
        $(document).ready(function () {

            // SmartWizard initialize
            //$('#smartwizard').smartWizard({
            //    lang: {
            //        next: 'Próximo',
            //        previous: 'Anterior'
            //    }
            //});

           $('#<%= DgAgendamentos.ClientID %>').DataTable({
                "bLengthChange": false,
                "bFilter": true,
                "language": {
                    "url": "Scripts/pt-br.json"
                }
            });

           <%--$('#<%= DgAgendamentos.ClientID %>').DataTable({
                "drawCallback": function (settings) {
                    AddBotaoFiltrar();
                },
                "dom": '<"botao-filtrar">frtip',
                "bLengthChange": false,
                "bFilter": true,
                "language": {
                    "url": "Scripts/pt-br.json"
                },
                "order": [],
            });--%>
        });
     </script>
</asp:Content>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master" CodeBehind="CadastrarAgendamentos.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.CadastrarAgendamentos" %>
<%--<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master" CodeBehind="ConsultarLotesImportacao.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.ConsultarLotesImportacao" %>--%>
<%--<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" Inherits="AgendamentoCargaSoltaSGIPA.CadastrarAgendamentos" CodeBehind="CadastrarAgendamentos.aspx.vb" %>--%>

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

    <link href="Content/plugins/easyAutocomplete/easy-autocomplete.css" rel="stylesheet" />
    

    <style type="text/css">
        * {
            padding: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }

        .QtdItens {
            text-align: center;
        }

        .PromptCSS {
            color: black;
            font-weight: bold;
            background-color: #EEE8AA;
            font-family: Arial;
            font-size: 10pt;
            border: solid 1px black;
        }

        .pnl-erro {
            padding: 6px;
            margin-bottom: 4px;
            width: 1100px;
            background-color: #F08080;
            color: white;
            font-weight: bold;
            border: 1px solid black;
        }
    </style>

    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <form id="form2" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:HiddenField ID="hddnTransportadora"  runat="server" Value="0"/>

                    <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="card">
                    <div class="card-header">
                    <i class="fa fa-calendar-alt fa-lg"></i>
                        Detalhes do Agendamento
                    <div class="float-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                
                <div class="card-body">
                    <div id="smartwizard" class="sw-main sw-theme-arrows">
                         <ul class="nav">
                            <li><a class="nav-link" href="#motorista">Passo 1<br /><small>Escolha do Motorista</small></a></li>
                            <li><a class="nav-link" href="#veiculo">Passo 2<br /><small>Escolha do Veículo</small></a></li>
                            <li><a class="nav-link" href="#carregamento">Passo 3<br /><small>Informações da Carga</small></a></li>
                            <li><a class="nav-link" href="#periodo">Passo 4<br /><small>Escolha do Período</small></a></li>
                        </ul>
                         <div class="mt-3">
                             <div id="motorista" class="">
                                 <div class="row">
                                     <div class="form-group col-md-6">
                                         <label for="txtMotorista">Motorista:</label>
                                         <asp:TextBox ID="txtMotorista" runat="server" ClientIDMode="Static"   CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <div class="form-group col-md-2">
                                        <label for="txtCNH">CNH:</label>
                                         <asp:TextBox ID="txtCNH" runat="server" ClientIDMode="Static"   CssClass="form-control"></asp:TextBox>
                                      </div>
                                 </div>
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label for="txtNomeMotorista">Nome Motorista:</label>
                                         <asp:TextBox ID="txtNomeMotorista" runat="server" ClientIDMode="Static"  Enabled="False" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2 ">
                                        <label for="cbCNH">CNH:</label>
                                        <asp:DropDownList ID="cbCNH" runat="server" ClientIDMode="Static"  Enabled="False" CssClass="form-control selecionar" data-placeholder="Selecione a CNH"></asp:DropDownList>
                                    </div>
                                
                                     <div class="form-group col-md-2">
                                         <label for="txtValidadeCNH">Validade CNH:</label>
                                         <asp:TextBox ID="txtValidadeCNH" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <div class="form-group col-md-2">
                                         <label for="txtCPF">CPF:</label>
                                         <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                    </div>
                                 <div class="row mt-3">
                                    <div class="form-group col-md-2">
                                         <label for="txtCelular">Celular:</label>
                                         <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>  
                                     <div class="form-group col-md-2">
                                         <label for="txtNextel">Nextel:</label>
                                         <asp:TextBox ID="txtNextel" runat="server"  CssClass="form-control"></asp:TextBox>
                                     </div>
                                 </div>

                             </div>
                             <div id="veiculo" class="">
                                 <div class="row">
                                      <div class="form-group col-md-3 ">
                                        <label for="cbCavalo">Placa Cavalo:</label>
                                            <asp:DropDownList ID="cbCavalo" runat="server" Enabled="False" AutoPostBack="True" CssClass="form-control selecionar" data-placeholder="Selecione a Placa do Cavalo"></asp:DropDownList>
                                    </div>
                                     <div class="form-group col-md-3">
                                         <label for="cbCarreta">Placa Carreta:</label>
                                         <asp:DropDownList ID="cbCarreta" runat="server" AutoPostBack="True" CssClass="form-control selecionar" data-placeholder="Selecione a Placa da Carreta"></asp:DropDownList>
                                     </div>
                                     <div class="form-group col-md-3">
                                         <label for="txtTara"> Tara</label>
                                         <asp:TextBox ID="txtTara" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>

                                     </div>
                                     <div class="form-group col-md-3">
                                         <label for="txtChassi">Chassi</label>
                                         <asp:TextBox ID="txtChassi" runat="server" CssClass="form-control AlphaNumeric" Enabled="False" ></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="row">
                                 <div class="col-sm-2">
                                     <div class="form-group">
                                         <label class="control-label">&nbsp;</label>
                                         <div class="checkbox text-left">
                                             <asp:CheckBox ID="CheckAutonomo" runat="server" Enabled="false" AutoPostBack="True" Text="Autônomo" />
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             </div>
                             
                             <div id="carregamento" class="">
                                 <div class="row">
                                     <div class="form-group col-lg-3">
                                         <label for="txtNotaFiscal">Nota Fiscal</label>
                                         <asp:TextBox ID="txtNotaFiscal" runat="server" Width="80px" CssClass="AlphaNumeric" Font-Names="Verdana" Font-Size="12px" Enabled="False" MaxLength="10"></asp:TextBox>
                                     </div>
                                     <div class="form-group col-lg-3">
                                         <label for="txtSerie">Série:</label>
                                         <asp:TextBox ID="txtSerie" runat="server" CssClass="form-control AlphaNumeric" Enabled="False" MaxLength="4"></asp:TextBox>
                                     </div>
                                     <div class="form-group col-lg-3">
                                         <label for="txtDataEmissao">Emissão:</label>
                                         <asp:TextBox ID="txtDataEmissao" runat="server" CssClass="form-control Calendario" Enabled="False" MaxLength="10"></asp:TextBox>
                                     </div>
                                     <div class="form-group col-lg-3">
                                         <label for="cbItensCS">Carga Solta</label>
                                         <asp:DropDownList ID="cbItensCS" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control"></asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="row">
                                     <div class="form-group col-lg-3">
                                         <label for="txtQtde">Quantidade:</label>
                                         <asp:TextBox ID="txtQtde" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="row">
                                     <div class="form-group col-lg-3 col-lg-offset-6">
                                         <asp:Button ID="btnAdicProduto" runat="server" Text="Adicionar Nota Fiscal" BorderColor="#999999"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9px"
                                                                    Width="115px" Height="18px" CommandName="ADICIONAR" />
                                      </div>
                                     <div class="col-lg-3">
                                         <asp:Button ID="btnExcluirProduto" runat="server" Text="Excluir Notas Fiscais" BorderColor="#999999"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9px"
                                                                    Width="115px" Height="18px" Visible="false" OnClientClick="return confirm('Deseja excluir as Notas Fiscais deste agendamento?');" />
                                     </div>
                                 </div>
                                 <asp:Panel ID="PanelPatioInvalido" runat="server" Visible="false">
                                     <div class="row">
                                         <div class="form-group col-lg-12">
                                             <asp:Label ID="lblPatioInvalido" runat="server" ForeColor="Red"></asp:Label>
                                         </div>
                                     </div>
                                 </asp:Panel>

                                 <hr class="separador" />

                                 <div class="row">
                                     <div class="col-lg-12 mt-1">
                                         <div class="table-responsive">
                                              <asp:Panel ID="PanelScrollNotas" runat="server" ScrollBars="Vertical" Height="113px"
                                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgNotas" ClientIDMode="Static" runat="server" DataKeyNames="AUTONUM,AUTONUM_PRODUTO,LOTE,NOTAFISCAL,TIPO,SERIE,EMISSAO,QTDE"
                                                            CssClass="table table-hover table-sm grdViewTable" GridLines="None"  AutoGenerateColumns="False"
                                                            CellSpacing="1" CellPadding="4"
                                                            EmptyDataText="Nenhuma Nota Fiscal com Carga Solta foram vinculados." 
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="AUTONUMAGENDAMENTO" HeaderText="AUTONUMAGENDAMENTO" Visible="False" />
                                                                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False"></asp:BoundField>
                                                                <asp:BoundField DataField="NOTAFISCAL" HeaderText="NOTA FISCAL">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SERIE" HeaderText="SÉRIE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMISSAO" HeaderText="EMISSÃO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TIPO" HeaderText="DOCUMENTO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LOTE" HeaderText="LOTE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PATIO" HeaderText="PATIO">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PRODUTO" HeaderText="PRODUTO" HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMBALAGEM" HeaderText="EMBALAGEM" HtmlEncode="False">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QTDE" HeaderText="QTDE">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AUTONUM_PRODUTO" HeaderText="AUTONUM_PRODUTO"
                                                                    HtmlEncode="False" Visible="False" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif"
                                                                            CommandName="EDITAR" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                         </div>
                                     </div>
                                 </div>

                                 <div class="row">
                                     <div class="col-lg-12 mt-1">
                                         <iframe id="frameDocumentos" runat="server" height="350px" scrolling="auto" visible="false" width="100%"></iframe>
                                     </div>
                                 </div>
                                  <div class="row">
                                     <div class="col-lg-12 mt-1">
                                         <iframe id="frameEmails" runat="server" height="350px" scrolling="auto" visible="false" width="98%"></iframe>
                                     </div>
                                 </div>


                             </div>
                             <div id="periodo" class="">
                                 <div class="row">
                                     <div class="form-group">
                                         <label for="lblPeriodo">Período</label>
                                         <asp:Label ID="lblPeriodo" runat="server" CssClass="form-control" Text="Nenhum período foi selecionado." Visible="False"></asp:Label>
                                     </div>
                                     <div class="form-group">
                                         <label for="lblProtocolo">Nº Protocolo</label>
                                         <asp:Label ID="lblProtocolo" runat="server" CssClass="form-control">Não Gerado</asp:Label>
                                         <asp:Label ID="lblID" runat="server" CssClass="form-control" Visible="False"></asp:Label>
                                     </div>
                                 </div>
                                 <div class="row">
                                     <div class="form-group col-lg-12">
                                          <asp:Panel ID="PanelScrollPeriodos" runat="server" ScrollBars="Vertical"
                                            Height="277px">
                                            <asp:GridView ID="dgPeriodos" ClientIDMode="Static" runat="server"
                                                DataKeyNames="AUTONUM_GD_RESERVA,PERIODO_INICIAL,PERIODO_FINAL,SALDO,FLAG_DTA"
                                              CssClass="table table-hover table-sm grdViewTable"
                                                GridLines="None"
                                                AutoGenerateColumns="False"
                                                 CellPadding="0" CellSpacing="1"
                                                EmptyDataText="Nenhum período disponível."  ShowHeader="true">
                                                <Columns>
                                                    <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="AUTONUM" Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODO_INICIAL" HeaderText="PERÍODO INICIAL">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODO_FINAL"
                                                        HeaderText="PERÍODO FINAL">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SALDO" HeaderText="SALDO">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FLAG_DTA"
                                                        HeaderText="FLAG_DTA" Visible="False" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/imagens/unchecked.png" Text="Button">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
                                        </asp:Panel>
                                     </div>
                                 </div>
                             </div>
                        </div>
                        </div>
                </div>
            </div>
        </div>

                    </div>
           

                <asp:Panel ID="pnlMsgErro" runat="server" Visible="false" CssClass="pnl-erro">
                    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
                </asp:Panel>

                <table align="center" style="width: 1100px;">
                    <tr>
                        <td valign="top">
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Informações Veículo / Motorista
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                        <td valign="top" align="left">
                            <table bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                width="350px">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Transportadora
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="left" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="right" width="80px">Razão Social:
                                                            </td>
                                                            <td align="right">
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="right" width="80px" height="22px">Período:</td>
                                                            <td align="right" colspan="2">&nbsp;
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            
                                                            <td align="right">
                                                                <asp:Button ID="btNovo" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"  Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Novo" Width="50px" />
                                                                <asp:Button ID="btSalvar" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10px" Height="18px" Text="Salvar" Width="50px" />
                                                                <asp:Button ID="btExcluir" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="10px" Height="18px" OnClientClick="return confirm('Confirma a exclusão do agendamento?');" Text="Excluir" Width="50px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Informações Documentação</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                            <tr>
                                                <td width="110px" align="left">Tipo Doc. Saída:</td>
                                                <td align="left" width="220px">
                                                    <asp:DropDownList ID="cbTipoDocSaida" runat="server" Width="220px" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="CR">Conhecimento Rodoviário (CR)</asp:ListItem>
                                                        <asp:ListItem Value="MN">Minuta (MN)</asp:ListItem>
                                                        <asp:ListItem Value="OC">Ordem de Coleta (OC)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">Série Doc. Saída:</td>
                                                <td align="right" colspan="2">
                                                    <asp:TextBox ID="txtSerieDocSaida" runat="server" Width="110px" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" MaxLength="15"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" align="left">Nro. Doc. Saída:</td>
                                                <td align="left" width="220px">
                                                    <asp:TextBox ID="txtNrDocSaida" runat="server" Width="216px" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" CssClass="Numeric"></asp:TextBox>
                                                </td>
                                                <td align="right">Emissão Doc. Saída:
                                                </td>
                                                <td align="right" colspan="2">
                                                    <asp:TextBox ID="txtEmissaoDocSaida" runat="server" Width="110px"
                                                        CssClass="Calendario" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="110px" align="left">Lote/BL:
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="cbLote" runat="server" Width="100%" Font-Names="Verdana"
                                                        Font-Size="12px" Enabled="False" DataTextField="DESCR"
                                                        DataValueField="LOTE" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    &nbsp;</td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="2" valign="top">
                            <table bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                width="350px">
                                <tr>
                                    <td align="center" bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF">
                                        <table cellspacing="0" width="100%" align="center">
                                            <tr>
                                                <td align="center" width="150px">Período Inicial
                                                </td>
                                                <td align="center" width="150px">Período Final
                                                </td>
                                                <td align="center" width="50px">Saldo
                                                </td>
                                                <td width="50px">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" style="height: auto;">
                                       



                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px"
                                bgcolor="GhostWhite" width="100%">
                                <tr>
                                    <td bgcolor="<%= Session("SIS_COR_PADRAO") %>" style="font-family: verdana; font-size: 11px; font-weight: bold; color: #FFFFFF"
                                        align="left">Nota Fiscal
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="font-family: verdana; font-size: 11px" width="100%">
                                           




                                            <tr>
                                                <td align="center" colspan="6">
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                            
                        </td>
                        <td align="center" bgcolor="GhostWhite" style="border-style: solid; border-width: 1px; border-color: #999999">
                            
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2">
                            <div style="text-align: left">
                                <asp:Panel ID="pnLegenda" runat="server" Visible="False">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10px" Text="CARGA DISPONÍVEL NO LOTE/BL:"></asp:Label>
                                    <asp:Label ID="lblLoteSelecionado1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#CC0000"></asp:Label>
                                    <asp:Label ID="lblPatio" runat="server" Visible="False"></asp:Label>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>

                </table>
        </form>

  
       
    

    <script type="text/javascript" src="scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="scripts/toastr.js"></script>
    <script type="text/javascript" src="scripts/select2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.dataTables.min.js"></script>
    
    <script type="text/javascript" src="Content/plugins/smartWizard/js/jquery.smartWizard.min.js"></script>
    <script type="text/javascript" src="Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.js"></script>
    <script type="text/javascript" src="scripts/agendamento.js"></script>
    <script type="text/javascript" src="Content/plugins/easyAutocomplete/jquery.easy-autocomplete.js"></script>

    
    <script type="text/javascript">
        $(document).ready(function () {

            // SmartWizard initialize
            $('#smartwizard').smartWizard({
                lang: {
                    next: 'Próximo',
                    previous: 'Anterior'
                }
            });

            $('.selecionar').select2({
                language: 'pt-BR',
                width: 'resolve'
            });


            //

            function ChecaMotorista(valor) {
                if (valor.Liberado === 1) {
                    $("#CPF").val(valor.CPF);
                    $("#Motorista").val(valor.Nome);
                    $('#MotoristaId').val(valor.MotoristaId);
                    $("#CNH").val(valor.CNH);
                    $("#TransportadoraId").val(valor.TransportadoraId);
                    $("#TransportadoraRazao").val(valor.RazaoSocial);
                }
                else {
                    toastr.error('Motorista não liberado!', 'Coletor');
                    $("#CPF").val('');
                }
            }

           

            var optionsMotorista = {
                url: function (phrase) {

                    //return '@Url.Action("ObterMotoristas", "Transito")' + "?termo=" + phrase + "&idTransportadora=" + idTransportadora;

                    var transportadoraId = '<%= Session("SIS_ID") %>';

                    var postData = { termo: phrase, transportadora: transportadoraId };
                    return $.ajax({
                        type: "POST",
                        url: "CadastrarAgendamentos.aspx/ObterMotoristas",
                        data: JSON.stringify(postData),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            //return resultado.d;

                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))  
                            //$(resultado.d).each(function () {
                            //    var htmlText = this.Nome;
                            //    console.log(htmlText);
                            //});
                        },
                        error: function (response) {
                            console.log(response.d);
                            var json = JSON.parse(response.responseText);

                            if (json != null) {
                                console.log(json.Message, 'Agendamento');
                            }
                        }
                    });

                },
                getValue: "Nome",
                listLocation: "dados",
                requestDelay: 500,
                template: {
                    type: "custom",
                    method: function (value, item) {
                        return `${value} | Nome: ${item.Nome}  `;
                    }
                }
            };

            var pesquisarMotorista = function () {
                var valor = $("#txtMotorista").getSelectedItemData();
                //ChecaMotorista(valor);
            }

            optionsMotorista.list = {
                onClickEvent: function () {
                    pesquisarMotorista();
                },
                onKeyEnterEvent: function () {
                    pesquisarMotorista();
                }
            }

            $("#txtMotorista").easyAutocomplete(optionsMotorista);

            //var optionsCNH = {
            //    url: function (phrase) {
            //        var idTransportadora = $('#TransportadoraId').val();
            //        console.log(idTransportadora);
            //        return '@Url.Action("ObterCNHs", "Transito")' + "?termo=" + phrase + "&idTransportadora=" + idTransportadora;
            //    },
            //    getValue: "CNH",
            //    listLocation: "dados",
            //    requestDelay: 500,
            //    template: {
            //        type: "custom",
            //        method: function (value, item) {
            //            return `${value} | CPF: ${item.CPF}  `;
            //        }
            //    }
            //};

            //var pesquisarCNH = function () {
            //    var valor = $("#CNH").getSelectedItemData();
            //    ChecaMotorista(valor);
            //}

            //optionsCNH.list = {
            //    onClickEvent: function () {
            //        pesquisarCNH();
            //    },
            //    onKeyEnterEvent: function () {
            //        pesquisarCNH();
            //    }
            //}

            //$("#CNH").easyAutocomplete(optionsCNH);

            //

        });

        $(document).on('change', "#cbCNH", function () {
            alert($(this).val())
            ObterDadosMotoristaPorCNH($(this).val());
        });

        $(document).on('change', "#txtNomeMotorista", function () {
            alert($(this).val())
            ObterDadosMotoristaPorMotorista($(this).val());
        });

        function ObterDadosMotoristaPorCNH(cnh) {
            
            var transportadoraId = '<%= Session("SIS_ID") %>';

            var postData = { cnh: cnh, transportadora: transportadoraId};
            $.ajax({
                type: "POST",
                url: "CadastrarAgendamentos.aspx/ObterDadosMotoristaPorCNH",
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    $(resultado.d).each(function () {
                        var htmlText = this.Nome;
                        console.log(htmlText);
                    });
                },
                error: function (response) {
                    console.log(response.d);
                    var json = JSON.parse(response.responseText);

                    if (json != null) {
                        console.log(json.Message, 'Agendamento');
                    }
                }
            });

        }

        function ObterDadosMotoristaPorNome(nome) {

            var transportadoraId = '<%= Session("SIS_ID") %>';

            var postData = { nome: nome, transportadora: transportadoraId };
            $.ajax({
                type: "POST",
                url: "CadastrarAgendamentos.aspx/ObterDadosMotoristaPorNome",
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    $(resultado.d).each(function () {
                        var htmlText = this.CNH;
                        console.log(htmlText);
                    });
                },
                error: function (response) {
                    console.log(response.d);
                    var json = JSON.parse(response.responseText);

                    if (json != null) {
                        console.log(json.Message, 'Agendamento');
                    }
                }
            });

        }

    </script>
</asp:Content>

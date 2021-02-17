<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AgendarCSCarregamento.aspx.vb" Inherits="AgendamentoREDEX.AgendarCSCarregamento"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            var activeIndex = parseInt($('#<%=AccordionIndex.ClientID %>').val());

            $("#accordion").accordion({
                heightStyle: 'panel',
                navigation: true,
                active: activeIndex,
                change: function (event, ui) {
                    var index = $(this).children('h3').index(ui.newHeader);
                    $('#<%=AccordionIndex.ClientID %>').val(index);
                }
            });

        });
    </script>
    
    
    <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
            bgcolor="#F8FFDE" width="1000px">
            <tr>
                <td colspan="2" style="border-style: solid; border-width: 1px; border-color: Black;
                    color: White; font-family: tahoma; font-size: 13px; font-weight: bold;" bgcolor="#B3C63C">
                    Consulta de Reserva
                </td>
                <td style="padding: 0px; margin: 0px; border-color: Black; color: White; font-family: tahoma;
                    font-size: 13px; font-weight: bold; border-left-style: solid; border-left-width: 1px;
                    padding-left: 4px;" align="left" rowspan="2" colspan="2">
                    <asp:Button ID="btnSalvar" runat="server" Height="44px" Text="Salvar" Width="78px"
                        CssClass="Botao" Visible="false" />
                    <asp:Button ID="btnCancelar" runat="server" Height="44px" Text="Excluir" Width="78px"
                        CssClass="Botao" OnClientClick="return confirm('Confirma a exclusão do Agendamento?');"
                        Visible="false" />
                    <asp:Button ID="btnSair" runat="server" Height="44px" Text="Sair" Width="78px" CssClass="Botao" />
                </td>
            </tr>
            <tr bgcolor="White">
                <td style="border-style: solid; border-width: 1px">
                    Informe a Reserva:
                </td>
                <td style="border-style: solid; border-width: 1px; width: 406px;">
                    <asp:TextBox ID="txtReserva" runat="server" AutoPostBack="True" Width="99%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div id="accordion" style="width: 1000px; text-align: left; display: none;">
            <h3>
                1. Informações da Reserva</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Informações da Reserva
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Navio:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblNavio" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Viagem:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblViagem" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Exportador:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblExportador" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Dead Line:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblDeadLine" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Informações da Carga
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Tipo:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTipo" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Total:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTotal" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Status:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblStatus" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            M3:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblM3" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Utilizados:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblUtilizados" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Peso Bruto:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblPesoBruto" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Disponíveis:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblDisponiveis" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Volumes:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblVolumes" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                2. Motorista / Veículo</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Motorista / Veículo
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Nome Motorista / CNH:
                        </td>
                        <td width="350px" style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="txtMotorista" runat="server" Width="99%" AutoPostBack="True" Font-Size="11px"></asp:TextBox>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Cavalo:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="cbCavalo" runat="server" Width="100px" DataTextField="PLACA_CAVALO"
                                DataValueField="PLACA_CAVALO" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            Transportadora:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblTransportadora" runat="server"></asp:Label>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Carreta:
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="cbCarreta" runat="server" Width="100px" DataTextField="PLACA_CARRETA"
                                DataValueField="PLACA_CARRETA">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                3. Carregamento de Carga Solta</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMsgSalvar" runat="server" BackColor="#FFFACD" CssClass="MsgSalvar"
                                Text="Atenção: O Agendamento ainda não foi salvo. Para vincular as Mercadorias, clique no botão Salvar."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-color: Black; border-style: solid; border-width: 1px;
                            font-weight: bold; font-family: tahoma; font-size: 13px; color: White;" bgcolor="#B3C63C">
                            Mercadorias
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-style: solid; border-width: 1px">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr bgcolor="White">
                                    <td>
                                        Mercadoria:
                                    </td>
                                    <td width="60px">
                                        Quantidade:
                                    </td>
                                    <td width="100px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td>
                                        <asp:DropDownList ID="cbMercadoria" runat="server" Width="100%" DataValueField="AUTONUM_BCG"
                                            DataTextField="PRODUTO" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="60px">
                                        <asp:TextBox ID="txtQuantidade" runat="server" Width="60px"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:Button ID="btnSalvarMercadoria" runat="server" Text="Salvar" Enabled="False"
                                            Width="100px" />
                                    </td>
                                </tr>
                                <tr bgcolor="White">
                                    <td colspan="3">
                                        <asp:GridView ID="dgConsultaMercadoria" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" EmptyDataText="Nenhuma Carga encontrada." ForeColor="#333333"
                                            ShowHeaderWhenEmpty="True" Width="100%" Font-Size="11px" DataKeyNames="AUTONUM">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Mercadoria" DataField="PRODUTO"></asp:BoundField>
                                                <asp:BoundField HeaderText="Qtde" DataField="QTDE" DataFormatString="{0:N2}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VOLUME" DataFormatString="{0:N2}" HeaderText="Volume">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PESO_BRUTO" DataFormatString="{0:N2}" HeaderText="Peso">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdEditar" ImageUrl="~/imagens/editar.gif" CommandName="EDITAR"
                                                            CommandArgument="<%# Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                                                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Confirma a exclusão da Carga?');" />
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                4. Escolha do Período</h3>
            <div>
                <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                    bgcolor="#F8FFDE" width="100%">
                    <tr>
                        <td colspan="4">
                            <center>
                                <asp:Panel ID="pnGrid" runat="server" Height="150px" ScrollBars="Vertical" BorderStyle="None">
                                    <asp:GridView ID="dgConsultaPeriodos" runat="server" AutoGenerateColumns="False"
                                    BorderStyle="None" EmptyDataText="Nenhum período disponível." ForeColor="#333333"
                                    ShowHeaderWhenEmpty="True" Width="99%" Font-Size="11px" DataKeyNames="AUTONUM_GD_RESERVA">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField HeaderText="Período Inicial" DataField="PERIODO_INICIAL">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Período Final" DataField="PERIODO_FINAL">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Limite Peso" DataField="LIMITE_PESO">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Limite M³" DataField="LIMITE_M3">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Limite Volumes" DataField="LIMITE_VOLUMES">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Limite Veículos" DataField="LIMITE_CAMINHOES">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="cmdSelecionar" ImageUrl="~/imagens/check.png"
                                                    CommandName="SEL" CommandArgument="<%# Container.DataItemIndex %>" />
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
                            </center>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="width:1000px; background-color:coral; color: white; height:20px;line-height:20px;">
            <p>Atenção: É necessário apresentar 03 vias do CTE e Danfe no Gate!</p>
        </div>
        <asp:Label ID="lblCodigoPatio" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoAgendamento" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoBooking" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoMotorista" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoVeiculo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoProtocolo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoNF" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSaldoCarga" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoCarga" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCodigoViagem" runat="server" Visible="False"></asp:Label>

        <asp:Label ID="lblQuantidadeSelecionadaCarga" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblVolumesSelecionadaCarga" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPesoSelecionadaCarga" runat="server" Visible="False"></asp:Label>

        <asp:HiddenField ID="AccordionIndex" runat="server" Value="0" />
        <asp:Panel ID="pnCadastro" runat="server" Visible="false" Style="background: white;"
            Width="800px" Height="180px">
            <div align="center">
                <iframe id="frameCadastro" runat="server" width="800px" height="180px"></iframe>
                <br />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="modalReserva" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="btnCancelar" DropShadow="true" PopupControlID="pnCadastro" PopupDragHandleControlID="panel3"
            TargetControlID="btnCancelar" OkControlID="btnCancelar">
        </cc1:ModalPopupExtender>
    </center>

</asp:Content>

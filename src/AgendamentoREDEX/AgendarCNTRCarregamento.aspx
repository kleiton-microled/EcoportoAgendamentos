<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AgendarCNTRCarregamento.aspx.vb" Inherits="AgendamentoREDEX.AgendarCNTRCarregamento"
    MaintainScrollPositionOnPostback="true" %>

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
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Viagem:
                        </td>
                        <td style="border-style: solid; border-width: 1px" align="left">
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
                        <td align="right" style="border-style: solid; border-width: 1px">
                            Dead Line:
                        </td>
                        <td style="border-style: solid; border-width: 1px" align="left">
                            <asp:Label ID="lblDeadLine" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="White">
                        <td style="border-style: solid; border-width: 1px">
                            NVOCC:
                        </td>
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblNVOCC" runat="server" Font-Size="11px"></asp:Label>
                        </td>
                        <td align="right" style="border-style: solid; border-width: 1px">
                            POD:
                        </td>
                        <td align="left" style="border-style: solid; border-width: 1px">
                            <asp:Label ID="lblPortoDescarga" runat="server" Font-Size="11px"></asp:Label>
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
                        <td align="right" style="border-style: solid; border-width: 1px">
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
                        <td align="right" style="border-style: solid; border-width: 1px">
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
                        <td align="right" style="border-style: solid; border-width: 1px">
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
                        <td align="right" style="border-style: solid; border-width: 1px">
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
                        <td width="300px" style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="txtMotorista" runat="server" Width="300px" AutoPostBack="True" Font-Size="11px"></asp:TextBox>
                        </td>
                        <td width="100px" align="right" style="border-style: solid; border-width: 1px">
                            Placa Cavalo:
                        </td>
                        <td style="border-style: solid; border-width: 1px; width: 90px;">
                            <asp:DropDownList ID="cbCavalo" runat="server" Width="90px" DataTextField="PLACA_CAVALO"
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
                        <td style="border-style: solid; border-width: 1px; width: 90px;">
                            <asp:DropDownList ID="cbCarreta" runat="server" Width="90px" DataTextField="PLACA_CARRETA"
                                DataValueField="PLACA_CARRETA">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <h3>
                3. Informações do Contêiner</h3>
            <div>
                <center>
                    <table border="1" style="font-family: tahoma; font-size: 11px; outline-width: 0;
                        outline-style: none; outline-color: invert;" width="50%" align="center">
                        <tr>
                            <td bgcolor="#B3C63C" style="font-family: tahoma; font-size: 13px; font-weight: bold;
                                color: #FFFFFF;">
                                &nbsp;Contêiner
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" align="center" colspan="3" valign="middle">
                                <asp:DropDownList ID="cbConteiner" runat="server" Width="200px" DataTextField="CONTEINER"
                                    DataValueField="AUTONUM_PATIO" AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="btnSelecionarConteiner" runat="server" Text="Selecionar" />
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        <h3>
            4. Escolha do Período</h3>
        <div>
            <table style="font-family: tahoma; font-size: 12px; border-collapse: collapse;" align="center"
                bgcolor="#F8FFDE" width="100%">
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnGrid" runat="server" Height="150px" ScrollBars="Vertical" BorderStyle="None">
                            <asp:GridView ID="dgConsultaPeriodos" runat="server" AutoGenerateColumns="False"
                                BorderStyle="None" EmptyDataText="Nenhum período encontrado." ForeColor="#333333"
                                ShowHeaderWhenEmpty="True" Width="100%" Font-Size="11px" DataKeyNames="AUTONUM_GD_RESERVA">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="AUTONUM_GD_RESERVA" HeaderText="Codigo" Visible="False" />
                                    <asp:BoundField HeaderText="Período Inicial" DataField="PERIODO_INICIAL">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Período Final" DataField="PERIODO_FINAL">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Saldo" DataField="LIMITE_CAMINHOES">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                    </td>
                </tr>
            </table>
        </div>
        </div>
          <div style="width:1000px; background-color:coral; color: white; height:20px;line-height:20px;">
            <p>Atenção: É necessário apresentar 03 vias do CTE e Danfe no Gate!</p>
        </div>
    </center>
   
    <asp:Label ID="lblCodigoPatio" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoAgendamento" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoBooking" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoMotorista" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoVeiculo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoProtocolo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoNF" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoViagem" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCodigoPeriodo" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="AccordionIndex" runat="server" Value="0" />
    </asp:Content>

﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/master/Consultas.Master"
    CodeBehind="ConsultarMotoristas.aspx.vb" Inherits="AgendamentoCargaSoltaSGIPA.ConsultarMotoristas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
 <div id="ConteudoGeral" style="margin-left: 20px; margin-right: 20px;">
       

       <table align="center" cellpadding="0" cellspacing="0" width="100%">
          <asp:Panel ID="PanelBarra" runat="server">
            <tr>
                                
                <td align="center" style="padding-right: 20px;"  Height="22px">
                    <asp:Button ID="btNovo" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Novo"
                        Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    <asp:Button ID="btPesquisar" runat="server" Font-Names="Verdana" Font-Size="10px"
                        Text="Pesquisar" Width="80px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </td>
            </tr>           
            </asp:Panel>
            <tr>
                <td>   
                    
                    <asp:Panel ID="PanelPesquisa" runat="server" Visible="False">
                        <br />
                        <table align="center" bgcolor="#999999" border="0" cellpadding="1" cellspacing="0"
                            
                            style="border-style: solid; border-width: 1px; border-color: #999999; font-family: verdana; font-size: 11px;">
                            <tr>
                                <td align="center" colspan="3" style="font-family: verdana; font-size: 11px; font-weight: bold;
                                    color: #FFFFFF; border-right-style: dotted; border-bottom-style: dotted; border-right-width: 1px;
                                    border-bottom-width: 1px; border-right-color: #999999; border-bottom-color: #999999;"
                                    bgcolor="<%= Session("SIS_COR_PADRAO") %>">
                                    Pesquisa
                                </td>
                            </tr>
                            <tr>
                                <td width="70px" align="left" bgcolor="White" style="border-right-style: dotted;
                                    border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px;
                                    border-right-color: #999999; border-bottom-color: #999999">
                                    Cavalo:
                                </td>
                                <td align="left" bgcolor="White" style="border-right-style: dotted;
                                    border-bottom-style: dotted; border-right-width: 1px; border-bottom-width: 1px;
                                    border-right-color: #999999; border-bottom-color: #999999" width="100px">
                                    
                                     <asp:DropDownList ID="cbFiltro1" runat="server" 
                    Font-Names="Verdana" Font-Size="11px" Enabled="False">
                    <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                    <asp:ListItem Value="1">Contém</asp:ListItem>
                </asp:DropDownList>
                                    
                                    </td>
                                <td width="150px" align="left" bgcolor="White" style="border-bottom-style: dotted;
                                    border-bottom-width: 1px; border-bottom-color: #999999">
                                    <asp:TextBox ID="txtCavalo" runat="server" CssClass="Placa" 
                                        Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="70px">
                                    Carreta:
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
                                    <asp:TextBox ID="txtCarreta" runat="server" CssClass="Placa" Font-Names="Verdana"
                                        Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="70px">
                                    CNH:
                                </td>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="100px">
                                    <asp:DropDownList ID="cbFiltro3" runat="server" Font-Names="Verdana" 
                                        Font-Size="11px">
                                        <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                        <asp:ListItem Value="1">Contém</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999" width="150px">
                                    <asp:TextBox ID="txtCNH" runat="server" CssClass="Numeric" Font-Names="Verdana" 
                                        Font-Size="11px" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="70px">
                                    Motorista:</td>
                                <td align="left" bgcolor="White" style="border-right-style: dotted; border-right-width: 1px;
                                    border-right-color: #999999; border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999;" width="100px">
                                    <asp:DropDownList ID="cbFiltro4" runat="server" Font-Names="Verdana" 
                                        Font-Size="11px">
                                        <asp:ListItem Value="0">Exatamente Igual</asp:ListItem>
                                        <asp:ListItem Value="1">Contém</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" bgcolor="White" style="border-bottom-style: dotted; border-bottom-width: 1px;
                                    border-bottom-color: #999999" width="150px">
                                    <asp:TextBox ID="txtMotorista" runat="server" CssClass="AlphaNumeric" 
                                        Font-Names="Verdana" Font-Size="11px" Style="margin-left: 0px" 
                                        Width="200px"></asp:TextBox>
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
        </table>


        <asp:Panel ID="PanelConsMotor" runat="server" Height="100%">
        <asp:GridView ID="DgMotoristas" runat="server" AutoGenerateColumns="False" CellPadding="1"
            ForeColor="#333333" GridLines="None" CellSpacing="1" Width="100%" EmptyDataText="Nenhum registro encontrado."
            ShowHeaderWhenEmpty="True" BackColor="#999999" Font-Names="Verdana" Font-Size="10px"
            DataKeyNames="AUTONUM,ID_TRANSPORTADORA" AllowPaging="True" PageSize="20">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="EDIT" Text="Editar" ImageUrl="~/imagens/editar.gif">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:ButtonField>
               
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="cmdExcluir" ImageUrl="~/imagens/excluir.png"
                            CommandName="DEL" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:TemplateField>

                <asp:BoundField DataField="AUTONUM" HeaderText="AUTONUM" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TRANSPORTADORA" DataField="TRANSPORTADORA" 
                    Visible="False">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="NOME DO MOTORISTA" DataField="NOME">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="CPF" HeaderText="CPF">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CNH" DataField="CNH">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="CELULAR" HeaderText="CELULAR">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ID_TRANSPORTADORA" HeaderText="ID_TRANSPORTADORA" 
                    Visible="False" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataRowStyle BackColor="#E9E9E9" HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CorPadrao" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="CorPadrao"/>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        </asp:Panel>
    </div>
    </form>
</asp:Content>

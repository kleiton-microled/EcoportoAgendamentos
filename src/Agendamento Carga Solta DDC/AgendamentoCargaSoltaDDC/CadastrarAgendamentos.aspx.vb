Imports AjaxControlToolkit
Imports System.Linq
Imports System.Globalization

Public Class CadastrarAgendamentos
    Inherits System.Web.UI.Page

    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo
    Dim Transportadora As New Transportadora
    Dim NotaFiscal As New NotaFiscal
    Dim Agendamento As New Agendamento

    Dim DescRetorno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Lista1 As New List(Of String)
            Dim I As Integer

            Me.dgPeriodos.DataSource = Lista1
            Me.dgPeriodos.DataBind()

            Me.dgNotas.DataSource = Lista1
            Me.dgNotas.DataBind()

            ConsultarCNH()
            CarregarDadosProtocolo()
            ConsultarConteineresDDC()

            If Not Request.QueryString("id") Is Nothing Then
                If Integer.TryParse(Request.QueryString("id").ToString(), I) Then
                    If Agendamento.VerificarAgendTransp(Request.QueryString("id").ToString(), Session("SIS_ID").ToString()) Then
                        If Not Request.QueryString("action") Is Nothing Then
                            If Request.QueryString("action").ToString() = "edit" Then
                                Editar(Request.QueryString("id").ToString())
                            End If
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Id do agendamento não pertence à transportadora logada!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                    End If
                Else
                    Response.Redirect("ConsultarAgendamentos.aspx")
                End If
            End If '

        End If

    End Sub

    Private Sub Editar(ByVal ID As String)

        Dim Ds1 As New DataTable
        Dim Conteiner As String

        Ds1 = Agendamento.ConsultarDadosAgendamento(ID)

        lblID.Text = Ds1.Rows(0)("AUTONUM").ToString()
        lblCodigoPeriodo.Text = Ds1.Rows(0)("AUTONUM_GD_RESERVA").ToString()
        lblProtocolo.Text = Ds1.Rows(0)("PROTOCOLO").ToString()
        lblPeriodo.Text = Ds1.Rows(0)("PERIODO").ToString()
        txtNomeMotorista.Text = Ds1.Rows(0)("NOME").ToString()
        txtTara.Text = Ds1.Rows(0)("TARA").ToString()
        txtChassi.Text = Ds1.Rows(0)("CHASSI").ToString()
        Veiculo.Cavalo = Ds1.Rows(0)("PLACA_CAVALO").ToString()
        Veiculo.Carreta = Ds1.Rows(0)("PLACA_CARRETA").ToString()
        Session("PERIODO_ANTERIOR_COD") = lblCodigoPeriodo.Text
        Session("PERIODO_ANTERIOR") = lblPeriodo.Text

        txtNrDocSaida.Text = Ds1.Rows(0)("NUM_DOC_SAIDA").ToString()
        txtSerieDocSaida.Text = Ds1.Rows(0)("SERIE_DOC_SAIDA").ToString()
        cbTipoDocSaida.SelectedValue = Ds1.Rows(0)("TIPO_DOC_SAIDA").ToString()
        txtEmissaoDocSaida.Text = Ds1.Rows(0)("EMISSAO_DOC_SAIDA").ToString()

        CarregarDados(Agendamento.ObterCNHPeloIdAgendamento(lblID.Text))
        ConsultarDadosNota(lblID.Text)

        HabilitarControles()
        txtTara.Enabled = False
        txtChassi.Enabled = False
        btNovo.Enabled = False
        btNovo.BackColor = btSalvar.BackColor
        btNovo.BorderStyle = btSalvar.BorderStyle
        btNovo.BorderColor = btSalvar.BorderColor
        btNovo.ForeColor = btSalvar.ForeColor
        btNovo.Font.Bold = btSalvar.Font.Bold
        btnExcluirProduto.Visible = True

        Conteiner = Agendamento.ConsultarConteinerAgendamento(lblID.Text)
        lblConteinerSelecionado.Text = Conteiner
        Me.cbConteiner.SelectedValue = Conteiner

        ConsultarPeriodos()
        lblPeriodo.Visible = True

        If Conteiner <> "-1" Then
            CarregarItensCS(Conteiner, True)
        End If

        Session("LOTE_DOCUMENTO") = Agendamento.ObterDocumentoLote(Conteiner)

        If Not Agendamento.AgendamentoFinalizado(Convert.ToInt32(lblID.Text)) And dgNotas.Rows.Count = 0 Then
            cbConteiner.Enabled = True
        End If

    End Sub

    Private Sub CarregarDadosProtocolo()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Private Sub ConsultarConteineresDDC()

        'Dim Acao As String
        Dim Conteiner As String = String.Empty

        If Request.QueryString("action") = "edit" Then

            Conteiner = Agendamento.ConsultarConteinerAgendamento(Request.QueryString("id").ToString())

            'Acao = "edit"            
            Me.cbConteiner.DataSource = Agendamento.ConsultarConteineresDDC(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")), Conteiner)
            Me.cbConteiner.DataBind()

            If Me.cbConteiner.Items.Count > 0 Then
                If Conteiner <> String.Empty Then
                    cbConteiner.SelectedValue = Conteiner
                End If
            Else
                cbConteiner.Items.Insert(0, New ListItem("", 0))
                cbConteiner.SelectedIndex = 0
            End If

        Else
            'Acao = "new"
            Me.cbConteiner.DataSource = Agendamento.ConsultarConteineresDDC(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")), Conteiner)
            Me.cbConteiner.DataBind()
            cbConteiner.Items.Insert(0, New ListItem("SELECIONE...", 0))
        End If

    End Sub

    Private Sub ConsultarPeriodos(ByVal Lote As String)
        dgPeriodos.DataSource = Agendamento.ConsultarPeriodos(Lote)
        dgPeriodos.DataBind()
    End Sub

    Private Sub ConsultarCNH()

        Me.cbCNH.Items.Add("")

        Dim Lista As List(Of String) = Agendamento.ConsultarCNH(Session("SIS_ID").ToString())

        If Lista IsNot Nothing Then
            For Each CNH As String In Lista
                Me.cbCNH.Items.Add(CNH)
            Next
        End If

        Lista = Nothing

    End Sub

    Protected Sub txtNomeMotorista_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNomeMotorista.TextChanged

        'Dim CPF As String
        cbCNH.Items.Clear()
        Motorista = Nothing
        If Not txtNomeMotorista.Text = String.Empty Then
            'CPF = Motorista.ObterCPFMotorista(txtNomeMotorista.Text.Trim())
            'CarregarDados()
            CarregarListaCNH(txtNomeMotorista.Text.ToUpper.Trim(), "NomeMotoristaDigitado")
            CarregarDadosVeiculo()
            If cbCNH.Items.Count = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Nenhum motorista foi encontrado.');</script>", False)
            End If
        Else
            ConsultarCNH()
            CarregarDadosVeiculo()
            cbCNH.Enabled = True
            cbCavalo.Enabled = True
            cbCarreta.Enabled = True
        End If

    End Sub

    Private Sub CarregarDadosVeiculo()
        'Carrega Dados do Veículo para não perder as placas selecionadas do cavalo e da carreta

        CarregarListaCavalos(txtNomeMotorista.Text, Session("SIS_ID").ToString())
        CarregarListaCarretas(txtNomeMotorista.Text, Session("SIS_ID").ToString())
        CarregarDadosVeiculo(cbCavalo.Text, cbCarreta.Text, Session("SIS_ID").ToString())

        If Not Veiculo.Cavalo = String.Empty Then
            Try
                cbCavalo.SelectedValue = Veiculo.Cavalo
            Catch ex As Exception
                cbCavalo.SelectedIndex = -1 'Caso Placa do Cavalo não exista na lista de itens
            End Try

        End If
        If Not Veiculo.Carreta = String.Empty Then
            Try
                cbCarreta.SelectedValue = Veiculo.Carreta
            Catch ex As Exception
                cbCarreta.SelectedIndex = -1 'Caso Placa da Carreta não exista na lista de itens
            End Try
        End If
    End Sub

    Private Sub CarregarDados(Optional ByVal CNH As String = "")

        Dim PlacaCavalo As String = cbCavalo.Text
        Dim PlacaCarreta As String = cbCarreta.Text

        cbCNH.Items.Clear()
        cbCavalo.Items.Clear()
        cbCarreta.Items.Clear()

        CheckAutonomo.Checked = False

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Session("SIS_ID").ToString()

        Dim MotoristaOBJ As New Motorista
        'MotoristaOBJ.Nome = txtNomeMotorista.Text.ToUpper() - CarregarDados já não é mais acessado pelo txtNomeMotorista_TextChanged
        MotoristaOBJ.CNH = CNH
        MotoristaOBJ.Transportadora = TransportadoraOBJ
        MotoristaOBJ.Nome = Agendamento.ConsultarNome(MotoristaOBJ)

        If Not Agendamento.ConsultarNome(MotoristaOBJ) = String.Empty Then
            txtNomeMotorista.Text = Agendamento.ConsultarNome(MotoristaOBJ)
            If Agendamento.VerificarHomonimos(MotoristaOBJ.Nome, Session("SIS_ID").ToString()) Then
                CarregarListaCNHLendoCNH(MotoristaOBJ.CNH)
            Else
                CarregarListaCNH(txtNomeMotorista.Text.Trim.ToUpper(), "RotinaCarregarDados")
            End If

            CarregarListaCavalos(txtNomeMotorista.Text, Session("SIS_ID").ToString())
            CarregarListaCarretas(txtNomeMotorista.Text, Session("SIS_ID").ToString())
            CarregarDadosVeiculo(cbCavalo.Text, cbCarreta.Text, Session("SIS_ID").ToString())

            If Not Veiculo.Cavalo = String.Empty Then
                Try
                    cbCavalo.SelectedValue = Veiculo.Cavalo
                Catch ex As Exception
                    cbCavalo.SelectedIndex = -1 'Caso Placa do Cavalo não exista na lista de itens
                End Try
            ElseIf PlacaCavalo <> "" Then
                cbCavalo.SelectedValue = PlacaCavalo
            End If
            If Not Veiculo.Carreta = String.Empty Then
                Try
                    cbCarreta.SelectedValue = Veiculo.Carreta
                Catch ex As Exception
                    cbCarreta.SelectedIndex = -1 'Caso Placa da Carreta não exista na lista de itens
                End Try
            ElseIf PlacaCarreta <> "" Then
                cbCarreta.SelectedValue = PlacaCarreta
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Nenhum motorista foi encontrado.');</script>", False)
        End If

        TransportadoraOBJ = Nothing
        MotoristaOBJ = Nothing

    End Sub
    '''<summary>
    ''' Carrega os produtos que têm num determinado lote; AutonumProd é Autonum da Mercadoria, caso não exista é -1
    ''' </summary>
    Private Sub CarregarItensCS(ByVal Conteiner As String, ByVal Edicao As Boolean, Optional ByVal AutonumProd As Integer = -1)

        Dim DTItensCS As New DataTable
        Dim CodProduto As String
        Dim ProdutoCS As String
        Dim Embalagem As String
        Dim Qtde As String
        Dim SaldoBD As String
        Dim Saldo As Integer = 1
        Dim TextoItensCS As String
        Dim index As Integer = 0 'index do cbItensCS

        cbItensCS.Items.Clear()
        DTItensCS = Agendamento.ConsultarItensCargaSolta(Conteiner, Session("SIS_TRANSPEMPRESA").ToString(), Session("SIS_ID").ToString(), Edicao)

        Me.cbItensCS.DataSource = DTItensCS
        Me.cbItensCS.DataBind()
        'For i As Integer = 0 To DTItensCS.Rows.Count - 1
        '    CodProduto = DTItensCS.Rows(i)("AUTONUM").ToString()
        '    Embalagem = DTItensCS.Rows(i)("EMBALAGEM").ToString()
        '    ProdutoCS = DTItensCS.Rows(i)("PRODUTO").ToString()
        '    Qtde = DTItensCS.Rows(i)("QUANTIDADE").ToString()
        '    SaldoBD = DTItensCS.Rows(i)("SALDO").ToString()
        '    Saldo = Convert.ToInt32(Val(SaldoBD))

        '    If Saldo > 0 Or AutonumProd = CodProduto Then
        '        TextoItensCS = "Qtde: " & Qtde & " | " & ProdutoCS & " (" & Embalagem & ") | Saldo: " & SaldoBD & ""
        '        cbItensCS.Items.Insert(index, New ListItem(TextoItensCS, CodProduto))
        '        index += 1
        '    End If

        'Next

        If cbItensCS.Items.Count > 0 Then
            cbItensCS.Items.Insert(0, New ListItem("SELECIONE...", ""))
            cbItensCS.Enabled = True
        Else
            cbItensCS.Items.Insert(0, New ListItem("NENHUM ITEM NA LISTA...", ""))
            cbItensCS.Enabled = False
        End If

    End Sub

    Private Sub CarregarDadosVeiculo(ByVal Cavalo As String, ByVal Carreta As String, ByVal Transportadora As Integer)

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Transportadora

        Dim VeiculoOBJ As New Veiculo

        VeiculoOBJ.Cavalo = Cavalo
        VeiculoOBJ.Carreta = Carreta
        VeiculoOBJ.Transportadora = TransportadoraOBJ

        If Not Agendamento.ConsultarDadosVeiculo(VeiculoOBJ) Is Nothing Then

            Dim NovoVeiculo As New Veiculo
            NovoVeiculo = Agendamento.ConsultarDadosVeiculo(VeiculoOBJ)

            txtTara.Text = NovoVeiculo.Tara
            txtChassi.Text = NovoVeiculo.Chassi

        End If

    End Sub

    Private Sub CarregarListaCNH(ByVal Nome As String, ByVal Origem As String)

        'Origem: Que local chamou este método

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ.Nome = Nome

        Dim ListaCNH As New List(Of String)
        If Origem = "NomeMotoristaDigitado" Then
            ListaCNH = Agendamento.ConsultarCNH(MotoristaOBJ, Session("SIS_ID").ToString())
        Else
            ListaCNH = Agendamento.ConsultarCNHSemHomonimo(MotoristaOBJ, Session("SIS_ID").ToString())
        End If

        If Not ListaCNH Is Nothing Then

            If ListaCNH.Count > 1 Then
                Me.cbCNH.Items.Add("")

                For Each Item As String In ListaCNH
                    cbCNH.Items.Add(Item.ToString())
                Next
            Else 'Objetivo de Possibilitar Alteração da ComboBox do Motorista quando o resultado foi encontrado
                MotoristaOBJ.Transportadora = Transportadora
                MotoristaOBJ.Transportadora.ID = Session("SIS_ID").ToString
                MotoristaOBJ.CNH = ListaCNH.Item(0)
                MotoristaOBJ.Nome = Agendamento.ConsultarNome(MotoristaOBJ) 'Garantia de que é o nome inteiro do motorista
                Me.txtNomeMotorista.Text = MotoristaOBJ.Nome
                ConsultarCNH()
                Me.cbCNH.Text = MotoristaOBJ.CNH
            End If

            cbCNH.Enabled = True
            cbCavalo.Enabled = True
            cbCarreta.Enabled = True

        End If

        ListaCNH = Nothing

    End Sub

    Private Sub CarregarListaCavalos(ByVal Nome As String, ByVal Transportadora As Integer)

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Transportadora

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ.Nome = Nome
        MotoristaOBJ.Transportadora = TransportadoraOBJ

        Dim ListaCavalos As New List(Of String)
        ListaCavalos = Agendamento.ConsultarPlacasCavalo(MotoristaOBJ)

        If Not ListaCavalos Is Nothing Then
            cbCavalo.Items.Add("")
            For Each Item As String In ListaCavalos
                cbCavalo.Items.Add(Item.ToString())
            Next
        End If

        ListaCavalos = Nothing

    End Sub

    Private Sub CarregarListaCarretas(ByVal Nome As String, ByVal Transportadora As Integer)

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Transportadora

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ.Nome = Nome
        MotoristaOBJ.Transportadora = TransportadoraOBJ

        Dim ListaCarretas As New List(Of String)
        ListaCarretas = Agendamento.ConsultarPlacasCarreta(MotoristaOBJ)

        If Not ListaCarretas Is Nothing Then
            cbCarreta.Items.Add("")
            For Each Item As String In ListaCarretas
                cbCarreta.Items.Add(Item.ToString())
            Next
        End If

        ListaCarretas = Nothing

    End Sub

    Private Sub CarregarListaCarretasPorCavalo()

        Dim ListaCarretas As New List(Of String)
        ListaCarretas = Agendamento.ConsultarPlacasCarretaPorCavalo(cbCavalo.Text, Session("SIS_ID").ToString())

        cbCarreta.Items.Clear()

        If Not ListaCarretas Is Nothing Then
            For Each Item As String In ListaCarretas
                cbCarreta.Items.Add(Item.ToString())
            Next
        End If

        ListaCarretas = Nothing

    End Sub

    Public Shared ReadOnly Property ListaItens() As List(Of NotaFiscalItem)
        Get
            Dim Lista = TryCast(HttpContext.Current.Session("ListaItens"), List(Of NotaFiscalItem))
            If Lista Is Nothing Then
                Lista = New List(Of NotaFiscalItem)()
                HttpContext.Current.Session("ListaItens") = Lista
            End If
            Return Lista
        End Get
    End Property

    Public Shared ReadOnly Property ListaNotaFiscais() As List(Of NotaFiscal)
        Get
            Dim Lista = TryCast(HttpContext.Current.Session("ListaNotaFiscal"), List(Of NotaFiscal))
            If Lista Is Nothing Then
                Lista = New List(Of NotaFiscal)()
                HttpContext.Current.Session("ListaNotaFiscal") = Lista
            End If
            Return Lista
        End Get
    End Property

    Private Sub ConsultarNotas()

        Me.dgNotas.DataSource = Agendamento.ConsultarDadosNota(lblID.Text)
        Me.dgNotas.DataBind()

    End Sub

    Private Sub ConsultarPeriodos()

        Dim Lotes As String = String.Empty
        Dim ListaLotes As New List(Of String)

        If Me.dgNotas.Rows.Count > 0 Then

            Dim ds As New DataTable

            If Request.QueryString("id") Is Nothing Then
                ds = Banco.Consultar("SELECT A.BL FROM SGIPA.TB_AMR_CNTR_BL A INNER JOIN SGIPA.TB_CNTR_BL B ON A.CNTR = B.AUTONUM WHERE A.CNTR = " & Val(Me.cbConteiner.SelectedValue))
            Else
                ds = Banco.Consultar("SELECT A.BL FROM SGIPA.TB_AMR_CNTR_BL A INNER JOIN SGIPA.TB_CNTR_BL B ON A.CNTR = B.AUTONUM WHERE A.CNTR = " & Val(Request.QueryString("cntr").ToString()))
            End If

            If ds.Rows.Count > 0 Then
                For Each Linha As DataRow In ds.Rows
                    ListaLotes.Add(Linha("BL").ToString())
                Next
            End If

            Lotes = String.Join(",", ListaLotes)
            ConsultarPeriodos(Lotes)

        End If

    End Sub

    Protected Sub cbCNH_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCNH.SelectedIndexChanged

        If Not cbCNH.SelectedItem.Text = String.Empty Then
            CarregarDados(cbCNH.SelectedItem.Text)
        End If

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click

        If lblID.Text = String.Empty Then

            Dim Usuario As New Usuario
            Usuario.UserId = Session("SIS_USUARIO_LOGADO")

            Transportadora.ID = Session("SIS_ID").ToString()
            Transportadora.Usuario = Usuario

            Dim ID As String = Agendamento.CriarAgendamento(Transportadora)

            If Not ID Is Nothing Then

                lblID.Text = ID

                txtNomeMotorista.Enabled = True
                cbCNH.Enabled = True
                CheckAutonomo.Enabled = True
                cbConteiner.Enabled = True
                dgPeriodos.Enabled = True
                cbTipoDocSaida.Enabled = True
                txtSerieDocSaida.Enabled = True
                txtNrDocSaida.Enabled = True
                txtEmissaoDocSaida.Enabled = True
                txtNotaFiscal.Enabled = True
                txtSerie.Enabled = True
                txtDataEmissao.Enabled = True

                btNovo.Text = "Cancelar"
                btNovo.ToolTip = "Cancelar o Agendamento."

                txtNomeMotorista.Focus()

            End If
        Else

            If Agendamento.CancelarAgendamento(lblID.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Agendamento Cancelado.');location.href='ConsultarAgendamentos.aspx';</script>", False)
            End If

        End If

    End Sub

    Private Function ValidarItem(Obrigatoria As Boolean) As Boolean

        'Obrigatoria: indica se a Nota Fiscal é Obrigatória ou não

        If cbConteiner.SelectedValue = "0" Then 'If cbLote.SelectedIndex = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Lote/BL.');</script>", False)
            Return False
        End If

        If cbItensCS.SelectedIndex = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Item da Carga Solta.');</script>", False)
            Return False
        End If

        If Obrigatoria Then
            If txtNotaFiscal.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Número da Nota Fiscal.');</script>", False)
                Return False
            End If

            If txtSerie.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Série da Nota Fiscal.');</script>", False)
                Return False
            End If

            If Not txtDataEmissao.Text = String.Empty Then
                If Not IsDate(txtDataEmissao.Text) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A Data de Emissão da Nota Fiscal é inválida.');</script>", False)
                    Return False
                Else
                    Dim dtEmissaoNF As DateTime = Convert.ToDateTime(txtDataEmissao.Text)
                    If dtEmissaoNF > Now Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A Data de Emissão da Nota Fiscal deverá ser menor que a data atual.');</script>", False)
                        Return False
                    End If
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Data e Emissão da Nota Fiscal.');</script>", False)
                Return False
            End If
        End If

        If cbConteiner.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Lote.');</script>", False)
            Return False
        End If

        Dim q As Integer
        If txtQtde.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Digite a quantidade do item a ser inserida.');</script>", False)
        End If

        If Integer.TryParse(txtQtde.Text, q) Then
            If q <= 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A quantidade informada deverá ser maior ou igual a 1');</script>", False)
                Return False
            End If

            If q > ObterSaldoItem() Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A quantidade informada do item é maior que o saldo.');</script>", False)
                Return False
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A quantidade informada deverá ser valor inteiro maior ou igual a 1');</script>", False)
            Return False
        End If

        Return True

    End Function

    Public Function Validar() As Boolean

        'Dim dtEmissaoNF As DateTime

        If lblPeriodo.Text = "Nenhum período foi selecionado." Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Nenhum período foi selecionado.');</script>", False)
            Return False
        End If

        If txtNomeMotorista.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Nome do Motorista.');</script>", False)
            Return False
        End If

        If cbCNH.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a CNH do Motorista.');</script>", False)
            Return False
        End If

        If cbCavalo.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Placa do Cavalo.');</script>", False)
            Return False
        End If

        If cbCarreta.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Placa da Carreta.');</script>", False)
            Return False
        End If

        If cbTipoDocSaida.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Tipo do Documento de Saída.');</script>", False)
            Return False
        End If

        If txtSerieDocSaida.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Série do Documento de Saída.');</script>", False)
            Return False
        End If

        If txtNrDocSaida.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Número do Documento de Saída.');</script>", False)
            Return False
        End If

        If Not txtEmissaoDocSaida.Text = String.Empty Then
            If IsDate(txtEmissaoDocSaida.Text) Then
                If Convert.ToDateTime(txtEmissaoDocSaida.Text) > Now Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A Data de Emissão do Documento deverá ser menor que a data atual.');</script>", False)
                    Return False
                End If
                If Convert.ToDateTime(txtEmissaoDocSaida.Text, New CultureInfo("pt-BR")).Date < Convert.ToDateTime(Now, New CultureInfo("pt-BR")).AddDays(-90).Date Then '90 dias (aprox 3 meses está momentaneamente sendo provisório)
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('A Data de Emissão do Documento é muito antiga.');</script>", False)
                    Return False
                End If
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Data de Emissão do Documento de Saída.');</script>", False)
            Return False
        End If

        If dgNotas.Rows.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não há nenhuma Nota Fiscal e nenhum produto inserido no agendamento');</script>", False)
            Return False
        End If

        Return True

    End Function

    Public Function ValidarPeriodo() As Boolean
        Dim SiglaAcao As Char

        If Not Request.QueryString("action") Is Nothing Then
            If Request.QueryString("action") = "edit" Then
                SiglaAcao = "E"
            Else
                SiglaAcao = "N"
            End If
        Else 'new
            SiglaAcao = "N"
        End If

        If SiglaAcao = "N" Then
            If Agendamento.VerificarLimiteMovPeriodo(lblCodigoPeriodo.Text) = True Then
                Return True
            Else
                Return False
            End If
        Else 'E
            If lblCodigoPeriodo.Text = Session("PERIODO_ANTERIOR_COD").ToString Then
                Return True
            Else
                If Agendamento.VerificarLimiteMovPeriodo(lblCodigoPeriodo.Text) = True Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If

    End Function

    Protected Sub dgNotas_RowCommand1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgNotas.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim Autonum As String = dgNotas.DataKeys(Index)("AUTONUM").ToString()
        Dim Lote As String = dgNotas.DataKeys(Index)("LOTE").ToString()
        Dim Nota As String = dgNotas.DataKeys(Index)("NOTAFISCAL").ToString()
        Dim Documento As String = dgNotas.DataKeys(Index)("TIPO").ToString()
        Dim Serie As String = dgNotas.DataKeys(Index)("SERIE").ToString()
        Dim Emissao As String = dgNotas.DataKeys(Index)("EMISSAO").ToString()
        Dim Qtde As String = dgNotas.DataKeys(Index)("QTDE").ToString()
        Dim IdProduto As String = dgNotas.DataKeys(Index)("AUTONUM_PRODUTO").ToString()

        If e.CommandName = "DEL" Then

            If dgNotas.Rows.Count = 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não há como excluir porque só tem uma Nota Fiscal para o agendamento');</script>", False)
            Else

                Agendamento.ExcluirNF(Autonum)
                ConsultarNotas()
                CarregarItensCS(lblConteinerSelecionado.Text, False)

                pnLegenda.Visible = False

            End If

        ElseIf e.CommandName = "EDITAR" Then

            Session("NF_EDITADA") = Autonum
            Session("NF_EDITADA_QTDEANT") = Qtde
            Session("NF_EDITADA_CODPRODANT") = IdProduto

            btnAdicProduto.Text = "Alterar Nota Fiscal"
            btnAdicProduto.CommandName = "ALTERAR"
            txtNotaFiscal.Text = Nota
            txtSerie.Text = Serie

            If IsDate(Emissao) Then
                txtDataEmissao.Text = Emissao
            Else
                txtDataEmissao.Text = ""
            End If

            txtQtde.Text = Qtde
            txtQtde.Enabled = True

            CarregarItensCS(lblConteinerSelecionado.Text, True)

            If cbItensCS.Items.Count > 0 Then
                cbItensCS.SelectedValue = IdProduto
            End If
        Else
            pnLegenda.Visible = True
            ConsultarPeriodos()
        End If

    End Sub

    Private Sub ConsultarDadosNota(ByVal ID As Integer)

        Dim Ds As New DataTable
        Ds = Agendamento.ConsultarDadosNota(ID)

        Me.dgNotas.DataSource = Ds
        Me.dgNotas.DataBind()

    End Sub



    Protected Sub dgPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgPeriodos.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim Autonum As String = dgPeriodos.DataKeys(Index)("AUTONUM_GD_RESERVA").ToString()
        Dim Periodo_Inicial As String = dgPeriodos.DataKeys(Index)("PERIODO_INICIAL").ToString()
        Dim Periodo_Final As String = dgPeriodos.DataKeys(Index)("PERIODO_FINAL").ToString()
        Dim Saldo As String = dgPeriodos.DataKeys(Index)("SALDO").ToString()
        Dim DTA As String = dgPeriodos.DataKeys(Index)("FLAG_DTA").ToString()

        If DTA Then
            If Saldo = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Saldo insuficiente para agendamento.');</script>", False)
                Exit Sub
            End If
        End If

        lblCodigoPeriodo.Text = Autonum

        If Not Agendamento.ConteinerComPendenciaRecebimento(lblCodigoPeriodo.Text, Me.cbConteiner.SelectedValue) Then
            If Agendamento.ConsultarFormaPagamento(Me.cbConteiner.SelectedValue) = 2 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner com Pendência de recebimento neste Periodo. Favor escolher outro período.');</script>", False)
                Exit Sub
            End If
        End If

        For Each Linha As GridViewRow In Me.dgPeriodos.Rows
            Linha.BackColor = Drawing.Color.White
        Next

        Me.dgPeriodos.Rows(Index).BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")

        lblPeriodo.Text = Periodo_Inicial & " - " & Periodo_Final

    End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        If Validar() Then
            'If ValidarPeriodo() Or (Session("LOTE_DOCUMENTO").ToString() = "DTA" Or Session("LOTE_DOCUMENTO").ToString().Trim() = "DTA-S") Then
            If ValidarPeriodo() Then
                Dim CPF As String = Motorista.ObterCPFMotorista(cbCNH.SelectedValue, Convert.ToInt32(Session("SIS_ID").ToString()))
                If String.IsNullOrWhiteSpace(CPF) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptSemCPF", "<script>alert('CPF do Motorista não cadastrado');</script>", False)
                    Exit Sub
                End If
                'If Not ValidarBDCC(CPF) Then
                'Else
                '    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>" & DescRetorno & "</script>", False)
                'End If

                Dim Lote As Integer = 0
                Lote = Banco.ExecutaRetorna("SELECT DISTINCT BL FROM SGIPA.TB_CARGA_SOLTA WHERE AUTONUM = " & Val(cbItensCS.SelectedValue))

                Dim TransportadoraOBJ As New Transportadora
                TransportadoraOBJ.ID = Session("SIS_ID").ToString()

                Dim VeiculoOBJ As New Veiculo
                VeiculoOBJ.Cavalo = cbCavalo.Text
                VeiculoOBJ.Carreta = cbCarreta.Text
                VeiculoOBJ.Transportadora = TransportadoraOBJ
                VeiculoOBJ.ID = Veiculo.ObterID(VeiculoOBJ)

                Dim MotoristaOBJ As New Motorista
                MotoristaOBJ.Nome = txtNomeMotorista.Text
                MotoristaOBJ.CNH = cbCNH.Text
                MotoristaOBJ.Transportadora = TransportadoraOBJ

                Dim AgendamentoOBJ As New Agendamento
                AgendamentoOBJ.Codigo = lblID.Text
                AgendamentoOBJ.EmissaoDocSaida = txtEmissaoDocSaida.Text
                AgendamentoOBJ.TipoDocSaida = cbTipoDocSaida.SelectedValue
                AgendamentoOBJ.NumDocSaida = txtNrDocSaida.Text
                AgendamentoOBJ.SerieDocSaida = txtSerieDocSaida.Text
                AgendamentoOBJ.Motorista = MotoristaOBJ
                AgendamentoOBJ.Periodo = lblCodigoPeriodo.Text
                AgendamentoOBJ.Transportadora = TransportadoraOBJ
                AgendamentoOBJ.Veiculo = VeiculoOBJ
                AgendamentoOBJ.ConteinerDDC = Val(Me.cbConteiner.SelectedValue)
                AgendamentoOBJ.Lote = Lote
                AgendamentoOBJ.UsuarioId = Session("SIS_USUARIO_LOGADO")

                Dim NotaFiscalOBJ As New NotaFiscal
                NotaFiscalOBJ.NotaFiscal = txtNotaFiscal.Text
                NotaFiscalOBJ.Serie = txtSerie.Text
                NotaFiscalOBJ.Emissao = txtDataEmissao.Text

                If lblProtocolo.Text = "Não Gerado" Then
                    If Agendamento.Agendar(AgendamentoOBJ) Then
                        Agendamento.InsereAgendamentoNaFila(Me.cbConteiner.SelectedValue, lblCodigoPeriodo.Text, Convert.ToInt32(Session("SIS_USRID")))
                        Agendamento.AlterarDataSaida(AgendamentoOBJ.Codigo)
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>" & DescRetorno & "alert('Agendamento Realizado com Sucesso!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                    End If
                Else
                    If Agendamento.AlterarAgendamento(AgendamentoOBJ) Then
                        Agendamento.InsereAgendamentoNaFila(Me.cbConteiner.SelectedValue, lblCodigoPeriodo.Text, Convert.ToInt32(Session("SIS_USRID")))
                        Agendamento.AlterarDataSaida(AgendamentoOBJ.Codigo)
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>" & DescRetorno & "alert('Agendamento Alterado com Sucesso!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                    End If
                End If


            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('\nSaldo insuficiente. Favor Escolher outro Periodo.');</script>", False)

                If Not Session("PERIODO_ANTERIOR_COD") Is Nothing Then
                    lblCodigoPeriodo.Text = Session("PERIODO_ANTERIOR_COD").ToString()
                Else
                    lblCodigoPeriodo.Text = ""
                End If
                If Not Session("PERIODO_ANTERIOR") Is Nothing Then
                    lblPeriodo.Text = Session("PERIODO_ANTERIOR").ToString()
                Else
                    lblPeriodo.Text = "Nenhum período foi selecionado." 'É o vazio desta lbl
                End If

                ConsultarPeriodos()
            End If
        Else

        End If

    End Sub

    Public Function InserirNotasFiscais() As Boolean

        Dim Lote As Integer = 0
        Lote = Banco.ExecutaRetorna("SELECT BL FROM SGIPA.TB_CARGA_SOLTA WHERE AUTONUM = " & Val(Me.cbItensCS.SelectedValue))

        Agendamento.InserirNF(Lote, txtNotaFiscal.Text, txtSerie.Text, txtDataEmissao.Text, lblID.Text, cbItensCS.SelectedValue, txtQtde.Text, Val(Me.cbConteiner.SelectedValue))

        Return True

    End Function

    Public Sub AlterarNotasFiscais()

        Dim Lote As Integer = 0
        Lote = Banco.ExecutaRetorna("SELECT DISTINCT BL FROM SGIPA.TB_CARGA_SOLTA WHERE AUTONUM = " & Val(cbItensCS.SelectedValue))

        Agendamento.AlterarNF(Lote, txtNotaFiscal.Text, txtSerie.Text, txtDataEmissao.Text, cbItensCS.SelectedValue, txtQtde.Text, Session("NF_EDITADA").ToString())

    End Sub

    Private Function ValidarBDCC(ByVal CPF As String) As Boolean

        Dim Autonomo As New Integer
        Dim WebService As New WebService

        If Not My.WebServices.ToString() = String.Empty Then

            If CheckAutonomo.Checked Then
                Autonomo = 1
            Else
                Autonomo = 0
            End If

            If WebService.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), Session("SIS_CNPJ"), Autonomo) Then
                If WebService.ValidarBDCC() Then

                    'lblMsgOK.Text = String.Empty
                    'lblMsgErro.Text = String.Empty
                    DescRetorno = "alert('BDCC:" & WebService.DescricaoRetorno & "');"

                    'If WebService.TipoBDCC = 1 Then
                    'PanelMsg.Visible = True
                    'lblMsgOK.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                    'lblMsgErro.Text = String.Empty
                    'End If

                    If WebService.TipoBDCC = 2 Then
                        'PanelMsg.Visible = True
                        'lblMsgOK.Text = String.Empty
                        'lblMsgErro.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                        Return False
                    End If

                    Return True

                End If
            End If

            Return False 'True

        End If

        Return True

    End Function

    Private Sub DesabilitarControles()

        txtNomeMotorista.Enabled = False
        txtTara.Enabled = False
        txtChassi.Enabled = False
        cbConteiner.Enabled = False
        txtNotaFiscal.Enabled = False
        txtSerie.Enabled = False
        txtDataEmissao.Enabled = False
        txtTransportadora.Enabled = False
        txtSerieDocSaida.Enabled = False
        txtNrDocSaida.Enabled = False
        txtEmissaoDocSaida.Enabled = False

        cbCNH.Enabled = False
        cbCavalo.Enabled = False
        cbCarreta.Enabled = False
        cbTipoDocSaida.Enabled = False

        CheckAutonomo.Enabled = False

        btNovo.Enabled = False
        btSalvar.Enabled = False
        btExcluir.Enabled = False
        'btVincular.Enabled = False

        dgPeriodos.Enabled = False
        dgNotas.Enabled = False

    End Sub

    Private Sub HabilitarControles()

        txtNomeMotorista.Enabled = True
        cbConteiner.Enabled = False 'Pois é somente edição
        txtNotaFiscal.Enabled = True
        txtSerie.Enabled = True
        txtDataEmissao.Enabled = True
        txtTransportadora.Enabled = True
        txtSerieDocSaida.Enabled = True
        txtNrDocSaida.Enabled = True
        txtEmissaoDocSaida.Enabled = True

        cbCNH.Enabled = True
        cbCavalo.Enabled = True
        cbCarreta.Enabled = True
        cbTipoDocSaida.Enabled = True

        CheckAutonomo.Enabled = True

        btNovo.Enabled = True
        btSalvar.Enabled = True
        btExcluir.Enabled = True
        'btVincular.Enabled = True

        dgPeriodos.Enabled = True
        dgNotas.Enabled = True

    End Sub

    Protected Sub dgNotas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgNotas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            'Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão da Nota: " & DataBinder.Eval(e.Row.DataItem, "NOTAFISCAL").ToString() & "\nDe série " & DataBinder.Eval(e.Row.DataItem, "SERIE").ToString() & ", produto " & DataBinder.Eval(e.Row.DataItem, "PRODUTO").ToString() & " e qtde " & DataBinder.Eval(e.Row.DataItem, "QTDE").ToString() & "?');")
            Dim Editar As ImageButton = DirectCast(e.Row.FindControl("cmdEditar"), ImageButton)
            Editar.Attributes.Add("onclick", "javascript:return " & "confirm('Confirmar edição de Nota Fiscal?');")
        End If

    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not Me.lblID.Text = String.Empty Then

            If Agendamento.AgendamentoComRegistro(Me.lblID.Text) > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser excluído pois já consta o registro da carga!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                Exit Sub
            End If

            If Agendamento.CancelarAgendamento(Me.lblID.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Agendamento Excluído com Sucesso!');location.href='ConsultarAgendamentos.aspx';</script>", False)
            End If

        End If

    End Sub

    Protected Sub cbCavalo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCavalo.SelectedIndexChanged

        CarregarListaCarretasPorCavalo()
        CarregarDadosVeiculo(cbCavalo.Text, cbCarreta.Text, Session("SIS_ID").ToString())

    End Sub

    Protected Sub cbCarreta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCarreta.SelectedIndexChanged

        If Not cbCavalo.Text = String.Empty And Not cbCarreta.Text = String.Empty Then
            CarregarDadosVeiculo(cbCavalo.Text, cbCarreta.Text, Session("SIS_ID").ToString())
        End If

    End Sub

    Protected Sub cbLote_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbConteiner.SelectedIndexChanged

        If cbConteiner.Items.Count > 0 Then
            If dgNotas.Rows.Count > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não há como alterar lote. Só pode ter um lote para cada agendamento\nSó há como trocar de lote caso não haja itens no agendamento.');</script>", False)
            Else
                lblConteinerSelecionado.Text = cbConteiner.SelectedValue
                CarregarItensCS(cbConteiner.SelectedValue, False)
                cbItensCS.Enabled = True
                Session("LOTE_DOCUMENTO") = Agendamento.ObterDocumentoLote(cbConteiner.SelectedValue)
            End If
        Else
            lblConteinerSelecionado.Text = String.Empty
            cbItensCS.Enabled = False
            txtQtde.Text = ""
            txtQtde.Enabled = False
        End If

    End Sub
    Private Sub CarregarListaCNHLendoCNH(ByVal CNH As String)

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ.CNH = CNH

        'Objetivo de Possibilitar Alteração da ComboBox do Motorista quando o resultado foi encontrado
        MotoristaOBJ.Transportadora = Transportadora
        MotoristaOBJ.Transportadora.ID = Session("SIS_ID").ToString
        MotoristaOBJ.Nome = Agendamento.ConsultarNome(MotoristaOBJ) 'Garantia de que é o nome inteiro do motorista
        Me.txtNomeMotorista.Text = MotoristaOBJ.Nome
        ConsultarCNH()
        Me.cbCNH.Text = MotoristaOBJ.CNH

        cbCNH.Enabled = True
        cbCavalo.Enabled = True
        cbCarreta.Enabled = True

    End Sub

    Protected Sub cbItensCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbItensCS.SelectedIndexChanged
        If cbItensCS.SelectedIndex <> 0 Then
            txtQtde.Enabled = True
        Else
            txtQtde.Enabled = False
            txtQtde.Text = ""
        End If
    End Sub

    Protected Sub btnAdicProduto_Click(sender As Object, e As EventArgs) Handles btnAdicProduto.Click

        Dim  AdicionaNF As Boolean = False

        If Agendamento.NFObrigatoria(cbConteiner.SelectedValue) Then
            If ValidarItem(True) Then

                AdicionaNF = True
            End If
        Else
            If ValidarItem(False) Then

                AdicionaNF = True
                If btSalvar.Enabled = False Then
                    btSalvar.Enabled = True
                End If
            End If
        End If

        If AdicionaNF Then

            If Not lblPatio.Text = String.Empty Then
                If Not Agendamento.ObterPatio(cbConteiner.SelectedValue) = lblPatio.Text Then
                    Dim Patio As String = Agendamento.ObterPatio(cbConteiner.SelectedValue)
                    PanelPatioInvalido.Visible = True
                    lblPatioInvalido.Text = "O Contêiner " & cbConteiner.SelectedItem.Text & " pertence ao Pátio " & Patio & ". Só poderão ser vinculados Lotes de um mesmo Pátio."
                    Exit Sub
                Else
                    PanelPatioInvalido.Visible = False
                    lblPatioInvalido.Text = String.Empty
                End If
            Else
                lblPatio.Text = Agendamento.ObterPatio(cbConteiner.SelectedValue)
            End If

            If txtNotaFiscal.Text.Trim() = "" Then 'Para selecionar itens corretos da Nota Fiscal (qdo documento não precisa de NF)
                txtNotaFiscal.Text = "0"
            End If

            If btnAdicProduto.CommandName = "ADICIONAR" Then
                InserirNotasFiscais()
            Else
                If btnAdicProduto.CommandName = "ALTERAR" Then
                    AlterarNotasFiscais()
                    btnAdicProduto.CommandName = "ADICIONAR"
                    btnAdicProduto.Text = "Adicionar Nota Fiscal"
                End If
            End If

            CarregarItensCS(lblConteinerSelecionado.Text, False)
            ConsultarNotas()

            'If ListaNotaFiscais.Count > 0 Then
            If dgNotas.Rows.Count > 0 Then
                ConsultarPeriodos()
                cbConteiner.Enabled = False
                btnExcluirProduto.Visible = True
            Else
                btnExcluirProduto.Visible = False
            End If

        End If

        If cbItensCS.SelectedIndex = 0 Then
            txtQtde.Text = ""
            txtQtde.Enabled = False
            txtNotaFiscal.Text = ""
            txtSerie.Text = ""
            txtDataEmissao.Text = ""
        End If

        CarregarItensCS(cbConteiner.SelectedValue, False)

    End Sub

    Public Function ObterSaldoItem() As Integer
        Dim Saldo As Integer
        Saldo = Agendamento.ConsultarSaldoCS(cbItensCS.SelectedValue)

        If btnAdicProduto.CommandName = "ALTERAR" And cbItensCS.SelectedValue = Session("NF_EDITADA_CODPRODANT") Then
            Saldo += Convert.ToInt32(Session("NF_EDITADA_QTDEANT"))
        End If

        Return Saldo
    End Function

    Protected Sub btnExcluirProduto_Click(sender As Object, e As EventArgs) Handles btnExcluirProduto.Click
        Agendamento.ExcluirNotas(lblID.Text)
        ConsultarDadosNota(lblID.Text)
        CarregarItensCS(cbConteiner.SelectedValue, False)
        lblPatio.Text = String.Empty
        cbConteiner.Enabled = True

        If btnAdicProduto.CommandName <> "ADICIONAR" Then
            btnAdicProduto.CommandName = "ADICIONAR"
            btnAdicProduto.Text = "Adicionar Nota Fiscal"
        End If
        btnExcluirProduto.Visible = False
    End Sub

End Class
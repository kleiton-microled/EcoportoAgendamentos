Imports System.Globalization
Imports System.Web.Services

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

            If Not Request.QueryString("action") IsNot Nothing Then
                ConsultarLotes()

            End If

            If Request.QueryString("lote") IsNot Nothing Then

                Dim IdAgendamento = Agendamento.ExistePreAgendamento(Request.QueryString("lote").ToString(), Session("SIS_ID").ToString(), Val(Session("SIS_TRANSPEMPRESA")))

                If IdAgendamento > 0 Then
                    Response.Redirect("CadastrarAgendamentos.aspx?id=" & IdAgendamento & "&action=edit")
                End If

                Me.cbLote.SelectedValue = Request.QueryString("lote").ToString()
                Me.pnlMsgErro.Visible = False
                Me.lblMsgErro.Text = ""
                lblLoteSelecionado1.Text = Request.QueryString("lote").ToString()
                CarregarItensCS(Request.QueryString("lote").ToString())
                cbItensCS.Enabled = True
                Session("LOTE_DOCUMENTO") = Agendamento.ObterDocumentoLote(Request.QueryString("lote").ToString())
                'CarregarFrames()
                ConsultarPeriodos(Request.QueryString("lote").ToString())
            End If

            If Not Page.IsPostBack Then
                Session("DOC_ALTERADO") = Nothing
            End If

            If Not Request.QueryString("id") Is Nothing Then
                If Integer.TryParse(Request.QueryString("id").ToString(), I) Then
                    If Agendamento.VerificarAgendTransp(Request.QueryString("id").ToString(), Session("SIS_ID").ToString()) Then
                        If Not Request.QueryString("action") Is Nothing Then
                            If Request.QueryString("action").ToString() = "edit" Then
                                Editar(Request.QueryString("id").ToString())
                            End If
                        End If
                    Else
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = "Id do agendamento não pertence à transportadora logada!"
                    End If
                Else
                    Response.Redirect("ConsultarAgendamentos.aspx")
                End If
            Else
                NovoAgendamento()
            End If

        End If

    End Sub

    Private Sub Editar(ByVal ID As String)

        Dim Ds1 As New DataTable
        Dim Lote As String
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

        'ConsultarLotes()
        CarregarDados(Agendamento.ObterCNHPeloIdAgendamento(lblID.Text))
        ConsultarDadosNota(lblID.Text)
        'ConsultarDadosItens(lblID.Text)

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

        ConsultarPeriodos()
        lblPeriodo.Visible = True

        Lote = Agendamento.ConsultarLoteAgendamento(lblID.Text)
        lblLoteSelecionado1.Text = Lote
        If Lote <> "-1" Then
            CarregarItensCS(Lote)
        End If
        Session("LOTE_DOCUMENTO") = Agendamento.ObterDocumentoLote(Lote)

        Dim LoteDoc As String = Agendamento.ConsultarLoteDocumentoDoAgendamento(Lote)

        Me.cbLote.Items.Clear()
        Me.cbLote.Items.Add(New ListItem(LoteDoc, Lote))


        If Not Agendamento.AgendamentoFinalizado(Convert.ToInt32(lblID.Text)) And dgNotas.Rows.Count = 0 Then
            cbLote.Enabled = True
        End If

        Dim liberado = Banco.Conexao.Execute("SELECT NVL(FLAG_LIBERADO,0) FLAG_LIBERADO FROM TB_AG_CS WHERE AUTONUM = " & Me.lblID.Text).Fields(0).Value

        If Val(liberado) > 0 Then
            dgNotas.Columns(12).Visible = False
            dgNotas.Columns(13).Visible = False
        End If

        CarregarFrames()

    End Sub

    Private Sub CarregarDadosProtocolo()
        hddnTransportadora.Value = Session("SIS_RAZAO").ToString()
    End Sub

    Private Sub ConsultarLotes()

        'Dim Acao As String
        Dim Lote As String

        If Request.QueryString("action") = "edit" Then
            'Acao = "edit"
            Lote = Agendamento.ConsultarLoteAgendamento(Request.QueryString("id"))
            Me.cbLote.DataSource = Agendamento.ConsultarLotes(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")), Lote)
            Me.cbLote.DataBind()

            If cbLote.Items.Count = 0 Then
                cbLote.Items.Insert(0, New ListItem(Lote, Lote))
            End If
        Else
            'Acao = "new"
            If Request.QueryString("lote") IsNot Nothing Then
                Lote = Request.QueryString("lote")
                Me.cbLote.DataSource = Agendamento.ConsultarLotes(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")), Lote)
            Else
                Me.cbLote.DataSource = Agendamento.ConsultarLotes(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")))
            End If
            Me.cbLote.DataSource = Agendamento.ConsultarLotes(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")))
            Me.cbLote.DataBind()
            If Request.QueryString("lote") Is Nothing Then cbLote.Items.Insert(0, New ListItem("SELECIONE...", 0))
        End If
        'Me.cbLote.DataSource = Agendamento.ConsultarLotes(Session("SIS_ID").ToString(), Acao, Int(Session("SIS_TRANSPEMPRESA")))

        If Request.QueryString("action") = "edit" Then
            If Lote <> "-1" Then    'Lote já é acertado acima
                cbLote.SelectedValue = Lote
            Else
                cbLote.Items.Insert(0, New ListItem("", 0))
                cbLote.SelectedIndex = 0
            End If
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

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        'Dim CPF As String
        cbCNH.Items.Clear()
        Motorista = Nothing
        If Not txtNomeMotorista.Text = String.Empty Then
            'CPF = Motorista.ObterCPFMotorista(txtNomeMotorista.Text.Trim())
            'CarregarDados()
            CarregarListaCNH(txtNomeMotorista.Text.ToUpper.Trim(), "NomeMotoristaDigitado")
            CarregarDadosVeiculo()
            If cbCNH.Items.Count = 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Nenhum motorista foi encontrado."
            End If
        Else
            ConsultarCNH()
            CarregarDadosVeiculo()
            cbCNH.Enabled = True
            cbCavalo.Enabled = True
            cbCarreta.Enabled = True
        End If

        CarregarFrames()

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

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

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
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Nenhum motorista foi encontrado."
        End If

        TransportadoraOBJ = Nothing
        MotoristaOBJ = Nothing

    End Sub
    '''<summary>
    ''' Carrega os produtos que têm num determinado lote; AutonumProd é Autonum da Mercadoria, caso não exista é -1
    ''' </summary>
    Private Sub CarregarItensCS(ByVal Lote As String, Optional ByVal AutonumProd As Integer = -1)
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
        DTItensCS = Agendamento.ConsultarItensCargaSolta(Lote)

        For i As Integer = 0 To DTItensCS.Rows.Count - 1
            CodProduto = DTItensCS.Rows(i)("AUTONUM").ToString()
            Embalagem = DTItensCS.Rows(i)("EMBALAGEM").ToString()
            ProdutoCS = DTItensCS.Rows(i)("PRODUTO").ToString()
            Qtde = DTItensCS.Rows(i)("QUANTIDADE").ToString()
            SaldoBD = DTItensCS.Rows(i)("SALDO").ToString()
            Saldo = Convert.ToInt32(SaldoBD)

            If Saldo > 0 Or Request.QueryString("action") IsNot Nothing Then
                TextoItensCS = "Qtde: " & Qtde & " | Embalagem: " & Embalagem & " | Produto: " & ProdutoCS & " | Saldo: " & SaldoBD & ""
                cbItensCS.Items.Insert(index, New ListItem(TextoItensCS, CodProduto))
                index += 1
            End If

        Next

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

        If Me.dgNotas.Rows.Count > 0 Then
            For Each Linha As GridViewRow In Me.dgNotas.Rows
                Lotes = Lotes & Linha.Cells(6).Text & ","
            Next

            If Lotes.Substring(Lotes.Length - 1, 1).Equals(",") Then
                Lotes = Lotes.Remove(Lotes.Length - 1, 1)
            End If

            ConsultarPeriodos(Lotes)
        End If

    End Sub

    Protected Sub cbCNH_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCNH.SelectedIndexChanged

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If Not cbCNH.SelectedItem.Text = String.Empty Then

            CarregarDados(cbCNH.SelectedItem.Text)

            Try

                Dim dadosCNH = Motorista.ObterDadosCNHMotorista(cbCNH.SelectedItem.Text, Session("SIS_ID").ToString())
                'Dim ValidadeCNH As String = Motorista.ObterValidadeCNH(cbCNH.SelectedItem.Text, Session("SIS_ID").ToString())

                Dim Validade As DateTime = Convert.ToDateTime(dadosCNH.Validade).AddDays(30)

                If Validade < Convert.ToDateTime(DateTime.Now) Then
                    Me.cbCNH.SelectedIndex = -1
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A CNH do motorista está vencida. Escolha um outro motorista."
                    Exit Sub
                End If

                Me.txtNomeMotorista.Text = dadosCNH.Nome
                Me.txtCelular.Text = dadosCNH.Celular
                Me.txtNextel.Text = dadosCNH.Nextel
                Me.txtValidadeCNH.Text = dadosCNH.CNH
                Me.txtCPF.Text = dadosCNH.CPF

            Catch ex As Exception
                Console.Write(ex.Message)
            End Try

        End If

        CarregarFrames()

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        NovoAgendamento()

    End Sub

    Private Sub NovoAgendamento()
        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If lblID.Text = String.Empty Then

            Transportadora.ID = Session("SIS_ID").ToString()

            Dim ID As String = Agendamento.CriarAgendamento(Transportadora)

            If Not ID Is Nothing Then

                lblID.Text = ID

                txtNomeMotorista.Enabled = True
                cbCNH.Enabled = True
                CheckAutonomo.Enabled = True
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
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Agendamento Cancelado."
            End If

        End If
    End Sub

    Private Function ValidarItem(Obrigatoria As Boolean) As Boolean

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If cbLote.SelectedValue = "0" Then 'If cbLote.SelectedIndex = 0 Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Selecione o Lote/BL."
            Return False
        End If

        If cbItensCS.SelectedIndex = 0 Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Selecione o Item da Carga Solta."
            Return False
        End If

        If Obrigatoria Then
            If txtNotaFiscal.Text = String.Empty Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Informe o Número da Nota Fiscal."
                Return False
            End If

            If txtSerie.Text = String.Empty Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Informe a Série da Nota Fiscal."
                Return False
            End If

            If Not txtDataEmissao.Text = String.Empty Then
                If Not IsDate(txtDataEmissao.Text) Then
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A Data de Emissão da Nota Fiscal é inválida."
                    Return False
                Else
                    Dim dtEmissaoNF As DateTime = Convert.ToDateTime(txtDataEmissao.Text)
                    If dtEmissaoNF > Now Then
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = "A Data de Emissão da Nota Fiscal deverá ser menor que a data atual."
                        Return False
                    End If
                End If
            Else
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Informe a Data e Emissão da Nota Fiscal."
                Return False
            End If
        End If

        If cbLote.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Selecione o Lote."
            Return False
        End If

        Dim q As Integer
        If txtQtde.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Digite a quantidade do item a ser inserida."
        End If

        If Integer.TryParse(txtQtde.Text, q) Then
            If q <= 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "A quantidade informada deverá ser maior ou igual a 1"
                Return False
            End If

            If q > ObterSaldoItem() Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "A quantidade informada do item é maior que o saldo."
                Return False
            End If
        Else
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "A quantidade informada deverá ser valor inteiro maior ou igual a 1"
            Return False
        End If

        Return True

    End Function

    Public Function Validar() As Boolean

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If lblPeriodo.Text = "Nenhum período foi selecionado." Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Nenhum período foi selecionado."
            Return False
        End If

        If txtNomeMotorista.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe o Nome do Motorista."
            Return False
        End If

        If cbCNH.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a CNH do Motorista."
            Return False
        End If

        Try

            Dim ValidadeCNH As String = Motorista.ObterValidadeCNH(cbCNH.SelectedItem.Text, Session("SIS_ID").ToString())

            Dim Validade As DateTime = Convert.ToDateTime(ValidadeCNH)

            If Validade < Convert.ToDateTime(DateTime.Now) Then
                Me.cbCNH.SelectedIndex = -1
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "A CNH do motorista está vencida. Escolha um outro motorista."
                Return False
            End If

        Catch ex As Exception
            Console.Write(ex.Message)
        End Try

        If cbCavalo.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a Placa do Cavalo."
            Return False
        End If

        If cbCarreta.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a Placa da Carreta."
            Return False
        End If

        If cbTipoDocSaida.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe o Tipo do Documento de Saída."
            Return False
        End If

        If txtSerieDocSaida.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a Série do Documento de Saída."
            Return False
        End If

        If txtNrDocSaida.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe o Número do Documento de Saída."
            Return False
        End If

        If Not txtEmissaoDocSaida.Text = String.Empty Then
            If IsDate(txtEmissaoDocSaida.Text) Then
                If Convert.ToDateTime(txtEmissaoDocSaida.Text) > Now Then
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A Data de Emissão do Documento deverá ser menor que a data atual."
                    Return False
                End If
                If Convert.ToDateTime(txtEmissaoDocSaida.Text, New CultureInfo("pt-BR")).Date < Convert.ToDateTime(Now, New CultureInfo("pt-BR")).AddDays(-90).Date Then '90 dias (aprox 3 meses está momentaneamente sendo provisório)
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A Data de Emissão do Documento é muito antiga."
                    Return False
                End If
            End If
        Else
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a Data de Emissão do Documento de Saída."
            Return False
        End If

        If dgNotas.Rows.Count = 0 Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Não há nenhuma Nota Fiscal e nenhum produto inserido no agendamento"
            Return False
        End If

        ' INÍCIO ========================== ANTECIPAÇÂO REGISTRO R.A ========================== 

        ' Transportadora tem que ter um email cadastrado

        If Documento.ExigeEmailCadastrado() Then

            Dim EmailAgendamento As String = Transportadora.ObterEmailAgendamento(Val(Session("SIS_ID").ToString()))

            If String.IsNullOrEmpty(EmailAgendamento) Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Você deverá informar um email caso houver divergências na documentação."
                Return False
            End If

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

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        Dim Index As Integer = e.CommandArgument
        Dim Autonum As String = dgNotas.DataKeys(Index)("AUTONUM").ToString()
        Dim Lote As String = dgNotas.DataKeys(Index)("LOTE").ToString()
        Dim Nota As String = dgNotas.DataKeys(Index)("NOTAFISCAL").ToString()
        Dim Documento As String = dgNotas.DataKeys(Index)("TIPO").ToString()
        Dim Serie As String = dgNotas.DataKeys(Index)("SERIE").ToString()
        Dim Emissao As String = dgNotas.DataKeys(Index)("EMISSAO").ToString()
        Dim Qtde As String = dgNotas.DataKeys(Index)("QTDE").ToString()
        Dim idProduto As String = dgNotas.DataKeys(Index)("AUTONUM_PRODUTO").ToString()

        If e.CommandName = "DEL" Then

            If dgNotas.Rows.Count = 1 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Não há como excluir porque só tem uma Nota Fiscal para o agendamento"
            Else
                Agendamento.ExcluirNF(Autonum)
                ConsultarNotas()
                CarregarItensCS(lblLoteSelecionado1.Text)
                pnLegenda.Visible = False
            End If

        ElseIf e.CommandName = "EDITAR" Then

            Session("NF_EDITADA") = Autonum
            Session("NF_EDITADA_QTDEANT") = Qtde
            Session("NF_EDITADA_CODPRODANT") = idProduto

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
            CarregarItensCS(Lote, idProduto)

            Try
                cbItensCS.SelectedValue = idProduto

            Catch ex As Exception

            End Try


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

    Private Sub ConsultarDadosItens(ByVal ID As Integer)

        Dim Ds As New DataTable
        Ds = Agendamento.ConsultarItens(ID)

        Dim Contador As Integer = 1

        If Ds.Rows.Count > 0 Then
            For Each Item In Ds.Rows
                ListaItens.Add(New NotaFiscalItem(Item("AUTONUM").ToString(), Contador, lblID.Text, Item("AUTONUM_NF").ToString(), Item("LOTE").ToString(), Item("QTDE").ToString(), Item("AUTONUM_BCG").ToString(), Item("PRODUTO").ToString(), Item("EMBALAGEM").ToString()))
                Contador = Contador + 1
            Next

        End If

    End Sub

    Protected Sub dgPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgPeriodos.RowCommand

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        Dim Index As Integer = e.CommandArgument
        Dim Autonum As String = dgPeriodos.DataKeys(Index)("AUTONUM_GD_RESERVA").ToString()
        Dim Periodo_Inicial As String = dgPeriodos.DataKeys(Index)("PERIODO_INICIAL").ToString()
        Dim Periodo_Final As String = dgPeriodos.DataKeys(Index)("PERIODO_FINAL").ToString()
        Dim Saldo As String = dgPeriodos.DataKeys(Index)("SALDO").ToString()
        Dim DTA As String = dgPeriodos.DataKeys(Index)("FLAG_DTA").ToString()

        If DTA Then
            If Saldo = 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Saldo insuficiente para agendamento."
                Exit Sub
            End If
        End If

        Dim TransportadoraOBJ As New Transportadora With {.ID = Session("SIS_ID").ToString()}
        Dim VeiculoOBJ As New Veiculo With {.Cavalo = cbCavalo.Text, .Carreta = cbCarreta.Text, .Transportadora = TransportadoraOBJ}
        Dim CodigoVeiculo As Integer = VeiculoOBJ.ObterID(VeiculoOBJ)

        Dim Patio As String = Agendamento.ObterPatio(cbLote.SelectedValue)

        If Request.QueryString("id") IsNot Nothing Then
            If Agendamento.AgendamentoMesmoPatioPeriodo(TransportadoraOBJ.ID, CodigoVeiculo, Convert.ToDateTime(Periodo_Inicial).ToString("dd/MM/yyyy HH:mm"), Convert.ToDateTime(Periodo_Final).ToString("dd/MM/yyyy HH:mm"), Val(Patio), Val(Me.lblID.Text)) > 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Já existe um outro agendamento com esse mesmo veículo/perído em outro pátio."
                Exit Sub
            End If
        Else
            If Agendamento.AgendamentoMesmoPatioPeriodo(TransportadoraOBJ.ID, CodigoVeiculo, Convert.ToDateTime(Periodo_Inicial).ToString("dd/MM/yyyy HH:mm"), Convert.ToDateTime(Periodo_Final).ToString("dd/MM/yyyy HH:mm"), Val(Patio)) > 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Já existe um outro agendamento com esse mesmo veículo/perído em outro pátio"
                Exit Sub
            End If
        End If

        lblCodigoPeriodo.Text = Autonum

        If Agendamento.ConteinerComPendenciaRecebimento(lblCodigoPeriodo.Text, cbLote.SelectedValue) Then
            If Agendamento.ConsultarFormaPagamento(cbLote.SelectedValue) = 2 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Carga com Pendência de recebimento neste Periodo. Favor escolher outro período."
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

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If Me.cbLote.SelectedValue IsNot Nothing Then
            Agendamento.ColocarLoteAgendamento(Me.cbLote.SelectedValue, Me.lblID.Text)
        Else
            If Request.QueryString("lote") IsNot Nothing Then
                Agendamento.ColocarLoteAgendamento(Request.QueryString("lote").ToString(), Me.lblID.Text)
            End If
        End If


        If Validar() Then

            If ValidarPeriodo() Or (Session("LOTE_DOCUMENTO").ToString() = "DTA" Or Session("LOTE_DOCUMENTO").ToString().Trim() = "DTA-S") Then

                Dim CPF As String = Motorista.ObterCPFMotorista(cbCNH.SelectedValue.Trim, Convert.ToInt32(Session("SIS_ID").ToString()))

                If ValidarBDCC(CPF) Then

                    Dim TransportadoraOBJ As New Transportadora
                    TransportadoraOBJ.ID = Session("SIS_ID").ToString()

                    Dim VeiculoOBJ As New Veiculo With {
                        .Cavalo = cbCavalo.Text,
                        .Carreta = cbCarreta.Text,
                        .Transportadora = TransportadoraOBJ
                    }

                    VeiculoOBJ.ID = Veiculo.ObterID(VeiculoOBJ)

                    Dim MotoristaOBJ As New Motorista With {
                        .Nome = txtNomeMotorista.Text,
                        .CNH = cbCNH.Text,
                        .Transportadora = TransportadoraOBJ
                    }

                    Dim AgendamentoOBJ As New Agendamento With {
                        .Codigo = lblID.Text,
                        .EmissaoDocSaida = txtEmissaoDocSaida.Text,
                        .TipoDocSaida = cbTipoDocSaida.SelectedValue,
                        .NumDocSaida = txtNrDocSaida.Text,
                        .SerieDocSaida = txtSerieDocSaida.Text,
                        .Motorista = MotoristaOBJ,
                        .Periodo = lblCodigoPeriodo.Text,
                        .Transportadora = TransportadoraOBJ,
                        .Veiculo = VeiculoOBJ,
                        .Lote = cbLote.SelectedValue
                    }

                    Dim NotaFiscalOBJ As New NotaFiscal With {
                        .NotaFiscal = txtNotaFiscal.Text,
                        .Serie = txtSerie.Text,
                        .Emissao = txtDataEmissao.Text
                    }

                    Dim CodigoMotorista As Integer = Agendamento.ObterCodigoMotorista(AgendamentoOBJ.Motorista)
                    Dim Doct As Integer = Documento.ObterTipoDocumento(Me.cbLote.SelectedValue)
                    Dim QtdeRequerida As Integer = Documento.ObterQuantidadeRequerida(Doct)
                    Dim QtdeVinculada As Integer = 0

                    QtdeVinculada = Documento.ConsultarDoc(Me.cbLote.SelectedValue)


                    Dim Bloqueio = Banco.Conexao.Execute("SELECT NVL(FLAG_AGENDAMENTO_DOCUMENTOS,0) FROM TB_PARAMETROS_SISTEMA").Fields(0).Value

                    Dim ds As New DataTable

                    If QtdeVinculada < QtdeRequerida Then

                        Dim dsAux As New DataTable
                        dsAux = Documento.DocumentosQueFaltam(Me.lblID.Text, Doct)

                        Dim ausentes As New List(Of String)

                        If dsAux IsNot Nothing Then
                            If dsAux.Rows.Count > 0 Then

                                For Each Doc In dsAux.Rows
                                    If Val(Doc("AUTONUM").ToString()) = 8 Or Val(Doc("AUTONUM").ToString()) = 9 Then
                                        If ds.Rows(0)("PLACA_CAVALO").ToString() <> ds.Rows(0)("PLACA_CARRETA").ToString() Then
                                            ausentes.Add(Doc("DESCRICAO").ToString())
                                        End If
                                    Else
                                        ausentes.Add(Doc("DESCRICAO").ToString())
                                    End If
                                Next

                            End If
                        End If

                        If Val(Bloqueio) > 0 Then
                            Me.lblMsgErro.Text = "Falta anexar os seguintes documentos: " & String.Join(",", ausentes) & "</b> "
                            Me.pnlMsgErro.Visible = True
                            Exit Sub
                        End If

                    End If

                    If lblProtocolo.Text = "Não Gerado" Then
                        If Agendamento.Agendar(AgendamentoOBJ) Then

                            Agendamento.InsereAgendamentoNaFila(cbLote.SelectedValue, lblCodigoPeriodo.Text, Convert.ToInt32(Session("SIS_USRID")))
                            Agendamento.AlterarDataSaida(AgendamentoOBJ.Codigo)

                            EnviarEmailPlanejamento()

                            If (Session("LOTE_DOCUMENTO").ToString() = "DTA" Or Session("LOTE_DOCUMENTO").ToString().Trim() = "DTA-S") Then

                                Dim dsDTA As New DataTable
                                dsDTA = Documento.DocumentosObrigatoriosDTA(Val(Me.lblID.Text), Val(Me.cbLote.SelectedValue))

                                If dsDTA IsNot Nothing Then
                                    If dsDTA.Rows.Count >= 2 Then
                                        Banco.Conexao.Execute("UPDATE TB_AG_CS SET IMPRESSO = 0, FLAG_LIBERADO = 1, DT_AGENDAMENTO_LIBERACAO = SYSDATE, USUARIO_LIBERACAO = " & Val(Session("SIS_USRID").ToString()) & " WHERE AUTONUM = " & Me.lblID.Text)
                                        Response.Redirect(ResolveUrl("~/ConsultarAgendamentos.aspx"), True)
                                    End If
                                End If

                            End If

                            Response.Redirect("Concluido.aspx?id=" & Me.lblID.Text & "&motorista=" & CodigoMotorista & "&veiculo=" & VeiculoOBJ.ID)

                        End If
                    Else

                        Dim TeveAlteracoesDeDocumentos As Boolean = False

                        ' Essa Session fica na página VinculaDocumentos.aspx e indica quando é incluído algum documento em modo edição
                        ' Se a session estiver preenchida e a quantidade vinculada for maior ou igual a quantidade de documentso exigida marca a flag TeveAlteracoesDeDocumentos = true
                        If Session("DOC_ALTERADO") IsNot Nothing And QtdeVinculada >= QtdeRequerida Then
                            TeveAlteracoesDeDocumentos = True
                        End If

                        'obtém dados do agendamento original
                        Dim dsAgAntigo As New DataTable
                        dsAgAntigo = Agendamento.ConsultarDadosAgendamento(Val(Me.lblID.Text))


                        ' obtém a documentação anexada
                        QtdeVinculada = Documento.ConsultarDoc(Me.cbLote.SelectedValue)

                        ' obtém a quantidade vinculada

                        ' se a placa cavalo for igual a placa carreta (um caminhão truck, por exemplo), a  placa da carreta não é obrigatória e subtrai um documento da lista
                        If Me.cbCavalo.Text = Me.cbCarreta.Text Then
                            QtdeRequerida = QtdeRequerida - 1
                        End If

                        ' captura os campos do agendamento original caso o usuário desista da operação
                        Session("MOTORISTA_ANTERIOR") = dsAgAntigo.Rows(0)("AUTONUM_MOTORISTA").ToString()
                        Session("VEICULO_ANTERIOR") = dsAgAntigo.Rows(0)("AUTONUM_VEICULO").ToString()
                        Session("PERIODO_ANTERIOR") = dsAgAntigo.Rows(0)("AUTONUM_PERIODO").ToString()
                        Session("TIPO_DOC_SAIDA_ANTERIOR") = cbTipoDocSaida.SelectedValue
                        Session("NUM_DOC_SAIDA_ANTERIOR") = txtNrDocSaida.Text
                        Session("SERIE_DOC_SAIDA_ANTERIOR") = txtSerieDocSaida.Text
                        Session("EMISSAO_DOC_SAIDA_ANTERIOR") = txtEmissaoDocSaida.Text
                        Session("LOTE_ANTERIOR") = cbLote.SelectedValue
                        Session("CNH_ANTERIOR") = dsAgAntigo.Rows(0)("CNH").ToString()
                        Session("PLACA_CAVALO_ANTERIOR") = dsAgAntigo.Rows(0)("PLACA_CAVALO").ToString()
                        Session("PLACA_CARRETA_ANTERIOR") = dsAgAntigo.Rows(0)("PLACA_CARRETA").ToString()

                        ' atualiza o agendamento
                        If Agendamento.AlterarAgendamento(AgendamentoOBJ, TeveAlteracoesDeDocumentos) Then

                            Agendamento.InsereAgendamentoNaFila(cbLote.SelectedValue, lblCodigoPeriodo.Text, Convert.ToInt32(Session("SIS_USRID")))
                            Agendamento.AlterarDataSaida(AgendamentoOBJ.Codigo)

                            If TeveAlteracoesDeDocumentos Then
                                Response.Redirect(ResolveUrl("~/Concluido.aspx?id=" & Me.lblID.Text & "&edit=1&motorista=" & CodigoMotorista & "&veiculo=" & VeiculoOBJ.ID), True)
                            Else
                                Response.Redirect(ResolveUrl("~/ConsultarAgendamentos.aspx"), True)
                            End If

                        Else
                            Me.pnlMsgErro.Visible = True
                            Me.lblMsgErro.Text = "Erro durante o Agendamento. Tente Novamente."
                        End If


                    End If

                End If
            Else

                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Saldo insuficiente. Favor Escolher outro Periodo."

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
        End If

    End Sub

    Private Sub EnviarEmailPlanejamento()

        Dim EmailPlanejamento = Banco.Conexao.Execute("SELECT EMAIL_PLANEJAMENTO FROM SGIPA.TB_PARAMETROS_SISTEMA").Fields(0).Value.ToString()

        Dim AgendamentoOBJ As New Agendamento()

        Dim Cargas = AgendamentoOBJ.ObterCargaAgendada(Me.lblID.Text)

        Dim CargaDescricao = String.Empty

        If Cargas IsNot Nothing Then
            For Each Item As DataRow In Cargas.Rows
                CargaDescricao = CargaDescricao & Item("CARGA").ToString() & "<br/>"
            Next
        End If

        Dim Msg = "Santos, " & DateTime.Now.ToString() & " <br /><br />
                                    <strong>Novo Agendamento de Carga Solta:</strong> <br /><br />" &
                "<strong>Transportadora:</strong> " & hddnTransportadora.Value & "<br />" &
                "<strong>Lote:</strong> " & Me.cbLote.SelectedValue & "<br />" &
                "<strong>Carga:<br /></strong> " & CargaDescricao & "<br /><br />" &
                "<strong>Motorista:</strong> " & txtNomeMotorista.Text & " " & Me.cbCNH.Text & "<br />" &
                "<strong>Veículo:</strong> " & Me.cbCavalo.Text & " / " & Me.cbCarreta.Text & "<br />" &
                "<strong>Período:</strong> " & lblPeriodo.Text & ""

        Email.EnviarEmail("registroimportacao@ecoportosantos.com.br", EmailPlanejamento, "Novo Agendamento", String.Empty, String.Empty, Msg)

    End Sub

    Public Function InserirNotasFiscais() As Boolean

        If Agendamento.InserirNF(Val(Me.cbLote.SelectedValue), txtNotaFiscal.Text, txtSerie.Text, txtDataEmissao.Text, lblID.Text, cbItensCS.SelectedValue, txtQtde.Text) Then
            Return True
        End If

        Return False

    End Function

    Public Function AlterarNotasFiscais() As Boolean

        If Agendamento.AlterarNF(Val(Me.cbLote.SelectedValue), txtNotaFiscal.Text, txtSerie.Text, txtDataEmissao.Text, cbItensCS.SelectedValue, txtQtde.Text, Session("NF_EDITADA").ToString()) Then
            Return True
        End If

        Return False

    End Function

    Private Function ValidarBDCC(ByVal CPF As String) As Boolean

        Dim Autonomo As New Integer
        Dim WebService As New WebService

        Me.pnlMsgErro.Visible = False

        If Not My.Settings.AgendamentoCargaSoltaSGIPA_WsBDCC_WsSincrono.ToString() = String.Empty Then

            If CheckAutonomo.Checked Then
                Autonomo = 1
            Else
                Autonomo = 0
            End If

            If WebService.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), Session("SIS_CNPJ"), Autonomo) Then
                If WebService.ValidarBDCC() Then
                    DescRetorno = WebService.DescricaoRetorno
                    If WebService.TipoBDCC = 2 Then
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = DescRetorno
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
        cbLote.Enabled = False
        txtNotaFiscal.Enabled = False
        txtSerie.Enabled = False
        txtDataEmissao.Enabled = False
        'txtTransportadora.Enabled = False
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
        cbLote.Enabled = False 'Pois é somente edição
        txtNotaFiscal.Enabled = True
        txtSerie.Enabled = True
        txtDataEmissao.Enabled = True
        'txtTransportadora.Enabled = True
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
            Dim Editar As ImageButton = DirectCast(e.Row.FindControl("cmdEditar"), ImageButton)
            Editar.Attributes.Add("onclick", "javascript:return " & "confirm('Confirmar edição de Nota Fiscal?');")
        End If

    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not lblID.Text = String.Empty Then
            If Agendamento.CancelarAgendamento(lblID.Text) Then
                Response.Redirect("Agendamento Excluído com Sucesso!")
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

        CarregarFrames()

    End Sub

    Private Sub CarregarFrames()

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

        Dim CodigoMotorista As Integer = Agendamento.ObterCodigoMotorista(MotoristaOBJ)
        Dim CodigoVeiculo As Integer = Veiculo.ObterID(VeiculoOBJ)

        If Request.QueryString("id") Is Nothing Then
            frameDocumentos.Attributes("src") = "VinculaDocumentos.aspx?lote=" & Me.cbLote.SelectedValue & "&id=" & Val(Me.lblID.Text) & "&motorista=" & CodigoMotorista & "&veiculo=" & CodigoVeiculo
        Else
            frameDocumentos.Attributes("src") = "VinculaDocumentos.aspx?edit=1&lote=" & Me.cbLote.SelectedValue & "&id=" & Val(Me.lblID.Text) & "&motorista=" & CodigoMotorista & "&veiculo=" & CodigoVeiculo
        End If

        frameEmails.Attributes("src") = "EmailTransportadora.aspx?id=" & Val(Session("SIS_ID").ToString())

        frameDocumentos.Visible = True
        frameEmails.Visible = True

    End Sub

    Protected Sub cbLote_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLote.SelectedIndexChanged

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If cbLote.Items.Count > 0 Then
            If dgNotas.Rows.Count > 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Não há como alterar lote. Só pode ter um lote para cada agendamento\nSó há como trocar de lote caso não haja itens no agendamento."
            Else
                lblLoteSelecionado1.Text = cbLote.SelectedValue
                CarregarItensCS(cbLote.SelectedValue)
                cbItensCS.Enabled = True
                Session("LOTE_DOCUMENTO") = Agendamento.ObterDocumentoLote(cbLote.SelectedValue)

                CarregarFrames()

            End If
            If cbLote.SelectedValue <> 0 Then
                Agendamento.ColocarLoteAgendamento(cbLote.SelectedValue, lblID.Text)
            End If
        Else
            lblLoteSelecionado1.Text = String.Empty
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

        Dim AdicionaNF As Boolean = False

        If Agendamento.NFObrigatoria(cbLote.SelectedValue) Then
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
                If Not Agendamento.ObterPatio(cbLote.SelectedValue) = lblPatio.Text Then
                    Dim Patio As String = Agendamento.ObterPatio(cbLote.SelectedValue)
                    PanelPatioInvalido.Visible = True
                    lblPatioInvalido.Text = "O Lote " & cbLote.SelectedValue & " pertence ao Pátio " & Patio & ". Só poderão ser vinculados Lotes de um mesmo Pátio."
                    Exit Sub
                Else
                    PanelPatioInvalido.Visible = False
                    lblPatioInvalido.Text = String.Empty
                End If
            Else
                lblPatio.Text = Agendamento.ObterPatio(cbLote.SelectedValue)
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

            CarregarItensCS(lblLoteSelecionado1.Text)
            ConsultarNotas()

            If dgNotas.Rows.Count > 0 Then
                ConsultarPeriodos()
                cbLote.Enabled = False
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
        CarregarItensCS(cbLote.SelectedValue)
        lblPatio.Text = String.Empty
        cbLote.Enabled = True

        If btnAdicProduto.CommandName <> "ADICIONAR" Then
            btnAdicProduto.CommandName = "ADICIONAR"
            btnAdicProduto.Text = "Adicionar Nota Fiscal"
        End If
        btnExcluirProduto.Visible = False
    End Sub

    <WebMethod>
    Public Shared Function ObterDadosMotoristaPorCNH(cnh As String, transportadora As String) As Motorista

        Try
            Dim motorista = New Motorista()
            motorista = motorista.ObterDadosCNHMotorista(cnh, transportadora)
            Return motorista

        Catch ex As Exception
            Throw New Exception("Não foi possível obter os dados do motorista")
        End Try
    End Function

    <WebMethod>
    Public Shared Function ObterDadosMotoristaPorNome(nome As String, transportadora As String) As Motorista

        'Try

        '    Dim motorista = New Motorista()
        '    If Not nome = String.Empty Then
        '        'CPF = Motorista.ObterCPFMotorista(txtNomeMotorista.Text.Trim())
        '        'CarregarDados()
        '        CarregarListaCNH(nome.ToUpper.Trim(), "NomeMotoristaDigitado")
        '        CarregarDadosVeiculo()
        '        If cbCNH.Items.Count = 0 Then
        '            Me.pnlMsgErro.Visible = True
        '            Me.lblMsgErro.Text = "Nenhum motorista foi encontrado."
        '        End If
        '    Else
        '        ConsultarCNH()
        '        CarregarDadosVeiculo()
        '        cbCNH.Enabled = True
        '        cbCavalo.Enabled = True
        '        cbCarreta.Enabled = True
        '    End If

        '    CarregarFrames()

        '    Dim motorista = New Motorista()
        '    motorista = motorista.ObterDadosCNHMotorista(cnh, transportadora)
        '    Return motorista

        'Catch ex As Exception
        '    Throw New Exception("Não foi possível obter os dados do motorista")
        'End Try
    End Function


    <WebMethod>
    Public Shared Function ObterMotoristas(termo As String, transportadora As String) As List(Of Motorista)

        Try
            Dim agendamentoDAO = New Agendamento()
            Dim listaMotorista = New List(Of Motorista)
            listaMotorista = agendamentoDAO.ObterMotoristasPesquisa(termo, transportadora)
            Return listaMotorista

        Catch ex As Exception
            Throw New Exception("Não foi possível obter os dados do motorista")
        End Try
    End Function

End Class
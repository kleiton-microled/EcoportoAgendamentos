Imports System.Globalization

Public Class AgendamentoImportacao
    Inherits System.Web.UI.Page

    Dim Importacao As New Importacao
    Dim Conteiner As New ConteinerImportacao
    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo
    Dim Transportadora As New Transportadora
    Dim NotaFiscal As New NotaFiscal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Lista1 As New List(Of String)

            Me.dgPeriodos.DataSource = Lista1
            Me.dgPeriodos.DataBind()

            Me.dgNotas.DataSource = Lista1
            Me.dgNotas.DataBind()

            ConsultarCNH()
            CarregarDadosProtocolo()

            If Not Request.QueryString("conteiner") Is Nothing Then
                If Not Request.QueryString("conteiner").ToString() = String.Empty Then

                    ConsultarConteineres()
                    cbConteineres.SelectedValue = Request.QueryString("conteiner").ToString()
                    cbConteineres.Enabled = False

                    EditarAgendados(Request.QueryString("Conteiner").ToString())

                    If Request.QueryString("lote") IsNot Nothing Then
                        lblLote.Text = Request.QueryString("lote").ToString()
                    End If

                    If Request.QueryString("free_time") IsNot Nothing Then
                        lblFreeTime.Text = Request.QueryString("free_time").ToString()
                    End If

                    If Request.QueryString("doc") IsNot Nothing Then
                        lblTipoDoc.Text = Request.QueryString("doc").ToString()
                    End If

                    If Request.QueryString("patio") IsNot Nothing Then
                        lblPatio.Text = Request.QueryString("patio").ToString()
                    End If

                    If Request.QueryString("vip") IsNot Nothing Then
                        lblVip.Text = Request.QueryString("vip").ToString()
                    End If

                    If Not cbConteineres.Text = String.Empty Then
                        ConsultarPeriodos()
                    End If

                    If Me.cbConteineres.SelectedValue IsNot Nothing Then

                        CarregarFrames()

                    End If

                End If
            End If

            If Not Request.QueryString("id") Is Nothing Then
                Editar(Request.QueryString("id").ToString())
            Else
                Try
                    ConsultarConteineres()
                Catch ex As Exception
                    Editar(Request.QueryString("conteiner").ToString())
                End Try
            End If

        End If

        dgNotas.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgNotas.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

        dgPeriodos.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgPeriodos.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub ConsultarPeriodos()

        Dim PeriodosOBJ As New Importacao

        PeriodosOBJ.pFreeTime = lblFreeTime.Text
        PeriodosOBJ.pDTA = lblTipoDoc.Text
        PeriodosOBJ.pPatio = lblPatio.Text
        PeriodosOBJ.pVIP = lblVip.Text
        PeriodosOBJ.Id_Conteiner = cbConteineres.SelectedValue
        PeriodosOBJ.ID_Periodo = lblCodigoPeriodo.Text

        dgPeriodos.DataSource = Importacao.ConsultarPeriodos(PeriodosOBJ)
        dgPeriodos.DataBind()

        If dgPeriodos.Rows.Count > 0 Then
            For Each Linha As GridViewRow In Me.dgPeriodos.Rows
                If dgPeriodos.DataKeys(Linha.RowIndex)("AUTONUM_GD_RESERVA").ToString() = lblCodigoPeriodo.Text Then
                    Linha.BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")
                End If
            Next
        End If

    End Sub

    Private Sub ConsultarCNH()

        Dim Lista As List(Of String) = Motorista.ConsultarCNHPorTransportadora(Session("SIS_ID").ToString())

        For Each Item In Lista
            cbCNH.Items.Add(New ListItem(Item.ToString(), Item.ToString()))
        Next

        cbCNH.Items.Insert(0, New ListItem("", ""))

    End Sub

    Private Sub CarregarDadosProtocolo()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Private Sub ConsultarConteineres()

        Me.cbConteineres.DataSource = Conteiner.ConsultarConteineres(Session("SIS_ID").ToString(), Session("SIS_EMPRESA").ToString())
        Me.cbConteineres.DataBind()

        cbConteineres.Items.Insert(0, New ListItem("SELECIONE...", ""))

    End Sub

    Private Sub EditarAgendados(ByVal ID As String)

        Dim ImportacaoOBJ As Importacao = Importacao.ConsultarDadosAgendamentoPorConteinerAgendado(ID)

        If Not ImportacaoOBJ Is Nothing Then

            lblID.Text = ImportacaoOBJ.Autonum
            txtNomeMotorista.Text = ImportacaoOBJ.Nome
            cbCNH.Text = ImportacaoOBJ.CNH
            cbTipoDocSaida.SelectedItem.Text = ImportacaoOBJ.Tipo_doc_saida
            txtNrDocSaida.Text = ImportacaoOBJ.Num_doc_saida
            txtSerieDocSaida.Text = ImportacaoOBJ.Serie_doc_saida
            txtEmissaoDocSaida.Text = ImportacaoOBJ.Emissao_doc_saida
            lblProtocolo.Text = ImportacaoOBJ.Num_Protocolo & "/" & ImportacaoOBJ.Ano_Protocolo
            lblCodigoPeriodo.Text = ImportacaoOBJ.ID_Periodo
            lblPeriodo.Text = ImportacaoOBJ.Periodo
            lblPatio.Text = ImportacaoOBJ.pPatio
            lblLote.Text = ImportacaoOBJ.pLote

            If Not cbConteineres.SelectedValue = String.Empty Then
                ConsultarNotasPorConteiner()
            End If

            If Me.cbConteineres.SelectedValue IsNot Nothing Then

                CarregarFrames()

            End If

        End If

    End Sub

    Private Sub Editar(ByVal ID As String)

        Dim ImportacaoOBJ As Importacao = Importacao.ConsultarDadosAgendamento(Session("SIS_ID").ToString(), ID)

        If Not ImportacaoOBJ Is Nothing Then

            lblID.Text = ImportacaoOBJ.Autonum
            txtNomeMotorista.Text = ImportacaoOBJ.Nome
            cbCNH.Text = ImportacaoOBJ.CNH

            ConfigurarPaginaEdicao()
            CarregarDadosPorCNH(ImportacaoOBJ.CNH)

            cbCavalo.SelectedValue = ImportacaoOBJ.Placa_Cavalo
            cbCarreta.SelectedValue = ImportacaoOBJ.Placa_carreta
            txtTara.Text = ImportacaoOBJ.Tara
            txtChassi.Text = ImportacaoOBJ.Chassi
            cbTipoDocSaida.SelectedItem.Text = ImportacaoOBJ.Tipo_doc_saida
            txtNrDocSaida.Text = ImportacaoOBJ.Num_doc_saida
            txtSerieDocSaida.Text = ImportacaoOBJ.Serie_doc_saida
            txtEmissaoDocSaida.Text = ImportacaoOBJ.Emissao_doc_saida
            lblProtocolo.Text = ImportacaoOBJ.Num_Protocolo & "/" & ImportacaoOBJ.Ano_Protocolo
            lblCodigoPeriodo.Text = ImportacaoOBJ.ID_Periodo
            lblPeriodo.Text = ImportacaoOBJ.Periodo
            lblPatio.Text = ImportacaoOBJ.pPatio
            lblVip.Text = ImportacaoOBJ.pVIP
            lblFreeTime.Text = ImportacaoOBJ.pFreeTime
            lblTipoDoc.Text = ImportacaoOBJ.pDTA
            lblLote.Text = ImportacaoOBJ.pLote

            cbConteineres.Items.Clear()
            cbConteineres.Items.Add(New ListItem(ImportacaoOBJ.Id_Conteiner, ImportacaoOBJ.Autonum))

            cbConteineres.Enabled = False
            ConsultarPeriodos()

            If Not cbConteineres.SelectedValue = String.Empty Then
                ConsultarNotasPorConteiner()
            End If

            If Me.cbConteineres.SelectedValue IsNot Nothing Then

                CarregarFrames()

            End If

            Dim liberado = Banco.Conexao.Execute("SELECT NVL(FLAG_LIBERADO,0) FLAG_LIBERADO FROM TB_CNTR_BL WHERE AUTONUM = " & Me.lblID.Text).Fields(0).Value

            If Val(liberado) > 0 Then
                dgNotas.Columns(8).Visible = False
            End If

        End If

    End Sub

    Private Sub ConfigurarPaginaEdicao()

        HabilitarControles()
        txtTara.Enabled = False
        txtChassi.Enabled = False

    End Sub

    Private Sub CarregarDadosPorCNH(Optional ByVal CNH As String = "")

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        Dim PlacaCavalo As String = cbCavalo.Text
        Dim PlacaCarreta As String = cbCarreta.Text

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Session("SIS_ID").ToString()

        Dim MotoristaOBJ As New Motorista

        MotoristaOBJ.CNH = CNH
        MotoristaOBJ.Transportadora = TransportadoraOBJ
        MotoristaOBJ.Nome = Importacao.ConsultarNome(MotoristaOBJ, TransportadoraOBJ)

        If Not Importacao.ConsultarNome(MotoristaOBJ, TransportadoraOBJ) = String.Empty Then
            txtNomeMotorista.Text = Importacao.ConsultarNome(MotoristaOBJ, TransportadoraOBJ)

            CarregarListaCNH(txtNomeMotorista.Text.Trim.ToUpper())
            CarregarListaCavalos(Session("SIS_ID").ToString())
            CarregarListaCarretas(Session("SIS_ID").ToString())
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

        Dim sSql = "SELECT COUNT(1) FROM OPERADOR.TB_MOTORISTAS WHERE FLAG_INATIVO = 1 AND TRIM(CNH) = '" & CNH.Trim() & "'"

        Dim Achou = Banco.Conexao().Execute(sSql).Fields(0).Value

        If Val(Achou) > 0 Then
            Me.cbCNH.SelectedIndex = -1
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Atenção: CNH bloqueada no Terminal"
            Exit Sub
        End If

        TransportadoraOBJ = Nothing
        MotoristaOBJ = Nothing

    End Sub

    Private Sub CarregarDadosPorNome(Optional ByVal CNH As String = "")

        cbCavalo.Items.Clear()
        cbCarreta.Items.Clear()

        Motorista.Nome = txtNomeMotorista.Text.ToUpper()
        Motorista.CNH = cbCNH.Text
        Transportadora.ID = Session("SIS_ID").ToString()

        txtNomeMotorista.Text = ConsultarNome(Motorista, Transportadora)

        If Not txtNomeMotorista.Text = String.Empty Then
            CarregarListaCNH(txtNomeMotorista.Text.Trim.ToUpper())
            CarregarListaCavalos(Session("SIS_ID").ToString())
            CarregarListaCarretas(Session("SIS_ID").ToString())
        End If
        Me.cbCavalo.Enabled = True
        Me.cbCarreta.Enabled = True
        If Not ValidarBDCC(Motorista.ObterCPFMotorista(cbCNH.Text)) Then
            Response.Redirect("ConsultarConteineresImportacao.aspx")
        End If

        Dim sSql = "SELECT COUNT(1) FROM OPERADOR.TB_MOTORISTAS WHERE FLAG_INATIVO = 1 AND TRIM(CNH) = '" & CNH.Trim() & "'"

        Dim Achou = Banco.Conexao().Execute(sSql).Fields(0).Value

        If Val(Achou) > 0 Then
            Me.cbCNH.SelectedIndex = -1
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Atenção: CNH bloqueada no Terminal"
            Exit Sub
        End If

    End Sub

    Private Function ConsultarNome(ByVal MotoristaOBJ As Motorista, ByVal TransportadoraOBJ As Transportadora) As String

        If Not Importacao.ConsultarNome(MotoristaOBJ, TransportadoraOBJ) = String.Empty Then
            Return Importacao.ConsultarNome(MotoristaOBJ, TransportadoraOBJ)
        End If

        Return String.Empty

    End Function

    Private Sub HabilitarControles()

        txtNomeMotorista.Enabled = True
        cbConteineres.Enabled = True
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
        btSalvar.Enabled = True
        btExcluir.Enabled = True
        btVincular.Enabled = True

        dgPeriodos.Enabled = True
        dgNotas.Enabled = True

    End Sub

    Private Sub CarregarListaCavalos(ByVal Transportadora As Integer)

        Me.cbCavalo.Items.Add(New ListItem(String.Empty, String.Empty))

        For Each Item In Importacao.ConsultarPlacasCavalo(Session("SIS_ID").ToString())
            cbCavalo.Items.Add(New ListItem(Item.ToString(), Item.ToString()))
        Next

    End Sub

    Private Sub CarregarListaCarretas(ByVal Transportadora As Integer)

        Me.cbCarreta.Items.Add(New ListItem(String.Empty, String.Empty))

        For Each Item In Importacao.ConsultarPlacasCarreta(Session("SIS_ID").ToString())
            cbCarreta.Items.Add(New ListItem(Item.ToString(), Item.ToString()))
        Next

    End Sub

    Private Sub CarregarListaCNH(ByVal Nome As String)

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Session("SIS_ID").ToString()

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ.Nome = txtNomeMotorista.Text.ToUpper()
        MotoristaOBJ.Transportadora = TransportadoraOBJ

        Dim CNH As String = Importacao.ConsultarCNH(MotoristaOBJ)
        Me.cbCNH.SelectedValue = CNH

    End Sub

    Private Sub CarregarDadosVeiculo(ByVal Cavalo As String, ByVal Carreta As String, ByVal Transportadora As Integer)

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = Transportadora

        Dim VeiculoOBJ As New Veiculo

        VeiculoOBJ.Cavalo = Cavalo
        VeiculoOBJ.Carreta = Carreta
        VeiculoOBJ.Transportadora = TransportadoraOBJ

        If Importacao.ConsultarDadosVeiculo(VeiculoOBJ) IsNot Nothing Then

            Dim NovoVeiculo As New Veiculo
            NovoVeiculo = Importacao.ConsultarDadosVeiculo(VeiculoOBJ)

            txtTara.Text = NovoVeiculo.Tara
            txtChassi.Text = NovoVeiculo.Chassi

        End If

    End Sub

    Protected Sub txtNomeMotorista_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNomeMotorista.TextChanged

        If Not txtNomeMotorista.Text = String.Empty Then
            CarregarDadosPorNome()
        End If


        CarregarFrames()


    End Sub

    Private Function ValidarBDCC(ByVal CPF As String) As Boolean

        Dim WebService As New WebService

        If Config.ValidaBDCC() Then

            Dim Autonomo As Integer = 0

            If chAutonomo.Checked Then
                Autonomo = 1
            End If

            'If WebService.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), Session("SIS_CNPJ").ToString(), Autonomo) Then
            If WebService.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), "?", Autonomo) Then
                If WebService.ValidarBDCC() Then

                    If WebService.TipoBDCC = 1 Or WebService.TipoBDCC = 2 Then
                        'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('" & WebService.DescricaoRetorno & "');</script>", False)
                    End If

                    If WebService.TipoBDCC = 2 Then
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = WebService.DescricaoRetorno
                        Return False
                    End If


                End If
            End If

            Return True

        End If

        Return True

    End Function

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If Validar() Then

            Dim Autonomo As Integer = 0

            If chAutonomo.Checked Then
                Autonomo = 1
            End If

            If Request.QueryString("id") Is Nothing Then
                If Not lblTipoDoc.Text.Contains("DTA") Then
                    If Importacao.VerificarSaldo(lblCodigoPeriodo.Text, cbConteineres.SelectedValue) Then
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = "Saldo insuficiente."
                        Exit Sub
                    End If
                End If
            End If

            Dim TransportadoraOBJ As New Transportadora
            TransportadoraOBJ.ID = Session("SIS_ID").ToString()

            Dim VeiculoOBJ As New Veiculo
            VeiculoOBJ.Cavalo = cbCavalo.Text
            VeiculoOBJ.Carreta = cbCarreta.Text
            VeiculoOBJ.Transportadora = TransportadoraOBJ

            Dim MotoristaOBJ As New Motorista
            MotoristaOBJ.Nome = txtNomeMotorista.Text.Trim
            MotoristaOBJ.CNH = cbCNH.Text.Trim
            MotoristaOBJ.Transportadora = TransportadoraOBJ

            Dim ID_Veiculo As Integer = VeiculoOBJ.ObterID(VeiculoOBJ)

            If ID_Veiculo = 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Veículo não encontrado. As placas Cavalo e Carreta devem pertencer a um veículo cadastrado."
                Exit Sub
            End If

            Dim sSql = "SELECT COUNT(1) FROM OPERADOR.TB_MOTORISTAS WHERE FLAG_INATIVO = 1 AND TRIM(CNH) = '" & MotoristaOBJ.CNH.Trim() & "'"

            Dim Achou = Banco.Conexao().Execute(sSql).Fields(0).Value

            If Val(Achou) > 0 Then
                Me.cbCNH.SelectedIndex = -1
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Atenção: CNH bloqueada no Terminal"
                Exit Sub
            End If

            Dim CodigoMotorista As Integer = Motorista.ObterCodigoMotorista(MotoristaOBJ)

            If CodigoMotorista = 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Motorista não encontrado."
                Exit Sub
            End If

            If Not ValidarBDCC(Motorista.ObterCPFMotorista(cbCNH.Text)) Then
                'Response.Redirect("ConsultarConteineresImportacao.aspx")
                Exit Sub
            End If

            Dim Doct As Integer = 0

            Dim QtdeRequerida As Integer = 0
            Dim QtdeVinculada As Integer = 0
            Dim QtdeAverbacao As Integer = 0

            Dim dsDocs As New DataTable
            Dim ListaDocs As New List(Of String)
            Dim TeveAlteracoesDeDocumentos As Boolean = False


            Doct = Documento.ObterTipoDocumento(Val(lblLote.Text))

            QtdeRequerida = Documento.ObterQuantidadeRequerida(Doct)
            QtdeVinculada = Documento.ObterQuantidadeVinculada(Me.cbConteineres.SelectedValue)
            QtdeAverbacao = Documento.ObterQuantidadeAverbacao(Me.cbConteineres.SelectedValue)

            Dim Bloqueio = Banco.Conexao.Execute("SELECT NVL(FLAG_AGENDAMENTO_DOCUMENTOS,0) FROM TB_PARAMETROS_SISTEMA").Fields(0).Value

            Dim ds As New DataTable

            If (QtdeVinculada + QtdeAverbacao) < QtdeRequerida Then

                Dim dsAux As New DataTable
                dsAux = Documento.DocumentosQueFaltam(Me.cbConteineres.SelectedValue, Doct)

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

            If (Val(Banco.Conexao.Execute("SELECT NVL(FLAG_DECLARA_DOCS_AGENDAMENTO, 0) FLAG_DECLARA_DOCS_AGENDAMENTO FROM TB_CNTR_BL WHERE AUTONUM = " & Me.cbConteineres.SelectedValue).Fields(0).Value)) = 0 Then
                Me.lblMsgErro.Text = "Você deve selecionar o aceite de documentos. <br/> ""Declaro, sob as penas da lei, que os documentos anexados são autênticos e equivalem aos originais que estão em meu poder."""
                Me.pnlMsgErro.Visible = True
                Exit Sub
            End If

            If lblID.Text = String.Empty Then

                If Importacao.AgendarConteiner(lblCodigoPeriodo.Text, Session("SIS_ID").ToString(), CodigoMotorista, ID_Veiculo, cbTipoDocSaida.Text, txtNrDocSaida.Text, txtSerieDocSaida.Text, txtEmissaoDocSaida.Text, cbConteineres.SelectedValue, Session("USRID").ToString(), Autonomo) Then

                    CadastrarNotasFiscais()

                    If Importacao.InsereAgendamentoNaFila(cbConteineres.SelectedValue, lblCodigoPeriodo.Text) Then
                        If Importacao.AlterarDataSaida(cbConteineres.SelectedValue) Then

                            If lblTipoDoc.Text.Contains("DTA") Then

                                Dim dsDTA As New DataTable
                                dsDTA = Documento.DocumentosObrigatoriosDTA(Val(Me.lblID.Text), Val(Me.cbConteineres.SelectedValue))

                                If dsDTA IsNot Nothing Then
                                    If dsDTA.Rows.Count >= 2 Then
                                        Banco.Conexao.Execute("UPDATE TB_CNTR_BL SET IMPRESSO = 0, FLAG_LIBERADO = 1, DT_AGENDAMENTO_LIBERACAO = SYSDATE, USUARIO_LIBERACAO_AGENDAMENTO = " & Val(Session("USRID").ToString()) & " WHERE AUTONUM = " & Val(Me.cbConteineres.SelectedValue))
                                        Response.Redirect("ConsultarAgendamentosImportacao.aspx")
                                    End If
                                End If

                            End If

                            Response.Redirect("Concluido.aspx?id=" & Me.cbConteineres.SelectedValue & "&motorista=" & CodigoMotorista & "&veiculo=" & ID_Veiculo & "")

                            'If QtdeVinculada < QtdeRequerida Then
                            '    Response.Redirect("ConsultarAgendamentosImportacao.aspx")
                            'Else
                            '    Response.Redirect("Concluido.aspx?id=" & Me.cbConteineres.SelectedValue & "&motorista=" & CodigoMotorista & "&veiculo=" & ID_Veiculo & "")
                            'End If

                        End If
                    Else
                        Me.pnlMsgErro.Visible = True
                        Me.lblMsgErro.Text = "Erro durante o Agendamento. Tente Novamente."
                    End If

                End If

            Else

                ' Essa Session fica na página VinculaDocumentos.aspx e indica quando é incluído algum documento em modo edição
                ' Se a session estiver preenchida e a quantidade vinculada for maior ou igual a quantidade de documentso exigida marca a flag TeveAlteracoesDeDocumentos = true
                If Session("DOC_ALTERADO") IsNot Nothing And QtdeVinculada >= QtdeRequerida Then
                    TeveAlteracoesDeDocumentos = True
                End If

                ' obtém a documentação anexada
                dsDocs = Documento.Consultar(Me.cbConteineres.SelectedValue, CodigoMotorista, Veiculo.ID, Session("SIS_ID").ToString())

                ' obtém a quantidade vinculada
                QtdeVinculada = dsDocs.Rows.Count

                ' atualiza o agendamento
                If Importacao.AlterarAgendamento(lblCodigoPeriodo.Text, CodigoMotorista, ID_Veiculo, cbTipoDocSaida.Text, txtNrDocSaida.Text, txtSerieDocSaida.Text, txtEmissaoDocSaida.Text, cbConteineres.SelectedValue, Session("USRID").ToString(), TeveAlteracoesDeDocumentos) Then

                    ' Se tudo estiver certo, atualiza as notas fiscais
                    AlterarNotasFiscais()

                    ' Insere na fila de posicionamento
                    If Importacao.InsereAgendamentoNaFila(cbConteineres.SelectedValue, lblCodigoPeriodo.Text) Then
                        ' altera data de saída
                        If Importacao.AlterarDataSaida(cbConteineres.SelectedValue) Then

                            If lblTipoDoc.Text.Contains("DTA") Then

                                Dim dsDTA As New DataTable
                                dsDTA = Documento.DocumentosObrigatoriosDTA(Val(Me.lblID.Text), Val(Me.cbConteineres.SelectedValue))

                                If dsDTA IsNot Nothing Then
                                    If dsDTA.Rows.Count >= 2 Then
                                        Banco.Conexao.Execute("UPDATE TB_CNTR_BL SET IMPRESSO = 0, FLAG_LIBERADO = 1, DT_AGENDAMENTO_LIBERACAO = SYSDATE, USUARIO_LIBERACAO_AGENDAMENTO = " & Val(Session("USRID").ToString()) & " WHERE AUTONUM = " & Val(Me.cbConteineres.SelectedValue))
                                        Response.Redirect("ConsultarAgendamentosImportacao.aspx")
                                    End If
                                End If

                            End If

                            If TeveAlteracoesDeDocumentos Then
                                Response.Redirect("Concluido.aspx?id=" & Me.cbConteineres.SelectedValue & "&edit=1&motorista=" & CodigoMotorista & "&veiculo=" & ID_Veiculo & "")
                            Else
                                Response.Redirect(ResolveUrl("~/ConsultarAgendamentosImportacao.aspx"), True)
                            End If

                        End If

                    End If
                Else
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "Erro durante o Agendamento. Tente Novamente."
                End If

            End If

        End If

    End Sub

    Private Function AlterarNotasFiscais() As Boolean

        If Importacao.ExcluirNotasFiscaisPorConteiner(cbConteineres.SelectedValue) Then
            CadastrarNotasFiscais()
        End If

        Return True

    End Function

    Private Function CadastrarNotasFiscais() As Boolean

        Dim Nota As String = String.Empty
        Dim Conteiner As String = String.Empty
        Dim Serie As String = String.Empty
        Dim Emissao As String = String.Empty
        Dim Lote As String = String.Empty

        For Each Row As GridViewRow In dgNotas.Rows

            Conteiner = Row.Cells(2).Text
            Nota = Row.Cells(3).Text
            Serie = Row.Cells(5).Text
            Emissao = Row.Cells(6).Text
            Lote = Row.Cells(7).Text

            Importacao.InserirNotaFiscal(Conteiner, Nota, Serie, Emissao, Lote)

        Next

        Return True

    End Function

    Private Function ValidarNF() As Boolean

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If cbConteineres.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Selecione o Contêiner."
            Return False
        End If

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
            If IsDate(txtDataEmissao.Text) Then
                If Date.Compare(txtDataEmissao.Text, Now.Date) > 0 Then
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A Data de Emissão deverá ser menor que a data atual."
                    Return False
                End If
                'If Convert.ToDateTime(txtDataEmissao.Text).Year < Now.Year Then
                If Convert.ToDateTime(txtDataEmissao.Text, New CultureInfo("pt-BR")).Date < Convert.ToDateTime(Now, New CultureInfo("pt-BR")).AddDays(-90).Date Then '90 dias de diferença como tolerância será temporário                    
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "Atenção: A Data de Emissão é muito antiga."
                    Return False
                End If
            End If
        Else
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a Data de Emissão."
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

        Dim ValidadeCNH As String = Motorista.ObterValidadeCNH(Me.cbCNH.Text, Session("SIS_ID").ToString())

        Try

            Dim Validade As String = Convert.ToDateTime(ValidadeCNH).ToString("dd/MM/yyyy")

            If IsDate(Validade) Then
                If Convert.ToDateTime(Validade) < DateTime.Now Then
                    Me.cbCNH.SelectedIndex = -1
                    Me.pnlMsgErro.Visible = True
                    Me.lblMsgErro.Text = "A CNH do motorista está vencida. Escolha um outro motorista."
                End If
            End If

        Catch ex As Exception
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Data de Validade CNH (" & ValidadeCNH & ") é inválida"
        End Try

        If cbCNH.Text = String.Empty Then
            Me.pnlMsgErro.Visible = True
            Me.lblMsgErro.Text = "Informe a CNH do Motorista."
            Return False
        End If

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
                If Date.Compare(txtEmissaoDocSaida.Text, Now.Date) > 0 Then
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

    Protected Sub btVincular_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btVincular.Click

        Me.pnlMsgErro.Visible = False
        Me.lblMsgErro.Text = ""

        If Not cbConteineres.SelectedValue = String.Empty Then

            If Importacao.NFObrigatoria(cbConteineres.SelectedValue) Then

                If ValidarNF() Then

                    For Each Row As GridViewRow In dgNotas.Rows
                        If Row.Cells(0).Text & Row.Cells(1).Text = txtNotaFiscal.Text & cbConteineres.SelectedItem.Text Then
                            Me.pnlMsgErro.Visible = True
                            Me.lblMsgErro.Text = "O Contêiner e Nota informados já foram vinculados."
                            Exit Sub
                        End If
                    Next

                    Dim NotaFiscalOBJ As New NotaFiscal

                    NotaFiscalOBJ.ID = String.Empty

                    If Request.QueryString("id") IsNot Nothing Then
                        NotaFiscalOBJ.ID_Conteiner = cbConteineres.Text
                    Else
                        NotaFiscalOBJ.ID_Conteiner = Conteiner.ObterCodigoConteiner(cbConteineres.SelectedItem.Text)
                    End If

                    NotaFiscalOBJ.Emissao = txtDataEmissao.Text
                    NotaFiscalOBJ.NotaFiscal = txtNotaFiscal.Text
                    NotaFiscalOBJ.Serie = txtSerie.Text
                    NotaFiscalOBJ.Conteiner = cbConteineres.SelectedItem.Text
                    NotaFiscalOBJ.Lote = Conteiner.ObterLoteConteiner(cbConteineres.SelectedValue)

                    ListaNotas.Add(NotaFiscalOBJ)
                    ConsultarListaNotas()

                    If btSalvar.Enabled = False Then
                        btSalvar.Enabled = True
                    End If

                End If
            Else

                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Não é necessária a inclusão da nota fiscal."

                If btSalvar.Enabled = False Then
                    btSalvar.Enabled = True
                End If

            End If

        End If

    End Sub

    Private Sub ConsultarListaNotas()

        dgNotas.DataSource = ListaNotas
        dgNotas.DataBind()

    End Sub

    Private Sub ConsultarNotasPorConteiner()

        ListaNotas = Importacao.ConsultarDadosNotas(cbConteineres.SelectedValue, Session("SIS_ID").ToString())

        dgNotas.DataSource = ListaNotas
        dgNotas.DataBind()

    End Sub

    Private Property ListaNotas() As List(Of NotaFiscal)
        Get
            If ViewState("ListaNotas") Is Nothing Then
                ViewState("ListaNotas") = New List(Of NotaFiscal)
            End If
            Return DirectCast(ViewState("ListaNotas"), List(Of NotaFiscal))
        End Get
        Set(ByVal value As List(Of NotaFiscal))
            ViewState("ListaNotas") = value
        End Set
    End Property

    Protected Sub cbCNH_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCNH.SelectedIndexChanged

        Me.pnlMsgErro.Visible = True
        Me.lblMsgErro.Text = ""

        If Not cbCNH.SelectedItem.Text = String.Empty Then

            CarregarDadosPorCNH(cbCNH.SelectedItem.Text)


            CarregarFrames()

            Dim ValidadeCNH As String = Motorista.ObterValidadeCNH(Me.cbCNH.Text, Session("SIS_ID").ToString())

            Try

                If Not String.IsNullOrEmpty(ValidadeCNH) Then
                    Dim Validade As String = Convert.ToDateTime(ValidadeCNH).ToString("dd/MM/yyyy")

                    If IsDate(Validade) Then
                        If Convert.ToDateTime(Validade) < DateTime.Now Then
                            Me.cbCNH.SelectedIndex = -1
                            Me.pnlMsgErro.Visible = True
                            Me.lblMsgErro.Text = "A CNH do motorista está vencida. Escolha um outro motorista."
                        End If
                    End If
                End If

            Catch ex As Exception
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Data de Validade CNH (" & ValidadeCNH & ") é inválida"
            End Try

        End If

        Me.cbCavalo.Enabled = True
        Me.cbCarreta.Enabled = True

    End Sub

    Protected Sub cbConteineres_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbConteineres.SelectedIndexChanged

        If Not cbConteineres.Text = String.Empty Then

            Dim ImportacaoOBJ As Importacao = Importacao.ObterDadosConteiner(cbConteineres.SelectedItem.Text)

            If ImportacaoOBJ IsNot Nothing Then

                lblVip.Text = ImportacaoOBJ.pVIP
                lblPatio.Text = ImportacaoOBJ.pPatio
                lblTipoDoc.Text = ImportacaoOBJ.pDTA
                lblFreeTime.Text = ImportacaoOBJ.pFreeTime
                lblLote.Text = ImportacaoOBJ.pLote

                ConsultarPeriodos()
                dgPeriodos.Enabled = True

                If Me.cbConteineres.SelectedValue IsNot Nothing Then

                    CarregarFrames()

                End If

            End If

        End If

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

        Dim CodigoVeiculo As Integer = VeiculoOBJ.ObterID(VeiculoOBJ)
        Dim CodigoMotorista As Integer = Motorista.ObterCodigoMotorista(MotoristaOBJ)

        If CodigoVeiculo <> 0 And CodigoMotorista <> 0 Then

            frameDocumentos.Attributes("src") = "VinculaDocumentos.aspx?edit=1&lote=" & Me.lblLote.Text & "&id=" & Val(Me.cbConteineres.SelectedValue) & "&motorista=" & CodigoMotorista & "&veiculo=" & CodigoVeiculo & ""
            frameEmails.Attributes("src") = "EmailTransportadora.aspx?id=" & Val(Session("SIS_ID").ToString())

            frameDocumentos.Visible = True
            frameEmails.Visible = True

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

        If Request.QueryString("id") IsNot Nothing Then
            If Importacao.AgendamentoMesmoPatioPeriodo(TransportadoraOBJ.ID, CodigoVeiculo, Convert.ToDateTime(Periodo_Inicial).ToString("dd/MM/yyyy HH:mm"), Convert.ToDateTime(Periodo_Final).ToString("dd/MM/yyyy HH:mm"), Val(Me.lblPatio.Text), Val(Me.lblID.Text)) > 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Já existe um outro agendamento com esse mesmo veículo/período em outro pátio."
                Exit Sub
            End If
        Else
            If Importacao.AgendamentoMesmoPatioPeriodo(TransportadoraOBJ.ID, CodigoVeiculo, Convert.ToDateTime(Periodo_Inicial).ToString("dd/MM/yyyy HH:mm"), Convert.ToDateTime(Periodo_Final).ToString("dd/MM/yyyy HH:mm"), Val(Me.lblPatio.Text)) > 0 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Já existe um outro agendamento com esse mesmo veículo/período em outro pátio."
                Exit Sub
            End If
        End If

        lblCodigoPeriodo.Text = Autonum

        If Not Importacao.ConteinerComPendenciaRecebimento(lblCodigoPeriodo.Text, cbConteineres.SelectedValue) Then
            If Importacao.ConsultarFormaPagamento(cbConteineres.SelectedValue) = 2 Then
                Me.pnlMsgErro.Visible = True
                Me.lblMsgErro.Text = "Contêiner com Pendência de recebimento neste Periodo. Favor escolher outro período."
                Exit Sub
            End If
        End If

        For Each Linha As GridViewRow In Me.dgPeriodos.Rows
            Linha.BackColor = Drawing.Color.White
        Next

        Me.dgPeriodos.Rows(Index).BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")

        lblPeriodo.Text = Periodo_Inicial & " - " & Periodo_Final

    End Sub

    Protected Sub dgNotas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgNotas.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = Me.dgNotas.DataKeys(Index)("ID").ToString()

        If e.CommandName = "DEL" Then

            ListaNotas.RemoveAt(Index)

            If Not String.IsNullOrEmpty(ID) And ID <> "0 Then" Then
                Importacao.ExcluirNotaFiscal(ID)
            End If

            ConsultarListaNotas()
        End If

    End Sub

    Protected Sub dgNotas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgNotas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão da Nota: " & DataBinder.Eval(e.Row.DataItem, "NOTAFISCAL").ToString() & "?');")
        End If

    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not lblID.Text = String.Empty Then

            If Importacao.ExcluirAgendameto(lblID.Text, cbConteineres.SelectedValue) Then

                For i As Integer = 0 To dgNotas.Rows.Count - 1
                    Importacao.ExcluirNotaFiscal(Integer.Parse(DirectCast(dgNotas.Rows(0).FindControl("lblID"), Label).Text.ToString()))
                Next

                Response.Redirect("ConsultarAgendamentosImportacao.aspx")

            End If
        End If

    End Sub

    Protected Sub cbCavalo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCavalo.SelectedIndexChanged

        cbCarreta.SelectedValue = Veiculo.ObterCarreta(cbCavalo.SelectedItem.Text, Session("SIS_ID").ToString())

        If Not String.IsNullOrEmpty(cbCavalo.Text) And Not String.IsNullOrEmpty(cbCarreta.Text) Then
            CarregarDadosVeiculo(cbCavalo.Text, cbCarreta.Text, Session("SIS_ID").ToString())

            CarregarFrames()
        End If

    End Sub

    Protected Sub dgNotas_SelectedIndexChanged()

    End Sub
End Class
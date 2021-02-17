Imports System.Data.OleDb

Public Class CadastroReservas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
            ConsultarArmador()
            'ConsultarExportador()            
            'ConsultarNVOCC()
            ConsultarPorto()
            ConsultarViagem()
            ConsultarNavio(String.Empty)

            Me.txtEmail.Text = Banco.ExecuteScalar("SELECT EMAIL FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())

        End If

    End Sub

    Private Sub ConsultarNavio(ByVal Autonum_Viagem As String)

        If Autonum_Viagem <> String.Empty Then
            Me.cbNavio.DataSource = Banco.List("SELECT DISTINCT TO_NUMBER(B.AUTONUM_NAV) AUTONUM_NAV,B.DESCRICAO_NAV FROM REDEX.TB_VIAGENS A INNER JOIN REDEX.TB_CAD_NAVIOS B ON A.AUTONUM_NAV = B.AUTONUM_NAV WHERE A.AUTONUM_VIA = " & Autonum_Viagem)
            Me.cbNavio.DataBind()
        End If

        Me.cbNavio.SelectedIndex = 0
        'Me.cbNavio.Items.Insert(0, New ListItem("-- SELECIONE O NAVIO --", "0"))

    End Sub

    <System.Web.Services.WebMethod()> _
    <System.Web.Script.Services.ScriptMethod()> _
    Public Shared Function ListarExportadores(ByVal prefixText As String, ByVal count As Integer) As String()
        Return Banco.Reader("SELECT DISTINCT AUTONUM || ' - ' || RAZAO || ' (' || CGC || ')' RAZAO FROM REDEX.TB_CAD_PARCEIROS WHERE NVL(FLAG_EXPORTADOR,0) = 1 AND FLAG_ATIVO = 1 AND UPPER(RAZAO) LIKE '%" & prefixText.ToUpper() & "%' ORDER BY RAZAO")
    End Function

    <System.Web.Services.WebMethod()> _
    <System.Web.Script.Services.ScriptMethod()> _
    Public Shared Function ListarNVOCC(ByVal prefixText As String, ByVal count As Integer) As String()
        Return Banco.Reader("SELECT DISTINCT AUTONUM || ' - ' || RAZAO || ' (' || CGC || ')' RAZAO FROM REDEX.TB_CAD_PARCEIROS WHERE NVL(FLAG_NVOCC,0) = 1 AND FLAG_ATIVO = 1 AND UPPER(RAZAO) LIKE '%" & prefixText.ToUpper() & "%' ORDER BY RAZAO")
    End Function

    Private Sub ConsultarViagem()
        Me.cbViagem.DataSource = Banco.List("SELECT A.AUTONUM_VIA, TRIM(B.DESCRICAO_NAV)|| '/'|| A.NUM_VIAGEM NUM_VIAGEM FROM REDEX.TB_VIAGENS A INNER JOIN REDEX.TB_CAD_NAVIOS B ON A.AUTONUM_NAV   = B.AUTONUM_NAV WHERE A.NUM_VIAGEM = 'TBN' UNION ALL SELECT A.AUTONUM_VIA, TRIM(B.DESCRICAO_NAV)  || '/' || A.NUM_VIAGEM NUM_VIAGEM FROM REDEX.TB_VIAGENS A INNER JOIN REDEX.TB_CAD_NAVIOS B ON A.AUTONUM_NAV  = B.AUTONUM_NAV WHERE TO_CHAR(DT_DEAD_LINE,'YYYY') >= " & Now.Year)
        Me.cbViagem.DataBind()
        Me.cbViagem.Items.Insert(0, New ListItem("-- SELECIONE --", "0"))
    End Sub

    Private Sub ConsultarPorto()

        Me.cbPortoOrigem.DataSource = Banco.List("SELECT DISTINCT AUTONUM_POR,DESCRICAO_POR FROM REDEX.TB_CAD_PORTOS ORDER BY DESCRICAO_POR")
        Me.cbPortoOrigem.DataBind()

        Me.cbPortoDestino.DataSource = Banco.List("SELECT DISTINCT AUTONUM_POR,DESCRICAO_POR FROM REDEX.TB_CAD_PORTOS ORDER BY DESCRICAO_POR")
        Me.cbPortoDestino.DataBind()

        Me.cbPortoOrigem.Items.Insert(0, New ListItem("-- SELECIONE --", "0"))
        Me.cbPortoDestino.Items.Insert(0, New ListItem("-- SELECIONE --", "0"))

    End Sub

    Private Sub ConsultarArmador()

        Me.cbArmador.DataSource = Banco.List("SELECT DISTINCT AUTONUM_ARM,DESCRICAO FROM REDEX.TB_CAD_ARMADOR ORDER BY DESCRICAO")
        Me.cbArmador.DataBind()

        Me.cbArmador.Items.Insert(0, New ListItem("-- SELECIONE --", "0"))

    End Sub

    Private Sub Consultar()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM, ")
        SQL.Append("    A.RESERVA, ")
        SQL.Append("    A.QUANTIDADE, ")
        SQL.Append("    A.QUANTIDADE_CNTR, ")
        SQL.Append("    A.PESO, ")
        SQL.Append("    A.VOLUMES, ")
        SQL.Append("    A.METRAGEM_CUBICA, ")
        SQL.Append("    F.DESCRICAO AS ARMADOR, ")
        SQL.Append("    E.DESCRICAO_NAV AS NAVIO, ")
        SQL.Append("    C.FANTASIA AS EXPORTADOR, ")
        SQL.Append("    D.FANTASIA AS NVOCC, ")
        SQL.Append("    B.NUM_VIAGEM, ")
        SQL.Append("    G.DESCRICAO_POR AS PORTO_ORIGEM, ")
        SQL.Append("    H.DESCRICAO_POR AS PORTO_DESTINO ")
        SQL.Append("FROM ")
        SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA A ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_VIAGENS B ON A.AUTONUM_VIAGEM = B.AUTONUM_VIA ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_PARCEIROS C ON A.AUTONUM_EXPORTADOR = C.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_PARCEIROS D ON A.AUTONUM_NVOCC = D.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_NAVIOS E ON A.AUTONUM_NAVIO = E.AUTONUM_NAV ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_ARMADOR F ON A.AUTONUM_ARMADOR = F.AUTONUM_ARM ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_PORTOS G ON A.AUTONUM_PORTO_ORIGEM = G.AUTONUM_POR ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    REDEX.TB_CAD_PORTOS H ON A.AUTONUM_PORTO_DESTINO = H.AUTONUM_POR ")
        SQL.Append("WHERE ")
        SQL.Append("    A.AUTONUM_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())

        Me.dgConsulta.DataSource = Banco.List(SQL.ToString())
        Me.dgConsulta.DataBind()

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        If String.IsNullOrEmpty(Me.txtReserva.Text) Or String.IsNullOrWhiteSpace(Me.txtReserva.Text) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Reserva é Obrigatório.');", True)
            Me.txtReserva.Focus()
            Exit Sub
        End If

        'If Me.cbTipo.SelectedIndex = 1 Then

        '    If String.IsNullOrEmpty(Me.txtQuantidade.Text) Or String.IsNullOrWhiteSpace(Me.txtQuantidade.Text) Then
        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade é Obrigatório.');", True)
        '        Me.txtQuantidade.Focus()
        '        Exit Sub
        '    Else
        '        If Not IsNumeric(Me.txtQuantidade.Text) Then                    
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade deve ser um valor Numérico.');", True)
        '            Me.txtQuantidade.Focus()
        '            Exit Sub
        '        End If
        '    End If

        '    If String.IsNullOrEmpty(Me.txtPeso.Text) Or String.IsNullOrWhiteSpace(Me.txtPeso.Text) Then                
        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Peso é Obrigatório.');", True)
        '        Me.txtPeso.Focus()
        '        Exit Sub
        '    Else
        '        If Not IsNumeric(Me.txtPeso.Text) Then
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Peso deve ser um valor Numérico.');", True)
        '            Me.txtPeso.Focus()
        '            Exit Sub
        '        End If
        '    End If

        '    If String.IsNullOrEmpty(Me.txtM3.Text) Or String.IsNullOrWhiteSpace(Me.txtM3.Text) Then
        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Metragem Cúbica (m3) é Obrigatório.');", True)
        '        Me.txtM3.Focus()
        '        Exit Sub
        '    Else
        '        If Not IsNumeric(Me.txtM3.Text) Then                    
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Metragem Cúbica (m3) deve ser um valor Numérico.');", True)
        '            Me.txtM3.Focus()
        '            Exit Sub
        '        End If
        '    End If

        'ElseIf Me.cbTipo.SelectedIndex = 2 Then
        '    If String.IsNullOrEmpty(Me.txtQtdeConteineres.Text) Or String.IsNullOrWhiteSpace(Me.txtQtdeConteineres.Text) Then                
        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade Contêineres é Obrigatória.');", True)
        '        Me.txtQtdeConteineres.Focus()
        '        Exit Sub
        '    Else
        '        If Not IsNumeric(Me.txtQtdeConteineres.Text) Then                    
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade Contêineres deve ser um valor Numérico.');", True)
        '            Me.txtQtdeConteineres.Focus()
        '            Exit Sub
        '        End If
        '    End If
        'End If

        'If Me.cbExportador.SelectedIndex = 0 Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Exportador não informado.');", True)
        '    Me.cbExportador.Focus()
        '    Exit Sub
        'End If

        'If Me.cbNavio.SelectedIndex = 0 Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Navio não informado.');", True)
        '    Me.cbNavio.Focus()
        '    Exit Sub
        'End If

        'If Me.cbViagem.SelectedIndex = 0 Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Viagem não informado.');", True)
        '    Me.cbViagem.Focus()
        '    Exit Sub
        'End If

        If Me.txtEmail.Text = String.Empty Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Não existe nenhum Email cadastrado para a Transportadora " & Session("SIS_RAZAO").ToString() & ". É Necessário informar um email válido para que a Transportadora seja notificada quando a Reserva for liberada/rejeitada.');", True)
            Me.txtEmail.Focus()
            Exit Sub
        Else
            If Not ValidarEmail(Me.txtEmail.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Email informado (" & Me.txtEmail.Text & ") não é um Email válido.');", True)
                Me.txtEmail.Focus()
                Exit Sub
            End If
        End If

        Dim CodigoExportador As String = String.Empty
        Dim CodigoNVOCC As String = String.Empty
        Dim CodigoNavio As String = String.Empty
        Dim CodigoViagem As String = String.Empty

        Dim Exportador() As String = Nothing
        Dim NVOCC() As String = Nothing

        If Me.txtExportador.Text <> String.Empty Then
            If Me.txtExportador.Text.Contains("-") Then
                Exportador = Me.txtExportador.Text.Split("-")
            End If
        End If

        If Me.txtNVOCC.Text <> String.Empty Then
            If Me.txtNVOCC.Text.Contains("-") Then
                NVOCC = Me.txtNVOCC.Text.Split("-")
            End If
        End If

        If Exportador IsNot Nothing Then
            If Exportador.Count > 1 Then
                CodigoExportador = Exportador(0).ToString()
            End If
        End If

        If NVOCC IsNot Nothing Then
            If NVOCC.Count > 1 Then
                CodigoNVOCC = NVOCC(0).ToString()
            End If
        End If

        If Me.cbViagem.SelectedIndex = 0 Or Me.cbViagem.Text = String.Empty Then
            CodigoViagem = Banco.ExecuteScalar("SELECT AUTONUM_VIA FROM REDEX.TB_VIAGENS WHERE NUM_VIAGEM = 'TBN'")
        Else
            CodigoViagem = Me.cbViagem.SelectedValue
        End If

        If Me.cbNavio.Text = String.Empty Then
            CodigoNavio = Banco.ExecuteScalar("SELECT AUTONUM_NAV FROM REDEX.TB_CAD_NAVIOS WHERE DESCRICAO_NAV = 'TBN'")
        Else
            CodigoNavio = Me.cbNavio.SelectedValue
        End If

        Dim SQL As New StringBuilder

        If Me.lblAutonum.Text = String.Empty Then

            'If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA WHERE RESERVA = '" & Me.txtReserva.Text & "'")) > 0 Then                
            '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Reserva " & Me.txtReserva.Text & " já está Cadastrada!');", True)
            '    Exit Sub
            'End If

            SQL.Append("INSERT INTO ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA ")
            SQL.Append("( ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    AUTONUM_TRANSPORTADORA, ")

            If Me.cbTipo.SelectedIndex = 1 Then
                SQL.Append("    QUANTIDADE, ")
                SQL.Append("    PESO, ")
                SQL.Append("    VOLUMES, ")
                SQL.Append("    METRAGEM_CUBICA, ")
                SQL.Append("    QUANTIDADE_CNTR, ")
            ElseIf Me.cbTipo.SelectedIndex = 2 Then
                SQL.Append("    QUANTIDADE_CNTR, ")
                SQL.Append("    QUANTIDADE, ")
                SQL.Append("    PESO, ")
                SQL.Append("    VOLUMES, ")
                SQL.Append("    METRAGEM_CUBICA, ")
            End If

            SQL.Append("    AUTONUM_ARMADOR, ")
            SQL.Append("    AUTONUM_NAVIO, ")
            SQL.Append("    AUTONUM_EXPORTADOR, ")
            SQL.Append("    AUTONUM_NVOCC, ")
            SQL.Append("    AUTONUM_VIAGEM, ")
            SQL.Append("    AUTONUM_PORTO_ORIGEM, ")
            SQL.Append("    AUTONUM_PORTO_DESTINO ")
            SQL.Append("    )VALUES( ")
            SQL.Append("        REDEX.SEQ_AGENDAMENTO_WEB_RESERVAS.NEXTVAL, ")
            SQL.Append("        '" & Me.txtReserva.Text.ToUpper() & "', ")
            SQL.Append("        " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ", ")

            If Me.cbTipo.SelectedIndex = 1 Then
                SQL.Append("        " & Replace(Replace(Nnull(Me.txtQuantidade.Text, 0), ".", ""), ",", "") & ", ")
                SQL.Append("        " & Replace(Replace(Nnull(Me.txtPeso.Text, 0), ".", ""), ",", ".") & ", ")
                SQL.Append("        " & Replace(Replace(Nnull(Me.txtVolumes.Text, 0), ".", ""), ",", "") & ", ")
                SQL.Append("        " & Replace(Replace(Nnull(Me.txtM3.Text, 0), ".", ""), ",", ".") & ", ")
                SQL.Append("        NULL, ")
            ElseIf Me.cbTipo.SelectedIndex = 2 Then
                SQL.Append("        " & Replace(Replace(Me.txtQtdeConteineres.Text, ".", ""), ",", "") & ", ")
                SQL.Append("        NULL, ")
                SQL.Append("        NULL, ")
                SQL.Append("        NULL, ")
                SQL.Append("        NULL, ")
            End If

            SQL.Append("        " & Nnull(Me.cbArmador.SelectedValue, 0) & ", ")
            SQL.Append("        " & Nnull(CodigoNavio, 0) & ", ")
            SQL.Append("        " & Nnull(CodigoExportador, 0) & ", ")
            SQL.Append("        " & Nnull(CodigoNVOCC, 0) & ", ")
            SQL.Append("        " & Nnull(CodigoViagem, 0) & ", ")
            SQL.Append("        " & Nnull(Me.cbPortoOrigem.SelectedValue, 0) & ", ")
            SQL.Append("        " & Nnull(Me.cbPortoDestino.SelectedValue, 0) & " ")
            SQL.Append("    ) ")

            If Banco.BeginTransaction(SQL.ToString()) Then
                Banco.BeginTransaction("UPDATE OPERADOR.TB_CAD_TRANSPORTADORAS SET EMAIL = '" & Me.txtEmail.Text & "' WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva Cadastrada com Sucesso.');", True)
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Cadastrar a Reserva.');", True)
            End If
        Else

            'If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA WHERE RESERVA = '" & Me.txtReserva.Text & "' AND AUTONUM <> " & Me.lblAutonum.Text)) > 0 Then                
            '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Reserva " & Me.txtReserva.Text & " já está Cadastrada!');", True)
            '    Exit Sub
            'End If

            SQL.Append("UPDATE ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA ")
            SQL.Append("SET ")
            SQL.Append("    RESERVA = '" & Me.txtReserva.Text.ToUpper() & "', ")

            If Me.cbTipo.SelectedIndex = 1 Then
                SQL.Append("    QUANTIDADE = " & Replace(Replace(Nnull(Me.txtQuantidade.Text, 0), ".", ""), ",", "") & ", ")
                SQL.Append("    PESO = " & Replace(Replace(Nnull(Me.txtPeso.Text, 0), ".", ""), ",", ".") & ", ")
                SQL.Append("    VOLUMES = " & Replace(Replace(Nnull(Me.txtVolumes.Text, 0), ".", ""), ",", "") & ", ")
                SQL.Append("    METRAGEM_CUBICA = " & Replace(Replace(Nnull(Me.txtM3.Text, 0), ".", ""), ",", ".") & ", ")
                SQL.Append("    QUANTIDADE_CNTR = NULL, ")
            ElseIf Me.cbTipo.SelectedIndex = 2 Then
                SQL.Append("    QUANTIDADE_CNTR = " & Replace(Replace(Me.txtQtdeConteineres.Text, ".", ""), ",", "") & ", ")
                SQL.Append("    QUANTIDADE = NULL, ")
                SQL.Append("    PESO = NULL, ")
                SQL.Append("    VOLUMES = NULL, ")
                SQL.Append("    METRAGEM_CUBICA = NULL, ")
            End If
            
            SQL.Append("    AUTONUM_ARMADOR = " & Nnull(Me.cbArmador.SelectedValue, 0) & ", ")
            SQL.Append("    AUTONUM_NAVIO = " & Nnull(CodigoNavio, 0) & ", ")
            SQL.Append("    AUTONUM_EXPORTADOR = " & Nnull(CodigoExportador, 0) & ", ")
            SQL.Append("    AUTONUM_NVOCC = " & Nnull(CodigoNVOCC, 0) & ", ")
            SQL.Append("    AUTONUM_VIAGEM = " & Nnull(CodigoViagem, 0) & ", ")
            SQL.Append("    AUTONUM_PORTO_ORIGEM = " & Nnull(Me.cbPortoOrigem.SelectedValue, 0) & ", ")
            SQL.Append("    AUTONUM_PORTO_DESTINO = " & Nnull(Me.cbPortoDestino.SelectedValue, 0) & " ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = " & Me.lblAutonum.Text)

            If Banco.BeginTransaction(SQL.ToString()) Then
                Banco.BeginTransaction("UPDATE OPERADOR.TB_CAD_TRANSPORTADORAS SET EMAIL = '" & Me.txtEmail.Text & "' WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva Alterada com Sucesso.');", True)
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Alterar a Reserva.');", True)
            End If
        End If

        Limpar()
        Consultar()

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = Me.dgConsulta.DataKeys(Index)("AUTONUM").ToString()

        If e.CommandName = "SELECIONAR" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then

                Dim SQL As New StringBuilder

                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.RESERVA, ")
                SQL.Append("    A.QUANTIDADE, ")
                SQL.Append("    A.PESO, ")
                SQL.Append("    A.VOLUMES, ")
                SQL.Append("    A.METRAGEM_CUBICA, ")
                SQL.Append("    A.AUTONUM_ARMADOR, ")
                SQL.Append("    A.AUTONUM_NAVIO, ")
                SQL.Append("    A.AUTONUM_EXPORTADOR, ")
                SQL.Append("    A.AUTONUM_NVOCC, ")
                SQL.Append("    A.AUTONUM_VIAGEM, ")
                SQL.Append("    A.AUTONUM_PORTO_ORIGEM, ")
                SQL.Append("    A.AUTONUM_PORTO_DESTINO, ")
                SQL.Append("    F.DESCRICAO AS ARMADOR, ")
                SQL.Append("    E.DESCRICAO_NAV AS NAVIO, ")
                SQL.Append("    C.RAZAO AS EXPORTADOR, ")
                SQL.Append("    D.RAZAO AS NVOCC, ")
                SQL.Append("    B.NUM_VIAGEM, ")
                SQL.Append("    G.DESCRICAO_POR AS PORTO_ORIGEM, ")
                SQL.Append("    H.DESCRICAO_POR AS PORTO_DESTINO ")
                SQL.Append("FROM ")
                SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA A ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_VIAGENS B ON A.AUTONUM_VIAGEM = B.AUTONUM_VIA ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_PARCEIROS C ON A.AUTONUM_EXPORTADOR = C.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_PARCEIROS D ON A.AUTONUM_NVOCC = D.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_NAVIOS E ON A.AUTONUM_NAVIO = E.AUTONUM_NAV ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_ARMADOR F ON A.AUTONUM_ARMADOR = F.AUTONUM_ARM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_PORTOS G ON A.AUTONUM_PORTO_ORIGEM = G.AUTONUM_POR ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    REDEX.TB_CAD_PORTOS H ON A.AUTONUM_PORTO_DESTINO = H.AUTONUM_POR ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM = " & ID)

                Dim Ds As New DataTable
                Ds = Banco.List(SQL.ToString())

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then

                        Me.lblAutonum.Text = Ds.Rows(0)("AUTONUM").ToString()
                        Me.txtM3.Text = Ds.Rows(0)("METRAGEM_CUBICA").ToString()
                        Me.txtPeso.Text = Ds.Rows(0)("PESO").ToString()
                        Me.txtQuantidade.Text = Ds.Rows(0)("QUANTIDADE").ToString()
                        Me.txtReserva.Text = Ds.Rows(0)("RESERVA").ToString()
                        Me.txtVolumes.Text = Ds.Rows(0)("VOLUMES").ToString()
                        Me.cbArmador.SelectedValue = Ds.Rows(0)("AUTONUM_ARMADOR").ToString()                        
                        Me.cbPortoDestino.SelectedValue = Ds.Rows(0)("AUTONUM_PORTO_DESTINO").ToString()
                        Me.cbPortoOrigem.SelectedValue = Ds.Rows(0)("AUTONUM_PORTO_ORIGEM").ToString()
                        Me.cbViagem.SelectedValue = Ds.Rows(0)("AUTONUM_VIAGEM").ToString()
                        ConsultarNavio(Me.cbViagem.SelectedValue)
                        Me.cbNavio.SelectedValue = Ds.Rows(0)("AUTONUM_NAVIO").ToString()

                        Me.txtExportador.Text = Banco.ExecuteScalar("SELECT DISTINCT AUTONUM || ' - ' || RAZAO || ' (' || CGC || ')' RAZAO FROM REDEX.TB_CAD_PARCEIROS WHERE AUTONUM = " & Ds.Rows(0)("AUTONUM_EXPORTADOR").ToString())
                        Me.txtNVOCC.Text = Banco.ExecuteScalar("SELECT DISTINCT AUTONUM || ' - ' || RAZAO || ' (' || CGC || ')' RAZAO FROM REDEX.TB_CAD_PARCEIROS WHERE AUTONUM = " & Ds.Rows(0)("AUTONUM_NVOCC").ToString())                        

                    End If
                End If
            End If

        End If

    End Sub

    Protected Sub btnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcluir.Click

        If Not Me.lblAutonum.Text = String.Empty Then
            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 And
                Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 And
                Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_GD_CONTEINER WHERE UPPER(REFERENCE) = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 And
                Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 Then
                If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CAD_RESERVA WHERE AUTONUM = " & Me.lblAutonum.Text) Then
                    Limpar()
                    Consultar()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva Excluída com Sucesso.');", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Excluir a Reserva.');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Existem Agendamentos Vinculados a esta Reserva. Operação não realizada.');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhuma Reserva Selecionada.');", True)
        End If

    End Sub

    Private Sub Limpar()

        Me.lblAutonum.Text = String.Empty
        Me.txtM3.Text = String.Empty
        Me.txtPeso.Text = String.Empty
        Me.txtQuantidade.Text = String.Empty
        Me.txtReserva.Text = String.Empty
        Me.txtVolumes.Text = String.Empty
        Me.cbArmador.SelectedIndex = -1
        'Me.cbExportador.SelectedIndex = -1
        Me.cbNavio.SelectedIndex = -1
        'Me.cbNVOCC.SelectedIndex = -1
        Me.cbPortoDestino.SelectedIndex = -1
        Me.cbPortoOrigem.SelectedIndex = -1
        Me.cbViagem.SelectedIndex = -1

    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSair.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub cbViagem_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbViagem.SelectedIndexChanged

        If Me.cbViagem.Items.Count > 0 Then
            If Me.cbViagem.SelectedValue IsNot Nothing Then
                ConsultarNavio(Me.cbViagem.SelectedValue)
            End If
        End If

    End Sub

    Protected Sub cTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTipo.SelectedIndexChanged

        If Me.cbTipo.SelectedIndex = 1 Then
            Me.pnCargaSolta.Visible = True
            Me.pnConteiner.Visible = False
        Else
            Me.pnConteiner.Visible = True
            Me.pnCargaSolta.Visible = False
        End If

    End Sub

    Public Function ValidarEmail(ByVal email As String) As Boolean

        Dim validEmail As Boolean = False
        Dim indexArr As Integer = 0

        If email <> String.Empty Then

            indexArr = email.IndexOf("@"c)

            If indexArr > 0 Then
                Dim indexDot As Integer = email.IndexOf("."c, indexArr)
                If indexDot - 1 > indexArr Then
                    If indexDot + 1 < email.Length Then
                        Dim indexDot2 As String = email.Substring(indexDot + 1, 1)
                        If indexDot2 <> "." Then
                            validEmail = True
                        End If
                    End If
                End If
            End If

        End If

        Return validEmail

    End Function

End Class
Public Class RetiradaTRA
    Inherits System.Web.UI.Page

    Dim Exp As New Exportacao
    Dim Recinto As New Recinto
    Dim Viagem As New Viagem
    Dim Retirar As New AgRetiradaTRA
    Dim Motorista As New Motorista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("SIS_COD_EMPRESA").ToString <> "1" Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado a este pátio');location.href='Principal.aspx';", True)
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            Dim I As Integer

            txtTransportadora.Text = Session("SIS_RAZAO").ToString()

            CarregarVeiculos()
            CarregarMotoristas()
            CarregarViagens()

            dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
            dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())


            If Not Request.QueryString("id") Is Nothing Then
                If Integer.TryParse(Request.QueryString("id").ToString(), I) Then
                    If Retirar.VerificarAgendTransp(Request.QueryString("id").ToString(), Session("SIS_ID").ToString()) And Session("SIS_TIPO").ToString() = "T" Then
                        If Request.QueryString("action").ToString = "edit" Then
                            If Retirar.ObterStatusImpressao(Convert.ToInt16(Request.QueryString("id").ToString())) = 1 Then
                                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não há como Editar Agendamento, pois este já foi Impresso.');location.href='ConsultarTRA.aspx';</script>", False)
                                Exit Sub
                            End If
                            Editar()
                        End If
                    Else
                        'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Id do agendamento não pertence à transportadora logada!');location.href='ConsultarTRA.aspx';</script>", False)
                    End If
                Else
                    Response.Redirect("ConsultarTRA.aspx")
                End If

            End If

        End If
    End Sub

    Private Sub CarregarVeiculos()
        Me.cbVeiculos.DataSource = Exp.ConsultarVeiculos(Session("SIS_ID").ToString()) 'Usa Exportacao porque a estrutura deste combo é = a da pág aspx de exportação
        Me.cbVeiculos.DataBind()
        Me.cbVeiculos.Items.Insert(0, New ListItem("SELECIONE...", 0))
    End Sub

    Private Sub CarregarMotoristas()
        Me.cbMotoristas.DataSource = Exp.ConsultarMotoristas(Session("SIS_ID").ToString()) 'Usa Exportacao porque a estrutura deste combo é = a da pág aspx de exportação
        Me.cbMotoristas.DataBind()
        Me.cbMotoristas.Items.Insert(0, New ListItem("SELECIONE...", 0))
    End Sub

    Private Sub CarregarViagens()
        cbViagem.DataTextField = "NAVIO_VIAGEM"
        cbViagem.DataValueField = "AUTONUMVIAGEM"

        Me.cbViagem.DataSource = Viagem.CarregarViagens(Session("SIS_ID").ToString())
        Me.cbViagem.DataBind()
        Me.cbViagem.Items.Insert(0, New ListItem("SELECIONE...", 0))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CodPeriodo">Opcional para quando há período, quando se edita</param>
    ''' <remarks></remarks>
    Private Sub CarregarPeriodos(Optional ByVal CodPeriodo As String = "0")
        Me.dgConsulta.DataSource = Retirar.ConsultarPeriodos(cbViagem.SelectedValue, Session("SIS_COD_EMPRESA").ToString(), CodPeriodo)
        Me.dgConsulta.DataBind()
    End Sub

    Protected Sub btRetornar_Click(sender As Object, e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarTRA.aspx")
    End Sub

    Public Sub Editar()
        Retirar = Retirar.ConsultarDadosAgendamento(Request.QueryString("id").ToString())

        cbVeiculos.SelectedValue = Retirar.Veiculo.ID.ToString()
        cbMotoristas.SelectedValue = Retirar.Motorista.ID.ToString()
        cbViagem.SelectedValue = Retirar.Viagem.Autonumviagem.ToString()

        Session("COD_PERIODO_RET") = Retirar.IdPeriodo
        CarregarPeriodos(Session("COD_PERIODO_RET").ToString())
        lblPeriodo.Text = Retirar.CarregarDatasPeriodo(Retirar.IdPeriodo)
        pnlPeriodo.Visible = True
        MarcarPeriodoEditado()

        Session("COD_AG_RETIRADA") = Request.QueryString("id").ToString()

        btAgendar.Enabled = True
    End Sub

    Public Sub MarcarPeriodoEditado()
        If dgConsulta.Rows.Count <> 0 Then
            For index As Integer = 0 To dgConsulta.Rows.Count - 1

                If Session("COD_PERIODO_RET").ToString() = dgConsulta.DataKeys(index)("AUTONUM_GD_RESERVA").ToString() Then
                    dgConsulta.Rows(index).BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")
                    index = dgConsulta.Rows.Count
                End If
            Next
        End If
    End Sub


    Public Function Validar() As Boolean
        If cbMotoristas.SelectedValue = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Motorista');</script>", False)
            Return False
        End If

        If cbVeiculos.SelectedValue = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Veículo');</script>", False)
            Return False
        End If

        If cbViagem.SelectedValue = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Viagem');</script>", False)
            Return False
        End If

        If lblPeriodo.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o período');</script>", False)
            Return False
        End If

        Return True

    End Function

    Private Function ValidarBDCC(ByVal CPF As String, ByVal ExibeMensBDCC As Boolean) As Boolean

        Dim WebServiceOBJ As New WebService

        If Config.ValidaBDCC() Then

            Dim Autonomo As Integer = 0

            If chAutonomo.Checked Then
                Autonomo = 1
            End If

            If WebServiceOBJ.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), Session("SIS_CNPJ").ToString(), Autonomo) Then
                If WebServiceOBJ.ValidarBDCC() Then

                    'lblMsgOK.Text = String.Empty
                    'lblMsgErro.Text = String.Empty

                    If WebServiceOBJ.TipoBDCC = 1 Then
                        'PanelMsg.Visible = True
                        If ExibeMensBDCC Then
                            'lblMsgOK.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('BDCC: " & WebServiceOBJ.DescricaoRetorno & "');</script>", False)
                        End If
                        'lblMsgErro.Text = String.Empty
                    End If

                    If WebServiceOBJ.TipoBDCC = 2 Then
                        'PanelMsg.Visible = True
                        'lblMsgOK.Text = String.Empty
                        If ExibeMensBDCC Then
                            'lblMsgErro.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('BDCC: " & WebServiceOBJ.DescricaoRetorno & "');</script>", False)
                        End If
                        Return False
                    End If

                    Return True

                End If
            End If

            Return True

        End If

        Return True

    End Function

    Public Sub LimparRelPeriodos()
        dgConsulta.DataSource = Nothing
        'dgConsulta.HeaderRow.Visible = True
        dgConsulta.DataBind()
        lblPeriodo.Text = ""
    End Sub

    Protected Sub btAgendar_Click(sender As Object, e As EventArgs) Handles btAgendar.Click
        Dim LinAfet As Integer

        If Validar() Then

            If Not cbVeiculos.Text = String.Empty Then

                Dim CPF As String = Motorista.ObterCPFMotorista(cbVeiculos.SelectedItem.Text.Split("-")(1).Trim)

                If Not ValidarBDCC(CPF, False) Then
                    Exit Sub
                End If
            End If

            If Not Request.QueryString("action") = "edit" Then
                LinAfet = Retirar.Agendar(Session("SIS_ID").ToString(), cbMotoristas.SelectedValue, cbVeiculos.SelectedValue, cbViagem.SelectedValue, Session("COD_PERIODO_RET").ToString())
                If LinAfet > 0 Then
                    btAgendar.Enabled = False 'Previne inserir agendamento após ter clicado no botão de Agendar
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Retirada agendada com sucesso!');location.href='ConsultarTRA.aspx';</script>", False)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Houve um erro ao tentar agendar retirada!')</script>", False)
                End If
            Else '<>new
                LinAfet = Retirar.AlterarAgendamento(Session("COD_AG_RETIRADA").ToString(), Session("SIS_ID").ToString(), cbMotoristas.SelectedValue, cbVeiculos.SelectedValue, cbViagem.SelectedValue, Session("COD_PERIODO_RET").ToString())
                If LinAfet >= 0 Then
                    If LinAfet > 0 Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Retirada alterada com sucesso!');location.href='ConsultarTRA.aspx';</script>", False)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Houve um erro ao tentar agendar retirada! Pode ser que este agendamento foi excluído!')</script>", False)
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Houve um erro ao tentar agendar retirada!')</script>", False)
                End If
            End If

        End If

    End Sub

    Protected Sub cbViagem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbViagem.SelectedIndexChanged
        If Not cbViagem.SelectedValue = 0 Then
            If Request.QueryString("action") = "edit" Then
                CarregarPeriodos(Session("COD_PERIODO_RET").ToString())
            Else 'inserção
                CarregarPeriodos()
            End If
            btAgendar.Enabled = True
        Else
            LimparRelPeriodos()
        End If
    End Sub

    Protected Sub dgConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = dgConsulta.DataKeys(Index)("AUTONUM_GD_RESERVA").ToString()
        Dim Periodo As String = dgConsulta.DataKeys(Index)("PERIODO_INICIAL").ToString() & " - " & dgConsulta.DataKeys(Index)("PERIODO_FINAL").ToString()
        Dim Saldo As String = dgConsulta.DataKeys(Index)("SALDO").ToString()

        If Not ID = String.Empty Or Not ID = 0 Then
            If e.CommandName = "CHECK" Then

                For Each Linha As GridViewRow In Me.dgConsulta.Rows
                    Linha.BackColor = Drawing.Color.White
                Next

                Session("COD_PERIODO_RET") = ID
                lblPeriodo.Text = "Período Selecionado: " & Periodo
                pnlPeriodo.Visible = True

                Me.dgConsulta.Rows(Index).BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")

            End If
        End If
    End Sub
End Class
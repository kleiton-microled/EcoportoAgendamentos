Public Class AlterarVincTransp
    Inherits System.Web.UI.Page

    Dim Recinto As New Recinto
    Dim Transportadora As New Transportadora

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("SIS_COD_EMPRESA").ToString <> "1" Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado a este pátio');location.href='Principal.aspx';", True)
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            If Request.QueryString("Id") IsNot Nothing Then
                If Session("SIS_TIPO").ToString() = "E" Or _
                    (Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) AndAlso Recinto.VerificarVincRec(Session("SIS_ID_REC").ToString(), Request.QueryString("id").ToString())) Then 'Fator de segurança
                    'Session("SIS_TIPO").ToString() <> "T" And _
                    '(Session("SIS_TIPO").ToString() = "E" OrElse Recinto.VerificarVincRec(Session("SIS_ID").ToString(), Request.QueryString("id").ToString())) Then 'Fator de segurança
                    CarregarTransportadoras()
                    CarregarRecintos()
                    CarregarTranspRec()
                    If Session("SIS_TIPO").ToString() = "E" Then
                        cbRecintos.SelectedValue = Recinto.ObterCodRecVinc(Request.QueryString("Id").ToString())
                        cbRecintos.Enabled = True
                    Else 'Recinto
                        cbRecintos.SelectedValue = Session("SIS_ID_REC").ToString()
                        cbRecintos.Enabled = False
                        'ElseIf Session("SIS_TIPO").ToString() = "E" Then
                        '   cbRecintos.SelectedValue = Recinto.ObterCodRecVinc(Request.QueryString("Id").ToString())
                        '  cbRecintos.Enabled = True
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('id da Vinculação não pertence à transportadora logada');location.href='ConsultarRecTransp.aspx';</script>", False)
                End If
        End If
        End If
    End Sub

    Private Sub CarregarTransportadoras()
        cbTransportadoras.DataTextField = "RAZAO"
        cbTransportadoras.DataValueField = "AUTONUM"

        Me.cbTransportadoras.DataSource = Transportadora.CarregarTransportadoras()
        Me.cbTransportadoras.DataBind()
        cbTransportadoras.Items.Insert(0, New ListItem("SELECIONE...", "0"))
    End Sub

    Private Sub CarregarRecintos()
        cbRecintos.DataTextField = "DESCR"
        cbRecintos.DataValueField = "CODE"

        Me.cbRecintos.DataSource = Recinto.CarregarRecintos()
        Me.cbRecintos.DataBind()
        cbRecintos.Items.Insert(0, New ListItem("SELECIONE...", "0"))
    End Sub

    Private Sub CarregarTranspRec()
        Dim TranspRec As New Recinto

        TranspRec = Recinto.ConsultarDadosRecTransp(Request.QueryString("Id").ToString())

        If TranspRec.Autonum <> -1 Then
            If TranspRec.AutonumAtivo = 1 Then
                chkAtivo.Checked = True
            Else
                chkAtivo.Checked = False
            End If
            cbTransportadoras.SelectedValue = TranspRec.IdTransp.ToString()
        Else
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Erro ao carregar registro!\nPode ser que este tenha sido excluído!');location.href='ConsultarRecTransp.aspx';</script>", False)
        End If

    End Sub

    Private Function ValidarVinculacao() As Boolean
        If Recinto.ExisteVinculacao(cbRecintos.SelectedValue, cbTransportadoras.SelectedValue, Request.QueryString("Id")) Then
            pnlMensg.Visible = True
            lblMsgErro.Text = "Esta vinculação entre recinto e transportadora já existe"
            lblMsgOK.Text = String.Empty
            Return False
        End If

        Return True
    End Function

    Protected Sub btRetornar_Click(sender As Object, e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarRecTransp.aspx")
    End Sub

    Protected Sub btEditar_Click(sender As Object, e As EventArgs) Handles btEditar.Click
        Dim Ativo As Integer
        Dim Retorno As Integer

        If ValidarVinculacao() Then
            If chkAtivo.Checked Then
                Ativo = 1
            Else
                Ativo = 0
            End If
            Retorno = Recinto.AlterarRecintoTransp(Request.QueryString("Id").ToString(), cbRecintos.SelectedValue, cbTransportadoras.SelectedValue, Ativo)
            If Retorno > 0 Then
                pnlMensg.Visible = True
                lblMsgOK.Text = "Vinculação alterada com sucesso!"
                lblMsgErro.Text = String.Empty
            Else
                pnlMensg.Visible = True
                lblMsgErro.Text = "Erro ao alterar vinculação!"
                lblMsgOK.Text = String.Empty
            End If
        End If
    End Sub
End Class
Public Class VincularTransportadora
    Inherits System.Web.UI.Page

    Dim Transportadora As New Transportadora
    Dim Recinto As New Recinto

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("SIS_COD_EMPRESA").ToString <> "1" Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado a este pátio');location.href='Principal.aspx';", True)
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            CarregarTransportadoras()
            CarregarRecintos()
            If Session("SIS_TIPO").ToString() = "E" Then
                cbRecintos.Enabled = True
            ElseIf Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) Then
                cbRecintos.SelectedValue = Session("SIS_ID_REC").ToString()
                cbRecintos.Enabled = False
            Else 'não pode acessar como recinto
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado à empresas que não são recintos');location.href='ConsultarRecTransp.aspx';", True)
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

    Private Function ValidarVinculacao() As Boolean
        If Recinto.ExisteVinculacao(cbRecintos.SelectedValue, cbTransportadoras.SelectedValue) Then
            pnlMensg.Visible = True
            lblMsgErro.Text = "Esta vinculação entre recinto e transportadora já existe"
            lblMsgOK.Text = String.Empty
            Return False
        End If

        Return True

    End Function

    Private Sub CarregarRecintos()
        cbRecintos.DataTextField = "DESCR"
        cbRecintos.DataValueField = "CODE"

        Me.cbRecintos.DataSource = Recinto.CarregarRecintos()
        Me.cbRecintos.DataBind()
        cbRecintos.Items.Insert(0, New ListItem("SELECIONE...", "0"))
    End Sub

    Protected Sub btRetornar_Click(sender As Object, e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarRecTransp.aspx")
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs) Handles btSalvar.Click
        Dim Retorno As Integer

        If ValidarVinculacao() Then
            Retorno = Recinto.CadastrarTranspRecinto(cbRecintos.SelectedValue, cbTransportadoras.SelectedValue)
            If Retorno > 0 Then
                pnlMensg.Visible = True
                lblMsgOK.Text = "Vinculação inserida com sucesso!"
                lblMsgErro.Text = String.Empty
            Else
                pnlMensg.Visible = True
                lblMsgErro.Text = "Erro ao tentar inserir vinculação"
                lblMsgOK.Text = String.Empty
            End If
        End If
    End Sub
End Class
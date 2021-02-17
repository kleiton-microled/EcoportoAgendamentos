Public Class ConsultarUsuarios
    Inherits System.Web.UI.Page

    Dim UsuarioBLL As New Usuario
    Dim UsuarioDTO As New Usuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

    End Sub

    Private Sub Consultar()

        Me.DgUsuarios.DataSource = UsuarioBLL.Consultar()
        Me.DgUsuarios.DataBind()

    End Sub

    Protected Sub DgMotoristas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgUsuarios.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = DgUsuarios.DataKeys(Index)("AUTONUM_LOGIN_GD").ToString()

        If Not ID = String.Empty Or Not ID = 0 Then
            Select Case e.CommandName
                Case "EDIT"
                    Response.Redirect(String.Format("AlterarUsuarios.aspx?ID={0}", ID))
                Case "DEL"
                    UsuarioDTO.Codigo = ID
                    UsuarioBLL.ExcluirUsuario(UsuarioDTO)
                    Consultar()
            End Select
        End If

    End Sub
End Class
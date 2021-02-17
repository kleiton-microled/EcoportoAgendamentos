Public Class Index
    Inherits System.Web.UI.Page

    Dim Ds As New DataTable
    Dim CNPJ As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.QueryString("id") IsNot Nothing Then

            CNPJ = Banco.ExecuteScalar("SELECT TIACNPJ FROM INTERNET.TB_INT_ACESSO WHERE TIAID = " & Request.QueryString("id").ToString())

            If Not String.IsNullOrEmpty(CNPJ) Then

                Ds = Banco.List("SELECT AUTONUM,RAZAO,CGC FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE REPLACE(REPLACE(REPLACE(CGC,'.',''),'/',''),'-','') = '" & Replace(Replace(Replace(CNPJ, ".", ""), "/", ""), "-", "") & "'")

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then

                        Session("SIS_AUTONUM_TRANSPORTADORA") = Convert.ToInt32(Ds.Rows(0)("AUTONUM").ToString())
                        Session("SIS_RAZAO") = Ds.Rows(0)("RAZAO").ToString()
                        Session("SIS_CNPJ") = Ds.Rows(0)("CGC").ToString()

                        If Nnull(Session("SIS_AUTONUM_TRANSPORTADORA").ToString(), 0) <> 0 Then
                            'Banco.BeginTransaction("DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID = " & Request.QueryString("id").ToString())
                            Session("LOGADO") = True
                            Response.Redirect("Default.aspx")
                        Else
                            Response.Redirect("http://op.ecoportosantos.com.br/icc/")
                        End If

                    End If
                End If

            End If

        Else
            'Response.Redirect("http://op.ecoportosantos.com.br/icc/")
        End If

    End Sub

End Class
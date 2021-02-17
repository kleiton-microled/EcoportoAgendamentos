Public Class Config
    Public Shared Function ValidaBDCC() As Boolean
        Return Convert.ToInt32(ConfigurationManager.AppSettings("ValidaBDCC").ToString()) > 0
    End Function
End Class

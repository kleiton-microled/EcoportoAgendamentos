Imports System.Web.Security
Imports System.Net.Mail
Imports System.Data.OleDb
Public Class Usuario

    Private _Codigo As Integer
    Private _Nome As String
    Private _CNPJ As String
    Private _Tipo As String
    Private _Admin As Boolean
    Private _Ativo As Boolean
    Private _Email As String
    Private _USRID As String
    Private _Empresa As String
    Private _Transportadora As Transportadora
    Private _FlagTransportador As Boolean
    'Private _FlagRecinto As Boolean
    'Private _CodRecinto As Integer

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property CNPJ() As String
        Get
            Return _CNPJ
        End Get
        Set(ByVal value As String)
            _CNPJ = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value

            If value = "E" Then
                _FlagTransportador = True
                '_FlagRecinto = True
            Else
                If value.Contains("T") Then
                    _FlagTransportador = True
                End If
                'If value.Contains("R") Then
                '    _FlagRecinto = True
                'End If
            End If

        End Set
    End Property

    Public Property Ativo() As Boolean
        Get
            Return _Ativo
        End Get
        Set(ByVal value As Boolean)
            _Ativo = value
        End Set
    End Property

    Public Property Admin() As Boolean
        Get
            Return _Admin
        End Get
        Set(ByVal value As Boolean)
            _Admin = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property USRID() As String
        Get
            Return _USRID
        End Get
        Set(ByVal value As String)
            _USRID = value
        End Set
    End Property

    Public Property Empresa() As String
        Get
            Return _Empresa
        End Get
        Set(ByVal value As String)
            _Empresa = value
        End Set
    End Property

    Public Property Transportadora() As Transportadora
        Get
            Return _Transportadora
        End Get
        Set(ByVal value As Transportadora)
            _Transportadora = value
        End Set
    End Property

    Public Property FlagTransportador() As Boolean
        Get
            Return _FlagTransportador
        End Get
        Set(value As Boolean)
            _FlagTransportador = value
        End Set
    End Property

    'Public Property FlagRecinto() As Boolean
    '    Get
    '        Return _FlagRecinto
    '    End Get
    '    Set(value As Boolean)
    '        _FlagRecinto = value
    '    End Set
    'End Property

    'Public Property CodRecinto() As Integer
    '    Get
    '        Return _CodRecinto
    '    End Get
    '    Set(value As Integer)
    '        _CodRecinto = value
    '    End Set
    'End Property

    Public Function InserirUsuario(ByVal UsuarioOBJ As Usuario) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("INSERT INTO OPERADOR.TB_GD_LOGIN (AUTONUM_LOGIN_GD,AUTONUM_TRANSPORTADORA,NOME,SENHA,CPF,DT_CADASTRO,FLAG_ATIVO,FLAG_ADMIN,EMAIL) VALUES ({0},{1},'{2}','{3}','{4}','{5}',{6},{7},'{8}')", "OPERADOR.SEQ_GD_LOGIN.NEXTVAL", UsuarioOBJ.Transportadora.ID, UsuarioOBJ.Nome, GerarSenha(UsuarioOBJ.CNPJ), UsuarioOBJ.CNPJ, Now.Date, UsuarioOBJ.Ativo, UsuarioOBJ.Admin, UsuarioOBJ.Email))
        Else
            SQL.Append(String.Format("INSERT INTO OPERADOR.DBO.TB_GD_LOGIN (AUTONUM_TRANSPORTADORA,NOME,SENHA,CPF,DT_CADASTRO,FLAG_ATIVO,FLAG_ADMIN,EMAIL) VALUES ({0},{1},'{2}','{3}','{4}','{5}',{6},{7},'{8}')", UsuarioOBJ.Transportadora.ID, UsuarioOBJ.Nome, GerarSenha(UsuarioOBJ.CNPJ), UsuarioOBJ.CNPJ, Now.Date, UsuarioOBJ.Ativo, UsuarioOBJ.Admin, UsuarioOBJ.Email))
        End If

        Try
            Banco.Conexao.Execute(SQL.ToString(), Banco.LinhasAfetadas)
            If Banco.LinhasAfetadas Then
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("Erro. Tente novamente." & ex.Message())
        End Try

        Return False

    End Function

    Public Function AlterarUsuario(ByVal UsuarioOBJ As Usuario) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("UPDATE OPERADOR.TB_GD_LOGIN SET NOME='{0}',CPF='{1}',FLAG_ATIVO={2},FLAG_ADMIN={3},EMAIL='{4}' WHERE AUTONUM_LOGIN_GD={5}", UsuarioOBJ.Nome, UsuarioOBJ.CNPJ, UsuarioOBJ.Ativo, UsuarioOBJ.Email, UsuarioOBJ.Codigo))
        Else
            SQL.Append(String.Format("UPDATE OPERADOR.DBO.TB_GD_LOGIN SET NOME='{0}',CPF='{1}',FLAG_ATIVO={2},FLAG_ADMIN={3},EMAIL='{4}' WHERE AUTONUM_LOGIN_GD={5}", UsuarioOBJ.Nome, UsuarioOBJ.CNPJ, UsuarioOBJ.Ativo, UsuarioOBJ.Email, UsuarioOBJ.Codigo))
        End If

        Try
            Banco.Conexao.Execute(SQL.ToString(), Banco.LinhasAfetadas)
            If Banco.LinhasAfetadas Then
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("Erro. Tente novamente." & ex.Message())
        End Try

        Return False

    End Function

    Public Function ExcluirUsuario(ByVal UsuarioOBJ As Usuario) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("DELETE FROM OPERADOR.TB_GD_LOGIN WHERE AUTONUM_LOGIN_GD={0}", UsuarioOBJ.Codigo))
        Else
            SQL.Append(String.Format("DELETE FROM OPERADOR.DBO.TB_GD_LOGIN WHERE AUTONUM_LOGIN_GD={0}", UsuarioOBJ.Codigo))
        End If

        Try
            Banco.Conexao.Execute(SQL.ToString(), Banco.LinhasAfetadas)
            If Banco.LinhasAfetadas Then
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("Erro. Tente novamente." & ex.Message())
        End Try

        Return False

    End Function

    Public Function ValidarUsuario(ByVal UsuarioOBJ As Usuario) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("SELECT FROM OPERADOR.TB_GD_LOGIN WHERE CPF='{0}' AND AUTONUM_TRANSPORTADORA={1}", UsuarioOBJ.Codigo, UsuarioOBJ.Transportadora.ID))
        Else
            SQL.Append(String.Format("SELECT FROM OPERADOR.DBO.TB_GD_LOGIN WHERE CPF='{0}' AND AUTONUM_TRANSPORTADORA={1}", UsuarioOBJ.Codigo, UsuarioOBJ.Transportadora.ID))
        End If

        If Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function Consultar() As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT * FROM OPERADOR.TB_GD_LOGIN ORDER BY NOME ASC")
        Else
            SQL.Append("SELECT FROM OPERADOR.DBO.TB_GD_LOGIN ORDER BY NOME ASC")
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_LOGIN")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function GerarSenha(ByVal CPF As String) As String
        Return FormsAuthentication.HashPasswordForStoringInConfigFile(CPF, "MD5")
    End Function

    Public Function LembrarSenha(ByVal CPF As String) As Boolean

        Dim ID As String = ObterCodigoUsuario(CPF)
        Dim Email As String = ObterEmailUsuario(CPF)
        Dim Senha As String = ObterSenhaUsuario(ID)

        Return EnviarEmail(Email, CPF, Senha)

    End Function

    Public Function EnviarEmail(ByVal Email As String, ByVal CPF As String, ByVal Senha As String) As Boolean

        Dim Texto As New StringBuilder

        Try

            Texto.Append("<!DOCTYPE html PUBLIC -//W3C//DTD HTML 4.01 Transitional//EN>")
            Texto.Append("<html>")
            Texto.Append("<head>")
            Texto.Append("<meta content=text/html;charset=ISO-8859-1 http-equiv=Content-Type>")
            Texto.Append("</head>")
            Texto.Append("<body bgcolor=#ffffff text=#000000>")
            Texto.Append("Prezado(a) Usuário(a) <b>'.$nome.'</b>,<br><br>")
            Texto.Append("Em resposta à vossa solicitação, encaminhamos seus dados para acesso ao site <b>GATE POR DEMANDA - TECONDI</b>:<br><br> ")
            Texto.Append("Login: " & CPF & "<br>")
            Texto.Append("Senha: " & Senha & "<br><br>")
            Texto.Append("Att,<br>")
            Texto.Append("<b>TECONDI</b><br>")
            Texto.Append("</body>")
            Texto.Append("</html>'")

            Dim mMailMessage As New MailMessage()
            mMailMessage.From = New MailAddress("webmaster@tecondi.com.br")
            mMailMessage.To.Add(New MailAddress(Email))

            mMailMessage.Subject = "Senha de acesso ao site Gate por Demanda - TECONDI"
            mMailMessage.Body = Texto.ToString()
            mMailMessage.IsBodyHtml = True
            mMailMessage.Priority = MailPriority.Normal

            Dim mSmtpClient As New SmtpClient()
            mSmtpClient.Send(mMailMessage)

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ObterEmailUsuario(ByVal CPF As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT EMAIL FROM OPERADOR.TB_GD_LOGIN WHERE CPF='{0}'", CPF), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT EMAIL FROM OPERADOR.DBO.TB_GD_LOGIN WHERE CPF='{0}'", CPF), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("EMAIL").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterSenhaUsuario(ByVal ID As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT SENHA FROM OPERADOR.TB_GD_LOGIN WHERE AUTONUM_LOGIN_GD={0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT SENHA FROM OPERADOR.DBO.TB_GD_LOGIN WHERE AUTONUM_LOGIN_GD={0}", ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("SENHA").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterCodigoUsuario(ByVal CPF As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM_LOGIN_GD FROM OPERADOR.TB_GD_LOGIN WHERE CPF='{0}'", CPF), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM_LOGIN_GD FROM OPERADOR.DBO.TB_GD_LOGIN WHERE CPF='{0}'", CPF), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUM_LOGIN_GD").Value.ToString()
        End If

        Return String.Empty

    End Function


End Class

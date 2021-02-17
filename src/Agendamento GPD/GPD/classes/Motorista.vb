Imports System.Data.OleDb

Public Class Motorista

    Private _ID As Integer
    Private _CNH As String
    Private _Nome As String
    Private _RG As String
    Private _CPF As String
    Private _Celular As String
    Private _Nextel As String
    Private _Validade As String
    Private _NumeroMOP As String
    Private _Transportadora As Transportadora

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property CNH() As String
        Get
            Return _CNH
        End Get
        Set(ByVal value As String)
            _CNH = value
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

    Public Property RG() As String
        Get
            Return _RG
        End Get
        Set(ByVal value As String)
            _RG = value
        End Set
    End Property

    Public Property CPF() As String
        Get
            Return _CPF
        End Get
        Set(ByVal value As String)
            _CPF = value
        End Set
    End Property

    Public Property Celular() As String
        Get
            Return _Celular
        End Get
        Set(ByVal value As String)
            _Celular = value
        End Set
    End Property

    Public Property Nextel() As String
        Get
            Return _Nextel
        End Get
        Set(ByVal value As String)
            _Nextel = value
        End Set
    End Property

    Public Property Validade() As String
        Get
            Return _Validade
        End Get
        Set(ByVal value As String)
            _Validade = value
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

    Public Property NumeroMOP() As String
        Get
            Return _NumeroMOP
        End Get
        Set(ByVal value As String)
            _NumeroMOP = value
        End Set
    End Property

    Public Function Inserir(ByVal Motorista As Motorista) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("INSERT INTO OPERADOR.TB_AG_MOTORISTAS (AUTONUM,ID_TRANSPORTADORA,NOME,CNH,VALIDADE_CNH,RG,CPF,CELULAR,NEXTEL,NUMERO_MOP) VALUES ({0},{1},'{2}','{3}',TO_DATE('{4}','DD/MM/YYYY'),'{5}','{6}','{7}','{8}','{9}')", "OPERADOR.SEQ_AG_MOTORISTAS.NEXTVAL", Motorista.Transportadora.ID, Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP))
        Else
            SQL.Append(String.Format("INSERT INTO OPERADOR.TB_AG_MOTORISTAS (AUTONUM,ID_TRANSPORTADORA,NOME,CNH,VALIDADE_CNH,RG,CPF,CELULAR,NEXTEL,NUMERO_MOP) VALUES ({0},'{1}','{2}','{3}',TO_DATE('{4}','DD/MM/YYYY'),'{5}','{6}','{7}','{8}','{9}')", Motorista.Transportadora.ID, Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP))
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

    Public Function Alterar(ByVal Motorista As Motorista) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("UPDATE OPERADOR.TB_AG_MOTORISTAS SET NOME='{0}',CNH='{1}',VALIDADE_CNH=TO_DATE('{2}','DD/MM/YYYY'),RG='{3}',CPF='{4}',CELULAR='{5}',NEXTEL='{6}',NUMERO_MOP={7} WHERE AUTONUM={8}", Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP, Motorista.ID))
        Else
            SQL.Append(String.Format("UPDATE OPERADOR.DBO.TB_AG_MOTORISTAS SET NOME='{0}',CNH='{1}',VALIDADE_CNH=TO_DATE('{2}','DD/MM/YYYY'),RG='{3}',CPF='{4}',CELULAR='{5}',NEXTEL='{6}',NUMERO_MOP={7} WHERE AUTONUM={8}", Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP, Motorista.ID))
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

    Public Function AlterarCPFporCNH(ByVal Motorista As Motorista) As Integer
        Dim SQL As New StringBuilder

        SQL.Append("UPDATE ")
        SQL.Append("    OPERADOR.")
        If Banco.BancoEmUso = "SQLSERVER" Then
            SQL.Append("DBO.")
        End If
        SQL.Append("TB_AG_MOTORISTAS ")
        SQL.Append("SET ")
        SQL.Append("    CPF = '{0}' ")
        SQL.Append("WHERE ")
        SQL.Append("    CNH = '{1}' ")

        Try
            Banco.Conexao.Execute(String.Format(SQL.ToString(), Motorista.CPF, Motorista.CNH), Banco.LinhasAfetadas)
            If Banco.LinhasAfetadas Then
                Return Banco.LinhasAfetadas
            End If
        Catch ex As Exception
            Throw New Exception("Erro. Tente novamente." & ex.Message())
        End Try

        Return -1

    End Function

    Public Function Excluir(ByVal Motorista As Motorista) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("DELETE FROM OPERADOR.TB_AG_MOTORISTAS WHERE AUTONUM={0}", Motorista.ID))
        Else
            SQL.Append(String.Format("DELETE FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE AUTONUM={0}", Motorista.ID))
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

    Public Function ValidarExclusao(ByVal Motorista As Motorista)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
          SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM SGIPA.TB_AG_CS B WHERE B.AUTONUM_MOTORISTA = {0} AND B.AUTONUM_TRANSPORTADORA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM REDEX.TB_AG_CS C WHERE C.AUTONUM_MOTORISTA = {0} AND C.AUTONUM_TRANSPORTADORA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_GD_MOTORISTA FROM OPERADOR.TB_GD_CONTEINER D WHERE D.AUTONUM_GD_MOTORISTA = {0} AND D.AUTONUM_TRANSPORTADORA = {1}) ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM SGIPA.TB_AG_CS B WHERE B.AUTONUM_MOTORISTA = {0} AND B.AUTONUM_TRANSPORTADORA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_MOTORISTA FROM REDEX.TB_AG_CS C WHERE C.AUTONUM_MOTORISTA = {0} AND C.AUTONUM_TRANSPORTADORA = {1}) ")
            SQL.Append("OR ")
            SQL.Append("    A.AUTONUM IN ")
            SQL.Append("        (SELECT AUTONUM_GD_MOTORISTA FROM OPERADOR.TB_GD_CONTEINER D WHERE D.AUTONUM_GD_MOTORISTA = {0} AND D.AUTONUM_TRANSPORTADORA = {1}) ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Motorista.ID, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.NOME, ")
            SQL.Append("    A.CPF, ")
            SQL.Append("    A.CELULAR, ")
            SQL.Append("    A.CNH, ")
            SQL.Append("    A.ID_TRANSPORTADORA, ")
            SQL.Append("    B.RAZAO AS TRANSPORTADORA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} {1} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.NOME, ")
            SQL.Append("    A.CPF, ")
            SQL.Append("    A.CELULAR, ")
            SQL.Append("    A.CNH, ")
            SQL.Append("    A.ID_TRANSPORTADORA, ")
            SQL.Append("    B.RAZAO AS TRANSPORTADORA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} {1} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")
        End If

        Return String.Format(SQL.ToString(), ID, Filtro)

    End Function

    Public Function ConsultarPorID(ByVal ID As Integer) As Motorista

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.NOME, ")
            SQL.Append("    A.RG, ")
            SQL.Append("    A.CPF, ")
            SQL.Append("    A.CELULAR, ")
            SQL.Append("    A.NEXTEL, ")
            SQL.Append("    A.CNH, ")
            SQL.Append("    A.VALIDADE_CNH, ")
            SQL.Append("    A.NUMERO_MOP, ")
            SQL.Append("    B.RAZAO AS TRANSPORTADORA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")
        Else
          SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.NOME, ")
            SQL.Append("    A.RG, ")
            SQL.Append("    A.CPF, ")
            SQL.Append("    A.CELULAR, ")
            SQL.Append("    A.NEXTEL, ")
            SQL.Append("    A.CNH, ")
            SQL.Append("    TO_CHAR(A.VALIDADE_CNH,'DD/MM/YY') VALIDADE_CNH, ")
            SQL.Append("    A.NUMERO_MOP, ")
            SQL.Append("    B.RAZAO AS TRANSPORTADORA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim MotoristaOBJ As New Motorista

            MotoristaOBJ.ID = Rst.Fields("AUTONUM").Value.ToString()
            MotoristaOBJ.Celular = Rst.Fields("CELULAR").Value.ToString()
            MotoristaOBJ.CNH = Rst.Fields("CNH").Value.ToString()
            MotoristaOBJ.CPF = Rst.Fields("CPF").Value.ToString()
            MotoristaOBJ.Nextel = Rst.Fields("NEXTEL").Value.ToString()
            MotoristaOBJ.Nome = Rst.Fields("NOME").Value.ToString()
            MotoristaOBJ.NumeroMOP = Rst.Fields("NUMERO_MOP").Value.ToString()
            MotoristaOBJ.RG = Rst.Fields("RG").Value.ToString()
            MotoristaOBJ.Validade = Rst.Fields("VALIDADE_CNH").Value.ToString()

            Return MotoristaOBJ

        End If

        Return Nothing

    End Function

    Public Function ConsultarDadosIguais(ByVal CNH As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As String = "") As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("AND ")
            SQL.Append("    A.CNH = '{1}' ")
          
            If Not ID = String.Empty Then
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM <> {2} ")
            End If

            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")

        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("AND ")
            SQL.Append("    A.CNH = '{1}' ")

            If Not ID = String.Empty Then
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM <> {2} ")
            End If

            SQL.Append("ORDER BY ")
            SQL.Append("    A.NOME ASC ")

        End If

        If Not ID = String.Empty Then
            Rst.Open(String.Format(SQL.ToString(), ID_Transportadora, CNH, ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format(SQL.ToString(), ID_Transportadora, CNH), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function ConsultarCNH(ByVal CNH As String) As Motorista

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT CNH,CNH_VALIDADE,NOME,RG,CPF,TELEFONE FROM OPERADOR.TB_MOTORISTAS WHERE TRIM(CNH) = '{0}'", CNH), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT CNH,CNH_VALIDADE,NOME,RG,CPF,TELEFONE FROM OPERADOR.DBO.TB_MOTORISTAS WHERE RTRIM(CNH) = '{0}'", CNH), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            Dim Motorista As New Motorista

            Motorista.CNH = Rst.Fields("CNH").Value.ToString()
            Motorista.Validade = Rst.Fields("CNH_VALIDADE").Value.ToString()
            Motorista.Nome = Rst.Fields("NOME").Value.ToString()
            Motorista.RG = Rst.Fields("RG").Value.ToString()
            Motorista.CPF = Rst.Fields("CPF").Value.ToString()
            Motorista.Celular = Rst.Fields("TELEFONE").Value.ToString()

            Return Motorista

        End If

        Return Nothing

    End Function

    Public Function ObterValidadeCNH(ByVal CNH As String, ByVal Transportadora As String) As String

        Dim Rst As New ADODB.Recordset
        Rst.Open(String.Format("SELECT VALIDADE_CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE TRIM(CNH) = '{0}' AND ID_TRANSPORTADORA = {1}", CNH, Transportadora), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("VALIDADE_CNH").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ConsultarCNHPorTransportadora(ByVal ID As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim ListaCNH As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID), Banco.Conexao, 3, 3)
        End If

        While Not Rst.EOF
            ListaCNH.Add(Rst.Fields("CNH").Value.ToString())
            Rst.MoveNext()
        End While

        Return ListaCNH

    End Function

    Public Function ObterCodigoMotorista(ByVal Motorista As Motorista) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As String

        If Banco.BancoEmUso = "ORACLE" Then
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH='{1}' AND ID_TRANSPORTADORA={2}", Motorista.Nome, Motorista.CNH, Motorista.Transportadora.ID)
        Else
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE CNH='{1}' AND ID_TRANSPORTADORA={2}", Motorista.Nome, Motorista.CNH, Motorista.Transportadora.ID)
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUM").Value.ToString()
        End If

        Return False

    End Function

    Public Function ObterCPFMotorista(ByVal CNH As String) As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As String

        If Banco.BancoEmUso = "ORACLE" Then
            SQL = String.Format("SELECT CPF FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}'", CNH)
        Else
            SQL = String.Format("SELECT CPF FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE CNH = '{0}'", CNH)
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("CPF").Value.ToString()
        End If

        Return False

    End Function

End Class

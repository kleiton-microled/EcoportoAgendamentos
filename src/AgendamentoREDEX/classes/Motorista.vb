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

        Try
            Banco.BeginTransaction(String.Format("INSERT INTO OPERADOR.TB_AG_MOTORISTAS (AUTONUM,ID_TRANSPORTADORA,NOME,CNH,VALIDADE_CNH,RG,CPF,CELULAR,NEXTEL,NUMERO_MOP) VALUES (OPERADOR.SEQ_AG_MOTORISTAS.NEXTVAL,{0},'{1}','{2}',TO_DATE('{3}','DD/MM/YYYY'),'{4}','{5}','{6}','{7}',{8})", Motorista.Transportadora.ID, Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Alterar(ByVal Motorista As Motorista) As Boolean

        Try
            Banco.BeginTransaction(String.Format("UPDATE OPERADOR.TB_AG_MOTORISTAS SET NOME='{0}',CNH='{1}',VALIDADE_CNH=TO_DATE('{2}','DD?MM/YYYY'),RG='{3}',CPF='{4}',CELULAR='{5}',NEXTEL='{6}',NUMERO_MOP={7} WHERE AUTONUM={8}", Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP, Motorista.ID))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Excluir(ByVal Motorista As Motorista) As Boolean

        Try
            Banco.BeginTransaction(String.Format("DELETE FROM OPERADOR.TB_AG_MOTORISTAS WHERE AUTONUM={0}", Motorista.ID))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidarExclusao(ByVal Motorista As Motorista) As Boolean

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_MOTORISTA = {0} AND AUTONUM_TRANSPORTADORA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTADORA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTADORA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTADORA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM SGIPA.TB_AG_CS WHERE AUTONUM_MOTORISTA = {0} AND AUTONUM_TRANSPORTADORA = {1}", Motorista.ID, Motorista.Transportadora.ID))) > 0 Then
            Return False
        End If

        Return True

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As String

        Dim SQL As New StringBuilder

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

        Return String.Format(SQL.ToString(), ID, Filtro)

    End Function

    Public Function ConsultarPorID(ByVal ID As Integer) As Motorista

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM, ")
        SQL.Append("    A.NOME, ")
        SQL.Append("    A.RG, ")
        SQL.Append("    A.CPF, ")
        SQL.Append("    A.CELULAR, ")
        SQL.Append("    A.NEXTEL, ")
        SQL.Append("    A.CNH, ")
        SQL.Append("    TO_CHAR(A.VALIDADE_CNH,'DD/MM/YYYY') VALIDADE_CNH, ")
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

        Dim ds As New DataTable
        ds = Banco.List(String.Format(SQL.ToString(), ID))

        If ds IsNot Nothing Then
            If ds.Rows.Count > 0 Then

                Dim MotoristaOBJ As New Motorista

                MotoristaOBJ.ID = ds.Rows(0)("AUTONUM").ToString()
                MotoristaOBJ.Celular = ds.Rows(0)("CELULAR").ToString()
                MotoristaOBJ.CNH = ds.Rows(0)("CNH").ToString()
                MotoristaOBJ.CPF = ds.Rows(0)("CPF").ToString()
                MotoristaOBJ.Nextel = ds.Rows(0)("NEXTEL").ToString()
                MotoristaOBJ.Nome = ds.Rows(0)("NOME").ToString()
                MotoristaOBJ.NumeroMOP = ds.Rows(0)("NUMERO_MOP").ToString()
                MotoristaOBJ.RG = ds.Rows(0)("RG").ToString()
                MotoristaOBJ.Validade = ds.Rows(0)("VALIDADE_CNH").ToString()

                Return MotoristaOBJ

            End If
        End If

        Return Nothing

    End Function

    Public Function ConsultarDadosIguais(ByVal CNH As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As String = "") As Boolean

        Dim SQL As New StringBuilder

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

        Dim iSQL As String = ""
        Dim retorno As String = ""

        If Not ID = String.Empty Then
            iSQL = String.Format(SQL.ToString(), ID_Transportadora, CNH, ID)
        Else
            iSQL = String.Format(SQL.ToString(), ID_Transportadora, CNH)
        End If

        retorno = Banco.ExecuteScalar(iSQL)

        If retorno <> "" Then
            Return True
        End If

        Return False

    End Function

    Public Function ConsultarCNH(ByVal CNH As String) As Motorista

        Dim ds As New DataTable
        ds = Banco.List(String.Format("SELECT CNH,CNH_VALIDADE,NOME,RG,CPF,TELEFONE FROM OPERADOR.TB_MOTORISTAS WHERE RTRIM(CNH) = '{0}'", CNH))

        If ds IsNot Nothing Then
            If ds.Rows.Count > 0 Then

                Dim Motorista As New Motorista

                Motorista.CNH = ds.Rows(0)("CNH").ToString()
                Motorista.Validade = ds.Rows(0)("CNH_VALIDADE").ToString()
                Motorista.Nome = ds.Rows(0)("NOME").ToString()
                Motorista.RG = ds.Rows(0)("RG").ToString()
                Motorista.CPF = ds.Rows(0)("CPF").ToString()
                Motorista.Celular = ds.Rows(0)("TELEFONE").ToString()

                Return Motorista

            End If
        End If

        Return Nothing

    End Function

    Public Function MontarLista(ByVal SQL As String, ByVal Campo As String)

        Dim Lista As New List(Of String)
        Dim ds As New DataTable

        ds = Banco.List(SQL)

        If ds IsNot Nothing Then
            If ds.Rows.Count > 0 Then
                For Each Linha As DataRow In ds.Rows
                    Lista.Add(Linha("" & Campo & "").ToString())
                Next
            End If
        End If

        Return Lista

    End Function

    Public Function ConsultarCNHPorTransportadora(ByVal ID As String) As List(Of String)
        Return MontarLista(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID), "CNH")
    End Function

    Public Function ObterCodigoMotorista(ByVal Motorista As Motorista) As Integer
        Return Banco.ExecuteScalar(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME='{0}' AND CNH='{1}' AND ID_TRANSPORTADORA={2}", Motorista.Nome, Motorista.CNH, Motorista.Transportadora.ID))
    End Function

    Public Function ObterCPFMotorista(ByVal CNH As String) As String
        Return Banco.ExecuteScalar(String.Format("SELECT CPF FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}'", CNH))
    End Function

End Class

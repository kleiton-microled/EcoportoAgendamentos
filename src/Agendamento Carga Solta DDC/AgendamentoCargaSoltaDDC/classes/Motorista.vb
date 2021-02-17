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
            Banco.Executar(String.Format("INSERT INTO OPERADOR.TB_AG_MOTORISTAS (AUTONUM,ID_TRANSPORTADORA,NOME,CNH,VALIDADE_CNH,RG,CPF,CELULAR,NEXTEL,NUMERO_MOP) VALUES ({0},{1},'{2}','{3}',TO_DATE('{4}', 'DD/MM/YY'),'{5}','{6}','{7}','{8}','{9}')", "OPERADOR.SEQ_AG_MOTORISTAS.NEXTVAL", Motorista.Transportadora.ID, Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function Alterar(ByVal Motorista As Motorista) As Boolean

        Try
            Banco.Executar(String.Format("UPDATE OPERADOR.TB_AG_MOTORISTAS SET NOME='{0}',CNH='{1}',VALIDADE_CNH=TO_DATE('{2}', 'DD/MM/YY'),RG='{3}',CPF='{4}',CELULAR='{5}',NEXTEL='{6}',NUMERO_MOP={7} WHERE AUTONUM={8}", Motorista.Nome, Motorista.CNH, Motorista.Validade, Motorista.RG, Motorista.CPF, Motorista.Celular, Motorista.Nextel, Motorista.NumeroMOP, Motorista.ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function AlterarCPFporCNH(ByVal Motorista As Motorista) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("UPDATE ")
        SQL.Append("    OPERADOR.")
        SQL.Append("TB_AG_MOTORISTAS ")
        SQL.Append("SET ")
        SQL.Append("    CPF = '{0}' ")
        SQL.Append("WHERE ")
        SQL.Append("    CNH = '{1}' ")

        Try
            Banco.Executar(String.Format(SQL.ToString(), Motorista.CPF, Motorista.CNH))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function Excluir(ByVal Motorista As Motorista) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM OPERADOR.TB_AG_MOTORISTAS WHERE AUTONUM={0}", Motorista.ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ValidarExclusao(ByVal Motorista As Motorista) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_MOTORISTAS A ")
        SQL.Append("WHERE ")
        SQL.Append(" (")
        SQL.Append("    A.AUTONUM IN ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT ")
        SQL.Append("                AUTONUM_MOTORISTA ")
        SQL.Append("            FROM ")
        SQL.Append("                SGIPA.TB_AG_CS ")
        SQL.Append("        ) OR ")
        SQL.Append("    A.AUTONUM IN ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT ")
        SQL.Append("                AUTONUM_MOTORISTA ")
        SQL.Append("            FROM ")
        SQL.Append("                REDEX.TB_AG_CS ")
        SQL.Append("        ) OR ")
        SQL.Append("    A.AUTONUM IN ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT ")
        SQL.Append("                 AUTONUM_GD_MOTORISTA ")
        SQL.Append("            FROM ")
        SQL.Append("                OPERADOR.TB_GD_CONTEINER ")
        SQL.Append("        ) OR ")
        SQL.Append("    A.AUTONUM IN ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT ")
        SQL.Append("                AUTONUM_MOTORISTA")
        SQL.Append("            FROM ")
        SQL.Append("                SGIPA.TB_CNTR_BL ")
        SQL.Append("        ) ")
        SQL.Append(") ")
        SQL.Append("AND ")
        SQL.Append("    A.AUTONUM = {0} ")
        SQL.Append("AND ")
        SQL.Append("    A.ID_TRANSPORTADORA = {1} ")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), Motorista.ID, Motorista.Transportadora.ID))

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As DataTable

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

        Return Banco.Consultar(String.Format(SQL.ToString(), ID, Filtro))

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

        Dim Dt As New DataTable
        Dt = Banco.Consultar(String.Format(SQL.ToString(), ID))

        If Dt.Rows.Count > 0 Then

            Dim MotoristaOBJ As New Motorista

            MotoristaOBJ.ID = Dt.Rows(0)("AUTONUM").ToString()
            MotoristaOBJ.Celular = Dt.Rows(0)("CELULAR").ToString()
            MotoristaOBJ.CNH = Dt.Rows(0)("CNH").ToString()
            MotoristaOBJ.CPF = Dt.Rows(0)("CPF").ToString()
            MotoristaOBJ.Nextel = Dt.Rows(0)("NEXTEL").ToString()
            MotoristaOBJ.Nome = Dt.Rows(0)("NOME").ToString()
            MotoristaOBJ.NumeroMOP = Dt.Rows(0)("NUMERO_MOP").ToString()
            MotoristaOBJ.RG = Dt.Rows(0)("RG").ToString()
            MotoristaOBJ.Validade = Dt.Rows(0)("VALIDADE_CNH").ToString()

            Return MotoristaOBJ

        End If

        Return Nothing

    End Function

    Public Function ConsultarDadosIguais(ByVal CNH As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As String = "") As Boolean

        Dim SQL As New StringBuilder
        Dim Resultado As String = String.Empty

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

        If ID <> String.Empty Then
            Resultado = Banco.ExecutaRetorna(String.Format(SQL.ToString(), ID_Transportadora, CNH, ID))
        Else
            Resultado = Banco.ExecutaRetorna(String.Format(SQL.ToString(), ID_Transportadora, CNH))
        End If

        If Val(Resultado) > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function ConsultarCNH(ByVal CNH As String) As Motorista

        Dim Dt As New DataTable

        Dt = Banco.Consultar(String.Format("SELECT CNH,CNH_VALIDADE,NOME,RG,CPF,TELEFONE FROM OPERADOR.TB_MOTORISTAS WHERE TRIM(CNH) = '{0}'", CNH))

        If Dt.Rows.Count > 0 Then

            Dim Motorista As New Motorista

            Motorista.CNH = Dt.Rows(0)("CNH").ToString()
            Motorista.Validade = Dt.Rows(0)("CNH_VALIDADE").ToString()
            Motorista.Nome = Dt.Rows(0)("NOME").ToString()
            Motorista.RG = Dt.Rows(0)("RG").ToString()
            Motorista.CPF = Dt.Rows(0)("CPF").ToString()
            Motorista.Celular = Dt.Rows(0)("TELEFONE").ToString()

            Return Motorista

        End If

        Return Nothing

    End Function

    Public Function ConsultarCNHPorTransportadora(ByVal ID As String) As List(Of String)

        Dim Dt As New DataTable
        Dim ListaCNH As New List(Of String)

        Dt = Banco.Consultar(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID))

        For Each Item As DataRow In Dt.Rows
            ListaCNH.Add(Dt("CNH").ToString())
        Next

        Return ListaCNH

    End Function

    Public Function ObterCodigoMotorista(ByVal Motorista As Motorista) As Integer
        Return Banco.ExecutaRetorna(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH='{0}' AND ID_TRANSPORTADORA = {1}", Motorista.CNH, Motorista.Transportadora.ID))
    End Function

    Public Function ObterCPFMotorista(ByVal CNH As String, ByVal IdTransp As Integer) As String
        Return Banco.ExecutaRetorna(String.Format("SELECT NVL(CPF, ' ') CPF FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}' AND ID_TRANSPORTADORA = {1}", CNH, IdTransp))
    End Function

End Class

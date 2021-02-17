Imports System.Data.OleDb
Public Class Veiculo

    Private _ID As Integer
    Private _Cavalo As String
    Private _Carreta As String
    Private _Reboque As String
    Private _Chassi As String
    Private _Renavam As String
    Private _Tipo As String
    Private _Tara As String
    Private _Modelo As String
    Private _Cor As String
    Private _Transportadora As Transportadora

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property Cavalo() As String
        Get
            Return _Cavalo
        End Get
        Set(ByVal value As String)
            _Cavalo = value
        End Set
    End Property

    Public Property Carreta() As String
        Get
            Return _Carreta
        End Get
        Set(ByVal value As String)
            _Carreta = value
        End Set
    End Property

    Public Property Reboque() As String
        Get
            Return _Reboque
        End Get
        Set(ByVal value As String)
            _Reboque = value
        End Set
    End Property

    Public Property Chassi() As String
        Get
            Return _Chassi
        End Get
        Set(ByVal value As String)
            _Chassi = value
        End Set
    End Property

    Public Property Renavam() As String
        Get
            Return _Renavam
        End Get
        Set(ByVal value As String)
            _Renavam = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property

    Public Property Tara() As String
        Get
            Return _Tara
        End Get
        Set(ByVal value As String)
            _Tara = value
        End Set
    End Property

    Public Property Modelo() As String
        Get
            Return _Modelo
        End Get
        Set(ByVal value As String)
            _Modelo = value
        End Set
    End Property

    Public Property Cor() As String
        Get
            Return _Cor
        End Get
        Set(ByVal value As String)
            _Cor = value
        End Set
    End Property

    Public Property Transportadora As Transportadora
        Get
            Return _Transportadora
        End Get
        Set(ByVal value As Transportadora)
            _Transportadora = value
        End Set
    End Property

    Public Function Inserir(ByVal Veiculo As Veiculo) As Boolean

        Try
            Banco.BeginTransaction(String.Format("INSERT INTO OPERADOR.TB_AG_VEICULOS (AUTONUM,ID_TRANSPORTADORA,PLACA_CAVALO,PLACA_CARRETA,CHASSI,RENAVAM,ID_TIPO_CAMINHAO,TARA,MODELO,COR,PLACA_REBOQUE) VALUES (OPERADOR.SEQ_AG_VEICULOS.NEXTVAL,{0},'{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}')", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor, Veiculo.Reboque))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Alterar(ByVal Veiculo As Veiculo) As Boolean

        Try
            Banco.BeginTransaction(String.Format("UPDATE OPERADOR.TB_AG_VEICULOS SET ID_TRANSPORTADORA={0},PLACA_CAVALO='{1}',PLACA_CARRETA='{2}',CHASSI='{3}',RENAVAM={4},ID_TIPO_CAMINHAO={5},TARA={6},MODELO='{7}',COR='{8}', PLACA_REBOQUE = '{9}' WHERE AUTONUM={10}", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor, Veiculo.Reboque, Veiculo.ID))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Excluir(ByVal Veiculo As Veiculo) As Boolean

        Try
            Banco.BeginTransaction(String.Format("DELETE FROM OPERADOR.TB_AG_VEICULOS WHERE AUTONUM={0}", Veiculo.ID))
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidarExclusao(ByVal Veiculo As Veiculo)

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM SGIPA.TB_AG_CS WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        Dim DtVeic As DataTable = Veiculo.ConsultarPorID(Veiculo.ID)
        Dim SQL As New StringBuilder

        SQL.Clear()
        SQL.Append("SELECT COUNT(*) ")
        SQL.Append("FROM OPERADOR.TB_WORKING_LIST ")
        SQL.Append("WHERE ")
        SQL.Append("TRANSPORTADORA_CAB = {0} ")
        SQL.Append("AND PLACA_C_CAB = '{1}' ")
        SQL.Append("AND PLACA_CARRETA_CAB = '{2}' ")

        If DtVeic.Rows(0)("PLACA_REBOQUE").ToString() <> "___-____" And DtVeic.Rows(0)("PLACA_REBOQUE").ToString() <> String.Empty Then
            SQL.Append(" AND PLACA_REBOQUE_CAB = '" & DtVeic(0)("PLACA_REBOQUE").ToString() & "'")
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(String.Format(SQL.ToString(), Veiculo.Transportadora.ID, DtVeic(0)("PLACA_CAVALO").ToString(), DtVeic(0)("PLACA_CARRETA").ToString()))) > 0 Then
            Return False
        End If

        Return True

    End Function

    Public Function ValidarVeiculosIguais(ByVal Veiculo As Veiculo, ByVal Edit As Boolean)

        Dim Complemento As String

        If Veiculo.Reboque = "___-____" Or Veiculo.Reboque = "" Then
            Complemento = " OR PLACA_REBOQUE IS NULL"
            If Veiculo.Reboque = "" Then
                Veiculo.Reboque = "___-____" 'Com isto procura tanto ___-____ quanto vazio
            End If
        Else
            Complemento = ""
        End If

        If Edit Then
            If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND (PLACA_REBOQUE = '{2}'" & Complemento & ") AND ID_TRANSPORTADORA={3} AND AUTONUM <> {4}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Reboque, Veiculo.Transportadora.ID, Veiculo.ID))) = 0 Then
                Return True
            End If
        Else
            If Convert.ToInt32(Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND (PLACA_REBOQUE = '{2}'" & Complemento & ") AND ID_TRANSPORTADORA={3}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Reboque, Veiculo.Transportadora.ID))) = 0 Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As String

        Return String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.PLACA_REBOQUE,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TRANSPORTADORA,A.MODELO,A.COR,B.RAZAO AS TRANSPORTADORA,C.DESCR AS TIPO FROM OPERADOR.TB_AG_VEICULOS A LEFT JOIN OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM LEFT JOIN OPERADOR.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM WHERE A.ID_TRANSPORTADORA = {0} {1} ORDER BY AUTONUM DESC", ID, Filtro)

    End Function

    Public Function ConsultarPorID(ByVal ID As Integer) As DataTable
        Return Banco.List(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.PLACA_REBOQUE,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TIPO_CAMINHAO,A.MODELO,A.COR FROM OPERADOR.TB_AG_VEICULOS A WHERE A.AUTONUM = {0}", ID))
    End Function

    ''' <summary>
    ''' Consulta dados dos veículos de acordo com as placas dele
    ''' </summary>
    ''' <param name="Cavalo">Placa do Cavalo</param>
    ''' <param name="Carreta">Placa da Carreta</param>
    ''' <param name="Transportadora">Identificação da Transportadora</param>
    ''' <param name="Reboque">Placa do Reboque</param>
    ''' <returns>Dados do veículo</returns>
    ''' <remarks></remarks>
    Public Function ConsultarPorPlacas(ByVal Cavalo As String, ByVal Carreta As String, ByVal Transportadora As String, ByVal Reboque As String) As Veiculo

        Dim SQL As New StringBuilder
        Dim Veic As New Veiculo

        Dim Complemento As String

        If Reboque = "___-____" Or Reboque = "" Then
            Complemento = " OR TRIM(PLACA_REBOQUE) IS NULL"
            If Reboque = "" Then
                Reboque = "___-____"
            End If
        Else
            Complemento = ""
        End If

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM, TARA, CHASSI, RENAVAM, ID_TIPO_CAMINHAO, ")
        SQL.Append("    MODELO, COR ")
        SQL.Append("FROM ")
        SQL.Append("OPERADOR.TB_AG_VEICULOS")
        SQL.Append(" WHERE ")
        SQL.Append("    ID_TRANSPORTADORA = " & Transportadora & "")
        SQL.Append("    AND ")
        SQL.Append("    PLACA_CAVALO = '" & Cavalo & "'")
        SQL.Append("    AND ")
        SQL.Append("    PLACA_CARRETA = '" & Carreta & "'")
        SQL.Append("    AND ")
        SQL.Append("    (PLACA_REBOQUE = '" & Reboque & "'" & Complemento & ")")

        Dim ds As New DataTable
        ds = Banco.List(String.Format(SQL.ToString()))

        If ds IsNot Nothing Then
            Veic.ID = ds.Rows(0)("AUTONUM").ToString()
            Veic.Tara = ds.Rows(0)("TARA").ToString()
            Veic.Chassi = ds.Rows(0)("CHASSI").ToString()
            Veic.Renavam = ds.Rows(0)("RENAVAM").ToString()
            Veic.Tipo = ds.Rows(0)("ID_TIPO_CAMINHAO").ToString()
            Veic.Modelo = ds.Rows(0)("MODELO").ToString()
            Veic.Cor = ds.Rows(0)("COR").ToString()
        End If

        Return Veic

    End Function

    Public Function ConsultarTipos() As DataTable
        Return Banco.List("SELECT AUTONUM,DESCR FROM OPERADOR.TB_TIPOS_CAMINHAO ORDER BY DESCR ASC")
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

    ''' <summary>
    ''' Consulta todas as placas de cavalo, independente do veículo ser bitrem ou não
    ''' </summary>
    ''' <param name="ID_Transportadora"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarCavalos(ByVal ID_Transportadora As Integer) As List(Of String)
        Return MontarLista(String.Format("SELECT DISTINCT(PLACA_CAVALO) FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = {0}", ID_Transportadora), "PLACA_CAVALO")
    End Function

    ''' <summary>
    ''' Consulta as placas de cavalos de veículos de acordo com a booleana bitrem
    ''' </summary>
    ''' <param name="ID_Transportadora"></param>
    ''' <param name="Bitrem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarCavalos(ByVal ID_Transportadora As Integer, ByVal Bitrem As Boolean) As List(Of String)

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT(PLACA_CAVALO) ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS ")
        SQL.Append("WHERE ")
        SQL.Append("    ID_TRANSPORTADORA = {0} ")
        SQL.Append("AND ")
        If Bitrem Then
            SQL.Append("(PLACA_REBOQUE IS NOT NULL AND PLACA_REBOQUE <> '___-____')")
        Else
            SQL.Append("(PLACA_REBOQUE IS NULL OR PLACA_REBOQUE = '___-____')")
        End If

        Return MontarLista(SQL.ToString, "PLACA_CAVALO")

    End Function

    ''' <summary>
    ''' Consulta as placas de carretas de veículos independentes de serem bitrens ou não
    ''' </summary>
    ''' <param name="Cavalo"></param>
    ''' <param name="ID_Transportadora"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarCarretas(ByVal Cavalo As String, ByVal ID_Transportadora As Integer) As DataTable
        Return Banco.List(String.Format("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND ID_TRANSPORTADORA = {1}", Cavalo, ID_Transportadora))
    End Function

    ''' <summary>
    ''' Consulta as placas de carretas de veículos de acordo com a booleana bitrem
    ''' </summary>
    ''' <param name="Cavalo"></param>
    ''' <param name="ID_Transportadora"></param>
    ''' <param name="Bitrem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarCarretas(ByVal Cavalo As String, ByVal ID_Transportadora As Integer, ByVal Bitrem As Boolean) As DataTable

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT(PLACA_CARRETA) ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS ")
        SQL.Append("WHERE ")
        SQL.Append("    PLACA_CAVALO = '{0}' ")
        SQL.Append("AND ")
        SQL.Append("    ID_TRANSPORTADORA = {1} ")
        SQL.Append("AND ")
        If Bitrem Then
            SQL.Append("(PLACA_REBOQUE IS NOT NULL AND PLACA_REBOQUE <> '___-____')")
        Else
            SQL.Append("(PLACA_REBOQUE IS NULL OR PLACA_REBOQUE = '___-____')")
        End If

        Return Banco.List(SQL.ToString())

    End Function

    Public Function ConsultarReboques(ByVal Cavalo As String, ByVal Carreta As String, ByVal ID_Transportadora As Integer, ByVal SoBitrem As Boolean) As DataTable

        Dim SQLBitrem As String

        If SoBitrem Then
            SQLBitrem = " AND PLACA_REBOQUE <> '___-____'"
        Else
            SQLBitrem = ""
        End If

        Return Banco.List(String.Format("SELECT DISTINCT PLACA_REBOQUE FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND (PLACA_REBOQUE IS NOT NULL" & SoBitrem & ") AND ID_TRANSPORTADORA = {2}", Cavalo, Carreta, ID_Transportadora))

    End Function

    Public Function ConsultarPlacasIguais(ByVal Cavalo As String, ByVal Carreta As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As Integer = 0) As Boolean

        Dim SQL As String = ""

        If ID = 0 Then
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND PLACA_REBOQUE = '{2}' AND ID_TRANSPORTADORA = {3}", Cavalo, Carreta, Reboque, ID_Transportadora)
        Else
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' PLACA_REBOQUE = '{2}' AND ID_TRANSPORTADORA = {3} AND AUTONUM <> {4}", Cavalo, Carreta, Reboque, ID_Transportadora, ID)
        End If

        Dim retorno As String = ""
        retorno = Banco.ExecuteScalar(SQL)

        If retorno <> "" Then
            Return True
        End If

        Return False

    End Function

    Public Function ObterID(ByVal VeiculoOBJ As Veiculo) As Integer

        Dim Complemento As String

        If VeiculoOBJ.Reboque = "___-____" Or VeiculoOBJ.Reboque = "" Then
            Complemento = " OR PLACA_REBOQUE IS NULL"
            If VeiculoOBJ.Reboque = "" Then
                VeiculoOBJ.Reboque = "___-____"
            End If
        Else
            Complemento = ""
        End If

        Return Banco.ExecuteScalar(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND (PLACA_REBOQUE = '{2}'" & Complemento & ") AND ID_TRANSPORTADORA = {3}", VeiculoOBJ.Cavalo, VeiculoOBJ.Carreta, VeiculoOBJ.Reboque, VeiculoOBJ.Transportadora.ID))

    End Function

    Public Function ObterCarreta(ByVal Cavalo As String, ByVal ID_Transportadora As String, ByVal Bitrem As Boolean) As String

        Dim SQLBitrem As String

        If Bitrem Then
            SQLBitrem = "(PLACA_REBOQUE IS NOT NULL AND PLACA_REBOQUE <> '___-____')"
        Else
            SQLBitrem = "(PLACA_REBOQUE IS NULL OR PLACA_REBOQUE = '___-____')"
        End If

        Return Banco.ExecuteScalar(String.Format("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND ID_TRANSPORTADORA='{1}' AND {2} ", Cavalo, ID_Transportadora, SQLBitrem))

    End Function

    Public Function ObterReboque(ByVal Cavalo As String, ByVal Carreta As String, ByVal ID_Transportadora As String) As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT")
        SQL.Append("    PLACA_REBOQUE FROM ")
        SQL.Append("OPERADOR.TB_AG_VEICULOS ")
        SQL.Append("WHERE ")
        SQL.Append("    PLACA_CAVALO = '{0}' AND ")
        SQL.Append("    PLACA_CARRETA = '{1}' AND ")
        SQL.Append("    ID_TRANSPORTADORA = '{2}'")
        SQL.Append("    AND ")
        SQL.Append("    (")
        SQL.Append("    PLACA_REBOQUE IS NOT NULL ")
        SQL.Append("    AND ")
        SQL.Append("    PLACA_REBOQUE <> '___-____'")
        SQL.Append("    )")

        Return Banco.ExecuteScalar(String.Format(SQL.ToString(), Cavalo, Carreta, ID_Transportadora))

    End Function

End Class
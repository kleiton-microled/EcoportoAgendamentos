Imports System.Data.OleDb
Public Class Veiculo

    Private _ID As Integer
    Private _Cavalo As String
    Private _Carreta As String
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
            Banco.Executar(String.Format("INSERT INTO OPERADOR.TB_AG_VEICULOS (AUTONUM,ID_TRANSPORTADORA,PLACA_CAVALO,PLACA_CARRETA,CHASSI,RENAVAM,ID_TIPO_CAMINHAO,TARA,MODELO,COR) VALUES ({0},{1},'{2}','{3}','{4}',{5},{6},{7},'{8}','{9}')", "OPERADOR.SEQ_AG_VEICULOS.NEXTVAL", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function Alterar(ByVal Veiculo As Veiculo) As Boolean

        Try
            Banco.Executar(String.Format("UPDATE OPERADOR.TB_AG_VEICULOS SET ID_TRANSPORTADORA={0},PLACA_CAVALO='{1}',PLACA_CARRETA='{2}',CHASSI='{3}',RENAVAM={4},ID_TIPO_CAMINHAO={5},TARA={6},MODELO='{7}',COR='{8}' WHERE AUTONUM={9}", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor, Veiculo.ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function Excluir(ByVal Veiculo As Veiculo) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM OPERADOR.TB_AG_VEICULOS WHERE AUTONUM={0}", Veiculo.ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ValidarExclusao(ByVal Veiculo As Veiculo)

        If Convert.ToInt32(Banco.ExecutaRetorna(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_GD_CONTEINER WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecutaRetorna(String.Format("SELECT COUNT(*) FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If

        If Convert.ToInt32(Banco.ExecutaRetorna(String.Format("SELECT COUNT(*) FROM SGIPA.TB_AG_CS WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID))) > 0 Then
            Return False
        End If
        Return True

        Return False

    End Function

    Public Function ValidarVeiculosIguais(ByVal Veiculo As Veiculo, ByVal Edit As Boolean)

        If Edit Then
            If Convert.ToInt32(Banco.ExecutaRetorna(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND AUTONUM <> {2} AND ID_TRANSPORTADORA={3}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.ID, Veiculo.Transportadora.ID))) = 0 Then
                Return True
            End If
        Else
            If Convert.ToInt32(Banco.ExecutaRetorna(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND ID_TRANSPORTADORA={2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID))) = 0 Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As DataTable
        Return Banco.Consultar(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TRANSPORTADORA,A.MODELO,A.COR,B.RAZAO AS TRANSPORTADORA,C.DESCR AS TIPO FROM OPERADOR.TB_AG_VEICULOS A LEFT JOIN OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM LEFT JOIN OPERADOR.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM WHERE A.ID_TRANSPORTADORA = {0} {1} ORDER BY AUTONUM DESC", ID, Filtro))
    End Function

    Public Function ConsultarPorID(ByVal ID As Integer) As DataTable
        Return Banco.Consultar(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TIPO_CAMINHAO,A.MODELO,A.COR FROM OPERADOR.TB_AG_VEICULOS A WHERE A.AUTONUM = {0}", ID))
    End Function

    Public Function ConsultarTipos() As DataTable
        Return Banco.Consultar("SELECT AUTONUM,DESCR FROM OPERADOR.TB_TIPOS_CAMINHAO ORDER BY DESCR ASC")
    End Function

    Public Function ConsultarCavalos(ByVal ID_Transportadora As Integer) As List(Of String)

        Dim Dt As New DataTable
        Dim ListaCavalos As New List(Of String)

        Dt = Banco.Consultar(String.Format("SELECT DISTINCT(PLACA_CAVALO) FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = {0}", ID_Transportadora))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCavalos.Add(Item("PLACA_CAVALO").ToString())
            Next

            Return ListaCavalos

        End If

        Return Nothing

    End Function

    Public Function ConsultarCarretas(ByVal Cavalo As String, ByVal ID_Transportadora As Integer) As DataTable
        Return Banco.Consultar(String.Format("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND ID_TRANSPORTADORA = {1}", Cavalo, ID_Transportadora))
    End Function

    Public Function ConsultarPlacasIguais(ByVal Cavalo As String, ByVal Carreta As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As Integer = 0) As Boolean

        Dim Resultado As String = String.Empty

        If ID = 0 Then
            Resultado = Banco.ExecutaRetorna(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Cavalo, Carreta, ID_Transportadora))
        Else
            Resultado = Banco.ExecutaRetorna(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2} AND AUTONUM <> {3}", Cavalo, Carreta, ID_Transportadora, ID))
        End If

        If Val(Resultado) > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function ObterID(ByVal VeiculoOBJ As Veiculo) As Integer
        Return Banco.ExecutaRetorna(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", VeiculoOBJ.Cavalo, VeiculoOBJ.Carreta, VeiculoOBJ.Transportadora.ID))
    End Function

End Class
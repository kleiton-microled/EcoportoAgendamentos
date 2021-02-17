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

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("INSERT INTO OPERADOR.TB_AG_VEICULOS (AUTONUM,ID_TRANSPORTADORA,PLACA_CAVALO,PLACA_CARRETA,CHASSI,RENAVAM,ID_TIPO_CAMINHAO,TARA,MODELO,COR) VALUES ({0},{1},'{2}','{3}','{4}',{5},{6},{7},'{8}','{9}')", "OPERADOR.SEQ_AG_VEICULOS.NEXTVAL", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor))
        Else
            SQL.Append(String.Format("INSERT INTO OPERADOR.DBO.TB_AG_VEICULOS (ID_TRANSPORTADORA,PLACA_CAVALO,PLACA_CARRETA,CHASSI,RENAVAM,ID_TIPO_CAMINHAO,TARA,MODELO,COR) VALUES ({0},'{1}','{2}','{3}',{4},{5},{6},'{8}','{9}')", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor))
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

    Public Function Alterar(ByVal Veiculo As Veiculo) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("UPDATE OPERADOR.TB_AG_VEICULOS SET ID_TRANSPORTADORA={0},PLACA_CAVALO='{1}',PLACA_CARRETA='{2}',CHASSI='{3}',RENAVAM={4},ID_TIPO_CAMINHAO={5},TARA={6},MODELO='{7}',COR='{8}' WHERE AUTONUM={9}", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor, Veiculo.ID))
        Else
            SQL.Append(String.Format("UPDATE OPERADOR.DBO.TB_AG_VEICULOS SET ID_TRANSPORTADORA={0},PLACA_CAVALO='{1}',PLACA_CARRETA='{2}',CHASSI='{3}',RENAVAM={4},ID_TIPO_CAMINHAO={5},TARA={6},MODELO='{7}',COR='{8}' WHERE AUTONUM={9}", Veiculo.Transportadora.ID, Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Chassi, Veiculo.Renavam, Veiculo.Tipo, Veiculo.Tara, Veiculo.Modelo, Veiculo.Cor, Veiculo.ID))
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

    Public Function Excluir(ByVal Veiculo As Veiculo) As Boolean

        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append(String.Format("DELETE FROM OPERADOR.TB_AG_VEICULOS WHERE AUTONUM={0}", Veiculo.ID))
        Else
            SQL.Append(String.Format("DELETE FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE AUTONUM={0}", Veiculo.ID))
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

    Public Function ValidarExclusao(ByVal Veiculo As Veiculo)

        If Banco.BancoEmUso = "ORACLE" Then

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_GD_CONTEINER WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then
                Return False
            End If

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM SGIPA.TB_CNTR_BL WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then
                Return False
            End If

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM SGIPA.TB_AG_CS WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then 'Troquei AUTONUM_TRANSPORTE_AGENDA por AUTONUM_TRANSPORTADORA
                Return False
            End If
            Return True

        Else

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then
                Return False
            End If

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM SGIPA.DBO.TB_CNTR_BL WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTE_AGENDA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then
                Return False
            End If

            If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM SGIPA.DBO.TB_AG_CS WHERE AUTONUM_VEICULO = {0} AND AUTONUM_TRANSPORTADORA = {1}", Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value > 0 Then
                Return False
            End If
            Return True

        End If

        Return False

    End Function

    Public Function ValidarVeiculosIguais(ByVal Veiculo As Veiculo, ByVal Edit As Boolean)

        If Banco.BancoEmUso = "ORACLE" Then
            If Edit Then
                If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND AUTONUM <> {2} AND ID_TRANSPORTADORA={3}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value = 0 Then
                    Return True
                End If
            Else
                If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND ID_TRANSPORTADORA={2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID)).Fields(0).Value = 0 Then
                    Return True
                End If
            End If         
        Else
            If Edit Then
                If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND AUTONUM <> {2} AND ID_TRANSPORTADORA={3}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.ID, Veiculo.Transportadora.ID)).Fields(0).Value = 0 Then
                    Return True
                End If
            Else
                If Banco.Conexao.Execute(String.Format("SELECT COUNT(*) FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO='{0}' AND PLACA_CARRETA='{1}' AND ID_TRANSPORTADORA={2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID)).Fields(0).Value = 0 Then
                    Return True
                End If
            End If            
        End If

        Return False

    End Function

    Public Function Consultar(ByVal ID As Integer, Optional ByVal Filtro As String = "") As DataTable

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TRANSPORTADORA,A.MODELO,A.COR,B.RAZAO AS TRANSPORTADORA,C.DESCR AS TIPO FROM OPERADOR.TB_AG_VEICULOS A LEFT JOIN OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM LEFT JOIN OPERADOR.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM WHERE A.ID_TRANSPORTADORA = {0} {1} ORDER BY AUTONUM DESC", ID, Filtro), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TRANSPORTADORA,A.MODELO,A.COR,B.RAZAO AS TRANSPORTADORA,C.DESCR AS TIPO FROM OPERADOR.DBO.TB_AG_VEICULOS A LEFT JOIN OPERADOR.DBO.TB_CAD_TRANSPORTADORAS B ON A.ID_TRANSPORTADORA = B.AUTONUM LEFT JOIN OPERADOR.DBO.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM WHERE A.ID_TRANSPORTADORA = {0} {1} ORDER BY AUTONUM DESC", ID, Filtro), Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter()
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_VEICULOS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarPorID(ByVal ID As Integer) As DataTable

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TIPO_CAMINHAO,A.MODELO,A.COR FROM OPERADOR.TB_AG_VEICULOS A WHERE A.AUTONUM = {0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT A.AUTONUM,A.PLACA_CAVALO,A.PLACA_CARRETA,A.CHASSI,A.RENAVAM,A.TARA,A.ID_TIPO_CAMINHAO,A.MODELO,A.COR FROM OPERADOR.DBO.TB_AG_VEICULOS A WHERE A.AUTONUM = {0}", ID), Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter()
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_VEICULOS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarTipos() As DataTable

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT AUTONUM,DESCR FROM OPERADOR.TB_TIPOS_CAMINHAO ORDER BY DESCR ASC", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT AUTONUM,DESCR FROM OPERADOR.DBO.TB_TIPOS_CAMINHAO ORDER BY DESCR ASC", Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter()
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_TIPOS_CAMINHAO")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarCavalos(ByVal ID_Transportadora As Integer) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim ListaCavalos As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT(PLACA_CAVALO) FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = {0}", ID_Transportadora), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT(PLACA_CAVALO) FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = {0}", ID_Transportadora), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCavalos.Add(Rst.Fields("PLACA_CAVALO").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCavalos

        End If

        Return Nothing

    End Function

    Public Function ConsultarCarretas(ByVal Cavalo As String, ByVal ID_Transportadora As Integer) As DataTable

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND ID_TRANSPORTADORA = {1}", Cavalo, ID_Transportadora), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT PLACA_CARRETA FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND ID_TRANSPORTADORA = {1}", Cavalo, ID_Transportadora), Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter()
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_VEICULOS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarPlacasIguais(ByVal Cavalo As String, ByVal Carreta As String, ByVal ID_Transportadora As Integer, Optional ByVal ID As Integer = 0) As Boolean

        Dim Rst As New ADODB.Recordset

        If ID = 0 Then
            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Cavalo, Carreta, ID_Transportadora), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Cavalo, Carreta, ID_Transportadora), Banco.Conexao, 3, 3)
            End If
        Else
            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2} AND AUTONUM <> {3}", Cavalo, Carreta, ID_Transportadora, ID), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2} AND AUTONUM <> {3}", Cavalo, Carreta, ID_Transportadora, ID), Banco.Conexao, 3, 3)
            End If
        End If

        If Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function ObterID(ByVal VeiculoOBJ As Veiculo) As Integer

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", VeiculoOBJ.Cavalo, VeiculoOBJ.Carreta, VeiculoOBJ.Transportadora.ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", VeiculoOBJ.Cavalo, VeiculoOBJ.Carreta, VeiculoOBJ.Transportadora.ID), Banco.Conexao, 3, 3)
        End If

        Try
            If Not Rst.EOF Then
                Return Rst.Fields("AUTONUM").Value
            End If
        Catch ex As Exception
            Throw New Exception("Erro. Tente novamente." & ex.Message())
        End Try

        Return False
    End Function

End Class
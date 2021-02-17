Imports System.Data.OleDb
Public Class Agendamento

    Private _codigo As String
    Private _motorista As Motorista
    Private _transportadora As Transportadora
    Private _veiculo As Veiculo
    Private _notaFiscal As NotaFiscal
    Private _protocolo As String
    Private _ano_protocolo As String
    Private _doc_transporte As String
    Private _cod_documento As String
    Private _periodo As String
    Private _num_doc_saida As String
    Private _emissao_doc_saida As String
    Private _tipo_doc_saida As String
    Private _serie_doc_saida As String
    Private Patio As String
    Private Flag_VIP As String
    Private Tipo_Doc As String
    Private Data_Max As Date
    Private Data_Ref As Date
    Private Limite As Integer
    Private VIP As String
    Private DTA As String
    Private _VIP As String
    Private _Patio As String
    Private _DTA As String
    Private _FreeTime As String
    Private _Lote As String

    Public Property Codigo() As String
        Get
            Return Me._codigo
        End Get
        Set(ByVal value As String)
            Me._codigo = value
        End Set
    End Property

    Public Property Motorista() As Motorista
        Get
            Return Me._motorista
        End Get
        Set(ByVal value As Motorista)
            Me._motorista = value
        End Set
    End Property

    Public Property Transportadora() As Transportadora
        Get
            Return Me._transportadora
        End Get
        Set(ByVal value As Transportadora)
            Me._transportadora = value
        End Set
    End Property

    Public Property Veiculo() As Veiculo
        Get
            Return Me._veiculo
        End Get
        Set(ByVal value As Veiculo)
            Me._veiculo = value
        End Set
    End Property

    Public Property NotaFiscal() As NotaFiscal
        Get
            Return Me._notaFiscal
        End Get
        Set(ByVal value As NotaFiscal)
            Me._notaFiscal = value
        End Set
    End Property

    Public Property Protocolo() As String
        Get
            Return Me._protocolo
        End Get
        Set(ByVal value As String)
            Me._protocolo = value
        End Set
    End Property

    Public Property Ano_Protocolo() As String
        Get
            Return Me._ano_protocolo
        End Get
        Set(ByVal value As String)
            Me._ano_protocolo = value
        End Set
    End Property

    Public Property Doc_transporte() As String
        Get
            Return Me._doc_transporte
        End Get
        Set(ByVal value As String)
            Me._doc_transporte = value
        End Set
    End Property

    Public Property Cod_documento() As String
        Get
            Return Me._cod_documento
        End Get
        Set(ByVal value As String)
            Me._cod_documento = value
        End Set
    End Property

    Public Property Periodo() As String
        Get
            Return Me._periodo
        End Get
        Set(ByVal value As String)
            Me._periodo = value
        End Set
    End Property

    Public Property NumDocSaida() As String
        Get
            Return Me._num_doc_saida
        End Get
        Set(ByVal value As String)
            Me._num_doc_saida = value
        End Set
    End Property

    Public Property EmissaoDocSaida() As String
        Get
            Return Me._emissao_doc_saida
        End Get
        Set(ByVal value As String)
            Me._emissao_doc_saida = value
        End Set
    End Property

    Public Property TipoDocSaida() As String
        Get
            Return Me._tipo_doc_saida
        End Get
        Set(ByVal value As String)
            Me._tipo_doc_saida = value
        End Set
    End Property

    Public Property SerieDocSaida() As String
        Get
            Return Me._serie_doc_saida
        End Get
        Set(ByVal value As String)
            Me._serie_doc_saida = value
        End Set
    End Property

    Public Property pVIP() As String
        Get
            Return Me._VIP
        End Get
        Set(ByVal value As String)
            Me._VIP = value
        End Set
    End Property

    Public Property pPatio() As String
        Get
            Return Me._Patio
        End Get
        Set(ByVal value As String)
            Me._Patio = value
        End Set
    End Property

    Public Property pDTA() As String
        Get
            Return Me._DTA
        End Get
        Set(ByVal value As String)
            Me._DTA = value
        End Set
    End Property

    Public Property pFreeTime() As String
        Get
            Return Me._FreeTime
        End Get
        Set(ByVal value As String)
            Me._FreeTime = value
        End Set
    End Property

    Public Property Lote() As String
        Get
            Return Me._Lote
        End Get
        Set(ByVal value As String)
            Me._Lote = value
        End Set
    End Property

    ''' <summary>
    ''' Verifica se o Agendamento de Carga Solta realmente pertence à transportadora logada, por questões de segurança
    ''' </summary>
    ''' <param name="IdAgend">Id do Agendamento que se pretende acessar</param>
    ''' <param name="IdTransp">Id da Transportadora que está logada</param>
    ''' <returns>True: Agendamento pertence à transportadora logada, False: agendamento não pertence à transportadora logada</returns>
    ''' <remarks>IdTransp que não é número inteiro deve ser verificado antes de acessar o método</remarks>
    Public Function VerificarAgendTransp(ByVal IdAgend As String, ByVal IdTransp As String) As Boolean
        Dim SQL As New StringBuilder
        Dim IdTranspCSIPA As Integer
        Dim Rst As New ADODB.Recordset

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM_TRANSPORTADORA ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SGIPA.TB_AG_CS ")
        Else
            SQL.Append("    SGIPA.DBO.TB_AG_CS ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = '" & IdAgend & "' ")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            IdTranspCSIPA = Convert.ToInt32(Rst.Fields("AUTONUM_TRANSPORTADORA").Value.ToString())
            If IdTranspCSIPA = IdTransp Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function ExistePreAgendamento(ByVal Lote As String, ByVal IdTransp As String, ByVal TranspEmpresa As Integer) As Int32

        Dim SQL As New StringBuilder
        Dim Rst As New ADODB.Recordset

        SQL.Append("SELECT ")
        SQL.Append("    NVL(MAX(A.AUTONUM),0) AUTONUM FROM SGIPA.TB_AG_CS A ")
        SQL.Append(" INNER JOIN TB_BL B ON A.LOTE = B.AUTONUM  ")
        SQL.Append("WHERE B.ULTIMA_SAIDA IS NULL AND ")
        SQL.Append("    A.LOTE = " & Lote & " AND A.AUTONUM_TRANSPORTADORA = " & IdTransp & " AND NVL(A.AUTONUM_GD_RESERVA,0) = 0 ")

        If TranspEmpresa = 1 Then
            SQL.Append("    AND B.PATIO <> 5 ")
        Else
            SQL.Append("    AND B.PATIO = 5 ")
        End If

        SQL.Append(" AND A.AUTONUM IN (SELECT AUTONUM_AG_CS FROM VW_AGENDA_CS)")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Convert.ToInt32(Rst.Fields("AUTONUM").Value.ToString())
        End If

        Return 0

    End Function

    Public Function ConsultarCNH(ByVal Motorista As Motorista, ByVal Transportadora As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim ListaCNH As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCNH.Add(Rst.Fields("CNH").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCNH

        End If

        Return Nothing

    End Function
    Public Function ConsultarCNHSemHomonimo(ByVal Motorista As Motorista, ByVal Transportadora As String) As List(Of String)
        'Feito para pegar somente o nome TOTALMENTE IGUAL, quando seleciona o CNH do Motorista
        'SEMPRE vai retornar só UM elemento na lista

        Dim Rst As New ADODB.Recordset
        Dim ListaCNH As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE TRIM(NOME) = '{0}' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE TRIM(NOME) LIKE = '{0}' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCNH.Add(Rst.Fields("CNH").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCNH

        End If

        Return Nothing

    End Function
    Public Function ObterMotoristasPesquisa(ByVal pesquisa As String, ByVal Transportadora As String) As List(Of Motorista)

        Dim Rst As New ADODB.Recordset
        Dim ListaMotoristas As New List(Of Motorista)

        Rst.Open($"SELECT AUTONUM as Id, NOME, CNH, CPF, CELULAR, NEXTEL, VALIDADE_CNH as ValidadeCNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE (CNH like '{pesquisa}%' OR REPLACE ( REPLACE ( CPF , '.' , '' ),'-','')  LIKE '{pesquisa}%' or  LTRIM(RTRIM(NOME)) LIKE '{pesquisa.ToUpper()}%') AND ID_TRANSPORTADORA = {Transportadora}", Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaMotoristas.Add(
                    New Motorista() With {
                    .ID = Rst.Fields("Id").Value.ToString(),
                    .Nome = Rst.Fields("Nome").Value.ToString(),
                    .CNH = Rst.Fields("CNH").Value.ToString(),
                    .CPF = Rst.Fields("CPF").Value.ToString(),
                    .Celular = Rst.Fields("Celular").Value.ToString(),
                    .Nextel = Rst.Fields("Nextel").Value.ToString(),
                    .Validade = Rst.Fields("ValidadeCNH").Value.ToString()
                })

                Rst.MoveNext()
            End While

            Return ListaMotoristas

        End If

        Return Nothing

    End Function

    Public Function ConsultarNome(ByVal Motorista As Motorista) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            If Not Motorista.CNH = String.Empty Then
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}' AND ID_TRANSPORTADORA={1}", Motorista.CNH, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
            End If
        Else
            If Not Motorista.CNH = String.Empty Then
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE CNH = '{0}' AND ID_TRANSPORTADORA={1}", Motorista.CNH, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
            End If
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("NOME").Value.ToString()
        End If

        Return String.Empty

    End Function
    Public Function VerificarHomonimos(ByVal NomeMotorista As String, ByVal Transportadora As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT COUNT(*) AS QTDENOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME = '{0}' AND ID_TRANSPORTADORA = {1}", NomeMotorista, Transportadora), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT COUNT(*) AS QTDENOME FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE NOME = '{0}' AND ID_TRANSPORTADORA = {1}", NomeMotorista, Transportadora), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            If Int(Rst.Fields("QTDENOME").Value.ToString) > 1 Then
                Return True
            Else
                Return False
            End If
        End If

        Return False

    End Function


    Public Function ConsultarDadosVeiculo(ByVal Veiculo As Veiculo) As Veiculo

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT TARA,CHASSI FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT TARA,CHASSI FROM OPERADOR.DBO.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            Dim VeiculoOBJ As New Veiculo
            VeiculoOBJ.Tara = Rst.Fields("TARA").Value.ToString()
            VeiculoOBJ.Chassi = Rst.Fields("CHASSI").Value.ToString()
            Return VeiculoOBJ

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCavalo(ByVal Motorista As Motorista) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT DISTINCT ")
            SQL.Append("    A.PLACA_CAVALO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
        Else
            SQL.Append("SELECT DISTINCT ")
            SQL.Append("    A.PLACA_CAVALO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Motorista.Transportadora.ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCavalo.Add(Rst.Fields("PLACA_CAVALO").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCarreta(ByVal Motorista As Motorista) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Motorista.Transportadora.ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCavalo.Add(Rst.Fields("PLACA_CARRETA").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCarretaPorCavalo(ByVal Cavalo As String, ByVal Transportadora As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.PLACA_CAVALO = '{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.ID_TRANSPORTADORA = {1} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.PLACA_CAVALO = '{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.ID_TRANSPORTADORA = {1} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Cavalo, Transportadora), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCavalo.Add(Rst.Fields("PLACA_CARRETA").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarItensCargaSolta(ByVal Lote As String) As DataTable

        Using Adapter As New OleDbDataAdapter

            Dim Rst As New ADODB.Recordset
            Dim SQL As New StringBuilder

            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.BL AS LOTE, ")
            SQL.Append("    A.QUANTIDADE, ")
            SQL.Append("    B.DESCR AS EMBALAGEM, ")
            SQL.Append("    A.MERCADORIA AS PRODUTO, ")
            SQL.Append("    (A.QUANTIDADE_REAL) - NVL((SELECT SUM(QTDE) FROM SGIPA.TB_AG_CS_NF WHERE autonum_cs=A.AUTONUM),0)  AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_CARGA_SOLTA A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.DTE_TB_EMBALAGENS B ON A.EMBALAGEM = B.CODE ")
            SQL.Append("WHERE ")
            SQL.Append("    A.BL = {0} ")
            SQL.Append("AND ")
            SQL.Append("    NVL(A.USUARIO_DDC,0) = 0 ")

            Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)

            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_CARGA_SOLTA")
            Return Ds.Tables(0)

        End Using

    End Function

    Public Function ObterCargaAgendada(ByVal Autonum_Agendamento As String) As DataTable

        Using Adapter As New OleDbDataAdapter

            Dim Rst As New ADODB.Recordset
            Dim SQL As New StringBuilder

            Rst.Open("SELECT 'Qtde: ' || QTDE || ' | Embalagem: ' || C.DESCR || ' | Produto: ' || B.MERCADORIA AS CARGA FROM TB_AG_CS_NF A INNER JOIN TB_CARGA_SOLTA B ON A.AUTONUM_CS = B.AUTONUM INNER JOIN DTE_TB_EMBALAGENS C ON B.EMBALAGEM = C.CODE WHERE A.AUTONUM_AGENDAMENTO = " & Autonum_Agendamento, Banco.Conexao, 3, 3)

            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_CARGA_SOLTA")
            Return Ds.Tables(0)

        End Using

    End Function
    Public Function ConsultarSaldoCS(ByVal CodProduto As String) As Integer
        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT  ")
        SQL.Append("    AUTONUM, MERCADORIA, ")
        SQL.Append("    (A.QUANTIDADE_REAL) - ")
        If Banco.BancoEmUso = "ORACLE" Then
            'SQL.Append(" NVL((SELECT sum(NF.Qtde) FROM SGIPA.TB_AG_CS_NF NF , SGIPA.TB_AG_CS AG WHERE NF.AUTONUM_AGENDAMENTO=AG.AUTONUM AND AG.AUTONUM_TRANSPORTADORA>0 AND AG.AUTONUM_VEICULO>0 and NF.autonum_cs=A.AUTONUM),0)  AS SALDO ")
            SQL.Append("NVL((SELECT SUM(QTDE) FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_CS = A.AUTONUM), 0) AS SALDO")
        Else
            SQL.Append("ISNULL")
            SQL.Append("((SELECT SUM(QTDE) FROM ")
            SQL.Append("SGIPA.DBO.TB_AG_CS_NF")
            SQL.Append("    WHERE ")
            SQL.Append("    AUTONUM_CS = A.AUTONUM),0) AS SALDO")
        End If
        SQL.Append("    FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SGIPA.TB_CARGA_SOLTA A ")
        Else
            SQL.Append("    SGIPA.DBO.TB_CARGA_SOLTA A ")
        End If
        SQL.Append("    WHERE ")
        SQL.Append("    A.AUTONUM = {0}")

        Rst.Open(String.Format(SQL.ToString, CodProduto), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Convert.ToInt32(Rst.Fields("SALDO").Value)
        Else
            Return 0
        End If

    End Function


    Public Function ConsultarPeriodos(ByVal Lote As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    B.PATIO, ")
        SQL.Append("    B.AUTONUM, ")
        SQL.Append("    NVL(P.FLAG_RETIRADA_VIP,0) AS VIP, ")
        SQL.Append("    NVL(B.TIPO_DOCUMENTO,0) AS TIPO_DOC, ")
        SQL.Append("    to_char(nvl(c.dt_fim_periodo+0.99,sysdate+100),'DD/MM/YYYY HH24:MI:SS') AS DATA_MAX ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_CARGA_SOLTA C, ")
        SQL.Append("    SGIPA.TB_BL B, ")
        SQL.Append("    SGIPA.TB_CAD_PARCEIROS P, ")
        SQL.Append("    SGIPA.TB_GR_BL D ")
        SQL.Append("WHERE ")
        SQL.Append("    C.BL=B.AUTONUM ")
        SQL.Append("AND ")
        SQL.Append("    B.IMPORTADOR=P.AUTONUM(+) ")
        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM=D.BL(+) ")
        SQL.Append("AND ")
        SQL.Append("    B.FLAG_ATIVO=1 ")
        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM IN({0}) ")

        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)

        DTA = String.Empty

        While Not Rst.EOF

            Patio = Rst.Fields("PATIO").Value.ToString()
            Lote = Rst.Fields("AUTONUM").Value.ToString()
            Flag_VIP = Rst.Fields("VIP").Value.ToString()

            'Não há nenhuma necessidade de na comparação formatar a Data_Max:
            If (Data_Max = Nothing And Not Rst.Fields("DATA_MAX").Value Is DBNull.Value) Or Not Data_Max = Nothing Then
                '(Data_Max é vazia assim como o registro no BD) OU Data_Max NÃO é vazia
                If Data_Max < Rst.Fields("DATA_MAX").Value Then
                    Data_Max = Rst.Fields("DATA_MAX").Value
                End If
            End If

            If Convert.ToInt32(Rst.Fields("TIPO_DOC").Value.ToString()) > 7 And Convert.ToInt32(Rst.Fields("TIPO_DOC").Value.ToString()) < 13 Then
                DTA = "0"
                Limite = -9999
            Else
                If DTA = String.Empty Then
                    DTA = "1"
                    Limite = 0
                End If
            End If

            Rst.MoveNext()

        End While

        If Data_Max = Nothing Then
            Data_Max = DateTime.Now.AddDays(30)
        End If

        If Rst.State = 1 Then
            Rst.Close()
        End If

        Dim LimiteLate = 0

        Rst.Open("select count(1) TOTAL from TB_CARGA_SOLTA where nvl(cntr,0)=0 and bl = " & Lote, Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Val(Rst.Fields("TOTAL").Value.ToString()) = 0 Then

                If Rst.State = 1 Then
                    Rst.Close()
                End If

                Rst.Open("SELECT MAX(NVL(LIMITE_LATE, 0)) LIMITE_LATE FROM SGIPA.TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

                If Not Rst.EOF Then
                    LimiteLate = Rst.Fields("LIMITE_LATE").Value.ToString()
                End If

            End If
        End If

        If Rst.State = 1 Then
            Rst.Close()
        End If

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DATA_INICIAL_AG FROM SGIPA.VW_DATA_INICIAL_AG WHERE PATIO={0}", Patio), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT OPERADOR.DBO.TO_CHAR(DATA_INICIAL_AG,'DD/MM/YYYY HH24:MI') DATA_INICIAL_AG FROM SGIPA.DBO.VW_DATA_INICIAL_AG WHERE PATIO={0}", Patio), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Data_Ref = Rst.Fields("DATA_INICIAL_AG").Value.ToString()
        Else
            Data_Ref = Now
        End If

        Data_Ref = Data_Ref.AddDays(LimiteLate)

        SQL.Clear()

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM_GD_RESERVA, ")
        SQL.Append("    TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
        SQL.Append("    TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
        SQL.Append("    TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT(B.AUTONUM) FROM SGIPA.TB_AG_CS B WHERE B.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA),'000') AS SALDO, ")
        SQL.Append("    A.FLAG_DTA ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_GD_RESERVA A ")
        SQL.Append("WHERE ")
        SQL.Append("    A.LIMITE_MOVIMENTOS - (SELECT NVL(COUNT(B.AUTONUM),0) FROM SGIPA.TB_AG_CS B WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA) > " & Limite & " ")
        SQL.Append("AND ")
        SQL.Append("    A.PERIODO_INICIAL > TO_DATE('" & Format(Data_Ref, "dd/MM/yyyy HH:mm") & "','DD/MM/YYYY HH24:MI') ")
        SQL.Append("AND ")
        SQL.Append("    A.PERIODO_FINAL <= TO_DATE('" & Data_Max & "', 'DD/MM/YYYY HH24:MI:SS') ")
        SQL.Append("AND ")
        SQL.Append("    A.FLAG_VIP <= " & Flag_VIP & " ")
        SQL.Append("AND ")
        SQL.Append("    A.FLAG_DTA <= " & DTA & " ")
        SQL.Append("AND ")
        SQL.Append("    A.PATIO = " & Patio & " ")
        SQL.Append("AND ")
        SQL.Append("    A.SERVICO_GATE = 'C' ")
        SQL.Append("ORDER BY ")
        SQL.Append("    A.PERIODO_INICIAL ")

        If Rst.State = 1 Then
            Rst.Close()
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_RESERVA")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function VerificarLimiteMovPeriodo(Reserva As String) As Boolean
        'Retorna True: se período é disponível para agendar
        'Retorna False: se período já alcançou o limite de qtde de agendamentos

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim Limite, QtdePeriodo As Integer

        SQL.Append("SELECT ")
        SQL.Append("    LIMITE_MOVIMENTOS ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.")
        If Banco.BancoEmUso <> "ORACLE" Then
            SQL.Append("DBO.")
        End If
        SQL.Append("TB_GD_RESERVA ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM_GD_RESERVA = {0}")

        If Rst.State = 1 Then
            Rst.Close()
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)
        Limite = Rst.Fields("LIMITE_MOVIMENTOS").Value.ToString()

        SQL.Clear()

        'Pesquisa para saber a quantidade de movimentos já cadastradas para tal reserva
        SQL.Append("SELECT ")
        SQL.Append("    COUNT(AUTONUM_GD_RESERVA) AS QTDE ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.")
        If Banco.BancoEmUso <> "ORACLE" Then
            SQL.Append("DBO.")
        End If
        SQL.Append("TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM_GD_RESERVA = {0} ")

        If Rst.State = 1 Then
            Rst.Close()
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)
        QtdePeriodo = Convert.ToInt16(Rst.Fields("QTDE").Value.ToString())

        If Limite > QtdePeriodo Then
            Return True
        Else
            Return False
        End If

    End Function


    ''' <summary>
    ''' Consulta Lotes Disponíveis para um determinado agendamento
    ''' </summary>
    ''' <param name="ID">Autonum do Agendamento de Carga Solta</param>
    ''' <param name="Empresa">Nome da Empresa</param>
    ''' <param name="Lote">Lote do Agendamento (caso seja Consulta para Edição)</param>
    ''' <returns>Lotes Disponíveis para Agendamento</returns>
    ''' <remarks>Se for para edição também lista o lote do agendamento, independente de estar finalizado ou não o Agendamento,
    ''' Se estiver finalizado não tem problema listar os outros, POIS COM EDIT NÃO PODE ALTERAR O LOTE</remarks>
    Public Function ConsultarLotes(ByVal ID As String, ByVal Empresa As Integer, Optional ByVal Lote As String = "-1") As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append(" LOTE, ")
            SQL.Append(" DESCR,  ")
            SQL.Append(" AGENDAR  ")
            SQL.Append(" FROM VW_CS_DISPONIVEL  ")
            SQL.Append(" WHERE ")
            SQL.Append(" TRANSPORTADORA = {0} ")
            SQL.Append("  AND ")
            SQL.Append("  (")
            SQL.Append("  SALDO  > 0")
            If Lote <> "-1" Then
                SQL.Append("  OR LOTE = " & Lote)
            End If
            SQL.Append("  )")

            If Empresa = 1 Then 'ECOPORTO 
                SQL.Append("    AND ")
                SQL.Append("    PATIO <> 5 ")
            Else ' ECOPORTO RA
                SQL.Append("    AND ")
                SQL.Append("    PATIO = 5 ")
            End If

        Rst.Open(String.Format(SQL.ToString(), ID, Empresa), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_CARGA_SOLTA")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarLotesDiponiveis(ByVal ID As String, ByVal Empresa As Integer, Optional ByVal Lote As String = "-1") As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append(" LOTE, ")
        SQL.Append(" DESCR,  ")
        SQL.Append(" AGENDAR  ")
        SQL.Append(" FROM VW_CS_DISPONIVEL  ")
        SQL.Append(" WHERE ")
        SQL.Append(" TRANSPORTADORA = {0} ")
        SQL.Append("  AND ")
        SQL.Append("  (")
        SQL.Append("  SALDO  > 0")
        If Lote <> "-1" Then
            SQL.Append("  OR LOTE = " & Lote)
        End If
        SQL.Append("  )")

        If Empresa = 1 Then 'ECOPORTO 
            SQL.Append("    AND ")
            SQL.Append("    PATIO <> 5 ")
        Else ' ECOPORTO RA
            SQL.Append("    AND ")
            SQL.Append("    PATIO = 5 ")
        End If

        Return String.Format(SQL.ToString(), ID)

    End Function


    Public Function ConsultarLoteDocumentoDoAgendamento(ByVal Lote As String) As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT DISTINCT ")
        SQL.Append("    lote, ")
        SQL.Append("    'Num. Doc: ' ")
        SQL.Append("     || tipo_documento ")
        SQL.Append("     || ' ' ")
        SQL.Append("     || num_documento ")
        SQL.Append("     || ' BL: ' ")
        SQL.Append("     || numero ")
        SQL.Append("     || ' Lote: ' ")
        SQL.Append("     || lote AS descr ")
        SQL.Append("FROM ")
        SQL.Append("    ( ")
        SQL.Append("        SELECT ")
        SQL.Append("            td.descr AS tipo_documento, ")
        SQL.Append("            d.num_documento, ")
        SQL.Append("            TO_CHAR( ")
        SQL.Append("                a.dt_fim_periodo, ")
        SQL.Append("                'DD/MM/YYYY HH24:MI' ")
        SQL.Append("            ) AS data_free_time, ")
        SQL.Append("            d.lote AS lote, ")
        SQL.Append("            c.numero, ")
        SQL.Append("            c.tipo_documento AS tipo_doc ")
        SQL.Append("        FROM ")
        SQL.Append("            sgipa.tb_carga_solta a, ")
        SQL.Append("            sgipa.tb_bl c ")
        SQL.Append("            INNER JOIN operador.tb_patios p ON c.patio = p.autonum, ")
        SQL.Append("            sgipa.tb_av_online d, ")
        SQL.Append("            sgipa.tb_tipos_documentos td ")
        SQL.Append("        WHERE ")
        SQL.Append("                a.bl = c.autonum ")
        SQL.Append("            AND ")
        SQL.Append("                c.autonum = d.lote ")
        SQL.Append("            AND ")
        SQL.Append("                c.tipo_documento = td.code ")
        SQL.Append("            AND ")
        SQL.Append("                c.autonum = " & Lote)
        SQL.Append("        GROUP BY ")
        SQL.Append("            td.descr, ")
        SQL.Append("            d.num_documento, ")
        SQL.Append("            a.dt_fim_periodo, ")
        SQL.Append("            d.lote, ")
        SQL.Append("            c.tipo_documento, ")
        SQL.Append("            c.numero ")
        SQL.Append("    ) ")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Rst.RecordCount > 0 Then
            Return Rst.Fields("descr").Value.ToString()
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Consulta Lote de um determinado Agendamento de Carga Solta
    ''' </summary>
    ''' <param name="ID">Autonum do Agendamento</param>
    ''' <returns>Lote do Agendamento; se não tiver retorna -1</returns>
    ''' <remarks>Procura se o lote existe na tabela TB_AG_CS_NF 
    ''' Caso não exista faz uma 2ª procura na tabela TB_AG_CS
    ''' Caso não encontrou, retorna -1</remarks>
    Function ConsultarLoteAgendamento(ByVal ID As String) As String
        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            SQL.Append("    CNF.LOTE AS LOTE ")
            SQL.Append("FROM SGIPA.TB_AG_CS_NF CNF ")
            SQL.Append("WHERE CNF.AUTONUM_AGENDAMENTO = {0}")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("LOTE").Value.ToString
        Else
            Rst.Close()
            SQL.Clear()
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("SELECT ")
                SQL.Append("    LOTE FROM ")
                SQL.Append("SGIPA.TB_AG_CS ")
                SQL.Append("WHERE ")
                SQL.Append("    AUTONUM = {0} ")
            End If

            Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

            If Not Rst.EOF Then
                Return Rst.Fields("LOTE").Value.ToString
            Else
                Return "-1"
            End If

        End If

    End Function

    ''' <summary>
    ''' Insere ou atualiza lote do Agendamento de Carga Solta
    ''' </summary>
    ''' <param name="Lote">Lote selecionado</param>
    ''' <param name="CodAgendamento">Autonum do Agendamento de Carga Solta</param>
    ''' <returns>True: Se Função foi executada corretamente; False: Se houve erro ao executar função</returns>
    ''' <remarks></remarks>
    Public Function ColocarLoteAgendamento(ByVal Lote As String, ByVal CodAgendamento As String) As Boolean
        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Try
            SQL.Append("UPDATE ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    SGIPA.TB_AG_CS ")
            Else
                SQL.Append("    SGIPA.DBO.TB_AG_CS ")
            End If
            SQL.Append("SET LOTE = {0} ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = {1} ")

            Rst.Open(String.Format(SQL.ToString(), Lote, CodAgendamento), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception("Ocorreu um erro: " & ex.Message)
        End Try

        Return False

    End Function

    Public Function ObterDocumentoLote(ByVal Lote As String) As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    TD.DESCR AS TIPODOC ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SGIPA.TB_BL ")
        Else
            SQL.Append("    SGIPA.DBO.TB_BL ")
        End If
        SQL.Append(" BL ")
        SQL.Append("INNER JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SGIPA.TB_TIPOS_DOCUMENTOS ")
        Else
            SQL.Append("    SGIPA.DBO.TB_TIPOS_DOCUMENTOS ")
        End If
        SQL.Append(" TD ")
        SQL.Append(" ON BL.TIPO_DOCUMENTO = TD.CODE ")
        SQL.Append(" WHERE ")
        SQL.Append("    BL.AUTONUM = {0} ")

        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("TIPODOC").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ConsultarCNH(ByVal ID As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim ListaCNH As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCNH.Add(Rst.Fields("CNH").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCNH

        End If

        Return Nothing

    End Function

    Public Function CriarAgendamento(ByVal TransportadoraOBJ As Transportadora) As String

        Dim Rst As New ADODB.Recordset
        Dim NovoCodigo As String = String.Empty

        Rst.Open("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO IN (SELECT AUTONUM FROM SGIPA.TB_AG_CS WHERE AUTONUM_TRANSPORTADORA=" & TransportadoraOBJ.ID & " AND AUTONUM_VEICULO=0  AND DATA_CAD<SYSDATE-1/72 AND AUTONUM_MOTORISTA = 0 AND AUTONUM_GD_RESERVA = 0 AND LOTE = 0 AND NUM_PROTOCOLO = 0)", Banco.Conexao, 3, 3)
        Rst.Open("DELETE FROM SGIPA.TB_AG_CS WHERE AUTONUM IN(SELECT AG.AUTONUM FROM SGIPA.TB_AG_CS AG WHERE  AG.AUTONUM_TRANSPORTADORA=" & TransportadoraOBJ.ID & " AND AG.AUTONUM_VEICULO=0  AND DATA_CAD<SYSDATE-1/72 AND AG.AUTONUM_MOTORISTA = 0 AND AG.AUTONUM_GD_RESERVA = 0 AND AG.LOTE = 0 AND AG.NUM_PROTOCOLO = 0)", Banco.Conexao, 3, 3)

        Rst.Open("SELECT SGIPA.SEQ_TB_AG_CS.NEXTVAL AS SEQ FROM DUAL", Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            NovoCodigo = Rst.Fields("SEQ").Value.ToString()
            Conexao.Execute("INSERT INTO SGIPA.TB_AG_CS (AUTONUM,AUTONUM_MOTORISTA,AUTONUM_VEICULO,AUTONUM_TRANSPORTADORA,AUTONUM_GD_RESERVA,NUM_PROTOCOLO,ANO_PROTOCOLO,STATUS,IMPRESSO) VALUES (" & NovoCodigo & ",0,0," & TransportadoraOBJ.ID & ",0,0,0,0,0)")
            Return NovoCodigo
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica se um determinado Agendamento está Finalizado
    ''' </summary>
    ''' <param name="ID">Autonum do Agendamento</param>
    ''' <returns>True: Finalizado, False: Não finalizado</returns>
    ''' <remarks></remarks>
    Public Function AgendamentoFinalizado(ByVal ID As Integer) As Boolean
        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(*) AS AGENDAMENTO ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.")
        If Banco.BancoEmUso = "SQLSERVER" Then
            SQL.Append("DBO.")
        End If
        SQL.Append("TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("    NVL(AUTONUM_VEICULO, 0) <> 0 AND NVL(AUTONUM_MOTORISTA, 0) <> 0 AND ")
        SQL.Append("    NVL(AUTONUM_GD_RESERVA, 0) <> 0 AND NVL(LOTE, 0) <> 0 ")
        SQL.Append("    AND ")
        SQL.Append("    AUTONUM = {0} ")

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Convert.ToInt16(Rst.Fields("AGENDAMENTO").Value) = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function InserirNF(ByVal Lote As String, ByVal NotaFiscal As String, ByVal Serie As String, ByVal Emissao As String, ByVal CodigoAgendamento As String, ByVal CodProduto As String, Quantidade As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If NotaFiscal = "" Then
            NotaFiscal = "0" 'Para conseguir vincular lote, para não inserir valor nulo
        End If

        SQL.Append("INSERT INTO SGIPA.TB_AG_CS_NF ")
        SQL.Append("    (")
        SQL.Append("        AUTONUM,")
        SQL.Append("        LOTE,")
        SQL.Append("        NOTAFISCAL,")
        SQL.Append("        SERIE,")
        SQL.Append("        EMISSAO,")
        SQL.Append("        AUTONUM_AGENDAMENTO, ")
        SQL.Append("        AUTONUM_CS, ")
        SQL.Append("        QTDE ")
        SQL.Append("    )")
        SQL.Append("VALUES")
        SQL.Append("    (")
        SQL.Append("        SGIPA.SEQ_AG_CS_NF.NEXTVAL,")
        SQL.Append("        " & Lote & ",")
        SQL.Append("        '" & NotaFiscal & "',")
        SQL.Append("        '" & Serie & "',")
        SQL.Append("        TO_DATE('" & Emissao & "','DD/MM/YY'),")
        SQL.Append("        " & CodigoAgendamento & ", ")
        SQL.Append("        " & CodProduto & ", ")
        SQL.Append("        " & Quantidade & "")
        SQL.Append("    )")

        Try
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    ''' <summary>
    ''' Atualiza uma Nota Fiscal do Agendamento
    ''' </summary>
    Public Function AlterarNF(ByVal Lote As String, ByVal NotaFiscal As String, ByVal Serie As String, ByVal Emissao As String, ByVal CodProduto As String, ByVal Quantidade As String, ByVal AutonumNFiscal As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Try

            SQL.Append("UPDATE ")
            SQL.Append("    SGIPA.TB_AG_CS_NF ")
            SQL.Append("SET ")
            SQL.Append("    LOTE = {0}, ")
            SQL.Append("    NOTAFISCAL = '{1}', ")
            SQL.Append("    SERIE = '{2}', ")
            SQL.Append("    EMISSAO = TO_DATE('{3}', 'DD/MM/YY'), ")
            SQL.Append("    QTDE = {4}, ")
            SQL.Append("    AUTONUM_CS = {5} ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = {6} ")

            Rst.Open(String.Format(SQL.ToString(), Lote, NotaFiscal, Serie, Emissao, Quantidade, CodProduto, AutonumNFiscal), Banco.Conexao, 3, 3)

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return False

    End Function

    '''<summary>
    ''' Consulta dados das Notas Fiscais de um determinado agendamento, inclusive Produtos, embalagens e tipos de documentos
    '''</summary>
    ''' <param name="ID">Autonum do Agendamento CS</param>
    Public Function ConsultarDadosNota(ByVal ID As String) As DataTable
        Dim Rst As New ADODB.Recordset
        Dim SQLBuilder As New StringBuilder
        Dim SQL As String

        If Banco.BancoEmUso = "ORACLE" Then
            SQLBuilder.Append("SELECT   ")
            SQLBuilder.Append("     A.AUTONUM AS AUTONUM, A.LOTE AS LOTE, A.NOTAFISCAL AS NOTAFISCAL,    ")
            SQLBuilder.Append("     A.SERIE AS SERIE, TO_CHAR(A.EMISSAO,'DD/MM/YY') EMISSAO, A.AUTONUM_AGENDAMENTO AS AUTONUMAGENDAMENTO, ")
            SQLBuilder.Append("     A.AUTONUM_CS AS AUTONUM_PRODUTO, ")
            SQLBuilder.Append("     CS.MERCADORIA AS PRODUTO, E.DESCR AS EMBALAGEM, ")
            SQLBuilder.Append("     A.QTDE AS QTDE, ")
            SQLBuilder.Append("     B.PATIO AS PATIO, C.DESCR AS TIPO ")
            SQLBuilder.Append("FROM ")
            SQLBuilder.Append("    SGIPA.TB_AG_CS_NF A ")
            SQLBuilder.Append("LEFT JOIN SGIPA.TB_BL B ")
            SQLBuilder.Append("    ON A.LOTE = B.AUTONUM ")
            SQLBuilder.Append("LEFT JOIN SGIPA.TB_TIPOS_DOCUMENTOS C ")
            SQLBuilder.Append("    ON B.TIPO_DOCUMENTO = C.CODE ")
            SQLBuilder.Append("LEFT JOIN SGIPA.TB_CARGA_SOLTA CS ")
            SQLBuilder.Append("    ON CS.AUTONUM = A.AUTONUM_CS ")
            SQLBuilder.Append("LEFT JOIN SGIPA.DTE_TB_EMBALAGENS E")
            SQLBuilder.Append("    ON E.CODE = CS.EMBALAGEM ")
            SQLBuilder.Append(" WHERE A.AUTONUM_AGENDAMENTO={0}")
        Else
            SQLBuilder.Append("SELECT   ")
            SQLBuilder.Append("     A.AUTONUM AS AUTONUM, A.LOTE AS LOTE, A.NOTAFISCAL AS NOTAFISCAL,    ")
            SQLBuilder.Append("     A.SERIE AS SERIE, TO_CHAR(A.EMISSAO,'DD/MM/YY') EMISSAO, A.AUTONUM_AGENDAMENTO AS AUTONUMAGENDAMENTO, ")
            SQLBuilder.Append("     A.AUTONUM_CS AS AUTONUM_PRODUTO, ")
            SQLBuilder.Append("     CS.MERCADORIA AS PRODUTO, E.DESCR AS EMBALAGEM, ")
            SQLBuilder.Append("A.QTDE AS QTDE, ")
            SQLBuilder.Append("     B.PATIO AS PATIO, C.DESCR AS TIPO ")
            SQLBuilder.Append("FROM ")
            SQLBuilder.Append("    SGIPA.DBO.TB_AG_CS_NF A ")
            SQLBuilder.Append("LEFT JOIN SGIPA.DBO.TB_BL B ")
            SQLBuilder.Append("    ON A.LOTE = B.AUTONUM ")
            SQLBuilder.Append("LEFT JOIN SGIPA.DBO.TB_TIPOS_DOCUMENTOS C ")
            SQLBuilder.Append("ON B.TIPO_DOCUMENTO = C.CODE ")
            SQLBuilder.Append("LEFT JOIN SGIPA.DBO.TB_CARGA_SOLTA CS ")
            SQLBuilder.Append("    ON CS.AUTONUM = A.AUTONUM_CS ")
            SQLBuilder.Append("LEFT JOIN SGIPA.DBO.DTE_TB_EMBALAGENS E ")
            SQLBuilder.Append("    ON E.CODE = CS.EMBALAGEM ")
            SQLBuilder.Append(" WHERE A.AUTONUM_AGENDAMENTO={0}")
        End If
        SQL = String.Format(SQLBuilder.ToString(), ID)

        Using Adapter As New OleDbDataAdapter

            Rst.Open(SQL, Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_CS_NF")
            Return Ds.Tables(0)

        End Using

    End Function

    Public Function Consultar(ByVal ID As String, ByVal TranspEmpresa As Integer, Optional ByVal Filtro As String = "") As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    CS.AUTONUM_AG_CS, CS.PROTOCOLO, CS.NOME_MOTORISTA,")
        SQL.Append("    CS.CNH, CS.PLACA_CAVALO, CS.PLACA_CARRETA,")
        SQL.Append("    CS.PERIODO, CS.IMPRESSO, CS.STATUS,")
        SQL.Append("    CS.NUM_DOC_SAIDA, CS.NUMERO_BL, CS.NUM_DOCUMENTO, CS.DESCR_DOCUMENTO,")
        SQL.Append("    CS.DT_FREE_TIME, CS.DT_CHEGADA, CS.COD_TRANSPORTADORA,")
        SQL.Append("    CS.EMISSAO_DOC_SAIDA, CS.SERIE_DOC_SAIDA, CS.TIPO_DOC_SAIDA,")
        SQL.Append("    CS.COR, CS.MODELO, CS.RENAVAM, CS.LOTE, CS.AUTONUM_GD_RESERVA")
        SQL.Append(" FROM SGIPA.VW_AGENDA_CS CS ")
        SQL.Append("INNER JOIN SGIPA.TB_BL B ON CS.LOTE=B.AUTONUM")
        SQL.Append("    WHERE ")
        SQL.Append("    CS.COD_TRANSPORTADORA = {0} ")

        If TranspEmpresa = 1 Then
            SQL.Append("    AND B.PATIO <> 5 ")
        Else
            SQL.Append("    AND B.PATIO = 5 ")
        End If

        'Não mostra registros DDC
        SQL.Append("    AND NVL(CS.USUARIO_DDC,0) = 0 ")

        SQL.Append(Filtro)
        SQL.Append(" GROUP BY ")
        SQL.Append("    CS.AUTONUM_AG_CS, CS.PROTOCOLO, CS.NOME_MOTORISTA,")
        SQL.Append("    CS.CNH, CS.PLACA_CAVALO, CS.PLACA_CARRETA,")
        SQL.Append("    CS.PERIODO, CS.IMPRESSO, CS.STATUS,")
        SQL.Append("    CS.NUM_DOC_SAIDA, CS.NUMERO_BL, CS.NUM_DOCUMENTO, CS.DESCR_DOCUMENTO,")
        SQL.Append("    CS.DT_FREE_TIME, CS.DT_CHEGADA, CS.COD_TRANSPORTADORA,")
        SQL.Append("    CS.EMISSAO_DOC_SAIDA, CS.SERIE_DOC_SAIDA, CS.TIPO_DOC_SAIDA,")
        SQL.Append("    CS.COR, CS.MODELO, CS.RENAVAM, CS.LOTE, CS.AUTONUM_GD_RESERVA")
        SQL.Append(" ORDER BY ")
        SQL.Append("    CS.PROTOCOLO DESC")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "SGIPA.DBO.VW_AGENDA_CS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarDadosAgendamento(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
            SQL.Append("    A.NUM_DOC_SAIDA, ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.SERIE_DOC_SAIDA, ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    B.NOME, ")
            SQL.Append("    B.CNH, ")
            SQL.Append("    B.AUTONUM AS AUTONUM_MOTORISTA, ")
            SQL.Append("    C.AUTONUM AS AUTONUM_VEICULO, ")
            SQL.Append("    D.AUTONUM_GD_RESERVA AS AUTONUM_PERIODO, ")
            SQL.Append("    A.LOTE, ")
            SQL.Append("    C.PLACA_CAVALO, ")
            SQL.Append("    C.PLACA_CARRETA, ")
            SQL.Append("    C.CHASSI, ")
            SQL.Append("    C.TARA, ")
            SQL.Append("    E.EMAIL_AGENDAMENTO, ")
            SQL.Append("    E.AUTONUM AS TRANSPORTADORA, ")
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_AG_CS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS E ON A.AUTONUM_TRANSPORTADORA = E.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    A.NUM_PROTOCOLO + '/' + A.ANO_PROTOCOLO AS PROTOCOLO, ")
            SQL.Append("    A.NUM_DOC_SAIDA, ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.SERIE_DOC_SAIDA, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    B.NOME, ")
            SQL.Append("    B.CNH, ")
            SQL.Append("    C.PLACA_CAVALO, ")
            SQL.Append("    C.PLACA_CARRETA, ")
            SQL.Append("    C.CHASSI, ")
            SQL.Append("    C.TARA, ")
            SQL.Append("    D.PERIODO_INICIAL + ' - ' +  D.PERIODO_FINAL AS PERIODO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.DBO.TB_AG_CS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_CS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function InserirItem(ByVal AutonumAgendamento As String, ByVal AutonumNotaFiscal As String, ByVal Lote As String, ByVal Qtde As String, ByVal AutonumCS As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Try
                Rst.Open(String.Format("INSERT INTO SGIPA.TB_AG_CS_ITENS (AUTONUM,AUTONUM_AGENDAMENTO,AUTONUM_NF,LOTE,QTDE,AUTONUM_CS) VALUES (SGIPA.SEQ_AG_CS_ITENS.NEXTVAL,{0},{1},{2},{3},{4})", AutonumAgendamento, AutonumNotaFiscal, Lote, Qtde, AutonumCS), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Else
            Try
                Rst.Open(String.Format("INSERT INTO SGIPA.TB_AG_CS_ITENS (AUTONUM_AGENDAMENTO,AUTONUM_NF,LOTE,QTDE,AUTONUM_CS) VALUES ({0},{1},{2},{3},{4})", AutonumAgendamento, AutonumNotaFiscal, Lote, Qtde, AutonumCS), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If

        Return False

    End Function

    Public Function ConsultarItens(ByVal Codigo As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.LOTE, ")
            SQL.Append("    A.QTDE, ")
            SQL.Append("    B.MERCADORIA AS PRODUTO, ")
            SQL.Append("    C.DESCR AS EMBALAGEM, ")
            SQL.Append("    B.AUTONUM AS AUTONUM_BCG, ")
            SQL.Append("    E.NOTAFISCAL AS AUTONUM_NF ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_AG_CS_ITENS A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_CARGA_SOLTA B ON A.AUTONUM_CS = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.DTE_TB_EMBALAGENS C ON B.EMBALAGEM = C.CODE ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_AG_CS D ON A.AUTONUM_AGENDAMENTO = D.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("     SGIPA.TB_AG_CS_NF E ON A.AUTONUM_NF = E.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_AGENDAMENTO={0}")
        Else

        End If

        Rst.Open(String.Format(SQL.ToString(), Codigo), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter

            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_CS_ITENS")
            Return Ds.Tables(0)

        End Using

    End Function

    Public Shared Function ObterCodigoMotorista(ByVal Motorista As Motorista) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As String

        If Banco.BancoEmUso = "ORACLE" Then
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH='{0}' AND ID_TRANSPORTADORA = {1}", Motorista.CNH, Motorista.Transportadora.ID)
        Else
            SQL = String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE CNH='{0}' AND ID_TRANSPORTADORA = {1}", Motorista.CNH, Motorista.Transportadora.ID)
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUM").Value.ToString()
        End If

        Return False

    End Function
    Public Shared Function ObterProtocolo(ByVal Autonum As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As String

        If Banco.BancoEmUso = "ORACLE" Then
            SQL = ("Select NVL(NUM_PROTOCOLO,0) AS PROTOCOLO FROM SGIPA.TB_AG_CS   WHERE  AUTONUM=" & Autonum & "")
        Else
            SQL = ("Select ISNULL(NUM_PROTOCOLO,0) AS PROTOCOLO FROM SGIPA.TB_AG_CS   WHERE  AUTONUM=" & Autonum & "")
        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("PROTOCOLO").Value.ToString()
        End If

        Return 0

    End Function

    Public Function Agendar(ByVal AgendamentoOBJ As Agendamento) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Documento As New Documento

        Dim CodigoMotorista As Integer = ObterCodigoMotorista(AgendamentoOBJ.Motorista)

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("UPDATE SGIPA.TB_AG_CS ")
            SQL.Append("    SET ")
            SQL.Append("        AUTONUM_MOTORISTA=" & CodigoMotorista & ", ")
            SQL.Append("        EMISSAO_DOC_SAIDA=TO_DATE('" & AgendamentoOBJ.EmissaoDocSaida & "','DD/MM/YY'), ")
            SQL.Append("        SERIE_DOC_SAIDA='" & AgendamentoOBJ.SerieDocSaida & "', ")
            SQL.Append("        TIPO_DOC_SAIDA='" & AgendamentoOBJ.TipoDocSaida & "', ")
            SQL.Append("        NUM_DOC_SAIDA=" & AgendamentoOBJ.NumDocSaida & ", ")
            SQL.Append("        LOTE=" & AgendamentoOBJ.Lote & ", ")
            SQL.Append("        XML_CODESP=0, ")

            Dim RstLib As New ADODB.Recordset
            RstLib.Open("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

            If Not RstLib.EOF Then
                If RstLib.Fields("FLAG_BLOQUEIA_PROTOCOLO").Value.ToString() = "1" Then
                    SQL.Append("  IMPRESSO = 2, ")
                Else
                    SQL.Append("  IMPRESSO = 0, ")
                End If
            End If

            SQL.Append("        STATUS=0, ")
            SQL.Append("        NUM_PROTOCOLO=SGIPA.SEQ_AG_CS_PROT_" & Now.Year & ".NEXTVAL, ")
            SQL.Append("        ANO_PROTOCOLO=" & Now.Year & ", ")
            SQL.Append("        AUTONUM_GD_RESERVA=" & AgendamentoOBJ.Periodo & ", ")
            SQL.Append("        AUTONUM_TRANSPORTADORA=" & AgendamentoOBJ.Transportadora.ID & ", ")
            SQL.Append("        AUTONUM_VEICULO=" & AgendamentoOBJ.Veiculo.ID & " ")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM=" & AgendamentoOBJ.Codigo & "")

        Else
            SQL.Append("UPDATE SGIPA.DBO.TB_AG_CS ")
            SQL.Append("    SET ")
            SQL.Append("        AUTONUM_MOTORISTA=" & CodigoMotorista & ", ")
            SQL.Append("        EMISSAO_DOC_SAIDA=OPERADOR.DBO.TO_DATE('" & AgendamentoOBJ.EmissaoDocSaida & "','DD/MM/YY'), ")
            SQL.Append("        SERIE_DOC_SAIDA='" & AgendamentoOBJ.SerieDocSaida & "', ")
            SQL.Append("        TIPO_DOC_SAIDA='" & AgendamentoOBJ.TipoDocSaida & "', ")
            SQL.Append("        NUM_DOC_SAIDA=" & AgendamentoOBJ.NumDocSaida & ", ")
            SQL.Append("        LOTE=" & AgendamentoOBJ.Lote & ", ")
            SQL.Append("        IMPRESSO=0, ")
            SQL.Append("        STATUS=0, ")
            SQL.Append("        NUM_PROTOCOLO=SGIPA.SEQ_AG_CS_PROT_" & Now.Year & ".NEXTVAL, ")
            SQL.Append("        ANO_PROTOCOLO=" & Now.Year & ", ")
            SQL.Append("        AUTONUM_GD_RESERVA=" & AgendamentoOBJ.Periodo & ", ")
            SQL.Append("        AUTONUM_TRANSPORTADORA=" & AgendamentoOBJ.Transportadora.ID & ", ")
            SQL.Append("        AUTONUM_VEICULO=" & AgendamentoOBJ.Veiculo.ID & ", ")
            SQL.Append("        XML_CODESP=0 ")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM=" & AgendamentoOBJ.Codigo & "")
        End If

        Try
            Banco.Conexao.Execute(SQL.ToString())
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function AlterarAgendamento(ByVal AgendamentoOBJ As Agendamento, ByVal TeveAlteracoesDeDocumentos As Boolean) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim CodigoMotorista As Integer = ObterCodigoMotorista(AgendamentoOBJ.Motorista)

        SQL.Append("UPDATE SGIPA.TB_AG_CS ")
        SQL.Append("    SET ")

        If TeveAlteracoesDeDocumentos Then
            SQL.Append("  IMPRESSO = 2, ")
        Else
            Dim RstAux As New ADODB.Recordset
            RstAux.Open("SELECT NVL(FLAG_LIBERADO,0) FLAG_LIBERADO FROM TB_AG_CS WHERE AUTONUM = " & AgendamentoOBJ.Codigo, Banco.Conexao, 3, 3)

            If RstAux.RecordCount > 0 Then
                If Val(RstAux.Fields("FLAG_LIBERADO").Value.ToString()) = 0 Then
                    SQL.Append("  IMPRESSO = 2, ")
                Else
                    SQL.Append("  IMPRESSO = 0, ")
                End If
            End If

        End If

        SQL.Append("        XML_CODESP=0, ")
        SQL.Append("        AUTONUM_MOTORISTA=" & CodigoMotorista & ", ")
        SQL.Append("        EMISSAO_DOC_SAIDA=TO_DATE('" & AgendamentoOBJ.EmissaoDocSaida & "','DD/MM/YY'), ")
        SQL.Append("        SERIE_DOC_SAIDA='" & AgendamentoOBJ.SerieDocSaida & "', ")
        SQL.Append("        TIPO_DOC_SAIDA='" & AgendamentoOBJ.TipoDocSaida & "', ")
        SQL.Append("        NUM_DOC_SAIDA=" & AgendamentoOBJ.NumDocSaida & ", ")
        SQL.Append("        AUTONUM_GD_RESERVA=" & AgendamentoOBJ.Periodo & ", ")
        SQL.Append("        AUTONUM_VEICULO=" & AgendamentoOBJ.Veiculo.ID & " ")

        Dim Protocolo As Integer = ObterProtocolo(AgendamentoOBJ.Codigo)

        If Protocolo = 0 Then
            SQL.Append("        ,NUM_PROTOCOLO=SGIPA.SEQ_AG_CS_PROT_" & Now.Year & ".NEXTVAL, ")
            SQL.Append("        ANO_PROTOCOLO=" & Now.Year & " ")
        End If
        SQL.Append("    WHERE ")
        SQL.Append("        AUTONUM=" & AgendamentoOBJ.Codigo & "")

        Try
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function CancelarAgendamento(ByVal ID As Integer) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim Result As Object = Nothing

        Try

            Banco.Conexao.Execute(String.Format("DELETE FROM SGIPA.TB_AG_CS WHERE AUTONUM={0}", ID), Result)

            If Convert.ToInt32(Result) > 0 Then
                Banco.Conexao.Execute(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO={0}", ID))
            End If

            Dim RstExcluir As New ADODB.Recordset
            Dim ws As New WsSharepoint.WsIccSharepoint()

            Try
                RstExcluir.Open("SELECT A.AUTONUM, A.LOTE FROM TB_AV_IMAGEM A WHERE A.AUTONUM_AGENDAMENTO = " & ID, Banco.Conexao, 3, 3)

                If RstExcluir.EOF = False Then
                    While RstExcluir.EOF = False
                        ws.ExcluirImagemDocAverbacaoPorLoteEautonum(Val(RstExcluir.Fields("LOTE").Value.ToString()), Val(RstExcluir.Fields("AUTONUM").Value.ToString()))
                        RstExcluir.MoveNext()
                    End While
                End If
            Catch ex As Exception

            End Try

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function AgendamentoComRegistro(ByVal ID As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Try
            Rst.Open("SELECT COUNT(1) AS TOTAL FROM SGIPA.TB_REGISTRO_SAIDA_CS A INNER JOIN TB_CARGA_SOLTA B ON A.CS = B.AUTONUM INNER JOIN TB_AG_CS C ON B.CNTR = C.CNTR WHERE NVL(C.FLAG_DDC,0) = 0 AND C.AUTONUM = " & ID, Banco.Conexao, 3, 3)
        Catch ex As Exception
            Return False
        End Try

        If Not Rst.EOF Then
            Return Rst.Fields("TOTAL").Value.ToString()
        End If

    End Function

    Public Function AgendamentoLiberado(ByVal ID As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Try
            Rst.Open("SELECT NVL(MAX(B.FLAG_REGISTRO_LIB),0) FLAG_REGISTRO_LIB FROM TB_REGISTRO_SAIDA_CS A INNER JOIN TB_ORDEM_CARREGAMENTO B ON A.ORDEM_CARREG = B.AUTONUM INNER JOIN TB_CARGA_SOLTA C ON A.CS = C.AUTONUM INNER JOIN TB_AG_CS D ON C.AUTONUM = D.LOTE WHERE D.AUTONUM = " & ID, Banco.Conexao, 3, 3)
        Catch ex As Exception
            Return False
        End Try

        If Not Rst.EOF Then
            Return Rst.Fields("FLAG_REGISTRO_LIB").Value.ToString()
        End If

    End Function

    Public Function ExcluirItens(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.TB_AG_CS_ITENS WHERE AUTONUM_AGENDAMENTO={0}", ID), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        Else
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.DBO.TB_AG_CS_ITENS WHERE AUTONUM_AGENDAMENTO={0}", ID), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If

        Return False

    End Function
    Public Function ExcluirNF(ByVal IDNF As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM = {0}", IDNF), Banco.Conexao, 3, 3)
                If Banco.LinhasAfetadas Then
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Else
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.DBO.TB_AG_CS_NF WHERE AUTONUM = {0}", IDNF), Banco.Conexao, 3, 3)
                If Banco.LinhasAfetadas Then
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If

        Return False

    End Function

    Public Function ExcluirNotas(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO={0}", ID), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Else
            Try
                Rst.Open(String.Format("DELETE FROM SGIPA.DBO.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO={0}", ID), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If

        Return False

    End Function

    Public Function GerarProtocolo(ByVal ID As String, ByVal TiaEmpresa As Integer) As StringBuilder

        Dim Rst1 As New ADODB.Recordset
        Dim Rst2 As New ADODB.Recordset
        Dim Rst3 As New ADODB.Recordset

        Dim SQL As New StringBuilder

        Dim Estrutura As New StringBuilder
        Dim Tabela1 As New StringBuilder
        Dim Tabela2 As New StringBuilder
        Dim Tabela3 As New StringBuilder
        Dim Tabela4 As New StringBuilder
        Dim TabelaItem As New StringBuilder

        Dim Protocolos As String() = ID.Split(",")

        Dim Empresa As New Empresa

        TiaEmpresa = 1

        For Each Item In Protocolos

            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.AUTONUM_MOTORISTA, ")
                SQL.Append("    A.AUTONUM_TRANSPORTADORA, ")
                SQL.Append("    A.NUM_PROTOCOLO, ")
                SQL.Append("    A.ANO_PROTOCOLO, ")
                SQL.Append("    A.AUTONUM_GD_RESERVA, ")
                SQL.Append("    A.STATUS, ")
                SQL.Append("    A.IMPRESSO, ")
                SQL.Append("    A.AUTONUM_VEICULO, ")
                SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YYYY') EMISSAO_DOC_SAIDA, ")
                SQL.Append("    A.SERIE_DOC_SAIDA, ")
                SQL.Append("    A.TIPO_DOC_SAIDA, ")
                SQL.Append("    A.NUM_DOC_SAIDA, ")
                SQL.Append("    B.TARA, ")
                SQL.Append("    B.CHASSI, ")
                SQL.Append("    C.NOME, ")
                SQL.Append("    C.CNH, ")
                SQL.Append("    C.CPF, ")
                SQL.Append("    C.RG, ")
                SQL.Append("    (SELECT MAX(M1.ORGAO) AS ORGAO FROM OPERADOR.TB_MOTORISTAS M1 WHERE C.CNH = M1.CNH ) AS ORGAO, ")
                SQL.Append("    (SELECT MAX(TO_CHAR(M2.EMISSAO_RG,'DD/MM/YYYY')) AS EMISSAO_RG FROM OPERADOR.TB_MOTORISTAS M2 WHERE C.CNH = M2.CNH ) AS EMISSAO_RG, ")
                SQL.Append("    C.NEXTEL, ")
                SQL.Append("    B.PLACA_CAVALO, ")
                SQL.Append("    B.PLACA_CARRETA, ")
                SQL.Append("    B.MODELO, ")
                SQL.Append("    B.COR, ")
                SQL.Append("    TO_CHAR(D.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') AS PERIODO, ")
                SQL.Append("    E.RAZAO, ")
                SQL.Append("    D.PATIO, ")
                SQL.Append("    NVL(P.RAZAO, '-') AS IMPORTADOR, ")
                SQL.Append("    CS.LOCAL_ISO9000 AS LOCALIZACAO, ")
                SQL.Append("    CS.TERMO_AVARIA AS TERMO_AVARIA ")
                SQL.Append("FROM ")
                SQL.Append("    SGIPA.TB_AG_CS A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_AG_VEICULOS B ON A.AUTONUM_VEICULO = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM_MOTORISTA = C.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS E ON A.AUTONUM_TRANSPORTADORA = E.AUTONUM ")
                SQL.Append("INNER JOIN ")
                'SQL.Append("    SGIPA.TB_AG_CS_ITENS CSI ON CSI.AUTONUM_AGENDAMENTO = A.AUTONUM ")
                SQL.Append("    SGIPA.TB_AG_CS_NF NF ON NF.AUTONUM_AGENDAMENTO = A.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    SGIPA.TB_CARGA_SOLTA CS ON NF.AUTONUM_CS = CS.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    SGIPA.TB_BL BL ON CS.BL = BL.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    SGIPA.TB_CAD_PARCEIROS P ON BL.IMPORTADOR = P.AUTONUM ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM IN ({0}) ")
            Else
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.AUTONUM_MOTORISTA, ")
                SQL.Append("    A.AUTONUM_TRANSPORTADORA, ")
                SQL.Append("    A.NUM_PROTOCOLO, ")
                SQL.Append("    A.ANO_PROTOCOLO, ")
                SQL.Append("    A.AUTONUM_GD_RESERVA, ")
                SQL.Append("    A.STATUS, ")
                SQL.Append("    A.IMPRESSO, ")
                SQL.Append("    A.AUTONUM_VEICULO, ")
                SQL.Append("    A.EMISSAO_DOC_SAIDA, ")
                SQL.Append("    A.SERIE_DOC_SAIDA, ")
                SQL.Append("    A.TIPO_DOC_SAIDA, ")
                SQL.Append("    A.NUM_DOC_SAIDA, ")
                SQL.Append("    B.TARA, ")
                SQL.Append("    B.CHASSI, ")
                SQL.Append("    C.NOME, ")
                SQL.Append("    C.CNH, ")
                SQL.Append("    C.CPF, ")
                SQL.Append("    C.RG, ")
                SQL.Append("    (SELECT MAX(M1.ORGAO) AS ORGAO FROM OPERADOR.TB_MOTORISTAS M1 WHERE C.CNH = M1.CNH ) AS ORGAO, ")
                SQL.Append("    (SELECT MAX(TO_CHAR(M2.EMISSAO_RG,'DD/MM/YYYY')) AS EMISSAO_RG FROM OPERADOR.TB_MOTORISTAS M2 WHERE C.CNH = M2.CNH ) AS EMISSAO_RG, ")
                SQL.Append("    C.NEXTEL, ")
                SQL.Append("    B.PLACA_CAVALO, ")
                SQL.Append("    B.PLACA_CARRETA, ")
                SQL.Append("    B.MODELO, ")
                SQL.Append("    B.COR, ")
                SQL.Append("    D.PERIODO_INICIAL + ' - ' + D.PERIODO_FINAL AS PERIODO, ")
                SQL.Append("    E.RAZAO, ")
                SQL.Append("    D.PATIO, ")
                SQL.Append("    ISNULL(P.RAZAO, '-') AS IMPORTADOR ")
                SQL.Append("FROM ")
                SQL.Append("    SGIPA.DBO.TB_AG_CS A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS B ON A.AUTONUM_VEICULO = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS C ON A.AUTONUM_MOTORISTA = C.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS E ON A.AUTONUM_TRANSPORTADORA = E.AUTONUM ")
                SQL.Append("INNER JOIN ")
                'SQL.Append("    SGIPA.TB_AG_CS_ITENS CSI ON CSI.AUTONUM_AGENDAMENTO = A.AUTONUM ")
                SQL.Append("    SGIPA.TB_AG_CS_NF NF ON NF.AUTONUM_AGENDAMENTO = A.AUTONUM ")
                SQL.Append("INNER JOIN ")
                'SQL.Append("    SGIPA.TB_CARGA_SOLTA CS ON CSI.AUTONUM_CS = CS.AUTONUM ")
                SQL.Append("    SGIPA.TB_CARGA_SOLTA CS ON NF.AUTONUM_CS = CS.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    SGIPA.TB_BL BL ON CS.BL = BL.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    SGIPA.TB_CAD_PARCEIROS P ON BL.IMPORTADOR = P.AUTONUM ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM IN ({0}) ")
            End If

            If Rst1.State = 1 Then
                Rst1.Close()
            End If

            Rst1.Open(String.Format(SQL.ToString(), Item), Banco.Conexao, 3, 3)

            If Not Rst1.EOF Then

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>IMPORTADOR</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMPORTADOR").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Dados do Transporte:</caption>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>TRANSPORTADORA</td>")
                Tabela1.Append("        <td>PLACA DO CAVALO</td>")
                Tabela1.Append("        <td>PLACA DA CARRETA</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("RAZAO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CAVALO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CARRETA").Value.ToString() & "</td>")

                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>TARA</td>")
                Tabela1.Append("        <td>CHASSI</td>")
                Tabela1.Append("        <td>COR</td>")
                Tabela1.Append("        <td>MODELO</td>")
                Tabela1.Append("        <td>LOCALIZAÇÃO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TARA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("CHASSI").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("COR").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("MODELO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LOCALIZACAO").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>MOTORISTA</td>")
                Tabela1.Append("        <td>CNH</td>")
                Tabela1.Append("        <td>CPF</td>")
                Tabela1.Append("        <td>RG</td>")
                Tabela1.Append("        <td>ÓRGÃO</td>")
                Tabela1.Append("        <td>EMISSÃO</td>")
                Tabela1.Append("        <td>NEXTEL</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("NOME").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("CNH").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("CPF").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("RG").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ORGAO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("EMISSAO_RG").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("NEXTEL").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Documentação: </caption>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>TIPO DOC. SAÍDA</td>")
                Tabela1.Append("        <td>SÉRIE DOC. SAÍDA</td>")
                Tabela1.Append("        <td>NUM. DOC. SAÍDA</td>")
                Tabela1.Append("        <td>EMISSÃO DOC. SAÍDA</td>")
                Tabela1.Append("        <td>TERMO AVARIA</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TIPO_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("SERIE_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("NUM_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("EMISSAO_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("TERMO_AVARIA").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                SQL.Clear()

                SQL.Append("SELECT ")
                SQL.Append("  A.LOTE, ")
                SQL.Append("  A.NOTAFISCAL, ")
                SQL.Append("  A.SERIE, ")
                SQL.Append("  TO_CHAR(A.EMISSAO,'DD/MM/YYYY') EMISSAO, ")
                SQL.Append("  D.DESCR AS DOCUMENTO, ")
                SQL.Append("  C.NUM_DOCUMENTO AS NUMDOC ")
                SQL.Append("FROM ")
                SQL.Append("  SGIPA.TB_AG_CS_NF A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_AG_CS B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_BL C ON B.LOTE = C.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_TIPOS_DOCUMENTOS D ON C.TIPO_DOCUMENTO = D.CODE ")
                SQL.Append("WHERE ")
                SQL.Append("  A.AUTONUM_AGENDAMENTO IN (" & Item & ") ")
                SQL.Append("GROUP BY ")
                SQL.Append("  A.LOTE, ")
                SQL.Append("  A.NOTAFISCAL, ")
                SQL.Append("  A.SERIE, ")
                SQL.Append("  A.EMISSAO, ")
                SQL.Append("  D.DESCR, ")
                SQL.Append("  C.NUM_DOCUMENTO ")
                SQL.Append("ORDER BY  ")
                SQL.Append("  A.NOTAFISCAL, ")
                SQL.Append("  A.SERIE, ")
                SQL.Append("  A.EMISSAO, ")
                SQL.Append("  A.LOTE  ")

                If Rst2.State = 1 Then
                    Rst2.Close()
                End If

                Rst2.Open(String.Format(SQL.ToString()), Banco.Conexao, 3, 3)

                SQL.Clear()

                While Not Rst2.EOF

                    Dim Lote As String
                    Dim NumNF As String
                    Dim SerieNF As String
                    Dim DataNF As String

                    If Rst3.State = 1 Then
                        Rst3.Close()
                    End If

                    Lote = Rst2.Fields("LOTE").Value.ToString()
                    'IdNotaFiscal = Int(Rst2.Fields("AUTONUM").Value.ToString())
                    NumNF = Rst2.Fields("NOTAFISCAL").Value.ToString()
                    SerieNF = Rst2.Fields("SERIE").Value.ToString()
                    DataNF = Rst2.Fields("EMISSAO").Value.ToString()

                    If Banco.BancoEmUso = "ORACLE" Then
                        SQL.Append("SELECT ")
                        SQL.Append("    A.AUTONUM,  ")
                        SQL.Append("    A.LOTE,  ")
                        SQL.Append("    A.QTDE AS QUANTIDADE,  ")
                        'SQL.Append("    A.AUTONUM_NF,   ")
                        SQL.Append("    B.MERCADORIA AS PRODUTO, ")
                        SQL.Append("    C.DESCR AS EMBALAGEM ")
                        SQL.Append("FROM  ")
                        SQL.Append("    SGIPA.TB_AG_CS_NF A  ")
                        SQL.Append("LEFT JOIN  ")
                        SQL.Append("    SGIPA.TB_CARGA_SOLTA B ON A.AUTONUM_CS = B.AUTONUM  ")
                        SQL.Append("LEFT JOIN  ")
                        SQL.Append("    SGIPA.DTE_TB_EMBALAGENS C ON B.EMBALAGEM = C.CODE  ")
                        SQL.Append("INNER JOIN  ")
                        SQL.Append("    SGIPA.TB_AG_CS D ON A.AUTONUM_AGENDAMENTO = D.AUTONUM  ")
                        SQL.Append("WHERE  ")
                        SQL.Append("    A.AUTONUM_AGENDAMENTO IN (" & Item & ") ")
                        SQL.Append("    AND ")
                        'SQL.Append("    A.AUTONUM_NF = " & IdNotaFiscal)
                        SQL.Append("    A.LOTE = " & Lote)
                        SQL.Append("    AND ")
                        SQL.Append("    A.NOTAFISCAL = '" & NumNF & "'")
                        SQL.Append("    AND ")
                        SQL.Append("    A.SERIE ") '& SerieNF & "'")
                        If SerieNF <> String.Empty Then
                            SQL.Append("= '" & SerieNF & "'")
                        Else
                            SQL.Append("IS NULL ")
                        End If
                        SQL.Append("    AND ")
                        SQL.Append("    A.EMISSAO ") '= TO_DATE('" & DataNF & "', 'dd/mm/yy')")
                        If DataNF <> String.Empty Then
                            SQL.Append("= TO_DATE('" & DataNF & "', 'dd/mm/yy')")
                        Else
                            SQL.Append("IS NULL ")
                        End If
                        SQL.Append("    AND ")
                        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & Item)
                    Else
                        SQL.Append("SELECT ")
                        SQL.Append("    A.AUTONUM,  ")
                        SQL.Append("    A.LOTE,  ")
                        SQL.Append("    A.QTDE AS QUANTIDADE,  ")
                        'SQL.Append("    A.AUTONUM_NF,   ")
                        SQL.Append("    B.MERCADORIA AS PRODUTO, ")
                        SQL.Append("    C.DESCR AS EMBALAGEM ")
                        SQL.Append("FROM  ")
                        SQL.Append("    SGIPA.TB_AG_CS_NF A  ")
                        SQL.Append("LEFT JOIN  ")
                        SQL.Append("    SGIPA.TB_CARGA_SOLTA B ON A.AUTONUM_CS = B.AUTONUM  ")
                        SQL.Append("LEFT JOIN  ")
                        SQL.Append("    SGIPA.DTE_TB_EMBALAGENS C ON B.EMBALAGEM = C.CODE  ")
                        'SQL.Append("LEFT JOIN  ")
                        'SQL.Append("    SGIPA.TB_AG_CS D ON A.AUTONUM_AGENDAMENTO = D.AUTONUM  ")
                        SQL.Append("WHERE  ")
                        'SQL.Append("    A.AUTONUM_AGENDAMENTO IN (" & Item & ") ")
                        'SQL.Append("    AND ")
                        'SQL.Append("    A.AUTONUM_NF = " & IdNotaFiscal)
                        SQL.Append("    A.LOTE = " & Lote)
                        SQL.Append("    AND ")
                        SQL.Append("    A.NOTAFISCAL = '" & NumNF & "'")
                        SQL.Append("    AND ")
                        SQL.Append("    A.SERIE = '" & SerieNF & "'")
                        SQL.Append("    AND ")
                        SQL.Append("    A.EMISSAO = TO_DATE('" & DataNF & "', 'dd/mm/yy')")
                        SQL.Append("    AND ")
                        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & Item)
                    End If

                    'Rst3.Open(String.Format(SQL.ToString(), Rst2.Fields("AUTONUM").Value.ToString()), Banco.Conexao, 3, 3)
                    Rst3.Open(String.Format(SQL.ToString()), Banco.Conexao, 3, 3)

                    SQL.Clear()

                    Tabela2.Append("<table>")
                    Tabela2.Append("    <thead>")
                    Tabela2.Append("        <td>LOTE</td>")
                    Tabela2.Append("        <td>DOCUMENTO</td>")
                    Tabela2.Append("        <td>NOTA FISCAL</td>")
                    Tabela2.Append("        <td>SÉRIE</td>")
                    Tabela2.Append("        <td>EMISSÃO</td>")
                    Tabela2.Append("    </thead>")
                    Tabela2.Append("    <tbody>")
                    Tabela2.Append("        <td>" & Lote & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Fields("DOCUMENTO").Value.ToString() & "&nbsp;-&nbsp" & Rst2.Fields("NUMDOC").Value.ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Fields("NOTAFISCAL").Value.ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Fields("SERIE").Value.ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Fields("EMISSAO").Value.ToString() & "</td>")
                    Tabela2.Append("    </tbody>")
                    Tabela2.Append("<tr>")
                    Tabela2.Append("<td colspan=9>")

                    Dim Contador As Integer = 0

                    TabelaItem.Append("<table class=itens>")
                    TabelaItem.Append("    <thead>")
                    TabelaItem.Append("        <td>ITEM</td>")
                    TabelaItem.Append("        <td>PRODUTO</td>")
                    TabelaItem.Append("        <td>EMBALAGEM</td>")
                    TabelaItem.Append("        <td>QUANTIDADE</td>")
                    TabelaItem.Append("    </thead>")

                    While Not Rst3.EOF
                        Contador += 1
                        TabelaItem.Append("    <tbody>")
                        TabelaItem.Append("        <td>" & Contador & "</td>")
                        TabelaItem.Append("        <td>" & Rst3.Fields("PRODUTO").Value.ToString() & "</td>")
                        TabelaItem.Append("        <td>" & Rst3.Fields("EMBALAGEM").Value.ToString() & "</td>")
                        TabelaItem.Append("        <td>" & Rst3.Fields("QUANTIDADE").Value.ToString() & "</td>")
                        TabelaItem.Append("    </tbody>")
                        Rst3.MoveNext()
                    End While

                    TabelaItem.Append("</table>")


                    Tabela2.Append(TabelaItem.ToString())

                    Tabela2.Append("</td>")
                    Tabela2.Append("</tr>")

                    Tabela2.Append("</table>")
                    Tabela2.Append("<br />")


                    TabelaItem.Clear()

                    Contador = 0

                    Rst2.MoveNext()

                End While

                Tabela4.Append("<table>")
                Tabela4.Append("    <thead>")
                Tabela4.Append("        <td colspan=3>Responsável</td>")
                Tabela4.Append("    </thead>")
                Tabela4.Append("    <tbody>")
                Tabela4.Append("        <tr>")
                Tabela4.Append("            <table>")
                Tabela4.Append("                <tr>")
                Tabela4.Append("                    <td class=assin colspan=3 align=left>Assinatura Motorista:</td>")
                Tabela4.Append("                </tr>")
                Tabela4.Append("                <tr>")
                Tabela4.Append("                    <tr>")
                Tabela4.Append("                        <td class=assinatura valign=top>Entregue em:<br/><br/>_____/_____/_____ _____:_____:_____<br/> <br/> <br/> <br/> ___________________________________ </td>")
                Tabela4.Append("                        <td width=30% valign=top>Conferente:</td>")
                Tabela4.Append("                        <td width=30% valign=top>Registro:</td>")
                Tabela4.Append("                    </tr>")
                Tabela4.Append("                </tr>")
                Tabela4.Append("            </table>")
                Tabela4.Append("        </tr>")
                Tabela4.Append("        <tr>")
                Tabela4.Append("        <b>Preferencialmente o motorista deverá se apresentar com 15 minutos de antecedência no registro;</b>")
                Tabela4.Append("        <br/><br/>")
                Tabela4.Append("        <b>- Em caso de necessidade de CANCELAMENTO, a transportadora deverá cancelar em até 03 horas antes do agendamento, <br/>sujeito à cobrança pela ausência do comparecimento no Terminal.</b>")
                Tabela4.Append("        </tr>")
                Tabela4.Append("    </tbody>")
                Tabela4.Append("</table>")
                Tabela4.Append("<br />")

                Tabela3.Append("<table id=cabecalho>")
                Tabela3.Append("	<tr>")
                Tabela3.Append("		<td align=left width=180px>")
                Tabela3.Append("			<img src=css/" & Empresa.ObterNomeFantasiaDiret(TiaEmpresa) & "/images/LogoTop.png />") 'imagens/LogoTop.png />")
                Tabela3.Append("		</td>")
                Tabela3.Append("		<td>")
                Tabela3.Append("		<font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE CARGA SOLTA (IMPORTAÇÃO)</font>")
                Tabela3.Append("		<br/>")
                Tabela3.Append("        <font face=Arial size=5>Nº: " & Rst1.Fields("NUM_PROTOCOLO").Value.ToString() & "/" & Rst1.Fields("ANO_PROTOCOLO").Value.ToString() & "</font>")
                Tabela3.Append("		<br/>")
                Tabela3.Append("        <font face=Arial size=4><b>PÁTIO: " & Rst1.Fields("PATIO").Value.ToString() & "</b></font>")
                Tabela3.Append("        <br/><br/>")
                Tabela3.Append("        <font face=Arial size=4>" & Rst1.Fields("PERIODO").Value.ToString() & "</font>")
                Tabela3.Append("		</td>")
                Tabela3.Append("	</tr>")
                Tabela3.Append("</table>")
                Tabela3.Append("<br/>")

                Estrutura.Append(Tabela3.ToString())
                Estrutura.Append(Tabela1.ToString())
                Estrutura.Append(Tabela2.ToString())
                Estrutura.Append(Tabela4.ToString())
                Estrutura.Append("<div class=folha></div>")

                Tabela3.Clear()
                Tabela1.Clear()
                Tabela2.Clear()
                Tabela4.Clear() 'Somente necessário para quando vai imprimir vários protocolos de uma só vez
                SQL.Clear()

                AlterarStatusImpressao(Item)

            End If
        Next

        Return Estrutura

    End Function

    Public Function AlterarStatusImpressao(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("UPDATE SGIPA.TB_AG_CS ")
            SQL.Append("    SET ")
            SQL.Append("        IMPRESSO=1")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM=" & ID & "")
        Else
            SQL.Append("UPDATE SGIPA.DBO.TB_AG_CS ")
            SQL.Append("    SET ")
            SQL.Append("        IMPRESSO=1")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM=" & ID & "")
        End If

        Try
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ObterPatio(ByVal Lote As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT PATIO FROM SGIPA.TB_BL WHERE AUTONUM={0}", Lote), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT PATIO FROM SGIPA.TB_BL WHERE AUTONUM={0}", Lote), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("PATIO").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function AgendamentoMesmoPatioPeriodo(ByVal CodigoTransportadora As Integer, ByVal CodigoVeiculo As Integer, ByVal PeriodoInicial As String, ByVal PeriodoFinal As String, ByVal Patio As Integer, Optional ByVal CodigoAgendamento As Integer = 0) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(1) AS TOTAL ")
        SQL.Append("FROM  ")
        SQL.Append("    TB_AG_CS A ")
        SQL.Append("INNER JOIN  ")
        SQL.Append("    OPERADOR.TB_GD_RESERVA B ON A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA ")
        SQL.Append("WHERE  ")
        SQL.Append("    A.AUTONUM_TRANSPORTADORA = {0} ")
        SQL.Append("AND  ")
        SQL.Append("    AUTONUM_VEICULO = {1} ")
        SQL.Append("AND  ")
        SQL.Append("    B.PATIO <> {2} ")
        SQL.Append("AND  ")
        SQL.Append("    NVL(A.FLAG_DDC,0) = 0 ")
        SQL.Append("AND  ")
        SQL.Append("    B.PERIODO_INICIAL >= TO_DATE('{3}','DD/MM/YYYY HH24:MI') ")
        SQL.Append("AND  ")
        SQL.Append("    B.PERIODO_INICIAL <= TO_DATE('{4}','DD/MM/YYYY HH24:MI') ")

        If CodigoAgendamento > 0 Then
            SQL.Append("AND A.AUTONUM <> {5} ")
            Rst.Open(String.Format(SQL.ToString(), CodigoTransportadora, CodigoVeiculo, Patio, PeriodoInicial, PeriodoFinal, CodigoAgendamento), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format(SQL.ToString(), CodigoTransportadora, CodigoVeiculo, Patio, PeriodoInicial, PeriodoFinal), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("TOTAL").Value.ToString()
        End If

    End Function

    Public Function ConteinerComPendenciaRecebimento(ByVal CodigoPeriodo As String, ByVal Lote As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Clear()

        SQL.Append("select count(1) contar from tb_servicos_faturados a inner join tb_gr_pre_calculo b on a.bl=b.bl where a.bl= " & Lote & " and a.seq_gr is null and formapagamento=2")
        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)

        If Convert.ToInt32(Rst.Fields("CONTAR").Value.ToString()) > 0 Then
            Return True
        End If

        If Rst.State = 1 Then
            Rst.Close()
        End If

        SQL.Clear()
        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM, ")
        SQL.Append("        CASE ")
        SQL.Append("            WHEN F.VALIDADE < B.PERIODO_INICIAL THEN ")
        SQL.Append("                'NAO' ")
        SQL.Append("            ELSE ")
        SQL.Append("                'OK' ")
        SQL.Append("        END AS VALIDO ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_BL A, ")
        SQL.Append("    OPERADOR.TB_GD_RESERVA B, ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT ")
        SQL.Append("                FAT.BL, ")
        SQL.Append("                TO_DATE(TO_CHAR((NVL(DT_BASE_CALCULO_REEFER, VALIDADE_GR)) + 1, ")
        SQL.Append("                    'DD/MM/YYYY') || ' 00:00:01', 'DD/MM/YYYY HH24:MI:SS') AS VALIDADE ")
        SQL.Append("            FROM SGIPA.TB_GR_BL, ")
        SQL.Append("                (SELECT ")
        SQL.Append("                    A.BL, MAX (A.SEQ_GR) SEQ_GR ")
        SQL.Append("                FROM ")
        SQL.Append("                    SGIPA.TB_SERVICOS_FATURADOS A, SGIPA.TB_GR_BL B ")
        SQL.Append("                WHERE ")
        SQL.Append("                    A.SEQ_GR = B.SEQ_GR ")
        SQL.Append("                    AND ")
        SQL.Append("                    (SERVICO = 52 OR SERVICO = 71) ")
        SQL.Append("                    AND ")
        SQL.Append("                    STATUS_GR = 'IM' AND A.BL = {1} AND B.BL = {1} ")
        SQL.Append("                GROUP BY A.BL) FAT ")
        SQL.Append("            WHERE ")
        SQL.Append("                TB_GR_BL.SEQ_GR = FAT.SEQ_GR ")
        SQL.Append("                AND ")
        SQL.Append("                TB_GR_BL.BL = FAT.BL ")
        SQL.Append("                AND ")
        SQL.Append("                STATUS_GR = 'IM'  AND TB_GR_BL.BL = {1}) F ")
        SQL.Append("WHERE ")
        SQL.Append("    A.FLAG_ATIVO=1 ")
        SQL.Append("AND ")
        SQL.Append("    A.AUTONUM=F.BL(+) ")
        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM_GD_RESERVA=NVL({0},0) ")
        SQL.Append("AND ")
        SQL.Append("    A.AUTONUM = {1} ")

        Rst.Open(String.Format(SQL.ToString(), CodigoPeriodo, Lote), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Rst.Fields("VALIDO").Value.ToString() <> "OK" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ConsultarFormaPagamento(ByVal Lote As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder


        SQL.Append("select count(1) contar   from tb_servicos_faturados a  inner join tb_gr_pre_calculo b on a.bl=b.bl where a.bl= " & Lote & " and a.seq_gr is null and formapagamento=2")
        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)
        If Convert.ToInt32(Rst.Fields("CONTAR").Value.ToString()) > 0 Then
            Return 2
        End If

        Rst.Close()

        SQL.Clear()
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    FORMA_PAGAMENTO ")
            SQL.Append("FROM ")
            SQL.Append("    (SELECT FAT.BL, FORMA_PAGAMENTO ")
            SQL.Append("    FROM ")
            SQL.Append("	    SGIPA.TB_GR_BL, ")
            SQL.Append("        (SELECT   A.BL, MAX (A.SEQ_GR) SEQ_GR ")
            SQL.Append("        FROM ")
            SQL.Append("            SGIPA.TB_SERVICOS_FATURADOS A, SGIPA.TB_GR_BL B ")
            SQL.Append("        WHERE ")
            SQL.Append("            A.SEQ_GR = B.SEQ_GR ")
            SQL.Append("			AND ")
            SQL.Append("            (SERVICO = 52 OR SERVICO = 71) ")
            SQL.Append("            AND ")
            SQL.Append("			(STATUS_GR = 'IM' OR STATUS_GR = 'GE') ")
            SQL.Append("		GROUP BY A.BL) FAT ")
            SQL.Append("    WHERE ")
            SQL.Append("        TB_GR_BL.SEQ_GR = FAT.SEQ_GR ")
            SQL.Append("	    AND ")
            SQL.Append("        TB_GR_BL.BL = FAT.BL ")
            SQL.Append("	    AND ")
            SQL.Append("		(STATUS_GR = 'IM' OR STATUS_GR = 'GE')) F ")
            SQL.Append("WHERE ")
            SQL.Append("    F.BL=NVL({0},0) ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    FORMA_PAGAMENTO ")
            SQL.Append("FROM ")
            SQL.Append("    (SELECT FAT.BL, FORMA_PAGAMENTO ")
            SQL.Append("    FROM ")
            SQL.Append("	    SGIPA.DBO.TB_GR_BL, ")
            SQL.Append("        (SELECT   A.BL, MAX (A.SEQ_GR) SEQ_GR ")
            SQL.Append("        FROM ")
            SQL.Append("            SGIPA.DBO.TB_SERVICOS_FATURADOS A, SGIPA.DBO.TB_GR_BL B ")
            SQL.Append("        WHERE ")
            SQL.Append("            A.SEQ_GR = B.SEQ_GR ")
            SQL.Append("			AND ")
            SQL.Append("            (SERVICO = 52 OR SERVICO = 71) ")
            SQL.Append("            AND ")
            SQL.Append("			(STATUS_GR = 'IM' OR STATUS_GR = 'GE') ")
            SQL.Append("		GROUP BY A.BL) FAT ")
            SQL.Append("    WHERE ")
            SQL.Append("        TB_GR_BL.SEQ_GR = FAT.SEQ_GR ")
            SQL.Append("	    AND ")
            SQL.Append("        TB_GR_BL.BL = FAT.BL ")
            SQL.Append("	    AND ")
            SQL.Append("		(STATUS_GR = 'IM' OR STATUS_GR = 'GE')) F ")
            SQL.Append("WHERE ")
            SQL.Append("    F.BL=ISNULL({0},0) ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Convert.ToInt32(Rst.Fields("FORMA_PAGAMENTO").Value.ToString())
        End If

        Return False

    End Function

    Public Function ObterParametroRetirada() As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT B.MOTIVO_RETIRADA FROM SGIPA.TB_PARAMETROS_SISTEMA B,SGIPA.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_DESOVA=A.CODE", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT B.MOTIVO_RETIRADA FROM SGIPA.DBO.TB_PARAMETROS_SISTEMA B,SGIPA.DBO.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_DESOVA=A.CODE", Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("MOTIVO_RETIRADA").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function InsereAgendamentoNaFila(ByVal Lote As String, ByVal Periodo As String, ByVal UserId As Integer) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim Data_Prevista As String = String.Empty
        Dim Codigo_Periodo As String = String.Empty

        Dim Motivo As String = ObterParametroRetirada()

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM_GD_RESERVA,TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI:SS') PERIODO_INICIAL FROM OPERADOR.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA={0}", Periodo), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM_GD_RESERVA,OPERADOR.DBO.TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI:SS') PERIODO_INICIAL FROM OPERADOR.DBO.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA={0}", Periodo), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Data_Prevista = Rst.Fields("PERIODO_INICIAL").Value.ToString()
        End If

        Rst.Close()

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT A.AUTONUM FROM SGIPA.TB_AGENDAMENTO_POSICAO A, SGIPA.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.LOTE={0} AND B.MOTIVO_POSICAO={1}", Lote, Motivo), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT A.AUTONUM FROM SGIPA.DBO.TB_AGENDAMENTO_POSICAO A, SGIPA.DBO.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.LOTE={0} AND B.MOTIVO_POSICAO={1}", Lote, Motivo), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            Codigo_Periodo = Rst.Fields("AUTONUM").Value.ToString()
            Rst.Close()

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE SGIPA.TB_AGENDAMENTO_POSICAO SET DT_PREVISTA=TO_DATE('{0}','DD/MM/YYYY HH24:MI:SS'), DATA_ATUALIZA=SYSDATE WHERE AUTONUM={1}", Data_Prevista, Codigo_Periodo), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE SGIPA.DBO.TB_AGENDAMENTO_POSICAO SET DT_PREVISTA=OPERADOR.DBO.TO_DATE('{0}','DD/MM/YYYY HH24:MI:SS'), DATA_ATUALIZA=GETDATE() WHERE AUTONUM={1}", Data_Prevista, Codigo_Periodo), Banco.Conexao, 3, 3)
            End If

            Return True

        Else

            Rst.Close()

            Dim ID As String = String.Empty

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open("SELECT SGIPA.SEQ_AGENDAMENTO_POSICAO.NEXTVAL AS ID FROM DUAL", Banco.Conexao, 3, 3)
            Else
                Rst.Open("SELECT MAX(AUTONUM)+1 AS ID FROM SGIPA.DBO.TB_AGENDAMENTO_POSICAO", Banco.Conexao, 3, 3)
            End If

            If Not Rst.EOF Then
                ID = Rst.Fields("ID").Value.ToString()
                Rst.Close()
            End If

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("INSERT INTO SGIPA.TB_AGENDAMENTO_POSICAO (AUTONUM,LOTE,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS,NUM_PROTOCOLO_INTERNET, IUSID) VALUES ({0},{1},TO_DATE('{2}','DD/MM/YYYY HH24:MI:SS'),SYSDATE,0,SGIPA.SEQ_OS" & Now.Year & ".NEXTVAL," & Now.Year & ", " & "SGIPA.SEQ_PROTOCOLO_AGENDA_POSICIONA.NEXTVAL || '/' || TO_CHAR(SYSDATE,'YYYY'), " & UserId & " )", ID, Lote, Data_Prevista), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("INSERT INTO SGIPA.DBO.TB_AGENDAMENTO_POSICAO (LOTE,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS) VALUES ({0},OPERADOR.DBO.TO_DATE('{1}','DD/MM/YYYY HH24:MI:SS'),GETDATE(),0," & ID & "," & Now.Year & ")", Lote, Data_Prevista), Banco.Conexao, 3, 3)
            End If

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("INSERT INTO SGIPA.TB_AGENDA_POSICAO_MOTIVO (AUTONUM,AUTONUM_AGENDA_POSICAO,MOTIVO_POSICAO) VALUES (SGIPA.SEQ_AGENDA_POSICAO_MOTIVO.NEXTVAL,{0},{1}) ", ID, Motivo), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("INSERT INTO SGIPA.DBO.TB_AGENDA_POSICAO_MOTIVO (AUTONUM_AGENDA_POSICAO,MOTIVO_POSICAO) VALUES ({0},{1}) ", ID, Motivo), Banco.Conexao, 3, 3)
            End If

            Return True

        End If

        Return False

    End Function

    Public Function AlterarDataSaida(ByVal Codigo As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE SGIPA.TB_ag_cs SET DT_AGENDA_SAIDA = SYSDATE WHERE DT_AGENDA_SAIDA is null AND  AUTONUM = {0}", Codigo), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE SGIPA.DBO.TB_ag_cs SET DT_AGENDA_SAIDA = GETDATE() WHERE DT_AGENDA_SAIDA is null AND  AUTONUM = {0}", Codigo), Banco.Conexao, 3, 3)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function NFObrigatoria(ByVal Lote As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Valida As Boolean

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SELECT DISTINCT ")
            SQL.Append("    TD.FLAG_NF_AV_ONLINE LIBERA")
            SQL.Append("    FROM SGIPA.TB_BL C,  ")
            SQL.Append("         SGIPA.TB_TIPOS_DOCUMENTOS TD  ")
            SQL.Append("    WHERE  ")
            SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("    C.AUTONUM={0}  ")
        Else
            SQL.Append("    SELECT DISTINCT ")
            SQL.Append("    TD.FLAG_NF_AV_ONLINE LIBERA ")
            SQL.Append("    FROM   ")
            SQL.Append("         DBO.SGIPA.TB_BL C,  ")
            SQL.Append("         DBO.SGIPA.TB_TIPOS_DOCUMENTOS TD  ")
            SQL.Append("    WHERE  ")
            SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("    C.AUTONUM={0}  ")
        End If
        Valida = True
        Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)
        If Not Rst.EOF Then
            If Rst.Fields("LIBERA").Value.ToString() <> 1 Then
                Valida = False
            End If
        End If
        Return Valida

    End Function

    Public Function ObterCNHPeloIdAgendamento(ByVal ID As String) As String
        Dim Rst As New ADODB.Recordset
        Dim Sql As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            Sql.Append("    select m.CNH from sgipa.tb_ag_cs acs")
            Sql.Append("    inner join ")
            Sql.Append("    operador.tb_ag_motoristas m")
            Sql.Append("    on acs.AUTONUM_MOTORISTA = M.AUTONUM")
            Sql.Append("    AND")
            Sql.Append("    ACS.AUTONUM = '{0}'")
        Else
            Sql.Append("    select m.CNH from sgipa.dbo.tb_ag_cs acs")
            Sql.Append("    inner join ")
            Sql.Append("    operador.dbo.tb_ag_motoristas m")
            Sql.Append("    on acs.AUTONUM_MOTORISTA = M.AUTONUM")
            Sql.Append("    AND")
            Sql.Append("    ACS.AUTONUM = '{0}'")
        End If
        Rst.Open(String.Format(Sql.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("CNH").Value.ToString()
        End If

        Return String.Empty

    End Function
End Class
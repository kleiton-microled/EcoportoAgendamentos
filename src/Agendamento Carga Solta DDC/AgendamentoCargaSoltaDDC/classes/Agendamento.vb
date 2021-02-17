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
    Private _ConteinerDDC As String
    Private _UsuarioId As String

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

    Public Property ConteinerDDC() As String
        Get
            Return Me._ConteinerDDC
        End Get
        Set(ByVal value As String)
            Me._ConteinerDDC = value
        End Set
    End Property

    Public Property UsuarioId() As String
        Get
            Return Me._UsuarioId
        End Get
        Set(ByVal value As String)
            Me._UsuarioId = value
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

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM_TRANSPORTADORA ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = '" & IdAgend & "' ")

        Dim dt As New DataTable
        dt = Banco.Consultar(SQL.ToString())

        If dt.Rows.Count > 0 Then
            IdTranspCSIPA = Convert.ToInt32(dt.Rows(0)("AUTONUM_TRANSPORTADORA").ToString())
            If IdTranspCSIPA = IdTransp Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function ConsultarCNH(ByVal Motorista As Motorista, ByVal Transportadora As String) As List(Of String)

        Dim Dt As New DataTable
        Dim ListaCNH As New List(Of String)

        Dt = Banco.Consultar(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCNH.Add(Item("CNH").ToString())
            Next

            Return ListaCNH

        End If

        Return Nothing

    End Function

    Public Function ConsultarCNHSemHomonimo(ByVal Motorista As Motorista, ByVal Transportadora As String) As List(Of String)

        'Feito para pegar somente o nome TOTALMENTE IGUAL, quando seleciona o CNH do Motorista
        'SEMPRE vai retornar só UM elemento na lista

        Dim Dt As New DataTable
        Dim ListaCNH As New List(Of String)

        Dt = Banco.Consultar(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE TRIM(NOME) = '{0}' AND ID_TRANSPORTADORA = {1}", Motorista.Nome, Transportadora))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCNH.Add(Item("CNH").ToString())
            Next

            Return ListaCNH

        End If

        Return Nothing

    End Function

    Public Function ConsultarNome(ByVal Motorista As Motorista) As String

        Dim Resultado As String = String.Empty

        If Not Motorista.CNH = String.Empty Then
            Resultado = Banco.ExecutaRetorna(String.Format("SELECT NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}' AND ID_TRANSPORTADORA={1}", Motorista.CNH, Motorista.Transportadora.ID))
        Else
            Resultado = Banco.ExecutaRetorna(String.Format("SELECT NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Motorista.Transportadora.ID))
        End If

        Return Resultado

    End Function

    Public Function VerificarHomonimos(ByVal NomeMotorista As String, ByVal Transportadora As String) As Boolean

        Dim Dt As New DataTable

        Dt = Banco.Consultar(String.Format("SELECT COUNT(*) AS QTDENOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME = '{0}' AND ID_TRANSPORTADORA = {1}", NomeMotorista, Transportadora))

        If Dt.Rows.Count > 0 Then
            If Int(Dt.Rows(0)("QTDENOME").ToString) > 1 Then
                Return True
            Else
                Return False
            End If
        End If

        Return False

    End Function

    Public Function ConsultarDadosVeiculo(ByVal Veiculo As Veiculo) As Veiculo

        Dim Dt As New DataTable

        Dt = Banco.Consultar(String.Format("SELECT TARA,CHASSI FROM OPERADOR.TB_AG_VEICULOS WHERE PLACA_CAVALO = '{0}' AND PLACA_CARRETA = '{1}' AND ID_TRANSPORTADORA = {2}", Veiculo.Cavalo, Veiculo.Carreta, Veiculo.Transportadora.ID))

        If Dt.Rows.Count > 0 Then

            Dim VeiculoOBJ As New Veiculo

            VeiculoOBJ.Tara = Dt.Rows(0)("TARA").ToString()
            VeiculoOBJ.Chassi = Dt.Rows(0)("CHASSI").ToString()

            Return VeiculoOBJ

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCavalo(ByVal Motorista As Motorista) As List(Of String)

        Dim Dt As New DataTable
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        SQL.Append("SELECT DISTINCT ")
        SQL.Append("    A.PLACA_CAVALO ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
        SQL.Append("WHERE ")
        SQL.Append("    A.ID_TRANSPORTADORA = {0} ")

        Dt = Banco.Consultar(String.Format(SQL.ToString(), Motorista.Transportadora.ID))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCavalo.Add(Item("PLACA_CAVALO").ToString())
            Next

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCarreta(ByVal Motorista As Motorista) As List(Of String)

        Dim Dt As New DataTable
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        SQL.Append("SELECT ")
        SQL.Append("    A.PLACA_CARRETA ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
        SQL.Append("WHERE ")
        SQL.Append("    A.ID_TRANSPORTADORA = {0} ")

        Dt = Banco.Consultar(String.Format(SQL.ToString(), Motorista.Transportadora.ID))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCavalo.Add(Item("PLACA_CARRETA").ToString())
            Next

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarPlacasCarretaPorCavalo(ByVal Cavalo As String, ByVal Transportadora As String) As List(Of String)

        Dim Dt As New DataTable
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        SQL.Append("SELECT ")
        SQL.Append("    A.PLACA_CARRETA ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
        SQL.Append("WHERE ")
        SQL.Append("    A.PLACA_CAVALO = '{0}' ")
        SQL.Append("AND ")
        SQL.Append("    A.ID_TRANSPORTADORA = {1} ")

        Dt = Banco.Consultar(String.Format(SQL.ToString(), Cavalo, Transportadora))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCavalo.Add(Item("PLACA_CARRETA").ToString())
            Next

            Return ListaCavalo

        End If

        Return Nothing

    End Function

    Public Function ConsultarItensCargaSolta(ByVal Conteiner As String, ByVal Empresa As String, ByVal Transportadora As String, ByVal Edicao As Boolean) As DataTable

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        AUTONUM, ")
        SQL.Append("        'Qtde: ' || QUANTIDADE || ' | ' || PRODUTO || ' (' || EMBALAGEM || ') | Saldo: ' || SALDO AS DISPLAY ")
        SQL.Append("FROM ( ")
        SQL.Append("    SELECT ")
        SQL.Append("        A.AUTONUM, ")
        SQL.Append("        A.QUANTIDADE, ")
        SQL.Append("        EMB.DESCR AS EMBALAGEM, ")
        SQL.Append("        A.MERCADORIA AS PRODUTO, ")
        SQL.Append("        (NVL(A.QUANTIDADE_REAL,0) - NVL(A.QUANTIDADE_SAIDA,0)) - (CASE WHEN NVL(A.QUANTIDADE_SAIDA,0)>=NVL(AGENDADOS.AGENDADO,0) THEN 0 ELSE NVL(AGENDADOS.AGENDADO,0)-NVL(A.QUANTIDADE_SAIDA,0) END ) AS SALDO , ")
        SQL.Append("        TD.DESCR AS TIPO_DOCUMENTO, ")
        SQL.Append("        D.NUM_DOCUMENTO,  ")
        SQL.Append("        TO_CHAR(A.DT_FIM_PERIODO,'DD/MM/YYYY HH24:MI') AS DATA_FREE_TIME, ")
        SQL.Append("        D.LOTE AS LOTE,C.NUMERO,  ")
        SQL.Append("        MIN(F.AVERBOU) AS AVERBOU,  ")
        SQL.Append("        MIN(F.ICMS_SEFAZ) AS DI_ICMS_SEFAZ, ")
        SQL.Append("        MIN(F.DESEMBARACADO) AS DI_DESEMBARACADA, ")
        SQL.Append("                MIN(F.MAPA) ")
        SQL.Append("             AS MAPA_DE_MADEIRA, ")
        SQL.Append("            CASE  ")
        SQL.Append("                WHEN MIN(F.FORMA_PAGAMENTO)=3 THEN 'FAT' ")
        SQL.Append("            ELSE  ")
        SQL.Append("                MIN(F.GR_PAGA) ")
        SQL.Append("            END AS  GR_PAGA,  ")
        SQL.Append("        MIN(F.SISCARGA) AS SISCARGA, ")
        SQL.Append("        MIN(F.BLOQUEIO_BL) AS BLOQUEIO_BL, ")
        SQL.Append("        MIN(F.BLOQUEIO_CNTR) AS BLOQUEIO_CNTR, ")
        SQL.Append("        MIN(F.FORMA_PAGAMENTO) AS FORMA_PAGAMENTO, ")
        SQL.Append("        MIN(F.FLAG_CERTIFICADO) AS FLAG_CERTIFICADO,  ")
        SQL.Append("        C.TIPO_DOCUMENTO AS TIPO_DOC  ")
        SQL.Append("    FROM  ")
        SQL.Append("        SGIPA.TB_CARGA_SOLTA A ")
        SQL.Append("    INNER JOIN ")
        SQL.Append("         SGIPA.TB_BL C ON A.BL = C.AUTONUM ")
        SQL.Append("    INNER JOIN  ")
        SQL.Append("        OPERADOR.TB_PATIOS P ON C.PATIO = P.AUTONUM ")

        ' O Site previa o agendamento concluido para mostrar a contagem do saldo atualizada caso fosse adicionar mais algum item
        ' Criei os campos Cntr e flag_ddc na tabela TB_AG_CS_NF para decrementar o saldo a medida que for incluindo itens na lista sem a necessidade da TB_AG_CS
        SQL.Append("    LEFT JOIN  ")
        SQL.Append("        (  ")
        SQL.Append("            SELECT ")
        SQL.Append("                NF.CNTR,NF.LOTE, ")
        SQL.Append("                NVL(SUM(NF.QTDE),0) AGENDADO ")
        SQL.Append("            FROM  ")
        SQL.Append("                SGIPA.TB_AG_CS_NF NF ")
        SQL.Append("            WHERE  ")
        SQL.Append("                NF.FLAG_DDC = 1 ")
        SQL.Append("            GROUP BY NF.CNTR,NF.LOTE ")
        SQL.Append("         ) AGENDADOS ON A.CNTR = AGENDADOS.CNTR AND A.BL = AGENDADOS.LOTE ")

        SQL.Append("         INNER JOIN ")
        SQL.Append("            SGIPA.TB_AV_ONLINE D ON C.AUTONUM = D.LOTE ")
        SQL.Append("         INNER JOIN ")
        SQL.Append("            SGIPA.TB_AV_ONLINE_TRANSP E ON D.AUTONUM = E.TB_AV_ONLINE ")
        SQL.Append("         INNER JOIN ")
        SQL.Append("            SGIPA.TB_TIPOS_DOCUMENTOS TD ON C.TIPO_DOCUMENTO = TD.CODE ")
        SQL.Append("         INNER JOIN ")
        SQL.Append("            SGIPA.VW_ETAPAS_AV_BL_CNTR F ON C.AUTONUM=F.LOTE ")
        SQL.Append("         LEFT JOIN  ")
        SQL.Append("            SGIPA.DTE_TB_EMBALAGENS EMB ON A.EMBALAGEM = EMB.CODE ")
        SQL.Append("    WHERE  ")
        SQL.Append("         C.FLAG_ATIVO = 1 ")
        SQL.Append("    AND  ")
        SQL.Append("        E.TRANSPORTADORA = " & Transportadora & " ")
        SQL.Append("    AND  ")
        SQL.Append("         A.FLAG_TERMINAL = 1 ")
        SQL.Append("    AND  ")
        SQL.Append("         A.FLAG_HISTORICO = 0 ")
        SQL.Append("      ")

        If Not Edicao Then
            SQL.Append("   AND ( ")
            SQL.Append("        A.QUANTIDADE_REAL - NVL((SELECT sum(NF.Qtde) FROM SGIPA.TB_AG_CS_NF NF INNER JOIN SGIPA.TB_AG_CS AG ON NF.AUTONUM_AGENDAMENTO=AG.AUTONUM  WHERE AG.AUTONUM_TRANSPORTADORA>0 AND AG.AUTONUM_VEICULO>0 and NF.autonum_cs=A.AUTONUM),0) > 0 ")
            SQL.Append("    ) ")
        End If

        If Empresa = 1 Then 'ECOPORTO 
            SQL.Append("    AND ")
            SQL.Append("        C.PATIO <> 5 ")
        Else ' ECOPORTO RA
            SQL.Append("    AND ")
            SQL.Append("        C.PATIO = 5 ")
        End If

        SQL.Append("      AND A.CNTR = " & Conteiner & " ")
        SQL.Append("    GROUP BY ")
        SQL.Append("        A.AUTONUM, ")
        SQL.Append("        A.QUANTIDADE, ")
        SQL.Append("        EMB.DESCR,  ")
        SQL.Append("        A.MERCADORIA,  ")
        SQL.Append("        A.QUANTIDADE_REAL, ")
        SQL.Append("        A.QUANTIDADE_SAIDA, ")
        SQL.Append("        AGENDADOS.AGENDADO, ")
        SQL.Append("        TD.DESCR,  ")
        SQL.Append("        D.NUM_DOCUMENTO,  ")
        SQL.Append("        A.DT_FIM_PERIODO,  ")
        SQL.Append("        D.LOTE,  ")
        SQL.Append("        C.TIPO_DOCUMENTO, ")
        SQL.Append("        C.NUMERO  ")
        SQL.Append("     )  ")
        SQL.Append(" WHERE  ")
        SQL.Append("    (  ")
        SQL.Append("    AVERBOU='SIM' ")
        SQL.Append(" AND  ")
        SQL.Append("    DI_ICMS_SEFAZ='SIM' ")
        SQL.Append(" AND  ")
        SQL.Append("     BLOQUEIO_BL = 'NAO' ")
        SQL.Append(" AND  ")
        SQL.Append("     BLOQUEIO_CNTR = 'NAO' ")
        SQL.Append(" AND  ")
        SQL.Append("     DI_DESEMBARACADA = 'SIM' ")
        SQL.Append(" AND  ")
        SQL.Append("     SISCARGA = 'SIM' ")
        SQL.Append(" AND  ")
        SQL.Append("     (MAPA_DE_MADEIRA = 'SIM'  )  ")
        SQL.Append(" AND  ")
        SQL.Append("     (GR_PAGA = 'SIM' OR GR_PAGA = 'FAT' OR FORMA_PAGAMENTO = 3)  ")
        SQL.Append(" AND  ")
        SQL.Append("     (TIPO_DOC<7 OR TIPO_DOC>13))  ")
        SQL.Append(" OR  ")
        SQL.Append("     (AVERBOU='SIM' AND MAPA_DE_MADEIRA = 'SIM' AND BLOQUEIO_BL = 'NAO'  ")
        SQL.Append("                    AND BLOQUEIO_CNTR = 'NAO'  ")
        SQL.Append("                    AND (GR_PAGA = 'SIM' OR GR_PAGA = 'FAT' OR  FORMA_PAGAMENTO = 3) ")
        SQL.Append("                    AND (TIPO_DOC>=7 AND TIPO_DOC<=13))  ")

        Return Banco.Consultar(String.Format(SQL.ToString(), Conteiner))

    End Function

    Public Function ConsultarSaldoCS(ByVal CodProduto As String) As Integer

        Dim SQL As New StringBuilder

        SQL.Append("SELECT  ")
        SQL.Append("    (A.QUANTIDADE_REAL) - ")
        SQL.Append("    NVL((SELECT SUM(QTDE) FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_CS = A.AUTONUM), 0) AS SALDO")
        SQL.Append("    FROM ")
        SQL.Append("    SGIPA.TB_CARGA_SOLTA A ")
        SQL.Append("    WHERE ")
        SQL.Append("    A.AUTONUM = {0}")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString, CodProduto))

    End Function

    Public Function ConsultarPeriodos(ByVal Lote As String) As DataTable

        Dim Dt As New DataTable
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

        Dt = Banco.Consultar(String.Format(SQL.ToString(), Lote))

        DTA = String.Empty

        For Each Item As DataRow In Dt.Rows

            Patio = Item("PATIO").ToString()
            Lote = Item("AUTONUM").ToString()
            Flag_VIP = Item("VIP").ToString()

            'Não há nenhuma necessidade de na comparação formatar a Data_Max:
            If (Data_Max = Nothing And Not Item("DATA_MAX").ToString() Is DBNull.Value) Or Not Data_Max = Nothing Then
                '(Data_Max é vazia assim como o registro no BD) OU Data_Max NÃO é vazia
                If Data_Max < Item("DATA_MAX").ToString() Then
                    Data_Max = Item("DATA_MAX").ToString()
                End If
            End If

            If Convert.ToInt32(Item("TIPO_DOC").ToString()) > 7 And Convert.ToInt32(Item("TIPO_DOC").ToString()) < 13 Then
                DTA = "0"
                Limite = -9999
            Else
                If DTA = String.Empty Then
                    DTA = "1"
                    Limite = 0
                End If
            End If

        Next

        If Data_Max = Nothing Then
            Data_Max = DateTime.Now.AddDays(30)
        End If

        Dt = Banco.Consultar(String.Format("SELECT DATA_INICIAL_AG FROM SGIPA.VW_DATA_INICIAL_AG WHERE PATIO={0}", Val(Patio)))

        If Dt.Rows.Count > 0 Then
            Data_Ref = Dt.Rows(0)("DATA_INICIAL_AG").ToString()
        Else
            Data_Ref = Now
        End If

        SQL.Clear()

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM_GD_RESERVA, ")
        SQL.Append("    TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
        SQL.Append("    TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
        SQL.Append("    TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT (B.AUTONUM) FROM SGIPA.TB_AG_CS B WHERE B.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA),'000') AS SALDO, ")
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
        SQL.Append("    A.FLAG_VIP <= " & Val(Flag_VIP) & " ")
        SQL.Append("AND ")
        SQL.Append("    A.FLAG_DTA <= " & Val(DTA) & " ")
        SQL.Append("AND ")
        SQL.Append("    A.PATIO = " & Val(Patio) & " ")
        SQL.Append("AND ")
        SQL.Append("    A.SERVICO_GATE = 'F' ")
        SQL.Append("ORDER BY ")
        SQL.Append("    A.PERIODO_INICIAL ")

        Return Banco.Consultar(SQL.ToString())

    End Function

    Public Function VerificarLimiteMovPeriodo(Reserva As String) As Boolean

        'Retorna True: se período é disponível para agendar
        'Retorna False: se período já alcançou o limite de qtde de agendamentos

        Dim SQL As New StringBuilder
        Dim Limite, QtdePeriodo As Integer

        SQL.Append("SELECT ")
        SQL.Append("    LIMITE_MOVIMENTOS ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.")
        SQL.Append("TB_GD_RESERVA ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM_GD_RESERVA = {0}")

        Limite = Banco.ExecutaRetorna(String.Format(SQL.ToString(), Reserva))

        SQL.Clear()

        'Pesquisa para saber a quantidade de movimentos já cadastradas para tal reserva
        SQL.Append("SELECT ")
        SQL.Append("    COUNT(AUTONUM_GD_RESERVA) AS QTDE ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.")
        SQL.Append("TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM_GD_RESERVA = {0} ")

        QtdePeriodo = Banco.ExecutaRetorna(String.Format(SQL.ToString(), Reserva))

        If Limite > QtdePeriodo Then
            Return True
        End If

        Return False

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
    Public Function ConsultarConteineresDDC(ByVal ID As String, ByVal Empresa As Integer, ByVal Conteiner As String) As DataTable

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM, ")
        SQL.Append("    ID_CONTEINER, ")
        SQL.Append("    MAPA_DE_MADEIRA, ")
        SQL.Append("    GR_PAGA, ")
        SQL.Append("    TIPO_DOCUMENTO, ")
        SQL.Append("    SISCARGA, ")
        SQL.Append("    BLOQUEIO_BL, ")
        SQL.Append("    BLOQUEIO_CNTR, ")
        SQL.Append("    FORMA_PAGAMENTO, ")
        SQL.Append("    FLAG_CERTIFICADO, ")
        SQL.Append("    AVERBOU, ")
        SQL.Append("    DI_ICMS_SEFAZ, ")
        SQL.Append("    DI_DESEMBARACADA ")
        SQL.Append("FROM ")
        SQL.Append("( ")
        SQL.Append("    SELECT ")
        SQL.Append("        DISTINCT ")
        SQL.Append("            A.ID_CONTEINER, ")
        SQL.Append("            A.AUTONUM, ")
        SQL.Append("            MIN(D.MAPA) AS MAPA_DE_MADEIRA, ")
        SQL.Append("            CASE WHEN MIN(D.FORMA_PAGAMENTO) = 3 THEN 'FAT' ELSE MIN(D.GR_PAGA) END AS GR_PAGA, ")
        SQL.Append("            C.TIPO_DOCUMENTO, ")
        SQL.Append("            MIN(D.SISCARGA) AS SISCARGA, ")
        SQL.Append("            MIN(D.BLOQUEIO_BL) AS BLOQUEIO_BL, ")
        SQL.Append("            MIN(D.BLOQUEIO_CNTR) AS BLOQUEIO_CNTR, ")
        SQL.Append("            MIN(D.FORMA_PAGAMENTO) AS FORMA_PAGAMENTO, ")
        SQL.Append("            MIN(D.FLAG_CERTIFICADO) AS FLAG_CERTIFICADO, ")
        SQL.Append("            MIN(D.AVERBOU) AS AVERBOU,  ")
        SQL.Append("            MIN(D.ICMS_SEFAZ) AS DI_ICMS_SEFAZ, ")
        SQL.Append("            MIN(D.DESEMBARACADO) AS DI_DESEMBARACADA ")
        SQL.Append("    FROM  ")
        SQL.Append("        SGIPA.TB_CNTR_BL A ")
        SQL.Append("    INNER JOIN  ")
        SQL.Append("        SGIPA.TB_CARGA_SOLTA B ON A.AUTONUM = B.CNTR ")
        SQL.Append("    INNER JOIN ")
        SQL.Append("        SGIPA.TB_BL C ON B.BL = C.AUTONUM ")
        SQL.Append("    INNER JOIN ")
        SQL.Append("        SGIPA.VW_ETAPAS_AV_BL_CNTR D ON C.AUTONUM = D.LOTE ")
        SQL.Append("    INNER JOIN ")
        SQL.Append("        SGIPA.TB_AV_ONLINE E ON C.AUTONUM = E.LOTE ")
        SQL.Append("    INNER JOIN ")
        SQL.Append("        SGIPA.TB_AV_ONLINE_TRANSP F ON E.AUTONUM = F.TB_AV_ONLINE ")
        SQL.Append("    WHERE  ")
        SQL.Append("        B.USUARIO_DDC > 0 ")
        SQL.Append("    AND  ")
        SQL.Append("        B.FLAG_TERMINAL = 1 ")
        SQL.Append("    AND  ")
        SQL.Append("        B.FLAG_HISTORICO = 0 ")
        SQL.Append("    AND  ")
        SQL.Append("        A.SAIDA_DDC IS NULL ")
        SQL.Append("    AND  ")
        SQL.Append("        F.TRANSPORTADORA = " & ID & " ")

        If ID = String.Empty Then
            SQL.Append("AND ")
            SQL.Append("  B.BL NOT IN (SELECT LOTE FROM TB_AG_CS_NF) ")
        End If

        If Lote <> String.Empty Then
            SQL.Append("AND ")
            SQL.Append("  A.AUTONUM = " & Conteiner)
        End If

        If Empresa = 1 Then
            SQL.Append("AND ")
            SQL.Append("  B.PATIO <> 5 ")
        Else
            SQL.Append("AND ")
            SQL.Append("  B.PATIO = 5 ")
        End If

        SQL.Append("    GROUP BY ")
        SQL.Append("        A.AUTONUM, ")
        SQL.Append("        A.ID_CONTEINER, ")
        SQL.Append("        C.TIPO_DOCUMENTO ")
        SQL.Append("  ) WHERE ")
        SQL.Append("      (  ")
        SQL.Append("        AVERBOU = 'SIM' ")
        SQL.Append("      AND  ")
        SQL.Append("        DI_ICMS_SEFAZ = 'SIM' ")
        SQL.Append("      AND  ")
        SQL.Append("        BLOQUEIO_BL = 'NAO' ")
        SQL.Append("      AND  ")
        SQL.Append("        BLOQUEIO_CNTR = 'NAO' ")
        SQL.Append("      AND  ")
        SQL.Append("        DI_DESEMBARACADA = 'SIM' ")
        SQL.Append("      AND  ")
        SQL.Append("        SISCARGA = 'SIM' ")
        SQL.Append("      AND  ")
        SQL.Append("        (MAPA_DE_MADEIRA = 'SIM'  ) ")
        SQL.Append("      AND  ")
        SQL.Append("        (GR_PAGA = 'SIM' OR GR_PAGA = 'FAT' OR FORMA_PAGAMENTO = 3) ")
        SQL.Append("      AND  ")
        SQL.Append("        (TIPO_DOCUMENTO < 7 OR TIPO_DOCUMENTO > 13) ")
        SQL.Append("      )  ")
        SQL.Append("    OR  ")
        SQL.Append("      (  ")
        SQL.Append("        AVERBOU ='SIM' AND MAPA_DE_MADEIRA = 'SIM'")
        SQL.Append("      AND  ")
        SQL.Append("        BLOQUEIO_BL = 'NAO' ")
        SQL.Append("      AND  ")
        SQL.Append("        BLOQUEIO_CNTR = 'NAO' ")
        SQL.Append("      AND  ")
        SQL.Append("        (GR_PAGA = 'SIM' OR GR_PAGA = 'FAT' OR FORMA_PAGAMENTO = 3) ")
        SQL.Append("      AND  ")
        SQL.Append("        (TIPO_DOCUMENTO >=7 AND TIPO_DOCUMENTO <=13) ")
        SQL.Append("      ) ")

        Return Banco.Consultar(String.Format(SQL.ToString(), ID, Empresa))

    End Function

    ''' <summary>
    ''' Consulta Lote de um determinado Agendamento de Carga Solta
    ''' </summary>
    ''' <param name="ID">Autonum do Agendamento</param>
    ''' <returns>Lote do Agendamento; se não tiver retorna -1</returns>
    ''' <remarks>Procura se o lote existe na tabela TB_AG_CS_NF 
    ''' Caso não exista faz uma 2ª procura na tabela TB_AG_CS
    ''' Caso não encontrou, retorna -1</remarks>
    Function ConsultarConteinerAgendamento(ByVal ID As String) As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("  CNTR ")
        SQL.Append("FROM ")
        SQL.Append("  SGIPA.TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("  AUTONUM = {0} ")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), ID))

    End Function

    Public Function ObterDocumentoLote(ByVal Lote As String) As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    TD.DESCR AS TIPODOC ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_BL ")
        SQL.Append(" BL ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    SGIPA.TB_TIPOS_DOCUMENTOS ")
        SQL.Append(" TD ")
        SQL.Append(" ON BL.TIPO_DOCUMENTO = TD.CODE ")
        SQL.Append(" WHERE ")
        SQL.Append("    BL.AUTONUM = {0} ")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), Lote))

    End Function

    Public Function ConsultarCNH(ByVal ID As String) As List(Of String)

        Dim Dt As New DataTable
        Dim ListaCNH As New List(Of String)

        Dt = Banco.Consultar(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE ID_TRANSPORTADORA={0} ORDER BY CNH", ID))

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                ListaCNH.Add(Item("CNH").ToString())
            Next

            Return ListaCNH

        End If

        Return Nothing

    End Function

    Public Function CriarAgendamento(ByVal TransportadoraOBJ As Transportadora) As Integer

        Dim NovoCodigo As Integer = 0

        Banco.Executar("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO IN (SELECT AUTONUM FROM SGIPA.TB_AG_CS WHERE AUTONUM_TRANSPORTADORA=" & TransportadoraOBJ.ID & " AND AUTONUM_VEICULO=0  AND DATA_CAD<SYSDATE-1/72 AND AUTONUM_MOTORISTA = 0 AND AUTONUM_GD_RESERVA = 0 AND LOTE = 0 AND NUM_PROTOCOLO = 0)")
        Banco.Executar("DELETE FROM SGIPA.TB_AG_CS WHERE AUTONUM IN(SELECT AG.AUTONUM FROM SGIPA.TB_AG_CS AG WHERE  AG.AUTONUM_TRANSPORTADORA=" & TransportadoraOBJ.ID & " AND AG.AUTONUM_VEICULO=0  AND DATA_CAD<SYSDATE-1/72 AND AG.AUTONUM_MOTORISTA = 0 AND AG.AUTONUM_GD_RESERVA = 0 AND AG.LOTE = 0 AND AG.NUM_PROTOCOLO = 0)")

        NovoCodigo = Banco.ExecutaRetorna("SELECT SGIPA.SEQ_TB_AG_CS.NEXTVAL SEQ FROM DUAL")

        If Val(NovoCodigo) <> 0 Then
            Banco.Executar("INSERT INTO SGIPA.TB_AG_CS (AUTONUM,AUTONUM_MOTORISTA,AUTONUM_VEICULO,AUTONUM_TRANSPORTADORA,AUTONUM_GD_RESERVA,NUM_PROTOCOLO,ANO_PROTOCOLO,STATUS,IMPRESSO,AUTONUM_USUARIO) VALUES (" & NovoCodigo & ",0,0," & TransportadoraOBJ.ID & ",0,0,0,0,0, " & TransportadoraOBJ.Usuario.UserId & ")")
        End If

        Return NovoCodigo

    End Function

    ''' <summary>
    ''' Verifica se um determinado Agendamento está Finalizado
    ''' </summary>
    ''' <param name="ID">Autonum do Agendamento</param>
    ''' <returns>True: Finalizado, False: Não finalizado</returns>
    ''' <remarks></remarks>
    Public Function AgendamentoFinalizado(ByVal ID As Integer) As Boolean

        Dim Retorno As String = String.Empty
        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(*) AS AGENDAMENTO ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.")
        SQL.Append("TB_AG_CS ")
        SQL.Append("WHERE ")
        SQL.Append("    NVL(AUTONUM_VEICULO, 0) <> 0 AND NVL(AUTONUM_MOTORISTA, 0) <> 0 AND ")
        SQL.Append("    NVL(AUTONUM_GD_RESERVA, 0) <> 0 AND NVL(CNTR, 0) <> 0 ")
        SQL.Append("    AND ")
        SQL.Append("    AUTONUM = {0} ")

        Retorno = Banco.ExecutaRetorna(String.Format(SQL.ToString(), ID))

        If Convert.ToInt16(Retorno) = 0 Then
            Return False
        End If

        Return True

    End Function

    Public Function InserirNF(ByVal Lote As String, ByVal NotaFiscal As String, ByVal Serie As String, ByVal Emissao As String, ByVal CodigoAgendamento As String, ByVal CodProduto As String, Quantidade As String, ByVal Cntr As String) As String

        Dim SQL As New StringBuilder
        Dim ID As String = String.Empty

        ID = Banco.ExecutaRetorna("SELECT SGIPA.SEQ_AG_CS_NF.NEXTVAL AS ID FROM DUAL")

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
        SQL.Append("        QTDE, ")
        SQL.Append("        CNTR, ")
        SQL.Append("        FLAG_DDC ")
        SQL.Append("    ) ")
        SQL.Append(" VALUES")
        SQL.Append("    ( ")
        SQL.Append("        " & ID & " ,")
        SQL.Append("        " & Lote & ",")
        SQL.Append("        '" & NotaFiscal & "',")
        SQL.Append("        '" & Serie & "',")
        SQL.Append("        TO_DATE('" & Emissao & "','DD/MM/YY'),")
        SQL.Append("        " & CodigoAgendamento & ", ")
        SQL.Append("        " & CodProduto & ", ")
        SQL.Append("        " & Quantidade & ",")
        SQL.Append("        " & Cntr & ",")
        SQL.Append("        1")
        SQL.Append("    ) ")

        Try
            Banco.Executar(SQL.ToString())
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return ID

    End Function

    ''' <summary>
    ''' Atualiza uma Nota Fiscal do Agendamento
    ''' </summary>
    Public Function AlterarNF(ByVal Lote As String, ByVal NotaFiscal As String, ByVal Serie As String, ByVal Emissao As String, ByVal CodProduto As String, ByVal Quantidade As String, ByVal AutonumNFiscal As String) As Boolean

        Dim SQL As New StringBuilder

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

        Try
            Banco.Executar(String.Format(SQL.ToString(), Lote, NotaFiscal, Serie, Emissao, Quantidade, CodProduto, AutonumNFiscal))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    '''<summary>
    ''' Consulta dados das Notas Fiscais de um determinado agendamento, inclusive Produtos, embalagens e tipos de documentos
    '''</summary>
    ''' <param name="ID">Autonum do Agendamento CS</param>
    Public Function ConsultarDadosNota(ByVal ID As String) As DataTable

        Dim SQLBuilder As New StringBuilder

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

        Return Banco.Consultar(String.Format(SQLBuilder.ToString(), ID))

    End Function

    Public Function Consultar(ByVal ID As String, ByVal TranspEmpresa As Integer, Optional ByVal Filtro As String = "") As DataTable

        Dim SQL As New StringBuilder

        SQL.Append("SELECT DISTINCT ")
        SQL.Append("    CS.AUTONUM_AG_CS, CS.PROTOCOLO, CS.NOME_MOTORISTA,")
        SQL.Append("    CS.CNH, CS.PLACA_CAVALO, CS.PLACA_CARRETA,")
        SQL.Append("    CS.PERIODO, CS.IMPRESSO, CS.STATUS,")
        SQL.Append("    CS.NUM_DOC_SAIDA, CS.NUMERO_BL, CS.NUM_DOCUMENTO, CS.DESCR_DOCUMENTO,")
        SQL.Append("    TO_CHAR(CS.DT_FREE_TIME,'DD/MM/YYYY') DT_FREE_TIME, CS.DT_CHEGADA, CS.COD_TRANSPORTADORA,")
        SQL.Append("    CS.EMISSAO_DOC_SAIDA, CS.SERIE_DOC_SAIDA, CS.TIPO_DOC_SAIDA,")
        SQL.Append("    CS.COR, CS.MODELO, CS.RENAVAM, CS.LOTE, CS.AUTONUM_GD_RESERVA, CS.ID_CONTEINER, CS.CNTR")
        SQL.Append(" FROM SGIPA.VW_AGENDA_CS CS ")
        SQL.Append("INNER JOIN SGIPA.TB_CNTR_BL B ON CS.CNTR=B.AUTONUM")
        SQL.Append("    WHERE ")
        SQL.Append("    CS.COD_TRANSPORTADORA = {0} ")

        If TranspEmpresa = 1 Then
            SQL.Append("    AND B.PATIO <> 5 ")
        Else
            SQL.Append("    AND B.PATIO = 5 ")
        End If

        SQL.Append("    AND NVL(CS.USUARIO_DDC,0) <> 0 ")

        SQL.Append(Filtro)

        SQL.Append(" GROUP BY ")
        SQL.Append("    CS.AUTONUM_AG_CS, CS.PROTOCOLO, CS.NOME_MOTORISTA,")
        SQL.Append("    CS.CNH, CS.PLACA_CAVALO, CS.PLACA_CARRETA,")
        SQL.Append("    CS.PERIODO, CS.IMPRESSO, CS.STATUS,")
        SQL.Append("    CS.NUM_DOC_SAIDA, CS.NUMERO_BL, CS.NUM_DOCUMENTO, CS.DESCR_DOCUMENTO,")
        SQL.Append("    CS.DT_FREE_TIME, CS.DT_CHEGADA, CS.COD_TRANSPORTADORA,")
        SQL.Append("    CS.EMISSAO_DOC_SAIDA, CS.SERIE_DOC_SAIDA, CS.TIPO_DOC_SAIDA,")
        SQL.Append("    CS.COR, CS.MODELO, CS.RENAVAM, CS.LOTE, CS.AUTONUM_GD_RESERVA,CS.ID_CONTEINER,CS.CNTR")
        SQL.Append(" ORDER BY ")
        SQL.Append("    CS.PROTOCOLO DESC")

        Return Banco.Consultar(String.Format(SQL.ToString(), ID))

    End Function

    Public Function ConsultarDadosAgendamento(ByVal ID As String) As DataTable

        Dim SQL As New StringBuilder

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
        SQL.Append("    C.PLACA_CAVALO, ")
        SQL.Append("    C.PLACA_CARRETA, ")
        SQL.Append("    C.CHASSI, ")
        SQL.Append("    C.TARA, ")
        SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_AG_CS A ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
        SQL.Append("WHERE ")
        SQL.Append("    A.AUTONUM = {0} ")

        Return Banco.Consultar(String.Format(SQL.ToString(), ID))

    End Function





    Public Function ObterCodigoMotorista(ByVal Motorista As Motorista) As Integer
        Return Banco.ExecutaRetorna(String.Format("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH='{0}' AND ID_TRANSPORTADORA = {1}", Motorista.CNH, Motorista.Transportadora.ID))
    End Function

    Public Function Agendar(ByVal AgendamentoOBJ As Agendamento) As Boolean

        Dim SQL As New StringBuilder

        Dim CodigoMotorista As Integer = ObterCodigoMotorista(AgendamentoOBJ.Motorista)

        SQL.Append("UPDATE SGIPA.TB_AG_CS ")
        SQL.Append("    SET ")
        SQL.Append("        AUTONUM_MOTORISTA=" & CodigoMotorista & ", ")
        SQL.Append("        EMISSAO_DOC_SAIDA=TO_DATE('" & AgendamentoOBJ.EmissaoDocSaida & "','DD/MM/YY'), ")
        SQL.Append("        SERIE_DOC_SAIDA='" & AgendamentoOBJ.SerieDocSaida & "', ")
        SQL.Append("        TIPO_DOC_SAIDA='" & AgendamentoOBJ.TipoDocSaida & "', ")
        SQL.Append("        NUM_DOC_SAIDA=" & AgendamentoOBJ.NumDocSaida & ", ")
        SQL.Append("        CNTR=" & AgendamentoOBJ.ConteinerDDC & ", ")
        SQL.Append("        LOTE=0, ")
        SQL.Append("        IMPRESSO=0, ")
        SQL.Append("        STATUS=0, ")
        SQL.Append("        NUM_PROTOCOLO=SGIPA.SEQ_AG_CS_PROT_" & Now.Year & ".NEXTVAL, ")
        SQL.Append("        ANO_PROTOCOLO=" & Now.Year & ", ")
        SQL.Append("        AUTONUM_GD_RESERVA=" & AgendamentoOBJ.Periodo & ", ")
        SQL.Append("        AUTONUM_TRANSPORTADORA=" & AgendamentoOBJ.Transportadora.ID & ", ")
        SQL.Append("        AUTONUM_VEICULO=" & AgendamentoOBJ.Veiculo.ID & ", ")
        SQL.Append("        FLAG_DDC = 1, ")
        SQL.Append("        DATA_AGENDAMENTO = SYSDATE ")
        SQL.Append("    WHERE ")
        SQL.Append("        AUTONUM=" & AgendamentoOBJ.Codigo & "")

        Try
            Banco.Executar(SQL.ToString())
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function AlterarAgendamento(ByVal AgendamentoOBJ As Agendamento) As Boolean

        Dim SQL As New StringBuilder

        Dim CodigoMotorista As Integer = ObterCodigoMotorista(AgendamentoOBJ.Motorista)

        SQL.Append("UPDATE SGIPA.TB_AG_CS ")
        SQL.Append("    SET ")
        SQL.Append("        AUTONUM_MOTORISTA=" & CodigoMotorista & ", ")
        SQL.Append("        EMISSAO_DOC_SAIDA=TO_DATE('" & AgendamentoOBJ.EmissaoDocSaida & "','DD/MM/YY'), ")
        SQL.Append("        SERIE_DOC_SAIDA='" & AgendamentoOBJ.SerieDocSaida & "', ")
        SQL.Append("        TIPO_DOC_SAIDA='" & AgendamentoOBJ.TipoDocSaida & "', ")
        SQL.Append("        NUM_DOC_SAIDA=" & AgendamentoOBJ.NumDocSaida & ", ")
        SQL.Append("        AUTONUM_GD_RESERVA=" & AgendamentoOBJ.Periodo & ", ")
        SQL.Append("        AUTONUM_TRANSPORTADORA=" & AgendamentoOBJ.Transportadora.ID & ", ")
        SQL.Append("        AUTONUM_VEICULO=" & AgendamentoOBJ.Veiculo.ID & ", ")
        SQL.Append("        NUM_PROTOCOLO=SGIPA.SEQ_AG_CS_PROT_" & Now.Year & ".NEXTVAL, ")
        SQL.Append("        ANO_PROTOCOLO=" & Now.Year & " ")
        SQL.Append("    WHERE ")
        SQL.Append("        AUTONUM=" & AgendamentoOBJ.Codigo & "")

        Try
            Banco.Executar(SQL.ToString())
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function CancelarAgendamento(ByVal ID As Integer) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM SGIPA.TB_AG_CS WHERE AUTONUM={0}", ID))
            Banco.Executar(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO={0}", ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function AgendamentoComRegistro(ByVal ID As Integer) As Integer

        Try
            Return Banco.ExecutaRetorna("SELECT COUNT(1) FROM SGIPA.TB_REGISTRO_SAIDA_CS A INNER JOIN TB_CARGA_SOLTA B ON A.CS = B.AUTONUM INNER JOIN TB_AG_CS C ON B.CNTR = C.CNTR WHERE NVL(C.FLAG_DDC,0) > 0 AND C.AUTONUM = " & ID)
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ExcluirItens(ByVal ID As String) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM SGIPA.TB_AG_CS_ITENS WHERE AUTONUM_AGENDAMENTO={0}", ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ExcluirNF(ByVal IDNF As String) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM = {0}", IDNF))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ExcluirNotas(ByVal ID As String) As Boolean

        Try
            Banco.Executar(String.Format("DELETE FROM SGIPA.TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO={0}", ID))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function GerarProtocolo(ByVal ID As String, ByVal TiaEmpresa As Integer) As StringBuilder

        Dim Rst1 As New DataTable
        Dim Rst2 As New DataTable
        Dim Rst3 As New DataTable

        Dim SQL As New StringBuilder

        Dim Estrutura As New StringBuilder
        Dim Tabela1 As New StringBuilder
        Dim Tabela2 As New StringBuilder
        Dim Tabela3 As New StringBuilder
        Dim Tabela4 As New StringBuilder
        Dim TabelaItem As New StringBuilder
        Dim Contador As Integer = 0
        Dim Protocolos As String() = ID.Split(",")

        Dim Empresa As New Empresa

        TiaEmpresa = 1

        For Each Item In Protocolos

            SQL.Clear()
            SQL.Append("SELECT DISTINCT ")
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
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(D.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            SQL.Append("    E.RAZAO, ")
            SQL.Append("    D.PATIO, ")
            SQL.Append("    A.CNTR, ")
            SQL.Append("    CNTR.ID_CONTEINER ")

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
            SQL.Append("    SGIPA.TB_CNTR_BL CNTR ON A.CNTR = CNTR.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM IN ({0}) ")

            Rst1 = Banco.Consultar(String.Format(SQL.ToString(), Item))

            For Each Item1 As DataRow In Rst1.Rows

                Tabela3.Append("<table id=cabecalho>")
                Tabela3.Append("	<tr>")
                Tabela3.Append("		<td align=left width=180px>")
                Tabela3.Append("			<img src=css/ecoporto/images/LogoTop.png />") 'imagens/LogoTop.png />")
                Tabela3.Append("		</td>")
                Tabela3.Append("		<td>")
                Tabela3.Append("		<font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE CARGA SOLTA (DDC)</font>")
                Tabela3.Append("		<br/>")
                Tabela3.Append("        <font face=Arial size=5>Nº: " & Item1("NUM_PROTOCOLO").ToString() & "/" & Item1("ANO_PROTOCOLO").ToString() & "</font>")
                Tabela3.Append("		<br/>")
                Tabela3.Append("        <font face=Arial size=4><b>PÁTIO: " & Item1("PATIO").ToString() & "</b></font>")
                Tabela3.Append("        <br/><br/>")
                Tabela3.Append("        <font face=Arial size=4>" & Item1("PERIODO").ToString() & "</font>")
                Tabela3.Append("		</td>")
                Tabela3.Append("	</tr>")
                Tabela3.Append("</table>")
                Tabela3.Append("<br/>")



                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Dados do Transporte:</caption>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>TRANSPORTADORA</td>")
                Tabela1.Append("        <td>PLACA DO CAVALO</td>")
                Tabela1.Append("        <td>PLACA DA CARRETA</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Item1("RAZAO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("PLACA_CAVALO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("PLACA_CARRETA").ToString() & "</td>")

                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead>")
                Tabela1.Append("        <td>TARA</td>")
                Tabela1.Append("        <td>CHASSI</td>")
                Tabela1.Append("        <td>COR</td>")
                Tabela1.Append("        <td>MODELO</td>")

                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Item1("TARA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("CHASSI").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("COR").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("MODELO").ToString() & "</td>")

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
                Tabela1.Append("        <td>" & Item1("NOME").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("CNH").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("CPF").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("RG").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("ORGAO").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("EMISSAO_RG").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("NEXTEL").ToString() & "</td>")
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

                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Item1("TIPO_DOC_SAIDA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("SERIE_DOC_SAIDA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("NUM_DOC_SAIDA").ToString() & "</td>")
                Tabela1.Append("        <td>" & Item1("EMISSAO_DOC_SAIDA").ToString() & "</td>")

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
                SQL.Append("  C.NUM_DOCUMENTO AS NUMDOC, ")
                SQL.Append("  I.RAZAO || ' ' || CGC  AS IMPORTADOR ")
                SQL.Append("FROM ")
                SQL.Append("  SGIPA.TB_AG_CS_NF A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_AG_CS B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_BL C ON A.LOTE = C.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_TIPOS_DOCUMENTOS D ON C.TIPO_DOCUMENTO = D.CODE ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  SGIPA.TB_CAD_PARCEIROS I ON C.IMPORTADOR = I.AUTONUM ")
                SQL.Append("WHERE ")
                SQL.Append("  B.AUTONUM = " & Item1("AUTONUM").ToString())
                SQL.Append("GROUP BY ")
                SQL.Append("  A.LOTE, ")
                SQL.Append("  A.NOTAFISCAL, ")
                SQL.Append("  A.SERIE, ")
                SQL.Append("  A.EMISSAO, ")
                SQL.Append("  D.DESCR, ")
                SQL.Append("  C.NUM_DOCUMENTO,I.RAZAO,I.CGC ")
                SQL.Append("ORDER BY  ")
                SQL.Append("  A.NOTAFISCAL, ")
                SQL.Append("  A.SERIE, ")
                SQL.Append("  A.EMISSAO, ")
                SQL.Append("  A.LOTE  ")

                Rst2 = Banco.Consultar(String.Format(SQL.ToString()))

                If Rst2.Rows.Count > 0 Then

                    Tabela2.Append("<table>")
                    Tabela2.Append("    <thead>")
                    Tabela2.Append("        <td>LOTE</td>")
                    Tabela2.Append("        <td>IMPORTADOR</td>")
                    Tabela2.Append("        <td>CONTÊINER</td>")
                    Tabela2.Append("        <td>DOCUMENTO</td>")
                    Tabela2.Append("        <td>NOTA FISCAL</td>")
                    Tabela2.Append("        <td>SÉRIE</td>")
                    Tabela2.Append("        <td>EMISSÃO</td>")
                    Tabela2.Append("    </thead>")
                    Tabela2.Append("    <tbody>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("LOTE").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("IMPORTADOR").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Item1("ID_CONTEINER").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("DOCUMENTO").ToString() & "&nbsp;-&nbsp" & Rst2.Rows(0)("NUMDOC").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("NOTAFISCAL").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("SERIE").ToString() & "</td>")
                    Tabela2.Append("        <td>" & Rst2.Rows(0)("EMISSAO").ToString() & "</td>")
                    Tabela2.Append("    </tbody>")
                    TabelaItem.Append("</table>")

                    SQL.Clear()
                    SQL.Append("SELECT ")
                    SQL.Append("    A.AUTONUM,  ")
                    SQL.Append("    A.LOTE,  ")
                    SQL.Append("    A.QTDE AS QUANTIDADE,  ")
                    SQL.Append("    B.MERCADORIA AS PRODUTO, ")
                    SQL.Append("    C.DESCR AS EMBALAGEM ")
                    SQL.Append("FROM  ")
                    SQL.Append("    SGIPA.TB_AG_CS_NF A  ")
                    SQL.Append("INNER JOIN  ")
                    SQL.Append("    SGIPA.TB_CARGA_SOLTA B ON A.AUTONUM_CS = B.AUTONUM  ")
                    SQL.Append("LEFT JOIN  ")
                    SQL.Append("    SGIPA.DTE_TB_EMBALAGENS C ON B.EMBALAGEM = C.CODE  ")
                    SQL.Append("INNER JOIN  ")
                    SQL.Append("    SGIPA.TB_AG_CS D ON A.AUTONUM_AGENDAMENTO = D.AUTONUM  ")
                    SQL.Append("WHERE  ")
                    SQL.Append("    D.CNTR = " & Item1("CNTR").ToString())
                    SQL.Append("AND  ")
                    SQL.Append("    A.AUTONUM_AGENDAMENTO = " & Item1("AUTONUM").ToString())

                    Rst3 = Banco.Consultar(String.Format(SQL.ToString()))

                    If Rst3.Rows.Count > 0 Then

                        TabelaItem.Append("<table class='itens'>")
                        TabelaItem.Append("    <thead>")
                        TabelaItem.Append("        <td>ITEM</td>")
                        TabelaItem.Append("        <td>PRODUTO</td>")
                        TabelaItem.Append("        <td>EMBALAGEM</td>")
                        TabelaItem.Append("        <td>QUANTIDADE</td>")
                        TabelaItem.Append("    </thead>")

                        For Each Item3 As DataRow In Rst3.Rows

                            Contador += 1

                            TabelaItem.Append("    <tbody>")
                            TabelaItem.Append("        <td>" & Contador & "</td>")
                            TabelaItem.Append("        <td>" & Item3("PRODUTO").ToString() & "</td>")
                            TabelaItem.Append("        <td>" & Item3("EMBALAGEM").ToString() & "</td>")
                            TabelaItem.Append("        <td>" & Item3("QUANTIDADE").ToString() & "</td>")
                            TabelaItem.Append("    </tbody>")

                        Next

                        TabelaItem.Append("</table>")
                        TabelaItem.Append("<br/>")

                    End If

                End If

                Tabela2.Append("<tr>")
                Tabela2.Append("<td colspan=9>")
                Tabela2.Append("</td>")
                Tabela2.Append("</tr>")

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
                Tabela4.Append("    </tbody>")
                Tabela4.Append("</table>")
                Tabela4.Append("<br />")

                Estrutura.Append(Tabela3.ToString())
                Estrutura.Append(Tabela1.ToString())
                Estrutura.Append(Tabela2.ToString())
                Estrutura.Append(TabelaItem.ToString())
                Estrutura.Append(Tabela4.ToString())
                Estrutura.Append("<div class=folha></div>")

                Tabela3.Clear()
                Tabela1.Clear()
                Tabela2.Clear()
                Tabela4.Clear() 'Somente necessário para quando vai imprimir vários protocolos de uma só vez
                SQL.Clear()

                AlterarStatusImpressao(Item)

            Next

        Next

        Return Estrutura

    End Function

    Public Function AlterarStatusImpressao(ByVal ID As String) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("UPDATE SGIPA.TB_AG_CS ")
        SQL.Append("    SET ")
        SQL.Append("        IMPRESSO=1")
        SQL.Append("    WHERE ")
        SQL.Append("        AUTONUM=" & ID & "")

        Try
            Banco.Executar(SQL.ToString())
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function ObterPatio(ByVal Lote As String) As String
        Return Banco.ExecutaRetorna(String.Format("SELECT PATIO FROM SGIPA.TB_CNTR_BL WHERE AUTONUM={0}", Lote))
    End Function

    Public Function ConteinerComPendenciaRecebimento(ByVal CodigoPeriodo As String, ByVal Conteiner As String) As Boolean

        Dim SQL As New StringBuilder

        SQL.Clear()
        SQL.Append("SELECT a.autonum, ")
        SQL.Append("       CASE ")
        SQL.Append("              WHEN f.validade < b.periodo_inicial THEN 'NAO' ")
        SQL.Append("              ELSE 'OK' ")
        SQL.Append("       END AS valido ")
        SQL.Append("FROM   sgipa.tb_bl a, ")
        SQL.Append("       operador.tb_gd_reserva b, ")
        SQL.Append("       ( ")
        SQL.Append("              SELECT fat.bl, ")
        SQL.Append("                     to_date ( to_char ( ( nvl ( dt_base_calculo_reefer,validade_gr ) ) + 1,'DD/MM/YYYY' ) ")
        SQL.Append("                            || ' 00:00:01', 'DD/MM/YYYY HH24:MI:SS' ) AS validade ")
        SQL.Append("              FROM   sgipa.tb_gr_bl , ")
        SQL.Append("                     ( ")
        SQL.Append("                              SELECT   a.bl, ")
        SQL.Append("                                       max ( a.seq_gr ) seq_gr ")
        SQL.Append("                              FROM     sgipa.tb_servicos_faturados a, ")
        SQL.Append("                                       sgipa.tb_gr_bl b ")
        SQL.Append("                              WHERE    a.seq_gr = b.seq_gr ")
        SQL.Append("                                       AND ( ")
        SQL.Append("                                                servico = 52 ")
        SQL.Append("                                                OR servico = 71 ")
        SQL.Append("                                                OR servico = 57 ) ")
        SQL.Append("                                       AND status_gr = 'IM' AND a.bl IN ( select bl FROM sgipa.tb_amr_cntr_bl WHERE cntr={1} ) ")
        SQL.Append("                                        AND b.bl IN ( select bl FROM sgipa.tb_amr_cntr_bl WHERE cntr={1} ) ")
        SQL.Append("                              GROUP BY a.bl ")
        SQL.Append("                     ) fat ")
        SQL.Append("              WHERE  ( ")
        SQL.Append("                            tb_gr_bl.seq_gr = fat.seq_gr ")
        SQL.Append("                            AND tb_gr_bl.bl = fat.bl ) ")
        SQL.Append("                     AND status_gr = 'IM' AND tb_gr_bl.bl IN ( select bl FROM sgipa.tb_amr_cntr_bl WHERE cntr={1} ) ")
        SQL.Append("       ) f ")
        SQL.Append("WHERE  a.flag_ativo = 1 and a.autonum = f.bl(+) ")
        SQL.Append("       AND b.autonum_gd_reserva = nvl ( {0}, 0 ) ")
        SQL.Append("       AND a.autonum IN ( select bl FROM sgipa.tb_amr_cntr_bl WHERE cntr={1} ) ")

        Dim Dt As New DataTable
        Dt = Banco.Consultar(String.Format(SQL.ToString(), CodigoPeriodo, Conteiner))

        If Dt.Rows.Count > 0 Then
            If Dt.Rows(0)("VALIDO").ToString() = "OK" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ConsultarFormaPagamento(ByVal Conteiner As String) As Integer

        Dim SQL As New StringBuilder

        'SQL.Append("SELECT ")
        'SQL.Append("    FORMA_PAGAMENTO ")
        'SQL.Append("FROM ")
        'SQL.Append("    (SELECT FAT.BL, FORMA_PAGAMENTO ")
        'SQL.Append("    FROM ")
        'SQL.Append("	    SGIPA.TB_GR_BL, ")
        'SQL.Append("        (SELECT   A.BL, MAX (A.SEQ_GR) SEQ_GR ")
        'SQL.Append("        FROM ")
        'SQL.Append("            SGIPA.TB_SERVICOS_FATURADOS A, SGIPA.TB_GR_BL B ")
        'SQL.Append("        WHERE ")
        'SQL.Append("            A.SEQ_GR = B.SEQ_GR ")
        'SQL.Append("			AND ")
        'SQL.Append("            (SERVICO = 52 OR SERVICO = 71) ")
        'SQL.Append("            AND ")
        'SQL.Append("			(STATUS_GR = 'IM' OR STATUS_GR = 'GE') ")
        'SQL.Append("		GROUP BY A.BL) FAT ")
        'SQL.Append("    WHERE ")
        'SQL.Append("        TB_GR_BL.SEQ_GR = FAT.SEQ_GR ")
        'SQL.Append("	    AND ")
        'SQL.Append("        TB_GR_BL.BL = FAT.BL ")
        'SQL.Append("	    AND ")
        'SQL.Append("		(STATUS_GR = 'IM' OR STATUS_GR = 'GE')) F ")
        'SQL.Append("WHERE ")
        'SQL.Append("    F.BL=NVL({0},0) ")

        SQL.Append("SELECT ")
        SQL.Append("    FORMA_PAGAMENTO ")
        SQL.Append("FROM ")
        SQL.Append("    (")
        SQL.Append("    SELECT ")
        SQL.Append("        FAT.BL, FORMA_PAGAMENTO ")
        SQL.Append("    FROM ")
        SQL.Append("    SGIPA.TB_GR_BL, ")
        SQL.Append("    (SELECT A.BL, MAX(A.SEQ_GR) SEQ_GR ")
        SQL.Append("        FROM ")
        SQL.Append("        SGIPA.TB_SERVICOS_FATURADOS A, ")
        SQL.Append("        SGIPA.TB_GR_BL B ")
        SQL.Append("        WHERE ")
        SQL.Append("            A.SEQ_GR = B.SEQ_GR ")
        SQL.Append("            AND ")
        SQL.Append("            (SERVICO = 52 OR SERVICO = 71) ")
        SQL.Append("            AND ")
        SQL.Append("            (STATUS_GR = 'IM' OR STATUS_GR = 'GE') ")
        SQL.Append("            GROUP BY ")
        SQL.Append("                A.BL) FAT ")
        SQL.Append("        WHERE ")
        SQL.Append("            TB_GR_BL.SEQ_GR=FAT.SEQ_GR ")
        SQL.Append("            AND ")
        SQL.Append("            TB_GR_BL.BL = FAT.BL ")
        SQL.Append("            AND ")
        SQL.Append("            (STATUS_GR = 'IM' OR STATUS_GR = 'GE') ")
        SQL.Append("    ) F ")
        SQL.Append("    WHERE F.BL IN ")
        SQL.Append("        (SELECT BL ")
        SQL.Append("        FROM ")
        SQL.Append("        SGIPA.TB_AMR_CNTR_BL ")
        SQL.Append("        WHERE ")
        SQL.Append("            CNTR IN ")
        SQL.Append("                (SELECT AUTONUM FROM ")
        SQL.Append("                    SGIPA.TB_CNTR_BL ")
        SQL.Append("                WHERE ")
        SQL.Append("                AUTONUM = NVL({0}, 0)))")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), Conteiner))

    End Function

    Public Function ObterParametroRetirada() As String
        Return Banco.ExecutaRetorna("SELECT B.MOTIVO_RETIRADA_DDC FROM SGIPA.TB_PARAMETROS_SISTEMA B,SGIPA.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_RETIRADA_DDC=A.CODE")
    End Function

    Public Function InsereAgendamentoNaFila(ByVal Conteiner As String, ByVal Periodo As String, ByVal UserId As Integer) As Boolean

        Dim Dt As New DataTable
        Dim SQL As New StringBuilder

        Dim Data_Prevista As String = String.Empty
        Dim Codigo_Periodo As String = String.Empty

        Dim Motivo As String = ObterParametroRetirada()

        Dt = Banco.Consultar(String.Format("SELECT AUTONUM_GD_RESERVA,TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI:SS') PERIODO_INICIAL FROM OPERADOR.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA={0}", Periodo))

        If Dt.Rows.Count > 0 Then
            Data_Prevista = Dt.Rows(0)("PERIODO_INICIAL").ToString()
        End If

        Dt = Banco.Consultar(String.Format("SELECT A.AUTONUM FROM SGIPA.TB_AGENDAMENTO_POSICAO A, SGIPA.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.CNTR={0} AND B.MOTIVO_POSICAO={1}", Conteiner, Motivo))

        If Dt.Rows.Count > 0 Then
            Codigo_Periodo = Dt.Rows(0)("AUTONUM").ToString()
            Banco.Executar(String.Format("UPDATE SGIPA.TB_AGENDAMENTO_POSICAO SET DT_PREVISTA=TO_DATE('{0}','DD/MM/YYYY HH24:MI:SS'), DATA_ATUALIZA=SYSDATE WHERE AUTONUM={1}", Data_Prevista, Codigo_Periodo))
            Return True
        Else

            Dim ID As String = String.Empty

            ID = Banco.ExecutaRetorna("SELECT SGIPA.SEQ_AGENDAMENTO_POSICAO.NEXTVAL AS ID FROM DUAL")

            If Val(ID) <> 0 Then
                Banco.Executar(String.Format("INSERT INTO SGIPA.TB_AGENDAMENTO_POSICAO (AUTONUM,CNTR,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS,NUM_PROTOCOLO_INTERNET, IUSID) VALUES ({0},{1},TO_DATE('{2}','DD/MM/YYYY HH24:MI:SS'),SYSDATE,0,SGIPA.SEQ_OS" & Now.Year & ".NEXTVAL," & Now.Year & ", " & "SGIPA.SEQ_PROTOCOLO_AGENDA_POSICIONA.NEXTVAL || '/' || TO_CHAR(SYSDATE,'YYYY'), " & UserId & " )", ID, Conteiner, Data_Prevista))
                Banco.Executar(String.Format("INSERT INTO SGIPA.TB_AGENDA_POSICAO_MOTIVO (AUTONUM,AUTONUM_AGENDA_POSICAO,MOTIVO_POSICAO) VALUES (SGIPA.SEQ_AGENDA_POSICAO_MOTIVO.NEXTVAL,{0},{1}) ", ID, Motivo))
            End If

            Return True

        End If

        Return False

    End Function

    Public Function AlterarDataSaida(ByVal Codigo As String) As Boolean

        Try
            Banco.Executar(String.Format("UPDATE SGIPA.TB_ag_cs SET DT_AGENDA_SAIDA = SYSDATE WHERE AUTONUM = {0}", Codigo))
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Public Function NFObrigatoria(ByVal Lote As String) As Boolean

        Dim SQL As New StringBuilder
        Dim Valida As String = String.Empty

        SQL.Append("    SELECT DISTINCT ")
        SQL.Append("    TD.FLAG_NF_AV_ONLINE LIBERA")
        SQL.Append("    FROM SGIPA.TB_BL C,  ")
        SQL.Append("         SGIPA.TB_TIPOS_DOCUMENTOS TD  ")
        SQL.Append("    WHERE  ")
        SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
        SQL.Append("    AND  ")
        SQL.Append("    C.AUTONUM={0}  ")

        Valida = Banco.ExecutaRetorna(String.Format(SQL.ToString(), Lote))

        If Val(Valida) > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Function ObterCNHPeloIdAgendamento(ByVal ID As String) As String

        Dim Sql As New StringBuilder

        Sql.Append("    select m.CNH from sgipa.tb_ag_cs acs")
        Sql.Append("    inner join ")
        Sql.Append("    operador.tb_ag_motoristas m")
        Sql.Append("    on acs.AUTONUM_MOTORISTA = M.AUTONUM")
        Sql.Append("    AND")
        Sql.Append("    ACS.AUTONUM = '{0}'")

        Return Banco.ExecutaRetorna(String.Format(Sql.ToString(), ID))

    End Function

End Class
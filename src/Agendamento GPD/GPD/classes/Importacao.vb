Imports System.Data.OleDb
Public Class Importacao

    Private Patio As String
    Private Flag_VIP As String
    Private Lote As String
    Private Tipo_Doc As String
    Private Data_Max As String
    Private Data_Ref As Date
    Private Limite As Integer
    Private VIP As String
    Private DTA As String
    Private _Autonum As String
    Private _Tipo_doc_saida As String
    Private _Num_doc_saida As String
    Private _Serie_doc_saida As String
    Private _Emissao_doc_saida As String
    Private _Id_Conteiner As String
    Private _AutonumMotorista As String
    Private _AutonumVeiculo As String
    Private _Num_Protocolo As String
    Private _Ano_Protocolo As String
    Private _Nome As String
    Private _CNH As String
    Private _Placa_Cavalo As String
    Private _Placa_carreta As String
    Private _Tara As String
    Private _Chassi As String
    Private _ID_Periodo As String
    Private _Periodo As String
    Private _VIP As String
    Private _Patio As String
    Private _DTA As String
    Private _FreeTime As String
    Private _Lote As String
    Private _Liberado As String

    Public Property Autonum() As String
        Get
            Return Me._Autonum
        End Get
        Set(ByVal value As String)
            Me._Autonum = value
        End Set
    End Property

    Public Property Tipo_doc_saida() As String
        Get
            Return Me._Tipo_doc_saida
        End Get
        Set(ByVal value As String)
            Me._Tipo_doc_saida = value
        End Set
    End Property

    Public Property Num_doc_saida() As String
        Get
            Return Me._Num_doc_saida
        End Get
        Set(ByVal value As String)
            Me._Num_doc_saida = value
        End Set
    End Property

    Public Property Serie_doc_saida() As String
        Get
            Return Me._Serie_doc_saida
        End Get
        Set(ByVal value As String)
            Me._Serie_doc_saida = value
        End Set
    End Property

    Public Property Emissao_doc_saida() As String
        Get
            Return Me._Emissao_doc_saida
        End Get
        Set(ByVal value As String)
            Me._Emissao_doc_saida = value
        End Set
    End Property

    Public Property Id_Conteiner() As String
        Get
            Return Me._Id_Conteiner
        End Get
        Set(ByVal value As String)
            Me._Id_Conteiner = value
        End Set
    End Property

    Public Property Num_Protocolo() As String
        Get
            Return Me._Num_Protocolo
        End Get
        Set(ByVal value As String)
            Me._Num_Protocolo = value
        End Set
    End Property

    Public Property AutonumMotorista() As String
        Get
            Return Me._AutonumMotorista
        End Get
        Set(ByVal value As String)
            Me._AutonumMotorista = value
        End Set
    End Property

    Public Property AutonumVeiculo() As String
        Get
            Return Me._AutonumVeiculo
        End Get
        Set(ByVal value As String)
            Me._AutonumVeiculo = value
        End Set
    End Property

    Public Property Ano_Protocolo() As String
        Get
            Return Me._Ano_Protocolo
        End Get
        Set(ByVal value As String)
            Me._Ano_Protocolo = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return Me._Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property

    Public Property CNH() As String
        Get
            Return Me._CNH
        End Get
        Set(ByVal value As String)
            Me._CNH = value
        End Set
    End Property

    Public Property Placa_Cavalo() As String
        Get
            Return Me._Placa_Cavalo
        End Get
        Set(ByVal value As String)
            Me._Placa_Cavalo = value
        End Set
    End Property

    Public Property Placa_carreta() As String
        Get
            Return Me._Placa_carreta
        End Get
        Set(ByVal value As String)
            Me._Placa_carreta = value
        End Set
    End Property

    Public Property Tara() As String
        Get
            Return Me._Tara
        End Get
        Set(ByVal value As String)
            Me._Tara = value
        End Set
    End Property

    Public Property Chassi() As String
        Get
            Return Me._Chassi
        End Get
        Set(ByVal value As String)
            Me._Chassi = value
        End Set
    End Property

    Public Property ID_Periodo() As String
        Get
            Return Me._ID_Periodo
        End Get
        Set(ByVal value As String)
            Me._ID_Periodo = value
        End Set
    End Property

    Public Property Periodo() As String
        Get
            Return Me._Periodo
        End Get
        Set(ByVal value As String)
            Me._Periodo = value
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

    Public Property pLote() As String
        Get
            Return Me._Lote
        End Get
        Set(ByVal value As String)
            Me._Lote = value
        End Set
    End Property

    Public Property pLiberado() As String
        Get
            Return Me._Liberado
        End Get
        Set(ByVal value As String)
            Me._Liberado = value
        End Set
    End Property

    Public Function ConsultarVeiculosMotoristas(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("   AUTONUM, ")
            SQL.Append("   PLACA_CAVALO || ' - ' || PLACA_CARRETA || ' - ' ||  NOME AS NOME ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.TB_AG_MOTORISTAS ")
            SQL.Append("WHERE ")
            SQL.Append("   (ID_TRANSPORTADORA = {0}) AND ")
            SQL.Append("   (AUTONUM NOT IN (SELECT AUTONUM_GD_MOT FROM SGIPA.TB_GD_IMP_AMR)) ")
            SQL.Append("ORDER BY ")
            SQL.Append("   PLACA_CAVALO ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("   AUTONUM, ")
            SQL.Append("   PLACA_CAVALO + ' - ' + PLACA_CARRETA + ' - ' +  NOME AS NOME ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.DBO.TB_AG_MOTORISTAS ")
            SQL.Append("WHERE ")
            SQL.Append("   (ID_TRANSPORTADORA = {0}) AND ")
            SQL.Append("   (AUTONUM NOT IN (SELECT AUTONUM_GD_MOT FROM SGIPA.DBO.TB_GD_IMP_AMR)) ")
            SQL.Append("ORDER BY ")
            SQL.Append("   PLACA_CAVALO ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_MOTORISTAS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ObterDadosConteiner(ByVal Conteiner As String) As Importacao

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    VIP, ")
            SQL.Append("    PATIO,  ")
            SQL.Append("    ID_CONTEINER, ")
            SQL.Append("    TIPO_DOCUMENTO AS DTA, ")
            SQL.Append("    DATA_FREE_TIME, ")
            SQL.Append("    LOTE ")
            SQL.Append("FROM (  ")
            SQL.Append("    SELECT ")
            SQL.Append("        NVL(K.FLAG_RETIRADA_VIP,0) AS VIP, ")
            SQL.Append("        A.PATIO,  ")
            SQL.Append("        A.ID_CONTEINER, ")
            SQL.Append("        TD.DESCR AS TIPO_DOCUMENTO, ")
            SQL.Append("        P.VALIDADE_GR AS DATA_FREE_TIME, ")
            SQL.Append("        C.AUTONUM AS LOTE ")
            SQL.Append("    FROM SGIPA.TB_CNTR_BL A,  ")
            SQL.Append("         SGIPA.TB_AMR_CNTR_BL B, ")
            SQL.Append("         SGIPA.TB_BL C,  ")
            SQL.Append("         SGIPA.TB_AV_ONLINE D, ")
            SQL.Append("         SGIPA.TB_AV_ONLINE_TRANSP E, ")
            SQL.Append("         SGIPA.TB_TIPOS_DOCUMENTOS TD,  ")
            SQL.Append("         OPERADOR.VW_DADOS_EI V,  ")
            SQL.Append("         SGIPA.DTE_TB_TIPOS_CONTEINER TC, ")
            SQL.Append("         (select AUTONUM,TEMP_REMOCOES,TEMP_RUA from OPERADOR.TB_REMOCAO where SISTEMA='I') RE, ")
            SQL.Append("         SGIPA.VW_ETAPAS_AV_BL_CNTR F, ")
            SQL.Append("         SGIPA.TB_GR_PRE_CALCULO P,  ")
            SQL.Append("         SGIPA.TB_CAD_PARCEIROS K  ")
            SQL.Append("    WHERE  ")
            SQL.Append("         A.AUTONUM = B.CNTR  ")
            SQL.Append("    AND  ")
            SQL.Append("         B.BL = C.AUTONUM  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.AUTONUM=F.LOTE  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.FLAG_ATIVO = 1  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.AUTONUM=RE.AUTONUM(+)  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.AUTONUM = D.LOTE  ")
            SQL.Append("    AND  ")
            SQL.Append("         D.AUTONUM = E.TB_AV_ONLINE  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.AUTONUM=V.CNTR_IPA (+)  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.TIPO=TC.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_DESOVADO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_TERMINAL = 1  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_HISTORICO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("        C.AUTONUM = P.BL  ")
            SQL.Append("    AND  ")
            SQL.Append("        C.IMPORTADOR = K.AUTONUM   ")
            SQL.Append("    GROUP BY  ")
            SQL.Append("         C.AUTONUM,  ")
            SQL.Append("         K.FLAG_RETIRADA_VIP,  ")
            SQL.Append("         A.PATIO, ")
            SQL.Append("         A.ID_CONTEINER,  ")
            SQL.Append("         TD.DESCR, ")
            SQL.Append("         P.VALIDADE_GR ")
            SQL.Append("     )  ")
            SQL.Append("WHERE   ")
            SQL.Append("    ID_CONTEINER='{0}'  ")
        Else

        End If

        Rst.Open(String.Format(SQL.ToString(), Conteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim ImportacaoOBJ As New Importacao

            ImportacaoOBJ.pVIP = Rst.Fields("VIP").Value.ToString()
            ImportacaoOBJ.pPatio = Rst.Fields("PATIO").Value.ToString()
            ImportacaoOBJ.pDTA = Rst.Fields("DTA").Value.ToString()
            ImportacaoOBJ.pFreeTime = Rst.Fields("DATA_FREE_TIME").Value.ToString()
            ImportacaoOBJ.pLote = Rst.Fields("LOTE").Value.ToString()

            Return ImportacaoOBJ

        End If

        Return Nothing

    End Function

    Public Function AgendamentoMesmoPatioPeriodo(ByVal CodigoTransportadora As Integer, ByVal CodigoVeiculo As Integer, ByVal PeriodoInicial As String, ByVal PeriodoFinal As String, ByVal Patio As Integer, Optional ByVal CodigoAgendamento As Integer = 0) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(1) AS TOTAL ")
        SQL.Append("FROM  ")
        SQL.Append("    TB_CNTR_BL A ")
        SQL.Append("INNER JOIN  ")
        SQL.Append("    OPERADOR.TB_GD_RESERVA B ON A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA ")
        SQL.Append("WHERE  ")
        SQL.Append("    A.AUTONUM_TRANSPORTE_AGENDA = {0} ")
        SQL.Append("AND  ")
        SQL.Append("    AUTONUM_VEICULO = {1} ")
        SQL.Append("AND  ")
        SQL.Append("   A.PATIO <> {2} ")
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

    Public Function NFObrigatoria(ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Valida As Boolean

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    SELECT DISTINCT ")
            SQL.Append("    TD.FLAG_NF_AV_ONLINE LIBERA")
            SQL.Append("    FROM SGIPA.TB_CNTR_BL A,  ")
            SQL.Append("         SGIPA.TB_AMR_CNTR_BL B, ")
            SQL.Append("         SGIPA.TB_BL C,  ")
            SQL.Append("         SGIPA.TB_TIPOS_DOCUMENTOS TD  ")
            SQL.Append("    WHERE  ")
            SQL.Append("         A.AUTONUM = B.CNTR  ")
            SQL.Append("    AND  ")
            SQL.Append("         B.BL = C.AUTONUM  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_DESOVADO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_TERMINAL = 1  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_HISTORICO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("    A.AUTONUM={0}  ")
        Else
            SQL.Append("    SELECT DISTINCT ")
            SQL.Append("    TD.FLAG_NF_AV_ONLINE LIBERA ")
            SQL.Append("    FROM DBO.SGIPA.TB_CNTR_BL A,  ")
            SQL.Append("         DBO.SGIPA.TB_AMR_CNTR_BL B, ")
            SQL.Append("         DBO.SGIPA.TB_BL C,  ")
            SQL.Append("         DBO.SGIPA.TB_TIPOS_DOCUMENTOS TD  ")
            SQL.Append("    WHERE  ")
            SQL.Append("         A.AUTONUM = B.CNTR  ")
            SQL.Append("    AND  ")
            SQL.Append("         B.BL = C.AUTONUM  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.TIPO_DOCUMENTO = TD.CODE  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_DESOVADO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_TERMINAL = 1  ")
            SQL.Append("    AND  ")
            SQL.Append("         A.FLAG_HISTORICO = 0  ")
            SQL.Append("    AND  ")
            SQL.Append("         C.FLAG_ATIVO = 1  ")
            SQL.Append("    AND  ")
            SQL.Append("    A.AUTONUM={0}  ")
        End If
        Valida = True
        Rst.Open(String.Format(SQL.ToString(), Conteiner), Banco.Conexao, 3, 3)
        If Not Rst.EOF Then
            If Rst.Fields("LIBERA").Value.ToString() <> 1 Then
                Valida = False
            End If
        End If
        Return Valida

    End Function

    Public Function ConteinerComPendenciaRecebimento(ByVal CodigoPeriodo As String, ByVal CodigoConteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("select count(1) contar from tb_servicos_faturados a inner join tb_gr_pre_calculo b on a.bl=b.bl where a.bl IN ( select bl FROM sgipa.tb_amr_cntr_bl WHERE cntr=" & CodigoConteiner & " ) and a.seq_gr is null and formapagamento=2")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Convert.ToInt32(Rst.Fields("contar").Value.ToString()) > 0 Then
            Return True
        End If

        If Rst.State = 1 Then
            Rst.Close()
        End If

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

        Rst.Open(String.Format(SQL.ToString(), CodigoPeriodo, CodigoConteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Rst.Fields("VALIDO").Value.ToString() = "OK" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ConsultarFormaPagamento(ByVal CodigoConteiner As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
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
        Else
            SQL.Append("SELECT ")
            SQL.Append("    FORMA_PAGAMENTO ")
            SQL.Append("FROM ")
            SQL.Append("    (")
            SQL.Append("    SELECT ")
            SQL.Append("        FAT.BL, FORMA_PAGAMENTO ")
            SQL.Append("    FROM ")
            SQL.Append("    SGIPA.DBO.TB_GR_BL, ")
            SQL.Append("    (SELECT A.BL, MAX(A.SEQ_GR) SEQ_GR ")
            SQL.Append("        FROM ")
            SQL.Append("        SGIPA.DBO.TB_SERVICOS_FATURADOS A, ")
            SQL.Append("        SGIPA.DBO.TB_GR_BL B ")
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
            SQL.Append("        SGIPA.DBO.TB_AMR_CNTR_BL ")
            SQL.Append("        WHERE ")
            SQL.Append("            CNTR IN ")
            SQL.Append("                (SELECT AUTONUM FROM ")
            SQL.Append("                    SGIPA.DBO.TB_CNTR_BL ")
            SQL.Append("                WHERE ")
            SQL.Append("                AUTONUM = NVL({0}, 0)))")
        End If

        Rst.Open(String.Format(SQL.ToString(), CodigoConteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Convert.ToInt32(Rst.Fields("FORMA_PAGAMENTO").Value.ToString())
        End If

        Return False

    End Function

    Public Function ConsultarPeriodos(ByVal PeriodoOBJ As Importacao) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim ID_Periodo_Antigo As String = String.Empty

        SQL.Append("SELECT ")
        SQL.Append("    C.PATIO, ")
        SQL.Append("    C.TAMANHO, ")
        SQL.Append("    B.AUTONUM, ")
        SQL.Append("    C.AUTONUM_GD_RESERVA, ")
        SQL.Append("    NVL(P.FLAG_RETIRADA_VIP,0) AS VIP, ")
        SQL.Append("    C.ID_CONTEINER, ")
        SQL.Append("    NVL(B.TIPO_DOCUMENTO,0) AS TIPO_DOC, ")
        SQL.Append("    to_char(nvl(c.dt_fim_periodo+0.99,sysdate+100),'DD/MM/YYYY HH24:MI:SS') AS DATA_MAX ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_CNTR_BL C, ")
        SQL.Append("    SGIPA.TB_BL B, ")
        SQL.Append("    SGIPA.TB_AMR_CNTR_BL A, ")
        SQL.Append("    SGIPA.TB_CAD_PARCEIROS P, ")
        SQL.Append("    SGIPA.TB_GR_BL D ")
        SQL.Append("WHERE ")
        SQL.Append("    C.AUTONUM=A.CNTR ")
        SQL.Append("AND ")
        SQL.Append("    A.BL=B.AUTONUM ")
        SQL.Append("AND ")
        SQL.Append("    B.IMPORTADOR=P.AUTONUM(+) ")
        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM=D.BL(+) ")
        SQL.Append("AND ")
        SQL.Append("    B.FLAG_ATIVO=1 ")
        SQL.Append("AND ")
        SQL.Append("    C.AUTONUM={0} ")

        Rst.Open(String.Format(SQL.ToString(), PeriodoOBJ.Id_Conteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Patio = Rst.Fields("PATIO").Value.ToString()
            Lote = Rst.Fields("AUTONUM").Value.ToString()
            Flag_VIP = Rst.Fields("VIP").Value.ToString()
            Tipo_Doc = Rst.Fields("TIPO_DOC").Value.ToString()
            Data_Max = Rst.Fields("DATA_MAX").Value.ToString()
            ID_Periodo_Antigo = Rst.Fields("AUTONUM_GD_RESERVA").Value.ToString()

            Rst.Close()

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

            If PeriodoOBJ.pDTA.Contains("DTA") Then
                DTA = "0"
                Limite = -9999
            Else
                DTA = "1"
                Limite = 0
            End If

            SQL.Clear()

            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM_GD_RESERVA, ")
                SQL.Append("    TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
                SQL.Append("    TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
                SQL.Append("    TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT (B.AUTONUM) FROM SGIPA.TB_CNTR_BL B WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA),'000') AS SALDO, ")
                SQL.Append("    A.FLAG_DTA, ")
                SQL.Append("    A.PERIODO_INICIAL AS PERINICIAL_DATE ")
                SQL.Append("FROM ")
                SQL.Append("    OPERADOR.TB_GD_RESERVA A ")
                SQL.Append("WHERE ")
                SQL.Append("    A.LIMITE_MOVIMENTOS - (SELECT NVL(COUNT(B.AUTONUM),0) FROM SGIPA.TB_CNTR_BL B WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA) > " & Limite & " ")
                SQL.Append("AND ")
                SQL.Append("    A.PERIODO_INICIAL > TO_DATE('" & Format(Data_Ref, "dd/MM/yyyy HH:mm") & "','DD/MM/YYYY HH24:MI') ")
                SQL.Append("AND ")
                SQL.Append("    A.PERIODO_FINAL <= TO_DATE('" & Data_Max & "','DD/MM/YYYY HH24:MI:SS') ")
                SQL.Append("AND ")
                SQL.Append("    A.FLAG_VIP <= " & Flag_VIP & " ")
                SQL.Append("AND ")
                SQL.Append("    A.FLAG_DTA <= " & DTA & " ")
                SQL.Append("AND ")
                SQL.Append("    A.PATIO = " & Patio & " ")
                SQL.Append("AND ")
                SQL.Append("    A.SERVICO_GATE = 'I' ")

                If Not String.IsNullOrEmpty(ID_Periodo_Antigo) Then

                    SQL.Append("    UNION ")

                    SQL.Append("SELECT ")
                    SQL.Append("    Z.AUTONUM_GD_RESERVA, ")
                    SQL.Append("    TO_CHAR(Z.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
                    SQL.Append("    TO_CHAR(Z.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
                    SQL.Append("    TO_CHAR(Z.LIMITE_MOVIMENTOS - (SELECT COUNT (B.AUTONUM) FROM SGIPA.TB_CNTR_BL B WHERE Z.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA),'000') AS SALDO, ")
                    SQL.Append("    Z.FLAG_DTA, ")
                    SQL.Append("    Z.PERIODO_INICIAL AS PERINICIAL_DATE ")
                    SQL.Append("FROM ")
                    SQL.Append("    OPERADOR.TB_GD_RESERVA Z ")
                    SQL.Append("WHERE ")
                    SQL.Append("    Z.LIMITE_MOVIMENTOS - (SELECT NVL(COUNT(B.AUTONUM),0) FROM SGIPA.TB_CNTR_BL B WHERE Z.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA) > " & Limite & " ")
                    SQL.Append("AND ")
                    SQL.Append("   Z.AUTONUM_GD_RESERVA = " & ID_Periodo_Antigo & " ")

                End If

                SQL.Append("ORDER BY ")
                SQL.Append("    6 ")

            Else
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM_GD_RESERVA, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT (B.AUTONUM) FROM SGIPA.TB_CNTR_BL B WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA),'000') AS SALDO, ")
                SQL.Append("    A.FLAG_DTA ")
                SQL.Append("FROM ")
                SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA A ")
                SQL.Append("WHERE ")
                SQL.Append("    A.LIMITE_MOVIMENTOS - (SELECT NVL(COUNT(B.AUTONUM),0) FROM SGIPA.DBO.TB_CNTR_BL B WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA) > " & Limite & " ")
                SQL.Append("AND ")
                SQL.Append("    A.PERIODO_INICIAL > OPERADOR.DBO.TO_DATE('" & Format(Data_Ref, "dd/MM/yyyy HH:mm") & "','DD/MM/YYYY HH24:MI') ")
                SQL.Append("AND ")
                SQL.Append("    A.PERIODO_FINAL <= OPERADOR.DBO.TO_DATE('" & Data_Max & "','DD/MM/YYYY HH24:MI:SS') ")
                SQL.Append("AND ")
                SQL.Append("    A.FLAG_VIP = " & Flag_VIP & " ")
                SQL.Append("AND ")
                SQL.Append("    A.FLAG_DTA = " & DTA & " ")
                SQL.Append("AND ")
                SQL.Append("    A.PATIO = " & Patio & " ")
                SQL.Append("AND ")
                SQL.Append("    A.SERVICO_GATE = 'I' ")
                SQL.Append("ORDER BY ")
                SQL.Append(" A.PERIODO_INICIAL ")
            End If

            Rst.Close()
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        End If

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_RESERVA")
            Return Ds.Tables(0)
        End Using

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

    Public Function VerificarSaldo(ByVal ID_Periodo As String, ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT(B.AUTONUM) AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_CNTR_BL B ")
            SQL.Append("WHERE ")
            SQL.Append("    B.AUTONUM <> " & ID & " ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUM_GD_RESERVA=B.AUTONUM_GD_RESERVA),'000') AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA={0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT(B.AUTONUM) AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.DBO.TB_CNTR_BL B ")
            SQL.Append("WHERE ")
            SQL.Append("    B.AUTONUM <> " & ID & " ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUM_GD_RESERVA=B.AUTONUM_GD_RESERVA),'000') AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA={0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID_Periodo), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Convert.ToInt32(Rst.Fields("SALDO").Value.ToString()) > 0 Or VIP = "-9999" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ConsultarPlacasCavalo(ByVal ID As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim ListaCavalo As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("   DISTINCT A.PLACA_CAVALO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.PLACA_CAVALO ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("   DISTINCT A.PLACA_CAVALO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.PLACA_CAVALO ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        While Not Rst.EOF
            ListaCavalo.Add(Rst.Fields("PLACA_CAVALO").Value.ToString())
            Rst.MoveNext()
        End While

        Return ListaCavalo

    End Function

    Public Function ConsultarPlacasCarreta(ByVal ID As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim ListaCarreta As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("   DISTINCT A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.PLACA_CARRETA ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("   DISTINCT A.PLACA_CARRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.PLACA_CARRETA ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        While Not Rst.EOF
            ListaCarreta.Add(Rst.Fields("PLACA_CARRETA").Value.ToString())
            Rst.MoveNext()
        End While

        Return ListaCarreta

    End Function

    Public Function ConsultarCNH(ByVal Motorista As Motorista) As String

        Dim Rst As New ADODB.Recordset
        Dim ListaCNH As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE NOME = '{0}' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT DISTINCT CNH FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE  ID_TRANSPORTADORA={1}", Motorista.Nome, Motorista.Transportadora.ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("CNH").Value.ToString()
        End If

        Return String.Empty

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

    Public Function ConsultarDadosAgendamento(ByVal ID_Transportadora As String, ByVal ID As String) As Importacao

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.PATIO,  ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.NUM_DOC_SAIDA,  ")
            SQL.Append("    A.SERIE_DOC_SAIDA,  ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    A.ID_CONTEINER,  ")
            SQL.Append("    A.NUM_PROTOCOLO,  ")
            SQL.Append("    A.ANO_PROTOCOLO,  ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    B.NOME,  ")
            SQL.Append("    B.AUTONUM AUTONUM_MOTORISTA,  ")
            SQL.Append("    C.AUTONUM AUTONUM_VEICULO,  ")
            SQL.Append("    B.CNH,  ")
            SQL.Append("    NVL(A.FLAG_LIBERADO,0) FLAG_LIBERADO,  ")
            SQL.Append("    C.PLACA_CAVALO, ")
            SQL.Append("    C.PLACA_CARRETA,  ")
            SQL.Append("    C.TARA,  ")
            SQL.Append("    C.CHASSI,  ")
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            SQL.Append("    G.FLAG_RETIRADA_VIP AS VIP, ")
            SQL.Append("    TO_CHAR(H.VALIDADE_GR,'DD/MM/YYYY') DATA_FREE_TIME, ")
            SQL.Append("    I.DESCR AS TIPO_DOCUMENTO, ")
            SQL.Append("    F.AUTONUM AS LOTE ")
            SQL.Append("FROM  ")
            SQL.Append("    SGIPA.TB_CNTR_BL A ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
            SQL.Append("INNER JOIN  ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
            SQL.Append("INNER JOIN  ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.TB_AMR_CNTR_BL E ON A.AUTONUM = E.CNTR ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.TB_BL F ON E.BL = F.AUTONUM ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.TB_CAD_PARCEIROS G ON F.IMPORTADOR = G.AUTONUM ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.TB_GR_PRE_CALCULO H ON F.AUTONUM = H.BL ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.TB_TIPOS_DOCUMENTOS I ON F.TIPO_DOCUMENTO = I.CODE ")
            SQL.Append("WHERE  ")
            SQL.Append("    A.AUTONUM_TRANSPORTE_AGENDA = {0} ")
            SQL.Append("AND  ")
            SQL.Append("    A.AUTONUM = {1} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.PATIO,  ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.NUM_DOC_SAIDA,  ")
            SQL.Append("    A.SERIE_DOC_SAIDA,  ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    A.ID_CONTEINER,  ")
            SQL.Append("    A.NUM_PROTOCOLO,  ")
            SQL.Append("    A.ANO_PROTOCOLO,  ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    B.NOME,  ")
            SQL.Append("    B.CNH,  ")
            SQL.Append("    C.PLACA_CAVALO, ")
            SQL.Append("    C.PLACA_CARRETA,  ")
            SQL.Append("    C.TARA,  ")
            SQL.Append("    C.CHASSI,  ")
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') + ' - ' + TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            SQL.Append("    G.FLAG_RETIRADA_VIP AS VIP, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(H.VALIDADE_GR,'DD/MM/YYYY') DATA_FREE_TIME, ")
            SQL.Append("    I.DESCR AS TIPO_DOCUMENTO ")
            SQL.Append("FROM  ")
            SQL.Append("    SGIPA.DBO.TB_CNTR_BL A ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
            SQL.Append("INNER JOIN  ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
            SQL.Append("INNER JOIN  ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.DBO.TB_AMR_CNTR_BL E ON A.AUTONUM = E.CNTR ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.DBO.TB_BL F ON E.BL = F.AUTONUM ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.DBO.TB_CAD_PARCEIROS G ON F.IMPORTADOR = G.AUTONUM ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.DBO.TB_GR_PRE_CALCULO H ON F.AUTONUM = H.BL ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    SGIPA.DBO.TB_TIPOS_DOCUMENTOS I ON F.TIPO_DOCUMENTO = I.CODE ")
            SQL.Append("WHERE  ")
            SQL.Append("    A.AUTONUM_TRANSPORTE_AGENDA = {0} ")
            SQL.Append("AND  ")
            SQL.Append("    A.AUTONUM = {1} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID_Transportadora, ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim ImportacaoOBJ As New Importacao

            ImportacaoOBJ.Autonum = Rst.Fields("AUTONUM").Value.ToString()
            ImportacaoOBJ.pPatio = Rst.Fields("PATIO").Value.ToString()
            ImportacaoOBJ.pVIP = Rst.Fields("VIP").Value.ToString()
            ImportacaoOBJ.pFreeTime = Rst.Fields("DATA_FREE_TIME").Value.ToString()
            ImportacaoOBJ.pDTA = Rst.Fields("TIPO_DOCUMENTO").Value.ToString()
            ImportacaoOBJ.Tipo_doc_saida = Rst.Fields("TIPO_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Num_doc_saida = Rst.Fields("NUM_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Serie_doc_saida = Rst.Fields("SERIE_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Emissao_doc_saida = Rst.Fields("EMISSAO_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Id_Conteiner = Rst.Fields("ID_CONTEINER").Value.ToString()
            ImportacaoOBJ.Num_Protocolo = Rst.Fields("NUM_PROTOCOLO").Value.ToString()
            ImportacaoOBJ.Ano_Protocolo = Rst.Fields("ANO_PROTOCOLO").Value.ToString()
            ImportacaoOBJ.Nome = Rst.Fields("NOME").Value.ToString()
            ImportacaoOBJ.CNH = Rst.Fields("CNH").Value.ToString()
            ImportacaoOBJ.Placa_Cavalo = Rst.Fields("PLACA_CAVALO").Value.ToString()
            ImportacaoOBJ.Placa_carreta = Rst.Fields("PLACA_CARRETA").Value.ToString()
            ImportacaoOBJ.Tara = Rst.Fields("TARA").Value.ToString()
            ImportacaoOBJ.Chassi = Rst.Fields("CHASSI").Value.ToString()
            ImportacaoOBJ.ID_Periodo = Rst.Fields("AUTONUM_GD_RESERVA").Value.ToString()
            ImportacaoOBJ.Periodo = Rst.Fields("PERIODO").Value.ToString()
            ImportacaoOBJ.pLote = Rst.Fields("LOTE").Value.ToString()
            ImportacaoOBJ.pLiberado = Rst.Fields("FLAG_LIBERADO").Value.ToString()
            ImportacaoOBJ.AutonumMotorista = Rst.Fields("AUTONUM_MOTORISTA").Value.ToString()
            ImportacaoOBJ.AutonumVeiculo = Rst.Fields("AUTONUM_VEICULO").Value.ToString()

            Return ImportacaoOBJ

        End If

        Return Nothing

    End Function

    Public Function ConsultarDadosAgendamentoPorConteinerAgendado(ByVal ID As String) As Importacao

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.PATIO, ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.NUM_DOC_SAIDA, ")
            SQL.Append("    A.SERIE_DOC_SAIDA, ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    A.ID_CONTEINER, ")
            SQL.Append("    A.NUM_PROTOCOLO, ")
            SQL.Append("    A.ANO_PROTOCOLO, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    TO_CHAR(B.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR (B.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_CNTR_BL A ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA B ON A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM, ")
            SQL.Append("    A.PATIO, ")
            SQL.Append("    A.TIPO_DOC_SAIDA, ")
            SQL.Append("    A.NUM_DOC_SAIDA, ")
            SQL.Append("    A.SERIE_DOC_SAIDA, ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC_SAIDA,'DD/MM/YY') EMISSAO_DOC_SAIDA, ")
            SQL.Append("    A.ID_CONTEINER, ")
            SQL.Append("    A.NUM_PROTOCOLO, ")
            SQL.Append("    A.ANO_PROTOCOLO, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    TO_CHAR(B.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR (B.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.DBO.TB_CNTR_BL A ")
            SQL.Append("INNER JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA B ON A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM = {0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim ImportacaoOBJ As New Importacao

            ImportacaoOBJ.Autonum = Rst.Fields("AUTONUM").Value.ToString()
            ImportacaoOBJ.pPatio = Rst.Fields("PATIO").Value.ToString()
            ImportacaoOBJ.Tipo_doc_saida = Rst.Fields("TIPO_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Num_doc_saida = Rst.Fields("NUM_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Serie_doc_saida = Rst.Fields("SERIE_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Emissao_doc_saida = Rst.Fields("EMISSAO_DOC_SAIDA").Value.ToString()
            ImportacaoOBJ.Id_Conteiner = Rst.Fields("ID_CONTEINER").Value.ToString()
            ImportacaoOBJ.Num_Protocolo = Rst.Fields("NUM_PROTOCOLO").Value.ToString()
            ImportacaoOBJ.Ano_Protocolo = Rst.Fields("ANO_PROTOCOLO").Value.ToString()
            ImportacaoOBJ.ID_Periodo = Rst.Fields("AUTONUM_GD_RESERVA").Value.ToString()
            ImportacaoOBJ.Periodo = Rst.Fields("PERIODO").Value.ToString()

            Return ImportacaoOBJ

        End If

        Return Nothing

    End Function


    Public Function ConsultarDadosNotas(ByVal ID_Conteiner As String, ByVal ID_Transportadora As String) As List(Of NotaFiscal)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Lista As New List(Of NotaFiscal)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM AS ID, ")
            SQL.Append("    A.AUTONUM_CNTR, ")
            SQL.Append("    TO_CHAR(A.EMISSAO_DOC,'DD/MM/YY') EMISSAO_DOC, ")
            SQL.Append("    A.LOTE, ")
            SQL.Append("    A.NUM_DOC, ")
            SQL.Append("    A.SERIE_DOC, ")
            SQL.Append("    B.ID_CONTEINER ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_AG_IMP_NOTA_FISCAL A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_CNTR_BL B ON A.AUTONUM_CNTR = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_CNTR = {0} ")
            SQL.Append("AND ")
            SQL.Append("    B.AUTONUM_TRANSPORTE_AGENDA = {1} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM AS ID, ")
            SQL.Append("    A.AUTONUM_CNTR, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(A.EMISSAO_DOC,'DD/MM/YY') EMISSAO_DOC, ")
            SQL.Append("    A.LOTE, ")
            SQL.Append("    A.NUM_DOC, ")
            SQL.Append("    A.SERIE_DOC, ")
            SQL.Append("    B.ID_CONTEINER ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.DBO.TB_CNTR_BL B ON A.AUTONUM_CNTR = B.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_CNTR = {0} ")
            SQL.Append("AND ")
            SQL.Append("    B.AUTONUM_TRANSPORTE_AGENDA = {1} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID_Conteiner, ID_Transportadora), Banco.Conexao, 3, 3)

        While Not Rst.EOF

            Dim NotaFiscalOBJ As New NotaFiscal

            NotaFiscalOBJ.ID = Rst.Fields("ID").Value.ToString()
            NotaFiscalOBJ.ID_Conteiner = Rst.Fields("AUTONUM_CNTR").Value.ToString()
            NotaFiscalOBJ.Conteiner = Rst.Fields("ID_CONTEINER").Value.ToString()
            NotaFiscalOBJ.Emissao = Rst.Fields("EMISSAO_DOC").Value.ToString()
            NotaFiscalOBJ.Lote = Rst.Fields("LOTE").Value.ToString()
            NotaFiscalOBJ.NotaFiscal = Rst.Fields("NUM_DOC").Value.ToString()
            NotaFiscalOBJ.Serie = Rst.Fields("SERIE_DOC").Value.ToString()

            Lista.Add(NotaFiscalOBJ)
            NotaFiscalOBJ = Nothing
            Rst.MoveNext()

        End While

        Return Lista

    End Function

    Public Function ConsultarNome(ByVal Motorista As Motorista, ByVal Transportadora As Transportadora) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            If Not Motorista.CNH = String.Empty Then
                Rst.Open(String.Format("SELECT UPPER(NOME) AS NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE CNH = '{0}' AND ID_TRANSPORTADORA={1}", Motorista.CNH, Transportadora.ID), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT UPPER(NOME) AS NOME FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(NOME) LIKE '%{0}%' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Transportadora.ID), Banco.Conexao, 3, 3)
            End If
        Else
            If Not Motorista.CNH = String.Empty Then
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE CNH = '{1}' AND ID_TRANSPORTADORA={1}", Motorista.CNH, Transportadora.ID), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT NOME FROM OPERADOR.DBO.TB_AG_MOTORISTAS WHERE NOME LIKE '%{0}%' AND ID_TRANSPORTADORA={1}", Motorista.Nome, Transportadora.ID), Banco.Conexao, 3, 3)
            End If
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("NOME").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function AlterarDataSaida(ByVal ID_Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE SGIPA.TB_CNTR_BL SET DT_AGENDA_SAIDA = SYSDATE WHERE AUTONUM = {0}", ID_Conteiner), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE SGIPA.DBO.TB_CNTR_BL SET DT_AGENDA_SAIDA = GETDATE() WHERE AUTONUM = {0}", ID_Conteiner), Banco.Conexao, 3, 3)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function VerificarNotasIguais(ByVal Nota As String, ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT * FROM SGIPA.TB_AG_IMP_NOTA_FISCAL WHERE AUTONUM_CNTR={0} AND NUM_DOC='{1}'", Conteiner, Nota), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT * FROM SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL WHERE AUTONUM_CNTR={0} AND NUM_DOC='{1}'", Conteiner, Nota), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function InserirNotaFiscal(ByVal ID_Conteiner As String, ByVal Num_Doc As String, ByVal Serie_Doc As String, ByVal Emissao_Doc As String, ByVal Lote As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("INSERT INTO ")
            SQL.Append("    SGIPA.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("        ( ")
            SQL.Append("            AUTONUM, ")
            SQL.Append("            AUTONUM_CNTR, ")
            SQL.Append("            NUM_DOC, ")
            SQL.Append("            SERIE_DOC, ")
            SQL.Append("            EMISSAO_DOC, ")
            SQL.Append("            LOTE ")
            SQL.Append("        ) ")
            SQL.Append("    VALUES ")
            SQL.Append("        ( ")
            SQL.Append("             SGIPA.SEQ_AG_IMP_NOTA_FISCAL.NEXTVAL, ")
            SQL.Append("              {0}, ")
            SQL.Append("             '{1}', ")
            SQL.Append("             '{2}', ")
            SQL.Append("             TO_DATE('{3}','DD/MM/YY'), ")
            SQL.Append("              {4} ")
            SQL.Append("        ) ")
        Else
            SQL.Append("INSERT INTO ")
            SQL.Append("    SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("        ( ")
            SQL.Append("            AUTONUM_CNTR, ")
            SQL.Append("            NUM_DOC, ")
            SQL.Append("            SERIE_DOC, ")
            SQL.Append("            EMISSAO_DOC, ")
            SQL.Append("            LOTE ")
            SQL.Append("        ) ")
            SQL.Append("    VALUES ")
            SQL.Append("        ( ")
            SQL.Append("              {0}, ")
            SQL.Append("             '{1}', ")
            SQL.Append("             '{2}', ")
            SQL.Append("             OPERAOR.DBO.TO_DATE('{3}','DD/MM/YY'), ")
            SQL.Append("              {4} ")
            SQL.Append("        ) ")
        End If

        Try
            Rst.Open(String.Format(SQL.ToString(), ID_Conteiner, Num_Doc, Serie_Doc, Emissao_Doc, Lote), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ExcluirNotaFiscal(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("DELETE FROM ")
            SQL.Append("    SGIPA.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM={0} ")
        Else
            SQL.Append("DELETE FROM ")
            SQL.Append("    SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM={0} ")
        End If

        Try
            Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ExcluirNotasFiscaisPorConteiner(ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("DELETE FROM ")
            SQL.Append("    SGIPA.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_CNTR={0} ")
        Else
            SQL.Append("DELETE FROM ")
            SQL.Append("    SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_CNTR={0} ")
        End If

        Try
            Rst.Open(String.Format(SQL.ToString(), Conteiner), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function AgendarConteiner(ByVal ID_Periodo As String, ByVal ID_Transportadora As String, ByVal ID_Motorista As String, ByVal ID_Veiculo As String, ByVal Tipo_Doc_Saida As String, ByVal Num_Doc_Saida As String, ByVal Serie_Doc_Saida As String, ByVal Emissao_Doc_Saida As String, ByVal ID_Conteiner As String, ByVal USRID As String, ByVal Autonomo As Integer) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("UPDATE ")
            SQL.Append("    SGIPA.TB_CNTR_BL SET ")
            SQL.Append("            AUTONUM_GD_RESERVA={0}, ")
            SQL.Append("            AUTONUM_TRANSPORTE_AGENDA={1}, ")
            SQL.Append("            AUTONUM_MOTORISTA={2}, ")
            SQL.Append("            AUTONUM_VEICULO={3}, ")
            SQL.Append("            NUM_PROTOCOLO={4}, ")
            SQL.Append("            ANO_PROTOCOLO={5}, ")
            SQL.Append("            TIPO_DOC_SAIDA='{6}', ")
            SQL.Append("            NUM_DOC_SAIDA={7}, ")
            SQL.Append("            SERIE_DOC_SAIDA='{8}', ")
            SQL.Append("            EMISSAO_DOC_SAIDA=TO_DATE('{9}','DD/MM/YY'), ")
            SQL.Append("            USRID={10}, ")
            SQL.Append("            FLAG_AUTONOMO={11}, ")

            SQL.Append(" XML_CODESP = 0, ")

            Dim RstLib As New ADODB.Recordset
            RstLib.Open("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

            If Not RstLib.EOF Then
                If RstLib.Fields("FLAG_BLOQUEIA_PROTOCOLO").Value.ToString() = "1" Then
                    SQL.Append("  IMPRESSO = 2 ")
                Else
                    SQL.Append("  IMPRESSO = 0 ")
                End If
            End If

            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM={12} ")

            Try
                Rst.Open(String.Format(SQL.ToString(), ID_Periodo, ID_Transportadora, ID_Motorista, ID_Veiculo, "SGIPA.SEQ_GD_PROT_" & Now.Year & ".NEXTVAL", Now.Year, Tipo_Doc_Saida, Num_Doc_Saida, Serie_Doc_Saida, Emissao_Doc_Saida, USRID, Autonomo, ID_Conteiner), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        Else

            Dim Protocolo As String = String.Empty

            Rst.Open("SELECT MAX(NUM_PROTOCOLO)+1 AS PROTOCOLO FROM SGIPA.DBO.TB_CNTR_BL", Banco.Conexao, 3, 3)

            If Not Rst.EOF Then
                Protocolo = Rst.Fields("ID").Value.ToString()
                Rst.Close()
            End If

            SQL.Append("UPDATE ")
            SQL.Append("    SGIPA.DBO.TB_CNTR_BL SET ")
            SQL.Append("            AUTONUM_GD_RESERVA={0}, ")
            SQL.Append("            AUTONUM_TRANSPORTE_AGENDA={1}, ")
            SQL.Append("            AUTONUM_MOTORISTA={2}, ")
            SQL.Append("            AUTONUM_VEICULO={3}, ")
            SQL.Append("            NUM_PROTOCOLO={4}, ")
            SQL.Append("            ANO_PROTOCOLO={5}, ")
            SQL.Append("            TIPO_DOC_SAIDA='{6}', ")
            SQL.Append("            NUM_DOC_SAIDA={7}, ")
            SQL.Append("            SERIE_DOC_SAIDA='{8}', ")
            SQL.Append("            EMISSAO_DOC_SAIDA=OPERADOR.DBO.TO_DATE('{9}','DD/MM/YY'), ")
            SQL.Append("            USRID={10}, ")
            SQL.Append("            FLAG_AUTONOMO={11}, ")
            SQL.Append("            XML_CODESP = 0 ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM={12} ")

            Try
                Rst.Open(String.Format(SQL.ToString(), ID_Periodo, ID_Transportadora, ID_Motorista, ID_Veiculo, Protocolo, Now.Year, Tipo_Doc_Saida, Num_Doc_Saida, Serie_Doc_Saida, Emissao_Doc_Saida, USRID, Autonomo, ID_Conteiner), Banco.Conexao, 3, 3)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If

        Return False

    End Function

    Public Function DesassociarConteiner(ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("UPDATE SGIPA.TB_CNTR_BL SET AUTONUM_GD_RESERVA=0, NUM_PROTOCOLO=0, ANO_PROTOCOLO=0, AUTONUM_MOTORISTA=0, AUTONUM_TRANSPORTE_AGENDA=0, FLAG_LIBERADO = 0, MOTIVO_AGENDAMENTO_RECUSADO = NULL, DT_AGENDAMENTO_LIBERACAO = NULL, USUARIO_LIBERACAO_AGENDAMENTO = NULL  WHERE AUTONUM={0}", Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("UPDATE SGIPA.DBO.TB_CNTR_BL SET AUTONUM_GD_RESERVA=0, NUM_PROTOCOLO=0, ANO_PROTOCOLO=0, AUTONUM_MOTORISTA=0, AUTONUM_TRANSPORTE_AGENDA=0, FLAG_LIBERADO = 0, MOTIVO_AGENDAMENTO_RECUSADO = NULL, DT_AGENDAMENTO_LIBERACAO = NULL, USUARIO_LIBERACAO_AGENDAMENTO = NULL WHERE AUTONUM={0}", Conteiner), Banco.Conexao, 3, 3)
        End If

        Dim SQL As New StringBuilder

        Dim RstExcluir As New ADODB.Recordset
        Dim ws As New WsSharepoint.WsIccSharepointSoapClient()

        Try
            RstExcluir.Open("SELECT A.AUTONUM, A.LOTE FROM TB_AV_IMAGEM A INNER JOIN TB_CNTR_BL B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM WHERE AUTONUM_AGENDAMENTO = " & Conteiner, Banco.Conexao, 3, 3)

            If RstExcluir.EOF = False Then
                While RstExcluir.EOF = False
                    ws.ExcluirImagemDocAverbacaoPorLoteEautonum(Val(RstExcluir.Fields("LOTE").Value.ToString()), Val(RstExcluir.Fields("AUTONUM").Value.ToString()))
                    RstExcluir.MoveNext()
                End While
            End If
        Catch ex As Exception

        End Try

        Return True

    End Function


    Public Function ExcluirAgendameto(ByVal Codigo As String, ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("UPDATE ")
        SQL.Append("    SGIPA.TB_CNTR_BL SET ")
        SQL.Append("            AUTONUM_GD_RESERVA=0, ")
        SQL.Append("            AUTONUM_TRANSPORTE_AGENDA=0, ")
        SQL.Append("            AUTONUM_MOTORISTA=0, ")
        SQL.Append("            NUM_PROTOCOLO=0, ")
        SQL.Append("            ANO_PROTOCOLO=0, ")
        SQL.Append("            TIPO_DOC_SAIDA='', ")
        SQL.Append("            NUM_DOC_SAIDA=0, ")
        SQL.Append("            SERIE_DOC_SAIDA='', ")
        SQL.Append("            EMISSAO_DOC_SAIDA=NULL ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM={0} ")

        Try
            Rst.Open(String.Format(SQL.ToString(), Codigo), Banco.Conexao, 3, 3)
            ExcluirNotasFiscaisPorConteiner(Conteiner)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function AlterarAgendamento(ByVal ID_Periodo As String, ByVal ID_Motorista As String, ByVal ID_Veiculo As String, ByVal Tipo_Doc_Saida As String, ByVal Num_Doc_Saida As String, ByVal Serie_Doc_Saida As String, ByVal Emissao_Doc_Saida As String, ByVal ID_Conteiner As String, ByVal USRID As String, ByVal TeveAlteracoesDeDocumentos As Boolean) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("UPDATE ")
        SQL.Append("    SGIPA.TB_CNTR_BL SET ")
        SQL.Append("            AUTONUM_GD_RESERVA={0}, ")
        SQL.Append("            AUTONUM_MOTORISTA={1}, ")
        SQL.Append("            AUTONUM_VEICULO={2}, ")
        SQL.Append("            TIPO_DOC_SAIDA='{3}', ")
        SQL.Append("            NUM_DOC_SAIDA={4}, ")
        SQL.Append("            SERIE_DOC_SAIDA='{5}', ")
        SQL.Append("            EMISSAO_DOC_SAIDA=TO_DATE('{6}','DD/MM/YY'), ")
        SQL.Append("            USRID={7}, ")
        SQL.Append("            XML_CODESP = 0, ")

        If TeveAlteracoesDeDocumentos Then
            SQL.Append("  IMPRESSO = 2 ")
        Else
            Dim RstAux As New ADODB.Recordset
            RstAux.Open("SELECT NVL(FLAG_LIBERADO,0) FLAG_LIBERADO FROM TB_CNTR_BL WHERE AUTONUM = " & ID_Conteiner, Banco.Conexao, 3, 3)

            If RstAux.RecordCount > 0 Then
                If Val(RstAux.Fields("FLAG_LIBERADO").Value.ToString()) = 0 Then
                    SQL.Append("  IMPRESSO = 2 ")
                Else
                    SQL.Append("  IMPRESSO = 0 ")
                End If
            End If
        End If

        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM={8} ")

        Try
            Rst.Open(String.Format(SQL.ToString(), ID_Periodo, ID_Motorista, ID_Veiculo, Tipo_Doc_Saida, Num_Doc_Saida, Serie_Doc_Saida, Emissao_Doc_Saida, USRID, ID_Conteiner), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function InsereAgendamentoNaFila(ByVal Conteiner As String, ByVal Periodo As String) As Boolean

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
            Rst.Open(String.Format("SELECT A.AUTONUM FROM SGIPA.TB_AGENDAMENTO_POSICAO A, SGIPA.TB_AGENDA_POSICAO_MOTIVO B WHERE  A.CANCELADO=0 AND A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.CNTR={0} AND B.MOTIVO_POSICAO={1}", Conteiner, Motivo), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT A.AUTONUM FROM SGIPA.DBO.TB_AGENDAMENTO_POSICAO A, SGIPA.DBO.TB_AGENDA_POSICAO_MOTIVO B WHERE A.CANCELADO=0 AND A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.CNTR={0} AND B.MOTIVO_POSICAO={1}", Conteiner, Motivo), Banco.Conexao, 3, 3)
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
                Rst.Open(String.Format("INSERT INTO SGIPA.TB_AGENDAMENTO_POSICAO (AUTONUM,CNTR,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS) VALUES ({0},{1},TO_DATE('{2}','DD/MM/YYYY HH24:MI:SS'),SYSDATE,0,SGIPA.SEQ_OS" & Now.Year & ".NEXTVAL," & Now.Year & ")", ID, Conteiner, Data_Prevista), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("INSERT INTO SGIPA.DBO.TB_AGENDAMENTO_POSICAO (CNTR,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS) VALUES ({0},OPERADOR.DBO.TO_DATE('{1}','DD/MM/YYYY HH24:MI:SS'),GETDATE(),0," & ID & "," & Now.Year & ")", Conteiner, Data_Prevista), Banco.Conexao, 3, 3)
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

    Public Function Consultar(ByVal ID As String, ByVal Empresa As String, Optional ByVal Filtro As String = "") As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("  A.AUTONUM, ")
        SQL.Append("  A.PATIO, ")
        SQL.Append("  NVL(K.FLAG_RETIRADA_VIP,0) AS VIP, ")
        SQL.Append("  A.ID_CONTEINER || ' ' || A.TAMANHO || ' ' || TC.DESCR AS ID_CONTEINER, ")
        SQL.Append("  TD.DESCR || ' ' || C.NUM_DOCUMENTO AS DOCUMENTO, ")
        SQL.Append("  (NVL(A.IMO1,'') || ' ' || NVL(A.IMO2,'') || ' ' || NVL(A.IMO3,'') || ' ' || NVL(A.IMO4,'')) AS IMO, ")
        SQL.Append("  (NVL(A.UNDG,'') || ' ' || NVL(A.UNDG2,'') || ' ' || NVL(A.UNDG3,'') || ' ' || NVL(A.UNDG4,'')) AS ONU, ")
        SQL.Append("  TO_CHAR(P.VALIDADE_GR,'DD/MM/YYYY') AS DATA_FREE_TIME, ")
        SQL.Append("  NVL(A.YARD,'') || ' - ' || NVL(RE.TEMP_REMOCOES,0) ||' ' ||NVL(RE.TEMP_RUA,'') AS REMOCAO, ")
        SQL.Append("  'RODOVIÁRIO' AS TIPO_TRANSPORTE, ")
        SQL.Append("  A.DT_SAIDA, ")
        'SQL.Append("  CASE WHEN NVL(A.IMPRESSO,0) = 0 THEN 'Não' WHEN NVL(A.IMPRESSO,0) = 1 THEN 'Sim' WHEN NVL(A.IMPRESSO,0) = 2 THEN 'Pendente' WHEN A.MOTIVO_AGENDAMENTO_RECUSADO <> ' ' THEN 'Recusado' END IMPRESSO, ")
        SQL.Append("  CASE WHEN nvl(a.impresso,0) = 0 AND NVL(a.motivo_agendamento_recusado,'R') = 'R' THEN 'Não' WHEN nvl(a.impresso,0) = 1 AND NVL(a.motivo_agendamento_recusado,'R') = 'R'  THEN 'Sim' WHEN nvl(a.impresso,0) = 2 AND NVL(a.motivo_agendamento_recusado,'R') = 'R'  THEN 'Pendente' WHEN a.motivo_agendamento_recusado <> ' ' THEN 'Recusado' END impresso, ")
        '
        SQL.Append("  M.NOME, ")
        SQL.Append("  M.CNH, ")
        SQL.Append("  V.PLACA_CAVALO || '/' || V.PLACA_CARRETA AS VEICULO, ")
        SQL.Append("  TO_CHAR(R.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || '-' || TO_CHAR(R.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO, ")
        SQL.Append("  A.PATIO AS TERMINAL, ")
        SQL.Append("  A.NUM_PROTOCOLO, ")
        SQL.Append("  A.ANO_PROTOCOLO, ")
        SQL.Append("  C.NUM_DOCUMENTO AS BL, ")
        SQL.Append("  TO_CHAR(O.DATA_ORDEM,'DD/MM/YYYY HH24:MI') DT_CHEGADA_VEI, ")
        SQL.Append("  A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO ")
        SQL.Append("FROM ")
        SQL.Append("  SGIPA.TB_CNTR_BL A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.TB_AMR_CNTR_BL B ON A.AUTONUM = B.CNTR ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.TB_BL C ON B.BL = C.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.TB_TIPOS_DOCUMENTOS TD ON C.TIPO_DOCUMENTO = TD.CODE ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.DTE_TB_TIPOS_CONTEINER TC ON A.TIPO = TC.CODE ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("  (SELECT ")
        SQL.Append("      AUTONUM, TEMP_REMOCOES, TEMP_RUA ")
        SQL.Append("    FROM ")
        SQL.Append("      OPERADOR.TB_REMOCAO ")
        SQL.Append("    WHERE SISTEMA = 'I' ")
        SQL.Append("  ) RE ON A.AUTONUM = RE.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  OPERADOR.TB_GD_RESERVA R ON A.AUTONUM_GD_RESERVA = R.AUTONUM_GD_RESERVA ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  OPERADOR.TB_AG_MOTORISTAS M ON A.AUTONUM_MOTORISTA = M.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  OPERADOR.TB_AG_VEICULOS V ON A.AUTONUM_VEICULO = V.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.TB_CAD_PARCEIROS K ON C.IMPORTADOR = K.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  SGIPA.TB_GR_PRE_CALCULO P ON C.AUTONUM = P.BL ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("  SGIPA.TB_REGISTRO_SAIDA_CNTR RO ON A.AUTONUM = RO.CNTR ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("  SGIPA.TB_ORDEM_CARREGAMENTO O ON RO.ORDEM_CARREG = O.AUTONUM ")
        SQL.Append("WHERE ")
        SQL.Append("  C.FLAG_ATIVO = 1 ")
        SQL.Append("AND ")
        SQL.Append("  A.AUTONUM_TRANSPORTE_AGENDA = {0} ")
        SQL.Append("AND ")
        SQL.Append("  NVL(NUM_PROTOCOLO,0) <> 0 ")
        SQL.Append("AND ")
        SQL.Append("  A.FLAG_TERMINAL = 1 ")
        SQL.Append("AND ")
        SQL.Append("  A.FLAG_HISTORICO = 0 ")

        If Empresa = 1 Then
            SQL.Append("  AND A.PATIO <> 5 ")
        Else
            SQL.Append("  AND A.PATIO = 5 ")
        End If

        SQL.Append(Filtro)

        Return String.Format(SQL.ToString(), ID)

    End Function

    Public Function AlteraStatusImpressao(ByVal Protocolo As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE TB_CNTR_BL SET IMPRESSO = 1 WHERE NUM_PROTOCOLO || ANO_PROTOCOLO IN({0})", Protocolo), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE TB_CNTR_BL SET IMPRESSO = 1 WHERE NUM_PROTOCOLO + ANO_PROTOCOLO IN({0})", Protocolo), Banco.Conexao, 3, 3)
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function GerarProtocolo(ByVal ID As String, ByVal CorPadrao As String, ByVal Empresa As String) As StringBuilder

        Dim Rst1 As New ADODB.Recordset
        Dim Rst2 As New ADODB.Recordset
        Dim Rst3 As New ADODB.Recordset
        Dim Autonum As String = String.Empty

        Dim SQL As New StringBuilder

        Dim Estrutura As New StringBuilder
        Dim Tabela1 As New StringBuilder
        Dim Tabela2 As New StringBuilder
        Dim Header As New StringBuilder
        Dim TabelaItem As New StringBuilder

        Dim Protocolos As String() = ID.Split(",")

        For Each Item In Protocolos

            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.ID_CONTEINER, ")
                SQL.Append("    A.TAMANHO, ")
                SQL.Append("    A.NUM_PROTOCOLO, ")
                SQL.Append("    A.ANO_PROTOCOLO, ")
                SQL.Append("    A.TIPO_DOC_SAIDA, ")
                SQL.Append("    A.SERIE_DOC_SAIDA, ")
                SQL.Append("    A.NUM_DOC_SAIDA, ")
                SQL.Append("    A.EMISSAO_DOC_SAIDA, ")
                SQL.Append("    C.AUTONUM AS LOTE, ")
                SQL.Append("    C.NUMERO AS BL, ")
                SQL.Append("    B.BRUTO AS BRUTO_MANIFESTADO, ")
                SQL.Append("    P.RAZAO AS IMPORTADOR, ")
                SQL.Append("    TC.DESCR AS TIPOBASICO, ")
                SQL.Append("    TD.DESCR || ' ' ||  C.NUM_DOCUMENTO AS DOCUMENTO, ")
                SQL.Append("    NVL(RE.TEMP_REMOCOES,0)||' '||NVL(RE.TEMP_RUA,'') AS REMOCOES, ")
                SQL.Append("    NVL(A.YARD,'') AS LOCALIZACAO, ")
                SQL.Append("    M.NOME, ")
                SQL.Append("    M.CNH, ")
                SQL.Append("    M.RG, ")
                SQL.Append("    M.CPF, ")
                SQL.Append("    M.NEXTEL, ")
                SQL.Append("    V.PLACA_CAVALO, ")
                SQL.Append("    V.PLACA_CARRETA, ")
                SQL.Append("    V.MODELO, ")
                SQL.Append("    V.COR, ")
                'SQL.Append("    TO_CHAR(R.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(R.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO, ")
                SQL.Append("    TO_CHAR(R.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') PERIODO, ")
                SQL.Append("    A.PATIO, ")
                SQL.Append("    A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append("    (SELECT MAX(LACRE1 || ' ' || LACRE2 || '  ' || LACRE3 || '  ' || LACRE4) AS DIVLACRES FROM OPERADOR.VW_DADOS_EI WHERE CNTR_IPA = A.AUTONUM) AS LACRES_ENCONTRADOS, ")
                SQL.Append("    (SELECT MAX(LACRE_1 || ' ' || LACRE_2 || '  ' || LACRE_3 || '  ' || LACRE_4) AS DIVLACRES FROM SGIPA.TB_AGENDAMENTO_POSICAO WHERE CNTR = A.AUTONUM) AS LACRES_ADICIONAIS, ")
                SQL.Append("    (SELECT MAX(PESO_ENTRADA - PESO_SAIDA) AS VALOR_PESAGEM FROM OPERADOR.VW_DADOS_EI  WHERE CNTR_IPA = A.AUTONUM) AS BRUTO_APURADO, ")
                SQL.Append("    A.LACRE_IPA, ")
                SQL.Append("    T.SEQUENCIA || '/' || T.ANO AS TERMO_AVARIA, ")
                SQL.Append("    J.RAZAO AS TRANSPORTADORA, ")
                SQL.Append("    (SELECT MAX(MOT1.ORGAO) AS ORGAO FROM OPERADOR.TB_MOTORISTAS MOT1 WHERE M.CNH = MOT1.CNH ) AS ORGAO, ")
                SQL.Append("    (SELECT MAX(TO_CHAR(MOT2.EMISSAO_RG,'DD/MM/YYYY')) AS EMISSAO_RG FROM OPERADOR.TB_MOTORISTAS MOT2 WHERE M.CNH = MOT2.CNH ) AS EMISSAO_RG ")
                SQL.Append("FROM ")
                SQL.Append("    SGIPA.TB_CNTR_BL A, ")
                SQL.Append("    SGIPA.TB_AMR_CNTR_BL B, ")
                SQL.Append("    SGIPA.TB_BL C, ")
                SQL.Append("    SGIPA.TB_TIPOS_DOCUMENTOS TD, ")
                SQL.Append("    SGIPA.DTE_TB_TIPOS_CONTEINER TC, ")
                SQL.Append("    SGIPA.TB_CAD_PARCEIROS P, ")
                SQL.Append("    SGIPA.TB_TERMO_AVARIA T, ")
                'SQL.Append("    OPERADOR.TB_REMOCAO RE, ")
                SQL.Append("    (select AUTONUM,TEMP_REMOCOES,TEMP_RUA from OPERADOR.TB_REMOCAO where SISTEMA='I') RE, ")
                SQL.Append("    OPERADOR.TB_GD_RESERVA R, ")
                SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS J, ")
                SQL.Append("    OPERADOR.TB_AG_MOTORISTAS M, ")
                SQL.Append("    OPERADOR.TB_AG_VEICULOS V ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM = B.CNTR ")
                SQL.Append("AND ")
                SQL.Append("    B.BL = C.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    C.FLAG_ATIVO = 1 ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM=RE.AUTONUM(+) ")
                'SQL.Append("AND ")
                'SQL.Append("    NVL(RE.SISTEMA,'I')='I' ")
                SQL.Append("AND ")
                SQL.Append("    C.TIPO_DOCUMENTO = TD.CODE ")
                SQL.Append("AND ")
                SQL.Append("    A.TIPO=TC.CODE ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_GD_RESERVA = R.AUTONUM_GD_RESERVA ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_MOTORISTA = M.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_VEICULO = V.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    C.IMPORTADOR = P.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.TERMO_AVARIA = T.AUTONUM(+) ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_TRANSPORTE_AGENDA = J.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.NUM_PROTOCOLO || A.ANO_PROTOCOLO = {0} ")
            Else
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.ID_CONTEINER, ")
                SQL.Append("    A.TAMANHO, ")
                SQL.Append("    A.NUM_PROTOCOLO, ")
                SQL.Append("    A.ANO_PROTOCOLO, ")
                SQL.Append("    A.TIPO_DOC_SAIDA, ")
                SQL.Append("    A.SERIE_DOC_SAIDA, ")
                SQL.Append("    A.NUM_DOC_SAIDA, ")
                SQL.Append("    A.EMISSAO_DOC_SAIDA, ")
                SQL.Append("    C.AUTONUM AS LOTE, ")
                SQL.Append("    C.NUMERO AS BL, ")
                SQL.Append("    B.BRUTO AS BRUTO_MANIFESTADO, ")
                SQL.Append("    P.RAZAO AS IMPORTADOR, ")
                SQL.Append("    TC.DESCR AS TIPOBASICO, ")
                SQL.Append("    TD.DESCR + ' ' +  C.NUM_DOCUMENTO AS DOCUMENTO, ")
                SQL.Append("    ISNULL(RE.TEMP_REMOCOES,0)+' '+ISNULL(RE.TEMP_RUA,'') AS REMOCOES, ")
                SQL.Append("    ISNULL(A.YARD,'') AS LOCALIZACAO, ")
                SQL.Append("    M.NOME, ")
                SQL.Append("    M.CNH, ")
                SQL.Append("    M.RG, ")
                SQL.Append("    M.NEXTEL, ")
                SQL.Append("    V.PLACA_CAVALO, ")
                SQL.Append("    V.PLACA_CARRETA, ")
                SQL.Append("    V.MODELO, ")
                SQL.Append("    V.COR, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(R.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') + ' - ' + OPERADOR.DBO.TO_CHAR(R.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO, ")
                SQL.Append("    A.PATIO, ")
                SQL.Append("    A.NUM_PROTOCOLO + '/' + A.ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append("    (SELECT MAX(LACRE1 + ' ' + LACRE2 + '  ' + LACRE3 + '  ' + LACRE4) AS DIVLACRES FROM OPERADOR.DBO.VW_DADOS_EI WHERE CNTR_IPA = A.AUTONUM) AS LACRES_ENCONTRADOS, ")
                SQL.Append("    (SELECT MAX(LACRE_1 + ' ' + LACRE_2 + '  ' + LACRE_3 + '  ' + LACRE_4) AS DIVLACRES FROM SGIPA.DBO.TB_AGENDAMENTO_POSICAO WHERE CNTR = A.AUTONUM) AS LACRES_ADICIONAIS, ")
                SQL.Append("    (SELECT MAX(PESO_ENTRADA - PESO_SAIDA) AS VALOR_PESAGEM FROM OPERADOR.DBO.VW_DADOS_EI  WHERE CNTR_IPA = A.AUTONUM) AS BRUTO_APURADO, ")
                SQL.Append("    A.LACRE_IPA, ")
                SQL.Append("    T.SEQUENCIA + '/' + T.ANO AS TERMO_AVARIA, ")
                SQL.Append("    J.RAZAO AS TRANSPORTADORA ")
                SQL.Append("FROM ")
                SQL.Append("    SGIPA.DBO.TB_CNTR_BL A, ")
                SQL.Append("    SGIPA.DBO.TB_AMR_CNTR_BL B, ")
                SQL.Append("    SGIPA.DBO.TB_BL C, ")
                SQL.Append("    SGIPA.DBO.TB_TIPOS_DOCUMENTOS TD, ")
                SQL.Append("    SGIPA.DBO.DTE_TB_TIPOS_CONTEINER TC, ")
                SQL.Append("    SGIPA.DBO.TB_CAD_PARCEIROS P, ")
                SQL.Append("    SGIPA.DBO.TB_TERMO_AVARIA T, ")
                'SQL.Append("    OPERADOR.DBO.TB_REMOCAO RE, ")
                SQL.Append("    (select AUTONUM,TEMP_REMOCOES,TEMP_RUA from OPERADOR.DBO.TB_REMOCAO where SISTEMA='I') RE, ")
                SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA R, ")
                SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS J, ")
                SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS M, ")
                SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS V ")
                SQL.Append("WHERE ")
                SQL.Append("    A.AUTONUM = B.CNTR ")
                SQL.Append("AND ")
                SQL.Append("    B.BL = C.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    C.FLAG_ATIVO = 1 ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM=RE.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    C.TIPO_DOCUMENTO = TD.CODE ")
                SQL.Append("AND ")
                SQL.Append("    A.TIPO=TC.CODE ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_GD_RESERVA = R.AUTONUM_GD_RESERVA ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_MOTORISTA = M.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    M.AUTONUM_VEICULO = V.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    C.IMPORTADOR = P.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.TERMO_AVARIA = T.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.AUTONUM_TRANSPORTE_AGENDA = J.AUTONUM ")
                SQL.Append("AND ")
                SQL.Append("    A.NUM_PROTOCOLO + A.ANO_PROTOCOLO = {0} ")
            End If

            If Rst1.State = 1 Then
                Rst1.Close()
            End If

            Rst1.Open(String.Format(SQL.ToString(), Item), Banco.Conexao, 3, 3)

            While Not Rst1.EOF

                Header.Append("<table id=cabecalho>")
                Header.Append("	<tr>")
                Header.Append("		<td align=left width=180px>")

                If Rst1.Fields("PATIO").Value.ToString() = "1" Or Rst1.Fields("PATIO").Value.ToString() = "2" Or Rst1.Fields("PATIO").Value.ToString() = "3" Or Rst1.Fields("PATIO").Value.ToString() = "4" Then
                    Header.Append("			<img src=css/tecondi/images/LogoTop.png />")
                    'CorPadrao = "#B3C63C"
                Else
                    Header.Append("			<img src=css/termares/images/LogoTop.png />")
                    'CorPadrao = "#B9D3EE"
                End If

                Header.Append("		</td>")
                Header.Append("		<td>")
                Header.Append("		<font face=Arial size=3>CERTIFICADO DE ENTREGA DE MERCADORIA</font>")
                Header.Append("		<br/>")
                Header.Append("        <font face=Arial size=5>Nº: " & Rst1.Fields("NUM_PROTOCOLO").Value.ToString() & "/" & Rst1.Fields("ANO_PROTOCOLO").Value.ToString() & "</font> <br/> <font face=Arial size=4>Pátio: " & Rst1.Fields("PATIO").Value.ToString() & "</font> ")
                Header.Append("		<br/><br/>")
                Header.Append("        " & Rst1.Fields("PERIODO").Value.ToString())
                Header.Append("		</td>")
                Header.Append("	</tr>")
                Header.Append("</table>")
                Header.Append("<br/>")

                Autonum = Rst1.Fields("AUTONUM").Value.ToString()

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>IMPORTADOR</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMPORTADOR").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Dados do Transporte</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>TRANSPORTADORA</td>")
                Tabela1.Append("        <td>PLACA CAVALO</td>")
                Tabela1.Append("        <td>PLACA CARRETA</td>")
                Tabela1.Append("        <td>COR</td>")
                Tabela1.Append("        <td>CPF</td>")
                Tabela1.Append("        <td>MODELO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TRANSPORTADORA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CAVALO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CARRETA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("COR").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("CPF").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("MODELO").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>MOTORISTA</td>")
                Tabela1.Append("        <td>CNH</td>")
                Tabela1.Append("        <td>RG</td>")
                Tabela1.Append("        <td>ÓRGÃO</td>")
                Tabela1.Append("        <td>EMISSÃO</td>")
                Tabela1.Append("        <td>NEXTEL</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("NOME").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("CNH").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("RG").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ORGAO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("EMISSAO_RG").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("NEXTEL").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Documentação</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>DOC. SAÍDA</td>")
                Tabela1.Append("        <td>SÉRIE DOC. SAÍDA</td>")
                Tabela1.Append("        <td>NÚMERO</td>")
                Tabela1.Append("        <td>EMISSÃO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TIPO_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("SERIE_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("NUM_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("EMISSAO_DOC_SAIDA").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Recebi as mercadorias constantes abaixo em perfeito estado</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>DOCUMENTO</td>")
                Tabela1.Append("        <td>LOTES</td>")
                Tabela1.Append("        <td>BL</td>")
                Tabela1.Append("        <td>LOCALIZAÇÃO</td>")
                Tabela1.Append("        <td>CONTÊINER</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("DOCUMENTO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LOTE").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("BL").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LOCALIZACAO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ID_CONTEINER").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>TAMANHO</td>")
                Tabela1.Append("        <td>TIPO</td>")
                Tabela1.Append("        <td>PESO BRUTO APURADO</td>")
                Tabela1.Append("        <td>BRUTO MANIFESTO</td>")
                Tabela1.Append("        <td>REMOÇÕES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TAMANHO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("TIPOBASICO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("BRUTO_APURADO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("BRUTO_MANIFESTADO").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("REMOCOES").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<br/>")

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td colspan=4>Dados dos Lacres</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("    <tr>")
                Tabela1.Append("        <td>Encontrados</td>")
                Tabela1.Append("        <td>Adicionais</td>")
                Tabela1.Append("        <td>IPA</td>")
                Tabela1.Append("        <td>Termo Avaria</td>")
                Tabela1.Append("    </tr>")
                Tabela1.Append("    <tr>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRES_ENCONTRADOS").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRES_ADICIONAIS").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE_IPA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("TERMO_AVARIA").Value.ToString() & "</td>")
                Tabela1.Append("    </tr>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                SQL.Clear()

                If Banco.BancoEmUso = "ORACLE" Then
                    SQL.Append("SELECT ")
                    SQL.Append("    A.NUM_DOC, ")
                    SQL.Append("    A.SERIE_DOC, ")
                    SQL.Append("    TO_CHAR(A.EMISSAO_DOC,'DD/MM/YY') EMISSAO_DOC, ")
                    SQL.Append("    A.LOTE, ")
                    SQL.Append("    B.ID_CONTEINER ")
                    SQL.Append("FROM ")
                    SQL.Append("    SGIPA.TB_AG_IMP_NOTA_FISCAL A ")
                    SQL.Append("LEFT JOIN ")
                    SQL.Append("    SGIPA.TB_CNTR_BL B ON A.AUTONUM_CNTR = B.AUTONUM ")
                    SQL.Append("WHERE ")
                    SQL.Append("    AUTONUM_CNTR = {0} ")
                Else
                    SQL.Append("SELECT ")
                    SQL.Append("    A.NUM_DOC, ")
                    SQL.Append("    A.SERIE_DOC, ")
                    SQL.Append("    OPERADOR.DBO.TO_CHAR(A.EMISSAO_DOC,'DD/MM/YY') EMISSAO_DOC, ")
                    SQL.Append("    A.LOTE, ")
                    SQL.Append("    B.ID_CONTEINER ")
                    SQL.Append("FROM ")
                    SQL.Append("    SGIPA.DBO.TB_AG_IMP_NOTA_FISCAL A ")
                    SQL.Append("LEFT JOIN ")
                    SQL.Append("    SGIPA.DBO.TB_CNTR_BL B ON A.AUTONUM_CNTR = B.AUTONUM ")
                    SQL.Append("WHERE ")
                    SQL.Append("    AUTONUM_CNTR = {0} ")
                End If

                If Rst2.State = 1 Then
                    Rst2.Close()
                End If

                Rst2.Open(String.Format(SQL.ToString(), Autonum), Banco.Conexao, 3, 3)

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td colspan=5>Notas Fiscais</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("    <tr>")
                Tabela1.Append("        <td>Contêiner</td>")
                Tabela1.Append("        <td>Nota Fiscal</td>")
                Tabela1.Append("        <td>Série</td>")
                Tabela1.Append("        <td>Emissão</td>")
                Tabela1.Append("        <td>Lote</td>")
                Tabela1.Append("    </tr>")

                While Not Rst2.EOF
                    Tabela1.Append("    <tr>")
                    Tabela1.Append("        <td>" & Rst2.Fields("ID_CONTEINER").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("NUM_DOC").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("SERIE_DOC").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("EMISSAO_DOC").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("LOTE").Value.ToString() & "</td>")
                    Tabela1.Append("    </tr>")
                    Rst2.MoveNext()
                End While

                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")

                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td colspan=3>Responsável</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <tr>")
                Tabela1.Append("                <tr>")
                Tabela1.Append("                    <td class=assin colspan=3 align=left>Assinatura Motorista:</td>")
                Tabela1.Append("                </tr>")
                Tabela1.Append("                <tr>")
                Tabela1.Append("                    <tr>")
                Tabela1.Append("                        <td class=assinatura valign=top>Entregue em:<br/><br/>_____/_____/_____ _____:_____:_____<br/> <br/> <br/> <br/> ___________________________________ </td>")
                Tabela1.Append("                        <td width=30% valign=top>Conferente:</td>")
                Tabela1.Append("                        <td width=30% valign=top>Fiel:</td>")
                Tabela1.Append("                    </tr>")
                Tabela1.Append("                </tr>")
                Tabela1.Append("        </tr>")
                Tabela1.Append("        <tr>")
                Tabela1.Append("        <b>Preferencialmente o motorista deverá se apresentar com 15 minutos de antecedência no registro;</b>")
                Tabela1.Append("        <br/><br/>")
                Tabela1.Append("        <b>- Em caso de necessidade de CANCELAMENTO, a transportadora deverá cancelar em até 03 horas antes do agendamento, <br/>sujeito à cobrança pela ausência do comparecimento no Terminal.</b>")
                Tabela1.Append("        </tr>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")


                Tabela1.Append("<table style='margin-top: 20px;'>")
                Tabela1.Append("    <tr>")
                Tabela1.Append("        <td style='border: 0;font-family: Tahoma;font-size: 16;'>")
                Tabela1.Append("        <b>Taxa no show: </b>Estando previamente agendado para carregamento/descarregamento, o veículo transportador deixar de comparecer na data/horário previsto, sem que tenha cancelado o agendamento, ou ainda, comparecer após a janela prevista para carregamento, a documentação apresentada estiver divergente ou o veículo não for compatível para o carregamento da carga.")
                Tabela1.Append("        </td>")
                Tabela1.Append("    </tr>")
                Tabela1.Append("</table>")

                Tabela1.Append("<br />")

                Estrutura.Append(Header.ToString())
                Estrutura.Append(Tabela1.ToString())
                Estrutura.Append("<div class=folha></div>")

                Header.Clear()
                Tabela1.Clear()
                SQL.Clear()

                Rst1.MoveNext()

            End While

            AlteraStatusImpressao(Item)

        Next

        Desconectar()

        Return Estrutura

    End Function

    Public Function VerificarLiberacaoCalculo(ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Rst.Open(String.Format("select a.DT_LIBERACAO_CALCULO from SGIPA.tb_bl a ,sgipa.tb_cntr_bl b,sgipa.tb_amr_cntr_bl c where b.AUTONUM = c.CNTR  and a.autonum = c.BL   and  b.ID_CONTEINER='{0}' ", Conteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.Fields("DT_LIBERACAO_CALCULO").Value.ToString().Length = String.Empty Then
                Return False
            End If
        End If

        Return True

    End Function

End Class

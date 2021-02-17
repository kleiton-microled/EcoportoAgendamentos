Imports System.Data.OleDb

Public Class DocumentoDto

    Private _Lote As String
    Private _TipoDocumento As String
    Private _NumeroDocumento As String

    Public Property Lote() As String
        Get
            Return _Lote
        End Get
        Set(ByVal value As String)
            _Lote = value
        End Set
    End Property

    Public Property TipoDocumento() As String
        Get
            Return _TipoDocumento
        End Get
        Set(ByVal value As String)
            _TipoDocumento = value
        End Set
    End Property

    Public Property NumeroDocumento() As String
        Get
            Return _NumeroDocumento
        End Get
        Set(ByVal value As String)
            _NumeroDocumento = value
        End Set
    End Property

End Class

Public Class Documento

    Public Shared Function ObterInformacoesLote(ByVal Lote As String) As DocumentoDto

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT A.AUTONUM, A.NUM_DOCUMENTO, B.DESCR FROM TB_BL A INNER JOIN TB_TIPOS_DOCUMENTOS B ON A.TIPO_DOCUMENTO = B.CODE WHERE A.AUTONUM = {0}")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(String.Format(SQL.ToString(), Lote), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AGENDAMENTO_DOC_VINCULO")

            Dim dt As New DataTable
            dt = Ds.Tables(0)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    Dim dto As New DocumentoDto With {
                        .Lote = dt.Rows(0)("AUTONUM").ToString(),
                        .TipoDocumento = dt.Rows(0)("DESCR").ToString(),
                        .NumeroDocumento = dt.Rows(0)("NUM_DOCUMENTO").ToString()
                    }

                    Return dto

                End If
            End If

        End Using

        Return Nothing

    End Function

    Public Shared Function DocumentosObrigatoriosDTA(ByVal Id As Long, ByVal Cntr As Long) As DataTable

        Dim Rst As New ADODB.Recordset

        Dim SqlDta As New StringBuilder()

        SqlDta.Append("SELECT DISTINCT ")
        SqlDta.Append("    A.DESCRICAO ")
        SqlDta.Append(" FROM  ")
        SqlDta.Append("    TB_AGENDAMENTO_DOC A ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_AV_IMAGEM B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_CNTR_BL C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SqlDta.Append(" WHERE  ")
        SqlDta.Append("    A.AUTONUM IN (1, 6) AND B.AUTONUM_AGENDAMENTO = " & Cntr & "  ")
        SqlDta.Append("UNION ALL ")
        SqlDta.Append("SELECT ") ' da averbação
        SqlDta.Append("    DISTINCT ")
        SqlDta.Append("        A.DESCRICAO ")
        SqlDta.Append(" FROM  ")
        SqlDta.Append("    TB_AGENDAMENTO_DOC A ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_AV_IMAGEM B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC ")
        SqlDta.Append(" INNER JOIN ")
        SqlDta.Append("    TB_AMR_CNTR_BL C ON B.LOTE = C.BL ")
        SqlDta.Append(" INNER JOIN ")
        SqlDta.Append("    TB_CNTR_BL D ON C.CNTR = D.AUTONUM ")
        SqlDta.Append(" WHERE  ")
        SqlDta.Append("    B.AUTONUM_AGENDAMENTO IS NULL AND A.AUTONUM IN (1, 6) AND D.AUTONUM = " & Cntr & " ")

        Dim dsDta As New DataTable

        Using Adapter As New OleDbDataAdapter
            Rst.Open(SqlDta.ToString(), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AGENDAMENTO_DOC")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Shared Function DocumentosQueFaltam(ByVal CodigoAgendamento As Integer, ByVal TipoDoc As Integer) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    B.AUTONUM, ")
        SQL.Append("    B.DESCRICAO ")
        SQL.Append("FROM  ")
        SQL.Append("    TB_AMR_AGENDAMENTO_DOC A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    SGIPA.TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("WHERE  ")
        SQL.Append("    A.AUTONUM_TIPO_DOC = " & TipoDoc)
        SQL.Append(" AND  ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO_DOC NOT IN ( ")
        SQL.Append("        SELECT ")
        SQL.Append("            A.AUTONUM_AGENDAMENTO_DOC ")
        SQL.Append("        FROM ")
        SQL.Append("            TB_AV_IMAGEM A ")
        SQL.Append("        INNER JOIN ")
        SQL.Append("            TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("        WHERE ")
        SQL.Append("            A.AUTONUM_AGENDAMENTO = " & CodigoAgendamento)
        SQL.Append(") ")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AMR_AGENDAMENTO_DOC")
            Return Ds.Tables(0)
        End Using

    End Function
    Public Shared Function ConsultarDoc(ByVal Lote As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append(" SELECT
                    b.autonum   
                FROM
                    tb_av_imagem a
                        INNER JOIN tb_agendamento_doc B ON a.autonum_agendamento_doc = B.autonum
                   
                WHERE
                    A.lote = " & Lote & "   group by b.autonum    
                    ")
        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.RecordCount
            End If
        End If

    End Function
    Public Shared Function Consultar(ByVal CodigoConteiner As String, ByVal CodigoMotorista As String, ByVal CodigoVeiculo As String, ByVal CodigoTransportadora As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append(" SELECT DISTINCT ")
        SQL.Append("        DESCRICAO, ")
        SQL.Append("        LOTE, ")
        SQL.Append("        NOME_IMG, ")
        SQL.Append("        DT_INCLUSAO, ")
        SQL.Append("        AUTONUM_AV_IMAGEM ")
        SQL.Append(" FROM ")
        SQL.Append(" ( ")

        ' Documentos da averbação
        SQL.Append("  SELECT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("   FROM ")
        SQL.Append("        TB_AV_IMAGEM A ")
        SQL.Append("   INNER JOIN ")
        SQL.Append("        TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("   INNER JOIN ")
        SQL.Append("        TB_AMR_CNTR_BL C ON A.LOTE = C.BL ")
        SQL.Append("   INNER JOIN ")
        SQL.Append("        TB_AV_ONLINE D ON C.BL = D.LOTE ")
        SQL.Append("   INNER JOIN ")
        SQL.Append("        TB_AV_ONLINE_TRANSP E ON D.AUTONUM = E.TB_AV_ONLINE ")
        SQL.Append("   WHERE ")
        SQL.Append("        C.CNTR = " & CodigoConteiner)
        SQL.Append("   AND ")
        SQL.Append("        E.TRANSPORTADORA = " & CodigoTransportadora)
        SQL.Append("   AND ")
        SQL.Append("        A.AUTONUM_AGENDAMENTO IS NULL ")
        SQL.Append("   AND ")
        SQL.Append("        A.AUTONUM_AGENDAMENTO_DOC <> 7 AND A.AUTONUM_AGENDAMENTO_DOC <> 8 AND A.AUTONUM_AGENDAMENTO_DOC <> 9 ")

        SQL.Append(" UNION ALL ")

        SQL.Append(" SELECT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append(" FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    TB_CNTR_BL C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SQL.Append(" WHERE ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & CodigoConteiner)

        'Os unions abaixo servem para pegar o documento padrão que é definido na tela do ADM Pátio (documentações)
        'Não há necessidade de enviar a CNH e doc do veiculo a cada agendamento

        SQL.Append(" UNION ALL ")

        SQL.Append(" SELECT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append(" FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM = C.DOC_MOTORISTA ")
        SQL.Append(" WHERE ")
        SQL.Append("    C.AUTONUM = " & CodigoMotorista)

        SQL.Append(" UNION ALL ")

        SQL.Append(" SELECT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append(" FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM = C.DOC_CAVALO ")
        SQL.Append(" WHERE ")
        SQL.Append("    C.AUTONUM = " & CodigoVeiculo)

        SQL.Append(" UNION ALL ")

        SQL.Append(" SELECT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append(" FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append(" INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM = C.DOC_CARRETA ")
        SQL.Append(" WHERE ")
        SQL.Append("    C.AUTONUM = " & CodigoVeiculo)
        SQL.Append(" ) ")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AV_IMAGEM")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Shared Function ObterTipoDocumento(ByVal Lote As Integer) As Short

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT NVL(TIPO_DOCUMENTO,0) TIPO_DOCUMENTO FROM TB_BL WHERE AUTONUM = " & Lote, Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("TIPO_DOCUMENTO").Value.ToString()
            End If
        End If

    End Function

    Public Shared Function ObterTipoDocumentoPorConteiner(ByVal Cntr As Integer) As Short

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT NVL(TIPO_DOCUMENTO,0) TIPO_DOCUMENTO FROM TB_BL A INNER JOIN TB_AMR_CNTR_BL B ON A.AUTONUM = B.BL WHERE B.CNTR = " & Cntr, Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("TIPO_DOCUMENTO").Value.ToString()
            End If
        End If

    End Function

    Public Shared Function ObterQuantidadeVinculada(ByVal CodigoConteiner As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        'SQL.Append("Select   DESCRICAO from TB_AGENDAMENTO_DOC A INNER JOIN TB_AMR_AGENDAMENTO_DOC B On A.AUTONUM=B.AUTONUM_AGENDAMENTO_DOC  ")
        'SQL.Append("INNER Join TB_BL BL ON BL.TIPO_DOCUMENTO=B.AUTONUM_TIPO_DOC ")
        'SQL.Append("INNER Join TB_AMR_CNTR_BL C ON BL.AUTONUM = C.BL  ")
        'SQL.Append("INNER Join TB_AV_IMAGEM IM ON BL.AUTONUM=IM.LOTE ")
        'SQL.Append("WHERE       C.CNTR = " & CodigoConteiner)
        'SQL.Append("Group BY A.DESCRICAO ")

        SQL.Append("
            SELECT 
                NVL(MAX(TOTAL + AVERBADOS),0) As Total FROM
            (
                SELECT
                    DISTINCT
                        A.AUTONUM_AGENDAMENTO_DOC As TOTAL,
                        COUNT(B.LOTE) As AVERBADOS
                FROM
                    SGIPA.TB_AV_IMAGEM A 
                LEFT JOIN
                    (
                        SELECT DISTINCT LOTE, AUTONUM_AGENDAMENTO_DOC FROM SGIPA.TB_AV_IMAGEM WHERE AUTONUM_AGENDAMENTO IS NULL
                    ) B ON A.LOTE = B.LOTE
                WHERE 
                    A.AUTONUM_AGENDAMENTO = " & CodigoConteiner & "
                GROUP BY
                    A.AUTONUM_AGENDAMENTO_DOC)")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Return Val(Rst.Fields("Total").Value.ToString())

    End Function

    Public Shared Function ObterQuantidadeAverbacao(ByVal CodigoConteiner As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(1) Total FROM sgipa.tb_av_imagem where lote IN (select bl from tb_amr_cntr_bl where cntr = " & CodigoConteiner & ")")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Return Val(Rst.Fields("Total").Value.ToString())

    End Function

    Public Shared Function ObterQuantidadeRequerida(ByVal CodigoDocumento As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT COUNT(1) TOTAL FROM TB_AMR_AGENDAMENTO_DOC WHERE AUTONUM_TIPO_DOC = " & CodigoDocumento, Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("TOTAL").Value.ToString()
            End If
        End If

    End Function

    Public Shared Function ObterDocumentoPorTipoDocumento(ByVal Documento As Integer) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    A.AUTONUM, ")
        SQL.Append("    A.DESCRICAO ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AGENDAMENTO_DOC A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AMR_AGENDAMENTO_DOC B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC ")
        SQL.Append("WHERE ")
        SQL.Append("    B.AUTONUM_TIPO_DOC = {0} ")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(String.Format(SQL.ToString(), Documento), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AGENDAMENTO_DOC")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Shared Function AgendamentoRecusado(ByVal ID As Integer, ByVal Transportadora As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT TRIM(MOTIVO_AGENDAMENTO_RECUSADO) MOTIVO_AGENDAMENTO_RECUSADO FROM TB_CNTR_BL WHERE NUM_PROTOCOLO || ANO_PROTOCOLO = " & ID & " AND AUTONUM_TRANSPORTE_AGENDA = " & Val(Transportadora), Banco.Conexao, 3, 3)

        Dim BloqueiaProtocolo As String = Banco.Conexao.Execute("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA").Fields(0).Value.ToString()

        If Not Rst.EOF Then
            If Not String.IsNullOrEmpty(Rst.Fields("MOTIVO_AGENDAMENTO_RECUSADO").Value.ToString()) Then
                If Val(BloqueiaProtocolo) > 0 Then
                    Return True
                End If
            End If
        End If

        Return False

    End Function

    Public Shared Function EhObrigatorioDocumentacaoDoAgendamento() As Boolean

        Dim BloqueiaProtocolo As String = Banco.Conexao.Execute("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA").Fields(0).Value.ToString()

        Return Val(BloqueiaProtocolo) > 0

    End Function

    Public Shared Function AgendamentoLiberado(ByVal ID As Integer, ByVal Transportadora As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            If Val(Rst.Fields("FLAG_BLOQUEIA_PROTOCOLO").Value.ToString()) = 0 Then
                Return True
            End If

            Rst.Close()

            Rst.Open("SELECT NVL(FLAG_LIBERADO,0) LIBERADO, NVL(IMPRESSO,0) IMPRESSO FROM TB_CNTR_BL WHERE NUM_PROTOCOLO || ANO_PROTOCOLO = " & ID & " AND AUTONUM_TRANSPORTE_AGENDA = " & Val(Transportadora), Banco.Conexao, 3, 3)

            If Not Rst.EOF Then
                If Val(Rst.Fields("LIBERADO").Value.ToString()) = 1 Or Val(Rst.Fields("IMPRESSO").Value.ToString()) = 1 Then
                    Return True
                End If
            End If

        End If

        Return False

    End Function

    Public Shared Function ExigeEmailCadastrado() As Boolean

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Val(Rst.Fields("FLAG_BLOQUEIA_PROTOCOLO").Value.ToString()) = 1 Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Shared Function DocumentoJaCadastrado(ByVal CodigoConteiner As String, ByVal CodigoDocumento As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(1) AS TOTAL ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_CNTR_BL C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM ")

        SQL.Append("WHERE ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & CodigoConteiner)

        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM = " & CodigoDocumento)

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("TOTAL").Value.ToString() > 0
            End If
        End If

    End Function

End Class

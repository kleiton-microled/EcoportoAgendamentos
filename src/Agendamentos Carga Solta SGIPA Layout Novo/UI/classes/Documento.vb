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

    Public Shared Function Consultar(ByVal CodigoMotorista As String, ByVal CodigoVeiculo As String, ByVal CodigoAgendamento As String, ByVal Lote As String, ByVal CodigoTransportadora As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        DESCRICAO, ")
        SQL.Append("        LOTE, ")
        SQL.Append("        NOME_IMG, ")
        SQL.Append("        DT_INCLUSAO, ")
        SQL.Append("        AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("( ")

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AV_ONLINE C ON A.LOTE = C.LOTE ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AV_ONLINE_TRANSP D ON C.AUTONUM = D.TB_AV_ONLINE ")
        SQL.Append("WHERE ")
        SQL.Append("    A.LOTE = " & Lote & " AND A.AUTONUM_AGENDAMENTO IS NULL ")
        SQL.Append("AND ")
        SQL.Append("    D.TRANSPORTADORA = " & CodigoTransportadora)
        SQL.Append(" AND ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO IS NULL ")
        SQL.Append(" AND ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO_DOC <> 7 AND A.AUTONUM_AGENDAMENTO_DOC <> 8 AND A.AUTONUM_AGENDAMENTO_DOC <> 9 ")

        SQL.Append(" UNION ALL ")

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("WHERE A.LOTE = " & Lote & "  AND  ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & CodigoAgendamento)

        'Os unions abaixo servem para pegar o documento padrão que é definido na tela do ADM Pátio (documentações)
        'Não há necessidade de enviar a CNH e doc do veiculo a cada agendamento

        SQL.Append(" UNION ALL ")

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM = C.DOC_MOTORISTA ")
        SQL.Append(" WHERE  A.LOTE = " & Lote & "  AND  ")
        SQL.Append("    C.AUTONUM = " & CodigoMotorista)

        SQL.Append(" UNION ALL ")

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM = C.DOC_CAVALO ")
        SQL.Append(" WHERE  A.LOTE = " & Lote & "  AND ")
        SQL.Append("    C.AUTONUM = " & CodigoVeiculo)

        SQL.Append(" UNION ALL ")

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("        B.DESCRICAO, ")
        SQL.Append("        A.LOTE, ")
        SQL.Append("        A.NOME_IMG, ")
        SQL.Append("        TO_CHAR(A.DT_INCLUSAO, 'DD/MM/YYYY HH24:MI') DT_INCLUSAO, ")
        SQL.Append("        A.AUTONUM AS AUTONUM_AV_IMAGEM ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM = C.DOC_CARRETA ")
        SQL.Append(" WHERE  A.LOTE = " & Lote & "  AND ")
        SQL.Append("    C.AUTONUM = " & CodigoVeiculo)
        SQL.Append(" ) ")

        Using Adapter As New OleDbDataAdapter
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AV_IMAGEM")
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
                    A.lote = " & Lote & "  group by b.autonum    
                    ")
        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.RecordCount
            End If
        End If

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


    Public Shared Function EhObrigatorioDocumentacaoDoAgendamento() As Boolean

        Dim BloqueiaProtocolo As String = Banco.Conexao.Execute("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA").Fields(0).Value.ToString()

        Return Val(BloqueiaProtocolo) > 0

    End Function

    Public Shared Function ObterQuantidadeVinculada(ByVal CodigoAgendamento As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT A.AUTONUM_AGENDAMENTO_DOC ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        'SQL.Append("INNER JOIN ")
        'SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        'SQL.Append("INNER JOIN ")
        'SQL.Append("    TB_AV_ONLINE C ON A.LOTE = C.LOTE ")
        'SQL.Append("INNER JOIN ")
        'SQL.Append("    TB_AV_ONLINE_TRANSP D ON C.AUTONUM = D.TB_AV_ONLINE ")
        SQL.Append("WHERE ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & CodigoAgendamento)
        'SQL.Append("AND ")
        'SQL.Append("    D.TRANSPORTADORA = " & CodigoTransportadora)

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Return Rst.RecordCount

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

    Public Shared Function DocumentosObrigatoriosDTA(ByVal Id As Long, ByVal lote As Long) As DataTable

        Dim Rst As New ADODB.Recordset

        Dim SqlDta As New StringBuilder()

        SqlDta.Append("SELECT ")
        SqlDta.Append("    DISTINCT ")
        SqlDta.Append("        A.DESCRICAO ")
        SqlDta.Append(" FROM  ")
        SqlDta.Append("    TB_AGENDAMENTO_DOC A ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_AV_IMAGEM B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_AG_CS C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SqlDta.Append(" WHERE  ")
        SqlDta.Append("    A.AUTONUM IN (1, 6) AND B.AUTONUM_AGENDAMENTO = " & Id & " ")
        SqlDta.Append("UNION ALL ")
        SqlDta.Append("SELECT ") ' da averbação
        SqlDta.Append("    DISTINCT ")
        SqlDta.Append("        A.DESCRICAO ")
        SqlDta.Append(" FROM  ")
        SqlDta.Append("    TB_AGENDAMENTO_DOC A ")
        SqlDta.Append(" INNER JOIN  ")
        SqlDta.Append("    TB_AV_IMAGEM B ON A.AUTONUM = B.AUTONUM_AGENDAMENTO_DOC ")
        SqlDta.Append(" WHERE  ")
        SqlDta.Append("    B.AUTONUM_AGENDAMENTO IS NULL AND A.AUTONUM IN (1, 6) AND B.LOTE = " & lote & " ")

        Dim dsDta As New DataTable

        Using Adapter As New OleDbDataAdapter
            Rst.Open(SqlDta.ToString(), Banco.Conexao, 3, 3)
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AGENDAMENTO_DOC")
            Return Ds.Tables(0)
        End Using

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

    Public Shared Function AgendamentoLiberado(ByVal ID As Integer) As Boolean

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT NVL(FLAG_BLOQUEIA_PROTOCOLO,0) FLAG_BLOQUEIA_PROTOCOLO FROM TB_PARAMETROS_SISTEMA", Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            If Val(Rst.Fields("FLAG_BLOQUEIA_PROTOCOLO").Value.ToString()) = 0 Then
                Return True
            End If

            Rst.Close()

            Rst.Open("SELECT NVL(FLAG_LIBERADO,0) LIBERADO, NVL(IMPRESSO,0) IMPRESSO FROM TB_AG_CS WHERE AUTONUM = " & ID, Banco.Conexao, 3, 3)

            If Not Rst.EOF Then

                If Val(Rst.Fields("IMPRESSO").Value.ToString()) = 1 Or Val(Rst.Fields("LIBERADO").Value.ToString()) = 1 Then
                    Return True
                End If

            End If

        End If

        Return False

    End Function

    Public Shared Function AgendamentoRecusado(ByVal ID As Integer) As Boolean

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT TRIM(MOTIVO_AGENDAMENTO_RECUSADO) MOTIVO_AGENDAMENTO_RECUSADO FROM TB_AG_CS WHERE AUTONUM = " & ID, Banco.Conexao, 3, 3)

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

    Public Shared Function DocumentoJaCadastrado(ByVal CodigoAgendamento As String, ByVal CodigoDocumento As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(1) AS TOTAL ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AV_IMAGEM A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    TB_AGENDAMENTO_DOC B ON A.AUTONUM_AGENDAMENTO_DOC = B.AUTONUM ")
        'SQL.Append("INNER JOIN ")
        'SQL.Append("    TB_AV_ONLINE C ON A.LOTE = C.LOTE ")
        'SQL.Append("INNER JOIN ")
        'SQL.Append("    TB_AV_ONLINE_TRANSP D ON C.AUTONUM = D.TB_AV_ONLINE ")
        SQL.Append("WHERE ")
        SQL.Append("    A.AUTONUM_AGENDAMENTO = " & CodigoAgendamento)
        SQL.Append("AND ")
        SQL.Append("    B.AUTONUM = " & CodigoDocumento)

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("TOTAL").Value.ToString() > 0
            End If
        End If

    End Function

    Public Shared Function DeletarDocumento(ByVal IDAvImagem As Integer) As Boolean

        Dim RstExcluir As New ADODB.Recordset
        Dim ws As New WsSharepoint.WsIccSharepoint()

        Try
            RstExcluir.Open("SELECT A.AUTONUM, A.LOTE FROM TB_AV_IMAGEM A WHERE A.AUTONUM = " & IDAvImagem, Banco.Conexao, 3, 3)

            If RstExcluir.EOF = False Then
                While RstExcluir.EOF = False
                    ws.ExcluirImagemDocAverbacaoPorLoteEautonum(Val(RstExcluir.Fields("LOTE").Value.ToString()), Val(RstExcluir.Fields("AUTONUM").Value.ToString()))
                    RstExcluir.MoveNext()
                End While
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True

    End Function

End Class

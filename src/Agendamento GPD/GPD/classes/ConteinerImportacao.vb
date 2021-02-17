Imports System.Data.OleDb

Public Class ConteinerImportacao



    Public Function ConsultarConteineresDisponiveis(ByVal ID As String, ByVal Empresa As String) As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("  VIP, ")
        SQL.Append("  PATIO, ")
        SQL.Append("  ID_CONTEINER, ")
        SQL.Append("  AUTONUM, ")
        SQL.Append("  TIPO_DOCUMENTO, ")
        SQL.Append("  NUM_DOCUMENTO, ")
        SQL.Append("  PESO_APURADO, ")
        SQL.Append("  TIPOCONTEINER, ")
        SQL.Append("  TAMANHO, ")
        SQL.Append("  IMO, ")
        SQL.Append("  ONU, ")
        SQL.Append("  DATA_FREE_TIME, ")
        SQL.Append("  REMOCAO, ")
        SQL.Append("  LOTE, ")
        SQL.Append("  CASE WHEN AGENDAR ='SIM' THEN 'NAO' ELSE 'SIM' END AGENDAR  ")
        SQL.Append("FROM SGIPA.VW_CNTR_DISPONIVEL ")
        SQL.Append("    WHERE ")
        SQL.Append("      TRANSPORTADORA = {0} ")
        If Empresa = 1 Then
            SQL.Append("  AND PATIO <> 5 ")
        Else
            SQL.Append("  AND PATIO = 5 ")
        End If
        SQL.Append("    AND ")
        SQL.Append("      (NVL(AUTONUM_TRANSPORTE_AGENDA, 0) = 0 OR (NVL(AUTONUM_TRANSPORTE_AGENDA, 0) = {0} )) ")
        SQL.Append("    ORDER  BY ID_CONTEINER  ")

        Return String.Format(SQL.ToString(), ID)

    End Function

    Public Function ConsultarConteineres(ByVal ID As String, ByVal Empresa As String) As DataTable
        'Tem uma pequena diferença com a pesquisa ConsultarConteineresDisponiveis

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder


        SQL.Append("SELECT ")
        SQL.Append("  VIP, ")
        SQL.Append("  PATIO, ")
        SQL.Append("  ID_CONTEINER, ")
        SQL.Append("  AUTONUM, ")
        SQL.Append("  TIPO_DOCUMENTO, ")
        SQL.Append("  NUM_DOCUMENTO, ")
        SQL.Append("  PESO_APURADO, ")
        SQL.Append("  TIPOCONTEINER, ")
        SQL.Append("  TAMANHO, ")
        SQL.Append("  IMO, ")
        SQL.Append("  ONU, ")
        SQL.Append("  DATA_FREE_TIME, ")
        SQL.Append("  REMOCAO, ")
        SQL.Append("  LOTE, ")
        SQL.Append("  AGENDAR ")
        SQL.Append("FROM SGIPA.VW_CNTR_DISPONIVEL ")
        SQL.Append("    WHERE ")
        SQL.Append("      TRANSPORTADORA = {0} ")
        If Empresa = 1 Then
            SQL.Append("  AND PATIO <> 5 ")
        Else
            SQL.Append("  AND PATIO = 5 ")
        End If
        SQL.Append("    AND ")
        SQL.Append("      (NVL(AUTONUM_TRANSPORTE_AGENDA, 0) = 0 OR (NVL(AUTONUM_TRANSPORTE_AGENDA, 0) = {0} )) ")

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter()

            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_CNTR_BL")
            Return Ds.Tables(0)

        End Using

    End Function

    Public Function ObterCodigoConteiner(ByVal Conteiner As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM FROM SGIPA.TB_CNTR_BL WHERE ID_CONTEINER='{0}'", Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM FROM SGIPA.DBO.TB_CNTR_BL WHERE ID_CONTEINER='{0}'", Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUM").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterLoteConteiner(ByVal Conteiner As String) As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM AS LOTE ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_BL A, ")
            SQL.Append("    SGIPA.TB_CNTR_BL I, ")
            SQL.Append("    SGIPA.TB_AMR_CNTR_BL J ")
            SQL.Append("WHERE ")
            SQL.Append("    J.BL=A.AUTONUM ")
            SQL.Append("AND ")
            SQL.Append("    I.AUTONUM=J.CNTR ")
            SQL.Append("AND ")
            SQL.Append("    I.AUTONUM='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.FLAG_ATIVO=1 ")
        Else

        End If

        Rst.Open(String.Format(SQL.ToString(), Conteiner), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("LOTE").Value.ToString()
        End If

        Return String.Empty

    End Function

End Class

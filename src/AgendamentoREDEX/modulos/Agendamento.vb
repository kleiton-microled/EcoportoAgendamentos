Module Agendamento

    Public Enum TipoAgendamento
        CARGA_SOLTA_CARREGAMENTO = 0
        CARGA_SOLTA_DESCARGA = 1
        CONTEINER_CARREGAMENTO = 2
        CONTEINER_DESCARGA = 3
    End Enum

    Public Function InsereAgendamentoNaFila(ByVal Conteiner As Integer, ByVal CodigoPeriodo As String, ByVal CodigoBooking As Integer, ByVal CodigoAgendamento As Integer, ByVal Tipo As TipoAgendamento) As Boolean

        Dim Ds As New DataTable
        Dim SQL As New StringBuilder

        Dim DataPrevista As String = Now.ToString()
        Dim Motivo As String = String.Empty

        Select Case Tipo
            Case TipoAgendamento.CARGA_SOLTA_CARREGAMENTO
                Motivo = Banco.ExecuteScalar("SELECT B.MOTIVO_CS_CARREGAMENTO FROM REDEX.TB_PARAMETROS_SISTEMA B,REDEX.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_CS_CARREGAMENTO=A.CODE")
            Case TipoAgendamento.CARGA_SOLTA_DESCARGA
                Motivo = Banco.ExecuteScalar("SELECT B.MOTIVO_CS_DESCARGA FROM REDEX.TB_PARAMETROS_SISTEMA B,REDEX.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_CS_DESCARGA=A.CODE")
            Case TipoAgendamento.CONTEINER_CARREGAMENTO
                Motivo = Banco.ExecuteScalar("SELECT B.MOTIVO_CNTR_CARREGAMENTO FROM REDEX.TB_PARAMETROS_SISTEMA B,REDEX.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_CNTR_CARREGAMENTO=A.CODE")
            Case TipoAgendamento.CONTEINER_DESCARGA
                Motivo = Banco.ExecuteScalar("SELECT B.MOTIVO_CNTR_DESCARGA FROM REDEX.TB_PARAMETROS_SISTEMA B,REDEX.TB_MOTIVO_POSICAO A WHERE B.MOTIVO_CNTR_DESCARGA=A.CODE")
        End Select

        If Motivo <> String.Empty Then

            Ds = Banco.List(String.Format("SELECT AUTONUM_GD_RESERVA,TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI:SS') PERIODO_INICIAL FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA={0}", CodigoPeriodo))

            If Ds IsNot Nothing Then
                If Ds.Rows.Count > 0 Then
                    DataPrevista = Ds.Rows(0)("PERIODO_INICIAL").ToString()
                End If
            End If

            If IsDate(DataPrevista) Then

                'Select Case Tipo
                '    Case TipoAgendamento.CARGA_SOLTA_CARREGAMENTO Or TipoAgendamento.CARGA_SOLTA_DESCARGA
                '        Ds = Banco.List(String.Format("SELECT A.AUTONUM FROM REDEX.TB_AGENDAMENTO_POSICAO A, REDEX.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.AUTONUM_BOOKING={0} AND A.CNTR=0 AND B.MOTIVO_POSICAO={1} AND A.AUTONUM_AGENDAMENTO={2}", CodigoBooking, Motivo, CodigoAgendamento))
                '    Case TipoAgendamento.CONTEINER_CARREGAMENTO Or TipoAgendamento.CONTEINER_DESCARGA
                '        Ds = Banco.List(String.Format("SELECT A.AUTONUM FROM REDEX.TB_AGENDAMENTO_POSICAO A, REDEX.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.AUTONUM_BOOKING={0} AND A.CNTR={1} AND B.MOTIVO_POSICAO={2} AND A.AUTONUM_AGENDAMENTO={3}", CodigoBooking, Val(Conteiner), Motivo, CodigoAgendamento))
                'End Select
                If Tipo = TipoAgendamento.CARGA_SOLTA_CARREGAMENTO Or Tipo = TipoAgendamento.CARGA_SOLTA_DESCARGA Then
                    Ds = Banco.List(String.Format("SELECT A.AUTONUM FROM REDEX.TB_AGENDAMENTO_POSICAO A, REDEX.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.AUTONUM_BOOKING={0} AND A.CNTR=0 AND B.MOTIVO_POSICAO={1} AND A.AUTONUM_AGENDAMENTO={2}", CodigoBooking, Motivo, CodigoAgendamento))
                End If

                If Tipo = TipoAgendamento.CONTEINER_CARREGAMENTO Or Tipo = TipoAgendamento.CONTEINER_DESCARGA Then
                    Ds = Banco.List(String.Format("SELECT A.AUTONUM FROM REDEX.TB_AGENDAMENTO_POSICAO A, REDEX.TB_AGENDA_POSICAO_MOTIVO B WHERE A.AUTONUM=B.AUTONUM_AGENDA_POSICAO AND A.AUTONUM_BOOKING={0} AND A.CNTR={1} AND B.MOTIVO_POSICAO={2} AND A.AUTONUM_AGENDAMENTO={3}", CodigoBooking, Val(Conteiner), Motivo, CodigoAgendamento))
                End If

                If Ds IsNot Nothing Then

                    If Ds.Rows.Count > 0 Then
                        If Banco.BeginTransaction(String.Format("UPDATE REDEX.TB_AGENDAMENTO_POSICAO SET DT_PREVISTA=TO_DATE('{0}','DD/MM/YYYY HH24:MI:SS'), DATA_ATUALIZA=SYSDATE WHERE AUTONUM={1}", DataPrevista, Val(Ds.Rows(0)("AUTONUM").ToString()))) Then
                            Return True
                        End If
                    Else

                        Dim Codigo As String = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_POSICAO.NEXTVAL FROM DUAL")

                        If Not String.IsNullOrEmpty(Codigo) Then

                            If Tipo = TipoAgendamento.CARGA_SOLTA_CARREGAMENTO Or Tipo = TipoAgendamento.CARGA_SOLTA_DESCARGA Then
                                Banco.BeginTransaction("INSERT INTO REDEX.TB_AGENDAMENTO_POSICAO (AUTONUM,CNTR,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS,AUTONUM_BOOKING,AUTONUM_AGENDAMENTO) VALUES (" & Codigo & ",0,TO_DATE('" & DataPrevista & "','DD/MM/YYYY HH24:MI:SS'),SYSDATE,0,REDEX.SEQ_OS" & Now.Year & ".NEXTVAL," & Now.Year & "," & CodigoBooking & "," & CodigoAgendamento & ")")
                            End If

                            If Tipo = TipoAgendamento.CONTEINER_CARREGAMENTO Or Tipo = TipoAgendamento.CONTEINER_DESCARGA Then
                                Banco.BeginTransaction("INSERT INTO REDEX.TB_AGENDAMENTO_POSICAO (AUTONUM,CNTR,DT_PREVISTA,DATA_ATUALIZA,ID_STATUS_AGENDAMENTO,NUM_OS,ANO_OS,AUTONUM_BOOKING,AUTONUM_AGENDAMENTO) VALUES (" & Codigo & "," & Conteiner & ",TO_DATE('" & DataPrevista & "','DD/MM/YYYY HH24:MI:SS'),SYSDATE,0,REDEX.SEQ_OS" & Now.Year & ".NEXTVAL," & Now.Year & "," & CodigoBooking & "," & CodigoAgendamento & ")")
                            End If

                            If Banco.BeginTransaction("INSERT INTO REDEX.TB_AGENDA_POSICAO_MOTIVO (AUTONUM,AUTONUM_AGENDA_POSICAO,MOTIVO_POSICAO) VALUES (REDEX.SEQ_AGENDA_POSICAO_MOTIVO.NEXTVAL," & Codigo & "," &  Motivo & ")") Then
                                Return True
                            End If

                        End If

                    End If

                End If

            End If

        End If

        Return False

    End Function

End Module

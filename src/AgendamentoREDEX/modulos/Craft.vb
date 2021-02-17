Module Craft

    Public Function MontaXMLCraft(ByVal Dados As WsCraft.clsDadosIntegracao) As String

        If Dados IsNot Nothing Then

            Dim strXML As String = String.Empty

            strXML = "<?xml version=""1.0"" encoding=""UTF-8""?>"
            strXML = strXML & "<CRAFT> "
            strXML = strXML & "    <HOUSE> "
            strXML = strXML & "        <CodigoReserva>" & Dados.Ex_Identificador & "</CodigoReserva> "
            strXML = strXML & "        <TypeImpExp>" & Dados.Ex_TpCarga2 & "</TypeImpExp> "
            strXML = strXML & "        <NumeroReserva>" & Dados.Ex_ReservaNr & "</NumeroReserva> "
            strXML = strXML & "        <DataReserva>" & Dados.Ex_DtReserva & "</DataReserva> "
            strXML = strXML & "        <NumeroHBL></NumeroHBL> "
            strXML = strXML & "        <Navio>" & Dados.Ex_Navio & "</Navio> "
            strXML = strXML & "        <Cliente_Razao>" & Dados.Ex_ClienteFanta & "</Cliente_Razao> "
            strXML = strXML & "        <Cliente_CNPJ>" & Dados.Ex_Cliente_CNPJ & "</Cliente_CNPJ> "
            strXML = strXML & "        <Cliente_Ref>" & Dados.Ex_RefCliente & "</Cliente_Ref> "
            strXML = strXML & "        <SeuCliente_Razao>" & Dados.Ex_SCli_Fanta & "</SeuCliente_Razao> "
            strXML = strXML & "        <SeuCliente_CNPJ>" & Dados.Ex_SCli_CNPJ & "</SeuCliente_CNPJ> "
            strXML = strXML & "        <Peso>" & Dados.Ex_Peso & "</Peso> "
            strXML = strXML & "        <CBM>" & Dados.Ex_Cubagem & "</CBM> "
            strXML = strXML & "        <Volume_Tipo>" & Dados.Ex_TipoVolume & "</Volume_Tipo> "
            strXML = strXML & "        <Volume_Nr>" & Dados.Ex_Quantidade & "</Volume_Nr> "
            strXML = strXML & "        <Descricao>" & Dados.Ex_Descricao & "</Descricao> "
            strXML = strXML & "        <Porto_Via>" & Dados.Ex_Via & "</Porto_Via> "
            strXML = strXML & "        <Porto_Destino>" & Dados.Ex_Destino & "</Porto_Destino> "
            strXML = strXML & "        <Carga_UN>" & Dados.Ex_ONU & "</Carga_UN> "
            strXML = strXML & "        <Carga_IMDG>" & Dados.Ex_IMO & "</Carga_IMDG> "
            strXML = strXML & "       <DeadLineCarga>" & Dados.Ex_DeadLine & "</DeadLineCarga> "
            strXML = strXML & "    </HOUSE> "
            strXML = strXML & "</CRAFT> "

            Return strXML

        End If

        Return String.Empty

    End Function

    Public Function ImportarXMLCraft(ByVal Reserva As String) As String

        Dim ws As New WsCraft.ConexaoMB
        Dim clsDados As WsCraft.clsDadosIntegracao
        Dim ret As String = String.Empty

        clsDados = ws.RetornaDadosReserva(Reserva, My.Settings.WsCraftUsuario, My.Settings.WsCraftSenha)

        If clsDados IsNot Nothing Then
            If Not String.IsNullOrEmpty(clsDados.Ex_ReservaNr) Then

                Dim xml As String = Craft.MontaXMLCraft(clsDados)

                If xml <> String.Empty Then

                    Dim wsCh As New WsBloqueioCraft.Ws

                    Try
                        ret = wsCh.Ler_EDI_Craft(String.Empty, xml)
                    Catch ex As Exception
                        ret = ex.Message
                    End Try

                End If

            End If
        End If

        Return ret

    End Function

End Module

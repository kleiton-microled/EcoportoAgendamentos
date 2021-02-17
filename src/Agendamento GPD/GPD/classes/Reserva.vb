Public Class Reserva

    Dim _Codigo As Integer
    Dim _Reserva As String
    Dim _Exportador As String
    Dim _Navioviagem As String
    Dim _Data_dead_line As String
    Dim _Autonum_viagem As String
    Dim _Autonum_reserva As String
    Dim _POD As String
    Dim _FDES As String
    Dim _EF As String
    Dim _Imo1 As String
    Dim _Imo2 As String
    Dim _Imo3 As String
    Dim _Imo4 As String
    Dim _Un1 As String
    Dim _Un2 As String
    Dim _Un3 As String
    Dim _Un4 As String
    Dim _Temperatura As String
    Dim _Escala As String
    Dim _Overheight As String
    Dim _Overwidth As String
    Dim _Overlength As String
    Dim _Overwidthl As String
    Dim _Obs As String
    Dim _Flag_Bloqueado As String
    Dim _Carrier As String
    Dim _Lacre1 As String
    Dim _Lacre2 As String
    Dim _Lacre3 As String
    Dim _Lacre4 As String
    Dim _Lacre5 As String
    Dim _Lacre6 As String
    Dim _Lacre7 As String
    Dim _LacreSIF As String    
    Dim _PesoBruto As String
    Dim _Tara As String
    Dim _Volumes As String
    Dim _Umidade As String
    Dim _Ventilacao As String
    Dim _Sigla As String
    Dim _Tipo As String
    Dim _Tamanho As String
    Dim _Transporte As String
    Dim _Vagao As String
    Dim _Late As String
    Dim _Transportadora As Transportadora

    Public Property Codigo() As Integer
        Get
            Return Me._Codigo
        End Get
        Set(ByVal value As Integer)
            Me._Codigo = value
        End Set
    End Property

    Public Property Reserva() As String
        Get
            Return Me._Reserva
        End Get
        Set(ByVal value As String)
            Me._Reserva = value
        End Set
    End Property

    Public Property Exportador() As String
        Get
            Return Me._Exportador
        End Get
        Set(ByVal value As String)
            Me._Exportador = value
        End Set
    End Property

    Public Property Navioviagem() As String
        Get
            Return Me._Navioviagem
        End Get
        Set(ByVal value As String)
            Me._Navioviagem = value
        End Set
    End Property

    Public Property Data_dead_line() As String
        Get
            Return Me._Data_dead_line
        End Get
        Set(ByVal value As String)
            Me._Data_dead_line = value
        End Set
    End Property

    Public Property Autonum_viagem() As String
        Get
            Return Me._Autonum_viagem
        End Get
        Set(ByVal value As String)
            Me._Autonum_viagem = value
        End Set
    End Property

    Public Property Autonum_reserva() As String
        Get
            Return Me._Autonum_reserva
        End Get
        Set(ByVal value As String)
            Me._Autonum_reserva = value
        End Set
    End Property

    Public Property POD() As String
        Get
            Return Me._POD
        End Get
        Set(ByVal value As String)
            Me._POD = value
        End Set
    End Property

    Public Property FDES() As String
        Get
            Return Me._FDES
        End Get
        Set(ByVal value As String)
            Me._FDES = value
        End Set
    End Property

    Public Property EF() As String
        Get
            Return Me._EF
        End Get
        Set(ByVal value As String)
            Me._EF = value
        End Set
    End Property

    Public Property Imo1() As String
        Get
            Return Me._Imo1
        End Get
        Set(ByVal value As String)
            Me._Imo1 = value
        End Set
    End Property

    Public Property Imo2() As String
        Get
            Return Me._Imo2
        End Get
        Set(ByVal value As String)
            Me._Imo2 = value
        End Set
    End Property

    Public Property Imo3() As String
        Get
            Return Me._Imo3
        End Get
        Set(ByVal value As String)
            Me._Imo3 = value
        End Set
    End Property

    Public Property Imo4() As String
        Get
            Return Me._Imo4
        End Get
        Set(ByVal value As String)
            Me._Imo4 = value
        End Set
    End Property

    Public Property Un1() As String
        Get
            Return Me._Un1
        End Get
        Set(ByVal value As String)
            Me._Un1 = value
        End Set
    End Property

    Public Property Un2() As String
        Get
            Return Me._Un2
        End Get
        Set(ByVal value As String)
            Me._Un2 = value
        End Set
    End Property

    Public Property Un3() As String
        Get
            Return Me._Un3
        End Get
        Set(ByVal value As String)
            Me._Un3 = value
        End Set
    End Property

    Public Property Un4() As String
        Get
            Return Me._Un4
        End Get
        Set(ByVal value As String)
            Me._Un4 = value
        End Set
    End Property

    Public Property Temperatura() As String
        Get
            Return Me._Temperatura
        End Get
        Set(ByVal value As String)
            Me._Temperatura = value
        End Set
    End Property

    Public Property Escala() As String
        Get
            Return Me._Escala
        End Get
        Set(ByVal value As String)
            Me._Escala = value
        End Set
    End Property

    Public Property Overheight() As String
        Get
            Return Me._Overheight
        End Get
        Set(ByVal value As String)
            Me._Overheight = value
        End Set
    End Property

    Public Property Overwidth() As String
        Get
            Return Me._Overwidth
        End Get
        Set(ByVal value As String)
            Me._Overwidth = value
        End Set
    End Property

    Public Property Overlength() As String
        Get
            Return Me._Overlength
        End Get
        Set(ByVal value As String)
            Me._Overlength = value
        End Set
    End Property

    Public Property Overwidthl() As String
        Get
            Return Me._Overwidthl
        End Get
        Set(ByVal value As String)
            Me._Overwidthl = value
        End Set
    End Property

    Public Property Obs() As String
        Get
            Return Me._Obs
        End Get
        Set(ByVal value As String)
            Me._Obs = value
        End Set
    End Property

    Public Property Flag_Bloqueado() As String
        Get
            Return Me._Flag_Bloqueado
        End Get
        Set(ByVal value As String)
            Me._Flag_Bloqueado = value
        End Set
    End Property

    Public Property Carrier() As String
        Get
            Return Me._Carrier
        End Get
        Set(ByVal value As String)
            Me._Carrier = value
        End Set
    End Property

    Public Property Lacre1() As String
        Get
            Return Me._Lacre1
        End Get
        Set(ByVal value As String)
            Me._Lacre1 = value
        End Set
    End Property

    Public Property Lacre2() As String
        Get
            Return Me._Lacre2
        End Get
        Set(ByVal value As String)
            Me._Lacre2 = value
        End Set
    End Property

    Public Property Lacre3() As String
        Get
            Return Me._Lacre3
        End Get
        Set(ByVal value As String)
            Me._Lacre3 = value
        End Set
    End Property

    Public Property Lacre4() As String
        Get
            Return Me._Lacre4
        End Get
        Set(ByVal value As String)
            Me._Lacre4 = value
        End Set
    End Property

    Public Property Lacre5() As String
        Get
            Return Me._Lacre5
        End Get
        Set(ByVal value As String)
            Me._Lacre5 = value
        End Set
    End Property

    Public Property Lacre6() As String
        Get
            Return Me._Lacre6
        End Get
        Set(ByVal value As String)
            Me._Lacre6 = value
        End Set
    End Property

    Public Property Lacre7() As String
        Get
            Return Me._Lacre7
        End Get
        Set(ByVal value As String)
            Me._Lacre7 = value
        End Set
    End Property

    Public Property LacreSIF() As String
        Get
            Return Me._LacreSIF
        End Get
        Set(ByVal value As String)
            Me._LacreSIF = value
        End Set
    End Property

    Public Property PesoBruto() As String
        Get
            Return Me._PesoBruto
        End Get
        Set(ByVal value As String)
            Me._PesoBruto = value
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

    Public Property Volumes() As String
        Get
            Return Me._Volumes
        End Get
        Set(ByVal value As String)
            Me._Volumes = value
        End Set
    End Property

    Public Property Umidade() As String
        Get
            Return Me._Umidade
        End Get
        Set(ByVal value As String)
            Me._Umidade = value
        End Set
    End Property

    Public Property Ventilacao() As String
        Get
            Return Me._Ventilacao
        End Get
        Set(ByVal value As String)
            Me._Ventilacao = value
        End Set
    End Property

    Public Property Sigla() As String
        Get
            Return Me._Sigla
        End Get
        Set(ByVal value As String)
            Me._Sigla = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return Me._Tipo
        End Get
        Set(ByVal value As String)
            Me._Tipo = value
        End Set
    End Property

    Public Property Tamanho() As String
        Get
            Return Me._Tamanho
        End Get
        Set(ByVal value As String)
            Me._Tamanho = value
        End Set
    End Property

    Public Property Transporte() As String
        Get
            Return Me._Transporte
        End Get
        Set(ByVal value As String)
            Me._Transporte = value
        End Set
    End Property

    Public Property Vagao() As String
        Get
            Return Me._Vagao
        End Get
        Set(ByVal value As String)
            Me._Vagao = value
        End Set
    End Property

    Public Property Transportadora() As Transportadora
        Get
            Return Me._Transportadora
        End Get
        Set(ByVal value As Transportadora)
            Me._Transportadora = value
        End Set
    End Property

    Public Property Late() As String
        Get
            Return Me._Late
        End Get
        Set(ByVal value As String)
            Me._Late = value
        End Set
    End Property

End Class
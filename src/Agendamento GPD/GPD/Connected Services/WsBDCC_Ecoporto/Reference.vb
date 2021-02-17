﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace WsBDCC_Ecoporto
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://tecondi.com.br/services/TecondiBdccWs", ConfigurationName:="WsBDCC_Ecoporto.WsSincronoSoap")>  _
    Public Interface WsSincronoSoap
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaCpf", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaCpf(ByVal cpf As String, ByVal registraEntradaSemCracha As Integer, ByVal cnpj As String, ByVal autonomo As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaRenavam", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaRenavam(ByVal renavam As String, ByVal registraEntradaSemCracha As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaCracha", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaCracha(ByVal cracha As String) As WsBDCC_Ecoporto.TecondiBdccCrachaResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/RegistraEntrada", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Sub RegistraEntrada(ByVal numeroCracha As String, ByVal tipoDocumento As String, ByVal numeroDocumento As String, ByVal local As String, ByVal usuarioLogado As String, ByVal ip As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/VerificacaoSGBDCC", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function VerificacaoSGBDCC() As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ListaAcessos", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ListaAcessos(ByVal dataInicial As String, ByVal dataFinal As String, ByVal local As String) As WsBDCC_Ecoporto.BdccAcesso()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaCpfIntranet", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaCpfIntranet(ByVal cpf As String, ByVal registraEntradaSemCracha As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaCNPJIntranet", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaCNPJIntranet(ByVal cnpj As String, ByVal autonomo As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tecondi.com.br/services/TecondiBdccWs/ConsultaEmpresaPorCnpj", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function ConsultaEmpresaPorCnpj(ByVal cnpj As String) As WsBDCC_Ecoporto.BdccCnpjEmpresa
    End Interface
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tecondi.com.br/services/TecondiBdccWs")>  _
    Partial Public Class TecondiBdccResponse
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private codigoRetornoField As String
        
        Private descricaoRetornoField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>  _
        Public Property CodigoRetorno() As String
            Get
                Return Me.codigoRetornoField
            End Get
            Set
                Me.codigoRetornoField = value
                Me.RaisePropertyChanged("CodigoRetorno")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>  _
        Public Property DescricaoRetorno() As String
            Get
                Return Me.descricaoRetornoField
            End Get
            Set
                Me.descricaoRetornoField = value
                Me.RaisePropertyChanged("DescricaoRetorno")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tecondi.com.br/services/TecondiBdccWs")>  _
    Partial Public Class BdccCnpjEmpresa
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private cnpjField As String
        
        Private nomeField As String
        
        Private situacaoField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>  _
        Public Property Cnpj() As String
            Get
                Return Me.cnpjField
            End Get
            Set
                Me.cnpjField = value
                Me.RaisePropertyChanged("Cnpj")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>  _
        Public Property Nome() As String
            Get
                Return Me.nomeField
            End Get
            Set
                Me.nomeField = value
                Me.RaisePropertyChanged("Nome")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>  _
        Public Property Situacao() As String
            Get
                Return Me.situacaoField
            End Get
            Set
                Me.situacaoField = value
                Me.RaisePropertyChanged("Situacao")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tecondi.com.br/services/TecondiBdccWs")>  _
    Partial Public Class BdccAcesso
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private numeroCrachaField As String
        
        Private tipoDocumentoField As String
        
        Private numeroDocumentoField As String
        
        Private dataAcessoField As Date
        
        Private localizacaoField As String
        
        Private usuarioLogadoField As String
        
        Private ipField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>  _
        Public Property NumeroCracha() As String
            Get
                Return Me.numeroCrachaField
            End Get
            Set
                Me.numeroCrachaField = value
                Me.RaisePropertyChanged("NumeroCracha")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>  _
        Public Property TipoDocumento() As String
            Get
                Return Me.tipoDocumentoField
            End Get
            Set
                Me.tipoDocumentoField = value
                Me.RaisePropertyChanged("TipoDocumento")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>  _
        Public Property NumeroDocumento() As String
            Get
                Return Me.numeroDocumentoField
            End Get
            Set
                Me.numeroDocumentoField = value
                Me.RaisePropertyChanged("NumeroDocumento")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>  _
        Public Property DataAcesso() As Date
            Get
                Return Me.dataAcessoField
            End Get
            Set
                Me.dataAcessoField = value
                Me.RaisePropertyChanged("DataAcesso")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=4)>  _
        Public Property Localizacao() As String
            Get
                Return Me.localizacaoField
            End Get
            Set
                Me.localizacaoField = value
                Me.RaisePropertyChanged("Localizacao")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=5)>  _
        Public Property UsuarioLogado() As String
            Get
                Return Me.usuarioLogadoField
            End Get
            Set
                Me.usuarioLogadoField = value
                Me.RaisePropertyChanged("UsuarioLogado")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=6)>  _
        Public Property Ip() As String
            Get
                Return Me.ipField
            End Get
            Set
                Me.ipField = value
                Me.RaisePropertyChanged("Ip")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tecondi.com.br/services/TecondiBdccWs")>  _
    Partial Public Class TecondiBdccCrachaResponse
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private codigoRetornoField As String
        
        Private descricaoRetornoField As String
        
        Private codigoCrachaField As String
        
        Private situacaoCrachaField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>  _
        Public Property CodigoRetorno() As String
            Get
                Return Me.codigoRetornoField
            End Get
            Set
                Me.codigoRetornoField = value
                Me.RaisePropertyChanged("CodigoRetorno")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>  _
        Public Property DescricaoRetorno() As String
            Get
                Return Me.descricaoRetornoField
            End Get
            Set
                Me.descricaoRetornoField = value
                Me.RaisePropertyChanged("DescricaoRetorno")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>  _
        Public Property CodigoCracha() As String
            Get
                Return Me.codigoCrachaField
            End Get
            Set
                Me.codigoCrachaField = value
                Me.RaisePropertyChanged("CodigoCracha")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>  _
        Public Property SituacaoCracha() As String
            Get
                Return Me.situacaoCrachaField
            End Get
            Set
                Me.situacaoCrachaField = value
                Me.RaisePropertyChanged("SituacaoCracha")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface WsSincronoSoapChannel
        Inherits WsBDCC_Ecoporto.WsSincronoSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class WsSincronoSoapClient
        Inherits System.ServiceModel.ClientBase(Of WsBDCC_Ecoporto.WsSincronoSoap)
        Implements WsBDCC_Ecoporto.WsSincronoSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function ConsultaCpf(ByVal cpf As String, ByVal registraEntradaSemCracha As Integer, ByVal cnpj As String, ByVal autonomo As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaCpf
            Return MyBase.Channel.ConsultaCpf(cpf, registraEntradaSemCracha, cnpj, autonomo)
        End Function
        
        Public Function ConsultaRenavam(ByVal renavam As String, ByVal registraEntradaSemCracha As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaRenavam
            Return MyBase.Channel.ConsultaRenavam(renavam, registraEntradaSemCracha)
        End Function
        
        Public Function ConsultaCracha(ByVal cracha As String) As WsBDCC_Ecoporto.TecondiBdccCrachaResponse Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaCracha
            Return MyBase.Channel.ConsultaCracha(cracha)
        End Function
        
        Public Sub RegistraEntrada(ByVal numeroCracha As String, ByVal tipoDocumento As String, ByVal numeroDocumento As String, ByVal local As String, ByVal usuarioLogado As String, ByVal ip As String) Implements WsBDCC_Ecoporto.WsSincronoSoap.RegistraEntrada
            MyBase.Channel.RegistraEntrada(numeroCracha, tipoDocumento, numeroDocumento, local, usuarioLogado, ip)
        End Sub
        
        Public Function VerificacaoSGBDCC() As String Implements WsBDCC_Ecoporto.WsSincronoSoap.VerificacaoSGBDCC
            Return MyBase.Channel.VerificacaoSGBDCC
        End Function
        
        Public Function ListaAcessos(ByVal dataInicial As String, ByVal dataFinal As String, ByVal local As String) As WsBDCC_Ecoporto.BdccAcesso() Implements WsBDCC_Ecoporto.WsSincronoSoap.ListaAcessos
            Return MyBase.Channel.ListaAcessos(dataInicial, dataFinal, local)
        End Function
        
        Public Function ConsultaCpfIntranet(ByVal cpf As String, ByVal registraEntradaSemCracha As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaCpfIntranet
            Return MyBase.Channel.ConsultaCpfIntranet(cpf, registraEntradaSemCracha)
        End Function
        
        Public Function ConsultaCNPJIntranet(ByVal cnpj As String, ByVal autonomo As Integer) As WsBDCC_Ecoporto.TecondiBdccResponse Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaCNPJIntranet
            Return MyBase.Channel.ConsultaCNPJIntranet(cnpj, autonomo)
        End Function
        
        Public Function ConsultaEmpresaPorCnpj(ByVal cnpj As String) As WsBDCC_Ecoporto.BdccCnpjEmpresa Implements WsBDCC_Ecoporto.WsSincronoSoap.ConsultaEmpresaPorCnpj
            Return MyBase.Channel.ConsultaEmpresaPorCnpj(cnpj)
        End Function
    End Class
End Namespace

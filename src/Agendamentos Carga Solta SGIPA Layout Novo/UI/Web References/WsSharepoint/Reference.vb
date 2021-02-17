﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace WsSharepoint
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WsIccSharepointSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class WsIccSharepoint
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private EnviarImagemDocAverbacaoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObterImagemDocAverbacaoPorLoteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObterImagemDocAverbacaoPorLoteEautonumOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObterInformacaoDasImagensGravadasNoSharepointOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.AgendamentoCargaSoltaSGIPA.My.MySettings.Default.AgendamentoCargaSoltaSGIPA_WsSharepoint_WsIccSharepoint
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event EnviarImagemDocAverbacaoCompleted As EnviarImagemDocAverbacaoCompletedEventHandler
        
        '''<remarks/>
        Public Event ObterImagemDocAverbacaoPorLoteCompleted As ObterImagemDocAverbacaoPorLoteCompletedEventHandler
        
        '''<remarks/>
        Public Event ObterImagemDocAverbacaoPorLoteEautonumCompleted As ObterImagemDocAverbacaoPorLoteEautonumCompletedEventHandler
        
        '''<remarks/>
        Public Event ExcluirImagemDocAverbacaoPorLoteEautonumCompleted As ExcluirImagemDocAverbacaoPorLoteEautonumCompletedEventHandler
        
        '''<remarks/>
        Public Event ObterInformacaoDasImagensGravadasNoSharepointCompleted As ObterInformacaoDasImagensGravadasNoSharepointCompletedEventHandler
        
        '''<remarks/>
        Public Event ObterInformacaoDasImagensGravadasNoSharepointPorDataCompleted As ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarImagemDocAverbacao", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub EnviarImagemDocAverbacao(ByVal averbacaoDocumento As ImagemAverbacao)
            Me.Invoke("EnviarImagemDocAverbacao", New Object() {averbacaoDocumento})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EnviarImagemDocAverbacaoAsync(ByVal averbacaoDocumento As ImagemAverbacao)
            Me.EnviarImagemDocAverbacaoAsync(averbacaoDocumento, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EnviarImagemDocAverbacaoAsync(ByVal averbacaoDocumento As ImagemAverbacao, ByVal userState As Object)
            If (Me.EnviarImagemDocAverbacaoOperationCompleted Is Nothing) Then
                Me.EnviarImagemDocAverbacaoOperationCompleted = AddressOf Me.OnEnviarImagemDocAverbacaoOperationCompleted
            End If
            Me.InvokeAsync("EnviarImagemDocAverbacao", New Object() {averbacaoDocumento}, Me.EnviarImagemDocAverbacaoOperationCompleted, userState)
        End Sub
        
        Private Sub OnEnviarImagemDocAverbacaoOperationCompleted(ByVal arg As Object)
            If (Not (Me.EnviarImagemDocAverbacaoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent EnviarImagemDocAverbacaoCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObterImagemDocAverbacaoPorLote", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObterImagemDocAverbacaoPorLote(ByVal lote As Long) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("ObterImagemDocAverbacaoPorLote", New Object() {lote})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObterImagemDocAverbacaoPorLoteAsync(ByVal lote As Long)
            Me.ObterImagemDocAverbacaoPorLoteAsync(lote, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObterImagemDocAverbacaoPorLoteAsync(ByVal lote As Long, ByVal userState As Object)
            If (Me.ObterImagemDocAverbacaoPorLoteOperationCompleted Is Nothing) Then
                Me.ObterImagemDocAverbacaoPorLoteOperationCompleted = AddressOf Me.OnObterImagemDocAverbacaoPorLoteOperationCompleted
            End If
            Me.InvokeAsync("ObterImagemDocAverbacaoPorLote", New Object() {lote}, Me.ObterImagemDocAverbacaoPorLoteOperationCompleted, userState)
        End Sub
        
        Private Sub OnObterImagemDocAverbacaoPorLoteOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObterImagemDocAverbacaoPorLoteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObterImagemDocAverbacaoPorLoteCompleted(Me, New ObterImagemDocAverbacaoPorLoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObterImagemDocAverbacaoPorLoteEautonum", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObterImagemDocAverbacaoPorLoteEautonum(ByVal lote As Long, ByVal autonum As Long) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("ObterImagemDocAverbacaoPorLoteEautonum", New Object() {lote, autonum})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObterImagemDocAverbacaoPorLoteEautonumAsync(ByVal lote As Long, ByVal autonum As Long)
            Me.ObterImagemDocAverbacaoPorLoteEautonumAsync(lote, autonum, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObterImagemDocAverbacaoPorLoteEautonumAsync(ByVal lote As Long, ByVal autonum As Long, ByVal userState As Object)
            If (Me.ObterImagemDocAverbacaoPorLoteEautonumOperationCompleted Is Nothing) Then
                Me.ObterImagemDocAverbacaoPorLoteEautonumOperationCompleted = AddressOf Me.OnObterImagemDocAverbacaoPorLoteEautonumOperationCompleted
            End If
            Me.InvokeAsync("ObterImagemDocAverbacaoPorLoteEautonum", New Object() {lote, autonum}, Me.ObterImagemDocAverbacaoPorLoteEautonumOperationCompleted, userState)
        End Sub
        
        Private Sub OnObterImagemDocAverbacaoPorLoteEautonumOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObterImagemDocAverbacaoPorLoteEautonumCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObterImagemDocAverbacaoPorLoteEautonumCompleted(Me, New ObterImagemDocAverbacaoPorLoteEautonumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ExcluirImagemDocAverbacaoPorLoteEautonum", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub ExcluirImagemDocAverbacaoPorLoteEautonum(ByVal lote As Long, ByVal autonum As Long)
            Me.Invoke("ExcluirImagemDocAverbacaoPorLoteEautonum", New Object() {lote, autonum})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ExcluirImagemDocAverbacaoPorLoteEautonumAsync(ByVal lote As Long, ByVal autonum As Long)
            Me.ExcluirImagemDocAverbacaoPorLoteEautonumAsync(lote, autonum, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ExcluirImagemDocAverbacaoPorLoteEautonumAsync(ByVal lote As Long, ByVal autonum As Long, ByVal userState As Object)
            If (Me.ExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted Is Nothing) Then
                Me.ExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted = AddressOf Me.OnExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted
            End If
            Me.InvokeAsync("ExcluirImagemDocAverbacaoPorLoteEautonum", New Object() {lote, autonum}, Me.ExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted, userState)
        End Sub
        
        Private Sub OnExcluirImagemDocAverbacaoPorLoteEautonumOperationCompleted(ByVal arg As Object)
            If (Not (Me.ExcluirImagemDocAverbacaoPorLoteEautonumCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ExcluirImagemDocAverbacaoPorLoteEautonumCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObterInformacaoDasImagensGravadasNoSharepoint", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObterInformacaoDasImagensGravadasNoSharepoint(ByVal lote As Long) As ImagemAverbacao()
            Dim results() As Object = Me.Invoke("ObterInformacaoDasImagensGravadasNoSharepoint", New Object() {lote})
            Return CType(results(0),ImagemAverbacao())
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObterInformacaoDasImagensGravadasNoSharepointAsync(ByVal lote As Long)
            Me.ObterInformacaoDasImagensGravadasNoSharepointAsync(lote, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObterInformacaoDasImagensGravadasNoSharepointAsync(ByVal lote As Long, ByVal userState As Object)
            If (Me.ObterInformacaoDasImagensGravadasNoSharepointOperationCompleted Is Nothing) Then
                Me.ObterInformacaoDasImagensGravadasNoSharepointOperationCompleted = AddressOf Me.OnObterInformacaoDasImagensGravadasNoSharepointOperationCompleted
            End If
            Me.InvokeAsync("ObterInformacaoDasImagensGravadasNoSharepoint", New Object() {lote}, Me.ObterInformacaoDasImagensGravadasNoSharepointOperationCompleted, userState)
        End Sub
        
        Private Sub OnObterInformacaoDasImagensGravadasNoSharepointOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObterInformacaoDasImagensGravadasNoSharepointCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObterInformacaoDasImagensGravadasNoSharepointCompleted(Me, New ObterInformacaoDasImagensGravadasNoSharepointCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObterInformacaoDasImagensGravadasNoSharepointPorData", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObterInformacaoDasImagensGravadasNoSharepointPorData(ByVal dataInicio As String, ByVal dataFinal As String) As ImagemAverbacao()
            Dim results() As Object = Me.Invoke("ObterInformacaoDasImagensGravadasNoSharepointPorData", New Object() {dataInicio, dataFinal})
            Return CType(results(0),ImagemAverbacao())
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObterInformacaoDasImagensGravadasNoSharepointPorDataAsync(ByVal dataInicio As String, ByVal dataFinal As String)
            Me.ObterInformacaoDasImagensGravadasNoSharepointPorDataAsync(dataInicio, dataFinal, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObterInformacaoDasImagensGravadasNoSharepointPorDataAsync(ByVal dataInicio As String, ByVal dataFinal As String, ByVal userState As Object)
            If (Me.ObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted Is Nothing) Then
                Me.ObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted = AddressOf Me.OnObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted
            End If
            Me.InvokeAsync("ObterInformacaoDasImagensGravadasNoSharepointPorData", New Object() {dataInicio, dataFinal}, Me.ObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted, userState)
        End Sub
        
        Private Sub OnObterInformacaoDasImagensGravadasNoSharepointPorDataOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObterInformacaoDasImagensGravadasNoSharepointPorDataCompleted(Me, New ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ImagemAverbacao
        
        Private autonumField As Long
        
        Private loteField As Long
        
        Private autonumAgendamentoField As System.Nullable(Of Long)
        
        Private tipoDocumentoField As String
        
        Private numeroDocumentoField As String
        
        Private dataInclusaoField As Date
        
        Private caminhoImagemField As String
        
        Private nomeImagemField As String
        
        Private idTipoDocUploadField As Integer
        
        Private descrTipoDocUploadField As String
        
        Private _byteArrayImagemField() As Byte
        
        '''<remarks/>
        Public Property Autonum() As Long
            Get
                Return Me.autonumField
            End Get
            Set
                Me.autonumField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property Lote() As Long
            Get
                Return Me.loteField
            End Get
            Set
                Me.loteField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(IsNullable:=true)>  _
        Public Property AutonumAgendamento() As System.Nullable(Of Long)
            Get
                Return Me.autonumAgendamentoField
            End Get
            Set
                Me.autonumAgendamentoField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property TipoDocumento() As String
            Get
                Return Me.tipoDocumentoField
            End Get
            Set
                Me.tipoDocumentoField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property NumeroDocumento() As String
            Get
                Return Me.numeroDocumentoField
            End Get
            Set
                Me.numeroDocumentoField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property DataInclusao() As Date
            Get
                Return Me.dataInclusaoField
            End Get
            Set
                Me.dataInclusaoField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property CaminhoImagem() As String
            Get
                Return Me.caminhoImagemField
            End Get
            Set
                Me.caminhoImagemField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property NomeImagem() As String
            Get
                Return Me.nomeImagemField
            End Get
            Set
                Me.nomeImagemField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property IdTipoDocUpload() As Integer
            Get
                Return Me.idTipoDocUploadField
            End Get
            Set
                Me.idTipoDocUploadField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property DescrTipoDocUpload() As String
            Get
                Return Me.descrTipoDocUploadField
            End Get
            Set
                Me.descrTipoDocUploadField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")>  _
        Public Property _byteArrayImagem() As Byte()
            Get
                Return Me._byteArrayImagemField
            End Get
            Set
                Me._byteArrayImagemField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub EnviarImagemDocAverbacaoCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub ObterImagemDocAverbacaoPorLoteCompletedEventHandler(ByVal sender As Object, ByVal e As ObterImagemDocAverbacaoPorLoteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObterImagemDocAverbacaoPorLoteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub ObterImagemDocAverbacaoPorLoteEautonumCompletedEventHandler(ByVal sender As Object, ByVal e As ObterImagemDocAverbacaoPorLoteEautonumCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObterImagemDocAverbacaoPorLoteEautonumCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub ExcluirImagemDocAverbacaoPorLoteEautonumCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub ObterInformacaoDasImagensGravadasNoSharepointCompletedEventHandler(ByVal sender As Object, ByVal e As ObterInformacaoDasImagensGravadasNoSharepointCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObterInformacaoDasImagensGravadasNoSharepointCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As ImagemAverbacao()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),ImagemAverbacao())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")>  _
    Public Delegate Sub ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEventHandler(ByVal sender As Object, ByVal e As ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObterInformacaoDasImagensGravadasNoSharepointPorDataCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As ImagemAverbacao()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),ImagemAverbacao())
            End Get
        End Property
    End Class
End Namespace
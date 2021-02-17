Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Web.Configuration

Public Class Banco

    Private Shared _Servidor As String
    Private Shared _Usuario As String
    Private Shared _Senha As String
    Private Shared _Schema As String

    Public Shared Property Servidor() As String
        Get
            Return _Servidor
        End Get
        Set(ByVal value As String)
            _Servidor = value
        End Set
    End Property

    Public Shared Property Usuario() As String
        Get
            Return _Usuario
        End Get
        Set(ByVal value As String)
            _Usuario = value
        End Set
    End Property

    Public Shared Property Senha() As String
        Get
            Return _Senha
        End Get
        Set(ByVal value As String)
            _Senha = value
        End Set
    End Property

    Public Shared Property Schema() As String
        Get
            Return _Schema
        End Get
        Set(ByVal value As String)
            _Schema = value
        End Set
    End Property

    Shared Sub New()

        Dim ConfigurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()

        Servidor = My.Settings.Servidor
        Usuario = My.Settings.Usuario
        Senha = My.Settings.Senha
        Schema = My.Settings.Schema

    End Sub

    Public Shared Function ExecuteScalar(ByVal SQL As String) As String

        Dim Result As Object = Nothing

        Using Con As New OleDbConnection(ConnectionString())
            Using Cmd As New OleDbCommand(SQL, Con)
                'Try
                Con.Open()
                Result = Cmd.ExecuteScalar()
                Con.Close()
                'Catch ex As Exception
                'Result = Nothing
                'End Try
            End Using
        End Using

        Return If(Result Is Nothing, Nothing, Result.ToString())

    End Function

    Public Shared Function BeginTransaction(ByVal SQL As String) As Boolean

        Dim Success As Boolean = False

        Using Con As New OleDbConnection(ConnectionString())
            Using Cmd As New OleDbCommand(SQL, Con)

                Dim Transaction As OleDbTransaction

                Con.Open()
                Transaction = Con.BeginTransaction()
                Cmd.Transaction = Transaction

                'Try
                Cmd.ExecuteNonQuery()
                Transaction.Commit()
                Success = True
                'Catch generatedExceptionName As Exception
                'Transaction.Rollback()
                'End Try

                Return Success

            End Using
        End Using

    End Function

    Public Shared Function List(ByVal SQL As String) As DataTable

        Dim Ds As New DataSet()

        Using Con As New OleDbConnection(ConnectionString())
            Using Adp As New OleDbDataAdapter(New OleDbCommand(SQL, Con))

                'Try
                Adp.Fill(Ds)
                'Catch generatedExceptionName As Exception
                'Return Nothing
                'End Try

                Return Ds.Tables(0)

            End Using
        End Using

    End Function

    Public Shared Function ListDs(ByVal SQL As String) As DataSet

        Dim Ds As New DataSet()

        Using Con As New OleDbConnection(ConnectionString())
            Using Adp As New OleDbDataAdapter(New OleDbCommand(SQL, Con))

                'Try
                Adp.Fill(Ds)
                'Catch generatedExceptionName As Exception
                'Return Nothing
                'End Try

                Return Ds

            End Using
        End Using

    End Function

    Public Shared Function Reader(ByVal SQL As String) As String()

        Dim lista As New List(Of String)()

        Using Con As New OleDbConnection(ConnectionString())
            Using Cmd As New OleDbCommand(SQL, Con)

                Dim dr As OleDbDataReader
                Con.Open()
                dr = Cmd.ExecuteReader()

                If dr.HasRows Then
                    While dr.Read()
                        lista.Add(dr(0).ToString())
                    End While
                End If

                dr.Close()
                Con.Close()

            End Using
        End Using

        Return lista.ToArray()

    End Function

    Public Shared Function ConnectionString() As String
        Return String.Format("Provider=OraOLEDB.Oracle;Data Source={0};User ID={1};Password={2};Unicode=True", Servidor, Usuario, Senha)
    End Function

End Class
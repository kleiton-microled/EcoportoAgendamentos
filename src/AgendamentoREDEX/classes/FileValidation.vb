Imports System.IO
Imports System.Runtime.InteropServices

Public Class FileValidation

    Public Shared Function IsValidFileType(fileByteContent As Byte()) As Boolean
        Dim isValid As Boolean = False
        Dim mimetypeOfFile As String = String.Empty

        Dim buffer As Byte() = New Byte(255) {}
        Using fs As New MemoryStream(fileByteContent)
            If fs.Length >= 256 Then
                fs.Read(buffer, 0, 256)
            Else
                fs.Read(buffer, 0, CInt(fs.Length))
            End If
        End Using

        Try
            Dim mimetype As System.UInt32
            FindMimeFromData(0, Nothing, buffer, 256, Nothing, 0,
                mimetype, 0)
            Dim mimeTypePtr As System.IntPtr = New IntPtr(mimetype)
            mimetypeOfFile = Marshal.PtrToStringUni(mimeTypePtr)
            Marshal.FreeCoTaskMem(mimeTypePtr)

        Catch e As Exception
        End Try

        If Not String.IsNullOrEmpty(mimetypeOfFile) Then
            Select Case mimetypeOfFile.ToLower()
                Case "application/msword"
                    ' for .doc  estension
                    isValid = True
                    Exit Select
                Case "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    ' for .docx  estension
                    isValid = True
                    Exit Select
                Case "application/vnd.ms-excel"
                    ' for .xls  estension
                    isValid = True
                    Exit Select
                Case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    ' for  .xlsx estension
                    isValid = True
                    Exit Select
                Case "application/vnd.ms-powerpoint"
                    ' for .ppt estension
                    isValid = True
                    Exit Select
                Case "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                    ' for .pptx estension
                    isValid = True
                    Exit Select
                Case "image/jpeg"
                    'jpeg and jpg both
                    isValid = True
                    Exit Select
                Case "image/pjpeg"
                    'jpeg and jpg both
                    isValid = True
                    Exit Select
                Case "image/png"
                    ' for .png estension
                    isValid = True
                    Exit Select
                Case "image/x-png"
                    ' for .png estension
                    isValid = True
                    Exit Select
                Case "image/gif"
                    ' for .gif estension
                    isValid = True
                    Exit Select
            End Select
        End If

        Return isValid

    End Function

    Public Shared Function GetStringApplicationOfMimetype(mimetypeOfFile As String) As String

        Select Case mimetypeOfFile.ToLower()
            Case "application/pdf"
                Exit Select
            Case "image/jpeg"
                'jpeg and jpg both
                'isValid = true;
                Exit Select
            Case "image/pjpeg"
                'jpeg and jpg both
                'isValid = true;
                Exit Select
            Case "image/png"
                ' for .png estension
                'isValid = true;
                Exit Select
            Case "image/x-png"
                ' for .png estension
                'isValid = true;
                Exit Select
            Case "image/gif"
                ' for .gif estension
                'isValid = true;
                Exit Select
        End Select

        Return String.Empty
    End Function

    <DllImport("urlmon.dll", CharSet:=CharSet.Auto)>
    Private Shared Function FindMimeFromData(pBC As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzUrl As System.String, <MarshalAs(UnmanagedType.LPArray)> pBuffer As Byte(), cbSize As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzMimeProposed As System.String, dwMimeFlags As System.UInt32,
        ByRef ppwzMimeOut As System.UInt32, dwReserverd As System.UInt32) As System.UInt32
    End Function

    Public Shared Function getMimeFromFile(byteArray As Byte()) As String

        Dim buffer As Byte() = New Byte(255) {}
        Using fs As New MemoryStream(byteArray)
            If fs.Length >= 256 Then
                fs.Read(buffer, 0, 256)
            Else
                fs.Read(buffer, 0, CInt(fs.Length))
            End If
        End Using
        Try
            Dim mimetype As System.UInt32
            FindMimeFromData(0, Nothing, buffer, 256, Nothing, 0,
                mimetype, 0)
            Dim mimeTypePtr As System.IntPtr = New IntPtr(mimetype)
            Dim mime As String = Marshal.PtrToStringUni(mimeTypePtr)
            Marshal.FreeCoTaskMem(mimeTypePtr)
            Return mime
        Catch e As Exception
            Return e.Message
        End Try
    End Function

End Class

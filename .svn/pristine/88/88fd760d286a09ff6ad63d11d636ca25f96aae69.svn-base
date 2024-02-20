Imports System.Data.Odbc
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web
Imports System
Public Class authdbconn
    Inherits System.Web.UI.Page
    Public Shared gs_ConnDB As OdbcConnection
    Public objCnf As ConfigurationManager
    Dim objODBCDataReader As OdbcDataReader
    Dim lsobjDR As OdbcDataReader
    Public trans As OdbcTransaction
    Dim conparameter As New List(Of String)
    Dim lsConnectionString As String = ""
    Dim lsDatabase As String
    Dim lsServer As String
    Dim lsUserID As String
    Dim lsPassword As String
    Dim lsPort As String

    Public Sub openconn_load()
        If lsConnectionString = "" Then

            Dim lsConnectionParameter As String = "pooling=true;Protocol=TCP;Min Pool Size=1;Max Pool Size=5;Connection Lifetime=0;Convert Zero Datetime=True"
            Dim lsRun_Type As String = UCase(ConfigurationManager.AppSettings("RUN_VERSION").ToString)

            conparameter = connectionstring(lsRun_Type)

            lsServer = conparameter.Item(0)
            lsDatabase = conparameter.Item(1)
            lsUserID = conparameter.Item(2)
            lsPassword = conparameter.Item(3)
            lsPort = conparameter.Item(4)

            lsConnectionString = "Driver={MySQL ODBC 5.3 UNICODE Driver};" &
                                  "Server=" & lsServer & ";" &
                                 "Database=" & lsDatabase & ";" &
                                 "User=" & lsUserID & ";" &
                                 "Password=" & lsPassword & ";" &
                                 "port=" & lsPort & ";" &
                                 "option=3;" &
                                 "OldSyntax=yes;" &
                                 lsConnectionParameter
            gs_ConnDB = New OdbcConnection(lsConnectionString)
            If gs_ConnDB.State <> ConnectionState.Open Then
                Try
                    gs_ConnDB.Open()
                    HttpContext.Current.Session("ConnectionString") = lsConnectionString
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Public Function connectionstring(ByVal Runversion As String) As List(Of String)
        Dim companyCode As String = HttpContext.Current.Request.QueryString("companycode")
        If companyCode = "VCIDEX" Then
            lsDatabase = "vcidex_angular"
            Select Case UCase(Runversion)
                Case "LIVE"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
                Case "TEST"
                    lsServer = "52.172.229.39"
                    lsUserID = "VCIDEX DBA"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case "LOCAL"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case Else
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
            End Select
        ElseIf companyCode = "vcidex" Then
            lsDatabase = "vcidex"
            Select Case UCase(Runversion)
                Case "LIVE"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
                Case "TEST"
                    lsServer = "52.172.229.39"
                    lsUserID = "VCIDEX DBA"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case "LOCAL"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case Else
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision18"
                    lsPort = 3306
            End Select
        ElseIf companyCode = "boba" Then
            lsDatabase = "boba_tea"
            Select Case UCase(Runversion)
                Case "LIVE"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
                Case "TEST"
                    lsServer = "52.172.229.39"
                    lsUserID = "VCIDEX DBA"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case "LOCAL"
                    lsServer = "localhost"
                    lsUserID = "VCIDEX DBA"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case Else
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision18"
                    lsPort = 3306
            End Select
        ElseIf companyCode = "bobatea" Then
            lsDatabase = "bobatea"
            Select Case UCase(Runversion)
                Case "LIVE"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
                Case "TEST"
                    lsServer = "52.172.229.39"
                    lsUserID = "VCIDEX DBA"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case "LOCAL"
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 4565
                Case Else
                    lsServer = "localhost"
                    lsUserID = "root"
                    lsPassword = "vision!8"
                    lsPort = 3306
            End Select
        End If

        Dim conparameter As New List(Of String)
        conparameter.Add(lsServer)
        conparameter.Add(lsDatabase)
        conparameter.Add(lsUserID)
        conparameter.Add(lsPassword)
        conparameter.Add(lsPort)
        Return conparameter

    End Function
End Class

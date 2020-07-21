
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class SqlUtility
    Public Const connectionStringName As String = "CSRDBConnectionString"
    Public Shared Function DeConstring() As String
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString
        Return connectionString
    End Function
    Public Shared Function GetConnection() As SqlConnection
        Dim log As New Log
        Dim er As New ApplicationException()
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString
        Dim conn As New SqlConnection(connectionString)
        If conn.State = ConnectionState.Closed Then
            Try
                conn.Open()
            Catch ex As Exception
                'Throw ex
                log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
            End Try
        End If
        Return conn
    End Function

    Public Shared Sub SqlExecute(ByVal sql As String)
        Dim log As New Log
        Dim conn As SqlConnection = Nothing
        Try
            sql = "BEGIN TRAN T1 " & sql & " COMMIT TRAN T1"

            conn = GetConnection()
            Dim cmd As New SqlCommand(sql, conn)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
            'Throw ex
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Public Shared Sub SqlExecute(ByVal sql As String, ByVal opc As SqlParameterCollection)
        Dim log As New Log
        Dim conn As SqlConnection = Nothing
        Try
            conn = GetConnection()
            Dim cmd As New SqlCommand(sql, conn)

            If opc.Count > 0 Then
                For Each op As SqlParameter In opc
                    cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value
                Next
            End If
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
            'Throw ex
        Finally
            conn.Close()
            conn.Dispose()

        End Try
    End Sub


    Public Shared Sub SqlExecute(ByVal sql As String, ByVal opc As SqlParameterCollection, ByVal tran As SqlTransaction, ByVal Con As SqlConnection) ', Optional ByVal Type As LHEnum.SQLDebugLog = LHEnum.SQLDebugLog.None)
        Dim log As New Log
        Try
            Dim cmd As New SqlCommand(sql, Con, tran)

            If opc.Count > 0 Then
                For Each op As SqlParameter In opc
                    cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value
                Next
            End If
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        End Try
    End Sub

    Public Shared Function SqlExecuteSP(ByVal sql As String, Optional ByVal opc As SqlParameterCollection = Nothing, Optional ByVal type As CommandType = CommandType.StoredProcedure, Optional ByVal tran As SqlTransaction = Nothing, Optional ByVal con As SqlConnection = Nothing) As DataSet
        Dim objCmd As New SqlCommand
        Dim dtAdapter As New SqlDataAdapter
        Dim ds As New DataSet
        Dim log As New Log
        Try
            If IsNothing(con) Then
                con = GetConnection()
            End If
            With objCmd
                .Connection = con
                .CommandText = sql
                .CommandType = type
                .Transaction = tran
            End With

            If opc IsNot Nothing Then
                If opc.Count > 0 Then
                    For Each op As SqlParameter In opc
                        objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value
                    Next
                End If
            End If

            dtAdapter.SelectCommand = objCmd
            dtAdapter.Fill(ds)

        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        Finally
            dtAdapter.Dispose()
            objCmd.Dispose()
            con.Close()
            con.Dispose()
        End Try
        Return ds
    End Function

    Public Shared Function SqlToValue(ByVal sql As String) As Object
        Dim cmd As New SqlCommand
        Dim obj As Object
        Dim log As New Log
        Dim con As New SqlConnection
        Try
            con = GetConnection()
            cmd.Connection = con
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text

            obj = cmd.ExecuteScalar()
            cmd.Connection.Close()
        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        Finally
            cmd.Dispose()
            con.Close()
            con.Dispose()
        End Try

        Return obj
    End Function

    Public Shared Function SqlToDataSet(ByVal sql As String) As DataSet

        Dim objConn As SqlConnection = GetConnection()
        Dim objCmd As New SqlCommand
        Dim dtAdapter As New SqlDataAdapter
        Dim ds As New DataSet
        Dim log As New Log
        Try
            With objCmd
                .Connection = objConn
                .CommandText = sql
                .CommandType = CommandType.Text
            End With
            dtAdapter.SelectCommand = objCmd
            dtAdapter.Fill(ds)
        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        Finally
            dtAdapter.Dispose()
            objConn.Close()
            objConn.Dispose()
        End Try

        Return ds
    End Function

    Public Shared Function SqlToTable(ByVal sql As String) As DataTable

        Dim objConn As SqlConnection = GetConnection()
        Dim objCmd As New SqlCommand
        Dim dtAdapter As New SqlDataAdapter
        Dim dt As New DataTable
        Dim log As New Log
        Try
            With objCmd
                .Connection = objConn
                .CommandText = sql
                .CommandType = CommandType.Text
            End With
            dtAdapter.SelectCommand = objCmd
            dtAdapter.Fill(dt)

        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        Finally
            dtAdapter.Dispose()
            objConn.Close()
            objConn.Dispose()
        End Try

        Return dt
    End Function

    Public Shared Function SqlExecuteScalar(ByVal sql As String, ByVal opc As SqlParameterCollection) As Object
        Dim log As New Log
        Dim obj As Object
        Dim conn As SqlConnection = Nothing
        Try

            conn = GetConnection()

            Dim cmd As New SqlCommand(sql, conn)
            cmd.CommandTimeout = 600
            If opc.Count > 0 Then
                For Each op As SqlParameter In opc
                    cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value
                Next
            End If
            obj = cmd.ExecuteScalar()
            Return obj
        Catch ex As Exception
            'Throw ex
            Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Function

    Public Shared Function SqlExecuteScalar(ByVal sql As String, ByVal opc As SqlParameterCollection, ByVal conn As SqlConnection, ByVal tran As SqlTransaction) As Object
        Dim log As New Log
        Dim obj As Object
        Try
            Dim cmd As New SqlCommand(sql, conn, tran)
            cmd.CommandTimeout = 600
            If Not opc Is Nothing Then
                If opc.Count > 0 Then
                    For Each op As SqlParameter In opc
                        cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value
                    Next
                End If
            End If
            obj = cmd.ExecuteScalar()
            Return obj
        Catch ex As Exception
            Return False
            'Throw ex
            log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        End Try
    End Function
End Class

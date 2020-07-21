Imports System.Data.SqlClient
Imports System.Data

Public Enum SqlHelperType
    Insert = 1
    InsertIdentity = 2
    Update = 3
    Delete = 4
    DeleteFlag = 5
End Enum

Public Class SqlHelper
    Private _tableName As String
    Private _where As String
    Private _column As Dictionary(Of String, Object)
    Private _whereParam As Dictionary(Of String, Object)
    Private _statementType As SqlHelperType
    Private _con As SqlConnection
    Private _tran As SqlTransaction

    Public Sub New()
        Me._tableName = String.Empty
        Me._column = New Dictionary(Of String, Object)()
        Me._whereParam = New Dictionary(Of String, Object)()
    End Sub
    Public Sub New(ByVal con As SqlConnection, ByVal tran As SqlTransaction)
        Me._tableName = String.Empty
        Me._column = New Dictionary(Of String, Object)()
        Me._whereParam = New Dictionary(Of String, Object)()
        Me._con = con
        Me._tran = tran
    End Sub

    Public Sub NewInsertStatement(ByVal tableName As String)
        Me._tableName = tableName
        Me._statementType = SqlHelperType.Insert
    End Sub

    Public Sub NewInsertIdentityStatement(ByVal tableName As String)
        Me._tableName = tableName
        Me._statementType = SqlHelperType.InsertIdentity
    End Sub

    Public Sub NewUpdateStatement(ByVal tableName As String)
        Me._tableName = tableName
        Me._statementType = SqlHelperType.Update
    End Sub

    Public Sub NewDeleteStatement(ByVal tableName As String)
        Me._tableName = tableName
        Me._statementType = SqlHelperType.Delete
    End Sub

    Public Sub Where(ByVal where As String)
        Me._where = where
    End Sub

    Public Sub WhereParam(ByVal ColumnName As String, ByVal ColumnValue As Object)
        Try
            _whereParam.Add(ColumnName, ColumnValue)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetColumnValue(ByVal ColumnName As String, ByVal ColumnValue As Object)
        Try
            _column.Add(ColumnName, ColumnValue)
        Catch ex As Exception

        End Try
    End Sub

    Public Function Execute() As Object
        Dim obj As Object = Nothing
        Select Case Me._statementType
            Case SqlHelperType.Insert
                InsertExecute()
            Case SqlHelperType.InsertIdentity
                obj = InsertIdentity()
            Case SqlHelperType.Update
                UpdateExecute()
            Case SqlHelperType.Delete
                DeleteExecute()
        End Select
        Return obj
    End Function


    Private Sub InsertExecute()
        Dim opc As SqlParameterCollection = New SqlCommand().Parameters
        Dim isFirst As Boolean = True
        Dim SqlVal As String = String.Empty
        Dim op As SqlParameter
        Dim sql As String = "INSERT INTO " + Me._tableName + " ("
        For Each item In Me._column
            If isFirst Then
                sql += item.Key.ToString()
                isFirst = False
            Else
                sql += "," + item.Key.ToString()
            End If

            op = New SqlParameter
            op.ParameterName = "@" + item.Key.ToString()
            op.Value = item.Value
            opc.Add(op)

            If SqlVal = String.Empty Then
                SqlVal += "@" + item.Key.ToString()
                isFirst = False
            Else
                SqlVal += ",@" + item.Key.ToString()
            End If

        Next

        sql += ") VALUES ("
        sql += SqlVal
        sql += ")"


        SqlUtility.SqlExecute(sql, opc)
    End Sub

    Private Function InsertIdentity() As String

        Dim opc As SqlParameterCollection = New SqlCommand().Parameters
        Dim isFirst As Boolean = True
        Dim op As SqlParameter
        Dim SqlVal As String = String.Empty
        Dim sql As String = "INSERT INTO " + Me._tableName + " ("
        For Each item In Me._column
            If isFirst Then
                sql += item.Key.ToString()
                isFirst = False
            Else
                sql += "," + item.Key.ToString()
            End If


            op = New SqlParameter
            op.ParameterName = "@" + item.Key.ToString()
            op.Value = item.Value
            opc.Add(op)
            If SqlVal = String.Empty Then
                SqlVal += "@" + item.Key.ToString()
            Else
                SqlVal += ",@" + item.Key.ToString()
            End If
        Next

        sql += ") VALUES ("
        sql += SqlVal
        sql += ") SELECT CAST(scope_identity() AS int)"

        If IsNothing(_con) Then
            Return SqlUtility.SqlExecuteScalar(sql, opc)
        Else
            Return SqlUtility.SqlExecuteScalar(sql, opc, _con, _tran)
        End If

    End Function

    Private Sub UpdateExecute()
        Dim opc As SqlParameterCollection = New SqlCommand().Parameters
        Dim op As SqlParameter
        Dim isFirst As Boolean = True

        Dim sql As String = "UPDATE " + Me._tableName + " SET "
        For Each item In Me._column
            If isFirst Then
                isFirst = False
            Else
                sql += ","
            End If

            sql += item.Key.ToString() + " = @" + item.Key.ToString()
            op = New SqlParameter
            op.ParameterName = "@" + item.Key.ToString()
            op.Value = item.Value
            opc.Add(op)
        Next

        If Me._where <> "" Then
            sql += " WHERE " + Me._where
        End If

        For Each item In Me._whereParam
            op = New SqlParameter
            op.ParameterName = "@" + item.Key.ToString()
            op.Value = item.Value
            opc.Add(op)
        Next

        If IsNothing(_con) Then
            SqlUtility.SqlExecute(sql, opc)
        Else
            SqlUtility.SqlExecute(sql, opc, _tran, _con)
        End If

    End Sub

    Private Sub DeleteExecute()
        Dim opc As SqlParameterCollection = New SqlCommand().Parameters
        Dim op As SqlParameter
        Dim isFirst As Boolean = True

        Dim sql As String = "DELETE FROM " + Me._tableName

        If Me._where <> "" Then
            sql += " WHERE " + Me._where
        End If

        For Each item In Me._whereParam
            op = New SqlParameter
            op.ParameterName = "@" + item.Key.ToString()
            op.Value = item.Value
            opc.Add(op)
        Next

        SqlUtility.SqlExecute(sql, opc)
    End Sub

End Class

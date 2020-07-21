Imports System.Data
Public Class UserProfile
    Public Property UserId As Integer?
    Public Property BranchId As String
    Public Property NodeId As Integer
    Public Property NodeGrpId As Integer
    'Public Property NodeDesc As String
    Public Property UserName As String
    Public Property FullName As String
    Public Property UserGroupID As Integer
    Public Property UserGroupDesc As String
    Public Property CostCenterId As Integer
    Public Property EmpCode As String
    Public Property AOCode As String
    Public Property LastLogin As String
    Public Property CurrPage As String
    Public Property Search As Dictionary(Of String, String)
    'Public Property Node As Dictionary(Of String, Node)
End Class
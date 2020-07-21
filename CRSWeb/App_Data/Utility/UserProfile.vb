Imports System.Data

Public Class UserProfile
    Public Property UserId As Integer
    Public Property Empcode As String
    Public Property UserName As String
    Public Property FullName As String
    Public Property Position As String
    Public Property Section As String
    Public Property Department As String
    Public Property Division As String
    Public Property Phone As String
    Public Property EMail As String
    Public Property PictureURL As String
    Public Property TitlePage As String
End Class

Public Class SysProject
    Public Property ProjectId As String
    Public Property ProjectName As String
    Public Property DateFr As String
    Public Property DateTo As String
    Public Property SubjectDetail As String
    Public Property ActiveFlag As String
    Public Property CreateDate As String
    Public Property CreateBy As String
    Public Property UpdateDate As String
    Public Property UpdateBy As String

    'Tab 1 
    Public Property Tab1_CodeCtrlId As String
    Public Property Tab1_FontName As String
    Public Property Tab1_FontColor As String
    Public Property Tab1_BGColor As String
    Public Property Tab1_Picture As String
    Public Property Tab1_URL As String
    Public Property Tab1_FileName As String
    Public Property MapDirectory As String


    'Tab 2 
    Public Property Tab2_CodeCtrlId As String
    Public Property Tab2_FontColor As String
    Public Property Tab2_BGColor As String

    'Tab 3
    Public Property Tab3_CodeCtrlId As String
    Public Property Tab3_BGColor As String
    Public Property Tab3_FontColor As String
    Public Property Tab3_TextMessage As String
    Public Property Tab3_TextFontName As String
    Public Property Tab3_TextFontColor As String
    Public Property Tab3_Picture As String
    Public Property Tab3_FileName As String

    'Tab 4
    Public Property Tab4_CodeCtrlId As String
    Public Property Tab4_BGColor As String
    Public Property Tab4_TextMessage As String
    Public Property Tab4_TextFontName As String
    Public Property Tab4_TextFontColor As String
    Public Property Tab4_Picture As String
    Public Property Tab4_FileName As String


    'Tab 5
    Public Property Tab5_CodeCtrlId As String
    Public Property Tab5_BGColor As String
    Public Property Tab5_FontColor As String

    'Tab 6
    Public Property Tab6_CodeCtrlId As String
    Public Property Tab6_BGColor As String
    Public Property Tab6_TextMessage As String
    Public Property Tab6_TextFontName As String
    Public Property Tab6_TextFontColor As String

End Class

Public Class SysEmployee
    Public Property empid As String
    Public Property empcode As String
    Public Property UserName As String
    Public Property FullName As String
    Public Property Position As String
    Public Property Section As String
    Public Property Department As String
    Public Property SECTER As String
    Public Property Phone As String
    Public Property EMail As String
    Public Property PictureURL As String
    Public Property Company As String
    Public Property UpdateDate As String
End Class

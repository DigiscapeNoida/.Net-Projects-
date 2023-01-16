Imports System.IO
Imports Microsoft.Office.Interop.Word
Public Class WordProcess
    Dim _DocText As String
    Property DocText() As String
        Get
            Return _DocText
        End Get
        Set(value As String)
            _DocText = value
        End Set
    End Property

    Public Sub New()
    End Sub
    Public Function GetMSSPages(ByVal fName As String) As Integer
        If Path.GetExtension(fName).Equals(".doc") Then
            Exit Function
        End If
        Dim count As Integer
        Dim wordapp As New Application
        Dim adoc As New Document
        Try
            adoc = wordapp.Documents.Open(fName)
            adoc.Paragraphs.Item(wordapp.ActiveDocument.Paragraphs.Count).Range.Select()
            count = wordapp.Selection.Information(WdInformation.wdNumberOfPagesInDocument)
            adoc.Range.Select()
            _DocText = adoc.Range.Text
        Catch ex As Exception

        End Try
        adoc.Close(False)
        wordapp.Quit(False)
        Return count
    End Function
    Public Sub SaveAsHTML(ByVal FileName As String)
        Dim wordapp As New Application
        Dim adoc As Document
        Try
            Dim HTMLFileName As String
            HTMLFileName = Path.GetDirectoryName(FileName) & "\\" & Path.GetFileNameWithoutExtension(FileName) & ".htm"
            adoc = wordapp.Documents.Open(FileName, , , , , , , , , , , True, , , )
            ''  adoc.Paragraphs.Item(wordapp.ActiveDocument.Paragraphs.Count).Range.Select()
            adoc.SaveAs(HTMLFileName, FileFormat:=WdSaveFormat.wdFormatFilteredHTML)
        Catch ex As Exception
        End Try
        wordapp.Quit(False)
    End Sub
End Class

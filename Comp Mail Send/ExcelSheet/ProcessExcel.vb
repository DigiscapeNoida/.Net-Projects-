Imports System.IO
Imports Microsoft.Office.Interop.Excel
Public Class ProcessExcel

    Public Sub ConvertToXML(ByVal ExlPath As String)

        Dim objBooks As New Microsoft.Office.Interop.Excel.Application
        Dim WB As Microsoft.Office.Interop.Excel.Workbook
        'Dim objSheets As Sheets
        'Dim objSheet As Worksheets

        ''Excel = New Microsoft.Office.Interop.Excel.ApplicationClass()
        ''ActiveWorkbook.SaveAs(Filename:="C:\Users\59882\Desktop\1.xml", FileFormat:=xlXMLSpreadsheet, ReadOnlyRecommended:=False, CreateBackup:=False)
        WB = objBooks.Workbooks.Open(ExlPath)
        WB.SaveAs(Path.ChangeExtension(ExlPath, ".xml"), Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, AccessMode:=Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange)
        WB.Close()


    End Sub

End Class

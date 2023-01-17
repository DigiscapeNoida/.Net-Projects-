Imports System.Configuration
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions

Public Class Form3
    Dim ISBN As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try



            Dim ChapterID As String = ""
            Dim AuthorName As String = ""
            Dim AuthorEmail As String = ""

            Dim EditorName As String = ""
            Dim EditorEmail As String = ""

            Dim ElsevierPMMail As String = ""
            Dim ElsevierPMName As String = ""
            Dim AdditionalCCAUMail As String = ""
            Dim AdditionalCCEDMail As String = ""
            Dim ThomsonPMMail As String = ""
            Dim ThomsonPMname As String = ""
            Dim DaysforAuthor As String = ""
            Dim DaysforEditors As String = ""
            Dim FTPDetails As String = ""
            Dim FinalStr As String = ""
            Dim JobType As String
            Dim arr1(14) As String

            If Len(ISBN) <> 13 Then
                Throw New Exception("ISBN Missing..")
            End If

            If ComboBox4.Text <> "" Then
                ChapterID = ComboBox4.Text
                arr1(0) = (ChapterID)
            Else
                Throw New Exception("ChapterID Missing..")
            End If

            If TextBox2.Text <> "" Then
                If Form1.checkAlphabetsOnly(TextBox2.Text) Then
                    AuthorName = TextBox2.Text
                    arr1(1) = (AuthorName)
                Else
                    Throw New Exception("Author Name must have alphabets or Space")
                End If
                
            Else
                Throw New Exception("AuthorName Missing..")
            End If

            If TextBox3.Text <> "" And TextBox3.Text.Contains("@") = True Then
                AuthorEmail = TextBox3.Text
                arr1(2) = (AuthorEmail)
            Else
                Throw New Exception("AuthorEmail must contain correct email ID..")
            End If



            If TextBox7.Text <> "" Then
                EditorName = TextBox7.Text
                arr1(3) = (EditorName)
            Else
                Throw New Exception("EditorName Missing..")
            End If

            If TextBox1.Text <> "" And TextBox1.Text.Contains("@") = True Then
                EditorEmail = TextBox1.Text
                arr1(4) = (EditorEmail)
            Else
                Throw New Exception("EditorEmail must contain correct email ID..")
            End If

            If ComboBox5.Text <> "" And ComboBox5.Text.Contains("@") = True Then
                ElsevierPMMail = ComboBox5.Text.Trim
                arr1(5) = ElsevierPMMail
            Else
                Throw New Exception("ElsevierPMMail must contain correct email ID..")
            End If

            If ComboBox6.Text <> "" Then
                ElsevierPMName = ComboBox6.Text
                arr1(6) = ElsevierPMName
            Else
                Throw New Exception("ElsevierPMName Missing..")
            End If

            If TextBox6.Text <> "" Then
                If TextBox6.Text.Contains("@") = True Then
                    AdditionalCCAUMail = TextBox6.Text
                    arr1(7) = AdditionalCCAUMail
                Else
                    Throw New Exception("AdditionalCCAUMail must contain correct email ID..")
                End If
            Else
                AdditionalCCAUMail = "Null"
                arr1(7) = ""
            End If
            If TextBox8.Text <> "" Then
                If TextBox8.Text.Contains("@") = True Then
                    AdditionalCCEDMail = TextBox8.Text
                    arr1(8) = AdditionalCCEDMail
                Else
                    Throw New Exception("AdditionalCCEDMail must contain correct email ID..")
                End If
            Else
                AdditionalCCEDMail = "Null"
                arr1(8) = ""
            End If

            If ComboBox2.Text <> "" And ComboBox2.Text.Contains("@") = True Then
                ThomsonPMMail = ComboBox2.Text
                arr1(9) = (ThomsonPMMail)
            Else
                Throw New Exception("ThomsonPMMail must contain correct email ID..")
            End If

            If ComboBox3.Text <> "" Then
                ElsevierPMName = ComboBox3.Text
                arr1(10) = (ElsevierPMName)
            Else
                Throw New Exception("ThomsonPMname Missing..")
            End If

            If TextBox9.Text <> "" And IsNumeric(TextBox9.Text) = True Then
                DaysforAuthor = TextBox9.Text
                arr1(11) = (DaysforAuthor)
            Else
                Throw New Exception("DaysforAuthor must contain Numeric Value..")
            End If


            If TextBox11.Text <> "" And IsNumeric(TextBox11.Text) = True Then
                DaysforEditors = TextBox11.Text
                arr1(12) = (DaysforEditors)
            Else
                Throw New Exception("DaysforEditors must contain Numeric Value..")
            End If


            If ComboBox1.Text <> "" Then
                JobType = ComboBox1.Text
                If JobType = "S&T" Then
                    JobType = "SNT"
                End If
                arr1(13) = JobType
            Else
                Throw New Exception("FTPDetails must contain any Value..")
            End If

            If ComboBox7.Text <> "" Then
                FTPDetails = ComboBox7.Text
                If FTPDetails = "2.0" Then
                    FTPDetails = "2"
                ElseIf FTPDetails = "3.0" Then
                    FTPDetails = "3"
                End If
                arr1(14) = (FTPDetails)
            Else
                Throw New Exception("FTPDetails must can not be blank..")
            End If

            Dim Row As New List(Of String)
            For x As Integer = 0 To arr1.Count - 1
                Row.Add(arr1(x))
                If Row.Count = 15 Then
                    DataGridView1.Rows.Add(Row.ToArray)
                    Row.Clear()
                End If
            Next


            Label10.Visible = True
            ChapterID = ""
            'AuthorName = ""
            'AuthorEmail = ""
            'ElsevierPMMail = ""
            'ElsevierPMName = ""
            'AdditionalCCAUMail = ""
            'ThomsonPMMail = ""
            'ThomsonPMname = ""
            'DaysforAuthor = ""

        Catch ex As Exception
            MessageBox.Show("Exception...!" + vbNewLine + ex.Message)
            Label10.Visible = False
            Return
        End Try
    End Sub

    Private Sub TextBox10_Leave(sender As Object, e As EventArgs) Handles TextBox10.Leave
        Try
            Dim JobType As String = ""
            Dim forder As String
            ISBN = TextBox10.Text.Replace("-", "").Trim
            If Not (CheckStringInt(ISBN) = True And Len(ISBN) = 13) Then
                Throw New Exception("Please provide correct ISBN..!")
            End If
            ComboBox4.Items.Clear()

            Dim opath As String
            opath = ConfigurationSettings.AppSettings("OrderPath")
            If File.Exists(opath + "S&T\" + ISBN + "\Q300\Current_Order\S&T_" + ISBN + "_Q300.xml") Then
                JobType = "S&T"
                ComboBox1.Text = "S&T"
                forder = opath + "S&T\" + ISBN + "\Q300\Current_Order\S&T_" + ISBN + "_Q300.xml"
            ElseIf File.Exists(opath + "EHS\" + ISBN + "\Q300\Current_Order\EHS_" + ISBN + "_Q300.xml") Then
                JobType = "EHS"
                ComboBox1.Text = "EHS"
                forder = opath + "EHS\" + ISBN + "\Q300\Current_Order\EHS_" + ISBN + "_Q300.xml"
            Else
                Throw New Exception("ISBN: " + ISBN + "Not found on server.")
            End If


            If File.Exists(forder) Then
                Dim sr As New StreamReader(forder)

                Dim reader As String
                reader = sr.ReadToEnd
                sr.Close()
                Dim matchCollection As MatchCollection
                matchCollection = Regex.Matches(reader, "(<cno>)(.*?)(<\/cno>)")
                Dim match As Match
                For Each match In matchCollection
                    ComboBox4.Items.Add(match.Groups(2).ToString)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Function CheckStringInt(str1 As String) As Boolean
        Dim cr As Char()
        cr = str1.ToCharArray()

        For i = 0 To cr.Length - 1
            If Char.IsDigit(cr(i)) = False Then
                Return False
            End If
        Next
        Return True
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'If MessageBox.Show("Please close all excel files before process further. Do you want to continue?", "Excel", MessageBoxButtons.YesNo) = vbNo Then
            '    Return
            'End If

            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim i As Integer
            Dim j As Integer

            xlApp = New Excel.Application
            xlApp.Visible = True

            xlWorkBook = xlApp.Workbooks.Add
            xlWorkSheet = xlWorkBook.Sheets("sheet1")




            xlWorkSheet.Cells(1, 1) = "ChapterID"
            xlWorkSheet.Cells(1, 2) = "AuthorName"
            xlWorkSheet.Cells(1, 3) = "AuthorEmail"
            xlWorkSheet.Cells(1, 4) = "EditorName"
            xlWorkSheet.Cells(1, 5) = "EditorEmail"
            xlWorkSheet.Cells(1, 6) = "ElsevierPMMail"
            xlWorkSheet.Cells(1, 7) = "ElsevierPMName"
            xlWorkSheet.Cells(1, 8) = "AdditionalCCAUMail"
            xlWorkSheet.Cells(1, 9) = "AdditionalCCEDMail"
            xlWorkSheet.Cells(1, 10) = "ThomsonPMMail"
            xlWorkSheet.Cells(1, 11) = "ThomsonPMname"
            xlWorkSheet.Cells(1, 12) = "DaysforAuthor"
            xlWorkSheet.Cells(1, 13) = "DaysforEditors"
            xlWorkSheet.Cells(1, 14) = "Type"
            xlWorkSheet.Cells(1, 15) = "FTPDetails"



            For i = 0 To DataGridView1.RowCount - 2
                For j = 0 To DataGridView1.ColumnCount - 1
                    xlWorkSheet.Cells(i + 2, j + 1) = DataGridView1(j, i).Value.ToString()
                Next
            Next
            Dim CSVPath As String
            CSVPath = ConfigurationManager.AppSettings("CSVPath")

            If File.Exists(CSVPath + "\ED_" + ISBN + ".csv") Then
                File.Delete(CSVPath + "\ED_" + ISBN + ".csv")
            End If
            xlWorkSheet.SaveAs(CSVPath + "\ED_" + ISBN + ".csv")
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            '****************************************
            Dim str1 As String = Nothing
            Dim files As String()
            Dim fil As String = Nothing
            Dim fname As String = Nothing
            Dim authorname As String = Nothing
            Dim sw As StreamReader
            Dim Path1 As String = "\\172.16.0.44\Elsinpt\ElsBook\Orders\PPM\" + ISBN + "\TYPESET-ORDER\Current_Order"
            Dim Path2 As String = "\\172.16.0.44\Elsinpt\ElsBook\Orders\PPM\" + ISBN + "\MANAGED-CE-AND-TYPESET-ORDER\Current_Order"


            If Directory.Exists(Path1) Then
                files = Directory.GetFiles(Path1, "*.xml")
                For Each fil In files
                    fname = Path.GetFileNameWithoutExtension(fil)
                    If fname.Substring(0, 3) = "KUP" Then
                        sw = New StreamReader(fil)
                        str1 = sw.ReadToEnd
                        Try

                            authorname = str1.Substring(str1.IndexOf("<last-name>") + 11, (str1.IndexOf("</last-name>") - 11) - str1.IndexOf("<last-name>")).Trim
                        Catch ex As Exception
                            MsgBox(" Author name not found in order, It will created in local drive only.")
                        End Try

                        Exit For
                    End If
                Next
            ElseIf Directory.Exists(Path2) Then
                files = Directory.GetFiles(Path2, "*.xml")
                For Each fil In files
                    fname = Path.GetFileNameWithoutExtension(fil)
                    If fname.Substring(0, 3) = "KUP" Then
                        sw = New StreamReader(fil)
                        str1 = sw.ReadToEnd
                        Try
                            authorname = str1.Substring(str1.IndexOf("<last-name>") + 11, (str1.IndexOf("</last-name>") - 11) - str1.IndexOf("<last-name>")).Trim
                        Catch ex As Exception
                            MsgBox(" Author name not found in order, It will created in local drive only.")
                        End Try

                        Exit For
                    End If
                Next
            Else
                MsgBox("Author name not found in order, It will created in local drive only.")
            End If


            Dim serverpath As String = ""
            Dim filename As String = ""
            serverpath = "\\172.16.0.40\Elsevier English Books\Other Books\English\" & authorname & "_" & ISBN & "\Input\CSV\"
            'serverpath = "C:\Users\74810\Desktop\CSV\test\"
            filename = "ED_" + ISBN
            Dim fileseq As Integer = 0
            fileseq = Form1.checkfileInServer(serverpath, filename)

            If fileseq = -1 Then
                MsgBox("Folder not found in server " + serverpath + ". Please use local file.")
            Else
                File.Copy(CSVPath + "\ED_" + ISBN + ".csv", serverpath & filename & "_" & fileseq.ToString & ".csv", True)
            End If

            'If Directory.Exists("\\172.16.0.40\Elsevier English Books\Other Books\English\" & authorname & "_" & ISBN & "\Input\CSV") Then
            '    File.Copy(CSVPath + "\AU_" + ISBN + ".csv", "\\172.16.0.40\Elsevier English Books\Other Books\English\" & authorname & "_" & ISBN & "\Input\CSV" + "\ED_" + ISBN + "_1.csv", True)
            'Else
            '    MsgBox("Folder not found in server. Please use local file.")
            'End If

        Catch ex As Exception
            releaseObject(New Excel.Application)
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End
    End Sub

    Private Sub EDElsPMMAIL_Leave(sender As Object, e As EventArgs) Handles ComboBox6.Leave
        Dim hs As New Hashtable
        Try
            'hs.Add("v.tamil@elsevier.com", "Vignesh Tamilselvvan")
            'hs.Add("s.viswam@elsevier.com", "Sruthi Satheesh")
            'hs.Add("p.govindaradjane@elsevier.com", "Punithavathy Govindaradjane")
            'hs.Add("m.bernard@elsevier.com", "Maria Bernadette Vidhya Bernard J")
            'hs.Add("k.ramajogi@elsevier.com", "Kamesh Ramajogi")
            'hs.Add("j.honestthangiah@elsevier.com", "Joy Christel Neumarin Honest Thangiah")
            'hs.Add("p.chandramohan@elsevier.com", "Paul Prasad Chandramohan")
            'hs.Add("s.raviraj@elsevier.com", "Selvaraj Raviraj")
            'hs.Add("n.arumugam@elsevier.com", "Nirmala Arumugam")
            'hs.Add("s.srinivasan.1@elsevier.com", "Swapna Srinivasan")
            'hs.Add("v.rajan@elsevier.com", "Vijay Bharath")
            'hs.Add("omer.m@elsevier.com", "Omer Mukthar")
            'hs.Add("A.Sivaraj@elsevier.com", "K.S. Anitha")
            'hs.Add("d.ghosh@elsevier.com", "Debasish Ghosh")
            'hs.Add("s.pazhayattil@elsevier.com", "Sojan P. Pazhayattil")
            'hs.Add("P.Joseph@elsevier.com", "Poulouse Joseph")
            'hs.Add("k.govindaraju@elsevier.com", "Kiruthika Govindaraju")
            'hs.Add("s.viswanathan.1@elsevier.com", "Sreejith Viswanathan")
            'hs.Add("V.Purush@elsevier.com", "Vijayaraj Purushothaman")
            'hs.Add("b.varatharajan@elsevier.com", "Bharatwaj Varatharajan")
            'hs.Add("p.kalyanaraman@elsevier.com", "Prasanna Kalyanaraman")
            'hs.Add("MRW-TRNS@elsevier.com ", "PM TRNS")

            hs.Add("Vignesh Tamilselvvan", "v.tamil@elsevier.com")
            hs.Add("Sruthi Satheesh", "s.viswam@elsevier.com")
            hs.Add("Punithavathy Govindaradjane", "p.govindaradjane@elsevier.com")
            hs.Add("Maria Bernadette Vidhya Bernard J", "m.bernard@elsevier.com")
            hs.Add("Kamesh Ramajogi", "k.ramajogi@elsevier.com")
            hs.Add("Joy Christel Neumarin Honest Thangiah", "j.honestthangiah@elsevier.com")
            hs.Add("Paul Prasad Chandramohan", "p.chandramohan@elsevier.com")
            hs.Add("Selvaraj Raviraj", "s.raviraj@elsevier.com")
            hs.Add("Nirmala Arumugam", "n.arumugam@elsevier.com")
            hs.Add("Swapna Srinivasan", "s.srinivasan.1@elsevier.com")
            hs.Add("Vijay Bharath", "v.rajan@elsevier.com")
            hs.Add("Omer Mukthar", "omer.m@elsevier.com")
            hs.Add("K.S. Anitha", "A.Sivaraj@elsevier.com")
            hs.Add("Debasish Ghosh", "d.ghosh@elsevier.com")
            hs.Add("Sojan P. Pazhayattil", "s.pazhayattil@elsevier.com")
            hs.Add("Poulouse Joseph", "P.Joseph@elsevier.com")
            hs.Add("Kiruthika Govindaraju", "k.govindaraju@elsevier.com")
            hs.Add("Sreejith Viswanathan", "s.viswanathan.1@elsevier.com")
            hs.Add("Vijayaraj Purushothaman", "V.Purush@elsevier.com")
            hs.Add("Bharatwaj Varatharajan", "b.varatharajan@elsevier.com")
            hs.Add("Prasanna Kalyanaraman", "p.kalyanaraman@elsevier.com")
            hs.Add("PM TRNS", "MRW-TRNS@elsevier.com")
            hs.Add("Prem Kumar Kaliamoorthi", "p.kaliamoorthi@elsevier.com")
            hs.Add("Kumar Anbazhagan", "k.anbazhagan@elsevier.com")
            hs.Add("Armelle Amouroux", "A.Amouroux@elsevier.com")
            hs.Add("Dalila Benabderrahmane", "D.Benabderrahmane@elsevier.com")
            hs.Add("Gaëtan Breteche", "G.Breteche@elsevier.com")
            hs.Add("Mélanie Chacun", "M.Chacun@elsevier.com")
            hs.Add("Audrey Di Santo", "a.disanto@elsevier.com")
            hs.Add("Anna Faroux", "a.faroux@elsevier.com")
            hs.Add("Robain Halluin", "R.Halluin@elsevier.com")
            hs.Add("Nathalie Humblot", "N.Humblot@elsevier.fr")
            hs.Add("Stéphanie Lecocq", "s.lecocq@elsevier.com")
            hs.Add("Juliette Perron", "j.perron@elsevier.com")
            hs.Add("Christelle Rogues", "c.rogues@elsevier.com")
            hs.Add("Séverine Rolland", "S.Rolland@elsevier.com")
            hs.Add("Marie Ronsseray", "M.Ronsseray@elsevier.com")
            hs.Add("Vincent Etienne", "e.vincent@elsevier.com")
            hs.Add("Lucie Vanherpen ", "l.vanherpen@elsevier.com")
            hs.Add("Benjamin Scias", "B.Scias@elsevier.com")
            hs.Add("Anke Drescher", "A.Drescher@elsevier.com")
            hs.Add("Marion Kraus", "M.Kraus@elsevier.com")
            hs.Add("Sophie Eckart", "s.eckart@elsevier.com")
            hs.Add("Sibylle Hartl", "S.Hartl@elsevier.com")
            hs.Add("Annekathrin Sichling", "A.Sichling@elsevier.com")
            hs.Add("Martha Kurzl-Harrison", "m.kuerzl-harrison@elsevier.com")
            hs.Add("Alexander Gattnarzik", "A.Gattnarzik@elsevier.com")
            hs.Add("Nicole Kopp", "N.Kopp@elsevier.com")
            hs.Add("Julia Stangle", "J.Staengle@elsevier.com")
            hs.Add("Elisabeth Martz", "E.Maertz@elsevier.com")
            hs.Add("Ulrike Schmidt", "U.Schmidt@elsevier.com")
            hs.Add("Petra Laurer", "P.Laurer@elsevier.com")
            hs.Add("Cornelia von Saint Paul", "C.Saint-Paul@elsevier.com")
            hs.Add("Stefanie Schroder", "S.Schroeder@elsevier.com")
            hs.Add("Sabine Hennhofer", "S.Hennhoefer@elsevier.com")
            hs.Add("Andrea Beilmann", "A.Beilmann@elsevier.com")
            hs.Add("Karin Kuhnel", "k.kuehnel@elsevier.com")
            hs.Add("Martina Gartner", "M.Gaertner@elsevier.com")
            hs.Add("Ines Mergenhagen", "i.mergenhagen@elsevier.com")
            hs.Add("Antje Gropke", "a.groepke@elsevier.com")
            hs.Add("Dagmar Wiederhold", "D.Wiederhold@elsevier.com")
            hs.Add("Christine Kosel", "C.Kosel@elsevier.com")
            hs.Add("Angelika Brandtner", "a.brandtner@elsevier.com")
            hs.Add("Birgit Pietzsch", "B.Pietzsch@elsevier.com")
            hs.Add("Petra Taint", "p.taint@elsevier.com")
            hs.Add("Ulrike Kriegel", "U.Kriegel@elsevier.com")
            hs.Add("Petra Sneed", "p.sneed@elsevier.com")
            hs.Add("Ute Landwehr Heldt", "mail@landwehrheldt.de")
            hs.Add("Brigitte Schlagintweit", "b@schlagintweit-beratung.de")
            hs.Add("Dietmar Raduenz", "hvs@raduenzd.com")
            hs.Add("Hildegard Graf", "mail@hildegardgraf.de")
            hs.Add("Nikola Schmidt", "kontakt@seitenspiel.com")
            hs.Add("Kadja Gericke", "proprint.herrenberg@t-online.de")
            hs.Add("Michael Kraft", "mik@mimo-booxx.de")
            hs.Add("Janin Schroth", "schroth@buchkompanie.de")
            hs.Add("Renate Hausdorf", "r.hausdorf@t-online.de")
            hs.Add("Steffen Zimmermann", "s.zimmermann@publishing-support.de")
            hs.Add("Felicitas Hübner", "FelicitasHuebner@web.de")
            hs.Add("Waltraud Hofbauer", "wh@hofbauer-typo.de")
            hs.Add("Michaela Mohr", "kontakt@mimo-booxx.de")
            hs.Add("Sibylle Tönjes", "SToenjes@t-online.de")
            hs.Add("Arthur Lenner", "arthur.lenner@derbuchmacher.de")
            hs.Add("Anne Wiehage Lektorat", "a.wiehage@web.de")


            ComboBox5.Text = hs(ComboBox6.Text)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub EDTDPMMAIL_Leave(sender As Object, e As EventArgs) Handles ComboBox3.Leave
        Try
            Dim hs As New Hashtable
            'hs.Add("sudhi.signh@thomsondigital.com", "Sudhi Singh")
            'hs.Add("shreya.tiwari@thomsondigital.com", "Shreya Tiwari")
            'hs.Add("abhishek.sarkari@thomsondigital.com", "Abhishek Sarkari")
            'hs.Add("manish.pandey@thomsondigital.com", "Manish Pandey")
            'hs.Add("pooja.malhotra@thomsondigital.com", "Pooja Malhotra")
            hs.Add("Sudhi Singh", "sudhi.signh@thomsondigital.com")
            hs.Add("Shreya Tiwari", "shreya.tiwari@thomsondigital.com")
            hs.Add("Abhishek Sarkari", "abhishek.sarkari@thomsondigital.com")
            hs.Add("Manish Pandey", "manish.pandey@thomsondigital.com")
            hs.Add("Pooja Malhotra", "pooja.malhotra@thomsondigital.com")
            hs.Add("Saumya Sharma", "saumya.sharma@thomsondigital.com")
            hs.Add("Akshit Verma", "pmels.german@thomsondigital.com")
            hs.Add("Vrishni Dabeedin-Heerooa", "vrishni.d@thomsondigital.com")
            ComboBox2.Text = hs(ComboBox3.Text)
        Catch ex As Exception

        End Try
    End Sub
End Class
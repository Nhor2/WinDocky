Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.VisualBasic.Devices
Imports Microsoft.Win32

Public Class WinDocky

    'WinDocky

    '21-03-2024
    'Crea un dock sullo sfondo dello schermo in Stile Apple. E' necessaria un'icona o immagine.
    'This code and program are licensed to GNU General Public License.


    Dim mouse_move As System.Drawing.Point
    Dim toggle_preferences As Boolean = False

    Dim links(15) As String
    Dim soft_index As Integer = 0

    Dim windocky_programma As String = ""
    Dim windocky_icona As String = ""
    'Colo over icone WinDocky
    Dim color_over As Color = Color.White
    Dim color_leave As Color = Color.FromArgb(64, 64, 64)



    'Load only on Primary Screen
    Private Sub WinDocky_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screen_width = Screen.PrimaryScreen.WorkingArea.Width
        Dim screen_height = Screen.PrimaryScreen.WorkingArea.Height
        Me.Location = New Point((Me.Width + screen_width) / 2 - Me.Width, (screen_height - Me.Height))

        'inizio
        Dim start_picturebox As Integer = 0
        For i = 0 To 15
            links(i) = ""
        Next

        LetturaDB()
    End Sub

    Public Sub LetturaDB()
        'Lettura DB utente se esiste altrimenti installa WinDocky nel registro Windows.

        Try
            Dim WinDockyKey As RegistryKey = Registry.LocalMachine.OpenSubKey("HKEY_CURRENT_USER\WinDocky")
            Dim install = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\WinDocky", "WinDockyInstall", Nothing)

            If install <> "OK" Then
                MsgBox("Non ci sono Dock creati.")
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "WinDockyInstall", "OK")
                For i = 1 To 15
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", i, "__windocky__") '"MyTestKeyValue", "This is a test value."
                Next
            Else
                For i = 1 To 15
                    'legge la chiave i e il valore della stessa
                    Dim value As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\WinDocky", i.ToString, Nothing)

                    'separa il link dal path icona
                    If value <> "__windocky__" Then

                        Try
                            Dim soft() As String = Strings.Split(value, ",")

                            'setta il link del programma
                            links(i) = soft(0)

                            'setta l'immagine icona
                            Select Case i
                                Case 1
                                    PictureBox1.Image = Image.FromFile(soft(1))
                                Case 2
                                    PictureBox2.Image = Image.FromFile(soft(1))
                                Case 3
                                    PictureBox3.Image = Image.FromFile(soft(1))
                                Case 4
                                    PictureBox4.Image = Image.FromFile(soft(1))
                                Case 5
                                    PictureBox5.Image = Image.FromFile(soft(1))
                                Case 6
                                    PictureBox6.Image = Image.FromFile(soft(1))
                                Case 7
                                    PictureBox7.Image = Image.FromFile(soft(1))
                                Case 8
                                    PictureBox8.Image = Image.FromFile(soft(1))
                                Case 9
                                    PictureBox9.Image = Image.FromFile(soft(1))
                                Case 10
                                    PictureBox10.Image = Image.FromFile(soft(1))
                                Case 11
                                    PictureBox11.Image = Image.FromFile(soft(1))
                                Case 12
                                    PictureBox12.Image = Image.FromFile(soft(1))
                                Case 13
                                    PictureBox13.Image = Image.FromFile(soft(1))
                                Case 14
                                    PictureBox14.Image = Image.FromFile(soft(1))
                                Case 15
                                    PictureBox15.Image = Image.FromFile(soft(1))
                            End Select
                            'esce se ci sono più di 15 programmi
                            If i = 15 Then Exit For
                        Catch ex As Exception

                        End Try

                    End If
                Next

            End If

        Catch ex As Exception
            MsgBox("Nessun Dock trovato. " & ex.Message)
        End Try
    End Sub

    'Funzioni
    Public Enum FOLDER_NAMES
        CSIDL_DESKTOP = &H0 '// The Desktop - virtual folder 
        CSIDL_PROGRAMS = 2 '// Program Files 
        CSIDL_CONTROLS = 3 '// Control Panel - virtual folder 
        CSIDL_PRINTERS = 4 '// Printers - virtual folder 
        CSIDL_DOCUMENTS = 5 '// My Documents 
        CSIDL_FAVORITES = 6 '// Favourites 
        CSIDL_STARTUP = 7 '// Startup Folder 
        CSIDL_RECENT = 8 '// Recent Documents 
        CSIDL_SENDTO = 9 '// Send To Folder 
        CSIDL_BITBUCKET = 10 '// Recycle Bin - virtual folder 
        CSIDL_STARTMENU = 11 '// Start Menu 
        CSIDL_DESKTOPFOLDER = 16 '// Desktop folder 
        CSIDL_DRIVES = 17 '// My Computer - virtual folder 
        CSIDL_NETWORK = 18 '// Network Neighbourhood - virtual folder 
        CSIDL_NETHOOD = 19 '// NetHood Folder 
        CSIDL_FONTS = 20 '// Fonts folder 
        CSIDL_SHELLNEW = 21 '// ShellNew folder
    End Enum

    Const CSIDL_DESKTOP = &H0        ' &lt;desktop&gt;
    Const CSIDL_INTERNET = &H1       ' Internet Explorer (icon on desktop)
    Const CSIDL_PROGRAMS = &H2       ' Start Menu\Programs
    Const CSIDL_CONTROLS = &H3       ' My Computer\Control Panel
    Const CSIDL_PRINTERS = &H4       ' My Computer\Printers
    Const CSIDL_PERSONAL = &H5       ' My Documents
    Const CSIDL_FAVORITES = &H6      ' &lt;user name&gt;\Favorites
    Const CSIDL_STARTUP = &H7        ' Start Menu\Programs\Startup
    Const CSIDL_RECENT = &H8     ' &lt;user name&gt;\Recent
    Const CSIDL_SENDTO = &H9     ' &lt;user name&gt;\SendTo
    Const CSIDL_BITBUCKET = &HA      ' &lt;desktop&gt;\Recycle Bin
    Const CSIDL_STARTMENU = &HB      ' &lt;user name&gt;\Start Menu
    Const CSIDL_MYDOCUMENTS = CSIDL_PERSONAL '  Personal was just a silly name for My Documents
    Const CSIDL_MYMUSIC = &HD        ' "My Music" folder
    Const CSIDL_MYVIDEO = &HE        ' "My Videos" folder
    Const CSIDL_DESKTOPDIRECTORY = &H10       ' &lt;user name&gt;\Desktop
    Const CSIDL_DRIVES = &H11     ' My Computer
    Const CSIDL_NETWORK = &H12        ' Network Neighborhood (My Network Places)
    Const CSIDL_NETHOOD = &H13        ' &lt;user name&gt;\nethood
    Const CSIDL_FONTS = &H14      ' windows\fonts
    Const CSIDL_TEMPLATES = &H15
    Const CSIDL_COMMON_STARTMENU = &H16       ' All Users\Start Menu
    Const CSIDL_COMMON_PROGRAMS = &H17        ' All Users\Start Menu\Programs
    Const CSIDL_COMMON_STARTUP = &H18     ' All Users\Startup
    Const CSIDL_COMMON_DESKTOPDIRECTORY = &H19        ' All Users\Desktop
    Const CSIDL_APPDATA = &H1A        ' &lt;user name&gt;\Application Data
    Const CSIDL_PRINTHOOD = &H1B      ' &lt;user name&gt;\PrintHood
    Const CSIDL_LOCAL_APPDATA = &H1C      ' &lt;user name&gt;\Local Settings\Applicaiton Data (non roaming)
    Const CSIDL_ALTSTARTUP = &H1D     ' non localized startup
    Const CSIDL_COMMON_ALTSTARTUP = &H1E      ' non localized common startup
    Const CSIDL_COMMON_FAVORITES = &H1F
    Const CSIDL_INTERNET_CACHE = &H20
    Const CSIDL_COOKIES = &H21
    Const CSIDL_HISTORY = &H22
    Const CSIDL_COMMON_APPDATA = &H23     ' All Users\Application Data
    Const CSIDL_WINDOWS = &H24        ' GetWindowsDirectory()
    Const CSIDL_SYSTEM = &H25     ' GetSystemDirectory()
    Const CSIDL_PROGRAM_FILES = &H26      ' C:\Program Files
    Const CSIDL_MYPICTURES = &H27     ' C:\Program Files\My Pictures
    Const CSIDL_PROFILE = &H28        ' USERPROFILE
    Const CSIDL_SYSTEMX86 = &H29      ' x86 system directory on RISC
    Const CSIDL_PROGRAM_FILESX86 = &H2A       ' x86 C:\Program Files on RISC
    Const CSIDL_PROGRAM_FILES_COMMON = &H2B       ' C:\Program Files\Common
    Const CSIDL_PROGRAM_FILES_COMMONX86 = &H2C        ' x86 Program Files\Common on RISC
    Const CSIDL_COMMON_TEMPLATES = &H2D       ' All Users\Templates
    Const CSIDL_COMMON_DOCUMENTS = &H2E       ' All Users\Documents
    Const CSIDL_COMMON_ADMINTOOLS = &H2F      ' All Users\Start Menu\Programs\Administrative Tools
    Const CSIDL_ADMINTOOLS = &H30     ' &lt;user name&gt;\Start Menu\Programs\Administrative Tools
    Const CSIDL_CONNECTIONS = &H31        ' Network and Dial-up Connections
    Const CSIDL_COMMON_MUSIC = &H35       ' All Users\My Music
    Const CSIDL_COMMON_PICTURES = &H36        ' All Users\My Pictures
    Const CSIDL_COMMON_VIDEO = &H37       ' All Users\My Video
    Const CSIDL_RESOURCES = &H38      ' Resource Direcotry
    Const CSIDL_RESOURCES_LOCALIZED = &H39        ' Localized Resource Direcotry
    Const CSIDL_COMMON_OEM_LINKS = &H3A       ' Links to All Users OEM specific apps
    Const CSIDL_CDBURN_AREA = &H3B        ' USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
    Const CSIDL_COMPUTERSNEARME = &H3D        ' Computers Near Me (computered from Workgroup membership)
    Const CSIDL_FLAG_CREATE = &H8000        ' combine with CSIDL_ value to force folder creation in SHGetFolderPath()
    Const CSIDL_FLAG_DONT_VERIFY = &H4000       ' combine with CSIDL_ value to return an unverified folder path
    Const CSIDL_FLAG_DONT_UNEXPAND = &H2000     ' combine with CSIDL_ value to avoid unexpanding environment variables
    Const CSIDL_FLAG_NO_ALIAS = &H1000      ' combine with CSIDL_ value to insure non-alias versions of the pidl
    Const CSIDL_FLAG_PER_USER_INIT = &H800     ' combine with CSIDL_ value to indicate per-user init (eg. upgrade)

    Declare Function SHGetSpecialFolderLocation Lib "Shell32.dll" (ByVal hwndOwner As Long, ByVal nFolder As Long, pidl As ITEMIDLIST) As Long
    Declare Function SHGetPathFromIDList Lib "Shell32.dll" Alias "SHGetPathFromIDListA" (ByVal pidl As Long, ByVal pszPath As String) As Long

    <DllImport("shell32.dll")>
    Private Shared Function SHGetSpecialFolderPath(hwndOwner As IntPtr, <Out> lpszPath As StringBuilder, nFolder As Integer, fCreate As Boolean) As Boolean
    End Function


    Public Structure EMID
        Dim cb As Long
        Dim abID As Byte
    End Structure

    Public Structure ITEMIDLIST
        Dim mkid As EMID
    End Structure
    Public Const MAX_PATH As Integer = 260  'Max path riferito al percorso del file del programma

    Public Function GetSpecialFolder(CSIDL As FOLDER_NAMES) As String
        Dim sPath As String
        Dim IDL As ITEMIDLIST
        ' 
        ' Retrieve info about system folders such as the "Recent Documents" folder. 
        ' Info is stored in the IDL structure. 
        ' 
        GetSpecialFolder = ""
        If SHGetSpecialFolderLocation(Me.Handle, CSIDL, IDL) = 0 Then
            ' 
            ' Get the path from the ID list, and return the folder. 
            ' 
            sPath = Space$(MAX_PATH)
            If SHGetPathFromIDList(IDL.mkid.cb, sPath) Then
                GetSpecialFolder = Strings.Left(sPath, InStr(sPath, vbNullChar) - 1) & "\"
            End If
        End If
    End Function

    Private Function GetStartMenu(start_folder As String) As String
        Dim start_menu As String = ""
        'Dim files() As String = IO.Directory.GetFiles(start_folder)
        Dim cartelle() As String = IO.Directory.GetDirectories(start_folder)

        For Each cartella As String In cartelle
            If System.IO.Directory.Exists(cartella) Then
                Dim files() As String = IO.Directory.GetFiles(cartella)
                For Each file As String In files
                    ' Do work, example
                    If FileLen(file) <> 0 Then
                        If System.IO.File.Exists(file) Then
                            If System.IO.Path.GetExtension(file) = ".lnk" Then
                                start_menu = start_menu & file & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If
        Next
        Dim files2() As String = IO.Directory.GetFiles(start_folder)
        For Each file As String In files2
            ' Do work, example
            If FileLen(file) <> 0 Then
                If System.IO.File.Exists(file) Then
                    If System.IO.Path.GetExtension(file) = ".lnk" Then
                        start_menu = start_menu & file & vbCrLf
                    End If
                End If
            End If
        Next

        Return start_menu
    End Function

    Public Function IconFromFilePath(filePath As String) As Icon
        Dim result As Icon = Nothing
        Try
            result = Icon.ExtractAssociatedIcon(filePath)
        Catch ''# swallow and return nothing. You could supply a default Icon here as well
            result = Icon.ExtractAssociatedIcon(Application.StartupPath & "\WinDocky.exe")
        End Try
        Return result
    End Function

    ' ----------------------






















    Private Sub window_header_Paint(sender As Object, e As PaintEventArgs) Handles window_header.Paint
        'Bordo panel
        'ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle, Color.Silver, ButtonBorderStyle.Solid)
    End Sub

    Private Sub window_header_MouseMove(sender As Object, e As MouseEventArgs) Handles window_header.MouseMove
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            Dim mposition As Point
            mposition = Control.MousePosition
            mposition.Offset(mouse_move.X, mouse_move.Y)
            Me.Location = mposition
        End If

    End Sub

    Private Sub window_header_MouseDown(sender As Object, e As MouseEventArgs) Handles window_header.MouseDown
        mouse_move = New Point(-e.X, -e.Y)
    End Sub

    Private Sub AppRestart()
        Application.Restart()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Preferenze
        Application.Exit()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If links(1) <> "" Then
            Process.Start(links(1))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\1.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\1.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try
                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "1", windocky_programma & "," & windocky_icona)
                        PictureBox1.Image = Image.FromFile(windocky_icona)
                        PictureBox1.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If links(2) <> "" Then
            Process.Start(links(2))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\2.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\2.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "2", windocky_programma & "," & windocky_icona)
                        PictureBox2.Image = Image.FromFile(windocky_icona)
                        PictureBox2.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If links(3) <> "" Then
            Process.Start(links(3))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\3.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\3.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try
                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "3", windocky_programma & "," & windocky_icona)
                        PictureBox3.Image = Image.FromFile(windocky_icona)
                        PictureBox3.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If links(4) <> "" Then
            Process.Start(links(4))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\4.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\4.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "4", windocky_programma & "," & windocky_icona)
                        PictureBox4.Image = Image.FromFile(windocky_icona)
                        PictureBox4.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        If links(5) <> "" Then
            Process.Start(links(5))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\5.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\5.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "5", windocky_programma & "," & windocky_icona)
                        PictureBox5.Image = Image.FromFile(windocky_icona)
                        PictureBox5.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If links(6) <> "" Then
            Process.Start(links(6))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\6.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\6.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "6", windocky_programma & "," & windocky_icona)
                        PictureBox6.Image = Image.FromFile(windocky_icona)
                        PictureBox6.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        If links(7) <> "" Then
            Process.Start(links(7))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\7.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\7.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "7", windocky_programma & "," & windocky_icona)
                        PictureBox7.Image = Image.FromFile(windocky_icona)
                        PictureBox7.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If links(8) <> "" Then
            Process.Start(links(8))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\8.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\8.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "8", windocky_programma & "," & windocky_icona)
                        PictureBox8.Image = Image.FromFile(windocky_icona)
                        PictureBox8.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        If links(9) <> "" Then
            Process.Start(links(9))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\9.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\9.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "9", windocky_programma & "," & windocky_icona)
                        PictureBox9.Image = Image.FromFile(windocky_icona)
                        PictureBox9.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        If links(10) <> "" Then
            Process.Start(links(10))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\10.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\10.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "10", windocky_programma & "," & windocky_icona)
                        PictureBox10.Image = Image.FromFile(windocky_icona)
                        PictureBox10.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        If links(11) <> "" Then
            Process.Start(links(11))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\11.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\11.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "11", windocky_programma & "," & windocky_icona)
                        PictureBox11.Image = Image.FromFile(windocky_icona)
                        PictureBox11.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        If links(12) <> "" Then
            Process.Start(links(12))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\12.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\12.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "12", windocky_programma & "," & windocky_icona)
                        PictureBox12.Image = Image.FromFile(windocky_icona)
                        PictureBox12.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        If links(13) <> "" Then
            Process.Start(links(13))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\13.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\13.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "13", windocky_programma & "," & windocky_icona)
                        PictureBox13.Image = Image.FromFile(windocky_icona)
                        PictureBox13.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        If links(14) <> "" Then
            Process.Start(links(14))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\14.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\14.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "14", windocky_programma & "," & windocky_icona)
                        PictureBox14.Image = Image.FromFile(windocky_icona)
                        PictureBox14.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click
        If links(15) <> "" Then
            Process.Start(links(15))
        Else
            OpenFileDialog1.Title = "Scegli un programma..."
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Tutti i files eseguibili|*.exe;*.bat;*.ps1"

            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                If OpenFileDialog1.FileName <> "" Then
                    windocky_programma = OpenFileDialog1.FileName

                    OpenFileDialog1.Title = "Scegli un icona (46x46)..."
                    OpenFileDialog1.FileName = ""
                    OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

                    If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                        If OpenFileDialog1.FileName <> "" Then
                            windocky_icona = OpenFileDialog1.FileName
                        End If
                    Else
                        If windocky_programma <> " " Or windocky_programma <> Nothing Then
                            Try
                                Dim icon As Icon = IconFromFilePath(windocky_programma)
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\15.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\15.ico"
                            Catch ex As Exception
                                'Se non hai un'icona viene usata windocky icon.
                                If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                Else
                                    Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                    Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                    icon.Save(sr)
                                    sr.Close()
                                    windocky_icona = Application.StartupPath & "\windocky.ico"
                                End If
                            End Try

                        Else
                            'Se non hai un'icona viene usata windocky icon.
                            If System.IO.File.Exists(Application.StartupPath & "\windocky.ico") Then
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            Else
                                Dim icon As Icon = IconFromFilePath(Application.StartupPath & "\WinDocky.exe")
                                Dim sr As Stream = New FileStream(Application.StartupPath & "\windocky.ico", FileMode.Create)
                                icon.Save(sr)
                                sr.Close()
                                windocky_icona = Application.StartupPath & "\windocky.ico"
                            End If
                        End If

                    End If

                    'Setta l'icona e il programma nel registro
                    Try

                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", "15", windocky_programma & "," & windocky_icona)
                        PictureBox15.Image = Image.FromFile(windocky_icona)
                        PictureBox15.Refresh()
                        AppRestart()
                    Catch ex As Exception
                        MsgBox("Errore : " & ex.Message)
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        PictureBox1.BackColor = color_over
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.BackColor = color_leave
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.BackColor = color_over
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.BackColor = color_leave
    End Sub

    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        PictureBox3.BackColor = color_over
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.BackColor = color_leave
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        PictureBox4.BackColor = color_over
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.BackColor = color_leave
    End Sub

    Private Sub PictureBox6_MouseHover(sender As Object, e As EventArgs) Handles PictureBox6.MouseHover
        PictureBox6.BackColor = color_over
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.BackColor = color_leave
    End Sub

    Private Sub PictureBox5_MouseHover(sender As Object, e As EventArgs) Handles PictureBox5.MouseHover
        PictureBox5.BackColor = color_over
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.BackColor = color_leave
    End Sub

    Private Sub PictureBox7_MouseHover(sender As Object, e As EventArgs) Handles PictureBox7.MouseHover
        PictureBox7.BackColor = color_over
    End Sub

    Private Sub PictureBox7_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox7.MouseLeave
        PictureBox7.BackColor = color_leave
    End Sub

    Private Sub PictureBox8_MouseHover(sender As Object, e As EventArgs) Handles PictureBox8.MouseHover
        PictureBox8.BackColor = color_over
    End Sub

    Private Sub PictureBox8_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox8.MouseLeave
        PictureBox8.BackColor = color_leave
    End Sub

    Private Sub PictureBox9_MouseHover(sender As Object, e As EventArgs) Handles PictureBox9.MouseHover
        PictureBox9.BackColor = color_over
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.BackColor = color_leave
    End Sub

    Private Sub PictureBox10_MouseHover(sender As Object, e As EventArgs) Handles PictureBox10.MouseHover
        PictureBox10.BackColor = color_over
    End Sub

    Private Sub PictureBox10_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox10.MouseLeave
        PictureBox10.BackColor = color_leave
    End Sub

    Private Sub PictureBox11_MouseHover(sender As Object, e As EventArgs) Handles PictureBox11.MouseHover
        PictureBox11.BackColor = color_over
    End Sub

    Private Sub PictureBox11_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox11.MouseLeave
        PictureBox11.BackColor = color_leave
    End Sub

    Private Sub PictureBox12_MouseHover(sender As Object, e As EventArgs) Handles PictureBox12.MouseHover
        PictureBox12.BackColor = color_over
    End Sub

    Private Sub PictureBox12_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox12.MouseLeave
        PictureBox12.BackColor = color_leave
    End Sub

    Private Sub PictureBox13_MouseHover(sender As Object, e As EventArgs) Handles PictureBox13.MouseHover
        PictureBox13.BackColor = color_over
    End Sub

    Private Sub PictureBox13_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox13.MouseLeave
        PictureBox13.BackColor = color_leave
    End Sub

    Private Sub PictureBox14_MouseHover(sender As Object, e As EventArgs) Handles PictureBox14.MouseHover
        PictureBox14.BackColor = color_over
    End Sub

    Private Sub PictureBox14_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox14.MouseLeave
        PictureBox14.BackColor = color_leave
    End Sub

    Private Sub PictureBox15_MouseHover(sender As Object, e As EventArgs) Handles PictureBox15.MouseHover
        PictureBox15.BackColor = color_over
    End Sub

    Private Sub PictureBox15_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox15.MouseLeave
        PictureBox15.BackColor = color_leave
    End Sub


    'Funzioni
    'https://stackoverflow.com/questions/5506811/right-click-menu-options
    Private Sub AggiornaDock(ByVal sender As Object, ByVal e As EventArgs)
        Dim item = CType(sender, ToolStripMenuItem)
        Dim selection = CInt(item.Tag)

        Select Case selection
            Case 1
                ChooseIcon(PictureBox1, 1)
            Case 2
                RemoveProg(1)
            Case 3
                ChooseIcon(PictureBox2, 2)
            Case 4
                RemoveProg(2)
            Case 5
                ChooseIcon(PictureBox3, 3)
            Case 6
                RemoveProg(3)
            Case 7
                ChooseIcon(PictureBox4, 4)
            Case 8
                RemoveProg(4)
            Case 9
                ChooseIcon(PictureBox5, 5)
            Case 10
                RemoveProg(5)
            Case 11
                ChooseIcon(PictureBox6, 6)
            Case 12
                RemoveProg(6)
            Case 13
                ChooseIcon(PictureBox7, 7)
            Case 14
                RemoveProg(7)
            Case 15
                ChooseIcon(PictureBox8, 8)
            Case 16
                RemoveProg(8)
            Case 17
                ChooseIcon(PictureBox9, 9)
            Case 18
                RemoveProg(9)
            Case 19
                ChooseIcon(PictureBox10, 10)
            Case 20
                RemoveProg(10)
            Case 21
                ChooseIcon(PictureBox11, 11)
            Case 22
                RemoveProg(11)
            Case 23
                ChooseIcon(PictureBox12, 12)
            Case 24
                RemoveProg(12)
            Case 25
                ChooseIcon(PictureBox13, 13)
            Case 26
                RemoveProg(13)
            Case 27
                ChooseIcon(PictureBox14, 14)
            Case 28
                RemoveProg(14)
            Case 29
                ChooseIcon(PictureBox15, 15)
            Case 30
                RemoveProg(15)
            Case 31
                AggiornaPath(1)
            Case 32
                AggiornaPath(2)
            Case 33
                AggiornaPath(3)
            Case 34
                AggiornaPath(4)
            Case 35
                AggiornaPath(5)
            Case 36
                AggiornaPath(6)
            Case 37
                AggiornaPath(7)
            Case 38
                AggiornaPath(8)
            Case 39
                AggiornaPath(9)
            Case 40
                AggiornaPath(10)
            Case 41
                AggiornaPath(11)
            Case 42
                AggiornaPath(12)
            Case 43
                AggiornaPath(13)
            Case 44
                AggiornaPath(14)
            Case 45
                AggiornaPath(15)
        End Select
    End Sub

    Private Sub AggiornaPath(docky As Integer)
        OpenFileDialog1.Title = "Scegli un programma..."
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Tutti i files Eseguibili|*.exe;*.bat"

        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            If OpenFileDialog1.FileName <> "" Then
                Dim value As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\WinDocky", docky.ToString, Nothing)
                Dim windocky() As String = Strings.Split(value, ",")
                Dim windocky_programma As String = OpenFileDialog1.FileName 'windocky(0)
                Dim windocky_icona As String = windocky(1)

                Try
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", docky.ToString, windocky_programma & "," & windocky_icona)
                    Dim pic = Image.FromFile(windocky_icona)
                    AppRestart()
                Catch ex As Exception
                    MsgBox("Errore : " & ex.Message)
                End Try

            End If
        End If
    End Sub

    Private Sub ChooseIcon(pic As PictureBox, docky As Integer)
        OpenFileDialog1.Title = "Scegli un icona..."
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Tutti i files immagine|*.ico;*.png;*.bmp;*.gif;*.jpg"

        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            If OpenFileDialog1.FileName <> "" Then
                windocky_icona = OpenFileDialog1.FileName

                Dim value As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\WinDocky", docky.ToString, Nothing)
                Dim windocky_programma As String = Strings.Mid(value, 1, Strings.InStrRev(value, ",") - 1)

                Try
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\WinDocky", docky.ToString, windocky_programma & "," & windocky_icona)
                    pic.Image = Image.FromFile(windocky_icona)
                    pic.Refresh()
                    AppRestart()
                Catch ex As Exception
                    MsgBox("Errore : " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub RemoveProg(docky As Integer)
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey("WinDocky", True).DeleteValue(docky.ToString)
            MsgBox("Dock Cancellato.")
            AppRestart()
        Catch ex As Exception
            MsgBox("Errore: " & ex.Message)
        End Try
    End Sub





    'Fine Funzioni
    '-----Old Routine---------
    'If e.Button = MouseButtons.Right Then
    'Dim risposta = MsgBox("Vuoi eliminare il Dock 1 ?", MsgBoxStyle.YesNo)
    '
    'If risposta = DialogResult.Yes Then
    'Try
    'My.Computer.Registry.CurrentUser.OpenSubKey("WinDocky", True).DeleteValue("1")
    'MsgBox("Dock Cancellato.")
    'AppRestart()
    'Catch ex As Exception
    'MsgBox("Errore: " & ex.Message)
    'End Try
    'End If
    'End If
    '-----------------

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 1
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi")
            item2.Tag = 2
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 31
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox1, e.Location)
        End If
    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 3
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 4
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 32
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox2, e.Location)
        End If
    End Sub

    Private Sub PictureBox3_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 5
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 6
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 33
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox3, e.Location)
        End If
    End Sub

    Private Sub PictureBox4_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox4.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 7
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 8
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 34
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox4, e.Location)
        End If
    End Sub

    Private Sub PictureBox5_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 9
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 10
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 35
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox5, e.Location)
        End If
    End Sub

    Private Sub PictureBox6_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox6.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 11
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 12
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 36
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox6, e.Location)
        End If
    End Sub

    Private Sub PictureBox7_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 13
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 14
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 37
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox7, e.Location)
        End If
    End Sub

    Private Sub PictureBox8_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox8.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 15
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 16
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 38
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox8, e.Location)
        End If
    End Sub

    Private Sub PictureBox9_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox9.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 17
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 18
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 39
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox9, e.Location)
        End If
    End Sub

    Private Sub PictureBox10_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox10.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 19
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 20
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 40
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox10, e.Location)
        End If
    End Sub

    Private Sub PictureBox11_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox11.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 21
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 22
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 41
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox11, e.Location)
        End If
    End Sub

    Private Sub PictureBox12_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox12.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 23
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 24
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 42
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox12, e.Location)
        End If
    End Sub

    Private Sub PictureBox13_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox13.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 25
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 26
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 43
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox13, e.Location)
        End If
    End Sub

    Private Sub PictureBox14_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox14.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 27
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 28
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 44
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox14, e.Location)
        End If
    End Sub

    Private Sub PictureBox15_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox15.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Aggiorna Icona")
            item1.Tag = 29
            AddHandler item1.Click, AddressOf AggiornaDock
            Dim item2 = cms.Items.Add("Rimuovi Dock")
            item2.Tag = 30
            AddHandler item2.Click, AddressOf AggiornaDock
            Dim item3 = cms.Items.Add("Aggiorna Pathfile")
            item3.Tag = 45
            AddHandler item3.Click, AddressOf AggiornaDock
            '-- etc
            '..
            cms.Show(PictureBox15, e.Location)
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        'Sfondo WinDocky
        If Me.Panel1.BackColor.R = 64 Then
            Me.Panel1.BackColor = Color.White
            Me.window_header.BackColor = Color.White
            Me.BackColor = Color.White
            Label1.ForeColor = Color.Black
            color_leave = Color.White
            color_over = Color.FromArgb(64, 64, 64)
        ElseIf Me.Panel1.BackColor = Color.White Then
            Me.Panel1.BackColor = Color.Black
            Me.window_header.BackColor = Color.Black
            Me.BackColor = Color.Black
            Label1.ForeColor = Color.Turquoise
            color_over = Color.White
            color_leave = Color.Black
        ElseIf Me.BackColor = Color.Black Then
            Me.Panel1.BackColor = Color.FromArgb(64, 64, 64)
            Me.window_header.BackColor = Color.FromArgb(64, 64, 64)
            Me.BackColor = Color.FromArgb(64, 64, 64)
            Label1.ForeColor = Color.Turquoise
            color_over = Color.White
            color_leave = Color.FromArgb(64, 64, 64)
        End If
    End Sub
End Class
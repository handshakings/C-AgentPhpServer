strArgs = Session.Property( "CustomActionData" )
'MsgBox(strArgs)

Dim filesys, newfolder, newfolderpath
newfolderpath = "c:\blueattackconfig"
set filesys=CreateObject("Scripting.FileSystemObject")
If Not filesys.FolderExists(newfolderpath) Then
Set newfolder = filesys.CreateFolder(newfolderpath)
'Response.Write("A new folder has been created at: " newfolderpath)
End If


Dim fso : Set fso = CreateObject("Scripting.FileSystemObject")
Dim file : Set file = fso.OpenTextFile("c:\blueattackconfig\config.txt", 2, True)
file.Write strArgs


file.Close
'MsgBox("end")

 
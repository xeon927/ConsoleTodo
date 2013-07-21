Imports System.IO
Module ConsoleToDo
    Dim todoPath As String = Path.Combine(Directory.GetCurrentDirectory(), "todoBase.db")
    Dim todo() As String
    Dim todoList As New List(Of String())
    Dim viewAll As Boolean = False

    Sub Main()
        'Just in case someone has a strangely-coloured terminal, that should fixed
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        LoadToDo()
        DisplayToDo()
    End Sub
    Sub LoadToDo()
        While todoList.Count > 0
            For i As Integer = 1 To todoList.Count
                todoList.RemoveAt(i - 1)
            Next
        End While
        If File.Exists(todoPath) Then
            todo = File.ReadAllLines(todoPath)
            For i As Integer = 1 To todo.Length
                Dim tempArray(2) As String
                tempArray = todo(i - 1).Split(New Char() {"|"}, 3)
                todoList.Add({tempArray(0), tempArray(1), tempArray(2)})
            Next
        Else
            todo = {}
        End If
    End Sub
    Sub SaveToDo()
        'Delete file and rewrite
        If File.Exists(todoPath) Then File.Delete(todoPath)
        For i As Integer = 0 To (todoList.Count - 1)
            File.AppendAllLines(todoPath, {String.Format("{0}|{1}|{2}", todoList(i)(0), todoList(i)(1), todoList(i)(2))})
        Next
    End Sub
    Sub DisplayToDo()
        Console.Clear()
        If viewAll Then
            For i As Integer = 0 To (todoList.Count - 1)
                Console.WriteLine(String.Format("{0} | {1} | {2}", todoList(i)(0), getState(i), todoList(i)(2)))
            Next
        Else
            For i As Integer = (todoList.Count - 1) To 0 Step -1
                If todoList(i)(1) = "y" Then Continue For
                If todoList(i)(1) = "n" Then Console.WriteLine(String.Format("{0} | {1}", todoList(i)(0), todoList(i)(2)))
            Next
        End If
        'Display Stuff
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.Write("[A]dd item  |  [U]pdate Item  |  [V]iew change  |  [Q]uit")
        Console.ForegroundColor = ConsoleColor.White
        Select Case Console.ReadKey(True).Key
            Case ConsoleKey.A : AddToDo()
            Case ConsoleKey.U : UpdateItem()
            Case ConsoleKey.V : If viewAll = False Then viewAll = True Else viewAll = False
            Case ConsoleKey.Q : Console.WriteLine() : End
        End Select
        DisplayToDo()
    End Sub
    Sub AddToDo()
        Console.Clear()
        Console.WriteLine("Please enter the description of the To-Do item:")
        Dim desc As String = Console.ReadLine()
        If Not desc = "" Then
            todoList.Add({todoList.Count.ToString(), "n", desc})
            SaveToDo()
        End If
    End Sub
    Function getState(id As Integer)
        If todoList(id)(1) = "y" Then Return " Complete "
        If todoList(id)(1) = "n" Then Return "Incomplete"
        Return "  Error   "
    End Function
    Sub UpdateItem()
        Console.Clear()
        Console.Write("Please enter item id: ")
        Dim id As String
        Do
            Dim inKey As ConsoleKeyInfo = Console.ReadKey(True)
            Select Case inKey.Key
                Case ConsoleKey.D0 : Console.Write("0") : id = id + "0"
                Case ConsoleKey.D1 : Console.Write("1") : id = id + "1"
                Case ConsoleKey.D2 : Console.Write("2") : id = id + "2"
                Case ConsoleKey.D3 : Console.Write("3") : id = id + "3"
                Case ConsoleKey.D4 : Console.Write("4") : id = id + "4"
                Case ConsoleKey.D5 : Console.Write("5") : id = id + "5"
                Case ConsoleKey.D6 : Console.Write("6") : id = id + "6"
                Case ConsoleKey.D7 : Console.Write("7") : id = id + "7"
                Case ConsoleKey.D8 : Console.Write("8") : id = id + "8"
                Case ConsoleKey.D9 : Console.Write("9") : id = id + "9"
                Case ConsoleKey.NumPad0 : Console.Write("0") : id = id + "0"
                Case ConsoleKey.NumPad1 : Console.Write("1") : id = id + "1"
                Case ConsoleKey.NumPad2 : Console.Write("2") : id = id + "2"
                Case ConsoleKey.NumPad3 : Console.Write("3") : id = id + "3"
                Case ConsoleKey.NumPad4 : Console.Write("4") : id = id + "4"
                Case ConsoleKey.NumPad5 : Console.Write("5") : id = id + "5"
                Case ConsoleKey.NumPad6 : Console.Write("6") : id = id + "6"
                Case ConsoleKey.NumPad7 : Console.Write("7") : id = id + "7"
                Case ConsoleKey.NumPad8 : Console.Write("8") : id = id + "8"
                Case ConsoleKey.NumPad9 : Console.Write("9") : id = id + "9"
                Case ConsoleKey.Backspace
                    If Not id = "" Then
                        Console.Write(vbBack)
                        Console.Write(" ")
                        Console.Write(vbBack)
                        id = id.Remove(id.Length - 1)
                    End If
                Case ConsoleKey.Enter : Exit Do
            End Select
        Loop
        If id = "" Then Exit Sub
        Console.WriteLine()
        Try
            Console.WriteLine("Current item is" + vbCrLf + String.Format("  ID #{0} | {1} | ""{2}""", todoList(id)(0), getState(id), todoList(id)(2)))
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.Write("[M]ark as complete/incomplete  |  [U]pdate Description  |  [P]ick Other ID  |  [R]eturn to Menu")
            Console.ForegroundColor = ConsoleColor.White
            Select Case Console.ReadKey(True).Key
                Case ConsoleKey.M
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine()
                    Console.Write("Mark as [C]omplete  |  Mark as [I]ncomplete  |  [R]eturn to Menu")
                    Console.ForegroundColor = ConsoleColor.White
                    Select Case Console.ReadKey(True).Key
                        Case ConsoleKey.C
                            todoList(id)(1) = "y"
                            Console.WriteLine()
                            Console.WriteLine(String.Format("ID #{0} marked as complete. Returning to menu.", id))
                            System.Threading.Thread.Sleep(3000)
                            SaveToDo()
                            Exit Sub
                        Case ConsoleKey.I
                            todoList(id)(1) = "n"
                            Console.WriteLine()
                            Console.WriteLine(String.Format("ID #{0} marked as incomplete. Returning to menu.", id))
                            System.Threading.Thread.Sleep(3000)
                            SaveToDo()
                            Exit Sub
                        Case ConsoleKey.R : Exit Sub
                    End Select
                Case ConsoleKey.U
                    Console.WriteLine()
                    Console.WriteLine(String.Format("Please enter the new description for ID#{0}:", id))
                    Dim desc As String = Console.ReadLine()
                    If Not desc = "" Then
                        todoList(id)(2) = desc
                        SaveToDo()
                        Console.WriteLine(String.Format("Description for ID#{0} updated. Returning to menu.", id))
                        System.Threading.Thread.Sleep(3000)
                        Exit Sub
                    End If
                    Console.WriteLine("Description not updated. Returning to menu.")
                    System.Threading.Thread.Sleep(3000)
                    Exit Sub
                Case ConsoleKey.P : UpdateItem()
                Case ConsoleKey.R : Exit Sub
            End Select
        Catch ex As Exception
            Console.WriteLine("ID not found. Returning to menu.")
            System.Threading.Thread.Sleep(3000)
            DisplayToDo()
        End Try
    End Sub
End Module

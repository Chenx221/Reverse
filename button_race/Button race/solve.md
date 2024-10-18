这么抽象的"赛马"游戏

打死也不会再研究pcode了，这反编译器给的结果全靠猜

解决方法：

在程序同目录建立`Reginfo.roo`文件，内容`1x2y-3412+lmn`

程序会基于此判断是否注册

细节：

```vb
Private Sub Command1_Click() '406474
  'Data Table: 401EC0
  Dim var_138 As Variant
  Dim var_DA As Integer
  Dim var_19C As Variant
  Dim var_18C As Variant
  loc_40610E: var_EC = CVar(Len(Me.txtname.Text)) 'Variant
  loc_406120: var_C4 = CVar(Me.txtname) 'Variant
  loc_40612C: var_A4 = CVar(Me.txtreply) 'Variant
  loc_406130: On Error Goto loc_4063F3
  loc_406145: For var_168 = 1 To Len(global_52): var_FC = var_168 'Variant
  loc_406180:   var_D8 = CVar(Val(CStr((Asc(CStr(Mid(global_52, CLng(var_FC), 1))) * 3)))) 'Variant
  loc_4061FE:   var_94 = var_94 & Hex(CVar(Val(CStr((var_D8 + CVar(CInt(Val(CStr((Asc(CStr(Mid(var_C4, CLng(var_FC), 1))) * 3)))))))))) 'Variant
  loc_406211: Next var_168 'Variant
  loc_406226: For var_1BC = 1 To Len(var_C4): var_10C = var_1BC 'Variant
  loc_40624D:   var_DA = Asc(CStr(Mid(var_C4, CLng(var_EC), 1)))
  loc_406274:   var_B4 = var_B4 & Right(var_D8, 2) 'Variant
  loc_406287:   var_EC = (var_EC - 1) 'Variant
  loc_40628E: Next var_1BC 'Variant
  loc_4062AD: For var_1C0 = CByte(1) To CByte(Len(var_94)): var_10E = var_1C0 'Byte
  loc_4062DC:   If (CVar(Val(CStr((1 + 1)))) > Len(var_B4)) Then
  loc_4062E4:     var_EC = 1 'Variant
  loc_4062E8:   End If
  loc_406306:   var_18C = CVar(var_C8) & Mid(var_94, CLng(var_10E), 1) 
  loc_40630A:   var_19C = 2
  loc_406327:   var_C8 = CStr(var_18C & Mid(var_B4, CLng(var_EC), var_19C))
  loc_406341:   var_138 = (var_EC + 1)
  loc_406345:   var_EC = var_138 'Variant
  loc_40634C: Next var_1C0 'Byte
  loc_406355: Randomize(var_138)
  loc_40638B: Me.lblquery.Caption = CStr(CVar(Call Proc_2_0_404A90(&H186A0, &HDBBA0)))
  loc_4063A2: If (CVar(var_C8) = var_A4) Then
  loc_4063A5:   Call makereg()
  loc_4063B7:   Me.Global.Unload Me
  loc_4063E0:   MsgBox("Thank you for registering this program, and enjoy. Please restart the game to get full registered use.", &H40, "Registered", var_18C, var_19C)
  loc_4063F0:   GoTo loc_406471
  loc_4063F3:   ' Referenced from: 406130
  loc_4063F3: End If
  loc_406417: var_138 = CVar("Sorry, the password you entered was incorrect." & vbCrLf & "To buy this program for only £253.27 please contact our sales department via email: quibus_umbra@hotmail.com") 'String
  loc_40641A: MsgBox(var_138, &H10, "Incorrect", var_18C, var_19C)
  loc_406430: Randomize(var_138)
  loc_406466: Me.lblquery.Caption = CStr(CVar(Call Proc_2_0_404A90(&H186A0, &HDBBA0)))
  loc_406471: ' Referenced from: 4063F0
  loc_406471: Exit Sub
End Sub
```

没什么好的思路，所以瞟了瞟x64dbg

```
150C05850105C50205150E05F50105E50305150D50705150D05D50 //True Reply
caonima //Name
50505050505050 //这个不好说明，看下面代码vd8
sb //Reply
1C81C21EF1E31D71DD //不好说没，看下面代码v94
556342 //随机值，显示在软件窗口上部分label
```

得到组合关系后，直接开vs

```c#
if(args.Length != 2)
{
    Console.WriteLine("Usage: sp_keygen.exe <name> <random value>");
}
else
{
    string s1 = args[1];
    string name = args[0];
    string reply = "";
    int length = Math.Min(name.Length, s1.Length);

    int vd8 = 0;
    string vd8s = "";
    string v94 = "";
    for (int i = 0; i < length; i++)
    {
        vd8 = s1[i] * 3;
        int vd9 = name[i] * 3;
        int n = vd8 + vd9;
        v94 += n.ToString("X");
    }
    for (int k = 0; k < name.Length; k++)
    {
        vd8s += vd8.ToString()[^2..];
    }
    for (int i = 0, j = 0; i < v94.Length; i++, j++)
    {
        reply += v94[i].ToString() + vd8s[j] + vd8s[j + 1];
        if (i == vd8s.Length - 2)
        {
            j = -1;
        }
    }
    Console.WriteLine(reply);
}
```

彩蛋：

我留了一份`Bracing_Patched.exe`赛马无敌版
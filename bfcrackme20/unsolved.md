打包工具: ASPack(2.000)

1. 老方法脱壳

2. MethCallEngine

   P-Code

3. 看起来有两关？计算serial

   ```vb
     loc_40512A: var_98 = 112 'Variant
     loc_405133: var_A8 = 1564 'Variant
     loc_40513C: var_B8 = 1464 'Variant
     loc_40515A: var_17C = CVar(Len(Me.lk.Text)) 'Variant
     loc_405185: If Not((Me.lk.Text = "Ab")) Then
     loc_40518D:   var_18C = 0 'Variant
     loc_4051A9:   var_19C = CVar(Me.lk.Text) 'Variant
     loc_4051B0:   ' Referenced from: 405246
     loc_4051C3:   If CBool(Not (var_19C = vbNullString)) Then
     loc_4051D2:     var_18C = (var_18C + 1) 'Variant
     loc_405224:     var_1FC = (var_1FC + CVar(Asc(CStr(Left(Left(var_19C, CLng(var_18C)), 1))))) 'Variant
     loc_405242:     var_19C = Right(var_19C, CLng((var_17C - var_18C))) 'Variant
     loc_405246:     GoTo loc_4051B0
     loc_405249:   End If
     loc_405252:   If (var_1FC = CVar(Asc(CStr(Left(Left(var_19C, CLng(var_18C)), 1))))) Then
     loc_405255:     End
     loc_405257:     GoTo loc_40525A
     loc_40525A:     ' Referenced from: 405257
     loc_40525A:   End If
     loc_405290:   var_108 = (((var_98 * var_A8) Xor var_B8 - var_E8) - 10) 'Variant
     loc_40529E:   var_128 = (var_108 * var_1FC) 'Variant
     loc_4052D4:   var_148 = (CVar(Val(Me.sh.Text)) * var_108) 'Variant
     loc_4052E1:   If (var_148 = CVar(Val(Me.sh.Text))) Then
     loc_4052E4:     End
     loc_4052E9:   Else
     loc_4052F9:     If CBool(Not (var_148 < var_128)) Then
     loc_40530F:       var_168 = 11
     loc_40532C:       If CBool(Not ((var_148 + var_168) > (var_128 + 11))) Then
     loc_405332:         Me.Hide
     loc_405345:         Homo.Show var_168, var_20C
   ```

   ```c#
   int result = 0; //serial
   foreach (char c in name)
   {
       result += c;
   }
   string serial = result.ToString();
   ```

4. 没了，第二关过不去

   ```vb
     loc_405422: var_98 = 144 'Variant
     loc_40542B: var_A8 = 135 'Variant
     loc_405434: var_B8 = 1234 'Variant
     loc_405445: var_1AC = Me.kk.Text
     loc_405458: If (var_1AC = vbNullString) Then
     loc_40545B:   End
     loc_40545D:   GoTo loc_405460
     loc_405460:   ' Referenced from: 40545D
     loc_405460: End If
     loc_405472:  = .Text
     loc_4054C3: var_F8 = ((var_B8 Mod var_98 Xor var_A8) + (CVar(Val(var_1AC)) * 100)) 'Variant
     loc_4054E1: var_1CC = CVar(Len(Me.kk.Text)) 'Variant
     loc_40550C: var_1EC = CVar(Me.kk.Text) 'Variant
     loc_405513: Do 'loop at: 4055E3
     loc_40551F: var_1DC = (0 + 1) 'Variant
     loc_40557B: var_22C = CVar(Asc(CStr(Left(Left(CVar(Me.kk.Text), CLng(var_1DC)), 1)))) 'Variant
     loc_405592: var_23C = 57
     loc_4055A2: If CBool((var_22C < 48) Or (var_22C > var_23C)) Then
     loc_4055B2:   Me.we.Caption = "Only intergers"
     loc_4055BA:   Exit Sub
     loc_4055BB: End If
     loc_4055E3: Loop Until (Right(var_1EC, CLng((var_1CC - var_1DC))) = var_1A8) 'do at: 405513
     loc_405601: Call var_1FC = CDec(CVar(Me.kk.Text))
     loc_40561F: Set var_88 = MemVar_407044.sh
     loc_405653: var_168 = Left(CVar(Val(blot.sh.Text)), 3) 'Variant
     loc_405661: var_128 = (Right(Right(Right(var_1EC, CLng((var_1CC - var_1DC))), CLng((var_1CC - var_1DC))), CLng((var_1CC - var_1DC))) * var_168) 'Variant
     loc_40567F: var_178 = ((var_F8 * var_168) - 18) 'Variant
     loc_405693: If CBool(Not (var_128 < var_178)) Then
     loc_4056C0:   var_1A8 = 20
     loc_4056DD:   If CBool(Not ((CVar(Val(CStr(var_128))) + var_1A8) > (var_178 + 20))) Then
     loc_4056E3:     Me.Hide
     loc_4056F6:     Kanel.Show var_1A8, var_23C
   ```

   这是给人算的？怎么看都不像是有正确serial2的样子

   ```c#
   string serial = result.ToString(); //上一步算出的serial
   
   int v168 = int.Parse(serial[..3]);
   
   int v128 = int.Parse(serial2[1..]) * v168;
   
   int vf8 = 213 + int.Parse(serial2) * 100;
   result2 = vf8 * v168 - 18;
   
   //v128: serial2的值去掉最高位后乘以v168
   //result2: 213加上serial2的值乘以100，再乘以v168，减去18
   if (v128 == result2)
   {
       //Success
   }
   ```

   
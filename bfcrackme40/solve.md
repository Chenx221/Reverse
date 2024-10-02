艹，怎么又是pcode

1. 老方法脱壳

2. 算serial

   ```vb
     loc_402A73: If ((Me.we.Text = vbNullString) Or (Me.er.Text = vbNullString)) Then
     loc_402A76:   End
     loc_402A7B: Else
     loc_402A90:   var_88 = Me.we.Text
     loc_402ABF:   var_8C = CStr(Asc(CStr(Left(var_88, 1))))
     loc_402AEB:   var_9C = CStr(Asc(CStr(Right(var_88, 1))))
     loc_402B17:   var_A0 = CStr(Asc(CStr(Right(var_88, 2))))
     loc_402B80:   If (CStr((CDbl(var_8C & CStr((CDbl(var_8C & var_9C & var_A0) - CDbl(1))) & var_A0) + CDbl(1))) > Me.er.Text) Then
     loc_402BA3:     If (CStr((CDbl(var_8C & var_9C & var_A0) - CDbl(1))) < Me.er.Text) Then
   ```

   用不到company值

   ```c#
   int l = name.Length;
   string a = ((int)name[0]).ToString();
   string b = l == 1 ? a : ((int)name[name.Length - 1]).ToString();
   string c = l == 1 ? a : ((int)name[name.Length - 2]).ToString();
   //serial = a+b+c;
   ```
   
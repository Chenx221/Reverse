```
INFO: This crackme is actually split on two. First you need to find the serial,
           then you have to change the ERROR message. This can be done both
           in W32Dasm and SoftIce. If you're a newbie *cough cough* I would
           advice you to try both in order to get the best out of this crackme.
RULEZ: You have to patch in such a way that if you enter a WRONG serial, 
              the CORRECT one will be shown on the screen. 
              (Instead of the: "Wrong serial....."   you must see the REAL serial)
```

1. 寻找Serial

   可以看出，Serial是 `This program must be run under Win32`

   ```
   004408C4 | 55               | push ebp                       |
   004408C5 | 8BEC             | mov ebp,esp                    |
   004408C7 | 6A 00            | push 0                         |
   004408C9 | 53               | push ebx                       | ebx:&"绬B"
   004408CA | 8BD8             | mov ebx,eax                    | ebx:&"绬B", eax:&"绬B"
   004408CC | 33C0             | xor eax,eax                    | eax:&"绬B"
   004408CE | 55               | push ebp                       |
   004408CF | 68 29094400      | push b-crk475.440929           |
   004408D4 | 64:FF30          | push dword ptr fs:[eax]        |
   004408D7 | 64:8920          | mov dword ptr fs:[eax],esp     |
   004408DA | 8D55 FC          | lea edx,dword ptr ss:[ebp-4]   |
   004408DD | 8B83 D0020000    | mov eax,dword ptr ds:[ebx+2D0] | eax:&"绬B", [ebx+2D0]:"$褸"
   004408E3 | E8 BC14FEFF      | call <b-crk475.GetText>        |
   004408E8 | 8B45 FC          | mov eax,dword ptr ss:[ebp-4]   | [ebp-04]:User input
   004408EB | BA 3C094400      | mov edx,b-crk475.44093C        | 44093C:"This program must be run under Win32"
   004408F0 | E8 3732FCFF      | call <b-crk475._LStrCmp>       | Check
   004408F5 | 75 0F            | jne b-crk475.440906            |
   004408F7 | B2 01            | mov dl,1                       | Success
   004408F9 | 8B83 D8020000    | mov eax,dword ptr ds:[ebx+2D8] | eax:&"绬B", [ebx+2D8]:&"绬B"
   004408FF | E8 9013FEFF      | call b-crk475.421C94           |
   00440904 | EB 0D            | jmp b-crk475.440913            |
   00440906 | B2 01            | mov dl,1                       | Fail
   00440908 | 8B83 D4020000    | mov eax,dword ptr ds:[ebx+2D4] | eax:&"绬B", [ebx+2D4]:"S嬝艭I"
   0044090E | E8 8113FEFF      | call b-crk475.421C94           |
   00440913 | 33C0             | xor eax,eax                    | eax:&"绬B"
   00440915 | 5A               | pop edx                        | edx:&"绬B"
   00440916 | 59               | pop ecx                        |
   00440917 | 59               | pop ecx                        |
   00440918 | 64:8910          | mov dword ptr fs:[eax],edx     | edx:&"绬B"
   0044091B | 68 30094400      | push b-crk475.440930           |
   00440920 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
   00440923 | E8 782EFCFF      | call <b-crk475._LStrClr>       |
   00440928 | C3               | ret                            |
   00440929 | E9 3229FCFF      | jmp b-crk475.403260            |
   0044092E | EB F0            | jmp b-crk475.440920            |
   00440930 | 5B               | pop ebx                        | ebx:&"绬B"
   00440931 | 59               | pop ecx                        |
   00440932 | 5D               | pop ebp                        |
   00440933 | C3               | ret                            |
   ```

   

2. 修改错误提示

   原先的错误提示: `Wrong serial.....try again ;)`

   使用Resource Hacker修改RCData TForm1中Panel2的Caption

   如果修改后的程序在Memo1.Lines.Strings提示无效属性值，请手动处理一下Lines.Strings值
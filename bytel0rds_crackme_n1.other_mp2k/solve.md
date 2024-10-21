先来通过的凭证：

```
Name: chenx221
RegKey: 123-456-789-X
Serial: 11141
```

细节：

检查长度，然后带着name, regkey,serial进`crackme_1.403790`

```assembly
00403B2 | 6A 14                | push 14                            |
00403B2 | 8D85 F3FBFFFF        | lea eax,dword ptr ss:[ebp-40D]     | name
00403B3 | 50                   | push eax                           |
00403B3 | 6A 05                | push 5                             |
00403B3 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]       |
00403B3 | 50                   | push eax                           |
00403B3 | E8 0DFBFFFF          | call <JMP.&_GetDlgItemTextA@16>    |
00403B3 | 8D45 F8              | lea eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
00403B4 | 8D95 F3FBFFFF        | lea edx,dword ptr ss:[ebp-40D]     |
00403B4 | E8 FFF4FFFF          | call crackme_1.40304C              |
00403B4 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:Name
00403B5 | E8 3FF5FFFF          | call <crackme_1.GetLength>         |
00403B5 | 83F8 05              | cmp eax,5                          |
00403B5 | 0F8C 91000000        | jl <crackme_1.Fail>                | length >= 5
00403B5 | 6A 14                | push 14                            |
00403B6 | 8D85 F3FBFFFF        | lea eax,dword ptr ss:[ebp-40D]     |
00403B6 | 50                   | push eax                           |
00403B6 | 6A 06                | push 6                             |
00403B6 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]       |
00403B6 | 50                   | push eax                           |
00403B6 | E8 DAFAFFFF          | call <JMP.&_GetDlgItemTextA@16>    |
00403B7 | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]       | [ebp-0C]:"11141"
00403B7 | 8D95 F3FBFFFF        | lea edx,dword ptr ss:[ebp-40D]     |
00403B7 | E8 CCF4FFFF          | call crackme_1.40304C              |
00403B8 | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | RegKey
00403B8 | E8 0CF5FFFF          | call <crackme_1.GetLength>         |
00403B8 | 83F8 0D              | cmp eax,D                          | length == 13
00403B8 | 75 62                | jne <crackme_1.Fail>               |
00403B8 | 6A 14                | push 14                            |
00403B8 | 8D85 F3FBFFFF        | lea eax,dword ptr ss:[ebp-40D]     |
00403B9 | 50                   | push eax                           |
00403B9 | 6A 07                | push 7                             |
00403B9 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]       |
00403B9 | 50                   | push eax                           |
00403B9 | E8 ABFAFFFF          | call <JMP.&_GetDlgItemTextA@16>    |
00403BA | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]       | [ebp-04]:"chenx221"
00403BA | 8D95 F3FBFFFF        | lea edx,dword ptr ss:[ebp-40D]     |
00403BA | E8 9DF4FFFF          | call crackme_1.40304C              |
00403BA | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]       | Serial
00403BB | E8 DDF4FFFF          | call <crackme_1.GetLength>         |
00403BB | 83F8 05              | cmp eax,5                          | length == 5
00403BB | 75 33                | jne <crackme_1.Fail>               |
00403BB | 8B4D FC              | mov ecx,dword ptr ss:[ebp-4]       | S
00403BB | 8B55 F4              | mov edx,dword ptr ss:[ebp-C]       | R
00403BC | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | N
00403BC | E8 C6FBFFFF          | call crackme_1.403790              |
```

name字符ascii求和，然后统计1的个数

```assembly
0040379 | 55                   | push ebp                           |
0040379 | 8BEC                 | mov ebp,esp                        |
0040379 | 83C4 E8              | add esp,FFFFFFE8                   |
0040379 | 53                   | push ebx                           |
0040379 | 56                   | push esi                           |
0040379 | 57                   | push edi                           | edi:&"1010101011"
0040379 | 33DB                 | xor ebx,ebx                        |
0040379 | 895D E8              | mov dword ptr ss:[ebp-18],ebx      | [ebp-18]:"1010101011"
0040379 | 895D EC              | mov dword ptr ss:[ebp-14],ebx      |
004037A | 895D F0              | mov dword ptr ss:[ebp-10],ebx      | [ebp-10]:"122xnehc"
004037A | 894D F4              | mov dword ptr ss:[ebp-C],ecx       | S
004037A | 8955 F8              | mov dword ptr ss:[ebp-8],edx       | R
004037A | 8945 FC              | mov dword ptr ss:[ebp-4],eax       | N
004037A | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]       | Name
004037B | E8 9FF9FFFF          | call <crackme_1.LStrAddRef>        |
004037B | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | RegKey
004037B | E8 97F9FFFF          | call <crackme_1.LStrAddRef>        |
004037B | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | Serial
004037C | E8 8FF9FFFF          | call <crackme_1.LStrAddRef>        |
004037C | BF 68564000          | mov edi,crackme_1.405668           | edi:&"1010101011", 405668:&"1010101011"
004037C | 33C0                 | xor eax,eax                        |
004037C | 55                   | push ebp                           |
004037C | 68 8D3A4000          | push crackme_1.403A8D              |
004037D | 64:FF30              | push dword ptr fs:[eax]            |
004037D | 64:8920              | mov dword ptr fs:[eax],esp         |
004037D | C705 88564000 040000 | mov dword ptr ds:[405688],4        |
004037E | 33C0                 | xor eax,eax                        |
004037E | A3 6C564000          | mov dword ptr ds:[40566C],eax      |
004037E | 8BC7                 | mov eax,edi                        | edi:&"1010101011"
004037E | E8 58F7FFFF          | call <crackme_1.LStrClr>           |
004037F | 8D45 F0              | lea eax,dword ptr ss:[ebp-10]      | [ebp-10]:"122xnehc"
004037F | E8 50F7FFFF          | call <crackme_1.LStrClr>           |
004037F | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]       | [ebp-04]:"chenx221"
004037F | E8 94F8FFFF          | call <crackme_1.GetLength>         |
0040380 | 8BF0                 | mov esi,eax                        |
0040380 | 85F6                 | test esi,esi                       |
0040380 | 7E 26                | jle crackme_1.40382C               |
0040380 | BB 01000000          | mov ebx,1                          |
0040380 | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]      |
0040380 | 8B55 FC              | mov edx,dword ptr ss:[ebp-4]       | [ebp-04]:"chenx221"
0040381 | 8A541A FF            | mov dl,byte ptr ds:[edx+ebx-1]     |
0040381 | E8 22F8FFFF          | call crackme_1.40303C              |
0040381 | 8B55 EC              | mov edx,dword ptr ss:[ebp-14]      |
0040381 | 8D45 F0              | lea eax,dword ptr ss:[ebp-10]      | [ebp-10]:"122xnehc"
0040382 | 8B4D F0              | mov ecx,dword ptr ss:[ebp-10]      | [ebp-10]:"122xnehc"
0040382 | E8 B8F8FFFF          | call <crackme_1.LStrCat3>          |
0040382 | 43                   | inc ebx                            |
0040382 | 4E                   | dec esi                            |
0040382 | 75 DF                | jne crackme_1.40380B               |
0040382 | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]      | [ebp-10]:翻转Name
0040382 | E8 60F8FFFF          | call <crackme_1.GetLength>         |
0040383 | 8BF0                 | mov esi,eax                        |
0040383 | 85F6                 | test esi,esi                       |
0040383 | 7E 17                | jle crackme_1.403851               |
0040383 | BB 01000000          | mov ebx,1                          |
0040383 | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]      | [ebp-10]:"122xnehc"
0040384 | 0FB64418 FF          | movzx eax,byte ptr ds:[eax+ebx-1]  |
0040384 | 0105 6C564000        | add dword ptr ds:[40566C],eax      |
0040384 | 43                   | inc ebx                            |
0040384 | 4E                   | dec esi                            |
0040384 | 75 EE                | jne crackme_1.40383F               |
0040385 | 8D55 E8              | lea edx,dword ptr ss:[ebp-18]      | [ebp-18]:"1010101011"
0040385 | A1 6C564000          | mov eax,dword ptr ds:[40566C]      | 累加ascii
0040385 | E8 7AFEFFFF          | call <crackme_1.Hex2Bin>           |
0040385 | 8B55 E8              | mov edx,dword ptr ss:[ebp-18]      | 累加结果的二进制（不用不足位数
0040386 | 8BC7                 | mov eax,edi                        | edi:&"1010101011"
0040386 | E8 34F7FFFF          | call <crackme_1.LStrAsg>           |
0040386 | 33C0                 | xor eax,eax                        |
0040386 | A3 6C564000          | mov dword ptr ds:[40566C],eax      |
0040386 | 8B07                 | mov eax,dword ptr ds:[edi]         | [edi]:"1010101011"
0040387 | E8 1EF8FFFF          | call <crackme_1.GetLength>         | 获取二进制长度
0040387 | 8BF0                 | mov esi,eax                        |
0040387 | 85F6                 | test esi,esi                       |
0040387 | 7E 18                | jle crackme_1.403894               | 统计二进制1的个数
0040387 | BB 01000000          | mov ebx,1                          |
0040388 | 8B07                 | mov eax,dword ptr ds:[edi]         | [edi]:"1010101011"
0040388 | 807C18 FF 31         | cmp byte ptr ds:[eax+ebx-1],31     | 31:'1'
0040388 | 75 06                | jne crackme_1.403890               |
0040388 | FF05 6C564000        | inc dword ptr ds:[40566C]          |
0040389 | 43                   | inc ebx                            |
0040389 | 4E                   | dec esi                            |
0040389 | 75 ED                | jne crackme_1.403881               |
0040389 | 33C0                 | xor eax,eax                        |
```

regkey检查格式并求一个值

```c#
for (int i = 0; i < RegKey.Length; i++)
{
    if (i == 3 || i == 7 || i == 11)
        continue; //这几个位置检查'-'
    else if (i == 12)
    {
        regSum += RegKey[i] % 2;
        continue;
    }
    int n = int.Parse(RegKey[i].ToString());
    if (i < 3)
        regSum += n * (0xB - (i + 1));
    else if (i < 7)
        regSum += n * (0xC - (i + 1));
    else if (i < 11)
        regSum += n * (0xD - (i + 1));
}
```

```assembly
0040389 | A3 70564000          | mov dword ptr ds:[405670],eax      |
0040389 | BB 01000000          | mov ebx,1                          |
004038A | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | RegKey
004038A | 8A5418 FF            | mov dl,byte ptr ds:[eax+ebx-1]     |
004038A | 8BC2                 | mov eax,edx                        |
004038A | 3C 31                | cmp al,31                          | <"1"
004038A | 0F82 C1010000        | jb <crackme_1.Fail2>               |
004038B | 3C 39                | cmp al,39                          | >"9"
004038B | 0F87 B9010000        | ja <crackme_1.Fail2>               |
004038B | 33C0                 | xor eax,eax                        |
004038B | 8AC2                 | mov al,dl                          |
004038B | 83E8 30              | sub eax,30                         | "数"2数
004038C | BA 0B000000          | mov edx,B                          | 0B:'\v'
004038C | 2BD3                 | sub edx,ebx                        |
004038C | F7EA                 | imul edx                           |
004038C | 0105 70564000        | add dword ptr ds:[405670],eax      |
004038C | 43                   | inc ebx                            |
004038D | 83FB 04              | cmp ebx,4                          |
004038D | 75 CB                | jne crackme_1.4038A0               |
004038D | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
004038D | 8078 03 2D           | cmp byte ptr ds:[eax+3],2D         | 第四位需要-
004038D | 0F85 90010000        | jne <crackme_1.Fail2>              |
004038E | BB 05000000          | mov ebx,5                          |
004038E | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
004038E | 8A4418 FF            | mov al,byte ptr ds:[eax+ebx-1]     |
004038E | 8BD0                 | mov edx,eax                        |
004038F | 80FA 31              | cmp dl,31                          | 31:'1'
004038F | 0F82 79010000        | jb <crackme_1.Fail2>               |
004038F | 80FA 39              | cmp dl,39                          | 39:'9'
004038F | 0F87 70010000        | ja <crackme_1.Fail2>               |
0040390 | 25 FF000000          | and eax,FF                         |
0040390 | 83E8 30              | sub eax,30                         |
0040390 | BA 0C000000          | mov edx,C                          | 0C:'\f'
0040390 | 2BD3                 | sub edx,ebx                        |
0040391 | F7EA                 | imul edx                           |
0040391 | 0105 70564000        | add dword ptr ds:[405670],eax      |
0040391 | 43                   | inc ebx                            |
0040391 | 83FB 08              | cmp ebx,8                          |
0040391 | 75 C8                | jne crackme_1.4038E7               |
0040391 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
0040392 | 8078 07 2D           | cmp byte ptr ds:[eax+7],2D         | 第八位-
0040392 | 0F85 46010000        | jne <crackme_1.Fail2>              |
0040392 | BB 09000000          | mov ebx,9                          | 09:'\t'
0040393 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
0040393 | 8A4418 FF            | mov al,byte ptr ds:[eax+ebx-1]     |
0040393 | 8BD0                 | mov edx,eax                        |
0040393 | 80FA 31              | cmp dl,31                          | 31:'1'
0040393 | 0F82 2F010000        | jb <crackme_1.Fail2>               |
0040394 | 80FA 39              | cmp dl,39                          | 39:'9'
0040394 | 0F87 26010000        | ja <crackme_1.Fail2>               |
0040394 | 25 FF000000          | and eax,FF                         |
0040395 | 83E8 30              | sub eax,30                         |
0040395 | BA 0D000000          | mov edx,D                          | 0D:'\r'
0040395 | 2BD3                 | sub edx,ebx                        |
0040395 | F7EA                 | imul edx                           |
0040395 | 0105 70564000        | add dword ptr ds:[405670],eax      |
0040396 | 43                   | inc ebx                            |
0040396 | 83FB 0C              | cmp ebx,C                          | 0C:'\f'
0040396 | 75 C8                | jne crackme_1.403931               |
0040396 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
0040396 | 8078 0B 2D           | cmp byte ptr ds:[eax+B],2D         | 第十二位-
0040397 | 0F85 FC000000        | jne <crackme_1.Fail2>              |
0040397 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | [ebp-08]:"123-456-789-X"
0040397 | 8A50 0C              | mov dl,byte ptr ds:[eax+C]         |
0040397 | 8BC2                 | mov eax,edx                        |
0040397 | 3C 41                | cmp al,41                          | 41:'A'
0040398 | 0F82 EC000000        | jb <crackme_1.Fail2>               |
0040398 | 3C 5A                | cmp al,5A                          | 5A:'Z'
0040398 | 0F87 E4000000        | ja <crackme_1.Fail2>               |
0040398 | 33C0                 | xor eax,eax                        |
0040399 | 8AC2                 | mov al,dl                          |
0040399 | 83E0 01              | and eax,1                          |
0040399 | 0105 70564000        | add dword ptr ds:[405670],eax      |
```

计算前面的值^二进制1的个数 (v1)

```assembly
0040399 | 33C0                 | xor eax,eax                        |
0040399 | 8905 7C564000        | mov dword ptr ds:[40567C],eax      |
004039A | C705 80564000 000000 | mov dword ptr ds:[405680],80000000 |
004039A | 66:C705 84564000 FF3 | mov word ptr ds:[405684],3FFF      | 00405684:"-@"
004039B | 8B35 6C564000        | mov esi,dword ptr ds:[40566C]      | 二进制中1的个数
004039B | 85F6                 | test esi,esi                       |
004039B | 7E 18                | jle crackme_1.4039D8               |
004039C | DB05 70564000        | fild dword ptr ds:[405670]         | 前面的结果^1的个数
004039C | DB2D 7C564000        | fld tword ptr ds:[40567C]          |
004039C | DEC9                 | fmulp st(1),st(0)                  |
004039C | DB3D 7C564000        | fstp tword ptr ds:[40567C]         |
004039D | 9B                   | fwait                              |
004039D | 4E                   | dec esi                            |
004039D | 75 E8                | jne crackme_1.4039C0               |
```

Serial字符串转数值

```assembly
004039D | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | Serial
004039D | E8 B4F6FFFF          | call <crackme_1.GetLength>         |
004039E | 8BF0                 | mov esi,eax                        |
004039E | 85F6                 | test esi,esi                       |
004039E | 7E 18                | jle crackme_1.4039FE               |
004039E | BB 01000000          | mov ebx,1                          |
004039E | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | 检查Serial是否都是数
004039E | 8A4418 FF            | mov al,byte ptr ds:[eax+ebx-1]     |
004039F | 3C 31                | cmp al,31                          | 31:'1'
004039F | 72 7C                | jb <crackme_1.Fail2>               |
004039F | 3C 39                | cmp al,39                          | 39:'9'
004039F | 77 78                | ja <crackme_1.Fail2>               |
004039F | 43                   | inc ebx                            |
004039F | 4E                   | dec esi                            |
004039F | 75 ED                | jne crackme_1.4039EB               |
004039F | 33C0                 | xor eax,eax                        |
00403A0 | A3 74564000          | mov dword ptr ds:[405674],eax      |
00403A0 | C705 78564000 010000 | mov dword ptr ds:[405678],1        |
00403A0 | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | [ebp-0C]:"11141"
00403A1 | E8 7DF6FFFF          | call <crackme_1.GetLength>         |
00403A1 | 8BF0                 | mov esi,eax                        |
00403A1 | 4E                   | dec esi                            |
00403A1 | 85F6                 | test esi,esi                       |
00403A1 | 7C 37                | jl crackme_1.403A55                | 字符串转数
00403A1 | 46                   | inc esi                            |
00403A1 | 33DB                 | xor ebx,ebx                        |
00403A2 | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | [ebp-0C]:"11141"
00403A2 | E8 6BF6FFFF          | call <crackme_1.GetLength>         |
00403A2 | 2BC3                 | sub eax,ebx                        |
00403A2 | 8B55 F4              | mov edx,dword ptr ss:[ebp-C]       | [ebp-0C]:"11141"
00403A2 | 0FB64402 FF          | movzx eax,byte ptr ds:[edx+eax-1]  | 从最后一位开始
00403A3 | 83E8 30              | sub eax,30                         | -0 字符转数
00403A3 | F72D 78564000        | imul dword ptr ds:[405678]         |
00403A3 | 0105 74564000        | add dword ptr ds:[405674],eax      |
00403A4 | A1 78564000          | mov eax,dword ptr ds:[405678]      |
00403A4 | 03C0                 | add eax,eax                        | *2
00403A4 | 8D0480               | lea eax,dword ptr ds:[eax+eax*4]   | *5
00403A4 | A3 78564000          | mov dword ptr ds:[405678],eax      |
00403A5 | 43                   | inc ebx                            |
00403A5 | 4E                   | dec esi                            |
00403A5 | 75 CC                | jne crackme_1.403A21               |
```

前面的前面 算的v1 % 上面序列号数值

```assembly
00403A5 | 99                   | cdq                                |
00403A5 | 52                   | push edx                           |
00403A5 | 50                   | push eax                           |
00403A5 | DB2D 7C564000        | fld tword ptr ds:[40567C]          |
00403A6 | E8 14EBFFFF          | call crackme_1.40257C              |
00403A6 | E8 63F7FFFF          | call crackme_1.4031D0              | 之前算的^结果 % 上面的结果
00403A6 | A3 88564000          | mov dword ptr ds:[405688],eax      |
```

检查模数是否是素数，是的就成功了

```
00403BC | A1 88564000          | mov eax,dword ptr ds:[405688]      |
00403BC | E8 80FBFFFF          | call crackme_1.403754              | 判断素数
00403BD | 3C 01                | cmp al,1                           |
00403BD | 75 17                | jne <crackme_1.Fail>               |
00403BD | 6A 40                | push 40                            | Success
00403BD | 68 4C3C4000          | push crackme_1.403C4C              | 403C4C:"Succeed!"
00403BD | 68 583C4000          | push crackme_1.403C58              | 403C58:"Great you got a working Serial and key,now write a keygen and send it to me!"
00403BE | 8B45 08              | mov eax,dword ptr ss:[ebp+8]       |
00403BE | 50                   | push eax                           |
00403BE | E8 67FAFFFF          | call <JMP.&_MessageBoxA@16>        |
```
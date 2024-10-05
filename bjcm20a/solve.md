寻找serial

```
长度9位，仅数字
比较 单位ascii==(index xor 2)(最后一位)
```

| index | (XOR) value | result | real result |
| ----- | ----------- | ------ | ----------- |
| 1     | 2           | 3      | 3           |
| 2     | 2           | 0      | 0           |
| 3     | 2           | 1      | 1           |
| 4     | 2           | 6      | 6           |
| 5     | 2           | 7      | 7           |
| 6     | 2           | 4      | 4           |
| 7     | 2           | 5      | 5           |
| 8     | 2           | 10     | 0           |
| 9     | 2           | 11     | 1           |

Serial结果: `301674501`

详细信息：

```assembly
00403620 | 55                   | push ebp                                    | CheckSerial
...
...
004036D9 | 8B45 E4              | mov eax,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
004036DC | 50                   | push eax                                    |
004036DD | FF15 08104000        | call dword ptr ds:[<__vbaLenBstr>]          |
004036E3 | 33C9                 | xor ecx,ecx                                 |
004036E5 | 83F8 09              | cmp eax,9                                   | 检查serial长度是否为9
004036E8 | 0F95C1               | setne cl                                    |
004036EB | F7D9                 | neg ecx                                     |
004036ED | 8BF1                 | mov esi,ecx                                 | esi:__vbaStrMove
004036EF | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
004036F2 | FF15 C0104000        | call dword ptr ds:[<__vbaFreeStr>]          |
004036F8 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
004036FB | FF15 C4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
00403701 | 66:3BF3              | cmp si,bx                                   |
00403704 | 0F85 1A030000        | jne <bjcm20a.Fail>                          |
0040370A | 8B17                 | mov edx,dword ptr ds:[edi]                  |
0040370C | 57                   | push edi                                    |
0040370D | FF92 08030000        | call dword ptr ds:[edx+308]                 |
00403713 | 50                   | push eax                                    |
00403714 | 8D45 D4              | lea eax,dword ptr ss:[ebp-2C]               |
00403717 | 50                   | push eax                                    |
00403718 | FF15 2C104000        | call dword ptr ds:[<__vbaObjSet>]           |
0040371E | 8BF0                 | mov esi,eax                                 | esi:__vbaStrMove
00403720 | 8D55 E4              | lea edx,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
00403723 | 52                   | push edx                                    |
00403724 | 56                   | push esi                                    | esi:__vbaStrMove
00403725 | 8B0E                 | mov ecx,dword ptr ds:[esi]                  | esi:__vbaStrMove
00403727 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                  |
0040372D | 3BC3                 | cmp eax,ebx                                 | ebx:rtcStrFromVar
0040372F | DBE2                 | fnclex                                      |
...
...
00403745 | 8B45 E4              | mov eax,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
00403748 | 50                   | push eax                                    |
00403749 | FF15 08104000        | call dword ptr ds:[<__vbaLenBstr>]          |
0040374F | 8BC8                 | mov ecx,eax                                 |
00403751 | FF15 50104000        | call dword ptr ds:[<__vbaI2I4>]             |
00403757 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
0040375A | 8985 14FFFFFF        | mov dword ptr ss:[ebp-EC],eax               |
00403760 | C745 E8 01000000     | mov dword ptr ss:[ebp-18],1                 | index=1
00403767 | FF15 C0104000        | call dword ptr ds:[<__vbaFreeStr>]          |
0040376D | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
00403770 | FF15 C4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
00403776 | 8B35 AC104000        | mov esi,dword ptr ds:[<__vbaStrMove>]       | esi:__vbaStrMove
0040377C | 66:8B8D 14FFFFFF     | mov cx,word ptr ss:[ebp-EC]                 | Loop
00403783 | 66:394D E8           | cmp word ptr ss:[ebp-18],cx                 | 取出每一位进行检查
00403787 | 0F8F 17030000        | jg <bjcm20a.Success>                        |
0040378D | 8B17                 | mov edx,dword ptr ds:[edi]                  |
...
...
0040381D | 51                   | push ecx                                    |
0040381E | 57                   | push edi                                    | edi:Index
0040381F | 52                   | push edx                                    | edx:Name
00403820 | 8945 C0              | mov dword ptr ss:[ebp-40],eax               |
00403823 | 8945 B0              | mov dword ptr ss:[ebp-50],eax               |
00403826 | FF15 44104000        | call dword ptr ds:[<Ordinal#631>]           |
0040382C | 8BD0                 | mov edx,eax                                 |
0040382E | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00403831 | FFD6                 | call esi                                    | 检查当前位是否是数字
00403833 | 50                   | push eax                                    | 通过循环检查整体
00403834 | FF15 1C104000        | call dword ptr ds:[<Ordinal#516>]           |
0040383A | 8B4D E4              | mov ecx,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
0040383D | 33DB                 | xor ebx,ebx                                 | ebx:rtcStrFromVar
0040383F | 66:3D 3900           | cmp ax,39                                   | 39:'9'
00403843 | 8D45 C0              | lea eax,dword ptr ss:[ebp-40]               |
00403846 | 50                   | push eax                                    |
00403847 | 57                   | push edi                                    |
00403848 | 0F9FC3               | setg bl                                     |
0040384B | 51                   | push ecx                                    |
0040384C | F7DB                 | neg ebx                                     | ebx:rtcStrFromVar
0040384E | FF15 44104000        | call dword ptr ds:[<Ordinal#631>]           |
00403854 | 8BD0                 | mov edx,eax                                 |
00403856 | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]               |
00403859 | FFD6                 | call esi                                    | esi:__vbaStrMove
0040385B | 50                   | push eax                                    |
0040385C | FF15 1C104000        | call dword ptr ds:[<Ordinal#516>]           |
00403862 | 33D2                 | xor edx,edx                                 |
00403864 | 66:3D 3000           | cmp ax,30                                   | 30:'0'
00403868 | 0F9CC2               | setl dl                                     |
0040386B | F7DA                 | neg edx                                     |
0040386D | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]               |
00403870 | 23DA                 | and ebx,edx                                 | ebx:rtcStrFromVar
00403872 | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]               | [ebp-24]:L" 49"
00403875 | 50                   | push eax                                    |
00403876 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]               |
00403879 | 51                   | push ecx                                    |
0040387A | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
...
...
004038A7 | 83C4 2C              | add esp,2C                                  |
004038AA | 66:85DB              | test bx,bx                                  |
004038AD | 0F85 6F010000        | jne <bjcm20a.Error2>                        | 如果包含非数字内容则Error2
004038B3 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                | [ebp+08]:"tZ@"
...
...
004038F1 | 66:8B45 E8           | mov ax,word ptr ss:[ebp-18]                 |
004038F5 | 8B1D 74104000        | mov ebx,dword ptr ds:[<Ordinal#536>]        | ebx:rtcStrFromVar
004038FB | 66:35 0200           | xor ax,2                                    | index xor 2
004038FF | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]               |
00403902 | 0F80 A4020000        | jo bjcm20a.403BAC                           |
00403908 | 51                   | push ecx                                    |
00403909 | 66:8945 A8           | mov word ptr ss:[ebp-58],ax                 |
0040390D | C745 A0 02000000     | mov dword ptr ss:[ebp-60],2                 |
00403914 | FFD3                 | call ebx                                    | ebx:rtcStrFromVar
00403916 | 8BD0                 | mov edx,eax                                 |
00403918 | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
0040391B | FFD6                 | call esi                                    | esi:__vbaStrMove
0040391D | 8B45 E4              | mov eax,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
00403920 | 8D55 C0              | lea edx,dword ptr ss:[ebp-40]               |
00403923 | 52                   | push edx                                    |
00403924 | 57                   | push edi                                    |
00403925 | 50                   | push eax                                    |
00403926 | C745 C8 01000000     | mov dword ptr ss:[ebp-38],1                 |
0040392D | C745 C0 02000000     | mov dword ptr ss:[ebp-40],2                 |
00403934 | FF15 44104000        | call dword ptr ds:[<Ordinal#631>]           |
0040393A | 8BD0                 | mov edx,eax                                 |
0040393C | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]               |
0040393F | FFD6                 | call esi                                    | esi:__vbaStrMove
00403941 | 50                   | push eax                                    |
00403942 | FF15 1C104000        | call dword ptr ds:[<Ordinal#516>]           |
00403948 | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]               |
0040394B | 66:8945 B8           | mov word ptr ss:[ebp-48],ax                 |
0040394F | 51                   | push ecx                                    |
00403950 | C745 B0 02000000     | mov dword ptr ss:[ebp-50],2                 |
00403957 | FFD3                 | call ebx                                    | ebx:rtcStrFromVar
00403959 | 8BD0                 | mov edx,eax                                 |
0040395B | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]               | [ebp-24]:L" 49"
0040395E | FFD6                 | call esi                                    | esi:__vbaStrMove
00403960 | 50                   | push eax                                    |
00403961 | FF15 84104000        | call dword ptr ds:[<__vbaR8Str>]            |
00403967 | DC25 D8104000        | fsub qword ptr ds:[4010D8]                  | 减去48(实际意义类似"1"->1)
0040396D | 8D55 90              | lea edx,dword ptr ss:[ebp-70]               |
00403970 | 6A 01                | push 1                                      |
00403972 | 52                   | push edx                                    |
00403973 | C785 30FFFFFF 058000 | mov dword ptr ss:[ebp-D0],8005              |
0040397D | DD9D 38FFFFFF        | fstp qword ptr ss:[ebp-C8]                  |
00403983 | DFE0                 | fnstsw ax                                   |
00403985 | A8 0D                | test al,D                                   |
00403987 | 0F85 1A020000        | jne bjcm20a.403BA7                          |
0040398D | 8B45 D8              | mov eax,dword ptr ss:[ebp-28]               |
00403990 | C745 D8 00000000     | mov dword ptr ss:[ebp-28],0                 |
00403997 | 8945 98              | mov dword ptr ss:[ebp-68],eax               | [ebp-68]:L" 3"
0040399A | 8D45 80              | lea eax,dword ptr ss:[ebp-80]               |
0040399D | 50                   | push eax                                    |
0040399E | C745 90 08000000     | mov dword ptr ss:[ebp-70],8                 |
004039A5 | FF15 B0104000        | call dword ptr ds:[<Ordinal#619>]           |
004039AB | 8D8D 30FFFFFF        | lea ecx,dword ptr ss:[ebp-D0]               |
004039B1 | 8D55 80              | lea edx,dword ptr ss:[ebp-80]               |
004039B4 | 51                   | push ecx                                    | (int) serial[index]
004039B5 | 52                   | push edx                                    | index Xor 2的结果取最后一位
004039B6 | FF15 A0104000        | call dword ptr ds:[<__vbaVarTstNe>]         | 比较
004039BC | 8BF8                 | mov edi,eax                                 |
004039BE | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]               |
004039C1 | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]               | [ebp-24]:L" 49"
004039C4 | 50                   | push eax                                    |
004039C5 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]               |
004039C8 | 51                   | push ecx                                    |
004039C9 | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]               | [ebp-1C]:L"123456789"
004039CC | 52                   | push edx                                    |
004039CD | 50                   | push eax                                    |
004039CE | 6A 04                | push 4                                      |
004039D0 | FF15 90104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
004039D6 | 83C4 14              | add esp,14                                  |
004039D9 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
004039DC | FF15 C4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
004039E2 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
004039E5 | 8D55 90              | lea edx,dword ptr ss:[ebp-70]               |
004039E8 | 51                   | push ecx                                    |
004039E9 | 8D45 A0              | lea eax,dword ptr ss:[ebp-60]               |
004039EC | 52                   | push edx                                    |
004039ED | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]               |
004039F0 | 50                   | push eax                                    |
004039F1 | 8D55 C0              | lea edx,dword ptr ss:[ebp-40]               |
004039F4 | 51                   | push ecx                                    |
004039F5 | 52                   | push edx                                    |
004039F6 | 6A 05                | push 5                                      |
004039F8 | FF15 0C104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
004039FE | 83C4 18              | add esp,18                                  |
00403A01 | 66:85FF              | test di,di                                  |
00403A04 | 75 1C                | jne <bjcm20a.Error2>                        |
00403A06 | 8B7D 08              | mov edi,dword ptr ss:[ebp+8]                | [ebp+08]:"tZ@"
00403A09 | B8 01000000          | mov eax,1                                   |
00403A0E | 66:0345 E8           | add ax,word ptr ss:[ebp-18]                 |
00403A12 | 0F80 94010000        | jo bjcm20a.403BAC                           |
00403A18 | 8945 E8              | mov dword ptr ss:[ebp-18],eax               |
00403A1B | 33DB                 | xor ebx,ebx                                 | ebx:rtcStrFromVar
00403A1D | E9 5AFDFFFF          | jmp bjcm20a.40377C                          | Next Loop
00403A22 | 33DB                 | xor ebx,ebx                                 | ebx:rtcStrFromVar
00403A24 | 8B35 A4104000        | mov esi,dword ptr ds:[<__vbaVarDup>]        | esi:__vbaStrMove
...FAIL
...
00403AA2 | EB 7E                | jmp bjcm20a.403B22                          |
00403AA4 | 8B35 A4104000        | mov esi,dword ptr ds:[<__vbaVarDup>]        | esi:__vbaStrMove
...SUCCESS
...
00403B22 | 6A 04                | push 4                                      |
...
...
```


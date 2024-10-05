计算serial

算法可简化为：

```c#
private static string? GenerateKey(string name)
{
	return $" {(name[0] + name[^1]) * 0xF423F}";
}
```

详细信息：

```assembly
004044E0 | 55                   | push ebp                                    | CheckSerial
004044E1 | 8BEC                 | mov ebp,esp                                 |
...
...
00404552 | FF92 0C030000        | call dword ptr ds:[edx+30C]                 |
00404558 | 8B1D 38104000        | mov ebx,dword ptr ds:[<__vbaObjSet>]        |
0040455E | 50                   | push eax                                    |
0040455F | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]               |
00404562 | 50                   | push eax                                    |
00404563 | FFD3                 | call ebx                                    |
00404565 | 8B08                 | mov ecx,dword ptr ds:[eax]                  |
00404567 | 8D55 D4              | lea edx,dword ptr ss:[ebp-2C]               | [ebp-2C]:Name
0040456A | 52                   | push edx                                    |
0040456B | 50                   | push eax                                    |
0040456C | 8985 44FFFFFF        | mov dword ptr ss:[ebp-BC],eax               |
00404572 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                  |
00404578 | 3BC7                 | cmp eax,edi                                 |
0040457A | DBE2                 | fnclex                                      |
0040457C | 7D 18                | jge bjcm10b.404596                          |
0040457E | 8B8D 44FFFFFF        | mov ecx,dword ptr ss:[ebp-BC]               |
00404584 | 68 A0000000          | push A0                                     |
00404589 | 68 00304000          | push bjcm10b.403000                         |
0040458E | 51                   | push ecx                                    |
0040458F | 50                   | push eax                                    |
00404590 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404596 | 8B55 D4              | mov edx,dword ptr ss:[ebp-2C]               |
00404599 | 52                   | push edx                                    |
0040459A | FF15 10104000        | call dword ptr ds:[<__vbaLenBstr>]          |
004045A0 | 33C9                 | xor ecx,ecx                                 |
004045A2 | 83F8 02              | cmp eax,2                                   | Name长度检查
004045A5 | 0F9CC1               | setl cl                                     |
004045A8 | F7D9                 | neg ecx                                     |
004045AA | 898D 3CFFFFFF        | mov dword ptr ss:[ebp-C4],ecx               |
004045B0 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
004045B3 | FF15 D0104000        | call dword ptr ds:[<__vbaFreeStr>]          |
004045B9 | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]               |
004045BC | FF15 D4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
004045C2 | 66:39BD 3CFFFFFF     | cmp word ptr ss:[ebp-C4],di                 |
004045C9 | 0F84 8B000000        | je bjcm10b.40465A                           |
...(长度不满足要求)
...
0040465A | 8B0E                 | mov ecx,dword ptr ds:[esi]                  |
0040465C | 56                   | push esi                                    |
0040465D | FF91 0C030000        | call dword ptr ds:[ecx+30C]                 |
00404663 | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]               |
00404666 | 50                   | push eax                                    |
00404667 | 52                   | push edx                                    |
00404668 | FFD3                 | call ebx                                    |
0040466A | 8B06                 | mov eax,dword ptr ds:[esi]                  |
0040466C | 56                   | push esi                                    |
0040466D | FF90 0C030000        | call dword ptr ds:[eax+30C]                 |
00404673 | 8D4D C8              | lea ecx,dword ptr ss:[ebp-38]               |
00404676 | 50                   | push eax                                    |
00404677 | 51                   | push ecx                                    |
00404678 | FFD3                 | call ebx                                    |
0040467A | 8B45 CC              | mov eax,dword ptr ss:[ebp-34]               |
0040467D | 8D55 B8              | lea edx,dword ptr ss:[ebp-48]               |
00404680 | 8945 C0              | mov dword ptr ss:[ebp-40],eax               |
00404683 | 6A 01                | push 1                                      |
00404685 | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404688 | 52                   | push edx                                    |
00404689 | 50                   | push eax                                    |
0040468A | 897D CC              | mov dword ptr ss:[ebp-34],edi               |
0040468D | C745 B8 09000000     | mov dword ptr ss:[ebp-48],9                 | 09:'\t'
00404694 | FF15 B4104000        | call dword ptr ds:[<Ordinal#617>]           |
0040469A | 8B45 C8              | mov eax,dword ptr ss:[ebp-38]               |
0040469D | 8D4D 98              | lea ecx,dword ptr ss:[ebp-68]               |
004046A0 | 6A 01                | push 1                                      |
004046A2 | 8D55 88              | lea edx,dword ptr ss:[ebp-78]               |
004046A5 | 51                   | push ecx                                    |
004046A6 | 52                   | push edx                                    |
004046A7 | 897D C8              | mov dword ptr ss:[ebp-38],edi               |
004046AA | 8945 A0              | mov dword ptr ss:[ebp-60],eax               |
004046AD | C745 98 09000000     | mov dword ptr ss:[ebp-68],9                 | 09:'\t'
004046B4 | FF15 C0104000        | call dword ptr ds:[<Ordinal#619>]           |
004046BA | 8B3D 80104000        | mov edi,dword ptr ds:[<__vbaStrVarVal>]     |
004046C0 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
004046C3 | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]               |
004046C6 | 50                   | push eax                                    |
004046C7 | 51                   | push ecx                                    |
004046C8 | FFD7                 | call edi                                    |
004046CA | 50                   | push eax                                    |
004046CB | FF15 24104000        | call dword ptr ds:[<Ordinal#516>]           | 和下面那块一样，取Name的头尾字符
004046D1 | 66:8BD0              | mov dx,ax                                   |
004046D4 | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
004046D7 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
004046DA | 50                   | push eax                                    |
004046DB | 51                   | push ecx                                    |
004046DC | 66:8995 26FFFFFF     | mov word ptr ss:[ebp-DA],dx                 |
004046E3 | FFD7                 | call edi                                    |
004046E5 | 50                   | push eax                                    |
004046E6 | FF15 24104000        | call dword ptr ds:[<Ordinal#516>]           |
004046EC | 66:8B95 26FFFFFF     | mov dx,word ptr ss:[ebp-DA]                 |
004046F3 | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
004046F6 | 66:03D0              | add dx,ax                                   | 两字符相加
004046F9 | C785 78FFFFFF 020000 | mov dword ptr ss:[ebp-88],2                 |
00404703 | 0F80 94030000        | jo <bjcm10b.ErrOverflow>                    |
00404709 | 66:8955 80           | mov word ptr ss:[ebp-80],dx                 |
0040470D | 8D95 78FFFFFF        | lea edx,dword ptr ss:[ebp-88]               |
00404713 | FF15 08104000        | call dword ptr ds:[<__vbaVarMove>]          |
...
...
00404751 | 83C4 2C              | add esp,2C                                  |
00404754 | 8D55 D8              | lea edx,dword ptr ss:[ebp-28]               | vb var
00404757 | 8D85 78FFFFFF        | lea eax,dword ptr ss:[ebp-88]               |
0040475D | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]               |
00404760 | 52                   | push edx                                    |
00404761 | 50                   | push eax                                    |
00404762 | 51                   | push ecx                                    |
00404763 | C745 80 3F420F00     | mov dword ptr ss:[ebp-80],F423F             |
0040476A | C785 78FFFFFF 030000 | mov dword ptr ss:[ebp-88],3                 |
00404774 | FF15 6C104000        | call dword ptr ds:[<__vbaVarMul>]           | 前面相加结果*0xF423F
0040477A | 50                   | push eax                                    |
0040477B | FF15 AC104000        | call dword ptr ds:[<__vbaI4Var>]            |
00404781 | 8B16                 | mov edx,dword ptr ds:[esi]                  | esi:"冈]"
00404783 | 56                   | push esi                                    | esi:"冈]"
00404784 | 8945 E8              | mov dword ptr ss:[ebp-18],eax               |
00404787 | FF92 FC020000        | call dword ptr ds:[edx+2FC]                 |
0040478D | 50                   | push eax                                    |
0040478E | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]               |
00404791 | 50                   | push eax                                    |
00404792 | FFD3                 | call ebx                                    |
00404794 | 8BF8                 | mov edi,eax                                 |
00404796 | 8D55 D4              | lea edx,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
00404799 | 52                   | push edx                                    |
0040479A | 57                   | push edi                                    |
0040479B | 8B0F                 | mov ecx,dword ptr ds:[edi]                  |
0040479D | FF91 A0000000        | call dword ptr ds:[ecx+A0]                  |
004047A3 | 85C0                 | test eax,eax                                |
004047A5 | DBE2                 | fnclex                                      |
004047A7 | 7D 12                | jge bjcm10b.4047BB                          |
004047A9 | 68 A0000000          | push A0                                     |
004047AE | 68 00304000          | push bjcm10b.403000                         |
004047B3 | 57                   | push edi                                    |
004047B4 | 50                   | push eax                                    |
004047B5 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004047BB | 8B45 D4              | mov eax,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
004047BE | 50                   | push eax                                    | eax:Serial
004047BF | 68 B0304000          | push bjcm10b.4030B0                         | Sorry, try again!
004047C4 | FF15 58104000        | call dword ptr ds:[<__vbaStrCmp>]           | 没用的检查
004047CA | 8BF8                 | mov edi,eax                                 |
004047CC | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
004047CF | F7DF                 | neg edi                                     |
004047D1 | 1BFF                 | sbb edi,edi                                 |
004047D3 | 47                   | inc edi                                     |
004047D4 | F7DF                 | neg edi                                     |
004047D6 | FF15 D0104000        | call dword ptr ds:[<__vbaFreeStr>]          |
004047DC | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]               |
004047DF | FF15 D4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
004047E5 | 66:85FF              | test di,di                                  |
004047E8 | 0F84 81000000        | je bjcm10b.40486F                           |
...
...
0040486F | 8B0E                 | mov ecx,dword ptr ds:[esi]                  | esi:"冈]"
00404871 | 8D45 E8              | lea eax,dword ptr ss:[ebp-18]               | 前面相乘的结果
00404874 | 56                   | push esi                                    | esi:"冈]"
00404875 | 8945 80              | mov dword ptr ss:[ebp-80],eax               |
00404878 | C785 78FFFFFF 034000 | mov dword ptr ss:[ebp-88],4003              |
00404882 | FF91 FC020000        | call dword ptr ds:[ecx+2FC]                 |
00404888 | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]               |
0040488B | 50                   | push eax                                    |
0040488C | 52                   | push edx                                    |
0040488D | FFD3                 | call ebx                                    |
0040488F | 8BF0                 | mov esi,eax                                 | esi:"冈]"
00404891 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
00404894 | 51                   | push ecx                                    |
00404895 | 56                   | push esi                                    | esi:"冈]"
00404896 | 8B06                 | mov eax,dword ptr ds:[esi]                  | esi:"冈]"
00404898 | FF90 A0000000        | call dword ptr ds:[eax+A0]                  |
0040489E | 85C0                 | test eax,eax                                |
004048A0 | DBE2                 | fnclex                                      |
004048A2 | 7D 12                | jge bjcm10b.4048B6                          |
004048A4 | 68 A0000000          | push A0                                     |
004048A9 | 68 00304000          | push bjcm10b.403000                         |
004048AE | 56                   | push esi                                    | esi:"冈]"
004048AF | 50                   | push eax                                    |
004048B0 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004048B6 | 8D95 78FFFFFF        | lea edx,dword ptr ss:[ebp-88]               |
004048BC | 52                   | push edx                                    |
004048BD | FF15 84104000        | call dword ptr ds:[<Ordinal#536>]           |
004048C3 | 8BD0                 | mov edx,eax                                 |
004048C5 | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]               |
004048C8 | FF15 BC104000        | call dword ptr ds:[<__vbaStrMove>]          |
004048CE | 50                   | push eax                                    |
004048CF | 8B45 D4              | mov eax,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
004048D2 | 50                   | push eax                                    |
004048D3 | FF15 58104000        | call dword ptr ds:[<__vbaStrCmp>]           | 比较输入的Serial与正确Serial
004048D9 | 8BF0                 | mov esi,eax                                 | esi:"冈]"
004048DB | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               | [ebp-2C]:"冈]"
004048DE | F7DE                 | neg esi                                     | esi:"冈]"
004048E0 | 1BF6                 | sbb esi,esi                                 | esi:"冈]"
004048E2 | 8D55 D0              | lea edx,dword ptr ss:[ebp-30]               |
004048E5 | 51                   | push ecx                                    |
004048E6 | 46                   | inc esi                                     | esi:"冈]"
004048E7 | 52                   | push edx                                    |
004048E8 | 6A 02                | push 2                                      |
004048EA | F7DE                 | neg esi                                     | esi:"冈]"
004048EC | FF15 9C104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
004048F2 | 83C4 0C              | add esp,C                                   |
004048F5 | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]               |
004048F8 | FF15 D4104000        | call dword ptr ds:[<__vbaFreeObj>]          |
004048FE | 8B3D 34104000        | mov edi,dword ptr ds:[<__vbaBoolStr>]       |
00404904 | 68 00314000          | push bjcm10b.403100                         | 403100:L"False"
00404909 | FFD7                 | call edi                                    |
0040490B | 66:3BF0              | cmp si,ax                                   |
0040490E | 0F85 81000000        | jne bjcm10b.404995                          |
...(错误的Serial)
...
00404995 | 68 10314000          | push bjcm10b.403110                         | 403110:L"True"
...(正确)
```


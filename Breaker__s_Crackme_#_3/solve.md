计算serial前要先把按钮的禁用状态取消掉：

```
第一个窗体的Button
139E: 00->01
第二个窗体的Button
22CA: 00->01
第二个窗体的TextBox
22E8: 00->01
2310: 00->01
2335: 00->01
235F: 00->01
```

计算serial

直接给成品：

```
以下索引全部从1开始
name2长度*name长度*age长度 字符串 p1
name从第二位开始取3个并翻转 p2
name2从第三位开始取2个 p3
name、name2、age字符串长度求和 p4 
age从第二位开始取1个 p5
name长度 p6
p1-p2p3p4-p5p6
```

细节：

```assembly
00402460 | 55                   | push ebp                                   |
00402461 | 8BEC                 | mov ebp,esp                                |
00402463 | 83EC 0C              | sub esp,C                                  |
00402466 | 68 F6104000          | push <JMP.&__vbaExceptHandler>             |
0040246B | 64:A1 00000000       | mov eax,dword ptr fs:[0]                   |
00402471 | 50                   | push eax                                   |
00402472 | 64:8925 00000000     | mov dword ptr fs:[0],esp                   |
00402479 | 81EC 28020000        | sub esp,228                                |
0040247F | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402480 | 56                   | push esi                                   |
00402481 | 57                   | push edi                                   | edi:"\\J@"
00402482 | 8965 F4              | mov dword ptr ss:[ebp-C],esp               |
00402485 | C745 F8 D0104000     | mov dword ptr ss:[ebp-8],breaker's crackme |
0040248C | 8B7D 08              | mov edi,dword ptr ss:[ebp+8]               | [ebp+08]:"\\J@"
0040248F | 8BC7                 | mov eax,edi                                | edi:"\\J@"
00402491 | 83E0 01              | and eax,1                                  |
00402494 | 8945 FC              | mov dword ptr ss:[ebp-4],eax               |
00402497 | 83E7 FE              | and edi,FFFFFFFE                           | edi:"\\J@"
0040249A | 57                   | push edi                                   | edi:"\\J@"
0040249B | 897D 08              | mov dword ptr ss:[ebp+8],edi               | [ebp+08]:"\\J@"
0040249E | 8B0F                 | mov ecx,dword ptr ds:[edi]                 | edi:"\\J@"
004024A0 | FF51 04              | call dword ptr ds:[ecx+4]                  |
004024A3 | 8B1D 94104000        | mov ebx,dword ptr ds:[<__vbaStrCopy>]      | ebx:__vbaStrCopy
004024A9 | 33F6                 | xor esi,esi                                |
004024AB | BA 501F4000          | mov edx,breaker's crackme # 3_patched.401F | edx:L"Hello", 401F50:L"Hello"
004024B0 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]              |
004024B3 | 8975 E8              | mov dword ptr ss:[ebp-18],esi              |
004024B6 | 8975 E4              | mov dword ptr ss:[ebp-1C],esi              |
004024B9 | 8975 E0              | mov dword ptr ss:[ebp-20],esi              |
004024BC | 8975 DC              | mov dword ptr ss:[ebp-24],esi              |
004024BF | 8975 D8              | mov dword ptr ss:[ebp-28],esi              |
004024C2 | 8975 D4              | mov dword ptr ss:[ebp-2C],esi              |
004024C5 | 8975 D0              | mov dword ptr ss:[ebp-30],esi              |
004024C8 | 8975 CC              | mov dword ptr ss:[ebp-34],esi              |
004024CB | 8975 C8              | mov dword ptr ss:[ebp-38],esi              |
004024CE | 8975 C4              | mov dword ptr ss:[ebp-3C],esi              |
004024D1 | 8975 C0              | mov dword ptr ss:[ebp-40],esi              |
004024D4 | 8975 BC              | mov dword ptr ss:[ebp-44],esi              |
004024D7 | 8975 B8              | mov dword ptr ss:[ebp-48],esi              |
004024DA | 8975 B4              | mov dword ptr ss:[ebp-4C],esi              |
004024DD | 8975 B0              | mov dword ptr ss:[ebp-50],esi              |
004024E0 | 8975 AC              | mov dword ptr ss:[ebp-54],esi              |
004024E3 | 8975 A8              | mov dword ptr ss:[ebp-58],esi              |
004024E6 | 8975 A4              | mov dword ptr ss:[ebp-5C],esi              |
004024E9 | 8975 A0              | mov dword ptr ss:[ebp-60],esi              |
004024EC | 8975 9C              | mov dword ptr ss:[ebp-64],esi              |
004024EF | 8975 98              | mov dword ptr ss:[ebp-68],esi              |
004024F2 | 8975 94              | mov dword ptr ss:[ebp-6C],esi              |
004024F5 | 8975 90              | mov dword ptr ss:[ebp-70],esi              |
004024F8 | 8975 8C              | mov dword ptr ss:[ebp-74],esi              |
004024FB | 8975 88              | mov dword ptr ss:[ebp-78],esi              |
004024FE | 8975 84              | mov dword ptr ss:[ebp-7C],esi              |
00402501 | 8975 80              | mov dword ptr ss:[ebp-80],esi              |
00402504 | 89B5 70FFFFFF        | mov dword ptr ss:[ebp-90],esi              |
0040250A | 89B5 60FFFFFF        | mov dword ptr ss:[ebp-A0],esi              |
00402510 | 89B5 50FFFFFF        | mov dword ptr ss:[ebp-B0],esi              |
00402516 | 89B5 40FFFFFF        | mov dword ptr ss:[ebp-C0],esi              |
0040251C | 89B5 30FFFFFF        | mov dword ptr ss:[ebp-D0],esi              |
00402522 | 89B5 20FFFFFF        | mov dword ptr ss:[ebp-E0],esi              |
00402528 | 89B5 10FFFFFF        | mov dword ptr ss:[ebp-F0],esi              |
0040252E | 89B5 00FFFFFF        | mov dword ptr ss:[ebp-100],esi             |
00402534 | 89B5 F0FEFFFF        | mov dword ptr ss:[ebp-110],esi             |
0040253A | 89B5 E0FEFFFF        | mov dword ptr ss:[ebp-120],esi             |
00402540 | 89B5 D0FEFFFF        | mov dword ptr ss:[ebp-130],esi             |
00402546 | 89B5 C0FEFFFF        | mov dword ptr ss:[ebp-140],esi             |
0040254C | 89B5 B0FEFFFF        | mov dword ptr ss:[ebp-150],esi             |
00402552 | 89B5 A0FEFFFF        | mov dword ptr ss:[ebp-160],esi             |
00402558 | 89B5 90FEFFFF        | mov dword ptr ss:[ebp-170],esi             |
0040255E | 89B5 80FEFFFF        | mov dword ptr ss:[ebp-180],esi             |
00402564 | 89B5 70FEFFFF        | mov dword ptr ss:[ebp-190],esi             |
0040256A | 89B5 60FEFFFF        | mov dword ptr ss:[ebp-1A0],esi             |
00402570 | 89B5 50FEFFFF        | mov dword ptr ss:[ebp-1B0],esi             |
00402576 | 89B5 30FEFFFF        | mov dword ptr ss:[ebp-1D0],esi             |
0040257C | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
0040257E | BA 601F4000          | mov edx,breaker's crackme # 3_patched.401F | edx:L"Hello", 401F60:L"ARE YOU CRAZY??? WRONG"
00402583 | 8D4D E8              | lea ecx,dword ptr ss:[ebp-18]              |
00402586 | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
00402588 | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:L"Hello", edi:"\\J@"
0040258A | 57                   | push edi                                   | edi:"\\J@"
0040258B | FF92 FC020000        | call dword ptr ds:[edx+2FC]                |
00402591 | 50                   | push eax                                   |
00402592 | 8D45 98              | lea eax,dword ptr ss:[ebp-68]              |
00402595 | 50                   | push eax                                   |
00402596 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
0040259C | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
0040259E | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]              | [ebp-34]: Name
004025A1 | 52                   | push edx                                   | edx:L"Hello"
004025A2 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004025A3 | 8B0B                 | mov ecx,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
004025A5 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                 |
004025AB | 3BC6                 | cmp eax,esi                                |
004025AD | DBE2                 | fnclex                                     |
004025AF | 7D 12                | jge breaker's crackme # 3_patched.4025C3   |
004025B1 | 68 A0000000          | push A0                                    |
004025B6 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
004025BB | 53                   | push ebx                                   | ebx:__vbaStrCopy
004025BC | 50                   | push eax                                   |
004025BD | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
004025C3 | 8B07                 | mov eax,dword ptr ds:[edi]                 | edi:"\\J@"
004025C5 | 57                   | push edi                                   | edi:"\\J@"
004025C6 | FF90 00030000        | call dword ptr ds:[eax+300]                |
004025CC | 8D4D 94              | lea ecx,dword ptr ss:[ebp-6C]              |
004025CF | 50                   | push eax                                   |
004025D0 | 51                   | push ecx                                   |
004025D1 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
004025D7 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
004025D9 | 8D45 BC              | lea eax,dword ptr ss:[ebp-44]              | [ebp-44]: Name2
004025DC | 50                   | push eax                                   |
004025DD | 53                   | push ebx                                   | ebx:__vbaStrCopy
004025DE | 8B13                 | mov edx,dword ptr ds:[ebx]                 | edx:L"Hello", ebx:__vbaStrCopy
004025E0 | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
004025E6 | 3BC6                 | cmp eax,esi                                |
004025E8 | DBE2                 | fnclex                                     |
004025EA | 7D 12                | jge breaker's crackme # 3_patched.4025FE   |
004025EC | 68 A0000000          | push A0                                    |
004025F1 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
004025F6 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004025F7 | 50                   | push eax                                   |
004025F8 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
004025FE | 8B0F                 | mov ecx,dword ptr ds:[edi]                 | edi:"\\J@"
00402600 | 57                   | push edi                                   | edi:"\\J@"
00402601 | FF91 04030000        | call dword ptr ds:[ecx+304]                |
00402607 | 8D55 84              | lea edx,dword ptr ss:[ebp-7C]              |
0040260A | 50                   | push eax                                   |
0040260B | 52                   | push edx                                   | edx:L"Hello"
0040260C | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402612 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
00402614 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]              | [ebp-54]: Age
00402617 | 51                   | push ecx                                   |
00402618 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402619 | 8B03                 | mov eax,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
0040261B | FF90 A0000000        | call dword ptr ds:[eax+A0]                 |
00402621 | 3BC6                 | cmp eax,esi                                |
00402623 | DBE2                 | fnclex                                     |
00402625 | 7D 12                | jge breaker's crackme # 3_patched.402639   |
00402627 | 68 A0000000          | push A0                                    |
0040262C | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
00402631 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402632 | 50                   | push eax                                   |
00402633 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
00402639 | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:L"Hello", edi:"\\J@"
0040263B | 57                   | push edi                                   | edi:"\\J@"
0040263C | FF92 FC020000        | call dword ptr ds:[edx+2FC]                |
00402642 | 50                   | push eax                                   |
00402643 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]              |
00402646 | 50                   | push eax                                   |
00402647 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
0040264D | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
0040264F | 8D55 DC              | lea edx,dword ptr ss:[ebp-24]              | [ebp-24]: Name
00402652 | 52                   | push edx                                   | edx:L"Hello"
00402653 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402654 | 8B0B                 | mov ecx,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
00402656 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                 |
0040265C | 3BC6                 | cmp eax,esi                                |
0040265E | DBE2                 | fnclex                                     |
00402660 | 7D 12                | jge breaker's crackme # 3_patched.402674   |
00402662 | 68 A0000000          | push A0                                    |
00402667 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
0040266C | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040266D | 50                   | push eax                                   |
0040266E | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
00402674 | 8B07                 | mov eax,dword ptr ds:[edi]                 | edi:"\\J@"
00402676 | 57                   | push edi                                   | edi:"\\J@"
00402677 | FF90 00030000        | call dword ptr ds:[eax+300]                |
0040267D | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]              |
00402680 | 50                   | push eax                                   |
00402681 | 51                   | push ecx                                   |
00402682 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402688 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
0040268A | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]              | [ebp-28]: Name2
0040268D | 50                   | push eax                                   |
0040268E | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040268F | 8B13                 | mov edx,dword ptr ds:[ebx]                 | edx:L"Hello", ebx:__vbaStrCopy
00402691 | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
00402697 | 3BC6                 | cmp eax,esi                                |
00402699 | DBE2                 | fnclex                                     |
0040269B | 7D 12                | jge breaker's crackme # 3_patched.4026AF   |
0040269D | 68 A0000000          | push A0                                    |
004026A2 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
004026A7 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004026A8 | 50                   | push eax                                   |
004026A9 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
004026AF | 8B0F                 | mov ecx,dword ptr ds:[edi]                 | edi:"\\J@"
004026B1 | 57                   | push edi                                   | edi:"\\J@"
004026B2 | FF91 04030000        | call dword ptr ds:[ecx+304]                |
004026B8 | 8D55 9C              | lea edx,dword ptr ss:[ebp-64]              |
004026BB | 50                   | push eax                                   |
004026BC | 52                   | push edx                                   | edx:L"Hello"
004026BD | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
004026C3 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
004026C5 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]              | [ebp-2C]: Age
004026C8 | 51                   | push ecx                                   |
004026C9 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004026CA | 8B03                 | mov eax,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
004026CC | FF90 A0000000        | call dword ptr ds:[eax+A0]                 |
004026D2 | 3BC6                 | cmp eax,esi                                |
004026D4 | DBE2                 | fnclex                                     |
004026D6 | 7D 12                | jge breaker's crackme # 3_patched.4026EA   |
004026D8 | 68 A0000000          | push A0                                    |
004026DD | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
004026E2 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004026E3 | 50                   | push eax                                   |
004026E4 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
004026EA | 8B45 CC              | mov eax,dword ptr ss:[ebp-34]              |
004026ED | 8D95 60FFFFFF        | lea edx,dword ptr ss:[ebp-A0]              |
004026F3 | 8985 78FFFFFF        | mov dword ptr ss:[ebp-88],eax              | [ebp-88]:GetWindowExtEx+59
004026F9 | 52                   | push edx                                   | length3
004026FA | 8D85 70FFFFFF        | lea eax,dword ptr ss:[ebp-90]              |
00402700 | 6A 02                | push 2                                     | index2
00402702 | 8D8D 50FFFFFF        | lea ecx,dword ptr ss:[ebp-B0]              |
00402708 | 50                   | push eax                                   | Name
00402709 | 51                   | push ecx                                   | store
0040270A | C785 68FFFFFF 030000 | mov dword ptr ss:[ebp-98],3                |
00402714 | C785 60FFFFFF 020000 | mov dword ptr ss:[ebp-A0],2                |
0040271E | 8975 CC              | mov dword ptr ss:[ebp-34],esi              |
00402721 | C785 70FFFFFF 080000 | mov dword ptr ss:[ebp-90],8                |
0040272B | FF15 44104000        | call dword ptr ds:[<Ordinal#632>]          |
00402731 | 8B55 D8              | mov edx,dword ptr ss:[ebp-28]              |
00402734 | 8B1D 10104000        | mov ebx,dword ptr ds:[<__vbaLenBstr>]      | ebx:__vbaStrCopy
0040273A | 52                   | push edx                                   | edx:L"Hello"
0040273B | FFD3                 | call ebx                                   | Name2.Length
0040273D | 8BD0                 | mov edx,eax                                | edx:L"Hello"
0040273F | 8B45 DC              | mov eax,dword ptr ss:[ebp-24]              |
00402742 | 50                   | push eax                                   |
00402743 | 8995 C8FDFFFF        | mov dword ptr ss:[ebp-238],edx             | name2len
00402749 | FFD3                 | call ebx                                   | Name.Length
0040274B | 8B9D C8FDFFFF        | mov ebx,dword ptr ss:[ebp-238]             | ebx:__vbaStrCopy, [ebp-238]:MinUserRequestViewHitTest+9AD44
00402751 | 8B4D D4              | mov ecx,dword ptr ss:[ebp-2C]              | [ebp-2C]: Age
00402754 | 0FAFD8               | imul ebx,eax                               | name2len*namelen
00402757 | 51                   | push ecx                                   |
00402758 | 0F80 CF070000        | jo breaker's crackme # 3_patched.402F2D    |
0040275E | FF15 10104000        | call dword ptr ds:[<__vbaLenBstr>]         | age.Length
00402764 | 0FAFD8               | imul ebx,eax                               | 前面len的*结果 再*agelen
00402767 | 0F80 C0070000        | jo breaker's crackme # 3_patched.402F2D    |
0040276D | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040276E | FF15 08104000        | call dword ptr ds:[<__vbaStrI4>]           | 计算结果转成对应十六进制的字符串
00402774 | 8B1D B0104000        | mov ebx,dword ptr ds:[<__vbaStrMove>]      | ebx:__vbaStrCopy
0040277A | 8BD0                 | mov edx,eax                                | edx:L"Hello"
0040277C | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]              |
0040277F | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
00402781 | 50                   | push eax                                   |
00402782 | 68 A41F4000          | push breaker's crackme # 3_patched.401FA4  | -
00402787 | FF15 24104000        | call dword ptr ds:[<__vbaStrCat>]          |
0040278D | 8BD0                 | mov edx,eax                                | edx:L"Hello"
0040278F | 8D4D C4              | lea ecx,dword ptr ss:[ebp-3C]              |
00402792 | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
00402794 | 50                   | push eax                                   | 末尾加了-的*结果
00402795 | 8D95 50FFFFFF        | lea edx,dword ptr ss:[ebp-B0]              | 前面取3位的内容
0040279B | 8D45 C8              | lea eax,dword ptr ss:[ebp-38]              |
0040279E | 52                   | push edx                                   | edx:L"Hello"
0040279F | 50                   | push eax                                   |
004027A0 | FF15 78104000        | call dword ptr ds:[<__vbaStrVarVal>]       |
004027A6 | 50                   | push eax                                   |
004027A7 | FF15 68104000        | call dword ptr ds:[<Ordinal#713>]          | 翻转三位结果
004027AD | 8BD0                 | mov edx,eax                                | edx:L"Hello"
004027AF | 8D4D C0              | lea ecx,dword ptr ss:[ebp-40]              |
004027B2 | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
004027B4 | 50                   | push eax                                   |
004027B5 | FF15 24104000        | call dword ptr ds:[<__vbaStrCat>]          | 组合
004027BB | 8985 18FFFFFF        | mov dword ptr ss:[ebp-E8],eax              |
004027C1 | B9 08000000          | mov ecx,8                                  |
004027C6 | B8 02000000          | mov eax,2                                  |
004027CB | 898D 10FFFFFF        | mov dword ptr ss:[ebp-F0],ecx              |
004027D1 | 8985 38FFFFFF        | mov dword ptr ss:[ebp-C8],eax              |
004027D7 | 8985 30FFFFFF        | mov dword ptr ss:[ebp-D0],eax              |
004027DD | 8B45 BC              | mov eax,dword ptr ss:[ebp-44]              | [ebp-44]: Name2
004027E0 | 898D 40FFFFFF        | mov dword ptr ss:[ebp-C0],ecx              |
004027E6 | 8D8D 30FFFFFF        | lea ecx,dword ptr ss:[ebp-D0]              |
004027EC | 8985 48FFFFFF        | mov dword ptr ss:[ebp-B8],eax              |
004027F2 | 51                   | push ecx                                   |
004027F3 | 8D95 40FFFFFF        | lea edx,dword ptr ss:[ebp-C0]              |
004027F9 | 6A 03                | push 3                                     |
004027FB | 8D85 20FFFFFF        | lea eax,dword ptr ss:[ebp-E0]              |
00402801 | 52                   | push edx                                   | edx:L"Hello"
00402802 | 50                   | push eax                                   |
00402803 | 8975 BC              | mov dword ptr ss:[ebp-44],esi              |
00402806 | FF15 44104000        | call dword ptr ds:[<Ordinal#632>]          | Name2 index3 取两位
0040280C | 8B0F                 | mov ecx,dword ptr ds:[edi]                 | edi:"\\J@"
0040280E | 57                   | push edi                                   | edi:"\\J@"
0040280F | FF91 FC020000        | call dword ptr ds:[ecx+2FC]                |
00402815 | 8D55 90              | lea edx,dword ptr ss:[ebp-70]              |
00402818 | 50                   | push eax                                   |
00402819 | 52                   | push edx                                   | edx:L"Hello"
0040281A | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402820 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
00402822 | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]              | [ebp-48]: Name
00402825 | 51                   | push ecx                                   |
00402826 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402827 | 8B03                 | mov eax,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
00402829 | FF90 A0000000        | call dword ptr ds:[eax+A0]                 |
0040282F | 3BC6                 | cmp eax,esi                                |
00402831 | DBE2                 | fnclex                                     |
00402833 | 7D 12                | jge breaker's crackme # 3_patched.402847   |
00402835 | 68 A0000000          | push A0                                    |
0040283A | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
0040283F | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402840 | 50                   | push eax                                   |
00402841 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
00402847 | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:L"Hello", edi:"\\J@"
00402849 | 57                   | push edi                                   | edi:"\\J@"
0040284A | FF92 00030000        | call dword ptr ds:[edx+300]                |
00402850 | 50                   | push eax                                   |
00402851 | 8D45 8C              | lea eax,dword ptr ss:[ebp-74]              |
00402854 | 50                   | push eax                                   |
00402855 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
0040285B | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
0040285D | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]              | [ebp-4C]: Name2
00402860 | 52                   | push edx                                   | edx:L"Hello"
00402861 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402862 | 8B0B                 | mov ecx,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
00402864 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                 |
0040286A | 3BC6                 | cmp eax,esi                                |
0040286C | DBE2                 | fnclex                                     |
0040286E | 7D 12                | jge breaker's crackme # 3_patched.402882   |
00402870 | 68 A0000000          | push A0                                    |
00402875 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
0040287A | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040287B | 50                   | push eax                                   |
0040287C | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
00402882 | 8B07                 | mov eax,dword ptr ds:[edi]                 | edi:"\\J@"
00402884 | 57                   | push edi                                   | edi:"\\J@"
00402885 | FF90 04030000        | call dword ptr ds:[eax+304]                |
0040288B | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]              |
0040288E | 50                   | push eax                                   |
0040288F | 51                   | push ecx                                   |
00402890 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402896 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
00402898 | 8D45 B0              | lea eax,dword ptr ss:[ebp-50]              | [ebp-50]: Age
0040289B | 50                   | push eax                                   |
0040289C | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040289D | 8B13                 | mov edx,dword ptr ds:[ebx]                 | edx:L"Hello", ebx:__vbaStrCopy
0040289F | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
004028A5 | 3BC6                 | cmp eax,esi                                |
004028A7 | DBE2                 | fnclex                                     |
004028A9 | 7D 12                | jge breaker's crackme # 3_patched.4028BD   |
004028AB | 68 A0000000          | push A0                                    |
004028B0 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
004028B5 | 53                   | push ebx                                   | ebx:__vbaStrCopy
004028B6 | 50                   | push eax                                   |
004028B7 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
004028BD | 8B4D B4              | mov ecx,dword ptr ss:[ebp-4C]              | [ebp-4C]: Name2
004028C0 | 8B1D 10104000        | mov ebx,dword ptr ds:[<__vbaLenBstr>]      | ebx:__vbaStrCopy
004028C6 | 51                   | push ecx                                   |
004028C7 | FFD3                 | call ebx                                   | Name2.Length
004028C9 | 8BD0                 | mov edx,eax                                | edx:L"Hello"
004028CB | 8B45 B8              | mov eax,dword ptr ss:[ebp-48]              |
004028CE | 50                   | push eax                                   |
004028CF | 8995 C4FDFFFF        | mov dword ptr ss:[ebp-23C],edx             |
004028D5 | FFD3                 | call ebx                                   | Name.Length
004028D7 | 8B9D C4FDFFFF        | mov ebx,dword ptr ss:[ebp-23C]             | ebx:__vbaStrCopy
004028DD | 8B4D B0              | mov ecx,dword ptr ss:[ebp-50]              | [ebp-50]: Age
004028E0 | 03D8                 | add ebx,eax                                | len name+ len name2
004028E2 | 51                   | push ecx                                   |
004028E3 | 0F80 44060000        | jo breaker's crackme # 3_patched.402F2D    |
004028E9 | FF15 10104000        | call dword ptr ds:[<__vbaLenBstr>]         |
004028EF | 03D8                 | add ebx,eax                                | 前面基础上+age len
004028F1 | 8B45 AC              | mov eax,dword ptr ss:[ebp-54]              |
004028F4 | B9 08000000          | mov ecx,8                                  |
004028F9 | 8D95 C0FEFFFF        | lea edx,dword ptr ss:[ebp-140]             |
004028FF | 8985 D8FEFFFF        | mov dword ptr ss:[ebp-128],eax             |
00402905 | 898D 50FEFFFF        | mov dword ptr ss:[ebp-1B0],ecx             |
0040290B | 898D D0FEFFFF        | mov dword ptr ss:[ebp-130],ecx             |
00402911 | 52                   | push edx                                   | edx:L"Hello"
00402912 | 8D85 D0FEFFFF        | lea eax,dword ptr ss:[ebp-130]             |
00402918 | 6A 02                | push 2                                     |
0040291A | 8D8D B0FEFFFF        | lea ecx,dword ptr ss:[ebp-150]             |
00402920 | 50                   | push eax                                   |
00402921 | 0F80 06060000        | jo breaker's crackme # 3_patched.402F2D    |
00402927 | 51                   | push ecx                                   |
00402928 | 899D 68FEFFFF        | mov dword ptr ss:[ebp-198],ebx             | ebx:__vbaStrCopy
0040292E | C785 60FEFFFF 030000 | mov dword ptr ss:[ebp-1A0],3               |
00402938 | C785 58FEFFFF A41F40 | mov dword ptr ss:[ebp-1A8],breaker's crack |
00402942 | C785 C8FEFFFF 010000 | mov dword ptr ss:[ebp-138],1               |
0040294C | C785 C0FEFFFF 020000 | mov dword ptr ss:[ebp-140],2               |
00402956 | 8975 AC              | mov dword ptr ss:[ebp-54],esi              |
00402959 | FF15 44104000        | call dword ptr ds:[<Ordinal#632>]          | age 从第二位开始取1位
0040295F | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:L"Hello", edi:"\\J@"
00402961 | 57                   | push edi                                   | edi:"\\J@"
00402962 | FF92 FC020000        | call dword ptr ds:[edx+2FC]                |
00402968 | 50                   | push eax                                   |
00402969 | 8D45 80              | lea eax,dword ptr ss:[ebp-80]              |
0040296C | 50                   | push eax                                   |
0040296D | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402973 | 8BD8                 | mov ebx,eax                                | ebx:__vbaStrCopy
00402975 | 8D55 A8              | lea edx,dword ptr ss:[ebp-58]              | [ebp-58]: Name
00402978 | 52                   | push edx                                   | edx:L"Hello"
00402979 | 53                   | push ebx                                   | ebx:__vbaStrCopy
0040297A | 8B0B                 | mov ecx,dword ptr ds:[ebx]                 | ebx:__vbaStrCopy
0040297C | FF91 A0000000        | call dword ptr ds:[ecx+A0]                 |
00402982 | 3BC6                 | cmp eax,esi                                |
00402984 | DBE2                 | fnclex                                     |
00402986 | 7D 12                | jge breaker's crackme # 3_patched.40299A   |
00402988 | 68 A0000000          | push A0                                    |
0040298D | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
00402992 | 53                   | push ebx                                   | ebx:__vbaStrCopy
00402993 | 50                   | push eax                                   |
00402994 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
0040299A | 8B45 A8              | mov eax,dword ptr ss:[ebp-58]              |
0040299D | 50                   | push eax                                   |
0040299E | FF15 10104000        | call dword ptr ds:[<__vbaLenBstr>]         | len name
004029A4 | 8B1D 7C104000        | mov ebx,dword ptr ds:[<__vbaVarCat>]       | ebx:__vbaStrCopy
004029AA | 8D8D 10FFFFFF        | lea ecx,dword ptr ss:[ebp-F0]              |
004029B0 | 8985 38FEFFFF        | mov dword ptr ss:[ebp-1C8],eax             |
004029B6 | 8D95 20FFFFFF        | lea edx,dword ptr ss:[ebp-E0]              |
004029BC | 51                   | push ecx                                   |
004029BD | 8D85 00FFFFFF        | lea eax,dword ptr ss:[ebp-100]             |
004029C3 | 52                   | push edx                                   | edx:L"Hello"
004029C4 | 50                   | push eax                                   |
004029C5 | C785 30FEFFFF 030000 | mov dword ptr ss:[ebp-1D0],3               |
004029CF | FFD3                 | call ebx                                   | 拼接: **-***+ name2后两位
004029D1 | 8D8D 60FEFFFF        | lea ecx,dword ptr ss:[ebp-1A0]             |
004029D7 | 50                   | push eax                                   |
004029D8 | 8D95 F0FEFFFF        | lea edx,dword ptr ss:[ebp-110]             |
004029DE | 51                   | push ecx                                   |
004029DF | 52                   | push edx                                   | edx:L"Hello"
004029E0 | FFD3                 | call ebx                                   | 拼接: 前面的结果+ 三个字符串长度求和
004029E2 | 50                   | push eax                                   |
004029E3 | 8D85 50FEFFFF        | lea eax,dword ptr ss:[ebp-1B0]             | -
004029E9 | 8D8D E0FEFFFF        | lea ecx,dword ptr ss:[ebp-120]             |
004029EF | 50                   | push eax                                   |
004029F0 | 51                   | push ecx                                   |
004029F1 | FFD3                 | call ebx                                   | 再拼个-
004029F3 | 50                   | push eax                                   |
004029F4 | 8D95 B0FEFFFF        | lea edx,dword ptr ss:[ebp-150]             |
004029FA | 8D85 A0FEFFFF        | lea eax,dword ptr ss:[ebp-160]             |
00402A00 | 52                   | push edx                                   | edx:L"Hello"
00402A01 | 50                   | push eax                                   |
00402A02 | FFD3                 | call ebx                                   | 再拼个Age第二位
00402A04 | 8D8D 30FEFFFF        | lea ecx,dword ptr ss:[ebp-1D0]             |
00402A0A | 50                   | push eax                                   |
00402A0B | 8D95 90FEFFFF        | lea edx,dword ptr ss:[ebp-170]             |
00402A11 | 51                   | push ecx                                   |
00402A12 | 52                   | push edx                                   | edx:L"Hello"
00402A13 | FFD3                 | call ebx                                   | 再拼个？
00402A15 | 50                   | push eax                                   |
00402A16 | FF15 0C104000        | call dword ptr ds:[<__vbaStrVarMove>]      |
00402A1C | 8BD0                 | mov edx,eax                                | edx:L"Hello"
00402A1E | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]              |
00402A21 | FF15 B0104000        | call dword ptr ds:[<__vbaStrMove>]         |
00402A27 | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]              | 清理工作
00402A2A | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]              |
00402A2D | 50                   | push eax                                   |
00402A2E | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]              |
00402A31 | 51                   | push ecx                                   |
00402A32 | 8D45 B8              | lea eax,dword ptr ss:[ebp-48]              |
00402A35 | 52                   | push edx                                   | edx:L"Hello"
00402A36 | 8D4D C0              | lea ecx,dword ptr ss:[ebp-40]              |
00402A39 | 50                   | push eax                                   |
00402A3A | 8D55 C4              | lea edx,dword ptr ss:[ebp-3C]              |
00402A3D | 51                   | push ecx                                   |
00402A3E | 8D45 C8              | lea eax,dword ptr ss:[ebp-38]              |
00402A41 | 52                   | push edx                                   | edx:L"Hello"
00402A42 | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]              |
00402A45 | 50                   | push eax                                   |
00402A46 | 8D55 D4              | lea edx,dword ptr ss:[ebp-2C]              |
00402A49 | 51                   | push ecx                                   |
00402A4A | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]              |
00402A4D | 52                   | push edx                                   | edx:L"Hello"
00402A4E | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]              |
00402A51 | 50                   | push eax                                   |
00402A52 | 51                   | push ecx                                   |
00402A53 | 6A 0B                | push B                                     |
00402A55 | FF15 98104000        | call dword ptr ds:[<__vbaFreeStrList>]     |
00402A5B | 8D55 80              | lea edx,dword ptr ss:[ebp-80]              |
00402A5E | 8D45 84              | lea eax,dword ptr ss:[ebp-7C]              |
00402A61 | 52                   | push edx                                   | edx:L"Hello"
00402A62 | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]              |
00402A65 | 50                   | push eax                                   |
00402A66 | 8D55 8C              | lea edx,dword ptr ss:[ebp-74]              |
00402A69 | 51                   | push ecx                                   |
00402A6A | 8D45 90              | lea eax,dword ptr ss:[ebp-70]              |
00402A6D | 52                   | push edx                                   | edx:L"Hello"
00402A6E | 8D4D 94              | lea ecx,dword ptr ss:[ebp-6C]              |
00402A71 | 50                   | push eax                                   |
00402A72 | 8D55 98              | lea edx,dword ptr ss:[ebp-68]              |
00402A75 | 51                   | push ecx                                   |
00402A76 | 52                   | push edx                                   | edx:L"Hello"
00402A77 | 8D45 9C              | lea eax,dword ptr ss:[ebp-64]              |
00402A7A | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]              |
00402A7D | 50                   | push eax                                   |
00402A7E | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]              |
00402A81 | 51                   | push ecx                                   |
00402A82 | 52                   | push edx                                   | edx:L"Hello"
00402A83 | 6A 0A                | push A                                     |
00402A85 | FF15 1C104000        | call dword ptr ds:[<__vbaFreeObjList>]     |
00402A8B | 83C4 5C              | add esp,5C                                 |
00402A8E | 8D85 90FEFFFF        | lea eax,dword ptr ss:[ebp-170]             |
00402A94 | 8D8D A0FEFFFF        | lea ecx,dword ptr ss:[ebp-160]             |
00402A9A | 8D95 B0FEFFFF        | lea edx,dword ptr ss:[ebp-150]             |
00402AA0 | 50                   | push eax                                   |
00402AA1 | 51                   | push ecx                                   |
00402AA2 | 8D85 E0FEFFFF        | lea eax,dword ptr ss:[ebp-120]             |
00402AA8 | 52                   | push edx                                   | edx:L"Hello"
00402AA9 | 8D8D C0FEFFFF        | lea ecx,dword ptr ss:[ebp-140]             |
00402AAF | 50                   | push eax                                   |
00402AB0 | 8D95 D0FEFFFF        | lea edx,dword ptr ss:[ebp-130]             |
00402AB6 | 51                   | push ecx                                   |
00402AB7 | 8D85 F0FEFFFF        | lea eax,dword ptr ss:[ebp-110]             |
00402ABD | 52                   | push edx                                   | edx:L"Hello"
00402ABE | 8D8D 00FFFFFF        | lea ecx,dword ptr ss:[ebp-100]             |
00402AC4 | 50                   | push eax                                   |
00402AC5 | 8D95 20FFFFFF        | lea edx,dword ptr ss:[ebp-E0]              |
00402ACB | 51                   | push ecx                                   |
00402ACC | 8D85 10FFFFFF        | lea eax,dword ptr ss:[ebp-F0]              |
00402AD2 | 8B1D 14104000        | mov ebx,dword ptr ds:[<__vbaFreeVarList>]  | ebx:__vbaStrCopy
00402AD8 | 52                   | push edx                                   | edx:L"Hello"
00402AD9 | 8D8D 30FFFFFF        | lea ecx,dword ptr ss:[ebp-D0]              |
00402ADF | 50                   | push eax                                   |
00402AE0 | 8D95 40FFFFFF        | lea edx,dword ptr ss:[ebp-C0]              |
00402AE6 | 51                   | push ecx                                   |
00402AE7 | 8D85 50FFFFFF        | lea eax,dword ptr ss:[ebp-B0]              |
00402AED | 52                   | push edx                                   | edx:L"Hello"
00402AEE | 8D8D 60FFFFFF        | lea ecx,dword ptr ss:[ebp-A0]              |
00402AF4 | 50                   | push eax                                   |
00402AF5 | 8D95 70FFFFFF        | lea edx,dword ptr ss:[ebp-90]              |
00402AFB | 51                   | push ecx                                   |
00402AFC | 52                   | push edx                                   | edx:L"Hello"
00402AFD | 6A 0F                | push F                                     |
00402AFF | FFD3                 | call ebx                                   | ebx:__vbaStrCopy
00402B01 | 8B07                 | mov eax,dword ptr ds:[edi]                 | edi:"\\J@"
00402B03 | 83C4 40              | add esp,40                                 |
00402B06 | 57                   | push edi                                   | edi:"\\J@"
00402B07 | FF90 08030000        | call dword ptr ds:[eax+308]                |
00402B0D | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]              |
00402B10 | 50                   | push eax                                   |
00402B11 | 51                   | push ecx                                   |
00402B12 | FF15 30104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402B18 | 8BF8                 | mov edi,eax                                | edi:"\\J@"
00402B1A | 8D45 DC              | lea eax,dword ptr ss:[ebp-24]              | [ebp-24]: Serial
00402B1D | 50                   | push eax                                   |
00402B1E | 57                   | push edi                                   | edi:"\\J@"
00402B1F | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:L"Hello", edi:"\\J@"
00402B21 | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
00402B27 | 3BC6                 | cmp eax,esi                                |
00402B29 | DBE2                 | fnclex                                     |
00402B2B | 7D 12                | jge breaker's crackme # 3_patched.402B3F   |
00402B2D | 68 A0000000          | push A0                                    |
00402B32 | 68 901F4000          | push breaker's crackme # 3_patched.401F90  |
00402B37 | 57                   | push edi                                   | edi:"\\J@"
00402B38 | 50                   | push eax                                   |
00402B39 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
00402B3F | 8B4D DC              | mov ecx,dword ptr ss:[ebp-24]              |
00402B42 | 8B55 E0              | mov edx,dword ptr ss:[ebp-20]              |
00402B45 | 51                   | push ecx                                   | serial
00402B46 | 52                   | push edx                                   | True Serial
00402B47 | FF15 50104000        | call dword ptr ds:[<__vbaStrCmp>]          |
00402B4D | 8BF8                 | mov edi,eax                                | edi:"\\J@"
00402B4F | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]              |
00402B52 | F7DF                 | neg edi                                    | edi:"\\J@"
00402B54 | 1BFF                 | sbb edi,edi                                | edi:"\\J@"
00402B56 | 47                   | inc edi                                    | edi:"\\J@"
00402B57 | F7DF                 | neg edi                                    | edi:"\\J@"
00402B59 | FF15 C0104000        | call dword ptr ds:[<__vbaFreeStr>]         |
00402B5F | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]              |
00402B62 | FF15 C4104000        | call dword ptr ds:[<__vbaFreeObj>]         |
00402B68 | 66:3BFE              | cmp di,si                                  | 最后的判断
00402B6B | 0F84 87000000        | je <breaker's crackme # 3_patched.Fail_Fak |
00402B71 | 8B3D A8104000        | mov edi,dword ptr ds:[<__vbaVarDup>]       | Success
```


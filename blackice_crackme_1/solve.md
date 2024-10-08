启用Check按钮：

```
1324h:00->01
```

计算Serial：

```c#
private static string? GenerateKey(string name)
{
	return string.Concat(name.Select(c => (c * name.Length).ToString("X")));
}
```

将Name的每一位字符*Name长度的十六进制值组合起来（大写）就是Serial了，另外Name至少4位

详情：

```assembly
00401E10 | 55                   | push ebp                                    | CheckBtn
00401E11 | 8BEC                 | mov ebp,esp                                 |
00401E13 | 83EC 0C              | sub esp,C                                   |
00401E16 | 68 E6104000          | push <JMP.&__vbaExceptHandler>              |
00401E1B | 64:A1 00000000       | mov eax,dword ptr fs:[0]                    |
00401E21 | 50                   | push eax                                    |
00401E22 | 64:8925 00000000     | mov dword ptr fs:[0],esp                    |
00401E29 | 81EC 34010000        | sub esp,134                                 |
00401E2F | 53                   | push ebx                                    |
00401E30 | 56                   | push esi                                    |
00401E31 | 57                   | push edi                                    |
00401E32 | 8965 F4              | mov dword ptr ss:[ebp-C],esp                |
00401E35 | C745 F8 D0104000     | mov dword ptr ss:[ebp-8],blackice crackme # |
00401E3C | 8B7D 08              | mov edi,dword ptr ss:[ebp+8]                |
00401E3F | 8BC7                 | mov eax,edi                                 |
00401E41 | 83E0 01              | and eax,1                                   |
00401E44 | 8945 FC              | mov dword ptr ss:[ebp-4],eax                |
00401E47 | 83E7 FE              | and edi,FFFFFFFE                            |
00401E4A | 57                   | push edi                                    |
00401E4B | 897D 08              | mov dword ptr ss:[ebp+8],edi                |
00401E4E | 8B0F                 | mov ecx,dword ptr ds:[edi]                  |
00401E50 | FF51 04              | call dword ptr ds:[ecx+4]                   |
00401E53 | 8B17                 | mov edx,dword ptr ds:[edi]                  |
00401E55 | 33F6                 | xor esi,esi                                 |
00401E57 | 57                   | push edi                                    |
00401E58 | 8975 DC              | mov dword ptr ss:[ebp-24],esi               |
00401E5B | 8975 CC              | mov dword ptr ss:[ebp-34],esi               | [ebp-34]:"Pj"
00401E5E | 8975 BC              | mov dword ptr ss:[ebp-44],esi               | [ebp-44]:int __stdcall DialogBox2(struct HWND__*, struct HWND__*, int, int)+1BA
00401E61 | 8975 AC              | mov dword ptr ss:[ebp-54],esi               |
00401E64 | 8975 9C              | mov dword ptr ss:[ebp-64],esi               | [ebp-64]:@HMValidateHandleNoSecure@8+47
00401E67 | 8975 8C              | mov dword ptr ss:[ebp-74],esi               |
00401E6A | 8975 88              | mov dword ptr ss:[ebp-78],esi               |
00401E6D | 8975 84              | mov dword ptr ss:[ebp-7C],esi               |
00401E70 | 8975 80              | mov dword ptr ss:[ebp-80],esi               |
00401E73 | 89B5 7CFFFFFF        | mov dword ptr ss:[ebp-84],esi               |
00401E79 | 89B5 6CFFFFFF        | mov dword ptr ss:[ebp-94],esi               | [ebp-94]:_PeekMessageW@20+1B1
00401E7F | 89B5 5CFFFFFF        | mov dword ptr ss:[ebp-A4],esi               |
00401E85 | 89B5 4CFFFFFF        | mov dword ptr ss:[ebp-B4],esi               | [ebp-B4]:CtfImeProcessKey+1960
00401E8B | 89B5 3CFFFFFF        | mov dword ptr ss:[ebp-C4],esi               |
00401E91 | 89B5 2CFFFFFF        | mov dword ptr ss:[ebp-D4],esi               |
00401E97 | 89B5 1CFFFFFF        | mov dword ptr ss:[ebp-E4],esi               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00401E9D | 89B5 DCFEFFFF        | mov dword ptr ss:[ebp-124],esi              |
00401EA3 | 89B5 CCFEFFFF        | mov dword ptr ss:[ebp-134],esi              |
00401EA9 | FF92 FC020000        | call dword ptr ds:[edx+2FC]                 |
00401EAF | 50                   | push eax                                    |
00401EB0 | 8D45 80              | lea eax,dword ptr ss:[ebp-80]               |
00401EB3 | 50                   | push eax                                    |
00401EB4 | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
00401EBA | 8BD8                 | mov ebx,eax                                 |
00401EBC | 8D55 88              | lea edx,dword ptr ss:[ebp-78]               |
00401EBF | 52                   | push edx                                    |
00401EC0 | 53                   | push ebx                                    |
00401EC1 | 8B0B                 | mov ecx,dword ptr ds:[ebx]                  |
00401EC3 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                  |
00401EC9 | 3BC6                 | cmp eax,esi                                 |
00401ECB | DBE2                 | fnclex                                      |
00401ECD | 7D 12                | jge blackice crackme #1_patched.401EE1      |
00401ECF | 68 A0000000          | push A0                                     |
00401ED4 | 68 681B4000          | push blackice crackme #1_patched.401B68     |
00401ED9 | 53                   | push ebx                                    |
00401EDA | 50                   | push eax                                    |
00401EDB | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00401EE1 | 8B45 88              | mov eax,dword ptr ss:[ebp-78]               | [ebp-78]: Name
00401EE4 | 50                   | push eax                                    |
00401EE5 | FF15 14104000        | call dword ptr ds:[<__vbaLenBstr>]          |
00401EEB | 33DB                 | xor ebx,ebx                                 |
00401EED | 83F8 04              | cmp eax,4                                   | Name长度需大于等于4
00401EF0 | 0F9CC3               | setl bl                                     |
00401EF3 | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]               |
00401EF6 | F7DB                 | neg ebx                                     |
00401EF8 | FF15 C4104000        | call dword ptr ds:[<__vbaFreeStr>]          |
00401EFE | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
00401F01 | FF15 C8104000        | call dword ptr ds:[<__vbaFreeObj>]          |
00401F07 | 66:3BDE              | cmp bx,si                                   |
00401F0A | 0F84 98000000        | je <blackice crackme #1_patched.Next#1>     |
00401F10 | B9 04000280          | mov ecx,80020004                            | Fail
00401F15 | B8 0A000000          | mov eax,A                                   | 0A:'\n'
00401F1A | 898D 44FFFFFF        | mov dword ptr ss:[ebp-BC],ecx               |
00401F20 | 898D 54FFFFFF        | mov dword ptr ss:[ebp-AC],ecx               |
00401F26 | 898D 64FFFFFF        | mov dword ptr ss:[ebp-9C],ecx               |
00401F2C | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
00401F32 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00401F38 | 8985 3CFFFFFF        | mov dword ptr ss:[ebp-C4],eax               |
00401F3E | 8985 4CFFFFFF        | mov dword ptr ss:[ebp-B4],eax               | [ebp-B4]:CtfImeProcessKey+1960
00401F44 | 8985 5CFFFFFF        | mov dword ptr ss:[ebp-A4],eax               |
00401F4A | C785 34FFFFFF 7C1B40 | mov dword ptr ss:[ebp-CC],blackice crackme  | 401B7C:L"more than 4 lettters please"
00401F54 | C785 2CFFFFFF 080000 | mov dword ptr ss:[ebp-D4],8                 |
00401F5E | FF15 A8104000        | call dword ptr ds:[<__vbaVarDup>]           |
00401F64 | 8D8D 3CFFFFFF        | lea ecx,dword ptr ss:[ebp-C4]               |
00401F6A | 8D95 4CFFFFFF        | lea edx,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
00401F70 | 51                   | push ecx                                    |
00401F71 | 8D85 5CFFFFFF        | lea eax,dword ptr ss:[ebp-A4]               |
00401F77 | 52                   | push edx                                    |
00401F78 | 50                   | push eax                                    |
00401F79 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00401F7F | 56                   | push esi                                    |
00401F80 | 51                   | push ecx                                    |
00401F81 | FF15 3C104000        | call dword ptr ds:[<Ordinal#595>]           |
00401F87 | 8D95 3CFFFFFF        | lea edx,dword ptr ss:[ebp-C4]               |
00401F8D | 8D85 4CFFFFFF        | lea eax,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
00401F93 | 52                   | push edx                                    |
00401F94 | 8D8D 5CFFFFFF        | lea ecx,dword ptr ss:[ebp-A4]               |
00401F9A | 50                   | push eax                                    |
00401F9B | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00401FA1 | 51                   | push ecx                                    |
00401FA2 | 52                   | push edx                                    |
00401FA3 | E9 C1040000          | jmp blackice crackme #1_patched.402469      |
00401FA8 | 8B07                 | mov eax,dword ptr ds:[edi]                  |
00401FAA | 57                   | push edi                                    |
00401FAB | FF90 FC020000        | call dword ptr ds:[eax+2FC]                 |
00401FB1 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
00401FB4 | 50                   | push eax                                    |
00401FB5 | 51                   | push ecx                                    |
00401FB6 | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
00401FBC | 8BD8                 | mov ebx,eax                                 |
00401FBE | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
00401FC1 | 50                   | push eax                                    |
00401FC2 | 53                   | push ebx                                    |
00401FC3 | 8B13                 | mov edx,dword ptr ds:[ebx]                  |
00401FC5 | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
00401FCB | 3BC6                 | cmp eax,esi                                 |
00401FCD | DBE2                 | fnclex                                      |
00401FCF | 7D 12                | jge blackice crackme #1_patched.401FE3      |
00401FD1 | 68 A0000000          | push A0                                     |
00401FD6 | 68 681B4000          | push blackice crackme #1_patched.401B68     |
00401FDB | 53                   | push ebx                                    |
00401FDC | 50                   | push eax                                    |
00401FDD | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00401FE3 | 8B4D 88              | mov ecx,dword ptr ss:[ebp-78]               | [ebp-78]: Name
00401FE6 | 51                   | push ecx                                    |
00401FE7 | FF15 14104000        | call dword ptr ds:[<__vbaLenBstr>]          |
00401FED | 8B1D 0C104000        | mov ebx,dword ptr ds:[<__vbaVarMove>]       |
00401FF3 | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
00401FF9 | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]               | -34:length
00401FFC | 8985 34FFFFFF        | mov dword ptr ss:[ebp-CC],eax               |
00402002 | C785 2CFFFFFF 030000 | mov dword ptr ss:[ebp-D4],3                 |
0040200C | FFD3                 | call ebx                                    | ebx:__vbaVarMove
0040200E | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]               |
00402011 | FF15 C4104000        | call dword ptr ds:[<__vbaFreeStr>]          |
00402017 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
0040201A | FF15 C8104000        | call dword ptr ds:[<__vbaFreeObj>]          |
00402020 | B8 02000000          | mov eax,2                                   |
00402025 | B9 01000000          | mov ecx,1                                   |
0040202A | 8985 2CFFFFFF        | mov dword ptr ss:[ebp-D4],eax               |
00402030 | 8985 1CFFFFFF        | mov dword ptr ss:[ebp-E4],eax               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402036 | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
0040203C | 898D 34FFFFFF        | mov dword ptr ss:[ebp-CC],ecx               |
00402042 | 898D 24FFFFFF        | mov dword ptr ss:[ebp-DC],ecx               |
00402048 | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]               | length
0040204B | 52                   | push edx                                    |
0040204C | 8D8D 1CFFFFFF        | lea ecx,dword ptr ss:[ebp-E4]               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402052 | 50                   | push eax                                    |
00402053 | 8D95 CCFEFFFF        | lea edx,dword ptr ss:[ebp-134]              |
00402059 | 51                   | push ecx                                    |
0040205A | 8D85 DCFEFFFF        | lea eax,dword ptr ss:[ebp-124]              |
00402060 | 52                   | push edx                                    |
00402061 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]               |
00402064 | 50                   | push eax                                    |
00402065 | 51                   | push ecx                                    |
00402066 | FF15 34104000        | call dword ptr ds:[<__vbaVarForInit>]       |
0040206C | 3BC6                 | cmp eax,esi                                 | Loop eax init 1
0040206E | 0F84 3E020000        | je <blackice crackme #1_patched.Next#2>     |
00402074 | 8B17                 | mov edx,dword ptr ds:[edi]                  |
00402076 | 57                   | push edi                                    |
00402077 | FF92 FC020000        | call dword ptr ds:[edx+2FC]                 |
0040207D | 50                   | push eax                                    |
0040207E | 8D45 80              | lea eax,dword ptr ss:[ebp-80]               |
00402081 | 50                   | push eax                                    |
00402082 | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
00402088 | 8BF0                 | mov esi,eax                                 |
0040208A | 8D55 88              | lea edx,dword ptr ss:[ebp-78]               | [ebp-78]:Name
0040208D | 52                   | push edx                                    |
0040208E | 56                   | push esi                                    |
0040208F | 8B0E                 | mov ecx,dword ptr ds:[esi]                  |
00402091 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                  |
00402097 | 85C0                 | test eax,eax                                |
00402099 | DBE2                 | fnclex                                      |
0040209B | 7D 12                | jge blackice crackme #1_patched.4020AF      |
0040209D | 68 A0000000          | push A0                                     |
004020A2 | 68 681B4000          | push blackice crackme #1_patched.401B68     |
004020A7 | 56                   | push esi                                    |
004020A8 | 50                   | push eax                                    |
004020A9 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004020AF | 8B07                 | mov eax,dword ptr ds:[edi]                  |
004020B1 | 57                   | push edi                                    |
004020B2 | FF90 FC020000        | call dword ptr ds:[eax+2FC]                 |
004020B8 | 8D8D 7CFFFFFF        | lea ecx,dword ptr ss:[ebp-84]               |
004020BE | 50                   | push eax                                    |
004020BF | 51                   | push ecx                                    |
004020C0 | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
004020C6 | 8BF0                 | mov esi,eax                                 |
004020C8 | 8D45 84              | lea eax,dword ptr ss:[ebp-7C]               | [ebp-7C]:Name
004020CB | 50                   | push eax                                    |
004020CC | 56                   | push esi                                    |
004020CD | 8B16                 | mov edx,dword ptr ds:[esi]                  |
004020CF | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
004020D5 | 85C0                 | test eax,eax                                |
004020D7 | DBE2                 | fnclex                                      |
004020D9 | 7D 12                | jge blackice crackme #1_patched.4020ED      |
004020DB | 68 A0000000          | push A0                                     |
004020E0 | 68 681B4000          | push blackice crackme #1_patched.401B68     |
004020E5 | 56                   | push esi                                    |
004020E6 | 50                   | push eax                                    |
004020E7 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004020ED | 8B4D 84              | mov ecx,dword ptr ss:[ebp-7C]               |
004020F0 | 51                   | push ecx                                    |
004020F1 | FF15 14104000        | call dword ptr ds:[<__vbaLenBstr>]          |
004020F7 | 8985 34FFFFFF        | mov dword ptr ss:[ebp-CC],eax               | -CC:length
004020FD | 8B45 88              | mov eax,dword ptr ss:[ebp-78]               |
00402100 | 8985 54FFFFFF        | mov dword ptr ss:[ebp-AC],eax               |
00402106 | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               | -D4:length var
0040210C | 8D45 AC              | lea eax,dword ptr ss:[ebp-54]               |
0040210F | 52                   | push edx                                    |
00402110 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402116 | 50                   | push eax                                    |
00402117 | 51                   | push ecx                                    |
00402118 | C785 2CFFFFFF 030000 | mov dword ptr ss:[ebp-D4],3                 |
00402122 | C785 24FFFFFF 010000 | mov dword ptr ss:[ebp-DC],1                 |
0040212C | C785 1CFFFFFF 020000 | mov dword ptr ss:[ebp-E4],2                 | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402136 | C745 88 00000000     | mov dword ptr ss:[ebp-78],0                 |
0040213D | C785 4CFFFFFF 080000 | mov dword ptr ss:[ebp-B4],8                 | [ebp-B4]:CtfImeProcessKey+1960
00402147 | FF15 00104000        | call dword ptr ds:[<__vbaVarSub>]           | length-index
0040214D | 50                   | push eax                                    |
0040214E | 8D95 1CFFFFFF        | lea edx,dword ptr ss:[ebp-E4]               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402154 | 8D85 5CFFFFFF        | lea eax,dword ptr ss:[ebp-A4]               |
0040215A | 52                   | push edx                                    |
0040215B | 50                   | push eax                                    |
0040215C | FF15 A4104000        | call dword ptr ds:[<__vbaVarAdd>]           | length+1
00402162 | 50                   | push eax                                    |
00402163 | FF15 A0104000        | call dword ptr ds:[<__vbaI4Var>]            |
00402169 | 8D8D 4CFFFFFF        | lea ecx,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
0040216F | 50                   | push eax                                    | length
00402170 | 8D95 3CFFFFFF        | lea edx,dword ptr ss:[ebp-C4]               |
00402176 | 51                   | push ecx                                    | Name
00402177 | 52                   | push edx                                    |
00402178 | FF15 B0104000        | call dword ptr ds:[<Ordinal#619>]           | right(Name,length)
0040217E | 8D95 3CFFFFFF        | lea edx,dword ptr ss:[ebp-C4]               |
00402184 | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]               |
00402187 | FFD3                 | call ebx                                    |
00402189 | 8D4D 84              | lea ecx,dword ptr ss:[ebp-7C]               |
0040218C | FF15 C4104000        | call dword ptr ds:[<__vbaFreeStr>]          |
00402192 | 8D85 7CFFFFFF        | lea eax,dword ptr ss:[ebp-84]               |
00402198 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
0040219B | 50                   | push eax                                    |
0040219C | 51                   | push ecx                                    |
0040219D | 6A 02                | push 2                                      |
0040219F | FF15 20104000        | call dword ptr ds:[<__vbaFreeObjList>]      |
004021A5 | 8D95 5CFFFFFF        | lea edx,dword ptr ss:[ebp-A4]               |
004021AB | 8D85 4CFFFFFF        | lea eax,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
004021B1 | 52                   | push edx                                    |
004021B2 | 50                   | push eax                                    |
004021B3 | 6A 02                | push 2                                      |
004021B5 | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
004021BB | 8B0F                 | mov ecx,dword ptr ds:[edi]                  |
004021BD | 83C4 18              | add esp,18                                  |
004021C0 | 57                   | push edi                                    |
004021C1 | FF91 FC020000        | call dword ptr ds:[ecx+2FC]                 |
004021C7 | 8D55 80              | lea edx,dword ptr ss:[ebp-80]               |
004021CA | 50                   | push eax                                    |
004021CB | 52                   | push edx                                    |
004021CC | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
004021D2 | 8BF0                 | mov esi,eax                                 |
004021D4 | 8D4D 84              | lea ecx,dword ptr ss:[ebp-7C]               | [ebp-7C]:Name
004021D7 | 51                   | push ecx                                    |
004021D8 | 56                   | push esi                                    |
004021D9 | 8B06                 | mov eax,dword ptr ds:[esi]                  |
004021DB | FF90 A0000000        | call dword ptr ds:[eax+A0]                  |
004021E1 | 85C0                 | test eax,eax                                |
004021E3 | DBE2                 | fnclex                                      |
004021E5 | 7D 12                | jge blackice crackme #1_patched.4021F9      |
004021E7 | 68 A0000000          | push A0                                     |
004021EC | 68 681B4000          | push blackice crackme #1_patched.401B68     |
004021F1 | 56                   | push esi                                    |
004021F2 | 50                   | push eax                                    |
004021F3 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004021F9 | 8D55 DC              | lea edx,dword ptr ss:[ebp-24]               | 前面right结果
004021FC | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
004021FF | 52                   | push edx                                    |
00402200 | 50                   | push eax                                    |
00402201 | FF15 78104000        | call dword ptr ds:[<__vbaStrVarVal>]        |
00402207 | 50                   | push eax                                    |
00402208 | FF15 24104000        | call dword ptr ds:[<Ordinal#516>]           | name[0] ASCII
0040220E | 8B4D 84              | mov ecx,dword ptr ss:[ebp-7C]               |
00402211 | 51                   | push ecx                                    |
00402212 | 0FBFF0               | movsx esi,ax                                |
00402215 | FF15 14104000        | call dword ptr ds:[<__vbaLenBstr>]          |
0040221B | 0FAFF0               | imul esi,eax                                | length*asc
0040221E | 0F80 02030000        | jo <blackice crackme #1_patched.ErrOverflow |
00402224 | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
0040222A | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               | result
0040222D | 89B5 34FFFFFF        | mov dword ptr ss:[ebp-CC],esi               |
00402233 | C785 2CFFFFFF 030000 | mov dword ptr ss:[ebp-D4],3                 |
0040223D | FFD3                 | call ebx                                    |
0040223F | 8D55 84              | lea edx,dword ptr ss:[ebp-7C]               |
00402242 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
00402245 | 52                   | push edx                                    |
00402246 | 50                   | push eax                                    |
00402247 | 6A 02                | push 2                                      |
00402249 | FF15 90104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
0040224F | 83C4 0C              | add esp,C                                   |
00402252 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
00402255 | FF15 C8104000        | call dword ptr ds:[<__vbaFreeObj>]          |
0040225B | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               | result
0040225E | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402264 | 51                   | push ecx                                    |
00402265 | 52                   | push edx                                    |
00402266 | FF15 8C104000        | call dword ptr ds:[<Ordinal#573>]           | HEX(result)
0040226C | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402272 | 8D4D BC              | lea ecx,dword ptr ss:[ebp-44]               | [ebp-44]:int __stdcall DialogBox2(struct HWND__*, struct HWND__*, int, int)+1BA
00402275 | FFD3                 | call ebx                                    | 读取上次的值
00402277 | 8D45 8C              | lea eax,dword ptr ss:[ebp-74]               | old
0040227A | 8D4D BC              | lea ecx,dword ptr ss:[ebp-44]               | newh
0040227D | 50                   | push eax                                    |
0040227E | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | storage
00402284 | 51                   | push ecx                                    |
00402285 | 52                   | push edx                                    |
00402286 | FF15 A4104000        | call dword ptr ds:[<__vbaVarAdd>]           | old+newh
0040228C | 8BD0                 | mov edx,eax                                 |
0040228E | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               |
00402291 | FFD3                 | call ebx                                    | ebp-74+=h
00402293 | 8D85 CCFEFFFF        | lea eax,dword ptr ss:[ebp-134]              |
00402299 | 8D8D DCFEFFFF        | lea ecx,dword ptr ss:[ebp-124]              |
0040229F | 50                   | push eax                                    |
004022A0 | 8D55 AC              | lea edx,dword ptr ss:[ebp-54]               |
004022A3 | 51                   | push ecx                                    |
004022A4 | 52                   | push edx                                    |
004022A5 | FF15 BC104000        | call dword ptr ds:[<__vbaVarForNext>]       |
004022AB | 33F6                 | xor esi,esi                                 |
004022AD | E9 BAFDFFFF          | jmp blackice crackme #1_patched.40206C      | NextLoop
004022B2 | 8B07                 | mov eax,dword ptr ds:[edi]                  |
004022B4 | 57                   | push edi                                    |
004022B5 | FF90 00030000        | call dword ptr ds:[eax+300]                 |
004022BB | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
004022BE | 50                   | push eax                                    |
004022BF | 51                   | push ecx                                    |
004022C0 | FF15 38104000        | call dword ptr ds:[<__vbaObjSet>]           |
004022C6 | 8BF8                 | mov edi,eax                                 |
004022C8 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               | [ebp-78]: UserInput Serial
004022CB | 50                   | push eax                                    |
004022CC | 57                   | push edi                                    |
004022CD | 8B17                 | mov edx,dword ptr ds:[edi]                  |
004022CF | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
004022D5 | 3BC6                 | cmp eax,esi                                 |
004022D7 | DBE2                 | fnclex                                      |
004022D9 | 7D 12                | jge blackice crackme #1_patched.4022ED      |
004022DB | 68 A0000000          | push A0                                     |
004022E0 | 68 681B4000          | push blackice crackme #1_patched.401B68     |
004022E5 | 57                   | push edi                                    |
004022E6 | 50                   | push eax                                    |
004022E7 | FF15 2C104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004022ED | 8B45 88              | mov eax,dword ptr ss:[ebp-78]               |
004022F0 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
004022F6 | 8D55 8C              | lea edx,dword ptr ss:[ebp-74]               |
004022F9 | 51                   | push ecx                                    | UserInput Serial
004022FA | 52                   | push edx                                    | True Serial
004022FB | 8975 88              | mov dword ptr ss:[ebp-78],esi               |
004022FE | 8985 74FFFFFF        | mov dword ptr ss:[ebp-8C],eax               |
00402304 | C785 6CFFFFFF 088000 | mov dword ptr ss:[ebp-94],8008              | [ebp-94]:_PeekMessageW@20+1B1
0040230E | FF15 54104000        | call dword ptr ds:[<__vbaVarTstEq>]         | cmp
00402314 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
00402317 | 8BF8                 | mov edi,eax                                 |
00402319 | FF15 C8104000        | call dword ptr ds:[<__vbaFreeObj>]          |
0040231F | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402325 | FF15 10104000        | call dword ptr ds:[<__vbaFreeVar>]          |
0040232B | B9 04000280          | mov ecx,80020004                            |
00402330 | B8 0A000000          | mov eax,A                                   | 0A:'\n'
00402335 | 66:3BFE              | cmp di,si                                   | serial检查
00402338 | 898D 44FFFFFF        | mov dword ptr ss:[ebp-BC],ecx               |
0040233E | 8985 3CFFFFFF        | mov dword ptr ss:[ebp-C4],eax               |
00402344 | 898D 54FFFFFF        | mov dword ptr ss:[ebp-AC],ecx               |
0040234A | 8985 4CFFFFFF        | mov dword ptr ss:[ebp-B4],eax               | [ebp-B4]:CtfImeProcessKey+1960
00402350 | 0F84 8C000000        | je <blackice crackme #1_patched.Fail>       |
00402356 | 8B3D A8104000        | mov edi,dword ptr ds:[<__vbaVarDup>]        | Success
0040235C | BB 08000000          | mov ebx,8                                   |
00402361 | 8D95 1CFFFFFF        | lea edx,dword ptr ss:[ebp-E4]               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402367 | 8D8D 5CFFFFFF        | lea ecx,dword ptr ss:[ebp-A4]               |
0040236D | C785 24FFFFFF D01B40 | mov dword ptr ss:[ebp-DC],blackice crackme  | 401BD0:L"Congrats"
00402377 | 899D 1CFFFFFF        | mov dword ptr ss:[ebp-E4],ebx               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
0040237D | FFD7                 | call edi                                    |
0040237F | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
00402385 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
0040238B | C785 34FFFFFF B81B40 | mov dword ptr ss:[ebp-CC],blackice crackme  | 401BB8:L"WELL DONE"
00402395 | 899D 2CFFFFFF        | mov dword ptr ss:[ebp-D4],ebx               |
0040239B | FFD7                 | call edi                                    |
0040239D | 8D85 3CFFFFFF        | lea eax,dword ptr ss:[ebp-C4]               |
004023A3 | 8D8D 4CFFFFFF        | lea ecx,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
004023A9 | 50                   | push eax                                    |
004023AA | 8D95 5CFFFFFF        | lea edx,dword ptr ss:[ebp-A4]               |
004023B0 | 51                   | push ecx                                    |
004023B1 | 52                   | push edx                                    |
004023B2 | 8D85 6CFFFFFF        | lea eax,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
004023B8 | 6A 40                | push 40                                     |
004023BA | 50                   | push eax                                    |
004023BB | FF15 3C104000        | call dword ptr ds:[<Ordinal#595>]           |
004023C1 | 8D8D 3CFFFFFF        | lea ecx,dword ptr ss:[ebp-C4]               |
004023C7 | 8D95 4CFFFFFF        | lea edx,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
004023CD | 51                   | push ecx                                    |
004023CE | 8D85 5CFFFFFF        | lea eax,dword ptr ss:[ebp-A4]               |
004023D4 | 52                   | push edx                                    |
004023D5 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
004023DB | 50                   | push eax                                    |
004023DC | 51                   | push ecx                                    |
004023DD | E9 87000000          | jmp blackice crackme #1_patched.402469      |
004023E2 | 8B3D A8104000        | mov edi,dword ptr ds:[<__vbaVarDup>]        | Fail
004023E8 | BB 08000000          | mov ebx,8                                   |
004023ED | 8D95 1CFFFFFF        | lea edx,dword ptr ss:[ebp-E4]               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
004023F3 | 8D8D 5CFFFFFF        | lea ecx,dword ptr ss:[ebp-A4]               |
004023F9 | C785 24FFFFFF 101C40 | mov dword ptr ss:[ebp-DC],blackice crackme  | 401C10:L"ARGH"
00402403 | 899D 1CFFFFFF        | mov dword ptr ss:[ebp-E4],ebx               | [ebp-E4]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00402409 | FFD7                 | call edi                                    |
0040240B | 8D95 2CFFFFFF        | lea edx,dword ptr ss:[ebp-D4]               |
00402411 | 8D8D 6CFFFFFF        | lea ecx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402417 | C785 34FFFFFF E81B40 | mov dword ptr ss:[ebp-CC],blackice crackme  | 401BE8:L"Sorry, try again"
00402421 | 899D 2CFFFFFF        | mov dword ptr ss:[ebp-D4],ebx               |
00402427 | FFD7                 | call edi                                    |
00402429 | 8D95 3CFFFFFF        | lea edx,dword ptr ss:[ebp-C4]               |
0040242F | 8D85 4CFFFFFF        | lea eax,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
00402435 | 52                   | push edx                                    |
00402436 | 8D8D 5CFFFFFF        | lea ecx,dword ptr ss:[ebp-A4]               |
0040243C | 50                   | push eax                                    |
0040243D | 51                   | push ecx                                    |
0040243E | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402444 | 6A 10                | push 10                                     |
00402446 | 52                   | push edx                                    |
00402447 | FF15 3C104000        | call dword ptr ds:[<Ordinal#595>]           |
0040244D | 8D85 3CFFFFFF        | lea eax,dword ptr ss:[ebp-C4]               |
00402453 | 8D8D 4CFFFFFF        | lea ecx,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
00402459 | 50                   | push eax                                    |
0040245A | 8D95 5CFFFFFF        | lea edx,dword ptr ss:[ebp-A4]               |
00402460 | 51                   | push ecx                                    |
00402461 | 8D85 6CFFFFFF        | lea eax,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
00402467 | 52                   | push edx                                    |
00402468 | 50                   | push eax                                    |
00402469 | 6A 04                | push 4                                      |
0040246B | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
00402471 | 83C4 14              | add esp,14                                  |
00402474 | 8975 FC              | mov dword ptr ss:[ebp-4],esi                |
00402477 | 68 07254000          | push blackice crackme #1_patched.402507     |
0040247C | EB 4B                | jmp blackice crackme #1_patched.4024C9      |
0040247E | 8D4D 84              | lea ecx,dword ptr ss:[ebp-7C]               |
00402481 | 8D55 88              | lea edx,dword ptr ss:[ebp-78]               |
00402484 | 51                   | push ecx                                    |
00402485 | 52                   | push edx                                    |
00402486 | 6A 02                | push 2                                      |
00402488 | FF15 90104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
0040248E | 8D85 7CFFFFFF        | lea eax,dword ptr ss:[ebp-84]               |
00402494 | 8D4D 80              | lea ecx,dword ptr ss:[ebp-80]               |
00402497 | 50                   | push eax                                    |
00402498 | 51                   | push ecx                                    |
00402499 | 6A 02                | push 2                                      |
0040249B | FF15 20104000        | call dword ptr ds:[<__vbaFreeObjList>]      |
004024A1 | 8D95 3CFFFFFF        | lea edx,dword ptr ss:[ebp-C4]               |
004024A7 | 8D85 4CFFFFFF        | lea eax,dword ptr ss:[ebp-B4]               | [ebp-B4]:CtfImeProcessKey+1960
004024AD | 52                   | push edx                                    |
004024AE | 8D8D 5CFFFFFF        | lea ecx,dword ptr ss:[ebp-A4]               |
004024B4 | 50                   | push eax                                    |
004024B5 | 8D95 6CFFFFFF        | lea edx,dword ptr ss:[ebp-94]               | [ebp-94]:_PeekMessageW@20+1B1
004024BB | 51                   | push ecx                                    |
004024BC | 52                   | push edx                                    |
004024BD | 6A 04                | push 4                                      |
004024BF | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
004024C5 | 83C4 2C              | add esp,2C                                  |
004024C8 | C3                   | ret                                         |
004024C9 | 8D85 CCFEFFFF        | lea eax,dword ptr ss:[ebp-134]              |
004024CF | 8D8D DCFEFFFF        | lea ecx,dword ptr ss:[ebp-124]              |
004024D5 | 50                   | push eax                                    |
004024D6 | 51                   | push ecx                                    |
004024D7 | 6A 02                | push 2                                      |
004024D9 | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
004024DF | 8B35 10104000        | mov esi,dword ptr ds:[<__vbaFreeVar>]       |
004024E5 | 83C4 0C              | add esp,C                                   |
004024E8 | 8D4D DC              | lea ecx,dword ptr ss:[ebp-24]               |
004024EB | FFD6                 | call esi                                    |
004024ED | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]               | [ebp-34]:"Pj"
004024F0 | FFD6                 | call esi                                    |
004024F2 | 8D4D BC              | lea ecx,dword ptr ss:[ebp-44]               | [ebp-44]:int __stdcall DialogBox2(struct HWND__*, struct HWND__*, int, int)+1BA
004024F5 | FFD6                 | call esi                                    |
004024F7 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]               |
004024FA | FFD6                 | call esi                                    |
004024FC | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               | [ebp-64]:@HMValidateHandleNoSecure@8+47
004024FF | FFD6                 | call esi                                    |
00402501 | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               |
00402504 | FFD6                 | call esi                                    |
00402506 | C3                   | ret                                         |
00402507 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                |
0040250A | 50                   | push eax                                    |
0040250B | 8B10                 | mov edx,dword ptr ds:[eax]                  |
0040250D | FF52 08              | call dword ptr ds:[edx+8]                   |
00402510 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                |
00402513 | 8B4D EC              | mov ecx,dword ptr ss:[ebp-14]               |
00402516 | 5F                   | pop edi                                     |
00402517 | 5E                   | pop esi                                     |
00402518 | 64:890D 00000000     | mov dword ptr fs:[0],ecx                    |
0040251F | 5B                   | pop ebx                                     |
00402520 | 8BE5                 | mov esp,ebp                                 |
00402522 | 5D                   | pop ebp                                     |
00402523 | C2 0400              | ret 4                                       |
00402526 | FF15 80104000        | call dword ptr ds:[<__vbaErrorOverflow>]    |
```


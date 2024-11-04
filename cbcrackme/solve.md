找密码

先上答案：`315751288`

细节：

```
0040DF6 | 55                   | push ebp                                   |
0040DF6 | 8BEC                 | mov ebp,esp                                |
0040DF6 | 83EC 0C              | sub esp,C                                  |
0040DF6 | 68 56104000          | push <JMP.&__vbaExceptHandler>             |
0040DF6 | 64:A1 00000000       | mov eax,dword ptr fs:[0]                   |
0040DF7 | 50                   | push eax                                   |
0040DF7 | 64:8925 00000000     | mov dword ptr fs:[0],esp                   |
0040DF7 | 81EC B4000000        | sub esp,B4                                 |
0040DF7 | 53                   | push ebx                                   |
0040DF8 | 8B5D 08              | mov ebx,dword ptr ss:[ebp+8]               |
0040DF8 | 8BC3                 | mov eax,ebx                                |
0040DF8 | 56                   | push esi                                   | esi:"€7\rt校\ft"
0040DF8 | 83E3 FE              | and ebx,FFFFFFFE                           |
0040DF8 | 57                   | push edi                                   |
0040DF8 | 8965 F4              | mov dword ptr ss:[ebp-C],esp               | [ebp-0C]:ThunRTMain+15D0
0040DF8 | 83E0 01              | and eax,1                                  |
0040DF9 | 8B33                 | mov esi,dword ptr ds:[ebx]                 | esi:"€7\rt校\ft"
0040DF9 | C745 F8 18104000     | mov dword ptr ss:[ebp-8],crackme.401018    |
0040DF9 | 53                   | push ebx                                   |
0040DF9 | 8945 FC              | mov dword ptr ss:[ebp-4],eax               |
0040DF9 | 895D 08              | mov dword ptr ss:[ebp+8],ebx               |
0040DFA | 89B5 44FFFFFF        | mov dword ptr ss:[ebp-BC],esi              | [ebp-BC]:L"瀀;"
0040DFA | FF56 04              | call dword ptr ds:[esi+4]                  | [esi+04]:ThunRTMain+211
0040DFA | 8BB6 08030000        | mov esi,dword ptr ds:[esi+308]             | esi:"€7\rt校\ft"
0040DFA | 33FF                 | xor edi,edi                                |
0040DFB | 53                   | push ebx                                   |
0040DFB | 897D E8              | mov dword ptr ss:[ebp-18],edi              |
0040DFB | 897D E4              | mov dword ptr ss:[ebp-1C],edi              |
0040DFB | 897D E0              | mov dword ptr ss:[ebp-20],edi              |
0040DFB | 897D D0              | mov dword ptr ss:[ebp-30],edi              |
0040DFB | 897D C0              | mov dword ptr ss:[ebp-40],edi              |
0040DFC | 897D B0              | mov dword ptr ss:[ebp-50],edi              |
0040DFC | 897D A0              | mov dword ptr ss:[ebp-60],edi              |
0040DFC | 897D 90              | mov dword ptr ss:[ebp-70],edi              | [ebp-70]:"€7\rt校\ft"
0040DFC | 897D 80              | mov dword ptr ss:[ebp-80],edi              |
0040DFC | 89B5 40FFFFFF        | mov dword ptr ss:[ebp-C0],esi              |
0040DFD | FFD6                 | call esi                                   |
0040DFD | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]              |
0040DFD | 50                   | push eax                                   |
0040DFD | 51                   | push ecx                                   |
0040DFD | FF15 00114100        | call dword ptr ds:[<__vbaObjSet>]          |
0040DFE | 8BF0                 | mov esi,eax                                | esi:"€7\rt校\ft"
0040DFE | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]              | [ebp-1C]: user input key
0040DFE | 50                   | push eax                                   |
0040DFE | 56                   | push esi                                   | esi:"€7\rt校\ft"
0040DFE | 8B16                 | mov edx,dword ptr ds:[esi]                 | [esi]:__vbaStrI2+D88
0040DFE | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
0040DFE | 3BC7                 | cmp eax,edi                                |
0040DFF | 7D 12                | jge crackme.40E005                         |
0040DFF | 68 A0000000          | push A0                                    |
0040DFF | 68 50344000          | push crackme.403450                        |
0040DFF | 56                   | push esi                                   | esi:"€7\rt校\ft"
0040DFF | 50                   | push eax                                   |
0040DFF | FF15 F8104100        | call dword ptr ds:[<__vbaHresultCheckObj>] |
0040E00 | 8B4D E4              | mov ecx,dword ptr ss:[ebp-1C]              |
0040E00 | 51                   | push ecx                                   |
0040E00 | 68 4C344000          | push crackme.40344C                        |
0040E00 | FF15 28114100        | call dword ptr ds:[<__vbaStrCmp>]          |
0040E01 | 8BF0                 | mov esi,eax                                | esi:"€7\rt校\ft"
0040E01 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]              |
0040E01 | F7DE                 | neg esi                                    | esi:"€7\rt校\ft"
0040E01 | 1BF6                 | sbb esi,esi                                | esi:"€7\rt校\ft"
0040E01 | 46                   | inc esi                                    | esi:"€7\rt校\ft"
0040E01 | F7DE                 | neg esi                                    | esi:"€7\rt校\ft"
0040E02 | FF15 8C114100        | call dword ptr ds:[<__vbaFreeStr>]         |
0040E02 | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]              |
0040E02 | FF15 90114100        | call dword ptr ds:[<__vbaFreeObj>]         |
0040E02 | 66:3BF7              | cmp si,di                                  |
0040E03 | 74 7D                | je crackme.40E0B1                          |
0040E03 | BF 0A000000          | mov edi,A                                  | Fail
0040E03 | BB 04000280          | mov ebx,80020004                           |
0040E03 | 897D A0              | mov dword ptr ss:[ebp-60],edi              |
0040E04 | 897D B0              | mov dword ptr ss:[ebp-50],edi              |
0040E04 | 8B3D 78114100        | mov edi,dword ptr ds:[<__vbaVarDup>]       |
0040E04 | BE 08000000          | mov esi,8                                  | esi:"€7\rt校\ft"
0040E04 | 8D55 80              | lea edx,dword ptr ss:[ebp-80]              |
0040E05 | 8D4D C0              | lea ecx,dword ptr ss:[ebp-40]              |
0040E05 | 895D A8              | mov dword ptr ss:[ebp-58],ebx              | [ebp-58]:L"瀀;"
0040E05 | 895D B8              | mov dword ptr ss:[ebp-48],ebx              |
0040E05 | C745 88 EC344000     | mov dword ptr ss:[ebp-78],crackme.4034EC   | 4034EC:L"Error"
0040E06 | 8975 80              | mov dword ptr ss:[ebp-80],esi              |
0040E06 | FFD7                 | call edi                                   |
0040E06 | 8D55 90              | lea edx,dword ptr ss:[ebp-70]              | [ebp-70]:"€7\rt校\ft"
0040E06 | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]              |
0040E06 | C745 98 94344000     | mov dword ptr ss:[ebp-68],crackme.403494   | 403494:L"You have to enter an 9 number key first."
0040E07 | 8975 90              | mov dword ptr ss:[ebp-70],esi              | [ebp-70]:"€7\rt校\ft"
0040E07 | FFD7                 | call edi                                   |
0040E07 | 8D55 A0              | lea edx,dword ptr ss:[ebp-60]              |
0040E07 | 8D45 B0              | lea eax,dword ptr ss:[ebp-50]              |
0040E07 | 52                   | push edx                                   |
0040E08 | 8D4D C0              | lea ecx,dword ptr ss:[ebp-40]              |
0040E08 | 50                   | push eax                                   |
0040E08 | 51                   | push ecx                                   |
0040E08 | 8D55 D0              | lea edx,dword ptr ss:[ebp-30]              |
0040E08 | 6A 40                | push 40                                    |
0040E08 | 52                   | push edx                                   |
0040E08 | FF15 04114100        | call dword ptr ds:[<Ordinal#595>]          |
0040E09 | 8D45 A0              | lea eax,dword ptr ss:[ebp-60]              |
0040E09 | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]              |
0040E09 | 50                   | push eax                                   |
0040E09 | 8D55 C0              | lea edx,dword ptr ss:[ebp-40]              |
0040E09 | 51                   | push ecx                                   |
0040E09 | 8D45 D0              | lea eax,dword ptr ss:[ebp-30]              |
0040E09 | 52                   | push edx                                   |
0040E0A | 50                   | push eax                                   |
0040E0A | 6A 04                | push 4                                     |
0040E0A | FF15 EC104100        | call dword ptr ds:[<__vbaFreeVarList>]     |
0040E0A | 83C4 14              | add esp,14                                 |
0040E0A | E9 DF030000          | jmp crackme.40E490                         |
0040E0B | 53                   | push ebx                                   |
0040E0B | FF95 40FFFFFF        | call dword ptr ss:[ebp-C0]                 |
0040E0B | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]              |
0040E0B | 50                   | push eax                                   |
0040E0B | 51                   | push ecx                                   |
0040E0B | FF15 00114100        | call dword ptr ds:[<__vbaObjSet>]          |
0040E0C | 8BF0                 | mov esi,eax                                | esi:"€7\rt校\ft"
0040E0C | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]              | [ebp-1C]: user key
0040E0C | 50                   | push eax                                   |
0040E0C | 56                   | push esi                                   | esi:"€7\rt校\ft"
0040E0C | 8B16                 | mov edx,dword ptr ds:[esi]                 | [esi]:__vbaStrI2+D88
0040E0C | FF92 A0000000        | call dword ptr ds:[edx+A0]                 |
0040E0D | 3BC7                 | cmp eax,edi                                |
0040E0D | 7D 12                | jge crackme.40E0E8                         |
0040E0D | 68 A0000000          | push A0                                    |
0040E0D | 68 50344000          | push crackme.403450                        |
0040E0E | 56                   | push esi                                   | esi:"€7\rt校\ft"
0040E0E | 50                   | push eax                                   |
0040E0E | FF15 F8104100        | call dword ptr ds:[<__vbaHresultCheckObj>] |
0040E0E | 8B4D E4              | mov ecx,dword ptr ss:[ebp-1C]              |
0040E0E | 51                   | push ecx                                   |
0040E0E | FF15 5C114100        | call dword ptr ds:[<__vbaR8Str>]           |
0040E0F | DB43 4C              | fild dword ptr ds:[ebx+4C]                 |
0040E0F | DD9D 38FFFFFF        | fstp qword ptr ss:[ebp-C8]                 | 315751288
0040E0F | DCA5 38FFFFFF        | fsub qword ptr ss:[ebp-C8]                 |
0040E10 | DFE0                 | fnstsw ax                                  |
0040E10 | A8 0D                | test al,D                                  |
0040E10 | 0F85 EB030000        | jne crackme.40E4F6                         |
0040E10 | FF15 14114100        | call dword ptr ds:[<__vbaFpR8>]            |
0040E11 | DC1D 08104000        | fcomp qword ptr ds:[401008]                |
0040E11 | DFE0                 | fnstsw ax                                  |
0040E11 | F6C4 40              | test ah,40                                 |
0040E11 | 74 05                | je crackme.40E123                          |
0040E11 | BF 01000000          | mov edi,1                                  |
0040E12 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]              |
0040E12 | FF15 8C114100        | call dword ptr ds:[<__vbaFreeStr>]         |
0040E12 | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]              |
0040E12 | FF15 90114100        | call dword ptr ds:[<__vbaFreeObj>]         |
0040E13 | F7DF                 | neg edi                                    |
0040E13 | 66:85FF              | test di,di                                 |
0040E13 | 0F84 2C010000        | je <crackme.Fail>                          |
0040E14 | BB 04000280          | mov ebx,80020004                           | Success
```


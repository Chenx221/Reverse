v1版本密钥是用随机数计算的，跳过

v2版本好像变得更简单了

先上serial计算公式：

```
1440*month*day+year
```

这些值是窗体载入时就计算的，所以如果你错过了一天，你可以用spyxx获取隐藏的textbox值

只有检查按钮会检查，注册按钮仅仅是弹窗提示win而已

细节：

```assembly
00402240 | 55                   | push ebp                                   |
00402241 | 8BEC                 | mov ebp,esp                                |
00402243 | 83EC 0C              | sub esp,C                                  |
00402246 | 68 F6104000          | push <JMP.&__vbaExceptHandler>             |
0040224B | 64:A1 00000000       | mov eax,dword ptr fs:[0]                   | eax:L"沁Ф3"
00402251 | 50                   | push eax                                   | eax:L"沁Ф3"
00402252 | 64:8925 00000000     | mov dword ptr fs:[0],esp                   |
00402259 | 81EC C8000000        | sub esp,C8                                 |
0040225F | 53                   | push ebx                                   |
00402260 | 56                   | push esi                                   |
00402261 | 57                   | push edi                                   |
00402262 | 8965 F4              | mov dword ptr ss:[ebp-C],esp               |
00402265 | C745 F8 E0104000     | mov dword ptr ss:[ebp-8],crackme_241028.40 |
0040226C | 8B7D 08              | mov edi,dword ptr ss:[ebp+8]               |
0040226F | 8BC7                 | mov eax,edi                                | eax:L"沁Ф3"
00402271 | 83E0 01              | and eax,1                                  | eax:L"沁Ф3"
00402274 | 8945 FC              | mov dword ptr ss:[ebp-4],eax               |
00402277 | 83E7 FE              | and edi,FFFFFFFE                           |
0040227A | 57                   | push edi                                   |
0040227B | 897D 08              | mov dword ptr ss:[ebp+8],edi               |
0040227E | 8B0F                 | mov ecx,dword ptr ds:[edi]                 |
00402280 | FF51 04              | call dword ptr ds:[ecx+4]                  |
00402283 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402289 | 33F6                 | xor esi,esi                                |
0040228B | 52                   | push edx                                   | edx:&"Load"
0040228C | 8975 E8              | mov dword ptr ss:[ebp-18],esi              | [ebp-18]:_GetParent@4
0040228F | 8975 D8              | mov dword ptr ss:[ebp-28],esi              |
00402292 | 8975 C8              | mov dword ptr ss:[ebp-38],esi              |
00402295 | 8975 B8              | mov dword ptr ss:[ebp-48],esi              |
00402298 | 8975 A8              | mov dword ptr ss:[ebp-58],esi              |
0040229B | 8975 98              | mov dword ptr ss:[ebp-68],esi              |
0040229E | 8975 88              | mov dword ptr ss:[ebp-78],esi              |
004022A1 | 89B5 78FFFFFF        | mov dword ptr ss:[ebp-88],esi              |
004022A7 | 89B5 74FFFFFF        | mov dword ptr ss:[ebp-8C],esi              |
004022AD | 89B5 64FFFFFF        | mov dword ptr ss:[ebp-9C],esi              | [ebp-9C]:_NtdllEditWndProc_A@0
004022B3 | 89B5 54FFFFFF        | mov dword ptr ss:[ebp-AC],esi              |
004022B9 | 89B5 44FFFFFF        | mov dword ptr ss:[ebp-BC],esi              |
004022BF | 89B5 34FFFFFF        | mov dword ptr ss:[ebp-CC],esi              |
004022C5 | FF15 90104000        | call dword ptr ds:[<Ordinal#610>]          |
004022CB | 8B1D 98104000        | mov ebx,dword ptr ds:[<__vbaVarDup>]       |
004022D1 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]              |
004022D7 | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]              |
004022DD | C785 3CFFFFFF 101940 | mov dword ptr ss:[ebp-C4],crackme_241028.4 | dd
004022E7 | C785 34FFFFFF 080000 | mov dword ptr ss:[ebp-CC],8                |
004022F1 | FFD3                 | call ebx                                   |
004022F3 | 6A 01                | push 1                                     |
004022F5 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
004022FB | 6A 01                | push 1                                     |
004022FD | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402303 | 50                   | push eax                                   | eax:L"沁Ф3"
00402304 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
0040230A | 51                   | push ecx                                   |
0040230B | 52                   | push edx                                   | edx:&"Load"
0040230C | FF15 24104000        | call dword ptr ds:[<Ordinal#660>]          | Get Current Date(Day)
00402312 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
00402318 | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]              |
0040231B | FF15 08104000        | call dword ptr ds:[<__vbaVarMove>]         |
00402321 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
00402327 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
0040232D | 50                   | push eax                                   | eax:L"沁Ф3"
0040232E | 51                   | push ecx                                   |
0040232F | 6A 02                | push 2                                     |
00402331 | FF15 14104000        | call dword ptr ds:[<__vbaFreeVarList>]     |
00402337 | 83C4 0C              | add esp,C                                  |
0040233A | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402340 | 52                   | push edx                                   | edx:&"Load"
00402341 | FF15 90104000        | call dword ptr ds:[<Ordinal#610>]          |
00402347 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]              |
0040234D | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]              |
00402353 | C785 3CFFFFFF 1C1940 | mov dword ptr ss:[ebp-C4],crackme_241028.4 | mm
0040235D | C785 34FFFFFF 080000 | mov dword ptr ss:[ebp-CC],8                |
00402367 | FFD3                 | call ebx                                   |
00402369 | 6A 01                | push 1                                     |
0040236B | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
00402371 | 6A 01                | push 1                                     |
00402373 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402379 | 50                   | push eax                                   | eax:L"沁Ф3"
0040237A | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
00402380 | 51                   | push ecx                                   |
00402381 | 52                   | push edx                                   | edx:&"Load"
00402382 | FF15 24104000        | call dword ptr ds:[<Ordinal#660>]          | Get current date (month)
00402388 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
0040238E | 8D8D 78FFFFFF        | lea ecx,dword ptr ss:[ebp-88]              |
00402394 | FF15 08104000        | call dword ptr ds:[<__vbaVarMove>]         |
0040239A | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
004023A0 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
004023A6 | 50                   | push eax                                   | eax:L"沁Ф3"
004023A7 | 51                   | push ecx                                   |
004023A8 | 6A 02                | push 2                                     |
004023AA | FF15 14104000        | call dword ptr ds:[<__vbaFreeVarList>]     |
004023B0 | 83C4 0C              | add esp,C                                  |
004023B3 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
004023B9 | 52                   | push edx                                   | edx:&"Load"
004023BA | FF15 90104000        | call dword ptr ds:[<Ordinal#610>]          |
004023C0 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]              |
004023C6 | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]              |
004023CC | C785 3CFFFFFF 281940 | mov dword ptr ss:[ebp-C4],crackme_241028.4 | yyyy
004023D6 | C785 34FFFFFF 080000 | mov dword ptr ss:[ebp-CC],8                |
004023E0 | FFD3                 | call ebx                                   |
004023E2 | 6A 01                | push 1                                     |
004023E4 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
004023EA | 6A 01                | push 1                                     |
004023EC | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
004023F2 | 50                   | push eax                                   | eax:L"沁Ф3"
004023F3 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
004023F9 | 51                   | push ecx                                   |
004023FA | 52                   | push edx                                   | edx:&"Load"
004023FB | FF15 24104000        | call dword ptr ds:[<Ordinal#660>]          | Get current date (year)
00402401 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
00402407 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]              |
0040240A | FF15 08104000        | call dword ptr ds:[<__vbaVarMove>]         |
00402410 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
00402416 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
0040241C | 50                   | push eax                                   | eax:L"沁Ф3"
0040241D | 51                   | push ecx                                   |
0040241E | 6A 02                | push 2                                     |
00402420 | FF15 14104000        | call dword ptr ds:[<__vbaFreeVarList>]     |
00402426 | 83C4 0C              | add esp,C                                  |
00402429 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
0040242F | 52                   | push edx                                   | edx:&"Load"
00402430 | FF15 9C104000        | call dword ptr ds:[<Ordinal#612>]          |
00402436 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]              |
0040243C | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]              |
00402442 | C785 3CFFFFFF 381940 | mov dword ptr ss:[ebp-C4],crackme_241028.4 | hh
0040244C | C785 34FFFFFF 080000 | mov dword ptr ss:[ebp-CC],8                |
00402456 | FFD3                 | call ebx                                   |
00402458 | 6A 01                | push 1                                     |
0040245A | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
00402460 | 6A 01                | push 1                                     |
00402462 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402468 | 50                   | push eax                                   | eax:L"沁Ф3"
00402469 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
0040246F | 51                   | push ecx                                   |
00402470 | 52                   | push edx                                   | edx:&"Load"
00402471 | FF15 24104000        | call dword ptr ds:[<Ordinal#660>]          | Get current date (hour)
00402477 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              |
0040247D | 8D4D C8              | lea ecx,dword ptr ss:[ebp-38]              |
00402480 | FF15 08104000        | call dword ptr ds:[<__vbaVarMove>]         |
00402486 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
0040248C | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402492 | 50                   | push eax                                   | eax:L"沁Ф3"
00402493 | 51                   | push ecx                                   |
00402494 | 6A 02                | push 2                                     |
00402496 | FF15 14104000        | call dword ptr ds:[<__vbaFreeVarList>]     |
0040249C | 8B1D 60104000        | mov ebx,dword ptr ds:[<__vbaVarMul>]       |
004024A2 | 83C4 0C              | add esp,C                                  |
004024A5 | 8D55 B8              | lea edx,dword ptr ss:[ebp-48]              | day
004024A8 | 8D85 78FFFFFF        | lea eax,dword ptr ss:[ebp-88]              | month
004024AE | 52                   | push edx                                   | edx:&"Load"
004024AF | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
004024B5 | 50                   | push eax                                   | eax:L"沁Ф3"
004024B6 | 51                   | push ecx                                   |
004024B7 | C785 3CFFFFFF A00500 | mov dword ptr ss:[ebp-C4],5A0              |
004024C1 | C785 34FFFFFF 020000 | mov dword ptr ss:[ebp-CC],2                |
004024CB | FFD3                 | call ebx                                   | month*day
004024CD | 50                   | push eax                                   | eax:L"沁Ф3"
004024CE | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]              | 0x5A0
004024D4 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]              |
004024DA | 52                   | push edx                                   | edx:&"Load"
004024DB | 50                   | push eax                                   | eax:L"沁Ф3"
004024DC | FFD3                 | call ebx                                   | month*day*0x5A0
004024DE | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]              | year
004024E1 | 50                   | push eax                                   | eax:L"沁Ф3"
004024E2 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]              | hour
004024E8 | 51                   | push ecx                                   |
004024E9 | 52                   | push edx                                   | edx:&"Load"
004024EA | FF15 94104000        | call dword ptr ds:[<__vbaVarAdd>]          | year+上面的结果
004024F0 | 50                   | push eax                                   | eax:L"沁Ф3"
004024F1 | FF15 10104000        | call dword ptr ds:[<__vbaStrVarMove>]      |
004024F7 | 8BD0                 | mov edx,eax                                | edx:&"Load", eax:L"沁Ф3"
004024F9 | 8D4D E8              | lea ecx,dword ptr ss:[ebp-18]              | [ebp-18]:_GetParent@4
004024FC | FF15 A4104000        | call dword ptr ds:[<__vbaStrMove>]         |
00402502 | 8D8D 44FFFFFF        | lea ecx,dword ptr ss:[ebp-BC]              |
00402508 | FF15 0C104000        | call dword ptr ds:[<__vbaFreeVar>]         |
0040250E | 8B07                 | mov eax,dword ptr ds:[edi]                 | eax:L"沁Ф3"
00402510 | 57                   | push edi                                   |
00402511 | FF90 FC020000        | call dword ptr ds:[eax+2FC]                |
00402517 | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]              |
0040251D | 50                   | push eax                                   | eax:L"沁Ф3"
0040251E | 51                   | push ecx                                   |
0040251F | FF15 34104000        | call dword ptr ds:[<__vbaObjSet>]          |
00402525 | 8BF8                 | mov edi,eax                                | eax:L"沁Ф3"
00402527 | 8B45 E8              | mov eax,dword ptr ss:[ebp-18]              | [ebp-18]:_GetParent@4
0040252A | 50                   | push eax                                   | eax:L"沁Ф3"
0040252B | 57                   | push edi                                   |
0040252C | 8B17                 | mov edx,dword ptr ds:[edi]                 | edx:&"Load"
0040252E | FF92 A4000000        | call dword ptr ds:[edx+A4]                 |
00402534 | 3BC6                 | cmp eax,esi                                | eax:L"沁Ф3"
00402536 | DBE2                 | fnclex                                     |
00402538 | 7D 12                | jge crackme_241028.40254C                  |
0040253A | 68 A4000000          | push A4                                    |
0040253F | 68 9C184000          | push crackme_241028.40189C                 |
00402544 | 57                   | push edi                                   |
00402545 | 50                   | push eax                                   | eax:L"沁Ф3"
00402546 | FF15 28104000        | call dword ptr ds:[<__vbaHresultCheckObj>] |
0040254C | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]              |
00402552 | FF15 B4104000        | call dword ptr ds:[<__vbaFreeObj>]         |
00402558 | 8975 FC              | mov dword ptr ss:[ebp-4],esi               |
0040255B | 68 C5254000          | push crackme_241028.4025C5                 |
00402560 | EB 2D                | jmp crackme_241028.40258F                  |
00402562 | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]              |
00402568 | FF15 B4104000        | call dword ptr ds:[<__vbaFreeObj>]         |
0040256E | 8D8D 44FFFFFF        | lea ecx,dword ptr ss:[ebp-BC]              |
00402574 | 8D95 54FFFFFF        | lea edx,dword ptr ss:[ebp-AC]              |
0040257A | 51                   | push ecx                                   |
0040257B | 8D85 64FFFFFF        | lea eax,dword ptr ss:[ebp-9C]              | [ebp-9C]:_NtdllEditWndProc_A@0
00402581 | 52                   | push edx                                   | edx:&"Load"
00402582 | 50                   | push eax                                   | eax:L"沁Ф3"
00402583 | 6A 03                | push 3                                     |
00402585 | FF15 14104000        | call dword ptr ds:[<__vbaFreeVarList>]     |
0040258B | 83C4 10              | add esp,10                                 |
0040258E | C3                   | ret                                        |
0040258F | 8D4D E8              | lea ecx,dword ptr ss:[ebp-18]              | [ebp-18]:_GetParent@4
00402592 | FF15 B8104000        | call dword ptr ds:[<__vbaFreeStr>]         |
00402598 | 8B35 0C104000        | mov esi,dword ptr ds:[<__vbaFreeVar>]      |
0040259E | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]              |
004025A1 | FFD6                 | call esi                                   |
004025A3 | 8D4D C8              | lea ecx,dword ptr ss:[ebp-38]              |
004025A6 | FFD6                 | call esi                                   |
004025A8 | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]              |
004025AB | FFD6                 | call esi                                   |
004025AD | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]              |
004025B0 | FFD6                 | call esi                                   |
004025B2 | 8D4D 98              | lea ecx,dword ptr ss:[ebp-68]              |
004025B5 | FFD6                 | call esi                                   |
004025B7 | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]              |
004025BA | FFD6                 | call esi                                   |
004025BC | 8D8D 78FFFFFF        | lea ecx,dword ptr ss:[ebp-88]              |
004025C2 | FFD6                 | call esi                                   |
004025C4 | C3                   | ret                                        |
```


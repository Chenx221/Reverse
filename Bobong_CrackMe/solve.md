找密码

解决方法：

click事件开头一堆垃圾，兜兜转转还是输入值，再往下一看发现是和系统盘序列号做比较（正确密码需要反转的十六进制分区序列号）

下面是生成正确serial的powershell命令

```powershell
"{0:X}" -f [convert]::ToInt32((Get-WmiObject Win32_LogicalDisk | Where-Object { $_.DeviceID -eq "C:" }).VolumeSerialNumber, 16) -split '' -join '' | % {$_[-1..-($_.Length)] -join ''}
```

细节：

```
0x4030F0
ulol()
 反转

0x402470
kupalka()
 存在μ -> Hex 2 Char 
 不存在μ -> Char 2 Hex

0x4028C0
gago(h)
 编码xx;xx;xx;

0x402CD0
siraulo
 读取

0x4038C0
wek
 读取系统盘序列号
```

以下是click事件内容，上面提到的几个子函数，篇幅问题不放进来了

```assembly
004032D0 | 55                   | push ebp                                        |
004032D1 | 8BEC                 | mov ebp,esp                                     |
004032D3 | 83EC 0C              | sub esp,C                                       |
004032D6 | 68 36124000          | push <JMP.&__vbaExceptHandler>                  |
004032DB | 64:A1 00000000       | mov eax,dword ptr fs:[0]                        | eax:_TppWorkerThread@4
004032E1 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004032E2 | 64:8925 00000000     | mov dword ptr fs:[0],esp                        |
004032E9 | 81EC 20010000        | sub esp,120                                     |
004032EF | 53                   | push ebx                                        |
004032F0 | 56                   | push esi                                        |
004032F1 | 57                   | push edi                                        |
004032F2 | 8965 F4              | mov dword ptr ss:[ebp-C],esp                    | [ebp-0C]:__except_handler4
004032F5 | C745 F8 B0114000     | mov dword ptr ss:[ebp-8],bobong crackme.4011B0  |
004032FC | 8B75 08              | mov esi,dword ptr ss:[ebp+8]                    |
004032FF | 8BC6                 | mov eax,esi                                     | eax:_TppWorkerThread@4
00403301 | 83E0 01              | and eax,1                                       | eax:_TppWorkerThread@4
00403304 | 8945 FC              | mov dword ptr ss:[ebp-4],eax                    | eax:_TppWorkerThread@4
00403307 | 83E6 FE              | and esi,FFFFFFFE                                |
0040330A | 56                   | push esi                                        |
0040330B | 8975 08              | mov dword ptr ss:[ebp+8],esi                    |
0040330E | 8B0E                 | mov ecx,dword ptr ds:[esi]                      |
00403310 | FF51 04              | call dword ptr ds:[ecx+4]                       |
00403313 | 8B16                 | mov edx,dword ptr ds:[esi]                      |
00403315 | 33FF                 | xor edi,edi                                     |
00403317 | 56                   | push esi                                        |
00403318 | 897D E8              | mov dword ptr ss:[ebp-18],edi                   |
0040331B | 897D E4              | mov dword ptr ss:[ebp-1C],edi                   |
0040331E | 897D E0              | mov dword ptr ss:[ebp-20],edi                   |
00403321 | 897D D0              | mov dword ptr ss:[ebp-30],edi                   |
00403324 | 897D CC              | mov dword ptr ss:[ebp-34],edi                   |
00403327 | 897D BC              | mov dword ptr ss:[ebp-44],edi                   |
0040332A | 897D B8              | mov dword ptr ss:[ebp-48],edi                   |
0040332D | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
00403330 | 897D B0              | mov dword ptr ss:[ebp-50],edi                   |
00403333 | 897D AC              | mov dword ptr ss:[ebp-54],edi                   |
00403336 | 897D A8              | mov dword ptr ss:[ebp-58],edi                   |
00403339 | 897D A4              | mov dword ptr ss:[ebp-5C],edi                   |
0040333C | 897D A0              | mov dword ptr ss:[ebp-60],edi                   |
0040333F | 897D 9C              | mov dword ptr ss:[ebp-64],edi                   |
00403342 | 897D 8C              | mov dword ptr ss:[ebp-74],edi                   |
00403345 | 89BD 7CFFFFFF        | mov dword ptr ss:[ebp-84],edi                   |
0040334B | 89BD 6CFFFFFF        | mov dword ptr ss:[ebp-94],edi                   |
00403351 | 89BD 5CFFFFFF        | mov dword ptr ss:[ebp-A4],edi                   |
00403357 | 89BD 4CFFFFFF        | mov dword ptr ss:[ebp-B4],edi                   |
0040335D | FF92 04030000        | call dword ptr ds:[edx+304]                     |
00403363 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403364 | 8D45 9C              | lea eax,dword ptr ss:[ebp-64]                   | eax:_TppWorkerThread@4
00403367 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403368 | FF15 4C104000        | call dword ptr ds:[<__vbaObjSet>]               |
0040336E | 8BD8                 | mov ebx,eax                                     | eax:_TppWorkerThread@4
00403370 | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]                   |
00403373 | 52                   | push edx                                        |
00403374 | 53                   | push ebx                                        |
00403375 | 8B0B                 | mov ecx,dword ptr ds:[ebx]                      |
00403377 | FF91 A0000000        | call dword ptr ds:[ecx+A0]                      |
0040337D | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
0040337F | DBE2                 | fnclex                                          |
00403381 | 7D 12                | jge bobong crackme.403395                       |
00403383 | 68 A0000000          | push A0                                         |
00403388 | 68 481E4000          | push bobong crackme.401E48                      |
0040338D | 53                   | push ebx                                        |
0040338E | 50                   | push eax                                        | eax:_TppWorkerThread@4
0040338F | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
00403395 | 8B55 B4              | mov edx,dword ptr ss:[ebp-4C]                   |
00403398 | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]                   |
0040339B | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
0040339E | FF15 04114000        | call dword ptr ds:[<__vbaStrMove>]              |
004033A4 | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
004033A6 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]                   |
004033A9 | 8D55 B0              | lea edx,dword ptr ss:[ebp-50]                   |
004033AC | 51                   | push ecx                                        |
004033AD | 52                   | push edx                                        |
004033AE | 56                   | push esi                                        |
004033AF | FF90 04070000        | call dword ptr ds:[eax+704]                     | Key翻转
004033B5 | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
004033B7 | 7D 12                | jge bobong crackme.4033CB                       |
004033B9 | 68 04070000          | push 704                                        |
004033BE | 68 181D4000          | push bobong crackme.401D18                      |
004033C3 | 56                   | push esi                                        |
004033C4 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004033C5 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
004033CB | 8B45 AC              | mov eax,dword ptr ss:[ebp-54]                   | eax:_TppWorkerThread@4
004033CE | 8D55 8C              | lea edx,dword ptr ss:[ebp-74]                   |
004033D1 | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]                   |
004033D4 | 897D AC              | mov dword ptr ss:[ebp-54],edi                   |
004033D7 | 8945 94              | mov dword ptr ss:[ebp-6C],eax                   | eax:_TppWorkerThread@4
004033DA | C745 8C 08000000     | mov dword ptr ss:[ebp-74],8                     |
004033E1 | FF15 0C104000        | call dword ptr ds:[<__vbaVarMove>]              |
004033E7 | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]                   |
004033EA | FF15 20114000        | call dword ptr ds:[<__vbaFreeStr>]              |
004033F0 | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]                   |
004033F3 | FF15 24114000        | call dword ptr ds:[<__vbaFreeObj>]              |
004033F9 | 8B1E                 | mov ebx,dword ptr ds:[esi]                      |
004033FB | 8D45 B0              | lea eax,dword ptr ss:[ebp-50]                   | eax:_TppWorkerThread@4
004033FE | 8D4D D0              | lea ecx,dword ptr ss:[ebp-30]                   |
00403401 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403402 | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]                   |
00403405 | 51                   | push ecx                                        |
00403406 | 52                   | push edx                                        |
00403407 | FF15 A0104000        | call dword ptr ds:[<__vbaStrVarVal>]            |
0040340D | 50                   | push eax                                        | eax:_TppWorkerThread@4
0040340E | 56                   | push esi                                        |
0040340F | FF93 F8060000        | call dword ptr ds:[ebx+6F8]                     | 转HEX
00403415 | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
00403417 | 7D 12                | jge bobong crackme.40342B                       |
00403419 | 68 F8060000          | push 6F8                                        |
0040341E | 68 181D4000          | push bobong crackme.401D18                      |
00403423 | 56                   | push esi                                        |
00403424 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403425 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
0040342B | 8B55 B0              | mov edx,dword ptr ss:[ebp-50]                   |
0040342E | 8B1D 04114000        | mov ebx,dword ptr ds:[<__vbaStrMove>]           |
00403434 | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]                   |
00403437 | 897D B0              | mov dword ptr ss:[ebp-50],edi                   |
0040343A | FFD3                 | call ebx                                        |
0040343C | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
0040343F | FF15 20114000        | call dword ptr ds:[<__vbaFreeStr>]              |
00403445 | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
00403447 | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
0040344A | 8D55 B8              | lea edx,dword ptr ss:[ebp-48]                   |
0040344D | 51                   | push ecx                                        |
0040344E | 52                   | push edx                                        |
0040344F | 56                   | push esi                                        |
00403450 | FF90 FC060000        | call dword ptr ds:[eax+6FC]                     | 计算一个长串（貌似有随机数参与）
00403456 | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
00403458 | 7D 12                | jge bobong crackme.40346C                       |
0040345A | 68 FC060000          | push 6FC                                        |
0040345F | 68 181D4000          | push bobong crackme.401D18                      |
00403464 | 56                   | push esi                                        |
00403465 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403466 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
0040346C | 8B55 B4              | mov edx,dword ptr ss:[ebp-4C]                   |
0040346F | 8D4D CC              | lea ecx,dword ptr ss:[ebp-34]                   |
00403472 | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
00403475 | FFD3                 | call ebx                                        |
00403477 | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
00403479 | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
0040347C | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]                   |
0040347F | 51                   | push ecx                                        |
00403480 | 52                   | push edx                                        |
00403481 | 56                   | push esi                                        |
00403482 | FF90 00070000        | call dword ptr ds:[eax+700]                     | 数据转换（又回到了前面的形式）&反转
00403488 | 3BC7                 | cmp eax,edi                                     | 不，好像是回到了原地（初始翻转+hex）？
0040348A | 7D 12                | jge bobong crackme.40349E                       |
0040348C | 68 00070000          | push 700                                        |
00403491 | 68 181D4000          | push bobong crackme.401D18                      |
00403496 | 56                   | push esi                                        |
00403497 | 50                   | push eax                                        | eax:_TppWorkerThread@4
00403498 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
0040349E | 8B55 B4              | mov edx,dword ptr ss:[ebp-4C]                   |
004034A1 | 8D4D B8              | lea ecx,dword ptr ss:[ebp-48]                   |
004034A4 | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
004034A7 | FFD3                 | call ebx                                        |
004034A9 | 8B55 B8              | mov edx,dword ptr ss:[ebp-48]                   |
004034AC | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
004034AE | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
004034B1 | 51                   | push ecx                                        |
004034B2 | 52                   | push edx                                        |
004034B3 | 56                   | push esi                                        |
004034B4 | FF90 F8060000        | call dword ptr ds:[eax+6F8]                     | 过了这一步，你会发现已经回到了翻转版本了
004034BA | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
004034BC | 7D 12                | jge bobong crackme.4034D0                       |
004034BE | 68 F8060000          | push 6F8                                        |
004034C3 | 68 181D4000          | push bobong crackme.401D18                      |
004034C8 | 56                   | push esi                                        |
004034C9 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004034CA | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
004034D0 | 8B55 B4              | mov edx,dword ptr ss:[ebp-4C]                   |
004034D3 | 8D4D E0              | lea ecx,dword ptr ss:[ebp-20]                   |
004034D6 | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
004034D9 | FFD3                 | call ebx                                        |
004034DB | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
004034DD | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
004034E0 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]                   |
004034E3 | 51                   | push ecx                                        |
004034E4 | 52                   | push edx                                        |
004034E5 | 56                   | push esi                                        |
004034E6 | FF90 04070000        | call dword ptr ds:[eax+704]                     | 回到了起点，现在的值就是初始输入
004034EC | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
004034EE | 7D 12                | jge bobong crackme.403502                       |
004034F0 | 68 04070000          | push 704                                        |
004034F5 | 68 181D4000          | push bobong crackme.401D18                      |
004034FA | 56                   | push esi                                        |
004034FB | 50                   | push eax                                        | eax:_TppWorkerThread@4
004034FC | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
00403502 | 8B55 B4              | mov edx,dword ptr ss:[ebp-4C]                   |
00403505 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]                   |
00403508 | 897D B4              | mov dword ptr ss:[ebp-4C],edi                   |
0040350B | FFD3                 | call ebx                                        |
0040350D | BA 5C1E4000          | mov edx,bobong crackme.401E5C                   |
00403512 | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
00403515 | FF15 C0104000        | call dword ptr ds:[<__vbaStrCopy>]              |
0040351B | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
0040351D | 8D4D B0              | lea ecx,dword ptr ss:[ebp-50]                   |
00403520 | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]                   |
00403523 | 51                   | push ecx                                        |
00403524 | 52                   | push edx                                        |
00403525 | 56                   | push esi                                        |
00403526 | FF90 08070000        | call dword ptr ds:[eax+708]                     | 读取C盘序列号
0040352C | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
0040352E | 7D 12                | jge bobong crackme.403542                       |
00403530 | 68 08070000          | push 708                                        |
00403535 | 68 181D4000          | push bobong crackme.401D18                      |
0040353A | 56                   | push esi                                        |
0040353B | 50                   | push eax                                        | eax:_TppWorkerThread@4
0040353C | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
00403542 | 8B55 B0              | mov edx,dword ptr ss:[ebp-50]                   |
00403545 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]                   |
00403548 | 897D B0              | mov dword ptr ss:[ebp-50],edi                   |
0040354B | FFD3                 | call ebx                                        |
0040354D | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
0040354F | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]                   |
00403552 | 8D55 AC              | lea edx,dword ptr ss:[ebp-54]                   |
00403555 | 51                   | push ecx                                        |
00403556 | 52                   | push edx                                        |
00403557 | 56                   | push esi                                        |
00403558 | FF90 04070000        | call dword ptr ds:[eax+704]                     | 翻转序列号
0040355E | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
00403560 | 7D 12                | jge bobong crackme.403574                       |
00403562 | 68 04070000          | push 704                                        |
00403567 | 68 181D4000          | push bobong crackme.401D18                      |
0040356C | 56                   | push esi                                        |
0040356D | 50                   | push eax                                        | eax:_TppWorkerThread@4
0040356E | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
00403574 | BA 5C1E4000          | mov edx,bobong crackme.401E5C                   |
00403579 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]                   |
0040357C | FF15 C0104000        | call dword ptr ds:[<__vbaStrCopy>]              |
00403582 | 8B06                 | mov eax,dword ptr ds:[esi]                      | eax:_TppWorkerThread@4
00403584 | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]                   |
00403587 | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]                   |
0040358A | 51                   | push ecx                                        |
0040358B | 52                   | push edx                                        |
0040358C | 56                   | push esi                                        |
0040358D | FF90 08070000        | call dword ptr ds:[eax+708]                     | eax+708:_TppWorkerThread@4+708
00403593 | 3BC7                 | cmp eax,edi                                     | eax:_TppWorkerThread@4
00403595 | 7D 12                | jge bobong crackme.4035A9                       |
00403597 | 68 08070000          | push 708                                        |
0040359C | 68 181D4000          | push bobong crackme.401D18                      |
004035A1 | 56                   | push esi                                        |
004035A2 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004035A3 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]      |
004035A9 | 8B45 E0              | mov eax,dword ptr ss:[ebp-20]                   | eax:_TppWorkerThread@4
004035AC | 8B4D A0              | mov ecx,dword ptr ss:[ebp-60]                   |
004035AF | 50                   | push eax                                        | eax:_TppWorkerThread@4
004035B0 | 51                   | push ecx                                        |
004035B1 | FF15 6C104000        | call dword ptr ds:[<__vbaStrCmp>]               | 正着比一遍
004035B7 | 8B55 E4              | mov edx,dword ptr ss:[ebp-1C]                   |
004035BA | 8BD8                 | mov ebx,eax                                     | eax:_TppWorkerThread@4
004035BC | 8B45 A8              | mov eax,dword ptr ss:[ebp-58]                   | eax:_TppWorkerThread@4
004035BF | 52                   | push edx                                        |
004035C0 | F7DB                 | neg ebx                                         |
004035C2 | 1BDB                 | sbb ebx,ebx                                     |
004035C4 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004035C5 | 43                   | inc ebx                                         |
004035C6 | F7DB                 | neg ebx                                         |
004035C8 | FF15 6C104000        | call dword ptr ds:[<__vbaStrCmp>]               | 倒着比一遍
004035CE | F7D8                 | neg eax                                         | eax:_TppWorkerThread@4
004035D0 | 1BC0                 | sbb eax,eax                                     | eax:_TppWorkerThread@4
004035D2 | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]                   |
004035D5 | 40                   | inc eax                                         | eax:_TppWorkerThread@4
004035D6 | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]                   |
004035D9 | F7D8                 | neg eax                                         | eax:_TppWorkerThread@4
004035DB | 23D8                 | and ebx,eax                                     | eax:_TppWorkerThread@4
004035DD | 51                   | push ecx                                        |
004035DE | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]                   | eax:_TppWorkerThread@4
004035E1 | 52                   | push edx                                        |
004035E2 | 8D4D AC              | lea ecx,dword ptr ss:[ebp-54]                   |
004035E5 | 50                   | push eax                                        | eax:_TppWorkerThread@4
004035E6 | 8D55 B4              | lea edx,dword ptr ss:[ebp-4C]                   |
004035E9 | 51                   | push ecx                                        |
004035EA | 52                   | push edx                                        |
004035EB | 6A 05                | push 5                                          |
004035ED | FF15 CC104000        | call dword ptr ds:[<__vbaFreeStrList>]          |
004035F3 | 83C4 18              | add esp,18                                      |
004035F6 | 66:3BDF              | cmp bx,di                                       |
004035F9 | 0F84 E3000000        | je <bobong crackme.Fail>                        |
004035FF | 8B06                 | mov eax,dword ptr ds:[esi]                      | Success
00403601 | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]                   |
```


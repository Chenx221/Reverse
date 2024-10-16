任务是找到某串神秘的字符串内容 `Willbeharder`

上来有个奇怪的函数，跳过不管

```assembly
00401255 | 55                   | push ebp                                   | EP
00401256 | 8BEC                 | mov ebp,esp                                |
00401258 | 51                   | push ecx                                   | ecx:EntryPoint
00401259 | 6A 10                | push 10                                    | secret? ↓
0040125B | 68 C8504000          | push bugger.4050C8                         | 4050C8:"jdy28lambcoplwerewaf23sdfcvxc"
00401260 | 68 F0504000          | push bugger.4050F0                         |
00401265 | E8 BEFEFFFF          | call bugger.401128                         |
0040126A | 33DB                 | xor ebx,ebx                                |
0040126C | 64:8B73 18           | mov esi,dword ptr fs:[ebx+18]              | esi:EntryPoint
00401270 | 8B7E 30              | mov edi,dword ptr ds:[esi+30]              | edi:EntryPoint
```

这里获取系统版本号信息，不过并没有什么用（至少没有给我带进`ExitProcessImplementation`

```assembly
00401273 | E8 B2010000          | call <JMP.&_GetVersionStub@0>              |
00401278 | 33D2                 | xor edx,edx                                |
0040127A | 3D 00000080          | cmp eax,80000000                           |
0040127F | 73 05                | jae bugger.401286                          |
00401281 | 8A57 02              | mov dl,byte ptr ds:[edi+2]                 |
00401284 | EB 0E                | jmp bugger.401294                          |
00401286 | 0B56 20              | or edx,dword ptr ds:[esi+20]               |
00401289 | 8B4F 20              | mov ecx,dword ptr ds:[edi+20]              |
0040128C | 0B57 54              | or edx,dword ptr ds:[edi+54]               |
0040128F | 83E1 01              | and ecx,1                                  |
00401292 | 0BD1                 | or edx,ecx                                 |
00401294 | 0BD2                 | or edx,edx                                 |
00401296 | 75 07                | jne bugger.40129F                          |
00401298 | 6A 00                | push 0                                     |
0040129A | E8 85010000          | call <JMP.&_ExitProcessImplementation@4>   |
```

之后是检测OD，如果没检测到窗体名为OLLYDBG的就退出

```assembly
0040129F | 6A 00                | push 0                                     |
004012A1 | 68 4661C479          | push 79C46146                              |
004012A6 | 68 CDE9540C          | push C54E9CD                               |
004012AB | 68 7971D6CC          | push CCD67179                              |
004012B0 | 68 A453F425          | push 25F453A4                              |
004012B5 | 54                   | push esp                                   |
004012B6 | 68 F0504000          | push bugger.4050F0                         |
004012BB | E8 D5FDFFFF          | call bugger.401095                         |
004012C0 | 8BCC                 | mov ecx,esp                                | ecx:_GetVersion_Win8@0
004012C2 | 6A 00                | push 0                                     |
004012C4 | 51                   | push ecx                                   | ecx:"OLLYDBG"
004012C5 | E8 3C010000          | call <JMP.&_FindWindowA@8>                 |
004012CA | 83F8 00              | cmp eax,0                                  | Detect OD
004012CD | 0BC0                 | or eax,eax                                 |
004012CF | 74 07                | je bugger.4012D8                           |
004012D1 | E8 0E000000          | call bugger.4012E4                         |
004012D6 | EB 07                | jmp bugger.4012DF                          |
004012D8 | 6A 00                | push 0                                     | ? 你什么意思
004012DA | E8 45010000          | call <JMP.&_ExitProcessImplementation@4>   |
004012DF | 59                   | pop ecx                                    | ecx:_GetVersion_Win8@0
```

之后来到一个新的函数 `4012E4`，这里准备检查od进程&再查一遍od窗体

```
004012E4 | 55                   | push ebp                                   |
004012E5 | 8BEC                 | mov ebp,esp                                |
004012E7 | 51                   | push ecx                                   |
004012E8 | B8 28010000          | mov eax,128                                |
004012ED | A3 50524000          | mov dword ptr ds:[405250],eax              |
004012F2 | 6A 00                | push 0                                     |
004012F4 | 6A 02                | push 2                                     |
004012F6 | E8 23010000          | call <JMP.&_CreateToolhelp32Snapshot@8>    |
004012FB | A3 78534000          | mov dword ptr ds:[405378],eax              |
00401300 | 68 50524000          | push bugger.405250                         |
00401305 | 50                   | push eax                                   |
00401306 | E8 2B010000          | call <JMP.&_Process32First@8>              |
0040130B | 6A 00                | push 0                                     |
0040130D | 68 4661C479          | push 79C46146                              |
00401312 | 68 CDE9540C          | push C54E9CD                               |
00401317 | 68 7971D6CC          | push CCD67179                              |
0040131C | 68 A453F425          | push 25F453A4                              |
00401321 | 54                   | push esp                                   |
00401322 | 68 F0504000          | push bugger.4050F0                         |
00401327 | E8 69FDFFFF          | call bugger.401095                         |
0040132C | 8BCC                 | mov ecx,esp                                | esp: "OLLYDBG"
0040132E | 6A 00                | push 0                                     |
00401330 | 51                   | push ecx                                   |
00401331 | E8 D0000000          | call <JMP.&_FindWindowA@8>                 |
00401336 | 83F8 00              | cmp eax,0                                  |
00401339 | 0BC0                 | or eax,eax                                 |
0040133B | 74 07                | je bugger.401344                           |
0040133D | E8 0C000000          | call bugger.40134E                         |
00401342 | EB 05                | jmp bugger.401349                          |
00401344 | E8 6A000000          | call bugger.4013B3                         |
00401349 | 59                   | pop ecx                                    |
0040134A | C9                   | leave                                      |
0040134B | C2 0400              | ret 4                                      |
```

如果找不到了就进 `4013B3`，找到了就进 `40134E`

先看`40134E`，遍历当前进程查找`OLLYDBG.EXE`，找到就掐然后退出自身

```assembly
0040134E | 55                   | push ebp                                   |
0040134F | 8BEC                 | mov ebp,esp                                |
00401351 | 51                   | push ecx                                   |
00401352 | 6A 00                | push 0                                     |
00401354 | 68 45584500          | push 455845                                |
00401359 | 68 4442472E          | push 2E474244                              |
0040135E | 68 4F4C4C59          | push 594C4C4F                              |
00401363 | 8BCC                 | mov ecx,esp                                |
00401365 | 51                   | push ecx                                   | ecx:"OLLYDBG.EXE"
00401366 | 68 74524000          | push bugger.405274                         | 405274:"[System Process]"
0040136B | E8 D8000000          | call <JMP.&_lstrcmpStub@8>                 |
00401370 | 0BC0                 | or eax,eax                                 |
00401372 | 75 2D                | jne bugger.4013A1                          |
00401374 | FF35 58524000        | push dword ptr ds:[405258]                 |
0040137A | 6A 01                | push 1                                     |
0040137C | 68 FF0F1F00          | push 1F0FFF                                |
00401381 | E8 AA000000          | call <JMP.&_OpenProcessStub@12>            |
00401386 | A3 80534000          | mov dword ptr ds:[405380],eax              |
0040138B | 6A 00                | push 0                                     |
0040138D | FF35 80534000        | push dword ptr ds:[405380]                 |
00401393 | E8 AA000000          | call <JMP.&_TerminateProcessStub@8>        |
00401398 | 6A 00                | push 0                                     |
0040139A | E8 85000000          | call <JMP.&_ExitProcessImplementation@4>   |
0040139F | EB 10                | jmp bugger.4013B1                          |
004013A1 | 68 50524000          | push bugger.405250                         |
004013A6 | FF35 78534000        | push dword ptr ds:[405378]                 |
004013AC | E8 8B000000          | call <JMP.&_Process32Next@8>               |
004013B1 | EB 9B                | jmp bugger.40134E                          |
```

再看下`4013B3`，再查一遍od窗体名，如果没找到就把密文`Willbeharder`交出来，否则退出进程

```assembly
004013B3 | 55                   | push ebp                                   |
004013B4 | 8BEC                 | mov ebp,esp                                |
004013B6 | 51                   | push ecx                                   |
004013B7 | 6A 00                | push 0                                     |
004013B9 | 68 4661C479          | push 79C46146                              |
004013BE | 68 CDE9540C          | push C54E9CD                               |
004013C3 | 68 7971D6CC          | push CCD67179                              |
004013C8 | 68 A453F425          | push 25F453A4                              |
004013CD | 54                   | push esp                                   |
004013CE | 68 F0504000          | push bugger.4050F0                         |
004013D3 | E8 BDFCFFFF          | call bugger.401095                         |
004013D8 | 8BCC                 | mov ecx,esp                                |
004013DA | 6A 00                | push 0                                     |
004013DC | 51                   | push ecx                                   |
004013DD | E8 24000000          | call <JMP.&_FindWindowA@8>                 |
004013E2 | 83F8 00              | cmp eax,0                                  |
004013E5 | 0BC0                 | or eax,eax                                 |
004013E7 | 74 09                | je bugger.4013F2                           |
004013E9 | 6A 00                | push 0                                     |
004013EB | E8 34000000          | call <JMP.&_ExitProcessImplementation@4>   |
004013F0 | EB 0F                | jmp bugger.401401                          |
004013F2 | 68 B7504000          | push bugger.4050B7                         | 4050B7:"Willbeharder"
004013F7 | 68 F0504000          | push bugger.4050F0                         |
004013FC | E8 94FCFFFF          | call bugger.401095                         |
00401401 | 59                   | pop ecx                                    |
00401402 | C9                   | leave                                      |
00401403 | C2 0400              | ret 4                                      |
```

要Patch的话

```assembly
1.
004012CF | 74 07                | je bugger.4012D8                           |
NOP掉

2.
0040133B | 74 07                | je bugger.401344                           |
↓ Patch
0040133B | EB 07                | jmp bugger.401344                          |

3.
004013E7 | 74 09                | je bugger.4013F2                           |
↓ Patch
004013E7 | EB 09                | jmp bugger_patched.4013F2                  |
```

可在`00401401`处设置断点




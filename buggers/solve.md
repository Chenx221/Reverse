程序最高只能在Windows XP系统上运行

这个程序感觉和昨天的很像，但兼容性上稍差一些

解决方法：

↓

细节：

EP下来是获取kernel32.dll地址

```assembly
00401000 | 8B4C24 24           | mov ecx,dword ptr ss:[esp+24]             | [esp+24]:ValidateLocale+2B0
00401004 | 49                  | dec ecx                                   |
00401005 | 0FB751 3C           | movzx edx,word ptr ds:[ecx+3C]            | edx:KiFastSystemCallRet
00401009 | 3B4C0A 34           | cmp ecx,dword ptr ds:[edx+ecx+34]         |
0040100D | 75 F5               | jne buggers.401004                        |
0040100F | 890D 30314000       | mov dword ptr ds:[403130],ecx             |
```

这里搜寻`GetProcAddress`

```assembly
0040103D | 8B3E                | mov edi,dword ptr ds:[esi]                | Search "GetProcAddress"
0040103F | 033D 30314000       | add edi,dword ptr ds:[403130]             |
00401045 | 68 04304000         | push buggers.403004                       | 403004:"GetProcAddress"
0040104A | 57                  | push edi                                  |
0040104B | E8 F0010000         | call buggers.401240                       |
00401050 | 0BC0                | or eax,eax                                |
00401052 | 74 48               | je buggers.40109C                         |
00401054 | 8B3D 04314000       | mov edi,dword ptr ds:[403104]             |
0040105A | 8B77 24             | mov esi,dword ptr ds:[edi+24]             |
0040105D | 8B0D 00304000       | mov ecx,dword ptr ds:[403000]             |
00401063 | 41                  | inc ecx                                   |
00401064 | D1E1                | shl ecx,1                                 |
00401066 | 03F1                | add esi,ecx                               |
00401068 | 0335 30314000       | add esi,dword ptr ds:[403130]             |
0040106E | 33C0                | xor eax,eax                               |
00401070 | 66:8B06             | mov ax,word ptr ds:[esi]                  |
00401073 | 8B77 1C             | mov esi,dword ptr ds:[edi+1C]             |
00401076 | 0335 30314000       | add esi,dword ptr ds:[403130]             |
0040107C | 48                  | dec eax                                   |
0040107D | C1E0 02             | shl eax,2                                 |
00401080 | 03F0                | add esi,eax                               |
00401082 | 8B3E                | mov edi,dword ptr ds:[esi]                |
00401084 | 033D 30314000       | add edi,dword ptr ds:[403130]             |
0040108A | 893D 08314000       | mov dword ptr ds:[<&GetProcAddress>],edi  |
00401090 | A1 30314000         | mov eax,dword ptr ds:[403130]             |
00401095 | A3 30314000         | mov dword ptr ds:[403130],eax             |
0040109A | EB 18               | jmp buggers.4010B4                        |
0040109C | FF05 00304000       | inc dword ptr ds:[403000]                 |
004010A2 | FF0D 00314000       | dec dword ptr ds:[403100]                 |
004010A8 | 83C6 04             | add esi,4                                 |
004010AB | 833D 00314000 00    | cmp dword ptr ds:[403100],0               |
004010B2 | 77 89               | ja buggers.40103D                         |
```

`FreeLibrary`和`LoadLibraryA` 

```assembly
004010B4 | 68 20304000         | push buggers.403020                       | 403020:"FreeLibrary"
004010B9 | FF35 30314000       | push dword ptr ds:[403130]                | kernel32
004010BF | FF15 08314000       | call dword ptr ds:[<&GetProcAddress>]     |
004010C5 | A3 0C314000         | mov dword ptr ds:[<&FreeLibrary>],eax     |
004010CA | 68 13304000         | push buggers.403013                       | 403013:"LoadLibraryA"
004010CF | FF35 30314000       | push dword ptr ds:[403130]                |
004010D5 | FF15 08314000       | call dword ptr ds:[<&GetProcAddress>]     |
004010DB | 8BD8                | mov ebx,eax                               | ebx:&L"=::=::\\"
```

加载user32.dll，然后搜寻函数地址

这里的意图很明显了

`"CreateToolhelp32Snapshot", "OpenProcess", "Process32First", "Process32Next", "TerminateProcess", "lstrcmpA", "FindWindowA"`

```assembly
004010DD | BE A2304000         | mov esi,buggers.4030A2                    | 4030A2:"user32.dll"
004010E2 | BF 2C314000         | mov edi,buggers.40312C                    |
004010E7 | 8A06                | mov al,byte ptr ds:[esi]                  |
004010E9 | 0AC0                | or al,al                                  |
004010EB | 74 14               | je buggers.401101                         |
004010ED | 56                  | push esi                                  |
004010EE | FFD3                | call ebx                                  |
004010F0 | 8907                | mov dword ptr ds:[edi],eax                |
004010F2 | 83C7 04             | add edi,4                                 |
004010F5 | 56                  | push esi                                  |
004010F6 | E8 05010000         | call buggers.401200                       |
004010FB | 8D7430 01           | lea esi,dword ptr ds:[eax+esi+1]          |
004010FF | EB E6               | jmp buggers.4010E7                        |
00401101 | BF 2C304000         | mov edi,buggers.40302C                    | 40302C:"01@"
00401106 | BE 10314000         | mov esi,<buggers.&CreateToolhelp32Snapsho |
0040110B | 8B07                | mov eax,dword ptr ds:[edi]                | 获取各类函数地址，检测od用
0040110D | 0BC0                | or eax,eax                                |
0040110F | 74 29               | je buggers.40113A                         |
00401111 | 8B08                | mov ecx,dword ptr ds:[eax]                |
00401113 | 8BD9                | mov ebx,ecx                               | ebx:&L"=::=::\\"
00401115 | 83C7 04             | add edi,4                                 |
00401118 | 8A07                | mov al,byte ptr ds:[edi]                  |
0040111A | 0AC0                | or al,al                                  |
0040111C | 75 03               | jne buggers.401121                        |
0040111E | 47                  | inc edi                                   |
0040111F | EB EA               | jmp buggers.40110B                        |
00401121 | 57                  | push edi                                  |
00401122 | 53                  | push ebx                                  | ebx:&L"=::=::\\"
00401123 | FF15 08314000       | call dword ptr ds:[<&GetProcAddress>]     |
00401129 | 8906                | mov dword ptr ds:[esi],eax                |
0040112B | 83C6 04             | add esi,4                                 |
0040112E | 57                  | push edi                                  |
0040112F | E8 CC000000         | call buggers.401200                       |
00401134 | 8D7C38 01           | lea edi,dword ptr ds:[eax+edi+1]          | eax+edi*1+01:"Process32First"
00401138 | EB DE               | jmp buggers.401118                        |
```

检测OD窗体（没检测到就退出）

```assembly
0040113A | 5B                  | pop ebx                                   | ebx:&L"=::=::\\"
0040113B | 5F                  | pop edi                                   |
0040113C | 5E                  | pop esi                                   |
0040113D | B8 28010000         | mov eax,128                               |
00401142 | A3 34314000         | mov dword ptr ds:[403134],eax             |
00401147 | 6A 00               | push 0                                    |
00401149 | 6A 02               | push 2                                    |
0040114B | FF15 10314000       | call dword ptr ds:[<&CreateToolhelp32Snap |
00401151 | A3 5C324000         | mov dword ptr ds:[40325C],eax             |
00401156 | 68 34314000         | push buggers.403134                       |
0040115B | 50                  | push eax                                  |
0040115C | FF15 18314000       | call dword ptr ds:[<&Process32First>]     |
00401162 | 6A 00               | push 0                                    |
00401164 | 68 AE304000         | push buggers.4030AE                       | 4030AE:"OLLYDBG"
00401169 | FF15 28314000       | call dword ptr ds:[<&FindWindowA>]        |
0040116F | 83F8 00             | cmp eax,0                                 |
00401172 | 0BC0                | or eax,eax                                |
00401174 | 74 04               | je buggers.40117A                         |
00401176 | 7C 27               | jl buggers.40119F                         |
00401178 | EB 25               | jmp buggers.40119F                        |
0040117A | 50                  | push eax                                  | Not Found "OLLYDBG"
0040117B | 56                  | push esi                                  |
0040117C | 57                  | push edi                                  |
0040117D | BF 01000000         | mov edi,1                                 |
00401182 | BE 2C314000         | mov esi,buggers.40312C                    |
00401187 | FF36                | push dword ptr ds:[esi]                   |
00401189 | FF15 0C314000       | call dword ptr ds:[<&FreeLibrary>]        |
0040118F | 83C6 04             | add esi,4                                 |
00401192 | 4F                  | dec edi                                   |
00401193 | 75 F2               | jne buggers.401187                        |
00401195 | 5F                  | pop edi                                   |
00401196 | 5E                  | pop esi                                   |
00401197 | 58                  | pop eax                                   |
00401198 | 6A 00               | push 0                                    |
0040119A | E8 57000000         | call <JMP.&ExitProcess>                   | Bye
```

检测OD进程，有则干掉进程，然后退出

```assembly
0040119F | 68 B6304000         | push buggers.4030B6                       | 4030B6:"OLLYDBG.EXE"
004011A4 | 68 58314000         | push buggers.403158                       | 403158:"OLLYDBG.EXE"
004011A9 | FF15 24314000       | call dword ptr ds:[<&lstrcmp>]            |
004011AF | 0BC0                | or eax,eax                                |
004011B1 | 75 2F               | jne buggers.4011E2                        |
004011B3 | FF35 3C314000       | push dword ptr ds:[40313C]                |
004011B9 | 6A 01               | push 1                                    |
004011BB | 68 FF0F1F00         | push 1F0FFF                               |
004011C0 | FF15 14314000       | call dword ptr ds:[<&OpenProcess>]        |
004011C6 | A3 64324000         | mov dword ptr ds:[403264],eax             |
004011CB | 6A 00               | push 0                                    |
004011CD | FF35 64324000       | push dword ptr ds:[403264]                |
004011D3 | FF15 20314000       | call dword ptr ds:[<&TerminateProcess>]   | Kill OD
004011D9 | 6A 00               | push 0                                    |
004011DB | E8 16000000         | call <JMP.&ExitProcess>                   | Bye
004011E0 | EB 11               | jmp buggers.4011F3                        |
004011E2 | 68 34314000         | push buggers.403134                       |
004011E7 | FF35 5C324000       | push dword ptr ds:[40325C]                |
004011ED | FF15 1C314000       | call dword ptr ds:[<&Process32Next>]      |
004011F3 | EB AA               | jmp buggers.40119F                        |
004011F5 | CC                  | int3                                      |
004011F6 | FF25 00204000       | jmp dword ptr ds:[<ExitProcess>]          |
```

好像没有一个目标？

那就不Patch了

OD用户可以nop掉

```assembly
00401174 | 74 04               | je buggers.40117A                         |
00401176 | 7C 27               | jl buggers.40119F                         |
```

这个想咋整就咋整，反正这个crackme没有成功目标

```assembly
004011B3 | FF35 3C314000       | push dword ptr ds:[40313C]                |
004011B9 | 6A 01               | push 1                                    |
004011BB | 68 FF0F1F00         | push 1F0FFF                               |
004011C0 | FF15 14314000       | call dword ptr ds:[<&OpenProcess>]        |
004011C6 | A3 64324000         | mov dword ptr ds:[403264],eax             |
004011CB | 6A 00               | push 0                                    |
004011CD | FF35 64324000       | push dword ptr ds:[403264]                |
004011D3 | FF15 20314000       | call dword ptr ds:[<&TerminateProcess>]   | Kill OD
```


serial计算方法

1. 首先看到程序的printf、scanf部分

   ```assembly
   00401428 | 83C4 10          | add esp,10                              |
   0040142B | 83C4 F4          | add esp,FFFFFFF4                        |
   0040142E | 68 70124000      | push babylon keygenme.401270            | 401270:"[x][x] Babylon KeygenMe [x][x] coded by haiklr\n\n"
   00401433 | E8 48040000      | call <JMP.&_printf>                     |
   00401438 | 83C4 10          | add esp,10                              |
   0040143B | 83C4 F4          | add esp,FFFFFFF4                        |
   0040143E | 68 A1124000      | push babylon keygenme.4012A1            | 4012A1:"[x] Name : "
   00401443 | E8 38040000      | call <JMP.&_printf>                     |
   00401448 | 83C4 10          | add esp,10                              |
   0040144B | 83C4 F8          | add esp,FFFFFFF8                        |
   0040144E | 8D85 E0FEFFFF    | lea eax,dword ptr ss:[ebp-120]          |
   00401454 | 50               | push eax                                |
   00401455 | 68 AD124000      | push babylon keygenme.4012AD            | 4012AD:"%s"
   0040145A | E8 19040000      | call <JMP.&_scanf>                      |
   ```

   这里让用户输入Name值

2. 接下来是对Name长度的检查，可以看到Name要求4~14位长度

   ```assembly
   0040145F | 83C4 10          | add esp,10                              |
   00401462 | 83C4 F4          | add esp,FFFFFFF4                        |
   00401465 | 8D85 E0FEFFFF    | lea eax,dword ptr ss:[ebp-120]          | ebp-120: Name
   0040146B | 50               | push eax                                |
   0040146C | E8 FF030000      | call <JMP.&_strlen>                     |
   00401471 | 83C4 10          | add esp,10                              |
   00401474 | 8945 E8          | mov dword ptr ss:[ebp-18],eax           | ebp-18: Name.Length
   00401477 | 837D E8 03       | cmp dword ptr ss:[ebp-18],3             |
   0040147B | 7E 08            | jle babylon keygenme.401485             | length > 3
   0040147D | 837D E8 0E       | cmp dword ptr ss:[ebp-18],E             |
   00401481 | 7F 02            | jg babylon keygenme.401485              | length <=14
   00401483 | EB 2B            | jmp babylon keygenme.4014B0             |
   00401485 | 83C4 F4          | add esp,FFFFFFF4                        | fail
   ...
   004014A7 | E9 B4020000      | jmp <babylon keygenme.Bye>              |
   004014AC | 8D7426 00        | lea esi,dword ptr ds:[esi]              | esi:"悙悙悙悙悙悙悙悙悙悙悙悙悙悙怺x][x] Babylon KeygenMe [x][x] coded by haiklr\n\n"
   004014B0 | 83C4 F4          | add esp,FFFFFFF4                        |
   ```

3. 接收用户输入的Serial

   ```assembly
   004014B3 | 68 D2124000      | push babylon keygenme.4012D2            | 4012D2:"[x] Serial : "
   004014B8 | E8 C3030000      | call <JMP.&_printf>                     |
   004014BD | 83C4 10          | add esp,10                              |
   004014C0 | 83C4 F8          | add esp,FFFFFFF8                        |
   004014C3 | 8D85 A0FAFFFF    | lea eax,dword ptr ss:[ebp-560]          | ebp-560: Serial
   004014C9 | 50               | push eax                                |
   004014CA | 68 AD124000      | push babylon keygenme.4012AD            | 4012AD:"%s"
   004014CF | E8 A4030000      | call <JMP.&_scanf>                      |
   ```

4. 接下来开始计算正确的Serial，首先在每位name字符间加入空格

   ```assembly
   004014D7 | C745 FC 00000000 | mov dword ptr ss:[ebp-4],0              |
   004014DE | 89F6             | mov esi,esi                             |
   004014E0 | 8B45 E8          | mov eax,dword ptr ss:[ebp-18]           | name每个字符之间插入0x20空格(包括末尾)
   004014E3 | 89C2             | mov edx,eax                             | edx: Name length
   004014E5 | 8D0412           | lea eax,dword ptr ds:[edx+edx]          |
   004014E8 | 3945 FC          | cmp dword ptr ss:[ebp-4],eax            | 循环条件，ebp-4<2*edx
   004014EB | 7C 03            | jl babylon keygenme.4014F0              |
   004014ED | EB 31            | jmp babylon keygenme.401520             |
   004014EF | 90               | nop                                     |
   004014F0 | 8D85 A0FDFFFF    | lea eax,dword ptr ss:[ebp-260]          |
   004014F6 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   004014F9 | 8D8D E0FEFFFF    | lea ecx,dword ptr ss:[ebp-120]          | ecx:_printf+67
   004014FF | 8B5D F8          | mov ebx,dword ptr ss:[ebp-8]            |
   00401502 | 8A0C0B           | mov cl,byte ptr ds:[ebx+ecx]            |
   00401505 | 880C02           | mov byte ptr ds:[edx+eax],cl            |
   00401508 | 8B45 FC          | mov eax,dword ptr ss:[ebp-4]            |
   0040150B | 40               | inc eax                                 |
   0040150C | 8D95 A0FDFFFF    | lea edx,dword ptr ss:[ebp-260]          | edx:_KiFastSystemCallRet@0
   00401512 | C60410 20        | mov byte ptr ds:[eax+edx],20            | 20:' '
   00401516 | FF45 F8          | inc dword ptr ss:[ebp-8]                |
   00401519 | 8345 FC 02       | add dword ptr ss:[ebp-4],2              |
   0040151D | EB C1            | jmp babylon keygenme.4014E0             |
   0040151F | 90               | nop                                     |
   00401520 | 90               | nop                                     |
   ```

5. 这里出场了第二组数据，现在只是每位+1

   ```assembly
   00401521 | C745 FC 00000000 | mov dword ptr ss:[ebp-4],0              |
   00401528 | 83C4 F4          | add esp,FFFFFFF4                        | 将某串字符串（-[#]]=}&&&+(=$*,,)&.*/+++[][;/..§0)每个字符+1
   0040152B | 8D85 A0FEFFFF    | lea eax,dword ptr ss:[ebp-160]          |
   00401531 | 50               | push eax                                |
   00401532 | E8 39030000      | call <JMP.&_strlen>                     |
   00401537 | 83C4 10          | add esp,10                              |
   0040153A | 89C0             | mov eax,eax                             |
   0040153C | 3945 FC          | cmp dword ptr ss:[ebp-4],eax            |
   0040153F | 72 02            | jb babylon keygenme.401543              |
   00401541 | EB 2D            | jmp babylon keygenme.401570             |
   00401543 | 8D85 A0FEFFFF    | lea eax,dword ptr ss:[ebp-160]          |
   00401549 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   0040154C | 8D8D A0FEFFFF    | lea ecx,dword ptr ss:[ebp-160]          | ecx:_printf+67
   00401552 | 8B5D FC          | mov ebx,dword ptr ss:[ebp-4]            |
   00401555 | 899D 9CFAFFFF    | mov dword ptr ss:[ebp-564],ebx          |
   0040155B | 8BB5 9CFAFFFF    | mov esi,dword ptr ss:[ebp-564]          |
   00401561 | 8A1C0E           | mov bl,byte ptr ds:[esi+ecx]            |
   00401564 | FEC3             | inc bl                                  |
   00401566 | 881C02           | mov byte ptr ds:[edx+eax],bl            |
   00401569 | FF45 FC          | inc dword ptr ss:[ebp-4]                |
   0040156C | EB BA            | jmp babylon keygenme.401528             |
   0040156E | 89F6             | mov esi,esi                             |
   00401570 | 90               | nop                                     |
   ```

6. 第二组数据 Xor 加空格的Name

   ```assembly
   00401571 | C745 FC 00000000 | mov dword ptr ss:[ebp-4],0              |
   00401578 | 83C4 F4          | add esp,FFFFFFF4                        | 加了空格的Name Xor 加了1的奇怪字符串 放字符串原位
   0040157B | 8D85 A0FDFFFF    | lea eax,dword ptr ss:[ebp-260]          |
   00401581 | 50               | push eax                                |
   00401582 | E8 E9020000      | call <JMP.&_strlen>                     |
   00401587 | 83C4 10          | add esp,10                              |
   0040158A | 89C0             | mov eax,eax                             |
   0040158C | 3945 FC          | cmp dword ptr ss:[ebp-4],eax            |
   0040158F | 72 02            | jb babylon keygenme.401593              |
   00401591 | EB 41            | jmp babylon keygenme.4015D4             |
   00401593 | 8D85 A0FEFFFF    | lea eax,dword ptr ss:[ebp-160]          |
   00401599 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   0040159C | 8D8D A0FDFFFF    | lea ecx,dword ptr ss:[ebp-260]          | ecx:_printf+67
   004015A2 | 8B7D FC          | mov edi,dword ptr ss:[ebp-4]            |
   004015A5 | 89BD 9CFAFFFF    | mov dword ptr ss:[ebp-564],edi          |
   004015AB | 8DB5 A0FEFFFF    | lea esi,dword ptr ss:[ebp-160]          |
   004015B1 | 8B5D FC          | mov ebx,dword ptr ss:[ebp-4]            |
   004015B4 | 899D 8CFAFFFF    | mov dword ptr ss:[ebp-574],ebx          |
   004015BA | 8BBD 9CFAFFFF    | mov edi,dword ptr ss:[ebp-564]          |
   004015C0 | 8A1C0F           | mov bl,byte ptr ds:[edi+ecx]            |
   004015C3 | 8BBD 8CFAFFFF    | mov edi,dword ptr ss:[ebp-574]          |
   004015C9 | 321C37           | xor bl,byte ptr ds:[edi+esi]            |
   004015CC | 881C02           | mov byte ptr ds:[edx+eax],bl            |
   004015CF | FF45 FC          | inc dword ptr ss:[ebp-4]                |
   004015D2 | EB A4            | jmp babylon keygenme.401578             |
   004015D4 | 90               | nop                                     |
   ```

7. 反转上一步的结果

   ```assembly
   004015D8 | 8D85 A0FEFFFF    | lea eax,dword ptr ss:[ebp-160]          |
   004015DE | 50               | push eax                                |
   004015DF | E8 8C020000      | call <JMP.&_strlen>                     |
   004015E4 | 83C4 10          | add esp,10                              |
   004015E7 | 89C0             | mov eax,eax                             |
   004015E9 | 8D50 FF          | lea edx,dword ptr ds:[eax-1]            | edx:_KiFastSystemCallRet@0
   004015EC | 8955 FC          | mov dword ptr ss:[ebp-4],edx            | edx:_KiFastSystemCallRet@0
   004015EF | 90               | nop                                     |
   004015F0 | 837D FC 00       | cmp dword ptr ss:[ebp-4],0              | 反转
   004015F4 | 7D 02            | jge babylon keygenme.4015F8             |
   004015F6 | EB 20            | jmp babylon keygenme.401618             |
   004015F8 | 8D85 A0FCFFFF    | lea eax,dword ptr ss:[ebp-360]          |
   004015FE | 8B55 F4          | mov edx,dword ptr ss:[ebp-C]            | edx:_KiFastSystemCallRet@0
   00401601 | 8D8D A0FEFFFF    | lea ecx,dword ptr ss:[ebp-160]          | ecx:_printf+67
   00401607 | 8B5D FC          | mov ebx,dword ptr ss:[ebp-4]            |
   0040160A | 8A0C0B           | mov cl,byte ptr ds:[ebx+ecx]            |
   0040160D | 880C02           | mov byte ptr ds:[edx+eax],cl            |
   00401610 | FF45 F4          | inc dword ptr ss:[ebp-C]                |
   00401613 | FF4D FC          | dec dword ptr ss:[ebp-4]                |
   00401616 | EB D8            | jmp babylon keygenme.4015F0             |
   00401618 | 90               | nop                                     |
   ```

8. 反转和没反转的插空放 (如:反转前"abcd" 反转后"dcba" 结果"dacb")

   ```assembly
   00401619 | C745 FC 00000000 | mov dword ptr ss:[ebp-4],0              |
   00401620 | 83C4 F4          | add esp,FFFFFFF4                        | 将前面未反转的内容插空放进反转内容中
   00401623 | 8D85 A0FCFFFF    | lea eax,dword ptr ss:[ebp-360]          |
   00401629 | 50               | push eax                                |
   0040162A | E8 41020000      | call <JMP.&_strlen>                     |
   0040162F | 83C4 10          | add esp,10                              |
   00401632 | 89C0             | mov eax,eax                             |
   00401634 | 3945 FC          | cmp dword ptr ss:[ebp-4],eax            |
   00401637 | 72 07            | jb babylon keygenme.401640              |
   00401639 | EB 45            | jmp babylon keygenme.401680             |
   0040163B | 90               | nop                                     |
   0040163C | 8D7426 00        | lea esi,dword ptr ds:[esi]              | esi:"悙悙悙悙悙悙悙悙悙悙悙悙悙悙怺x][x] Babylon KeygenMe [x][x] coded by haiklr\n\n"
   00401640 | 8D85 A0FBFFFF    | lea eax,dword ptr ss:[ebp-460]          |
   00401646 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   00401649 | 8D8D A0FCFFFF    | lea ecx,dword ptr ss:[ebp-360]          | ecx:_printf+67
   0040164F | 8B5D F0          | mov ebx,dword ptr ss:[ebp-10]           |
   00401652 | 8A0C0B           | mov cl,byte ptr ds:[ebx+ecx]            |
   00401655 | 880C02           | mov byte ptr ds:[edx+eax],cl            |
   00401658 | 8B45 FC          | mov eax,dword ptr ss:[ebp-4]            |
   0040165B | 40               | inc eax                                 |
   0040165C | 8D95 A0FBFFFF    | lea edx,dword ptr ss:[ebp-460]          | edx:_KiFastSystemCallRet@0
   00401662 | 8D8D A0FEFFFF    | lea ecx,dword ptr ss:[ebp-160]          | ecx:_printf+67
   00401668 | 8B5D EC          | mov ebx,dword ptr ss:[ebp-14]           |
   0040166B | 8A0C0B           | mov cl,byte ptr ds:[ebx+ecx]            |
   0040166E | 880C10           | mov byte ptr ds:[eax+edx],cl            |
   00401671 | FF45 F0          | inc dword ptr ss:[ebp-10]               |
   00401674 | FF45 EC          | inc dword ptr ss:[ebp-14]               |
   00401677 | 8345 FC 02       | add dword ptr ss:[ebp-4],2              |
   0040167B | EB A3            | jmp babylon keygenme.401620             |
   0040167D | 8D76 00          | lea esi,dword ptr ds:[esi]              | esi:"悙悙悙悙悙悙悙悙悙悙悙悙悙悙怺x][x] Babylon KeygenMe [x][x] coded by haiklr\n\n"
   00401680 | 90               | nop                                     |
   ```

9. 将不可打印字符转成0x36

   ```assembly
   00401681 | C745 FC 00000000 | mov dword ptr ss:[ebp-4],0              |
   00401688 | 83C4 F4          | add esp,FFFFFFF4                        | 把所有不可打印字符替换成0x36
   0040168B | 8D85 A0FBFFFF    | lea eax,dword ptr ss:[ebp-460]          |
   00401691 | 50               | push eax                                |
   00401692 | E8 D9010000      | call <JMP.&_strlen>                     |
   00401697 | 83C4 10          | add esp,10                              |
   0040169A | 89C0             | mov eax,eax                             |
   0040169C | 3945 FC          | cmp dword ptr ss:[ebp-4],eax            |
   0040169F | 72 02            | jb babylon keygenme.4016A3              |
   004016A1 | EB 32            | jmp babylon keygenme.4016D5             |
   004016A3 | 8D85 A0FBFFFF    | lea eax,dword ptr ss:[ebp-460]          |
   004016A9 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   004016AC | 803C02 1F        | cmp byte ptr ds:[edx+eax],1F            | <=0x1F则替换成0x36
   004016B0 | 7E 11            | jle babylon keygenme.4016C3             |
   004016B2 | 8D85 A0FBFFFF    | lea eax,dword ptr ss:[ebp-460]          |
   004016B8 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   004016BB | 803C02 7A        | cmp byte ptr ds:[edx+eax],7A            | 7A:'z'
   004016BF | 7F 02            | jg babylon keygenme.4016C3              | >0x7A则替换成0x36
   004016C1 | EB 0D            | jmp babylon keygenme.4016D0             |
   004016C3 | 8D85 A0FBFFFF    | lea eax,dword ptr ss:[ebp-460]          |
   004016C9 | 8B55 FC          | mov edx,dword ptr ss:[ebp-4]            | edx:_KiFastSystemCallRet@0
   004016CC | C60402 36        | mov byte ptr ds:[edx+eax],36            | 36:'6'
   004016D0 | FF45 FC          | inc dword ptr ss:[ebp-4]                |
   004016D3 | EB B3            | jmp babylon keygenme.401688             |
   004016D5 | 90               | nop                                     |
   ```

判断就不放了
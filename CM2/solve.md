算密钥

先来一组可用密钥：

```
chenx221
48-000002AB-GA
```

*此crackme在win8.1及以上的系统上存在兼容性问题，判断serial时会崩溃。`KeyGenMe_#1_cLoNeTrOnE.exe+1321`前缺少对edx进行清理

密钥由以下部分组成

1. `keyDatabase[name[0] % 0x10]`
2. `keyDatabase[name[1] % 0x10]`
3. `-`
4. `Name的Ascii码总和 (%.8X格式化输出)`
5. `-`
6. `keyDatabase[name[^2] % 0x10]`
7. `keyDatabase[name[^1] % 0x10]`

keyDatabase: `1AG4T3CX8ZF7R95Q`

细节：

```assembly
0040102B | 55               | push ebp                                      | start
0040102C | 8BEC             | mov ebp,esp                                   |
0040102E | 817D 0C 10010000 | cmp dword ptr ss:[ebp+C],110                  |
00401035 | 75 3B            | jne keygenme_#1_clonetrone.401072             |
00401037 | 66:C705 C9324000 | mov word ptr ds:[4032C9],5                    |
00401040 | C605 CB324000 00 | mov byte ptr ds:[4032CB],0                    |
00401047 | FF75 08          | push dword ptr ss:[ebp+8]                     |
0040104A | E8 93030000      | call <JMP.&_UpdateWindow@4>                   |
0040104F | 68 BA314000      | push keygenme_#1_clonetrone.4031BA            |
00401054 | 6A 06            | push 6                                        |
00401056 | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401059 | E8 7E030000      | call <JMP.&_SetDlgItemTextA@12>               |
0040105E | 68 BA314000      | push keygenme_#1_clonetrone.4031BA            |
00401063 | 6A 03            | push 3                                        |
00401065 | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401068 | E8 6F030000      | call <JMP.&_SetDlgItemTextA@12>               |
0040106D | E9 75010000      | jmp keygenme_#1_clonetrone.4011E7             |
00401072 | 837D 0C 10       | cmp dword ptr ss:[ebp+C],10                   |
00401076 | 75 0F            | jne keygenme_#1_clonetrone.401087             |
00401078 | 6A 00            | push 0                                        |
0040107A | FF75 08          | push dword ptr ss:[ebp+8]                     |
0040107D | E8 48030000      | call <JMP.&_EndDialog@8>                      |
00401082 | E9 60010000      | jmp keygenme_#1_clonetrone.4011E7             |
00401087 | 817D 0C 11010000 | cmp dword ptr ss:[ebp+C],111                  |
0040108E | 0F85 4A010000    | jne keygenme_#1_clonetrone.4011DE             |
00401094 | 33C0             | xor eax,eax                                   |
00401096 | 8B45 10          | mov eax,dword ptr ss:[ebp+10]                 | [ebp+10]:EntryPoint
00401099 | C1E8 10          | shr eax,10                                    |
0040109C | 0BC0             | or eax,eax                                    |
0040109E | 0F85 43010000    | jne keygenme_#1_clonetrone.4011E7             |
004010A4 | 33C0             | xor eax,eax                                   |
004010A6 | 8B45 10          | mov eax,dword ptr ss:[ebp+10]                 | [ebp+10]:EntryPoint
004010A9 | C1C0 10          | rol eax,10                                    |
004010AC | C1E8 10          | shr eax,10                                    |
004010AF | 83F8 01          | cmp eax,1                                     |
004010B2 | 0F85 E7000000    | jne keygenme_#1_clonetrone.40119F             |
004010B8 | 66:FF0D C9324000 | dec word ptr ds:[4032C9]                      | CheckBtn
004010BF | 6A 50            | push 50                                       |
004010C1 | 68 54324000      | push <keygenme_#1_clonetrone.Name>            | Name
004010C6 | 6A 03            | push 3                                        |
004010C8 | FF75 08          | push dword ptr ss:[ebp+8]                     |
004010CB | E8 00030000      | call <JMP.&_GetDlgItemTextA@16>               |
004010D0 | A3 B8324000      | mov dword ptr ds:[<Name.length>],eax          | length
004010D5 | 6A 14            | push 14                                       |
004010D7 | 68 A4324000      | push <keygenme_#1_clonetrone.Serial>          | Serial
004010DC | 6A 06            | push 6                                        |
004010DE | FF75 08          | push dword ptr ss:[ebp+8]                     |
004010E1 | E8 EA020000      | call <JMP.&_GetDlgItemTextA@16>               |
004010E6 | A3 BC324000      | mov dword ptr ds:[<Serial.length>],eax        | length
004010EB | FF75 08          | push dword ptr ss:[ebp+8]                     |
004010EE | E8 EF020000      | call <JMP.&_UpdateWindow@4>                   |
004010F3 | FF35 B8324000    | push dword ptr ds:[<Name.length>]             |
004010F9 | 68 54324000      | push <keygenme_#1_clonetrone.Name>            |
004010FE | E8 76020000      | call <keygenme_#1_clonetrone.isAscii>         |
00401103 | 833D B8324000 00 | cmp dword ptr ds:[<Name.length>],0            |
0040110A | 75 14            | jne keygenme_#1_clonetrone.401120             | check if name is empty
0040110C | 68 BB314000      | push keygenme_#1_clonetrone.4031BB            | 4031BB:"There is nothing here to be processed."
00401111 | 6A 03            | push 3                                        |
00401113 | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401116 | E8 C1020000      | call <JMP.&_SetDlgItemTextA@12>               |
0040111B | E9 9E000000      | jmp keygenme_#1_clonetrone.4011BE             |
00401120 | 833D B8324000 04 | cmp dword ptr ds:[<Name.length>],4            |
00401127 | 73 14            | jae keygenme_#1_clonetrone.40113D             | check if name length > 4
00401129 | 68 0B324000      | push keygenme_#1_clonetrone.40320B            | 40320B:"Name was too short. Put more than 3 chars"
0040112E | 6A 03            | push 3                                        |
00401130 | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401133 | E8 A4020000      | call <JMP.&_SetDlgItemTextA@12>               |
00401138 | E9 81000000      | jmp keygenme_#1_clonetrone.4011BE             |
0040113D | 833D B8324000 3C | cmp dword ptr ds:[<Name.length>],3C           | 3C:'<'
00401144 | 76 11            | jbe keygenme_#1_clonetrone.401157             | check if name length < 60
00401146 | 68 E2314000      | push keygenme_#1_clonetrone.4031E2            | 4031E2:"Name was too long, buffer will overflow."
0040114B | 6A 03            | push 3                                        |
0040114D | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401150 | E8 87020000      | call <JMP.&_SetDlgItemTextA@12>               |
00401155 | EB 67            | jmp keygenme_#1_clonetrone.4011BE             |
00401157 | 0BC0             | or eax,eax                                    |
00401159 | 75 11            | jne keygenme_#1_clonetrone.40116C             | check if name contains illegal char (!ascii)
0040115B | 68 00304000      | push keygenme_#1_clonetrone.403000            | 403000:"The Name contains invalid ASCII char [>127]."
00401160 | 6A 03            | push 3                                        |
00401162 | FF75 08          | push dword ptr ss:[ebp+8]                     |
00401165 | E8 72020000      | call <JMP.&_SetDlgItemTextA@12>               |
0040116A | EB 52            | jmp keygenme_#1_clonetrone.4011BE             |
0040116C | FF75 08          | push dword ptr ss:[ebp+8]                     |
0040116F | E8 EE000000      | call <keygenme_#1_clonetrone.CheckSerial>     |
00401174 | 803D CB324000 00 | cmp byte ptr ds:[4032CB],0                    |
0040117B | 75 19            | jne keygenme_#1_clonetrone.401196             |
0040117D | 68 10100000      | push 1010                                     | Fail
00401182 | 68 C6304000      | push keygenme_#1_clonetrone.4030C6            | 4030C6:"Invalid Serial - Serial Rejected"
00401187 | 68 7C304000      | push keygenme_#1_clonetrone.40307C            | 40307C:"Wrong Serial. The Serial Is Case-Sensitive. Try Again. Never Give Up !!!."
0040118C | FF75 08          | push dword ptr ss:[ebp+8]                     |
0040118F | E8 42020000      | call <JMP.&_MessageBoxA@16>                   |
00401194 | EB 07            | jmp keygenme_#1_clonetrone.40119D             |
```

```assembly
00401379 | 55               | push ebp                                      | isAscii (string,len)
0040137A | 8BEC             | mov ebp,esp                                   | EAX: True->1, False->0
0040137C | 51               | push ecx                                      |
0040137D | 52               | push edx                                      |
0040137E | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
00401380 | 33C9             | xor ecx,ecx                                   |
00401382 | 33D2             | xor edx,edx                                   |
00401384 | 8B4D 0C          | mov ecx,dword ptr ss:[ebp+C]                  | p1
00401387 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]                  | p0
0040138A | EB 27            | jmp keygenme_#1_clonetrone.4013B3             | Loop
0040138C | 8A10             | mov dl,byte ptr ds:[eax]                      | eax:_TppWorkerThread@4
0040138E | 80FA 7F          | cmp dl,7F                                     |
00401391 | 76 0B            | jbe keygenme_#1_clonetrone.40139E             | char <= ascii 127
00401393 | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
00401395 | B8 00000000      | mov eax,0                                     | eax:_TppWorkerThread@4
0040139A | EB 1B            | jmp keygenme_#1_clonetrone.4013B7             |
0040139C | EB 13            | jmp keygenme_#1_clonetrone.4013B1             |
0040139E | 80FA 80          | cmp dl,80                                     |
004013A1 | 73 0E            | jae keygenme_#1_clonetrone.4013B1             | char >= 128
004013A3 | 83F9 01          | cmp ecx,1                                     |
004013A6 | 75 09            | jne keygenme_#1_clonetrone.4013B1             | l !== 1
004013A8 | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
004013AA | B8 01000000      | mov eax,1                                     | eax:_TppWorkerThread@4
004013AF | EB 06            | jmp keygenme_#1_clonetrone.4013B7             |
004013B1 | 40               | inc eax                                       | eax:_TppWorkerThread@4
004013B2 | 49               | dec ecx                                       |
004013B3 | 0BC9             | or ecx,ecx                                    |
004013B5 | 75 D5            | jne keygenme_#1_clonetrone.40138C             | NextLoop
004013B7 | 5A               | pop edx                                       |
004013B8 | 59               | pop ecx                                       |
004013B9 | C9               | leave                                         |
004013BA | C2 0800          | ret 8                                         |
```

```assembly
00401262 | 55               | push ebp                                      | checkSerial
00401263 | 8BEC             | mov ebp,esp                                   |
00401265 | 60               | pushad                                        |
00401266 | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
00401268 | 33D2             | xor edx,edx                                   |
0040126A | B9 10000000      | mov ecx,10                                    |
0040126F | A0 54324000      | mov al,byte ptr ds:[<Name>]                   |
00401274 | F6F1             | div cl                                        | name[0] / 0x10
00401276 | 8AD4             | mov dl,ah                                     | mod
00401278 | 8A82 35324000    | mov al,byte ptr ds:[edx+<Keydatabase>]        | edx+403235:"1AG4T3CX8ZF7R95Q"
0040127E | 8A15 A4324000    | mov dl,byte ptr ds:[<Serial>]                 |
00401284 | 38D0             | cmp al,dl                                     |
00401286 | 0F85 E8000000    | jne <keygenme_#1_clonetrone.Fail>             | serial[0] !== Keydatabase[name[0] % 0x10]
0040128C | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
0040128E | A0 55324000      | mov al,byte ptr ds:[403255]                   | name[1]
00401293 | F6F1             | div cl                                        |
00401295 | 8AD4             | mov dl,ah                                     |
00401297 | 8A82 35324000    | mov al,byte ptr ds:[edx+<Keydatabase>]        | edx+403235:"1AG4T3CX8ZF7R95Q"
0040129D | 8A15 A5324000    | mov dl,byte ptr ds:[4032A5]                   |
004012A3 | 38D0             | cmp al,dl                                     |
004012A5 | 0F85 C9000000    | jne <keygenme_#1_clonetrone.Fail>             | serial[1] !== Keydatabase[name[1] % 0x10]
004012AB | A0 A6324000      | mov al,byte ptr ds:[4032A6]                   |
004012B0 | 2C 2D            | sub al,2D                                     |
004012B2 | 0F85 BC000000    | jne <keygenme_#1_clonetrone.Fail>             | serial[2] !== '-'
004012B8 | 33D2             | xor edx,edx                                   |
004012BA | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
004012BC | 33C9             | xor ecx,ecx                                   | Sum(name char ascii)
004012BE | 8A8A 54324000    | mov cl,byte ptr ds:[edx+<Name>]               |
004012C4 | 0AC9             | or cl,cl                                      |
004012C6 | 74 05            | je keygenme_#1_clonetrone.4012CD              |
004012C8 | 03C1             | add eax,ecx                                   | eax:_TppWorkerThread@4
004012CA | 42               | inc edx                                       |
004012CB | EB EF            | jmp keygenme_#1_clonetrone.4012BC             |
004012CD | 50               | push eax                                      | Sum
004012CE | 68 46324000      | push keygenme_#1_clonetrone.403246            | 403246:"%.8X"
004012D3 | 68 C0324000      | push <keygenme_#1_clonetrone.NameAscSum.Forma |
004012D8 | E8 E1000000      | call <JMP.&_wsprintfA>                        |
004012DD | 83C4 0C          | add esp,C                                     |
004012E0 | 33C9             | xor ecx,ecx                                   |
004012E2 | EB 11            | jmp keygenme_#1_clonetrone.4012F5             | Check serial[3..] == NameAscSumFormatStr
004012E4 | 8A81 C0324000    | mov al,byte ptr ds:[ecx+<NameAscSum.FormatStr |
004012EA | 8A91 A7324000    | mov dl,byte ptr ds:[ecx+4032A7]               |
004012F0 | 38D0             | cmp al,dl                                     |
004012F2 | 75 09            | jne keygenme_#1_clonetrone.4012FD             | mod:direct fail
004012F4 | 41               | inc ecx                                       |
004012F5 | 83F9 08          | cmp ecx,8                                     |
004012F8 | 75 EA            | jne keygenme_#1_clonetrone.4012E4             | keep
004012FA | 31D2             | xor edx,edx                                   |
004012FC | 90               | nop                                           |
004012FD | 75 75            | jne <keygenme_#1_clonetrone.Fail>             |
004012FF | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
00401301 | A0 AF324000      | mov al,byte ptr ds:[4032AF]                   |
00401306 | 2C 2D            | sub al,2D                                     |
00401308 | 75 6A            | jne <keygenme_#1_clonetrone.Fail>             | serial[11] !== '-'
0040130A | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
0040130C | B9 10000000      | mov ecx,10                                    |
00401311 | 8B1D B8324000    | mov ebx,dword ptr ds:[<Name.length>]          |
00401317 | 8A83 52324000    | mov al,byte ptr ds:[ebx+403252]               | name[^2]
0040131D | F6F1             | div cl                                        |
0040131F | 8AD4             | mov dl,ah                                     |
00401321 | 8A82 35324000    | mov al,byte ptr ds:[edx+<Keydatabase>]        | ??? BUG (forget xor edx,edx?)
00401327 | 8A15 B0324000    | mov dl,byte ptr ds:[4032B0]                   |
0040132D | 38D0             | cmp al,dl                                     |
0040132F | 75 43            | jne <keygenme_#1_clonetrone.Fail>             | serial[12] !== Keydatabase[name[^2] % 0x10]
00401331 | 33C0             | xor eax,eax                                   | eax:_TppWorkerThread@4
00401333 | 8A83 53324000    | mov al,byte ptr ds:[ebx+403253]               | name[^1]
00401339 | F6F1             | div cl                                        |
0040133B | 8AD4             | mov dl,ah                                     |
0040133D | 8A82 35324000    | mov al,byte ptr ds:[edx+<Keydatabase>]        | edx+403235:"1AG4T3CX8ZF7R95Q"
00401343 | 8A15 B1324000    | mov dl,byte ptr ds:[4032B1]                   |
00401349 | 38D0             | cmp al,dl                                     |
0040134B | 75 27            | jne <keygenme_#1_clonetrone.Fail>             | serial[13] !== Keydatabase[name[^1] % 0x10]
0040134D | 68 30100000      | push 1030                                     | Success
00401352 | 68 59304000      | push keygenme_#1_clonetrone.403059            | 403059:"WoW, Very Good Job."
00401357 | 68 2D304000      | push keygenme_#1_clonetrone.40302D            | 40302D:"Well Done Cracker !!!. Now, Code a KeyGen.?"
0040135C | FF75 08          | push dword ptr ss:[ebp+8]                     |
0040135F | E8 72000000      | call <JMP.&_MessageBoxA@16>                   |
00401364 | 66:C705 C9324000 | mov word ptr ds:[4032C9],FFFF                 |
0040136D | C605 CB324000 01 | mov byte ptr ds:[4032CB],1                    |
00401374 | 61               | popad                                         |
00401375 | C9               | leave                                         | ^ if jump here
00401376 | C2 0400          | ret 4                                         |
```

修复的几处代码请自己比对，我懒得放了
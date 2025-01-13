太水了

先上一组可用license&serial num

```
License Number:
221
Serial Number:
397907
```

算法：

`serial num = (license num * 2 + 753) * 333 - 13 - 15`

细节：

```assembly
00401000 | 55               | push ebp                     |
00401001 | 8BEC             | mov ebp,esp                  |
00401003 | 83E4 F8          | and esp,FFFFFFF8             |
00401006 | 83EC 10          | sub esp,10                   |
00401009 | D9EE             | fldz                         |
0040100B | 56               | push esi                     | esi:EntryPoint
0040100C | 8B35 98204000    | mov esi,dword ptr ds:[<print | esi:EntryPoint
00401012 | DD5424 0C        | fst qword ptr ss:[esp+C]     |
00401016 | 57               | push edi                     | edi:EntryPoint
00401017 | DD5C24 08        | fstp qword ptr ss:[esp+8]    |
0040101B | 68 E4204000      | push cm260807.4020E4         | 4020E4:"KeyGen Me 26.08.2007 by T. J. McPower:\n\n"
00401020 | FFD6             | call esi                     | esi:EntryPoint
00401022 | 68 10214000      | push cm260807.402110         | 402110:"License Number:\n"
00401027 | FFD6             | call esi                     | esi:EntryPoint
00401029 | 8B3D A0204000    | mov edi,dword ptr ds:[<scanf | edi:EntryPoint, 004020A0:"鮲\np"
0040102F | 8D4424 10        | lea eax,dword ptr ss:[esp+10 | license number
00401033 | 50               | push eax                     |
00401034 | 68 24214000      | push cm260807.402124         | 402124:"%Lf"
00401039 | FFD7             | call edi                     | edi:EntryPoint
0040103B | 68 28214000      | push cm260807.402128         | 402128:"Serial Number:\n"
00401040 | FFD6             | call esi                     | esi:EntryPoint
00401042 | 8D4C24 24        | lea ecx,dword ptr ss:[esp+24 | serial number
00401046 | 51               | push ecx                     | ecx:EntryPoint
00401047 | 68 24214000      | push cm260807.402124         | 402124:"%Lf"
0040104C | FFD7             | call edi                     | edi:EntryPoint
0040104E | D9EE             | fldz                         |
00401050 | DD4424 24        | fld qword ptr ss:[esp+24]    | license
00401054 | 83C4 1C          | add esp,1C                   |
00401057 | D8D1             | fcom st(1)                   |
00401059 | DFE0             | fnstsw ax                    |
0040105B | DDD9             | fstp st(1)                   |
0040105D | F6C4 05          | test ah,5                    |
00401060 | 7A 15            | jp cm260807.401077           |
00401062 | 68 38214000      | push cm260807.402138         | 402138:"ERROR:             Invalid Number! (maybe you entered a string or your Number was too short)"
00401067 | DDD8             | fstp st(0)                   |
00401069 | FFD6             | call esi                     | esi:EntryPoint
0040106B | 83C4 04          | add esp,4                    |
0040106E | 83C8 FF          | or eax,FFFFFFFF              |
00401071 | 5F               | pop edi                      | edi:EntryPoint
00401072 | 5E               | pop esi                      | esi:EntryPoint
00401073 | 8BE5             | mov esp,ebp                  |
00401075 | 5D               | pop ebp                      |
00401076 | C3               | ret                          |
00401077 | DCC0             | fadd st(0),st(0)             | license*2
00401079 | DC05 F0214000    | fadd qword ptr ds:[4021F0]   | +753
0040107F | DC0D E8214000    | fmul qword ptr ds:[4021E8]   | *333
00401085 | DC25 E0214000    | fsub qword ptr ds:[4021E0]   | -13
0040108B | DC25 D8214000    | fsub qword ptr ds:[4021D8]   | -15
00401091 | DC5C24 10        | fcomp qword ptr ss:[esp+10]  | compare(result,serial)
00401095 | DFE0             | fnstsw ax                    |
00401097 | F6C4 44          | test ah,44                   | eq
0040109A | 7A 12            | jp cm260807.4010AE           |
0040109C | 68 98214000      | push cm260807.402198         | 402198:"Absolutely correct...\n"
004010A1 | FFD6             | call esi                     | esi:EntryPoint
004010A3 | 83C4 04          | add esp,4                    |
004010A6 | 33C0             | xor eax,eax                  |
004010A8 | 5F               | pop edi                      | edi:EntryPoint
004010A9 | 5E               | pop esi                      | esi:EntryPoint
004010AA | 8BE5             | mov esp,ebp                  |
004010AC | 5D               | pop ebp                      |
004010AD | C3               | ret                          |
004010AE | 68 B0214000      | push cm260807.4021B0         | 4021B0:"Wrong answer, have a closer look !\n"
004010B3 | FFD6             | call esi                     | esi:EntryPoint
004010B5 | 83C4 04          | add esp,4                    |
004010B8 | 5F               | pop edi                      | edi:EntryPoint
004010B9 | 33C0             | xor eax,eax                  |
004010BB | 5E               | pop esi                      | esi:EntryPoint
004010BC | 8BE5             | mov esp,ebp                  |
004010BE | 5D               | pop ebp                      |
004010BF | C3               | ret                          |
```


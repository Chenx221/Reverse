好像没有什么可以暴力破解的

正确pwd: `yippee`

思路：

程序要求6位pwd，输入的pwd将与`34 78 12 FE DB 78`进行Xor运算，结果转成字符串并与正确答案`4D11628EBE1D`进行比较

所以只要算一下`4D 11 62 8E BE 1D xor 34 78 12 FE DB 78`的结果再转成字符串即可得到答案

细节：

```assembly
00401290 | 55               | push ebp                                      |
00401291 | 89E5             | mov ebp,esp                                   |
00401293 | 81EC D8020000    | sub esp,2D8                                   |
00401299 | 83E4 F0          | and esp,FFFFFFF0                              |
0040129C | B8 00000000      | mov eax,0                                     |
004012A1 | 83C0 0F          | add eax,F                                     |
004012A4 | 83C0 0F          | add eax,F                                     |
004012A7 | C1E8 04          | shr eax,4                                     |
004012AA | C1E0 04          | shl eax,4                                     |
004012AD | 8985 54FDFFFF    | mov dword ptr ss:[ebp-2AC],eax                |
004012B3 | 8B85 54FDFFFF    | mov eax,dword ptr ss:[ebp-2AC]                |
004012B9 | E8 82060000      | call cm4.401940                               |
004012BE | E8 1D030000      | call cm4.4015E0                               |
004012C3 | A1 00304000      | mov eax,dword ptr ds:[403000]                 | 00403000:"4D11628EBE1D"
004012C8 | 8985 88FDFFFF    | mov dword ptr ss:[ebp-278],eax                |
004012CE | A1 04304000      | mov eax,dword ptr ds:[403004]                 | 00403004:"628EBE1D"
004012D3 | 8985 8CFDFFFF    | mov dword ptr ss:[ebp-274],eax                |
004012D9 | A1 08304000      | mov eax,dword ptr ds:[403008]                 | 00403008:"BE1D"
004012DE | 8985 90FDFFFF    | mov dword ptr ss:[ebp-270],eax                |
004012E4 | 0FB605 0C304000  | movzx eax,byte ptr ds:[40300C]                |
004012EB | 8885 94FDFFFF    | mov byte ptr ss:[ebp-26C],al                  |
004012F1 | 8D95 95FDFFFF    | lea edx,dword ptr ss:[ebp-26B]                |
004012F7 | B8 BA000000      | mov eax,BA                                    |
004012FC | 894424 08        | mov dword ptr ss:[esp+8],eax                  |
00401300 | C74424 04 000000 | mov dword ptr ss:[esp+4],0                    | [esp+04]:"4D11628EBE1D"
00401308 | 891424           | mov dword ptr ss:[esp],edx                    | [esp]:"52D7195A217"
0040130B | E8 30070000      | call <JMP.&_memset>                           |
00401310 | C785 84FDFFFF 00 | mov dword ptr ss:[ebp-27C],0                  |
0040131A | C785 80FDFFFF 00 | mov dword ptr ss:[ebp-280],0                  |
00401324 | C785 7CFDFFFF 00 | mov dword ptr ss:[ebp-284],0                  |
0040132E | C70424 C8304000  | mov dword ptr ss:[esp],cm4.4030C8             | [esp]:"52D7195A217", 4030C8:" 8            .oPYo. 8         \n"
00401335 | E8 46070000      | call <JMP.&_printf>                           |
0040133A | C70424 EC304000  | mov dword ptr ss:[esp],cm4.4030EC             | [esp]:"52D7195A217", 4030EC:" 8            8  .o8 8                  \n"
00401341 | E8 3A070000      | call <JMP.&_printf>                           |
00401346 | C70424 18314000  | mov dword ptr ss:[esp],cm4.403118             | [esp]:"52D7195A217", 403118:" 8oPYo. oPYo. 8 .P'8 8  .o  .oPYo. odYo. \n"
0040134D | E8 2E070000      | call <JMP.&_printf>                           |
00401352 | C70424 44314000  | mov dword ptr ss:[esp],cm4.403144             | [esp]:"52D7195A217", 403144:" 8    8 8  `' 8.d' 8 8oP'   8oooo8 8' `8 \n"
00401359 | E8 22070000      | call <JMP.&_printf>                           |
0040135E | C70424 70314000  | mov dword ptr ss:[esp],cm4.403170             | [esp]:"52D7195A217", 403170:" 8    8 8     8o'  8 8 `b.  8.     8   8 \n"
00401365 | E8 16070000      | call <JMP.&_printf>                           |
0040136A | C70424 9C314000  | mov dword ptr ss:[esp],cm4.40319C             | [esp]:"52D7195A217", 40319C:" `YooP' 8     `YooP' 8  `o. `Yooo' 8   8 \n"
00401371 | E8 0A070000      | call <JMP.&_printf>                           |
00401376 | C70424 C8314000  | mov dword ptr ss:[esp],cm4.4031C8             | [esp]:"52D7195A217", 4031C8:" :.....:..:::::.....:..::...:.....:..::..\n"
0040137D | E8 FE060000      | call <JMP.&_printf>                           |
00401382 | C70424 F4314000  | mov dword ptr ss:[esp],cm4.4031F4             | [esp]:"52D7195A217", 4031F4:" ::::::::::::::::::::::::::::::::::::::::\n"
00401389 | E8 F2060000      | call <JMP.&_printf>                           |
0040138E | C70424 F4314000  | mov dword ptr ss:[esp],cm4.4031F4             | [esp]:"52D7195A217", 4031F4:" ::::::::::::::::::::::::::::::::::::::::\n"
00401395 | E8 E6060000      | call <JMP.&_printf>                           |
0040139A | C70424 1F324000  | mov dword ptr ss:[esp],cm4.40321F             | [esp]:"52D7195A217", 40321F:" CrackMe #4  (BruteforceMe)\n"
004013A1 | E8 DA060000      | call <JMP.&_printf>                           |
004013A6 | C70424 3C324000  | mov dword ptr ss:[esp],cm4.40323C             | [esp]:"52D7195A217", 40323C:"\n\n Password : "
004013AD | E8 CE060000      | call <JMP.&_printf>                           |
004013B2 | 8D85 28FFFFFF    | lea eax,dword ptr ss:[ebp-D8]                 |
004013B8 | 894424 04        | mov dword ptr ss:[esp+4],eax                  | [esp+04]:pwd
004013BC | C70424 4B324000  | mov dword ptr ss:[esp],cm4.40324B             | [esp]:"52D7195A217", 40324B:"%s"
004013C3 | E8 A8060000      | call <JMP.&_scanf>                            |
004013C8 | 8D85 28FFFFFF    | lea eax,dword ptr ss:[ebp-D8]                 |
004013CE | 890424           | mov dword ptr ss:[esp],eax                    | [esp]:"52D7195A217"
004013D1 | E8 8A060000      | call <JMP.&_strlen>                           |
004013D6 | 8985 78FDFFFF    | mov dword ptr ss:[ebp-288],eax                | length
004013DC | 8D85 28FFFFFF    | lea eax,dword ptr ss:[ebp-D8]                 |
004013E2 | 890424           | mov dword ptr ss:[esp],eax                    | [esp]:"52D7195A217"
004013E5 | E8 76060000      | call <JMP.&_strlen>                           |
004013EA | 83F8 06          | cmp eax,6                                     |
004013ED | 0F85 BE000000    | jne cm4.4014B1                                | pwd length == 6
004013F3 | 0FB685 28FFFFFF  | movzx eax,byte ptr ss:[ebp-D8]                |
004013FA | 34 34            | xor al,34                                     | pwd[0] xor 0x34
004013FC | 0FBEC0           | movsx eax,al                                  |
004013FF | 8985 74FDFFFF    | mov dword ptr ss:[ebp-28C],eax                |
00401405 | 0FB685 29FFFFFF  | movzx eax,byte ptr ss:[ebp-D7]                |
0040140C | 34 78            | xor al,78                                     |
0040140E | 0FBEC0           | movsx eax,al                                  |
00401411 | 8985 70FDFFFF    | mov dword ptr ss:[ebp-290],eax                |
00401417 | 0FB685 2AFFFFFF  | movzx eax,byte ptr ss:[ebp-D6]                |
0040141E | 34 12            | xor al,12                                     |
00401420 | 0FBEC0           | movsx eax,al                                  |
00401423 | 8985 6CFDFFFF    | mov dword ptr ss:[ebp-294],eax                |
00401429 | 0FBE85 2BFFFFFF  | movsx eax,byte ptr ss:[ebp-D5]                |
00401430 | 35 FE000000      | xor eax,FE                                    |
00401435 | 8985 68FDFFFF    | mov dword ptr ss:[ebp-298],eax                |
0040143B | 0FBE85 2CFFFFFF  | movsx eax,byte ptr ss:[ebp-D4]                |
00401442 | 35 DB000000      | xor eax,DB                                    |
00401447 | 8985 64FDFFFF    | mov dword ptr ss:[ebp-29C],eax                |
0040144D | 0FB685 2DFFFFFF  | movzx eax,byte ptr ss:[ebp-D3]                |
00401454 | 34 78            | xor al,78                                     |
00401456 | 0FBEC0           | movsx eax,al                                  |
00401459 | 8985 60FDFFFF    | mov dword ptr ss:[ebp-2A0],eax                |
0040145F | 8B85 60FDFFFF    | mov eax,dword ptr ss:[ebp-2A0]                |
00401465 | 894424 1C        | mov dword ptr ss:[esp+1C],eax                 |
00401469 | 8B85 64FDFFFF    | mov eax,dword ptr ss:[ebp-29C]                |
0040146F | 894424 18        | mov dword ptr ss:[esp+18],eax                 |
00401473 | 8B85 68FDFFFF    | mov eax,dword ptr ss:[ebp-298]                |
00401479 | 894424 14        | mov dword ptr ss:[esp+14],eax                 |
0040147D | 8B85 6CFDFFFF    | mov eax,dword ptr ss:[ebp-294]                |
00401483 | 894424 10        | mov dword ptr ss:[esp+10],eax                 |
00401487 | 8B85 70FDFFFF    | mov eax,dword ptr ss:[ebp-290]                |
0040148D | 894424 0C        | mov dword ptr ss:[esp+C],eax                  |
00401491 | 8B85 74FDFFFF    | mov eax,dword ptr ss:[ebp-28C]                |
00401497 | 894424 08        | mov dword ptr ss:[esp+8],eax                  |
0040149B | C74424 04 4E3240 | mov dword ptr ss:[esp+4],cm4.40324E           | [esp+04]:"4D11628EBE1D", 40324E:"%X%X%X%X%X%X"
004014A3 | 8D85 58FEFFFF    | lea eax,dword ptr ss:[ebp-1A8]                |
004014A9 | 890424           | mov dword ptr ss:[esp],eax                    | [esp]:"52D7195A217"
004014AC | E8 2F060000      | call <JMP.&_wsprintfA>                        |
004014B1 | 8D85 88FDFFFF    | lea eax,dword ptr ss:[ebp-278]                | 4D11628EBE1D
004014B7 | 8D95 58FEFFFF    | lea edx,dword ptr ss:[ebp-1A8]                | 52D7195A217(ex)
004014BD | 894424 04        | mov dword ptr ss:[esp+4],eax                  | [esp+04]:"4D11628EBE1D"
004014C1 | 891424           | mov dword ptr ss:[esp],edx                    | [esp]:"52D7195A217"
004014C4 | E8 87050000      | call <JMP.&_strcmp>                           |
004014C9 | 85C0             | test eax,eax                                  |
004014CB | 75 0E            | jne cm4.4014DB                                |
004014CD | C70424 5C324000  | mov dword ptr ss:[esp],cm4.40325C             | [esp]:"52D7195A217", 40325C:"\n\n That's right! Now write a small tut :)\n"
004014D4 | E8 A7050000      | call <JMP.&_printf>                           |
004014D9 | EB 0C            | jmp cm4.4014E7                                |
004014DB | C70424 87324000  | mov dword ptr ss:[esp],cm4.403287             | [esp]:"52D7195A217", 403287:"\n\n Nope... try again."
004014E2 | E8 99050000      | call <JMP.&_printf>                           |
004014E7 | E8 D4040000      | call <JMP.&__getch>                           |
004014EC | B8 00000000      | mov eax,0                                     |
004014F1 | C9               | leave                                         |
004014F2 | C3               | ret                                           |
```


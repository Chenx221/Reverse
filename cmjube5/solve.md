程序图标实际在提醒 这个crackme有坑... (

crackme加了UPX

先上key

`fEEL tHE bEAT`

计算步骤见keygen源码

有一个坑不得不提，因为这害得我浪费了不少时间：

输入key时会对长度进行检查，只有长度为0xD时，才会进行真的key检查；其他长度下兜兜转转，会进行虚假的key验证，最后都是Wrong (笑)

```
004012E3   68 60214000             PUSH OFFSET <CRK_ME5_.key>                         ; ASCII "rstuvwxyz{|}~"
004012E8   6A 64                   PUSH 64
004012EA   6A 0D                   PUSH 0D
004012EC   6A 69                   PUSH 69
004012EE   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
004012F1   E8 DB010000             CALL <JMP.&user32.SendDlgItemMessageA>
004012F6   83F8 00                 CMP EAX,0
004012F9   75 11                   JNZ SHORT CRK_ME5_.0040130C
004012FB   C705 58214000 42134000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.Fake>   ; ASCII "jdh`!@"
00401305   5E                      POP ESI
00401306   5F                      POP EDI
00401307   5B                      POP EBX
00401308   C9                      LEAVE
00401309   C2 0C00                 RETN 0C
0040130C   8BD8                    MOV EBX,EAX
0040130E   B9 9A020000             MOV ECX,29A
00401313   43                      INC EBX
00401314   D1C3                    ROL EBX,1
00401316  ^E2 FB                   LOOPD SHORT CRK_ME5_.00401313
00401318   81C3 020000C4           ADD EBX,C4000002
0040131E   74 11                   JE SHORT CRK_ME5_.00401331                         ; length == 0xD
00401320   C705 58214000 42134000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.Fake>   ; ASCII "jdh`!@"
0040132A   5E                      POP ESI
0040132B   5F                      POP EDI
0040132C   5B                      POP EBX
0040132D   C9                      LEAVE
0040132E   C2 0C00                 RETN 0C
00401331   C705 58214000 F1114000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.True>   ; ASCII "h`!@"
0040133B   5E                      POP ESI
0040133C   5F                      POP EDI
0040133D   5B                      POP EBX
0040133E   C9                      LEAVE


```

细节：

```
00401000 > 6A 00                   PUSH 0
00401002   E8 8E040000             CALL <JMP.&kernel32.GetModuleHandleA>
00401007   A3 00204000             MOV DWORD PTR DS:[402000],EAX
0040100C   E8 78040000             CALL <JMP.&kernel32.GetCommandLineA>
00401011   A3 54214000             MOV DWORD PTR DS:[402154],EAX
00401016   C705 58214000 42134000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.Fake>   ; ASCII "jdh`!@"
00401020   6A 00                   PUSH 0
00401022   68 49114000             PUSH CRK_ME5_.00401149
00401027   6A 00                   PUSH 0
00401029   6A 64                   PUSH 64
0040102B   FF35 00204000           PUSH DWORD PTR DS:[402000]                         ; CRK_ME5_.00400000
00401031   E8 A7040000             CALL <JMP.&user32.DialogBoxParamA>
00401036   6A 00                   PUSH 0
00401038   E8 52040000             CALL <JMP.&kernel32.ExitProcess>
0040103D > C8 000000               ENTER 0,0
00401041   6A 40                   PUSH 40
00401043   68 B9204000             PUSH CRK_ME5_.004020B9                             ; ASCII "Information"
00401048   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
0040104B   FF35 40214000           PUSH DWORD PTR DS:[402140]
00401051   E8 69040000             CALL <JMP.&user32.MessageBoxA>
00401056   C9                      LEAVE
00401057   C2 0400                 RETN 4
0040105A > C8 000000               ENTER 0,0
0040105E   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]                         ; 0x11
00401061   B8 27214000             MOV EAX,OFFSET <CRK_ME5_.WrongMessage2>
00401066   8038 00                 CMP BYTE PTR DS:[EAX],0
00401069   74 07                   JE SHORT CRK_ME5_.00401072
0040106B   3018                    XOR BYTE PTR DS:[EAX],BL
0040106D   FECB                    DEC BL
0040106F   40                      INC EAX
00401070  ^EB F4                   JMP SHORT CRK_ME5_.00401066
00401072   68 27214000             PUSH OFFSET <CRK_ME5_.WrongMessage2>
00401077   E8 C1FFFFFF             CALL <CRK_ME5_.Msgbox>
0040107C   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]
0040107F   B8 27214000             MOV EAX,OFFSET <CRK_ME5_.WrongMessage2>
00401084   8038 00                 CMP BYTE PTR DS:[EAX],0
00401087   74 07                   JE SHORT CRK_ME5_.00401090
00401089   3018                    XOR BYTE PTR DS:[EAX],BL
0040108B   FECB                    DEC BL
0040108D   40                      INC EAX
0040108E  ^EB F4                   JMP SHORT CRK_ME5_.00401084
00401090   C9                      LEAVE
00401091   C2 0400                 RETN 4
00401094   C8 000000               ENTER 0,0
00401098   B8 12214000             MOV EAX,OFFSET <CRK_ME5_.WrongMessage>
0040109D   33DB                    XOR EBX,EBX
0040109F   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]                         ; key[3]+0x22 key[3]!=" "
004010A2   8038 00                 CMP BYTE PTR DS:[EAX],0
004010A5   74 07                   JE SHORT CRK_ME5_.004010AE                         ; True EBX(pre) != 0x42
004010A7   3018                    XOR BYTE PTR DS:[EAX],BL                           ; 0x15 ^= bl
004010A9   FECB                    DEC BL
004010AB   40                      INC EAX
004010AC  ^EB F4                   JMP SHORT CRK_ME5_.004010A2
004010AE   B9 83190803             MOV ECX,3081983
004010B3   8BD3                    MOV EDX,EBX                                        ; True EBX != 0x2E
004010B5   83C3 02                 ADD EBX,2
004010B8   C1C3 03                 ROL EBX,3
004010BB   83C3 04                 ADD EBX,4
004010BE   C1CB 05                 ROR EBX,5
004010C1  ^E2 F2                   LOOPD SHORT CRK_ME5_.004010B5
004010C3   81FB ACAAAA34           CMP EBX,34AAAAAC
004010C9   75 24                   JNZ SHORT <CRK_ME5_.Wrong3>
004010CB   68 12214000             PUSH OFFSET <CRK_ME5_.WrongMessage>                ; Wrong2
004010D0   E8 68FFFFFF             CALL <CRK_ME5_.Msgbox>
004010D5   B8 12214000             MOV EAX,OFFSET <CRK_ME5_.WrongMessage>
004010DA   33DB                    XOR EBX,EBX
004010DC   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]
004010DF   8038 00                 CMP BYTE PTR DS:[EAX],0
004010E2   74 07                   JE SHORT CRK_ME5_.004010EB
004010E4   3018                    XOR BYTE PTR DS:[EAX],BL
004010E6   FECB                    DEC BL
004010E8   40                      INC EAX
004010E9  ^EB F4                   JMP SHORT CRK_ME5_.004010DF
004010EB   C9                      LEAVE
004010EC   C2 0400                 RETN 4
004010EF > 8BDA                    MOV EBX,EDX
004010F1   48                      DEC EAX
004010F2   FEC3                    INC BL
004010F4   3018                    XOR BYTE PTR DS:[EAX],BL
004010F6   3D 12214000             CMP EAX,OFFSET <CRK_ME5_.WrongMessage>
004010FB  ^75 F4                   JNZ SHORT CRK_ME5_.004010F1
004010FD   B8 11000000             MOV EAX,11
00401102   50                      PUSH EAX
00401103   E8 52FFFFFF             CALL <CRK_ME5_.Wrong3>
00401108   C9                      LEAVE
00401109   C2 0400                 RETN 4
0040110C > C8 000000               ENTER 0,0
00401110   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]
00401113   B8 DB204000             MOV EAX,CRK_ME5_.004020DB
00401118   8038 00                 CMP BYTE PTR DS:[EAX],0
0040111B   74 07                   JE SHORT CRK_ME5_.00401124
0040111D   3018                    XOR BYTE PTR DS:[EAX],BL
0040111F   FECB                    DEC BL
00401121   40                      INC EAX
00401122  ^EB F4                   JMP SHORT CRK_ME5_.00401118
00401124   68 DB204000             PUSH CRK_ME5_.004020DB
00401129   E8 0FFFFFFF             CALL <CRK_ME5_.Msgbox>
0040112E   8A5D 08                 MOV BL,BYTE PTR SS:[EBP+8]
00401131   B8 DB204000             MOV EAX,CRK_ME5_.004020DB
00401136   8038 00                 CMP BYTE PTR DS:[EAX],0
00401139   74 0A                   JE SHORT CRK_ME5_.00401145
0040113B   3018                    XOR BYTE PTR DS:[EAX],BL
0040113D   FECB                    DEC BL
0040113F   40                      INC EAX
00401140  ^E9 3FFFFFFF             JMP CRK_ME5_.00401084
00401145   C9                      LEAVE
00401146   C2 0400                 RETN 4
00401149   C8 000000               ENTER 0,0                                          ; event
0040114D   53                      PUSH EBX
0040114E   57                      PUSH EDI
0040114F   56                      PUSH ESI
00401150   837D 0C 02              CMP DWORD PTR SS:[EBP+C],2
00401154   74 25                   JE SHORT <CRK_ME5_.exit>
00401156   837D 0C 10              CMP DWORD PTR SS:[EBP+C],10
0040115A   74 1F                   JE SHORT <CRK_ME5_.exit>
0040115C   817D 0C 11010000        CMP DWORD PTR SS:[EBP+C],111
00401163   74 24                   JE SHORT CRK_ME5_.00401189
00401165   817D 0C 10010000        CMP DWORD PTR SS:[EBP+C],110
0040116C   0F84 7A020000           JE <CRK_ME5_.init>
00401172   33C0                    XOR EAX,EAX
00401174   5E                      POP ESI
00401175   5F                      POP EDI
00401176   5B                      POP EBX
00401177   C9                      LEAVE
00401178   C2 0C00                 RETN 0C
0040117B > 6A 00                   PUSH 0
0040117D   E8 49030000             CALL <JMP.&user32.PostQuitMessage>
00401182   5E                      POP ESI
00401183   5F                      POP EDI
00401184   5B                      POP EBX
00401185   C9                      LEAVE
00401186   C2 0C00                 RETN 0C
00401189   837D 10 0A              CMP DWORD PTR SS:[EBP+10],0A
0040118D   74 2B                   JE SHORT <CRK_ME5_.about>
0040118F   837D 10 01              CMP DWORD PTR SS:[EBP+10],1
00401193   0F84 E2020000           JE CRK_ME5_.0040147B                               ; ?
00401199   817D 10 69000004        CMP DWORD PTR SS:[EBP+10],4000069
004011A0   0F84 3D010000           JE CRK_ME5_.004012E3                               ; ?
004011A6   817D 10 69000003        CMP DWORD PTR SS:[EBP+10],3000069
004011AD   0F84 30010000           JE CRK_ME5_.004012E3                               ; ?
004011B3   5E                      POP ESI
004011B4   5F                      POP EDI
004011B5   5B                      POP EBX
004011B6   C9                      LEAVE
004011B7   C2 0C00                 RETN 0C
004011BA > 6A 01                   PUSH 1
004011BC   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
004011BF   E8 E3020000             CALL <JMP.&user32.GetDlgItem>
004011C4   A3 3C214000             MOV DWORD PTR DS:[40213C],EAX
004011C9   6A 01                   PUSH 1
004011CB   FF35 3C214000           PUSH DWORD PTR DS:[40213C]
004011D1   E8 D7020000             CALL <JMP.&user32.EnableWindow>
004011D6   6A 20                   PUSH 20
004011D8   68 3E204000             PUSH CRK_ME5_.0040203E                             ; ASCII "AbouT"
004011DD   68 5A204000             PUSH CRK_ME5_.0040205A                             ; ASCII "Crackme 5.0 by JuBE
E-mail: jube@poland.com"
004011E2   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
004011E5   E8 D5020000             CALL <JMP.&user32.MessageBoxA>
004011EA   5E                      POP ESI
004011EB   5F                      POP EDI
004011EC   5B                      POP EBX
004011ED   C9                      LEAVE
004011EE   C2 0C00                 RETN 0C
004011F1 > 68 60214000             PUSH OFFSET <CRK_ME5_.key>                         ; ASCII "rstuvwxyz{|}~"
004011F6   6A 64                   PUSH 64
004011F8   6A 0D                   PUSH 0D
004011FA   6A 69                   PUSH 69
004011FC   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
004011FF   E8 CD020000             CALL <JMP.&user32.SendDlgItemMessageA>
00401204   83F8 00                 CMP EAX,0                                          ; length !==0
00401207   75 05                   JNZ SHORT CRK_ME5_.0040120E
00401209   E9 C3000000             JMP <CRK_ME5_.Wrong3.1>
0040120E   BF 60214000             MOV EDI,OFFSET <CRK_ME5_.key>                      ; ASCII "rstuvwxyz{|}~"
00401213   33DB                    XOR EBX,EBX
00401215   0FB607                  MOVZX EAX,BYTE PTR DS:[EDI]                        ; loop, ebx=sum(key)
00401218   83F8 00                 CMP EAX,0
0040121B   74 05                   JE SHORT CRK_ME5_.00401222
0040121D   03D8                    ADD EBX,EAX
0040121F   47                      INC EDI
00401220  ^EB F3                   JMP SHORT CRK_ME5_.00401215
00401222   891D 5C214000           MOV DWORD PTR DS:[<keysum>],EBX
00401228   C1C3 02                 ROL EBX,2
0040122B   81C3 39190000           ADD EBX,1939
00401231   81FB 1D280000           CMP EBX,281D
00401237   0F85 94000000           JNZ <CRK_ME5_.Wrong3.1>                            ; sum ==0x3B9
0040123D   BF 60214000             MOV EDI,OFFSET <CRK_ME5_.key>                      ; ASCII "rstuvwxyz{|}~"
00401242   8B07                    MOV EAX,DWORD PTR DS:[EDI]                         ; key[0..4]
00401244   05 4542754A             ADD EAX,4A754245
00401249   8B0D 5C214000           MOV ECX,DWORD PTR DS:[<keysum>]
0040124F   40                      INC EAX
00401250   C1C0 03                 ROL EAX,3
00401253  ^E2 FA                   LOOPD SHORT CRK_ME5_.0040124F
00401255   05 BE56C9C5             ADD EAX,C5C956BE
0040125A   8907                    MOV DWORD PTR DS:[EDI],EAX
0040125C   83C7 04                 ADD EDI,4
0040125F   8B07                    MOV EAX,DWORD PTR DS:[EDI]                         ; key[4..8]
00401261   05 4542754A             ADD EAX,4A754245
00401266   8B0D 5C214000           MOV ECX,DWORD PTR DS:[<keysum>]
0040126C   48                      DEC EAX
0040126D   C1C8 03                 ROR EAX,3
00401270  ^E2 FA                   LOOPD SHORT CRK_ME5_.0040126C
00401272   05 77EDC1AC             ADD EAX,ACC1ED77
00401277   8907                    MOV DWORD PTR DS:[EDI],EAX
00401279   83C7 04                 ADD EDI,4
0040127C   8A07                    MOV AL,BYTE PTR DS:[EDI]                           ; key[8]
0040127E   04 5A                   ADD AL,5A
00401280   8807                    MOV BYTE PTR DS:[EDI],AL
00401282   47                      INC EDI
00401283   8B07                    MOV EAX,DWORD PTR DS:[EDI]                         ; key[9..13]
00401285   05 4542754A             ADD EAX,4A754245
0040128A   8B0D 5C214000           MOV ECX,DWORD PTR DS:[<keysum>]
00401290   40                      INC EAX
00401291   C1C0 05                 ROL EAX,5
00401294   48                      DEC EAX
00401295  ^E2 F9                   LOOPD SHORT CRK_ME5_.00401290
00401297   05 87ABA66A             ADD EAX,6AA6AB87
0040129C   8907                    MOV DWORD PTR DS:[EDI],EAX
0040129E   BF 60214000             MOV EDI,OFFSET <CRK_ME5_.key>                      ; ASCII "rstuvwxyz{|}~"
004012A3   33C0                    XOR EAX,EAX
004012A5   33DB                    XOR EBX,EBX
004012A7   8A07                    MOV AL,BYTE PTR DS:[EDI]                           ; key[0]
004012A9   3C 00                   CMP AL,0
004012AB   74 24                   JE SHORT <CRK_ME5_.Wrong3.1>
004012AD   47                      INC EDI
004012AE   8A1F                    MOV BL,BYTE PTR DS:[EDI]                           ; key[1]
004012B0   80FB 00                 CMP BL,0
004012B3   74 0A                   JE SHORT CRK_ME5_.004012BF
004012B5   2AD8                    SUB BL,AL
004012B7   FECB                    DEC BL
004012B9   75 16                   JNZ SHORT <CRK_ME5_.Wrong3.1>
004012BB   8A07                    MOV AL,BYTE PTR DS:[EDI]
004012BD  ^EB EE                   JMP SHORT CRK_ME5_.004012AD
004012BF   A1 5C214000             MOV EAX,DWORD PTR DS:[<keysum>]
004012C4   50                      PUSH EAX
004012C5   E8 42FEFFFF             CALL <CRK_ME5_.Solved>
004012CA   5E                      POP ESI
004012CB   5F                      POP EDI
004012CC   5B                      POP EBX
004012CD   C9                      LEAVE
004012CE   C2 0C00                 RETN 0C
004012D1 > B8 11000000             MOV EAX,11
004012D6   50                      PUSH EAX
004012D7   E8 7EFDFFFF             CALL <CRK_ME5_.Wrong3>
004012DC   5E                      POP ESI
004012DD   5F                      POP EDI
004012DE   5B                      POP EBX
004012DF   C9                      LEAVE
004012E0   C2 0C00                 RETN 0C
004012E3   68 60214000             PUSH OFFSET <CRK_ME5_.key>                         ; ASCII "rstuvwxyz{|}~"
004012E8   6A 64                   PUSH 64
004012EA   6A 0D                   PUSH 0D
004012EC   6A 69                   PUSH 69
004012EE   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
004012F1   E8 DB010000             CALL <JMP.&user32.SendDlgItemMessageA>
004012F6   83F8 00                 CMP EAX,0
004012F9   75 11                   JNZ SHORT CRK_ME5_.0040130C
004012FB   C705 58214000 42134000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.Fake>   ; ASCII "jdh`!@"
00401305   5E                      POP ESI
00401306   5F                      POP EDI
00401307   5B                      POP EBX
00401308   C9                      LEAVE
00401309   C2 0C00                 RETN 0C
0040130C   8BD8                    MOV EBX,EAX
0040130E   B9 9A020000             MOV ECX,29A
00401313   43                      INC EBX
00401314   D1C3                    ROL EBX,1
00401316  ^E2 FB                   LOOPD SHORT CRK_ME5_.00401313
00401318   81C3 020000C4           ADD EBX,C4000002
0040131E   74 11                   JE SHORT CRK_ME5_.00401331                         ; length == 0xD
00401320   C705 58214000 42134000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.Fake>   ; ASCII "jdh`!@"
0040132A   5E                      POP ESI
0040132B   5F                      POP EDI
0040132C   5B                      POP EBX
0040132D   C9                      LEAVE
0040132E   C2 0C00                 RETN 0C
00401331   C705 58214000 F1114000  MOV DWORD PTR DS:[402158],OFFSET <CRK_ME5_.True>   ; ASCII "h`!@"
0040133B   5E                      POP ESI
0040133C   5F                      POP EDI
0040133D   5B                      POP EBX
0040133E   C9                      LEAVE
0040133F   C2 0C00                 RETN 0C
00401342 > 6A 64                   PUSH 64                                            ; Fake
00401344   68 60214000             PUSH OFFSET <CRK_ME5_.key>                         ; ASCII "rstuvwxyz{|}~"
00401349   6A 69                   PUSH 69
0040134B   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
0040134E   E8 72010000             CALL <JMP.&user32.GetDlgItemTextA>
00401353   83F8 00                 CMP EAX,0                                          ; length > 0
00401356   0F84 7E000000           JE <CRK_ME5_.Wrong>
0040135C   33DB                    XOR EBX,EBX
0040135E   03D8                    ADD EBX,EAX                                        ; EBX+length
00401360   03D8                    ADD EBX,EAX                                        ; +=length
00401362   8BC8                    MOV ECX,EAX
00401364   C1E3 02                 SHL EBX,2                                          ; ebx<<2
00401367   43                      INC EBX                                            ; +1
00401368   C1E3 03                 SHL EBX,3                                          ; ebx<<3
0040136B   03D8                    ADD EBX,EAX                                        ; +length
0040136D  ^E2 F5                   LOOPD SHORT CRK_ME5_.00401364                      ; lopp length times
0040136F   81FB 17638C31           CMP EBX,318C6317                                   ; ebx <= 0x318C6317
00401375   76 63                   JBE SHORT <CRK_ME5_.Wrong>                         ; (ebx == 0x318C6318)
00401377   81FB 19638C31           CMP EBX,318C6319                                   ; ebx >= 0x318C6319
0040137D   73 5B                   JNB SHORT <CRK_ME5_.Wrong>
0040137F   BF 60214000             MOV EDI,OFFSET <CRK_ME5_.key>                      ; ASCII "rstuvwxyz{|}~"
00401384   8BC8                    MOV ECX,EAX
00401386   33DB                    XOR EBX,EBX
00401388   0FB617                  MOVZX EDX,BYTE PTR DS:[EDI]                        ; sum(key)
0040138B   03DA                    ADD EBX,EDX
0040138D   47                      INC EDI
0040138E  ^E2 F8                   LOOPD SHORT CRK_ME5_.00401388
00401390   B9 D0070000             MOV ECX,7D0                                        ; loop 0x7D0 times
00401395   33C0                    XOR EAX,EAX
00401397   03C3                    ADD EAX,EBX                                        ; eax=sum(..)
00401399   05 83190000             ADD EAX,1983                                       ; eax+=0x1983
0040139E   81F9 D1070000           CMP ECX,7D1                                        ; never
004013A4  ^74 E2                   JE SHORT CRK_ME5_.00401388
004013A6   C1C0 08                 ROL EAX,8                                          ; rol(eax,8)
004013A9  ^E2 EE                   LOOPD SHORT CRK_ME5_.00401399
004013AB   3D DAE7E1E1             CMP EAX,E1E1E7DA                                   ; eax == 0xE1E1E7DA
004013B0   75 28                   JNZ SHORT <CRK_ME5_.Wrong>
004013B2   83EF 0D                 SUB EDI,0D                                         ; 3
004013B5   0FB607                  MOVZX EAX,BYTE PTR DS:[EDI]                        ; key[3]
004013B8   B9 11000000             MOV ECX,11
004013BD   83C0 02                 ADD EAX,2                                          ; +=2*0x11
004013C0  ^E2 FB                   LOOPD SHORT CRK_ME5_.004013BD
004013C2   8BD8                    MOV EBX,EAX
004013C4   B1 02                   MOV CL,2
004013C6   F6F1                    DIV CL                                             ; %2==0?
004013C8   80FC 00                 CMP AH,0
004013CB   75 0D                   JNZ SHORT <CRK_ME5_.Wrong>
004013CD   53                      PUSH EBX
004013CE   E8 C1FCFFFF             CALL CRK_ME5_.00401094
004013D3   5E                      POP ESI
004013D4   5F                      POP EDI
004013D5   5B                      POP EBX
004013D6   C9                      LEAVE
004013D7   C2 0C00                 RETN 0C
004013DA > B8 11000000             MOV EAX,11
004013DF   50                      PUSH EAX
004013E0   E8 75FCFFFF             CALL <CRK_ME5_.Wrong3>
004013E5   5E                      POP ESI
004013E6   5F                      POP EDI
004013E7   5B                      POP EBX
004013E8   C9                      LEAVE
004013E9   C2 0C00                 RETN 0C
004013EC > 68 E8030000             PUSH 3E8
004013F1   FF35 00204000           PUSH DWORD PTR DS:[402000]                         ; CRK_ME5_.00400000
004013F7   E8 BD000000             CALL <JMP.&user32.LoadIconA>
004013FC   A3 04204000             MOV DWORD PTR DS:[402004],EAX
00401401   50                      PUSH EAX
00401402   68 E8030000             PUSH 3E8
00401407   68 80000000             PUSH 80
0040140C   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
0040140F   E8 C3000000             CALL <JMP.&user32.SendMessageA>
00401414   68 1E204000             PUSH CRK_ME5_.0040201E
00401419   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
0040141C   E8 92000000             CALL <JMP.&user32.GetWindowRect>
00401421   E8 75000000             CALL <JMP.&user32.GetDesktopWindow>
00401426   68 2E204000             PUSH CRK_ME5_.0040202E
0040142B   50                      PUSH EAX
0040142C   E8 82000000             CALL <JMP.&user32.GetWindowRect>
00401431   6A 00                   PUSH 0
00401433   A1 2A204000             MOV EAX,DWORD PTR DS:[40202A]
00401438   2B05 22204000           SUB EAX,DWORD PTR DS:[402022]
0040143E   8BF8                    MOV EDI,EAX
00401440   50                      PUSH EAX
00401441   A1 26204000             MOV EAX,DWORD PTR DS:[402026]
00401446   2B05 1E204000           SUB EAX,DWORD PTR DS:[40201E]
0040144C   8BF0                    MOV ESI,EAX
0040144E   50                      PUSH EAX
0040144F   A1 3A204000             MOV EAX,DWORD PTR DS:[40203A]
00401454   2BC7                    SUB EAX,EDI
00401456   D1E8                    SHR EAX,1
00401458   50                      PUSH EAX
00401459   A1 36204000             MOV EAX,DWORD PTR DS:[402036]
0040145E   2BC6                    SUB EAX,ESI
00401460   D1E8                    SHR EAX,1
00401462   50                      PUSH EAX
00401463   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
00401466   E8 36000000             CALL <JMP.&user32.MoveWindow>
0040146B   FF75 08                 PUSH DWORD PTR SS:[EBP+8]
0040146E   8F05 40214000           POP DWORD PTR DS:[402140]
00401474   5E                      POP ESI
00401475   5F                      POP EDI
00401476   5B                      POP EBX
00401477   C9                      LEAVE
00401478   C2 0C00                 RETN 0C
0040147B   A1 58214000             MOV EAX,DWORD PTR DS:[402158]                      ; !
00401480   FFE0                    JMP EAX
00401482   5E                      POP ESI
00401483   5F                      POP EDI
00401484   5B                      POP EBX
00401485   C9                      LEAVE
00401486   C2 0C00                 RETN 0C


```


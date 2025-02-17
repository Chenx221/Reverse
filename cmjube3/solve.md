此crackme只有一个正确serial

serial: `H-Blockx`

你需要将serial以参数的形式提供

算法：

serial为8位字符串，四位一组

前半部分的计算/验证从004011A1开始，进行了两次验证（多余了一次），可以逆向推出正确值

后半部分基于前半部分计算得出，见00401170

...

不想表述了，看我写的keygen代码去吧

细节：

```assembly
00401139  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]
0040113C  |. 8F05 F8204000  POP DWORD PTR DS:[4020F8]
00401142  |. 8B3D 78214000  MOV EDI,DWORD PTR DS:[<workPath>]        ;  work path
00401148  |. 47             INC EDI
00401149  |> 8A07           /MOV AL,BYTE PTR DS:[EDI]
0040114B  |. 3C 22          |CMP AL,22                               ;  "
0040114D  |. 74 03          |JE SHORT CRK_ME3.00401152
0040114F  |. 47             |INC EDI
00401150  |.^EB F7          \JMP SHORT CRK_ME3.00401149
00401152  |> 83C7 02        ADD EDI,2
00401155  |. 893D 7C214000  MOV DWORD PTR DS:[<arguments>],EDI
0040115B  |. 57             PUSH EDI                                 ; /String
0040115C  |. E8 BC000000    CALL <JMP.&KERNEL32.lstrlenA>            ; \lstrlenA
00401161  |. 83F8 08        CMP EAX,8
00401164  |. 0F85 96000000  JNZ <CRK_ME3.Fail>
0040116A  |. 8B3D 7C214000  MOV EDI,DWORD PTR DS:[<arguments>]
00401170  |. 8B1F           MOV EBX,DWORD PTR DS:[EDI]
00401172  |. 8B4F 04        MOV ECX,DWORD PTR DS:[EDI+4]
00401175  |. 66:81F3 ADDE   XOR BX,0DEAD                             ;  "12" xor 0xDEAD
0040117A  |. 81F3 34122143  XOR EBX,43211234                         ;  "1234" xor 0x43211234
00401180  |. C1C3 06        ROL EBX,6
00401183  |. 81F1 44332211  XOR ECX,11223344                         ;  "5678" xor 0x11223344
00401189  |. C1C9 06        ROR ECX,6
0040118C  |. 81F1 83190803  XOR ECX,3081983                          ;  "5678" xor 0x3081983
00401192  |. 81F1 76B7AA89  XOR ECX,89AAB776                         ;  "5678" xor 0x89AAB776
00401198  |. 03D9           ADD EBX,ECX
0040119A  |. 75 64          JNZ SHORT <CRK_ME3.Fail>
0040119C  |. BB 00124000    MOV EBX,<CRK_ME3.Fail>
004011A1  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]
004011A3  |. 35 A0A0A0A0    XOR EAX,A0A0A0A0                         ;  "1234" xor 0xA0A0A0A0
004011A8  |. C1C0 06        ROL EAX,6
004011AB  |. 05 11111111    ADD EAX,11111111
004011B0  |. C1C0 06        ROL EAX,6
004011B3  |. 35 CCCCCCCC    XOR EAX,CCCCCCCC
004011B8  |. 05 22E2115E    ADD EAX,5E11E222
004011BD  |. 74 02          JE SHORT CRK_ME3.004011C1
004011BF  |. FFE3           JMP EBX                                  ;  Fail
004011C1  |> 8B47 04        MOV EAX,DWORD PTR DS:[EDI+4]
004011C4  |. BB 00124000    MOV EBX,<CRK_ME3.Fail>
004011C9  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]               ;  ? WTF
004011CB  |. 35 B0B0B0B0    XOR EAX,B0B0B0B0
004011D0  |. C1C0 07        ROL EAX,7
004011D3  |. 05 22222222    ADD EAX,22222222
004011D8  |. C1C0 07        ROL EAX,7
004011DB  |. 35 DDDDDDDD    XOR EAX,DDDDDDDD
004011E0  |. 05 706AAD9A    ADD EAX,9AAD6A70
004011E5  |. 74 02          JE SHORT CRK_ME3.004011E9
004011E7  |. FFE3           JMP EBX                                  ;  fail
004011E9  |> B8 09214000    MOV EAX,CRK_ME3.00402109                 ;  ASCII "Registered"
004011EE  |. 50             PUSH EAX                                 ; /Text => "Registered"
004011EF  |. 6A 6E          PUSH 6E                                  ; |ControlID = 6E (110.)
004011F1  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
004011F4  |. E8 60000000    CALL <JMP.&USER32.SetDlgItemTextA>       ; \SetDlgItemTextA
004011F9  |. 5E             POP ESI
004011FA  |. 5F             POP EDI
004011FB  |. 5B             POP EBX
004011FC  |. C9             LEAVE
004011FD  |. C2 0C00        RETN 0C
00401200 >|> B8 FC204000    MOV EAX,CRK_ME3.004020FC                 ;  ASCII "Unregistered"
00401205  |. 50             PUSH EAX                                 ; /Text => "Unregistered"
00401206  |. 6A 6E          PUSH 6E                                  ; |ControlID = 6E (110.)
00401208  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
0040120B  |. E8 49000000    CALL <JMP.&USER32.SetDlgItemTextA>       ; \SetDlgItemTextA
00401210  |. 5E             POP ESI
00401211  |. 5F             POP EDI
00401212  |. 5B             POP EBX
00401213  |. C9             LEAVE
00401214  \. C2 0C00        RETN 0C


```


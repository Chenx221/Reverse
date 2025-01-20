不是，过分了

我尝试了win2000~win11虚拟机，没一个能跑的

最后找了个win98虚拟机跑起来了

本身非常简单

一组可用密钥：

```
name: chenx221 
serial: 708
```

算法：

serial = AsciiSum(name) ^ 0x7D0 ^ 0x7BF

细节：

```assembly
004010B4 <> 6A 28                      PUSH 28
004010B6    68 CA214000                PUSH CRK_ME1_.004021CA                  ; ASCII "chenx22100"
004010BB    FF35 AE214000              PUSH DWORD PTR DS:[4021AE]
004010C1    E8 F9010000                CALL <JMP.&user32.GetWindowTextA>
004010C6    83F8 00                    CMP EAX,0                               ; Name.length !== 0
004010C9    0F84 B7000000              JE CRK_ME1_.00401186
004010CF    83F8 04                    CMP EAX,4                               ; Name.length >= 4
004010D2    0F82 E4000000              JB CRK_ME1_.004011BC
004010D8    6A 28                      PUSH 28
004010DA    68 CA214000                PUSH CRK_ME1_.004021CA                  ; ASCII "chenx22100"
004010DF    FF35 B2214000              PUSH DWORD PTR DS:[4021B2]
004010E5    E8 D5010000                CALL <JMP.&user32.GetWindowTextA>
004010EA    83F8 00                    CMP EAX,0                               ; Serial.length !== 0
004010ED    0F84 AE000000              JE CRK_ME1_.004011A1
004010F3    A3 C2214000                MOV DWORD PTR DS:[4021C2],EAX
004010F8    B8 CA214000                MOV EAX,CRK_ME1_.004021CA               ; ASCII "chenx22100"
004010FD    8A18                       MOV BL,BYTE PTR DS:[EAX]                ; loop (Check if serial is a number)
004010FF    80FB 00                    CMP BL,0
00401102    74 15                      JE SHORT CRK_ME1_.00401119              ; jump if check fin
00401104    80FB 30                    CMP BL,30
00401107    0F82 CA000000              JB CRK_ME1_.004011D7
0040110D    80FB 39                    CMP BL,39
00401110    0F87 C1000000              JA CRK_ME1_.004011D7
00401116    40                         INC EAX
00401117   ^EB E4                      JMP SHORT CRK_ME1_.004010FD             ; next loop
00401119    6A 00                      PUSH 0
0040111B    68 C6214000                PUSH CRK_ME1_.004021C6
00401120    6A 6A                      PUSH 6A
00401122    FF75 08                    PUSH DWORD PTR SS:[EBP+8]
00401125    E8 83010000                CALL <JMP.&user32.GetDlgItemInt>
0040112A    833D C6214000 01           CMP DWORD PTR DS:[4021C6],1
00401131    0F85 A0000000              JNZ CRK_ME1_.004011D7
00401137    35 BF070000                XOR EAX,7BF                             ; serial xor 0x7BF
0040113C    A3 BA214000                MOV DWORD PTR DS:[<serialResult>],EAX
00401141    6A 28                      PUSH 28
00401143    68 CA214000                PUSH CRK_ME1_.004021CA                  ; ASCII "chenx22100"
00401148    FF35 AE214000              PUSH DWORD PTR DS:[4021AE]
0040114E    E8 6C010000                CALL <JMP.&user32.GetWindowTextA>
00401153    B9 CA214000                MOV ECX,CRK_ME1_.004021CA               ; ASCII "chenx22100"
00401158    33C0                       XOR EAX,EAX
0040115A    33DB                       XOR EBX,EBX
0040115C    8A19                       MOV BL,BYTE PTR DS:[ECX]                ; loop (sum name ascii)
0040115E    80FB 00                    CMP BL,0
00401161    74 05                      JE SHORT CRK_ME1_.00401168
00401163    03C3                       ADD EAX,EBX
00401165    41                         INC ECX
00401166   ^EB F4                      JMP SHORT CRK_ME1_.0040115C             ; next loop
00401168    35 D0070000                XOR EAX,7D0                             ; sum(name) xor 0x7D0
0040116D    3905 BA214000              CMP DWORD PTR DS:[<serialResult>],EAX
00401173    75 62                      JNZ SHORT CRK_ME1_.004011D7
00401175    68 7B214000                PUSH CRK_ME1_.0040217B                  ; ASCII "That's it ! Now make keygen
and keep cracking :-)"
0040117A    E8 AAFEFFFF                CALL CRK_ME1_.00401029
0040117F    5E                         POP ESI
00401180    5F                         POP EDI
00401181    5B                         POP EBX
00401182    C9                         LEAVE
00401183    C2 0C00                    RETN 0C
00401186    6A 30                      PUSH 30
00401188    68 D8204000                PUSH CRK_ME1_.004020D8                  ; ASCII "Error"
0040118D    68 DE204000                PUSH CRK_ME1_.004020DE                  ; ASCII "You must enter your name"
00401192    FF75 08                    PUSH DWORD PTR SS:[EBP+8]
00401195    E8 2B010000                CALL <JMP.&user32.MessageBoxA>
0040119A    5E                         POP ESI
0040119B    5F                         POP EDI
0040119C    5B                         POP EBX
0040119D    C9                         LEAVE
0040119E    C2 0C00                    RETN 0C
004011A1    6A 30                      PUSH 30
004011A3    68 D8204000                PUSH CRK_ME1_.004020D8                  ; ASCII "Error"
004011A8    68 26214000                PUSH CRK_ME1_.00402126                  ; ASCII "You must enter your key"
004011AD    FF75 08                    PUSH DWORD PTR SS:[EBP+8]
004011B0    E8 10010000                CALL <JMP.&user32.MessageBoxA>
004011B5    5E                         POP ESI
004011B6    5F                         POP EDI
004011B7    5B                         POP EBX
004011B8    C9                         LEAVE
004011B9    C2 0C00                    RETN 0C
004011BC    6A 30                      PUSH 30
004011BE    68 D8204000                PUSH CRK_ME1_.004020D8                  ; ASCII "Error"
004011C3    68 F7204000                PUSH CRK_ME1_.004020F7                  ; ASCII "Your name should be at least 4 characters long"
004011C8    FF75 08                    PUSH DWORD PTR SS:[EBP+8]
004011CB    E8 F5000000                CALL <JMP.&user32.MessageBoxA>
004011D0    5E                         POP ESI
004011D1    5F                         POP EDI
004011D2    5B                         POP EBX
004011D3    C9                         LEAVE
004011D4    C2 0C00                    RETN 0C
004011D7    68 4A214000                PUSH CRK_ME1_.0040214A                  ; ASCII "This key isn't correct,
but don't give up ! :-)"
004011DC    E8 48FEFFFF                CALL CRK_ME1_.00401029
004011E1    5E                         POP ESI
004011E2    5F                         POP EDI
004011E3    5B                         POP EBX
004011E4    C9                         LEAVE
004011E5    C2 0C00                    RETN 0C
```


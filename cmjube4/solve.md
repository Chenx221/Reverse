怎么感觉最近都是远古crackme

先上密钥：（提交按钮需要查看About页面以启用）
```
程序运行参数:reg
key:tHE Summer !
```
没什么好说明的，所以直接上细节吧
细节：
```
//About
004010C5 >|> 6A 01          PUSH 1                                    ; /ControlID = 1
004010C7  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd
004010CA  |. E8 D9010000    CALL <JMP.&USER32.GetDlgItem>             ; \GetDlgItem
004010CF  |. A3 E3214000    MOV DWORD PTR DS:[4021E3],EAX
004010D4  |. 6A 01          PUSH 1                                    ; /Enable = TRUE
004010D6  |. FF35 E3214000  PUSH DWORD PTR DS:[4021E3]                ; |hWnd = 000006D8 (class='Edit',wndproc=8053D3D8,parent=000005D0)
004010DC  |. E8 85010000    CALL <JMP.&USER32.EnableWindow>           ; \EnableWindow
004010E1  |. 6A 20          PUSH 20                                   ; /Style = MB_OK|MB_ICONQUESTION|MB_APPLMODAL
004010E3  |. 68 59204000    PUSH CRK_ME4.00402059                     ; |Title = "About"
004010E8  |. 68 5F204000    PUSH CRK_ME4.0040205F                     ; |Text = "Crackme 4.0 by Jube
E-mail: jube@poland.com

----------------------------------
INFO
You must enter correct key
NO PATCH ALLOWED
----------------------------------

Greets : Plastek  _Masta_
 _HaK_  Iczelion

7TeaM - Polish"...
004010ED  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hOwner
004010F0  |. E8 A1010000    CALL <JMP.&USER32.MessageBoxA>            ; \MessageBoxA
004010F5  |. 5E             POP ESI
004010F6  |. 5F             POP EDI
004010F7  |. 5B             POP EBX
004010F8  |. C9             LEAVE
004010F9  |. C2 0C00        RETN 0C
004010FC  |> 6A 69          PUSH 69                                   ; /ControlID = 69 (105.)
004010FE  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd = F65A0000
00401101  |. E8 A2010000    CALL <JMP.&USER32.GetDlgItem>             ; \GetDlgItem
00401106  |. A3 E3214000    MOV DWORD PTR DS:[4021E3],EAX
0040110B  |. 50             PUSH EAX                                  ; /hWnd
0040110C  |. E8 79010000    CALL <JMP.&USER32.GetWindowTextLengthA>   ; \GetWindowTextLengthA
00401111  |. 83C0 F4        ADD EAX,-0C
00401114  |. 75 66          JNZ SHORT <CRK_ME4.Fail>                  ;  check key length 0xC
00401116  |. 6A 64          PUSH 64                                   ; /Count = 64 (100.)
00401118  |. 68 FF214000    PUSH OFFSET <CRK_ME4.key>                 ; |Buffer = OFFSET <CRK_ME4.key>
0040111D  |. FF35 E3214000  PUSH DWORD PTR DS:[4021E3]                ; |hWnd = 000006D8 (class='Edit',wndproc=8053D3D8,parent=000005D0)
00401123  |. E8 5C010000    CALL <JMP.&USER32.GetWindowTextA>         ; \GetWindowTextA
00401128  |. 83F8 00        CMP EAX,0                                 ;  check empty
0040112B  |. 74 4F          JE SHORT <CRK_ME4.Fail>
0040112D  |. BF FF214000    MOV EDI,OFFSET <CRK_ME4.key>              ;  ASCII "tHE Summer !"
00401132  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]
00401134  |. C1C0 06        ROL EAX,6                                 ;  rol 6
00401137  |. 35 ADDEADDE    XOR EAX,DEADDEAD                          ;  xor 0xDEADDEAD
0040113C  |. 05 5B3C0030    ADD EAX,30003C5B                          ;  +0x30003C5B
00401141  |. 75 39          JNZ SHORT <CRK_ME4.Fail>
00401143  |. 83C7 04        ADD EDI,4
00401146  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]
00401148  |. C1C8 07        ROR EAX,7                                 ;  ror 7
0040114B  |. 35 88888888    XOR EAX,88888888                          ;  xor 0x88888888
00401150  |. 05 9EADADD1    ADD EAX,D1ADAD9E                          ;  +0xD1ADAD9E
00401155  |. 75 25          JNZ SHORT <CRK_ME4.Fail>
00401157  |. 83C7 04        ADD EDI,4
0040115A  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]
0040115C  |. C1C0 05        ROL EAX,5                                 ;  rol 5
0040115F  |. 35 21433412    XOR EAX,12344321                          ;  xor 0x12344321
00401164  |. 05 7BF0C5C9    ADD EAX,C9C5F07B                          ;  +0xC9C5F07B
00401169  |. 75 11          JNZ SHORT <CRK_ME4.Fail>
0040116B  |. 68 AE214000    PUSH CRK_ME4.004021AE                     ; /Arg1 = 004021AE ASCII "That's it! Send me your solution !
kEEP cRACKING :)"
00401170  |. E8 BEFEFFFF    CALL CRK_ME4.00401033                     ; \CRK_ME4.00401033
00401175  |. 5E             POP ESI
00401176  |. 5F             POP EDI
00401177  |. 5B             POP EBX
00401178  |. C9             LEAVE
00401179  |. C2 0C00        RETN 0C
0040117C >|> 68 7D214000    PUSH CRK_ME4.0040217D                     ; /Arg1 = 0040217D ASCII "This key isn't correct,
but don't give up ! :-)"
00401181  |. E8 ADFEFFFF    CALL CRK_ME4.00401033                     ; \CRK_ME4.00401033
00401186  |. 5E             POP ESI
00401187  |. 5F             POP EDI
00401188  |. 5B             POP EBX
00401189  |. C9             LEAVE
0040118A  |. C2 0C00        RETN 0C
0040118D  |> 68 E8030000    PUSH 3E8                                  ; /RsrcName = 1000.
00401192  |. FF35 00204000  PUSH DWORD PTR DS:[402000]                ; |hInst = 00400000
00401198  |. E8 CF000000    CALL <JMP.&USER32.LoadIconA>              ; \LoadIconA
0040119D  |. A3 04204000    MOV DWORD PTR DS:[402004],EAX
004011A2  |. 50             PUSH EAX                                  ; /lParam
004011A3  |. 68 E8030000    PUSH 3E8                                  ; |wParam = 3E8
004011A8  |. 68 80000000    PUSH 80                                   ; |Message = WM_SETICON
004011AD  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd
004011B0  |. E8 DB000000    CALL <JMP.&USER32.SendMessageA>           ; \SendMessageA
004011B5  |. 68 08204000    PUSH CRK_ME4.00402008                     ; /pRect = CRK_ME4.00402008
004011BA  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd
004011BD  |. E8 BC000000    CALL <JMP.&USER32.GetWindowRect>          ; \GetWindowRect
004011C2  |. E8 AB000000    CALL <JMP.&USER32.GetDesktopWindow>       ; [GetDesktopWindow
004011C7  |. 68 18204000    PUSH CRK_ME4.00402018                     ; /pRect = CRK_ME4.00402018
004011CC  |. 50             PUSH EAX                                  ; |hWnd
004011CD  |. E8 AC000000    CALL <JMP.&USER32.GetWindowRect>          ; \GetWindowRect
004011D2  |. 6A 00          PUSH 0                                    ; /Repaint = FALSE
004011D4  |. A1 14204000    MOV EAX,DWORD PTR DS:[402014]             ; |
004011D9  |. 2B05 0C204000  SUB EAX,DWORD PTR DS:[40200C]             ; |
004011DF  |. 8BF8           MOV EDI,EAX                               ; |
004011E1  |. 50             PUSH EAX                                  ; |Height
004011E2  |. A1 10204000    MOV EAX,DWORD PTR DS:[402010]             ; |
004011E7  |. 2B05 08204000  SUB EAX,DWORD PTR DS:[402008]             ; |
004011ED  |. 8BF0           MOV ESI,EAX                               ; |
004011EF  |. 50             PUSH EAX                                  ; |Width
004011F0  |. A1 24204000    MOV EAX,DWORD PTR DS:[402024]             ; |
004011F5  |. 2BC7           SUB EAX,EDI                               ; |
004011F7  |. D1E8           SHR EAX,1                                 ; |
004011F9  |. 50             PUSH EAX                                  ; |Y
004011FA  |. A1 20204000    MOV EAX,DWORD PTR DS:[402020]             ; |
004011FF  |. 2BC6           SUB EAX,ESI                               ; |
00401201  |. D1E8           SHR EAX,1                                 ; |
00401203  |. 50             PUSH EAX                                  ; |X
00401204  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd
00401207  |. E8 90000000    CALL <JMP.&USER32.MoveWindow>             ; \MoveWindow
0040120C  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]
0040120F  |. 8F05 E7214000  POP DWORD PTR DS:[4021E7]
00401215  |. 8B3D FB214000  MOV EDI,DWORD PTR DS:[4021FB]             ;  read path
0040121B  |. 47             INC EDI
0040121C  |> 8A07           /MOV AL,BYTE PTR DS:[EDI]
0040121E  |. 3C 22          |CMP AL,22                                ;  "
00401220  |. 74 03          |JE SHORT CRK_ME4.00401225
00401222  |. 47             |INC EDI
00401223  |.^EB F7          \JMP SHORT CRK_ME4.0040121C
00401225  |> 83C7 02        ADD EDI,2
00401228  |. 8B07           MOV EAX,DWORD PTR DS:[EDI]                ;  read arguments
0040122A  |. 05 8E9A98FF    ADD EAX,FF989A8E                          ;  +0xFF989A8E
0040122F  |. 74 1C          JE SHORT CRK_ME4.0040124D                 ;  need jmp (skip textbox disable)
00401231  |. 6A 69          PUSH 69                                   ; /ControlID = 69 (105.)
00401233  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                 ; |hWnd
00401236  |. E8 6D000000    CALL <JMP.&USER32.GetDlgItem>             ; \GetDlgItem
0040123B  |. A3 E3214000    MOV DWORD PTR DS:[4021E3],EAX
00401240  |. 6A 00          PUSH 0                                    ; /Enable = FALSE
00401242  |. FF35 E3214000  PUSH DWORD PTR DS:[4021E3]                ; |hWnd = 000006D8 (class='Edit',wndproc=8053D3D8,parent=000005D0)
00401248  |. E8 19000000    CALL <JMP.&USER32.EnableWindow>           ; \EnableWindow
0040124D  |> 5E             POP ESI
0040124E  |. 5F             POP EDI
0040124F  |. 5B             POP EBX
00401250  |. C9             LEAVE
00401251  \. C2 0C00        RETN 0C


```
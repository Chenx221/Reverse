好家伙，这远古crackme居然是个系列，接下来几周都要在win98下搞了

先上方法：

在crackme同目录下建立文件`crk2.a`，填入以下hex内容，启动crackme就会提示已注册

```
4A 75 62 65 8C 42 65 87 40 4B 46 4D 5B 11 11 12
```

*我的keygen并不能在远古环境下运行

key结构：

```
key[0..4]
Jube
key[4..8]
checksum: asciiSum(key[8..]) xor 0x87654321
key[8..]
name (per char)xor 0x23
```

细节：

```assembly
004010B0  |> 68 E8030000    PUSH 3E8                                 ; /RsrcName = 1000.
004010B5  |. FF35 00204000  PUSH DWORD PTR DS:[402000]               ; |hInst = NULL
004010BB  |. E8 15020000    CALL <JMP.&USER32.LoadIconA>             ; \LoadIconA
004010C0  |. A3 04204000    MOV DWORD PTR DS:[402004],EAX
004010C5  |. 50             PUSH EAX                                 ; /lParam
004010C6  |. 68 E8030000    PUSH 3E8                                 ; |wParam = 3E8
004010CB  |. 68 80000000    PUSH 80                                  ; |Message = WM_SETICON
004010D0  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
004010D3  |. E8 21020000    CALL <JMP.&USER32.SendMessageA>          ; \SendMessageA
004010D8  |. 68 08204000    PUSH CRK_ME2.00402008                    ; /pRect = CRK_ME2.00402008
004010DD  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
004010E0  |. E8 F6010000    CALL <JMP.&USER32.GetWindowRect>         ; \GetWindowRect
004010E5  |. E8 F7010000    CALL <JMP.&USER32.GetDesktopWindow>      ; [GetDesktopWindow
004010EA  |. 68 18204000    PUSH CRK_ME2.00402018                    ; /pRect = CRK_ME2.00402018
004010EF  |. 50             PUSH EAX                                 ; |hWnd
004010F0  |. E8 E6010000    CALL <JMP.&USER32.GetWindowRect>         ; \GetWindowRect
004010F5  |. 6A 00          PUSH 0                                   ; /Repaint = FALSE
004010F7  |. A1 14204000    MOV EAX,DWORD PTR DS:[402014]            ; |
004010FC  |. 2B05 0C204000  SUB EAX,DWORD PTR DS:[40200C]            ; |
00401102  |. 8BF8           MOV EDI,EAX                              ; |
00401104  |. 50             PUSH EAX                                 ; |Height
00401105  |. A1 10204000    MOV EAX,DWORD PTR DS:[402010]            ; |
0040110A  |. 2B05 08204000  SUB EAX,DWORD PTR DS:[402008]            ; |
00401110  |. 8BF0           MOV ESI,EAX                              ; |
00401112  |. 50             PUSH EAX                                 ; |Width
00401113  |. A1 24204000    MOV EAX,DWORD PTR DS:[402024]            ; |
00401118  |. 2BC7           SUB EAX,EDI                              ; |
0040111A  |. D1E8           SHR EAX,1                                ; |
0040111C  |. 50             PUSH EAX                                 ; |Y
0040111D  |. A1 20204000    MOV EAX,DWORD PTR DS:[402020]            ; |
00401122  |. 2BC6           SUB EAX,ESI                              ; |
00401124  |. D1E8           SHR EAX,1                                ; |
00401126  |. 50             PUSH EAX                                 ; |X
00401127  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
0040112A  |. E8 9A010000    CALL <JMP.&USER32.MoveWindow>            ; \MoveWindow
0040112F  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]
00401132  |. 8F05 E2204000  POP DWORD PTR DS:[4020E2]
00401138  |. B8 F9204000    MOV EAX,CRK_ME2.004020F9
0040113D  |. 33DB           XOR EBX,EBX
0040113F  |. 8918           MOV DWORD PTR DS:[EAX],EBX
00401141  |. 6A 00          PUSH 0                                   ; /hTemplateFile = NULL
00401143  |. 68 80000000    PUSH 80                                  ; |Attributes = NORMAL
00401148  |. 6A 03          PUSH 3                                   ; |Mode = OPEN_EXISTING
0040114A  |. 6A 00          PUSH 0                                   ; |pSecurity = NULL
0040114C  |. 6A 01          PUSH 1                                   ; |ShareMode = FILE_SHARE_READ
0040114E  |. 68 00000080    PUSH 80000000                            ; |Access = GENERIC_READ
00401153  |. 68 E6204000    PUSH CRK_ME2.004020E6                    ; |FileName = "crk2.a"
00401158  |. E8 60010000    CALL <JMP.&KERNEL32.CreateFileA>         ; \CreateFileA
0040115D  |. 83F8 FF        CMP EAX,-1
00401160  |. 0F84 05010000  JE CRK_ME2.0040126B                      ;  fail #1
00401166  |. A3 ED204000    MOV DWORD PTR DS:[<FileHandle>],EAX
0040116B  |. 6A 00          PUSH 0                                   ; /pFileSizeHigh = NULL
0040116D  |. FF35 ED204000  PUSH DWORD PTR DS:[<FileHandle>]         ; |hFile = NULL
00401173  |. E8 2D010000    CALL <JMP.&KERNEL32.GetFileSize>         ; \GetFileSize
00401178  |. 83F8 09        CMP EAX,9
0040117B  |. 0F82 DF000000  JB CRK_ME2.00401260                      ;  fail #2
00401181  |. A3 F1204000    MOV DWORD PTR DS:[<FileSize>],EAX
00401186  |. 50             PUSH EAX                                 ; /MemSize
00401187  |. 6A 00          PUSH 0                                   ; |Flags = GMEM_FIXED
00401189  |. E8 23010000    CALL <JMP.&KERNEL32.GlobalAlloc>         ; \GlobalAlloc
0040118E  |. 83F8 00        CMP EAX,0
00401191  |. 0F84 C9000000  JE CRK_ME2.00401260                      ;  fail #3
00401197  |. A3 F5204000    MOV DWORD PTR DS:[<MemAlloc>],EAX
0040119C  |. 6A 00          PUSH 0                                   ; /pOverlapped = NULL
0040119E  |. 68 FD204000    PUSH CRK_ME2.004020FD                    ; |pBytesRead = CRK_ME2.004020FD
004011A3  |. FF35 F1204000  PUSH DWORD PTR DS:[<FileSize>]           ; |BytesToRead = 0
004011A9  |. 50             PUSH EAX                                 ; |Buffer
004011AA  |. FF35 ED204000  PUSH DWORD PTR DS:[<FileHandle>]         ; |hFile = NULL
004011B0  |. E8 02010000    CALL <JMP.&KERNEL32.ReadFile>            ; \ReadFile
004011B5  |. 83F8 00        CMP EAX,0
004011B8  |. 0F84 A2000000  JE CRK_ME2.00401260                      ;  fail #4
004011BE  |. 8B1D FD204000  MOV EBX,DWORD PTR DS:[4020FD]
004011C4  |. 3B1D F1204000  CMP EBX,DWORD PTR DS:[<FileSize>]
004011CA  |. 0F85 85000000  JNZ CRK_ME2.00401255                     ;  fail #5
004011D0  |. 8B1D F5204000  MOV EBX,DWORD PTR DS:[<MemAlloc>]
004011D6  |. 8B03           MOV EAX,DWORD PTR DS:[EBX]
004011D8  |. 35 78563412    XOR EAX,12345678
004011DD  |. 2D 32235677    SUB EAX,77562332
004011E2  |. 75 71          JNZ SHORT CRK_ME2.00401255               ;  fail #6
004011E4  |. 83C3 04        ADD EBX,4
004011E7  |. 8B03           MOV EAX,DWORD PTR DS:[EBX]
004011E9  |. A3 01214000    MOV DWORD PTR DS:[402101],EAX            ;  key[4..8]
004011EE  |. 83C3 04        ADD EBX,4
004011F1  |. A1 F1204000    MOV EAX,DWORD PTR DS:[<FileSize>]
004011F6  |. 83E8 08        SUB EAX,8
004011F9  |. 8BC8           MOV ECX,EAX
004011FB  |. 33D2           XOR EDX,EDX
004011FD  |> 0FB603         /MOVZX EAX,BYTE PTR DS:[EBX]             ;  EDX = asciiSum(key[8..])
00401200  |. 03D0           |ADD EDX,EAX
00401202  |. 43             |INC EBX
00401203  |. 49             |DEC ECX
00401204  |.^75 F7          \JNZ SHORT CRK_ME2.004011FD
00401206  |. 81F2 21436587  XOR EDX,87654321
0040120C  |. 3315 01214000  XOR EDX,DWORD PTR DS:[402101]
00401212  |. 75 41          JNZ SHORT CRK_ME2.00401255               ;  fail #7
00401214  |. B8 05214000    MOV EAX,CRK_ME2.00402105                 ;  ASCII "Registered to "
00401219  |. BA 21214000    MOV EDX,CRK_ME2.00402121
0040121E  |> 8A18           /MOV BL,BYTE PTR DS:[EAX]                ;  Copy "Registered to " 0x402021
00401220  |. 80FB 00        |CMP BL,0
00401223  |. 74 06          |JE SHORT CRK_ME2.0040122B
00401225  |. 881A           |MOV BYTE PTR DS:[EDX],BL
00401227  |. 42             |INC EDX
00401228  |. 40             |INC EAX
00401229  |.^EB F3          \JMP SHORT CRK_ME2.0040121E
0040122B  |> 8B1D F5204000  MOV EBX,DWORD PTR DS:[<MemAlloc>]
00401231  |. 83C3 08        ADD EBX,8
00401234  |. 8B0D F1204000  MOV ECX,DWORD PTR DS:[<FileSize>]
0040123A  |. 83E9 08        SUB ECX,8
0040123D  |> 8A03           /MOV AL,BYTE PTR DS:[EBX]                ;  key[8..] ^ 0x23
0040123F  |. 34 23          |XOR AL,23
00401241  |. 8802           |MOV BYTE PTR DS:[EDX],AL
00401243  |. 42             |INC EDX
00401244  |. 43             |INC EBX
00401245  |. 49             |DEC ECX
00401246  |.^75 F5          \JNZ SHORT CRK_ME2.0040123D
00401248  |. C602 00        MOV BYTE PTR DS:[EDX],0
0040124B  |. BB F9204000    MOV EBX,CRK_ME2.004020F9
00401250  |. 33C0           XOR EAX,EAX
00401252  |. 40             INC EAX
00401253  |. 8903           MOV DWORD PTR DS:[EBX],EAX
00401255  |> FF35 F5204000  PUSH DWORD PTR DS:[<MemAlloc>]           ; /hMem = NULL
0040125B  |. E8 4B000000    CALL <JMP.&KERNEL32.GlobalFree>          ; \GlobalFree
00401260  |> FF35 ED204000  PUSH DWORD PTR DS:[<FileHandle>]         ; /hObject = NULL
00401266  |. E8 58000000    CALL <JMP.&KERNEL32.CloseHandle>         ; \CloseHandle
0040126B  |> 68 14214000    PUSH CRK_ME2.00402114                    ; /Text = "Unregistered"
00401270  |. 6A 6E          PUSH 6E                                  ; |ControlID = 6E (110.)
00401272  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
00401275  |. E8 79000000    CALL <JMP.&USER32.SetDlgItemTextA>       ; \SetDlgItemTextA
0040127A  |. 833D F9204000 >CMP DWORD PTR DS:[4020F9],0              ;  reg?
00401281  |. 74 0F          JE SHORT CRK_ME2.00401292
00401283  |. 68 21214000    PUSH CRK_ME2.00402121                    ; /Text = ""
00401288  |. 6A 6E          PUSH 6E                                  ; |ControlID = 6E (110.)
0040128A  |. FF75 08        PUSH DWORD PTR SS:[EBP+8]                ; |hWnd
0040128D  |. E8 61000000    CALL <JMP.&USER32.SetDlgItemTextA>       ; \SetDlgItemTextA
00401292  |> 5E             POP ESI
00401293  |. 5F             POP EDI
00401294  |. 5B             POP EBX
00401295  |. C9             LEAVE
00401296  \. C2 0C00        RETN 0C


```


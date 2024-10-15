过检测，修bug

先晒方法：

```
*文件偏移
8DD: 80->E3
8DE: 82->E3
8DF: 80->E3
8E0: 85->E3
```

细节：

EP进来先跳第一个检测

```assembly
00401007 | E9 03010000          | jmp bugged.40110F                          |
```

这里会对401000~4010F8代码进行求和校验

```assembly
0040110F | 60                   | pushad                                     |
00401110 | 33DB                 | xor ebx,ebx                                |
00401112 | B8 00104000          | mov eax,<bugged.OptionalHeader.AddressOfEn |
00401117 | 90                   | nop                                        |
00401118 | 90                   | nop                                        |
00401119 | 90                   | nop                                        |
0040111A | 90                   | nop                                        |
0040111B | 90                   | nop                                        |
0040111C | 90                   | nop                                        |
0040111D | 90                   | nop                                        |
0040111E | 0318                 | add ebx,dword ptr ds:[eax]                 |
00401120 | 83C3 04              | add ebx,4                                  |
00401123 | 83C0 04              | add eax,4                                  |
00401126 | 3D F8104000          | cmp eax,bugged.4010F8                      |
0040112B | 75 EA                | jne bugged.401117                          | 循环0x3E次，对EP开头这段进行求和校验
0040112D | 3B1D FA304000        | cmp ebx,dword ptr ds:[4030FA]              | 004030FA:"煼愚"
00401133 | 0F84 D3FEFFFF        | je bugged.40100C                           |
00401139 | 6A 30                | push 30                                    |
0040113B | 68 1C214000          | push bugged.40211C                         | 40211C:"Bad checksum!!"
00401140 | 68 2B214000          | push bugged.40212B                         | 40212B:"A virus or a cracker ;-) has corrupted the program, please reinstall it."
00401145 | 6A 00                | push 0                                     |
00401147 | E8 DCFEFFFF          | call <JMP.&_MessageBoxA@16>                |
0040114C | E9 CEFEFFFF          | jmp bugged.40101F                          |
```

校验无误后会一路跳：

```
0040100D | EB 7E                | jmp bugged.40108D                          |
```

这段好像没啥用：

```
0040108D | 60                   | pushad                                     |
0040108E | 40                   | inc eax                                    |
0040108F | 48                   | dec eax                                    |
00401090 | 33C9                 | xor ecx,ecx                                | ecx:EntryPoint
00401092 | 83E9 02              | sub ecx,2                                  | ecx:EntryPoint
00401095 | 41                   | inc ecx                                    | ecx:EntryPoint
00401096 | 41                   | inc ecx                                    | ecx:EntryPoint
00401097 | 8BD9                 | mov ebx,ecx                                | ecx:EntryPoint
00401099 | 85DB                 | test ebx,ebx                               |
0040109B | 75 82                | jne bugged.40101F                          |
0040109D | 61                   | popad                                      |
0040109E | EB 37                | jmp bugged.4010D7                          |
```

接下来是释放最后的检查代码：

```
004010D7 | 60                   | pushad                                     |
004010D8 | B8 8D104000          | mov eax,bugged.40108D                      |
004010DD | BB C0304000          | mov ebx,4030C0                             |
004010E2 | 33C9                 | xor ecx,ecx                                | ecx:EntryPoint
004010E4 | 8A13                 | mov dl,byte ptr ds:[ebx]                   |
004010E6 | 80C2 02              | add dl,2                                   |
004010E9 | 80F2 75              | xor dl,75                                  |
004010EC | 8810                 | mov byte ptr ds:[eax],dl                   |
004010EE | 40                   | inc eax                                    |
004010EF | 43                   | inc ebx                                    |
004010F0 | 41                   | inc ecx                                    | ecx:EntryPoint
004010F1 | 83F9 3A              | cmp ecx,3A                                 | ecx:EntryPoint, 3A:':'
004010F4 | 75 EE                | jne bugged.4010E4                          | 循环0x3A次，释放新的检查代码到上个检查
004010F6 | 61                   | popad                                      |
004010F7 | EB 94                | jmp bugged.40108D                          |
```

可以看出从4030C0位置开始3A大小内容是被加密的（再后面好像是前面的校验值，不过用不上）

```
13 44 AC F4 B4 75 FC BC F4 9A 75 11 44 B3 33 FC
AB 3C FC B4 CC 21 73 73 73 80 84 80 83 80 82 80
85 12 1D 33 1B 73 43 33 73 1B 65 43 33 73 1D 73
9B 11 88 88 88 9A 3B 88 88 88
```

经过解密`+0x2，xor 0x75`，可以得到：

```
60 33 DB 83 C3 02 8B CB 83 E9 02 66 33 C0 40 8B
D8 4B 8B C3 BB 56 00 00 00 F7 F3 F7 F0 [F7 F1 F7 ←
F2] 61 6A 40 68 00 30 40 00 68 12 30 40 00 6A 00
E8 66 FF FF FF E9 48 FF FF FF
```

文件先摆着，反正等会儿也要执行这块代码

这是解密的代码

```assembly
0040108D | 60                   | pushad                                     |
0040108E | 33DB                 | xor ebx,ebx                                |
00401090 | 83C3 02              | add ebx,2                                  |
00401093 | 8BCB                 | mov ecx,ebx                                | ecx:EntryPoint
00401095 | 83E9 02              | sub ecx,2                                  | ecx:EntryPoint
00401098 | 66:33C0              | xor ax,ax                                  |
0040109B | 40                   | inc eax                                    |
0040109C | 8BD8                 | mov ebx,eax                                |
0040109E | 4B                   | dec ebx                                    |
0040109F | 8BC3                 | mov eax,ebx                                |
004010A1 | BB 56000000          | mov ebx,56                                 | 56:'V'
004010A6 | F7F3                 | div ebx                                    |
004010A8 | F7F0                 | div eax                                    |
004010AA | F7F1                 | div ecx                                    | <--
004010AC | F7F2                 | div edx                                    | <--
004010AE | 61                   | popad                                      |
004010AF | 6A 40                | push 40                                    |
004010B1 | 68 00304000          | push bugged.403000                         | 403000:"Congratulations!!"
004010B6 | 68 12304000          | push 403012                                | 403012:"Bug anihilated/destroyed/defated. :p. Coded a little in Masm32 and more in hex editor."
004010BB | 6A 00                | push 0                                     |
004010BD | E8 66FFFFFF          | call <JMP.&_MessageBoxA@16>                |
004010C2 | E9 48FFFFFF          | jmp bugged.40100F                          |
```

其中`004010AA`、`004010AC`会出现问题（/0），NOP掉

`F7 F1 F7 F2`这块换成`90 90 90 90` (也就是上边解密的二进制内容[]指出部分)

更换后重新加密回去`xor 0x75, -0x2`，最后可得到

```
13 44 AC F4 B4 75 FC BC F4 9A 75 11 44 B3 33 FC
AB 3C FC B4 CC 21 73 73 73 80 84 80 83 [E3 E3 E3
E3] 12 1D 33 1B 73 43 33 73 1B 65 43 33 73 1D 73
9B 11 88 88 88 9A 3B 88 88 88
```

用你的hex编辑器打开exe文件，转到文件偏移`0x8C0`处

```
8DD: 80->E3
8DE: 82->E3
8DF: 80->E3
8E0: 85->E3
```

(可选) 有时候好像过不去这个文件校验？直接Patch成`jmp bugged.40100C`

```
00401133 | 0F84 D3FEFFFF        | je bugged.40100C                           |
```


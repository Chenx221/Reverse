只有三个弹窗

```
00401000 | 6A 00                | push 0                                          |
00401002 | 68 00304000          | push crackme_01.403000                          | 403000:"Acid_Cool_178's"
00401007 | 68 10304000          | push crackme_01.403010                          | 403010:"Win32Asm Crackme 1"
0040100C | 6A 00                | push 0                                          |
0040100E | E8 2D000000          | call <JMP.&_MessageBoxA@16>                     |
00401013 | 6A 00                | push 0                                          |
00401015 | 68 23304000          | push crackme_01.403023                          | 403023:"Greetings goes too all my friends.."
0040101A | 68 47304000          | push crackme_01.403047                          | 403047:"Hellforge, tCA, FHCF, DQF and the rest..."
0040101F | 6A 00                | push 0                                          |
00401021 | E8 1A000000          | call <JMP.&_MessageBoxA@16>                     |
00401026 | 6A 00                | push 0                                          |
00401028 | 68 71304000          | push crackme_01.403071                          | 403071:"Remove Me!"
0040102D | 68 7C304000          | push crackme_01.40307C                          | 40307C:"NAG NAG"
00401032 | 6A 00                | push 0                                          |
00401034 | E8 07000000          | call <JMP.&_MessageBoxA@16>                     |
00401039 | 6A 00                | push 0                                          |
0040103B | E8 06000000          | call <JMP.&ExitProcess>                         |
00401040 | FF25 08204000        | jmp dword ptr ds:[<MessageBoxA>]                |
00401046 | FF25 00204000        | jmp dword ptr ds:[<ExitProcess>]                |
```



1. 去掉第三个NAG弹窗，patch

   ```
   00401026 | 6A 00                | push 0                                          |
   
   00401026 | EB 11                | jmp crackme_01.401039                           |
   ```

   
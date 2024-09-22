VT 10/73

会不会有别的猫腻？

判断逻辑：(已简化整理代码)

```c#
int serial1 = "user input"; // ebp-10
int serial2 = "user input"; // ebp-14
int a = 0x539; // ebp-4
int c = 2; //ebp-C

while(a!=0){
    serial2 ^= c++;
    a--;
}
if(serial1==serial2){
    //SUCCESS
}else{
    //FAIL
}
```

Main: 00401390

```assembly
00401390 | 55                   | push ebp                                     |
00401391 | 89E5                 | mov ebp,esp                                  |
00401393 | 83EC 28              | sub esp,28                                   |
00401396 | 83E4 F0              | and esp,FFFFFFF0                             |
00401399 | B8 00000000          | mov eax,0                                    |
0040139E | 83C0 0F              | add eax,F                                    |
004013A1 | 83C0 0F              | add eax,F                                    |
004013A4 | C1E8 04              | shr eax,4                                    |
004013A7 | C1E0 04              | shl eax,4                                    |
004013AA | 8945 E8              | mov dword ptr ss:[ebp-18],eax                |
004013AD | 8B45 E8              | mov eax,dword ptr ss:[ebp-18]                |
004013B0 | E8 F3BD0000          | call ac1d.materie.40D1A8                     |
004013B5 | E8 2EBA0000          | call ac1d.materie.40CDE8                     |
004013BA | C745 FC 39050000     | mov dword ptr ss:[ebp-4],539                 |
004013C1 | C745 F8 00CA9A3B     | mov dword ptr ss:[ebp-8],3B9ACA00            |
004013C8 | C745 F4 02000000     | mov dword ptr ss:[ebp-C],2                   | [ebp-0C]:&"ALLUSERSPROFILE=C:\\ProgramData"
004013CF | C74424 04 00004400   | mov dword ptr ss:[esp+4],ac1d.materie.440000 | 440000:"############################\n"
004013D7 | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
004013DE | E8 2DAB0300          | call <ac1d.materie.Cout>                     |
004013E3 | C74424 04 1E004400   | mov dword ptr ss:[esp+4],ac1d.materie.44001E | 44001E:"#____[ AC1D Materie#1 ]____#\n"
004013EB | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
004013F2 | E8 19AB0300          | call <ac1d.materie.Cout>                     |
004013F7 | C74424 04 3C004400   | mov dword ptr ss:[esp+4],ac1d.materie.44003C | 44003C:"#__[ by #ParadoxX[AC1D] ]__#\n"
004013FF | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
00401406 | E8 05AB0300          | call <ac1d.materie.Cout>                     |
0040140B | C74424 04 5C004400   | mov dword ptr ss:[esp+4],ac1d.materie.44005C | 44005C:"############################\n\n"
00401413 | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
0040141A | E8 F1AA0300          | call <ac1d.materie.Cout>                     |
0040141F | C74424 04 7B004400   | mov dword ptr ss:[esp+4],ac1d.materie.44007B | 44007B:"First Serial: "
00401427 | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
0040142E | E8 DDAA0300          | call <ac1d.materie.Cout>                     |
00401433 | 8D45 F0              | lea eax,dword ptr ss:[ebp-10]                |
00401436 | 894424 04            | mov dword ptr ss:[esp+4],eax                 | serial1
0040143A | C70424 60344400      | mov dword ptr ss:[esp],ac1d.materie.443460   |
00401441 | E8 926B0200          | call <ac1d.materie.Cin>                      |
00401446 | C74424 04 8A004400   | mov dword ptr ss:[esp+4],ac1d.materie.44008A | 44008A:"\nSecond Serial: "
0040144E | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
00401455 | E8 B6AA0300          | call <ac1d.materie.Cout>                     |
0040145A | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]                | [ebp-14]:__get_image_app_type+1B
0040145D | 894424 04            | mov dword ptr ss:[esp+4],eax                 | serial2
00401461 | C70424 60344400      | mov dword ptr ss:[esp],ac1d.materie.443460   |
00401468 | E8 6B6B0200          | call <ac1d.materie.Cin>                      |
0040146D | 837D FC 00           | cmp dword ptr ss:[ebp-4],0                   | init ebp-4:539 计算serial
00401471 | 74 14                | je ac1d.materie.401487                       |
00401473 | 8B55 F4              | mov edx,dword ptr ss:[ebp-C]                 |
00401476 | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]                |
00401479 | 3110                 | xor dword ptr ds:[eax],edx                   |
0040147B | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]                 | 
0040147E | FF00                 | inc dword ptr ds:[eax]                       |
00401480 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]                 |
00401483 | FF08                 | dec dword ptr ds:[eax]                       |
00401485 | EB E6                | jmp ac1d.materie.40146D                      |
00401487 | C74424 04 9B004400   | mov dword ptr ss:[esp+4],ac1d.materie.44009B | 44009B:"\nChecking...\n"
0040148F | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
00401496 | E8 75AA0300          | call <ac1d.materie.Cout>                     |
0040149B | C70424 E8030000      | mov dword ptr ss:[esp],3E8                   |
004014A2 | E8 C9F30000          | call <JMP.&_SleepStub@4>                     |
004014A7 | 83EC 04              | sub esp,4                                    |
004014AA | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]                |
004014AD | 3B45 EC              | cmp eax,dword ptr ss:[ebp-14]                | 检查
004014B0 | 74 22                | je ac1d.materie.4014D4                       |
004014B2 | C74424 04 A9004400   | mov dword ptr ss:[esp+4],ac1d.materie.4400A9 | Fail, 4400A9:"Wrong Serial!\a\n"
004014BA | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
004014C1 | E8 4AAA0300          | call <ac1d.materie.Cout>                     |
004014C6 | C70424 B9004400      | mov dword ptr ss:[esp],ac1d.materie.4400B9   | 4400B9:"pause"
004014CD | E8 66F20000          | call <JMP.&_system>                          |
004014D2 | EB 34                | jmp ac1d.materie.401508                      |
004014D4 | C74424 04 C0004400   | mov dword ptr ss:[esp+4],ac1d.materie.4400C0 | Success, 4400C0:"Solved! Show me your resolution at Crackmes.de + KeyGen!\n"
004014DC | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
004014E3 | E8 28AA0300          | call <ac1d.materie.Cout>                     |
004014E8 | C74424 04 FC004400   | mov dword ptr ss:[esp+4],ac1d.materie.4400FC | 4400FC:"Powered by #ParadoxX[AC1D] and Crackmes.de\n\n"
004014F0 | C70424 C0334400      | mov dword ptr ss:[esp],ac1d.materie.4433C0   |
004014F7 | E8 14AA0300          | call <ac1d.materie.Cout>                     |
004014FC | C70424 B9004400      | mov dword ptr ss:[esp],ac1d.materie.4400B9   | 4400B9:"pause"
00401503 | E8 30F20000          | call <JMP.&_system>                          |
00401508 | B8 00000000          | mov eax,0                                    |
0040150D | C9                   | leave                                        |
0040150E | C3                   | ret                                          |
```


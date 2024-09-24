？？？你不会是就换了一个打包工具吧

打包工具: ASPack(2.11)

1. 脱壳

   pushad后在ESP设置硬件断点(access,word)

   ```assembly
   0046A001 | 60                   | pushad                                       |
   0046A002 | E9 00000000          | jmp ad_cm#4.46A007                           | <--
   0046A007 | E8 24040000          | call ad_cm#4.46A430                          |
   0046A00C | EB 00                | jmp ad_cm#4.46A00E                           |
   0046A00E | BB 30394400          | mov ebx,ad_cm#4.443930                       |
   ```

   继续运行，会中断在这个地方

   ```assembly
   0046A3AB | 75 08                | jne ad_cm#4.46A3B5                           | <--
   0046A3AD | B8 01000000          | mov eax,1                                    | 
   0046A3B2 | C2 0C00              | ret C                                        |
   0046A3B5 | 68 F0844500          | push ad_cm#4.4584F0                          |
   0046A3BA | C3                   | ret                                          |
   ```

   禁用断点，F7 F8几下 来到`4584F0`

   ```assembly
   004584F0 | 55                   | push ebp                                     | OEP
   004584F1 | 8BEC                 | mov ebp,esp                                  |
   004584F3 | 83C4 F4              | add esp,FFFFFFF4                             |
   004584F6 | B8 A0834500          | mov eax,ad_cm#4.4583A0                       |
   ```

   然后一套流程下来(dump, ImportREC修正(OEP: 000584F0))

2. 随便翻一下就可以找到有帮助的内容了(例如搜索“Enter you name”)

   下面是稍微整理过的内容：(可能存在一点类型错误，实际计算代码请看source)

   ```c#
   string name = "user input"; // ebp-14 ebp-8 
   string serial = "user input"; // ebp-18 
   string true_serial = ""; // ebp-c
   string part = "ADCM4-";
   string part2 = "-YEAH!";
   int time = name.Length; // edi
   int index = 1; // ebx
   
   do {
       char v1 = name[index - 1]; // esi
       char v2 = v1; // eax
       int v3 = 6; // ecx
       v2 /= v3; // eax = v1 / 6
       char v5 = (char)(v1 >> 2);
       v2 *= v5; // eax *= edx (v5)
       char v6 = v1; // eax
       int v7 = 0xA; // ecx
       v6 /= v7; // eax = v1 / 10
       int v8 = v2;
       var temp = v6; 
       v6 = (char)v8; 
       v8 = temp; 
       int v9 = v8;
       string v10 = "";
   	v6 /= (char)v9;
       v10 = v6.ToString();
       true_serial += v10;
       index++;
       time--;
   } while (time > 0);
   
   true_serial = part + true_serial + part2;
   ```

细节: 

```assembly
00458159 | 55                   | push ebp                                     |
0045815A | 68 90824500          | push <ad_cm#4.sub_458290>                    |
0045815F | 64:FF30              | push dword ptr fs:[eax]                      |
00458162 | 64:8920              | mov dword ptr fs:[eax],esp                   |
00458165 | 8D55 F8              | lea edx,dword ptr ss:[ebp-8]                 |
00458168 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
0045816B | 8B80 D8020000        | mov eax,dword ptr ds:[eax+2D8]               |
00458171 | E8 16BFFCFF          | call <ad_cm#4.GetText>                       |
00458176 | 8D55 EC              | lea edx,dword ptr ss:[ebp-14]                | [ebp-14]:&"l貮"
00458179 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
0045817C | 8B80 D8020000        | mov eax,dword ptr ds:[eax+2D8]               |
00458182 | E8 05BFFCFF          | call <ad_cm#4.GetText>                       |
00458187 | 837D EC 00           | cmp dword ptr ss:[ebp-14],0                  | [ebp-14]:&"l貮"
0045818B | 75 0A                | jne ad_cm#4.458197                           |
0045818D | B8 A8824500          | mov eax,<ad_cm#4.sub_4582A8>                 | 4582A8:"Enter you name, pls."
00458192 | E8 4DC1FEFF          | call <ad_cm#4.ShowMessage>                   |
00458197 | 8D55 E8              | lea edx,dword ptr ss:[ebp-18]                |
0045819A | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
0045819D | 8B80 DC020000        | mov eax,dword ptr ds:[eax+2DC]               |
004581A3 | E8 E4BEFCFF          | call <ad_cm#4.GetText>                       |
004581A8 | 837D E8 00           | cmp dword ptr ss:[ebp-18],0                  |
004581AC | 75 0A                | jne ad_cm#4.4581B8                           |
004581AE | B8 C8824500          | mov eax,ad_cm#4.4582C8                       | 4582C8:"Enter the serial, pls."
004581B3 | E8 2CC1FEFF          | call <ad_cm#4.ShowMessage>                   |
004581B8 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]                 |
004581BB | E8 BCB9FAFF          | call <ad_cm#4.GetLength>                     |
004581C0 | 8BF8                 | mov edi,eax                                  |
004581C2 | 85FF                 | test edi,edi                                 |
004581C4 | 7E 50                | jle ad_cm#4.458216                           |
004581C6 | BB 01000000          | mov ebx,1                                    |
004581CB | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]                 |
004581CE | 0FB67418 FF          | movzx esi,byte ptr ds:[eax+ebx-1]            |
004581D3 | 8BC6                 | mov eax,esi                                  |
004581D5 | B9 06000000          | mov ecx,6                                    | ecx:"p粿"
004581DA | 33D2                 | xor edx,edx                                  |
004581DC | F7F1                 | div ecx                                      | ecx:"p粿"
004581DE | 8B55 F8              | mov edx,dword ptr ss:[ebp-8]                 |
004581E1 | 8BD6                 | mov edx,esi                                  |
004581E3 | C1EA 02              | shr edx,2                                    |
004581E6 | F7EA                 | imul edx                                     |
004581E8 | 50                   | push eax                                     |
004581E9 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]                 |
004581EC | 8BC6                 | mov eax,esi                                  |
004581EE | B9 0A000000          | mov ecx,A                                    | ecx:"p粿", 0A:'\n'
004581F3 | 33D2                 | xor edx,edx                                  |
004581F5 | F7F1                 | div ecx                                      | ecx:"p粿"
004581F7 | 5A                   | pop edx                                      |
004581F8 | 92                   | xchg edx,eax                                 |
004581F9 | 8BCA                 | mov ecx,edx                                  | ecx:"p粿"
004581FB | 33D2                 | xor edx,edx                                  |
004581FD | F7F1                 | div ecx                                      | ecx:"p粿"
004581FF | 8D55 E4              | lea edx,dword ptr ss:[ebp-1C]                |
00458202 | E8 FDF8FAFF          | call <ad_cm#4.IntToStr>                      |
00458207 | 8B55 E4              | mov edx,dword ptr ss:[ebp-1C]                |
0045820A | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]                 |
0045820D | E8 72B9FAFF          | call <ad_cm#4._LStrCat>                      |
00458212 | 43                   | inc ebx                                      |
00458213 | 4F                   | dec edi                                      |
00458214 | 75 B5                | jne ad_cm#4.4581CB                           | loop
00458216 | 68 E8824500          | push ad_cm#4.4582E8                          | 4582E8:"ADCM4-"
0045821B | FF75 F4              | push dword ptr ss:[ebp-C]                    |
0045821E | 68 F8824500          | push ad_cm#4.4582F8                          | 4582F8:"-YEAH!"
00458223 | 8D45 F0              | lea eax,dword ptr ss:[ebp-10]                |
00458226 | BA 03000000          | mov edx,3                                    |
0045822B | E8 0CBAFAFF          | call <ad_cm#4.LStrCatN>                      |
00458230 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]                |
00458233 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
00458236 | 8B80 DC020000        | mov eax,dword ptr ds:[eax+2DC]               |
0045823C | E8 4BBEFCFF          | call <ad_cm#4.GetText>                       |
00458241 | 8B55 E0              | mov edx,dword ptr ss:[ebp-20]                |
00458244 | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]                |
00458247 | E8 40BAFAFF          | call <ad_cm#4._LStrCmp>                      |
0045824C | 75 0A                | jne ad_cm#4.458258                           |
0045824E | B8 08834500          | mov eax,<ad_cm#4.sub_458308>                 | 458308:"Well done Cracker, You did it!"
00458253 | E8 8CC0FEFF          | call <ad_cm#4.ShowMessage>                   |
00458258 | 33C0                 | xor eax,eax                                  |
0045825A | 5A                   | pop edx                                      |
```


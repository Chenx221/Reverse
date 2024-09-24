VT 22/73

DIE跑了下，打包工具: Petite(2.2)

1. PEtite脱壳

   在pushad后给esp下硬件read断点(word)

   ```assembly
   0046D042 | B8 00D04600          | mov eax,ad_cm#3.46D000                       | eax:@BaseThreadInitThunk@12
   0046D047 | 68 B4A64500          | push ad_cm#3.45A6B4                          |
   0046D04C | 64:FF35 00000000     | push dword ptr fs:[0]                        |
   0046D053 | 64:8925 00000000     | mov dword ptr fs:[0],esp                     |
   0046D05A | 66:9C                | pushf                                        |
   0046D05C | 60                   | pushad                                       |
   0046D05D | 50                   | push eax                                     | <--
   ```

   继续运行，来到这样的地方(底下全是jmp)

   ```assembly
   0046D03D | 66:9D                | popf                                         |
   0046D03F | 83C4 08              | add esp,8                                    |
   0046D042 | B8 79B4FEFF          | mov eax,FFFEB479                             |
   ```

   F8、F7几下，来到OEP

   ```assembly
   004584C0 | 55                   | push ebp                                     | OEP
   004584C1 | 8BEC                 | mov ebp,esp                                  |
   004584C3 | 83C4 F4              | add esp,FFFFFFF4                             |
   004584C6 | B8 70834500          | mov eax,<ad_cm#3.sub_458370>                 |
   ```

   打开Scylla，dump；再打开ImportREC，附加到运行中的`AD_CM#3.exe`。IAT自动搜索>获取导入表>自动跟踪>修正转储>修改修正后的文件EP为`584C0`

2. 随便翻找一下，然后就可以整理得到这样的内容

   ```c#
   string name = "user input"; // ebp-4
   string serial = "user input"; //ebp-14
   string result = ""; //ebp-8
   string part = "ADCM3-";
   int time = name.Length; //ebx
   v1 = 1; //esi
   do {
       string v2 = name; //eax
       char v3 = name[v1-1]; //eax
       int v4 = 3; //ecx
       string v5 = ''; //edx
       v3 /= v4;
       v5 = v3.tostring();
       result += v5;
       v1++;
       time--;
   }while(time>0);
   result = part+result;
   ```

细节: 

```assembly
00458173 | 55                   | push ebp                                     |
00458174 | 68 71824500          | push <ad_cm#3.sub_458271>                    |
00458179 | 64:FF30              | push dword ptr fs:[eax]                      |
0045817C | 64:8920              | mov dword ptr fs:[eax],esp                   |
0045817F | 8D55 FC              | lea edx,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
00458182 | 8B87 D8020000        | mov eax,dword ptr ds:[edi+2D8]               |
00458188 | E8 FFBEFCFF          | call <ad_cm#3.GetText>                       |
0045818D | 8D55 F0              | lea edx,dword ptr ss:[ebp-10]                |
00458190 | 8B87 D8020000        | mov eax,dword ptr ds:[edi+2D8]               |
00458196 | E8 F1BEFCFF          | call <ad_cm#3.GetText>                       |
0045819B | 837D F0 00           | cmp dword ptr ss:[ebp-10],0                  | 检查name长度
0045819F | 75 0A                | jne ad_cm#3.4581AB                           |
004581A1 | B8 88824500          | mov eax,ad_cm#3.458288                       | 458288:"Enter you name, pls."
004581A6 | E8 39C1FEFF          | call <ad_cm#3.ShowMessage>                   |
004581AB | 8D55 EC              | lea edx,dword ptr ss:[ebp-14]                | [ebp-14]:&"l貮"
004581AE | 8B87 DC020000        | mov eax,dword ptr ds:[edi+2DC]               |
004581B4 | E8 D3BEFCFF          | call <ad_cm#3.GetText>                       |
004581B9 | 837D EC 00           | cmp dword ptr ss:[ebp-14],0                  | 检查serial长度
004581BD | 75 0A                | jne ad_cm#3.4581C9                           |
004581BF | B8 A8824500          | mov eax,<ad_cm#3.sub_4582A8>                 | 4582A8:"Enter the serial, pls."
004581C4 | E8 1BC1FEFF          | call <ad_cm#3.ShowMessage>                   |
004581C9 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
004581CC | E8 ABB9FAFF          | call <ad_cm#3.sub_403B7C>                    | 可能是获取length
004581D1 | 8BD8                 | mov ebx,eax                                  | eax: name.length
004581D3 | 85DB                 | test ebx,ebx                                 |
004581D5 | 7E 2D                | jle ad_cm#3.458204                           |
004581D7 | BE 01000000          | mov esi,1                                    |
004581DC | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l貮"
004581DF | 0FB64430 FF          | movzx eax,byte ptr ds:[eax+esi-1]            |
004581E4 | B9 03000000          | mov ecx,3                                    | ecx:sub_458271
004581E9 | 33D2                 | xor edx,edx                                  |
004581EB | F7F1                 | div ecx                                      | ecx:sub_458271
004581ED | 8D55 E8              | lea edx,dword ptr ss:[ebp-18]                |
004581F0 | E8 0FF9FAFF          | call <ad_cm#3.IntToStr>                      |
004581F5 | 8B55 E8              | mov edx,dword ptr ss:[ebp-18]                |
004581F8 | 8D45 F8              | lea eax,dword ptr ss:[ebp-8]                 |
004581FB | E8 84B9FAFF          | call <ad_cm#3._LStrCat>                      |
00458200 | 46                   | inc esi                                      |
00458201 | 4B                   | dec ebx                                      |
00458202 | 75 D8                | jne ad_cm#3.4581DC                           | loop
00458204 | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]                 |
00458207 | 8B4D F8              | mov ecx,dword ptr ss:[ebp-8]                 | ecx:sub_458271
0045820A | BA C8824500          | mov edx,ad_cm#3.4582C8                       | 4582C8:"ADCM3-"
0045820F | E8 B4B9FAFF          | call <ad_cm#3._LStrCat3>                     |
00458214 | 8D55 E4              | lea edx,dword ptr ss:[ebp-1C]                |
00458217 | 8B87 DC020000        | mov eax,dword ptr ds:[edi+2DC]               |
0045821D | E8 6ABEFCFF          | call <ad_cm#3.GetText>                       |
00458222 | 8B55 E4              | mov edx,dword ptr ss:[ebp-1C]                |
00458225 | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]                 |
00458228 | E8 5FBAFAFF          | call <ad_cm#3._LStrCmp>                      | 结果比较
0045822D | 75 0A                | jne ad_cm#3.458239                           |
0045822F | B8 D8824500          | mov eax,ad_cm#3.4582D8                       | Success
00458234 | E8 ABC0FEFF          | call <ad_cm#3.ShowMessage>                   |
00458239 | 33C0                 | xor eax,eax                                  |
0045823B | 5A                   | pop edx                                      |
0045823C | 59                   | pop ecx                                      | ecx:sub_458271
0045823D | 59                   | pop ecx                                      | ecx:sub_458271
```


打包工具: ASPack(2.000)

1. 脱壳

   步骤和上一篇类似，OEP: 445834 (45834)

2. 搜索“Registered"找到可疑函数

   这里整理了一下：

   ```c#
   //读取同目录下cm5.dat
   //第一行name //ebp-1E8 //ebp-1E9开头含长度位
   //第二行serial //ebp-1FD //ebp-1FE开头含长度位
   //每行最大读取0x14长度
   
   string name = "cm5.dat 第一行";
   int length = name.Length; //esi
   string v = "159357852645875692311335664857125469857213526859478212124569348647951232165728761953213754495421375678543126721831"； //ebp-8
   string result = ""; //ebp-4
   int p=0;
   
   do{
       int v3 = name[p]; //edx
       result+=(char)(v[v3-0xB]);
       p++;
       length--;
   } while (length>0);
   ```

细节:

```assembly
004453C6 | 55                   | push ebp                                     |
004453C7 | 68 87554400          | push <ad_cm#5.sub_445587>                    |
004453CC | 64:FF30              | push dword ptr fs:[eax]                      |
004453CF | 64:8920              | mov dword ptr fs:[eax],esp                   |
004453D2 | 8D45 F8              | lea eax,dword ptr ss:[ebp-8]                 |
004453D5 | BA A0554400          | mov edx,ad_cm#5.4455A0                       | 4455A0:"159357852645875692311335664857125469857213526859478212124569348647951232165728761953213754495421375678543126721831"
004453DA | E8 99E5FBFF          | call <ad_cm#5.sub_403978>                    |
004453DF | 33D2                 | xor edx,edx                                  |
004453E1 | 55                   | push ebp                                     |
004453E2 | 68 3F554400          | push <ad_cm#5.sub_44553F>                    |
004453E7 | 64:FF32              | push dword ptr fs:[edx]                      |
004453EA | 64:8922              | mov dword ptr fs:[edx],esp                   |
004453ED | BA 1C564400          | mov edx,<ad_cm#5.sub_44561C>                 | 44561C:"cm5.dat"
004453F2 | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
004453F8 | E8 AD00FCFF          | call <ad_cm#5.ASSIGN>                        | ebp-1D4: File var
004453FD | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
00445403 | E8 EF02FCFF          | call <ad_cm#5.RESETTEXT>                     | 准备读取文件内容
00445408 | E8 8FD3FBFF          | call <ad_cm#5._IOTest>                       |
0044540D | 8D95 17FEFFFF        | lea edx,dword ptr ss:[ebp-1E9]               | ebp-1E9: Read content（Name)
00445413 | B9 14000000          | mov ecx,14                                   | 读取0x14长度内容
00445418 | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
0044541E | E8 D901FCFF          | call <ad_cm#5.READSTRING>                    |
00445423 | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
00445429 | E8 6602FCFF          | call <ad_cm#5.READLN>                        |
0044542E | E8 69D3FBFF          | call <ad_cm#5._IOTest>                       |
00445433 | 8D95 02FEFFFF        | lea edx,dword ptr ss:[ebp-1FE]               | ebp-1FE: Read content(Serial)
00445439 | B9 14000000          | mov ecx,14                                   | 读取0x14长度内容
0044543E | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
00445444 | E8 B301FCFF          | call <ad_cm#5.READSTRING>                    |
00445449 | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
0044544F | E8 4002FCFF          | call <ad_cm#5.READLN>                        |
00445454 | E8 43D3FBFF          | call <ad_cm#5._IOTest>                       |
00445459 | 8D85 2CFEFFFF        | lea eax,dword ptr ss:[ebp-1D4]               |
0044545F | E8 E800FCFF          | call <ad_cm#5.CLOSE>                         |
00445464 | E8 33D3FBFF          | call <ad_cm#5._IOTest>                       |
00445469 | 80BD 17FEFFFF 05     | cmp byte ptr ss:[ebp-1E9],5                  | 检查Name长度需>=5
00445470 | 73 0A                | jae ad_cm#5.44547C                           |
00445472 | B8 2C564400          | mov eax,<ad_cm#5.sub_44562C>                 | 44562C:"Name must be at least 5 characters long!"
00445477 | E8 A4F8FFFF          | call <ad_cm#5.ShowMessage>                   |
0044547C | 0FB6B5 17FEFFFF      | movzx esi,byte ptr ss:[ebp-1E9]              |
00445483 | 85F6                 | test esi,esi                                 |
00445485 | 7E 2E                | jle ad_cm#5.4454B5                           |
00445487 | 8D9D 18FEFFFF        | lea ebx,dword ptr ss:[ebp-1E8]               |
0044548D | 8D85 FCFDFFFF        | lea eax,dword ptr ss:[ebp-204]               |
00445493 | 33D2                 | xor edx,edx                                  |
00445495 | 8A13                 | mov dl,byte ptr ds:[ebx]                     |
00445497 | 8B4D F8              | mov ecx,dword ptr ss:[ebp-8]                 |
0044549A | 8A5411 F5            | mov dl,byte ptr ds:[ecx+edx-B]               |
0044549E | E8 E5E5FBFF          | call <ad_cm#5.sub_403A88>                    |
004454A3 | 8B95 FCFDFFFF        | mov edx,dword ptr ss:[ebp-204]               |
004454A9 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l贎"
004454AC | E8 B7E6FBFF          | call <ad_cm#5._LStrCat>                      |
004454B1 | 43                   | inc ebx                                      |
004454B2 | 4E                   | dec esi                                      |
004454B3 | 75 D8                | jne ad_cm#5.44548D                           |
004454B5 | 8D85 F8FDFFFF        | lea eax,dword ptr ss:[ebp-208]               |
004454BB | 8D95 02FEFFFF        | lea edx,dword ptr ss:[ebp-1FE]               |
004454C1 | E8 3EE6FBFF          | call <ad_cm#5.Len>                           |
004454C6 | 8B85 F8FDFFFF        | mov eax,dword ptr ss:[ebp-208]               |
004454CC | 8B55 FC              | mov edx,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l贎"
004454CF | E8 9CE7FBFF          | call <ad_cm#5._LStrCmp>                      |
004454D4 | 75 55                | jne ad_cm#5.44552B                           |
004454D6 | 8D85 F4FDFFFF        | lea eax,dword ptr ss:[ebp-20C]               |
004454DC | 8D95 17FEFFFF        | lea edx,dword ptr ss:[ebp-1E9]               |
004454E2 | E8 1DE6FBFF          | call <ad_cm#5.Len>                           |
004454E7 | 8B95 F4FDFFFF        | mov edx,dword ptr ss:[ebp-20C]               |
004454ED | 8B87 D4020000        | mov eax,dword ptr ds:[edi+2D4]               |
004454F3 | E8 B4F5FDFF          | call <ad_cm#5.SetText>                       |
004454F8 | 8B87 D8020000        | mov eax,dword ptr ds:[edi+2D8]               |
004454FE | 8B55 FC              | mov edx,dword ptr ss:[ebp-4]                 | [ebp-04]:&"l贎"
00445501 | E8 A6F5FDFF          | call <ad_cm#5.SetText>                       |
00445506 | 8B87 E8020000        | mov eax,dword ptr ds:[edi+2E8]               |
0044550C | BA 60564400          | mov edx,<ad_cm#5.sub_445660>                 | 445660:"Registered ... well done!"
00445511 | E8 96F5FDFF          | call <ad_cm#5.SetText>                       |
00445516 | 8B87 E8020000        | mov eax,dword ptr ds:[edi+2E8]               |
0044551C | 8B40 58              | mov eax,dword ptr ds:[eax+58]                |
0044551F | BA 00800000          | mov edx,8000                                 |
```


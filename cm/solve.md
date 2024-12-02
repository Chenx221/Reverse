算SN码~~（不要看成算N码了~~

先上几组可用SN码：

```
1A2A3A1A2A3A1C2C3C1C2C3C1A3B3B1A1A3B3B1A1A3B3B1A1A3B3B1A1A3B3B1A
3C3B1C2C3B2B2C1B1B3B2B3C1A2A3A1A3B2C3C1B2C3A2A2B3A3C1B3A3A1C2B3A
```

解释：

这里的SN码只接受64位长度，每4位一组，奇数位接受1~3数字，偶数位接受A~C字符

通过观察可以发现一块重要数据：

```
Mem:
	 5  6  7  8  9  A  B  C  D  E  F  0  1  2  3  4
0056F315 00 00 00 00 00 00 00 00 00 00 00[02 00 02]00 00 ................ 
0056F325 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................ 
0056F335 00 00 00 00 00[00 00 00]00 00 00 00 00 00 00 00 ................ 
0056F345 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00[01 ................ 
0056F355 00 01]00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................ 
```

稍微整理之后可以得到：

```c#
int[,] Data = {
    { 2,0,2 },
    { 0,0,0 },
    { 1,0,1 }
};
//和Data对应的关系是
//    { 1A,1B,1C },
//    { 2A,2B,2C },
//    { 3A,3B,3C }
```

结合EAX: [1]
EBX: [0]*0x1A
RESULT: 56F2C5+EAX+EBX，前两位要求RESULT不为0，后两位则要求为0，故我们可得到最初状态下的要求：

```
一组前两位:[1A,3A,1C,3C]
后两位:[2A,1B,2B,3B,2C]
```

继续观察会发现以四位为一组的字符串，例如`1A2A`可以视为1A->2A，1A=0，也就是复制1A数据到2A上，1A填充0

观察right前最后的一点代码，可见最后要求Data得是这样的：(说白了就是交换位置)

```c#
int[,] trueData = {
    { 1,0,1 },
    { 0,0,0 },
    { 2,0,2 }
};
```

这样一来我们的SN码就有两种思路：

1. 爆破

   如果你看了我的keygen，你会发现我用的就是爆破的方法...

   ```c#
   public static void CalcSerial()
   {
       StringBuilder result = new();
       Random random = new();
       do
       {
           result.Clear();
           string[] prefix = ["1A", "3A", "1C", "3C"];
           string[] suffix = ["2A", "1B", "2B", "3B", "2C"];
           while (result.Length < 64)
           {
               // 从prefix数组中随机选取一个元素
               int prefixIndex = random.Next(prefix.Length);
               string prefixSelected = prefix[prefixIndex];
   
               // 从suffix数组中随机选取一个元素
               int suffixIndex = random.Next(suffix.Length);
               string suffixSelected = suffix[suffixIndex];
   
               // 将选中的元素交换
               prefix[prefixIndex] = suffixSelected;
               suffix[suffixIndex] = prefixSelected;
   
               // 组合选中的两个元素并添加到结果
               result.Append(prefixSelected);
               result.Append(suffixSelected);
           }
       } while(!Check(result.ToString()));
   
       // 打印结果
       Console.WriteLine(result.ToString());
   }
   
   public static bool Check(string serial)
   {
       int[,] Data = {
           { 2,0,2 },
           { 0,0,0 },
           { 1,0,1 }
       };
       if (serial.Length != 64)
           return false;
       for (int i = 0; i < 16; i++)
       {
           int y1 = serial[i * 4] - '1';
           int x1 = serial[i * 4 + 1] - 'A';
           int y2 = serial[i * 4 + 2] - '1';
           int x2 = serial[i * 4 + 3] - 'A';            
           Data[y2, x2] = Data[y1, x1];
           Data[y1, x1] = 0;
       }
       if (Data[0, 0] == 1 && Data[0, 2] == 1 && Data[2, 0] == 2 && Data[2, 2] == 2) //交换完成?
           return true;
       return false;
   }
   ```

   爆破这个很快，所以这是一个方法

2. 根据逻辑自己推SN，然后用无用数据填充到64位

           //EX: 1A2A 3A1A 2A3A  1C2C 3C1C 2C3C //24
           //EX: 1A3B 3B1A 1A3B3B1A 1A3B3B1A1A3B3B1A1A3B3B1A //40

​	这里就有一组例子，前24位，我们将**2**0**2**与**1**0**1**交换，中间000用来临时存放数据，完成这24位实际上已经达成目标

​	后40位为无用操作，因为限制只能从非0位上移动数据，所以这里使用大量1A与3B作交换操作填充

细节：

```assembly
004203B0 | 55               | push ebp                              | CheckBtnEVE
004203B1 | 89E5             | mov ebp,esp                           |
004203B3 | 81EC 88000000    | sub esp,88                            |
004203B9 | 899D 78FFFFFF    | mov dword ptr ss:[ebp-88],ebx         |
004203BF | 89B5 7CFFFFFF    | mov dword ptr ss:[ebp-84],esi         |
004203C5 | 897D 80          | mov dword ptr ss:[ebp-80],edi         |
004203C8 | 8945 F8          | mov dword ptr ss:[ebp-8],eax          |
004203CB | 8955 FC          | mov dword ptr ss:[ebp-4],edx          |
004203CE | C745 84 00000000 | mov dword ptr ss:[ebp-7C],0           |
004203D5 | C745 88 00000000 | mov dword ptr ss:[ebp-78],0           |
004203DC | C745 8C 00000000 | mov dword ptr ss:[ebp-74],0           |
004203E3 | C745 90 00000000 | mov dword ptr ss:[ebp-70],0           |
004203EA | C745 94 00000000 | mov dword ptr ss:[ebp-6C],0           |
004203F1 | C745 98 00000000 | mov dword ptr ss:[ebp-68],0           | [ebp-68]:&L"幌畐叱瘞"
004203F8 | C745 9C 00000000 | mov dword ptr ss:[ebp-64],0           | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
004203FF | C745 A0 00000000 | mov dword ptr ss:[ebp-60],0           |
00420406 | C745 A4 00000000 | mov dword ptr ss:[ebp-5C],0           |
0042040D | C745 A8 00000000 | mov dword ptr ss:[ebp-58],0           | [ebp-58]:GetWindowThreadProcessId+9B
00420414 | C745 B0 00000000 | mov dword ptr ss:[ebp-50],0           |
0042041B | C745 B4 00000000 | mov dword ptr ss:[ebp-4C],0           | [ebp-4C]:L"幌畐叱瘞"
00420422 | C745 B8 00000000 | mov dword ptr ss:[ebp-48],0           |
00420429 | C745 BC 00000000 | mov dword ptr ss:[ebp-44],0           |
00420430 | C745 C0 00000000 | mov dword ptr ss:[ebp-40],0           |
00420437 | C745 C4 00000000 | mov dword ptr ss:[ebp-3C],0           |
0042043E | C745 C8 00000000 | mov dword ptr ss:[ebp-38],0           |
00420445 | C745 CC 00000000 | mov dword ptr ss:[ebp-34],0           |
0042044C | 8D4D EC          | lea ecx,dword ptr ss:[ebp-14]         |
0042044F | 8D55 D4          | lea edx,dword ptr ss:[ebp-2C]         |
00420452 | B8 01000000      | mov eax,1                             |
00420457 | E8 84ADFEFF      | call cm.40B1E0                        |
0042045C | E8 7FCDFEFF      | call cm.40D1E0                        |
00420461 | 50               | push eax                              |
00420462 | 85C0             | test eax,eax                          |
00420464 | 0F85 5D050000    | jne cm.4209C7                         |
0042046A | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042046D | 8B80 40040000    | mov eax,dword ptr ds:[eax+440]        |
00420473 | B2 00            | mov dl,0                              |
00420475 | 8B4D F8          | mov ecx,dword ptr ss:[ebp-8]          |
00420478 | 8B89 40040000    | mov ecx,dword ptr ds:[ecx+440]        |
0042047E | 8B09             | mov ecx,dword ptr ds:[ecx]            |
00420480 | FF91 18020000    | call dword ptr ds:[ecx+218]           |
00420486 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420489 | 8B80 54040000    | mov eax,dword ptr ds:[eax+454]        |
0042048F | B2 01            | mov dl,1                              |
00420491 | E8 3A570C00      | call cm.4E5BD0                        |
00420496 | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]         |
00420499 | E8 3235FEFF      | call cm.4039D0                        |
0042049E | C745 CC 00000000 | mov dword ptr ss:[ebp-34],0           |
004204A5 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004204A8 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004204AE | 8D55 CC          | lea edx,dword ptr ss:[ebp-34]         |
004204B1 | E8 4A9E0B00      | call <cm.GetText>                     | Get SN value
004204B6 | 8B45 CC          | mov eax,dword ptr ss:[ebp-34]         | [ebp-34]:SN
004204B9 | 85C0             | test eax,eax                          |
004204BB | 74 03            | je cm.4204C0                          |
004204BD | 8B40 FC          | mov eax,dword ptr ds:[eax-4]          |
004204C0 | 83F8 40          | cmp eax,40                            | eax: length == 0x40
004204C3 | 74 08            | je cm.4204CD                          |
004204C5 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004204C8 | E8 93060000      | call <cm.WrongSN>                     |
004204CD | C705 70F35600 01 | mov dword ptr ds:[56F370],1           | Loop index init: 1
004204D7 | FF0D 70F35600    | dec dword ptr ds:[56F370]             | SN共64位，这里循环32次，每次处理两位，[1~3][A~C]...
004204DD | FF05 70F35600    | inc dword ptr ds:[56F370]             | ---Loop Start
004204E3 | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]         |
004204E6 | E8 E534FEFF      | call cm.4039D0                        |
004204EB | C745 CC 00000000 | mov dword ptr ss:[ebp-34],0           |
004204F2 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004204F5 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004204FB | 8D55 CC          | lea edx,dword ptr ss:[ebp-34]         |
004204FE | E8 FD9D0B00      | call <cm.GetText>                     | Get SN
00420503 | 8B55 CC          | mov edx,dword ptr ss:[ebp-34]         | [ebp-34]:SN
00420506 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042050B | 8D0445 FFFFFFFF  | lea eax,dword ptr ds:[eax*2-1]        |
00420512 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
00420517 | 83F8 30          | cmp eax,30                            | >'0'
0042051A | 7E 39            | jle <cm.WrongSN2>                     |
0042051C | 8D45 C8          | lea eax,dword ptr ss:[ebp-38]         |
0042051F | E8 AC34FEFF      | call cm.4039D0                        |
00420524 | C745 C8 00000000 | mov dword ptr ss:[ebp-38],0           |
0042052B | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042052E | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420534 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]         |
00420537 | E8 C49D0B00      | call <cm.GetText>                     |
0042053C | 8B55 C8          | mov edx,dword ptr ss:[ebp-38]         | [ebp-38]:SN
0042053F | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420544 | 8D0445 FFFFFFFF  | lea eax,dword ptr ds:[eax*2-1]        |
0042054B | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
00420550 | 83F8 34          | cmp eax,34                            | 34:'4'
00420553 | 7C 0D            | jl cm.420562                          | <'4'
00420555 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN2
00420558 | E8 03060000      | call <cm.WrongSN>                     |
0042055D | E9 65040000      | jmp cm.4209C7                         |
00420562 | 8D45 C4          | lea eax,dword ptr ss:[ebp-3C]         |
00420565 | E8 6634FEFF      | call cm.4039D0                        |
0042056A | C745 C4 00000000 | mov dword ptr ss:[ebp-3C],0           |
00420571 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420574 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
0042057A | 8D55 C4          | lea edx,dword ptr ss:[ebp-3C]         |
0042057D | E8 7E9D0B00      | call <cm.GetText>                     |
00420582 | 8B55 C4          | mov edx,dword ptr ss:[ebp-3C]         | [ebp-3C]:SN
00420585 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042058A | D1E0             | shl eax,1                             | <<1
0042058C | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
00420591 | 83F8 40          | cmp eax,40                            | 40:'@'
00420594 | 7E 34            | jle <cm.WrongSN3>                     | >'@'
00420596 | 8D45 C0          | lea eax,dword ptr ss:[ebp-40]         |
00420599 | E8 3234FEFF      | call cm.4039D0                        |
0042059E | C745 C0 00000000 | mov dword ptr ss:[ebp-40],0           |
004205A5 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004205A8 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004205AE | 8D55 C0          | lea edx,dword ptr ss:[ebp-40]         |
004205B1 | E8 4A9D0B00      | call <cm.GetText>                     |
004205B6 | 8B55 C0          | mov edx,dword ptr ss:[ebp-40]         | [ebp-40]:SN
004205B9 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004205BE | D1E0             | shl eax,1                             |
004205C0 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
004205C5 | 83F8 44          | cmp eax,44                            | 44:'D'
004205C8 | 7C 0D            | jl cm.4205D7                          | <'D'
004205CA | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN3
004205CD | E8 8E050000      | call <cm.WrongSN>                     |
004205D2 | E9 F0030000      | jmp cm.4209C7                         |
004205D7 | 833D 70F35600 20 | cmp dword ptr ds:[56F370],20          | 20:' '
004205DE | 0F8C F9FEFFFF    | jl cm.4204DD                          | Next Loop---
004205E4 | C705 70F35600 01 | mov dword ptr ds:[56F370],1           | Loop index init 1
004205EE | FF0D 70F35600    | dec dword ptr ds:[56F370]             | 每四个一组，循环16次
004205F4 | FF05 70F35600    | inc dword ptr ds:[56F370]             | ---Loop Start
004205FA | 8D45 BC          | lea eax,dword ptr ss:[ebp-44]         |
004205FD | E8 CE33FEFF      | call cm.4039D0                        |
00420602 | C745 BC 00000000 | mov dword ptr ss:[ebp-44],0           |
00420609 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042060C | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420612 | 8D55 BC          | lea edx,dword ptr ss:[ebp-44]         |
00420615 | E8 E69C0B00      | call <cm.GetText>                     |
0042061A | 8B55 BC          | mov edx,dword ptr ss:[ebp-44]         | [ebp-44]:SN
0042061D | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420622 | 8D0485 FDFFFFFF  | lea eax,dword ptr ds:[eax*4-3]        |
00420629 | 0FB65C02 FF      | movzx ebx,byte ptr ds:[edx+eax-1]     |
0042062E | 83EB 30          | sub ebx,30                            | char2int(ebx)
00420631 | 6BDB 1A          | imul ebx,ebx,1A                       | ebx*=0x1A
00420634 | 8D45 B8          | lea eax,dword ptr ss:[ebp-48]         |
00420637 | E8 9433FEFF      | call cm.4039D0                        |
0042063C | C745 B8 00000000 | mov dword ptr ss:[ebp-48],0           |
00420643 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420646 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
0042064C | 8D55 B8          | lea edx,dword ptr ss:[ebp-48]         |
0042064F | E8 AC9C0B00      | call <cm.GetText>                     |
00420654 | 8B55 B8          | mov edx,dword ptr ss:[ebp-48]         | [ebp-48]:SN
00420657 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042065C | 8D0485 FEFFFFFF  | lea eax,dword ptr ds:[eax*4-2]        |
00420663 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
00420668 | 8A8403 C5F25600  | mov al,byte ptr ds:[ebx+eax+56F2C5]   |
0042066F | 84C0             | test al,al                            |
00420671 | 75 0D            | jne cm.420680                         |
00420673 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN4
00420676 | E8 E5040000      | call <cm.WrongSN>                     |
0042067B | E9 47030000      | jmp cm.4209C7                         |
00420680 | 8D45 B4          | lea eax,dword ptr ss:[ebp-4C]         | [ebp-4C]:L"幌畐叱瘞"
00420683 | E8 4833FEFF      | call cm.4039D0                        |
00420688 | C745 B4 00000000 | mov dword ptr ss:[ebp-4C],0           | [ebp-4C]:L"幌畐叱瘞"
0042068F | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420692 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420698 | 8D55 B4          | lea edx,dword ptr ss:[ebp-4C]         | [ebp-4C]:L"幌畐叱瘞"
0042069B | E8 609C0B00      | call <cm.GetText>                     |
004206A0 | 8B55 B4          | mov edx,dword ptr ss:[ebp-4C]         | [ebp-4C]:SN
004206A3 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004206A8 | 8D0485 FFFFFFFF  | lea eax,dword ptr ds:[eax*4-1]        |
004206AF | 0FB65C02 FF      | movzx ebx,byte ptr ds:[edx+eax-1]     |
004206B4 | 83EB 30          | sub ebx,30                            | char2int(ebx)
004206B7 | 6BDB 1A          | imul ebx,ebx,1A                       | ebx*=0x1A
004206BA | 8D45 B0          | lea eax,dword ptr ss:[ebp-50]         |
004206BD | E8 0E33FEFF      | call cm.4039D0                        |
004206C2 | C745 B0 00000000 | mov dword ptr ss:[ebp-50],0           |
004206C9 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004206CC | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004206D2 | 8D55 B0          | lea edx,dword ptr ss:[ebp-50]         |
004206D5 | E8 269C0B00      | call <cm.GetText>                     |
004206DA | 8B55 B0          | mov edx,dword ptr ss:[ebp-50]         | SN
004206DD | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004206E2 | C1E0 02          | shl eax,2                             |
004206E5 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
004206EA | 8A8403 C5F25600  | mov al,byte ptr ds:[ebx+eax+56F2C5]   |
004206F1 | 84C0             | test al,al                            |
004206F3 | 74 0D            | je cm.420702                          |
004206F5 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN5
004206F8 | E8 63040000      | call <cm.WrongSN>                     |
004206FD | E9 C5020000      | jmp cm.4209C7                         |
00420702 | 8D45 A8          | lea eax,dword ptr ss:[ebp-58]         | [ebp-58]:GetWindowThreadProcessId+9B
00420705 | E8 C632FEFF      | call cm.4039D0                        |
0042070A | C745 A8 00000000 | mov dword ptr ss:[ebp-58],0           | [ebp-58]:GetWindowThreadProcessId+9B
00420711 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420714 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
0042071A | 8D55 A8          | lea edx,dword ptr ss:[ebp-58]         | [ebp-58]:GetWindowThreadProcessId+9B
0042071D | E8 DE9B0B00      | call <cm.GetText>                     |
00420722 | 8B55 A8          | mov edx,dword ptr ss:[ebp-58]         | SN
00420725 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042072A | 8D0485 FDFFFFFF  | lea eax,dword ptr ds:[eax*4-3]        |
00420731 | 0FB65C02 FF      | movzx ebx,byte ptr ds:[edx+eax-1]     |
00420736 | 83EB 30          | sub ebx,30                            | char2int(ebx)
00420739 | 8D45 A4          | lea eax,dword ptr ss:[ebp-5C]         |
0042073C | E8 8F32FEFF      | call cm.4039D0                        |
00420741 | C745 A4 00000000 | mov dword ptr ss:[ebp-5C],0           |
00420748 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042074B | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420751 | 8D55 A4          | lea edx,dword ptr ss:[ebp-5C]         |
00420754 | E8 A79B0B00      | call <cm.GetText>                     |
00420759 | 8B55 A4          | mov edx,dword ptr ss:[ebp-5C]         | SN
0042075C | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420761 | 8D0485 FFFFFFFF  | lea eax,dword ptr ds:[eax*4-1]        |
00420768 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
0042076D | 83E8 30          | sub eax,30                            | char2int(eax)
00420770 | 29C3             | sub ebx,eax                           | ebx-=eax
00420772 | 895D AC          | mov dword ptr ss:[ebp-54],ebx         | s
00420775 | 8B45 AC          | mov eax,dword ptr ss:[ebp-54]         |
00420778 | 0FAFD8           | imul ebx,eax                          | ebx=(ebx-eax)*(ebx-eax)
0042077B | 8D45 A0          | lea eax,dword ptr ss:[ebp-60]         |
0042077E | E8 4D32FEFF      | call cm.4039D0                        |
00420783 | C745 A0 00000000 | mov dword ptr ss:[ebp-60],0           |
0042078A | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042078D | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420793 | 8D55 A0          | lea edx,dword ptr ss:[ebp-60]         |
00420796 | E8 659B0B00      | call <cm.GetText>                     |
0042079B | 8B55 A0          | mov edx,dword ptr ss:[ebp-60]         | SN
0042079E | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004207A3 | 8D0485 FEFFFFFF  | lea eax,dword ptr ds:[eax*4-2]        |
004207AA | 0FB67402 FF      | movzx esi,byte ptr ds:[edx+eax-1]     |
004207AF | 83EE 40          | sub esi,40                            | A->1... C->3
004207B2 | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]         | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
004207B5 | E8 1632FEFF      | call cm.4039D0                        |
004207BA | C745 9C 00000000 | mov dword ptr ss:[ebp-64],0           | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
004207C1 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004207C4 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004207CA | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]         | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
004207CD | E8 2E9B0B00      | call <cm.GetText>                     |
004207D2 | 8B55 9C          | mov edx,dword ptr ss:[ebp-64]         | SN
004207D5 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004207DA | C1E0 02          | shl eax,2                             |
004207DD | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
004207E2 | 83E8 40          | sub eax,40                            | A->1... C->3
004207E5 | 29C6             | sub esi,eax                           |
004207E7 | 8975 AC          | mov dword ptr ss:[ebp-54],esi         |
004207EA | 8B45 AC          | mov eax,dword ptr ss:[ebp-54]         |
004207ED | 8B55 AC          | mov edx,dword ptr ss:[ebp-54]         |
004207F0 | 0FAFC2           | imul eax,edx                          | eax=(esi-eax)*(esi-eax)
004207F3 | 01C3             | add ebx,eax                           |
004207F5 | F7D3             | not ebx                               |
004207F7 | 83FB 0A          | cmp ebx,A                             | 0A:'\n'
004207FA | 75 0D            | jne cm.420809                         | ?
004207FC | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN6
004207FF | E8 5C030000      | call <cm.WrongSN>                     |
00420804 | E9 BE010000      | jmp cm.4209C7                         |
00420809 | 8D45 98          | lea eax,dword ptr ss:[ebp-68]         | [ebp-68]:&L"幌畐叱瘞"
0042080C | E8 BF31FEFF      | call cm.4039D0                        |
00420811 | C745 98 00000000 | mov dword ptr ss:[ebp-68],0           | [ebp-68]:&L"幌畐叱瘞"
00420818 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042081B | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420821 | 8D55 98          | lea edx,dword ptr ss:[ebp-68]         | [ebp-68]:&L"幌畐叱瘞"
00420824 | E8 D79A0B00      | call <cm.GetText>                     |
00420829 | 8B55 98          | mov edx,dword ptr ss:[ebp-68]         | [ebp-68]:&L"幌畐叱瘞"
0042082C | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420831 | 8D0485 FFFFFFFF  | lea eax,dword ptr ds:[eax*4-1]        |
00420838 | 0FB67402 FF      | movzx esi,byte ptr ds:[edx+eax-1]     |
0042083D | 83EE 30          | sub esi,30                            |
00420840 | 6BF6 1A          | imul esi,esi,1A                       | esi*=0x1A
00420843 | 8D45 94          | lea eax,dword ptr ss:[ebp-6C]         |
00420846 | E8 8531FEFF      | call cm.4039D0                        |
0042084B | C745 94 00000000 | mov dword ptr ss:[ebp-6C],0           |
00420852 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420855 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
0042085B | 8D55 94          | lea edx,dword ptr ss:[ebp-6C]         |
0042085E | E8 9D9A0B00      | call <cm.GetText>                     |
00420863 | 8B55 94          | mov edx,dword ptr ss:[ebp-6C]         |
00420866 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042086B | C1E0 02          | shl eax,2                             |
0042086E | 0FB67C02 FF      | movzx edi,byte ptr ds:[edx+eax-1]     |
00420873 | 8D45 90          | lea eax,dword ptr ss:[ebp-70]         |
00420876 | E8 5531FEFF      | call cm.4039D0                        |
0042087B | C745 90 00000000 | mov dword ptr ss:[ebp-70],0           |
00420882 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420885 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
0042088B | 8D55 90          | lea edx,dword ptr ss:[ebp-70]         |
0042088E | E8 6D9A0B00      | call <cm.GetText>                     |
00420893 | 8B55 90          | mov edx,dword ptr ss:[ebp-70]         |
00420896 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
0042089B | 8D0485 FDFFFFFF  | lea eax,dword ptr ds:[eax*4-3]        |
004208A2 | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
004208A7 | 83E8 30          | sub eax,30                            |
004208AA | 6BD8 1A          | imul ebx,eax,1A                       |
004208AD | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]         |
004208B0 | E8 1B31FEFF      | call cm.4039D0                        |
004208B5 | C745 8C 00000000 | mov dword ptr ss:[ebp-74],0           |
004208BC | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004208BF | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
004208C5 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]         |
004208C8 | E8 339A0B00      | call <cm.GetText>                     |
004208CD | 8B55 8C          | mov edx,dword ptr ss:[ebp-74]         |
004208D0 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
004208D5 | 8D0485 FEFFFFFF  | lea eax,dword ptr ds:[eax*4-2]        |
004208DC | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
004208E1 | 8DBC3E C5F25600  | lea edi,dword ptr ds:[esi+edi+56F2C5] |
004208E8 | 8DB403 C5F25600  | lea esi,dword ptr ds:[ebx+eax+56F2C5] |
004208EF | FC               | cld                                   |
004208F0 | A4               | movsb                                 | esi->edi
004208F1 | 8D45 88          | lea eax,dword ptr ss:[ebp-78]         |
004208F4 | E8 D730FEFF      | call cm.4039D0                        |
004208F9 | C745 88 00000000 | mov dword ptr ss:[ebp-78],0           |
00420900 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
00420903 | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420909 | 8D55 88          | lea edx,dword ptr ss:[ebp-78]         |
0042090C | E8 EF990B00      | call <cm.GetText>                     |
00420911 | 8B55 88          | mov edx,dword ptr ss:[ebp-78]         |
00420914 | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420919 | 8D0485 FDFFFFFF  | lea eax,dword ptr ds:[eax*4-3]        |
00420920 | 0FB65C02 FF      | movzx ebx,byte ptr ds:[edx+eax-1]     |
00420925 | 83EB 30          | sub ebx,30                            |
00420928 | 6BDB 1A          | imul ebx,ebx,1A                       | *=0x1A
0042092B | 8D45 84          | lea eax,dword ptr ss:[ebp-7C]         |
0042092E | E8 9D30FEFF      | call cm.4039D0                        |
00420933 | C745 84 00000000 | mov dword ptr ss:[ebp-7C],0           |
0042093A | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
0042093D | 8B80 4C040000    | mov eax,dword ptr ds:[eax+44C]        |
00420943 | 8D55 84          | lea edx,dword ptr ss:[ebp-7C]         |
00420946 | E8 B5990B00      | call <cm.GetText>                     |
0042094B | 8B55 84          | mov edx,dword ptr ss:[ebp-7C]         |
0042094E | A1 70F35600      | mov eax,dword ptr ds:[56F370]         |
00420953 | 8D0485 FEFFFFFF  | lea eax,dword ptr ds:[eax*4-2]        |
0042095A | 0FB64402 FF      | movzx eax,byte ptr ds:[edx+eax-1]     |
0042095F | C68403 C5F25600  | mov byte ptr ds:[ebx+eax+56F2C5],0    |
00420967 | 833D 70F35600 10 | cmp dword ptr ds:[56F370],10          |
0042096E | 0F8C 80FCFFFF    | jl cm.4205F4                          | Next Loop---
00420974 | A0 20F35600      | mov al,byte ptr ds:[56F320]           |
00420979 | 3A05 22F35600    | cmp al,byte ptr ds:[56F322]           |
0042097F | 75 09            | jne <cm.WrongSN7>                     |
00420981 | A0 22F35600      | mov al,byte ptr ds:[56F322]           |
00420986 | 3C 01            | cmp al,1                              |
00420988 | 74 0A            | je cm.420994                          | 1?1
0042098A | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN7
0042098D | E8 CE010000      | call <cm.WrongSN>                     |
00420992 | EB 33            | jmp cm.4209C7                         |
00420994 | A0 54F35600      | mov al,byte ptr ds:[56F354]           |
00420999 | 3A05 56F35600    | cmp al,byte ptr ds:[56F356]           |
0042099F | 75 09            | jne <cm.WrongSN8>                     |
004209A1 | A0 56F35600      | mov al,byte ptr ds:[56F356]           |
004209A6 | 3C 02            | cmp al,2                              |
004209A8 | 74 0A            | je cm.4209B4                          | 2?2
004209AA | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          | WrongSN8
004209AD | E8 AE010000      | call <cm.WrongSN>                     |
004209B2 | EB 13            | jmp cm.4209C7                         |
004209B4 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]          |
004209B7 | 8B80 40040000    | mov eax,dword ptr ds:[eax+440]        |
004209BD | BA D8BD5200      | mov edx,cm.52BDD8                     | 52BDD8:"Right"
004209C2 | E8 89C90B00      | call cm.4DD350                        |
004209C7 | E8 E4AAFEFF      | call cm.40B4B0                        |
004209CC | 8D45 84          | lea eax,dword ptr ss:[ebp-7C]         |
004209CF | E8 FC2FFEFF      | call cm.4039D0                        |
004209D4 | C745 84 00000000 | mov dword ptr ss:[ebp-7C],0           |
004209DB | 8D45 88          | lea eax,dword ptr ss:[ebp-78]         |
004209DE | E8 ED2FFEFF      | call cm.4039D0                        |
004209E3 | C745 88 00000000 | mov dword ptr ss:[ebp-78],0           |
004209EA | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]         |
004209ED | E8 DE2FFEFF      | call cm.4039D0                        |
004209F2 | C745 8C 00000000 | mov dword ptr ss:[ebp-74],0           |
004209F9 | 8D45 90          | lea eax,dword ptr ss:[ebp-70]         |
004209FC | E8 CF2FFEFF      | call cm.4039D0                        |
00420A01 | C745 90 00000000 | mov dword ptr ss:[ebp-70],0           |
00420A08 | 8D45 94          | lea eax,dword ptr ss:[ebp-6C]         |
00420A0B | E8 C02FFEFF      | call cm.4039D0                        |
00420A10 | C745 94 00000000 | mov dword ptr ss:[ebp-6C],0           |
00420A17 | 8D45 98          | lea eax,dword ptr ss:[ebp-68]         | [ebp-68]:&L"幌畐叱瘞"
00420A1A | E8 B12FFEFF      | call cm.4039D0                        |
00420A1F | C745 98 00000000 | mov dword ptr ss:[ebp-68],0           | [ebp-68]:&L"幌畐叱瘞"
00420A26 | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]         | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
00420A29 | E8 A22FFEFF      | call cm.4039D0                        |
00420A2E | C745 9C 00000000 | mov dword ptr ss:[ebp-64],0           | [ebp-64]:MsgWaitForMultipleObjectsEx+D8
00420A35 | 8D45 A0          | lea eax,dword ptr ss:[ebp-60]         |
00420A38 | E8 932FFEFF      | call cm.4039D0                        |
00420A3D | C745 A0 00000000 | mov dword ptr ss:[ebp-60],0           |
00420A44 | 8D45 A4          | lea eax,dword ptr ss:[ebp-5C]         |
00420A47 | E8 842FFEFF      | call cm.4039D0                        |
00420A4C | C745 A4 00000000 | mov dword ptr ss:[ebp-5C],0           |
00420A53 | 8D45 A8          | lea eax,dword ptr ss:[ebp-58]         | [ebp-58]:GetWindowThreadProcessId+9B
00420A56 | E8 752FFEFF      | call cm.4039D0                        |
00420A5B | C745 A8 00000000 | mov dword ptr ss:[ebp-58],0           | [ebp-58]:GetWindowThreadProcessId+9B
00420A62 | 8D45 B0          | lea eax,dword ptr ss:[ebp-50]         |
00420A65 | E8 662FFEFF      | call cm.4039D0                        |
00420A6A | C745 B0 00000000 | mov dword ptr ss:[ebp-50],0           |
00420A71 | 8D45 B4          | lea eax,dword ptr ss:[ebp-4C]         | [ebp-4C]:L"幌畐叱瘞"
00420A74 | E8 572FFEFF      | call cm.4039D0                        |
00420A79 | C745 B4 00000000 | mov dword ptr ss:[ebp-4C],0           | [ebp-4C]:L"幌畐叱瘞"
00420A80 | 8D45 B8          | lea eax,dword ptr ss:[ebp-48]         |
00420A83 | E8 482FFEFF      | call cm.4039D0                        |
00420A88 | C745 B8 00000000 | mov dword ptr ss:[ebp-48],0           |
00420A8F | 8D45 BC          | lea eax,dword ptr ss:[ebp-44]         |
00420A92 | E8 392FFEFF      | call cm.4039D0                        |
00420A97 | C745 BC 00000000 | mov dword ptr ss:[ebp-44],0           |
00420A9E | 8D45 C0          | lea eax,dword ptr ss:[ebp-40]         |
00420AA1 | E8 2A2FFEFF      | call cm.4039D0                        |
00420AA6 | C745 C0 00000000 | mov dword ptr ss:[ebp-40],0           |
00420AAD | 8D45 C4          | lea eax,dword ptr ss:[ebp-3C]         |
00420AB0 | E8 1B2FFEFF      | call cm.4039D0                        |
00420AB5 | C745 C4 00000000 | mov dword ptr ss:[ebp-3C],0           |
00420ABC | 8D45 C8          | lea eax,dword ptr ss:[ebp-38]         |
00420ABF | E8 0C2FFEFF      | call cm.4039D0                        |
00420AC4 | C745 C8 00000000 | mov dword ptr ss:[ebp-38],0           |
00420ACB | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]         |
00420ACE | E8 FD2EFEFF      | call cm.4039D0                        |
00420AD3 | C745 CC 00000000 | mov dword ptr ss:[ebp-34],0           |
00420ADA | 58               | pop eax                               |
00420ADB | 85C0             | test eax,eax                          |
00420ADD | 74 05            | je cm.420AE4                          |
00420ADF | E8 3CABFEFF      | call cm.40B620                        |
00420AE4 | 8B9D 78FFFFFF    | mov ebx,dword ptr ss:[ebp-88]         |
00420AEA | 8BB5 7CFFFFFF    | mov esi,dword ptr ss:[ebp-84]         |
00420AF0 | 8B7D 80          | mov edi,dword ptr ss:[ebp-80]         |
00420AF3 | C9               | leave                                 |
00420AF4 | C3               | ret                                   |
```


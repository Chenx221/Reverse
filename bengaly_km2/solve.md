UPX，先脱壳

1. 先脱壳

   OEP: 4001000

   方法和以前一样，这里不多赘述

2. 计算serial

   ```assembly
   00401300 | 33F6             | xor esi,esi                              |
   00401302 | 8BC8             | mov ecx,eax                              | ecx: length of the name
   00401304 | B8 01000000      | mov eax,1                                |
   00401309 | 8B15 38304000    | mov edx,dword ptr ds:[403038]            | 根据Name计算Serial
   0040130F | 8A90 37304000    | mov dl,byte ptr ds:[eax+403037]          |
   00401315 | 81E2 FF000000    | and edx,FF                               |
   0040131B | 8BDA             | mov ebx,edx                              |
   0040131D | 0FAFDA           | imul ebx,edx                             |
   00401320 | 03F3             | add esi,ebx                              |
   00401322 | 8BDA             | mov ebx,edx                              |
   00401324 | D1FB             | sar ebx,1                                |
   00401326 | 03F3             | add esi,ebx                              |
   00401328 | 2BF2             | sub esi,edx                              |
   0040132A | 40               | inc eax                                  |
   0040132B | 49               | dec ecx                                  |
   0040132C | 75 DB            | jne key-crackme2_dump_.401309            |
   0040132E | 56               | push esi                                 |
   0040132F | 68 38314000      | push key-crackme2_dump_.403138           |
   00401334 | E8 4A000000      | call <key-crackme2_dump_.Str2Int>        |
   00401339 | 5E               | pop esi                                  |
   0040133A | 3BC6             | cmp eax,esi                              | EAX: Userinput, ESI: True Serial
   0040133C | 75 15            | jne <key-crackme2_dump_.WrongSerial>     |
   0040133E | 6A 00            | push 0                                   |
   00401340 | 68 62344000      | push key-crackme2_dump_.403462           | 403462:"Key/CrackMe #2   "
   00401345 | 68 B8344000      | push key-crackme2_dump_.4034B8           | 4034B8:" Good Job, I Wish You the Very Best"
   0040134A | 6A 00            | push 0                                   |
   0040134C | E8 9D000000      | call <JMP.&_MessageBoxA@16>              |
   00401351 | EB 13            | jmp key-crackme2_dump_.401366            |
   00401353 | 6A 00            | push 0                                   |
   00401355 | 68 62344000      | push key-crackme2_dump_.403462           | 403462:"Key/CrackMe #2   "
   0040135A | 68 86344000      | push key-crackme2_dump_.403486           | 403486:" You Have Enter A Wrong Serial, Please Try Again "
   0040135F | 6A 00            | push 0                                   |
   00401361 | E8 88000000      | call <JMP.&_MessageBoxA@16>              |
   00401366 | EB 15            | jmp key-crackme2_dump_.40137D            |
   ```

   其中计算serial部分可以精简整理成以下代码：

   ```c#
   string name = "UserInput";
   int esi = 0;
   foreach (char c in name)
   {
       esi += c * c + c / 2 - c;
   }
   ```

   
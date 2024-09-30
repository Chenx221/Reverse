还是熟悉的配方

1. 直接找serial算法

   ```assembly
   004012F6 | 68 3F304000      | push key4.40303F                         |
   004012FB | E8 34010000      | call <JMP.&_lstrlenAStub@4>              |
   00401300 | 33F6             | xor esi,esi                              |
   00401302 | 33DB             | xor ebx,ebx                              |
   00401304 | 8BC8             | mov ecx,eax                              |
   00401306 | B8 01000000      | mov eax,1                                |
   0040130B | 8B1D 3F304000    | mov ebx,dword ptr ds:[40303F]            |
   00401311 | 0FBE90 1F354000  | movsx edx,byte ptr ds:[eax+40351F]       |
   00401318 | 2BDA             | sub ebx,edx                              |
   0040131A | 0FAFDA           | imul ebx,edx                             |
   0040131D | 8BF3             | mov esi,ebx                              |
   0040131F | 2BD8             | sub ebx,eax                              |
   00401321 | 81C3 43353504    | add ebx,4353543                          |
   00401327 | 03F3             | add esi,ebx                              |
   00401329 | 33F2             | xor esi,edx                              |
   0040132B | B8 04000000      | mov eax,4                                |
   00401330 | 49               | dec ecx                                  |
   00401331 | 75 D8            | jne key4.40130B                          |
   00401333 | 56               | push esi                                 |
   00401334 | 68 3F314000      | push key4.40313F                         |
   00401339 | E8 4A000000      | call key4.401388                         |
   0040133E | 5E               | pop esi                                  |
   0040133F | 3BC6             | cmp eax,esi                              | EAX: UserInput, ESI: True Serial
   00401341 | 75 15            | jne key4.401358                          |
   00401343 | 6A 00            | push 0                                   |
   00401345 | 68 8C344000      | push key4.40348C                         | 40348C:"KeygenMe #3"
   0040134A | 68 DD344000      | push key4.4034DD                         | 4034DD:" Great, You are ranked as Level-3 at Keygening now"
   0040134F | 6A 00            | push 0                                   |
   00401351 | E8 9C000000      | call <JMP.&_MessageBoxA@16>              |
   00401356 | EB 13            | jmp key4.40136B                          |
   00401358 | 6A 00            | push 0                                   |
   0040135A | 68 8C344000      | push key4.40348C                         | 40348C:"KeygenMe #3"
   0040135F | 68 AA344000      | push key4.4034AA                         | 4034AA:" You Have Entered A Wrong Serial, Please Try Again"
   00401364 | 6A 00            | push 0                                   |
   00401366 | E8 87000000      | call <JMP.&_MessageBoxA@16>              |
   0040136B | EB 15            | jmp key4.401382                          |
   ```

   整理一下：

   ```c#
   int eax = 1;
   int esi = 0;
   string s = "%@$erwr#@$$!@#21$@^&*&(%rthdhdfw423%#DSgfY$%^#$%bre#B@@%#G3re";
   int length = name.Length;
   int v = 0;
   while (length > 0)
   {
       v = BitConverter.ToInt32(Encoding.ASCII.GetBytes(name), 0);
       char c = s[eax - 1];
       v -= c;
       v *= c;
       esi = v;
       v -= eax;
       v += 0x4353543;
       esi += v;
       esi ^= c;
       eax = 0x4;
       length--;
   }
   ```

   


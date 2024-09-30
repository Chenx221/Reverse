不清楚是不是兼容问题，这个crackme在winxp、win7都无法显示窗体

1. 修bug

   排查了一下

   ```assembly
   0040108B | 6A 00            | push 0                                         |
   0040108D | 6A 00            | push 0                                         |
   0040108F | 6A 00            | push 0                                         |
   00401091 | 68 1F304000      | push crackme1.40301F                           | 40301F:"MainWindow"
   00401096 | FF35 3C314000    | push dword ptr ds:[40313C]                     |
   0040109C | E8 1F020000      | call <JMP.&_CreateDialogParamA@20>             |
   ```

   这里call完之后返回值为null，错误`0x57F`

   尝试下修一下

   ```assembly
   00401096 | FF35 3C314000    | push dword ptr ds:[40313C]                     | Patch
   
   00401096 | 6A 00            | push 0                                         |
   00401098 | 90               | nop                                            |
   00401099 | 90               | nop                                            |
   0040109A | 90               | nop                                            |
   0040109B | 90               | nop                                            |
   ```

2. name

   ```assembly
   0040124B | 55               | push ebp                                       |
   0040124C | 8BEC             | mov ebp,esp                                    |
   0040124E | 53               | push ebx                                       |
   0040124F | 51               | push ecx                                       | ecx:_lstrlenA@4+2F
   00401250 | 52               | push edx                                       |
   00401251 | 57               | push edi                                       |
   00401252 | 56               | push esi                                       |
   00401253 | 55               | push ebp                                       |
   00401254 | 33D2             | xor edx,edx                                    |
   00401256 | 8B75 08          | mov esi,dword ptr ss:[ebp+8]                   |
   00401259 | 8B4D 0C          | mov ecx,dword ptr ss:[ebp+C]                   | ecx:_lstrlenA@4+2F
   0040125C | 49               | dec ecx                                        | ecx:_lstrlenA@4+2F
   0040125D | 0FBE06           | movsx eax,byte ptr ds:[esi]                    | 循环
   00401260 | 2B05 00304000    | sub eax,dword ptr ds:[403000]                  |
   00401266 | 0FBE5E 01        | movsx ebx,byte ptr ds:[esi+1]                  |
   0040126A | 2B1D 04304000    | sub ebx,dword ptr ds:[403004]                  |
   00401270 | 0FAFC3           | imul eax,ebx                                   |
   00401273 | 03D0             | add edx,eax                                    |
   00401275 | 46               | inc esi                                        |
   00401276 | E2 E5            | loop crackme1_fix.40125D                       |
   00401278 | 8BC2             | mov eax,edx                                    |
   0040127A | 5D               | pop ebp                                        |
   0040127B | 5E               | pop esi                                        |
   0040127C | 5F               | pop edi                                        |
   0040127D | 5A               | pop edx                                        |
   0040127E | 59               | pop ecx                                        | ecx:_lstrlenA@4+2F
   0040127F | 5B               | pop ebx                                        |
   00401280 | C9               | leave                                          |
   00401281 | C2 0800          | ret 8                                          |
   ```

   ```c#
   string name = "userinput";
   int ecx = name.Length - 1;
   int index = 0;
   int edx = 0;
   
   while (ecx > 0)
   {
       char c = (char)(name[index] - '*');
       char c2 = (char)(name[index + 1] - 'A');
       c = (char)(c * c2);
       edx += (int)c;
       index++;
       ecx--;
   }
   ```

   Name（不能使用数字）会计算出一个edx值

3. Serial

   ```assembly
   00401284 | 55               | push ebp                                       |
   00401285 | 8BEC             | mov ebp,esp                                    |
   00401287 | 53               | push ebx                                       |
   00401288 | 51               | push ecx                                       | ecx:_lstrlenA@4+2F
   00401289 | 52               | push edx                                       |
   0040128A | 56               | push esi                                       |
   0040128B | 57               | push edi                                       |
   0040128C | 55               | push ebp                                       |
   0040128D | 8B75 08          | mov esi,dword ptr ss:[ebp+8]                   |
   00401290 | 8B4D 0C          | mov ecx,dword ptr ss:[ebp+C]                   | ecx:_lstrlenA@4+2F
   00401293 | 33D2             | xor edx,edx                                    |
   00401295 | 0FB64431 FF      | movzx eax,byte ptr ds:[ecx+esi-1]              |
   0040129A | 83E8 30          | sub eax,30                                     |
   0040129D | 51               | push ecx                                       | ecx:_lstrlenA@4+2F
   0040129E | 8BD9             | mov ebx,ecx                                    | ecx:_lstrlenA@4+2F
   004012A0 | 2B4D 0C          | sub ecx,dword ptr ss:[ebp+C]                   | ecx:_lstrlenA@4+2F
   004012A3 | F7D9             | neg ecx                                        | ecx:_lstrlenA@4+2F
   004012A5 | 85C9             | test ecx,ecx                                   | ecx:_lstrlenA@4+2F
   004012A7 | 74 05            | je crackme1_fix.4012AE                         |
   004012A9 | 6BC0 0A          | imul eax,eax,A                                 |
   004012AC | E2 FB            | loop crackme1_fix.4012A9                       |
   004012AE | 59               | pop ecx                                        | ecx:_lstrlenA@4+2F
   004012AF | 03D0             | add edx,eax                                    |
   004012B1 | E2 E2            | loop crackme1_fix.401295                       |
   004012B3 | 8BC2             | mov eax,edx                                    |
   004012B5 | 5D               | pop ebp                                        |
   004012B6 | 5F               | pop edi                                        |
   004012B7 | 5E               | pop esi                                        |
   004012B8 | 5A               | pop edx                                        |
   004012B9 | 59               | pop ecx                                        | ecx:_lstrlenA@4+2F
   004012BA | 5B               | pop ebx                                        |
   004012BB | C9               | leave                                          |
   004012BC | C2 0800          | ret 8                                          |
   ```

   这里似乎只是将传入的serial字符串转成整形，后面比较用


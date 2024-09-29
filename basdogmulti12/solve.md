找serial

1. 第一个serial

   ```assembly
   00401767 | 8D55 F8          | lea edx,dword ptr ss:[ebp-8]                  | [ebp-08]:"753159"
   0040176A | 58               | pop eax                                       |
   0040176B | E8 54120000      | call <multicrackme v1.2.==>                   | #1 eax cmp edx
   ```

2. 第二个serial

   ```assembly
   004017E9 | 8D55 F0          | lea edx,dword ptr ss:[ebp-10]                 | [ebp-10]:"753209"
   004017EC | 58               | pop eax                                       |
   004017ED | E8 D2110000      | call <multicrackme v1.2.==>                   | #2
   ```

3. 第三个serial

   ```assembly
   0040186B | 8D55 E8          | lea edx,dword ptr ss:[ebp-18]                 | [ebp-18]:"753259"
   0040186E | 58               | pop eax                                       | 
   0040186F | E8 50110000      | call <multicrackme v1.2.==>                   | #3
   ```

4. 第四个serial

   ```assembly
   004018F0 | 8D55 E0          | lea edx,dword ptr ss:[ebp-20]                 | [ebp-20]:"755159"
   004018F3 | 58               | pop eax                                       | 
   004018F4 | E8 CB100000      | call <multicrackme v1.2.==>                   | #4
   ```

5. 最后的serial

   ```assembly
   00401E79 | 8D55 F8          | lea edx,dword ptr ss:[ebp-8]                   | [ebp-08]:"Yes-I-am-The-Best"
   00401E7C | 58               | pop eax                                        |
   00401E7D | E8 420B0000      | call <multicrackme v1.2.==>                    | #Final
   ```

   相关函数

   | Address  | Module/Label/Exception                 | Disassembly | Hits | Summary      |
   | -------- | -------------------------------------- | ----------- | ---- | ------------ |
   | 0040170C | \<multicrackme v1.2.exe.Button1Click\> | push ebp    | 0    | Button1Click |
   | 00401E30 | \<multicrackme v1.2.exe.Edit2Change\>  | push ebp    | 1    | Edit2Change  |
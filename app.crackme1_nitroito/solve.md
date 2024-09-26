没加壳

阅读作者的要求

```
 1. Remove the NAG screen...
 2. Find a correct serial or create a keygen
    (NO PATCH) is to easy to patch... ]:)
```

开始：

1. 在EP处可以看到我们需要去除的NAG弹窗

   ```assembly
   004047A0 | 55                   | push ebp                                     |
   004047A1 | 8BEC                 | mov ebp,esp                                  |
   004047A3 | 83C4 F0              | add esp,FFFFFFF0                             |
   004047A6 | 53                   | push ebx                                     |
   004047A7 | B8 68474000          | mov eax,crackme#1.404768                     |
   004047AC | E8 87EDFFFF          | call crackme#1.403538                        |
   004047B1 | 6A 00                | push 0                                       |
   004047B3 | E8 3CEEFFFF          | call <JMP.&_GetModuleHandleAStub@4>          |
   004047B8 | A3 00674000          | mov dword ptr ds:[406700],eax                | 00406700:&"MZP"
   004047BD | 68 40000400          | push 40040                                   |
   004047C2 | 68 F8474000          | push crackme#1.4047F8                        | 4047F8:"Nitroito - Crackme#1"
   004047C7 | 68 10484000          | push crackme#1.404810                        | 404810:"Hello! welcome to my Crackme#1, as you can guess,\r\nthis is the NAG screen to remove... :]"
   004047CC | 6A 00                | push 0                                       |
   004047CE | E8 71EEFFFF          | call <JMP.&_MessageBoxA@16>                  |
   004047D3 | 48                   | dec eax                                      |
   004047D4 | 75 13                | jne crackme#1.4047E9                         |
   004047D6 | B9 0A000000          | mov ecx,A                                    | 0A:'\n'
   004047DB | 33D2                 | xor edx,edx                                  |
   004047DD | A1 00674000          | mov eax,dword ptr ds:[406700]                | 00406700:&"MZP"
   004047E2 | E8 BDFCFFFF          | call crackme#1.4044A4                        |
   004047E7 | 8BD8                 | mov ebx,eax                                  |
   004047E9 | 53                   | push ebx                                     |
   004047EA | E8 FDEDFFFF          | call <JMP.&ExitProcess>                      |
   ```

2. Patch

   ```assembly
   004047B8 | A3 00674000          | mov dword ptr ds:[406700],eax                | 00406700:&"MZP"
   004047BD | EB 17...             | jmp crackme#1.4047D6                         | <--
   ...
   004047D6 | B9 0A000000          | mov ecx,A                                    | 0A:'\n'
   ```

3. 寻找注册方法，搜索字符串"Name must"

   摆烂，太复杂了，只研究出前两组的计算方式

   所以我打算找点新方法

4. 直接改造成keygen

   检查keygen.exe与源程序的区别你应该可以看出我改了什么：

   ```assembly
   004040D2 | E9 9E070000      | jmp keygen3.404875                           |
   ```

   (写的不咋地，code请至少填一位)
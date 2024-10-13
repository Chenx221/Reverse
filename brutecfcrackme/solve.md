看起来要准备一个指定内容的密钥文件

先放解决方法：

crackme同路径下新建文件`knowledge.is.power`，内容`BF D8`

细节：

给`CreateFileA`下个断点，点击按钮就能找到检查的函数了

首先程序会读取文件的前5字节内容（实际上只用前2字节）

```assembly
004011E7 | 6A 00                | push 0                                     |
004011E9 | 68 80000000          | push 80                                    |
004011EE | 6A 03                | push 3                                     |
004011F0 | 6A 00                | push 0                                     |
004011F2 | 6A 00                | push 0                                     |
004011F4 | 68 00000080          | push 80000000                              |
004011F9 | 68 F3304000          | push brutecfcrackme.4030F3                 | 4030F3:"knowledge.is.power"
004011FE | E8 E9000000          | call <JMP.&CreateFileA>                    |
00401203 | 3D FFFF0000          | cmp eax,FFFF                               |
00401208 | A3 F8314000          | mov dword ptr ds:[4031F8],eax              |
0040120D | 74 53                | je brutecfcrackme.401262                   |
0040120F | 6A 00                | push 0                                     |
00401211 | 68 06324000          | push brutecfcrackme.403206                 | lpNumberOfBytesRead
00401216 | 6A 05                | push 5                                     | nNumberOfBytesToRead
00401218 | 68 FC314000          | push brutecfcrackme.4031FC                 | lpBuffer
0040121D | FF35 F8314000        | push dword ptr ds:[4031F8]                 | file handle
00401223 | E8 D6000000          | call <JMP.&ReadFile>                       |
```

往下一点点就能看到程序对读取内容进行处理，再往后就是最后的检查了，有一点垃圾干扰，下面已经整理好了

```assembly
00401228 | B9 10000000          | mov ecx,10                                 |
0040122D | 8D35 FC314000        | lea esi,dword ptr ds:[4031FC]              |
00401233 | 66:8B06              | mov ax,word ptr ds:[esi]                   |
00401236 | 33D2                 | xor edx,edx                                |
00401238 | EB 02                | jmp brutecfcrackme.40123C                  |
0040123A | E8                   | ascii ￨                                    | 垃圾
0040123B | 33                   | ascii 3                                    | 垃圾
0040123C | 66:D1D0              | rcl ax,1                                   | Loop 0x10
0040123F | 66:13D0              | adc dx,ax                                  |
00401242 | E2 F8                | loop brutecfcrackme.40123C                 |
00401244 | 66:81FA 7EA1         | cmp dx,A17E                                | 最后的比较
00401249 | 75 17                | jne brutecfcrackme.401262                  |
0040124B | EB 02                | jmp brutecfcrackme.40124F                  | 成功
0040124D | E8                   | ascii ￨                                    | 垃圾
0040124E | 33                   | ascii 3                                    | 垃圾
0040124F | 6A 00                | push 0                                     |
00401251 | 68 06314000          | push brutecfcrackme.403106                 | 403106:"KeyFile Present && Valid !!!"
00401256 | 68 23314000          | push brutecfcrackme.403123                 | 403123:"Please contact our company so that we can provide you with the program now that you cracked its protection...;)"
```

说白了就是跑0x10次(`rcl ax,1`和`adc dx,ax`)，最后和`0xA17E`对比

```
0040123C | 66:D1D0              | rcl ax,1                                   | 
0040123F | 66:13D0              | adc dx,ax                                  |
00401242 | E2 F8                | loop brutecfcrackme.40123C                 |
00401244 | 66:81FA 7EA1         | cmp dx,A17E                                | 
```


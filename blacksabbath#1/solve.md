打包工具: UPX[modified]

感觉又不像是upx

脱壳：

在这里设置断点

```
004129C3 | 68 C87D4000          | push blacksabbath#1.407DC8                  |
```

F7两下就到OEP了

剩下重复的工作就不提了

程序检查序列号文件，错误或不存在就会直接退出

先公布一下答案：

密钥文件文件名是根据系统目录的写入时间进行生成的，我也懒得看具体实现了，直接断点断在

```
00407EBA | 8D05 98A74000        | lea eax,dword ptr ds:[40A798]                  | 
```

这行，然后看实际eax值就行了



这里决定的密钥文件的内容为：`00 00 06 66 00 00 02 9A`

```
00407F95 | 813D B0A84000 00000 | cmp dword ptr ds:[40A8B0],66060000        |
00407F9F | 75 15               | jne blacksabbath#1_dump_.407FB6           |
00407FA1 | 833D 98924000 08    | cmp dword ptr ds:[409298],8               |
00407FA8 | 75 0C               | jne blacksabbath#1_dump_.407FB6           |
00407FAA | 813D B4A84000 00000 | cmp dword ptr ds:[40A8B4],9A020000        |
00407FB4 | 74 18               | je blacksabbath#1_dump_.407FCE            |
```

最后在程序同目录下建立对应密钥文件即可
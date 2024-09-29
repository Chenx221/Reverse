寻找小球正确的摆放位置

1. 分析程序大致可得到这样的程序结构

   ```
   [ebp-18]: index
   [ebp-1C]: 正确计数
   2734（Index Loop）
   检查圈[index]与框[index]的左边
   2826
   检查圈[index]与框[index]的上边
   2918
   检查圈[index]与框[index]的右边
   2b02
   检查圈[index]与框[index]的下边
   ```

   这已经足够找出各个小球和盒子的位置关系了，因为我比较懒，所以这里就不列出详细坐标了。想看正确答案的看底下↓

2. 这是一个笨方法

   首先转到成功部分

   ```assembly
   00402CF8 | 74 11            | je balls.402D0B                         |
   00402CFA | 66:8B4D E4       | mov cx,word ptr ss:[ebp-1C]             |
   00402CFE | 66:83C1 01       | add cx,1                                |
   00402D02 | 0F80 07010000    | jo balls.402E0F                         |
   00402D08 | 894D E4          | mov dword ptr ss:[ebp-1C],ecx           |
   00402D0B | B8 01000000      | mov eax,1                               |
   00402D10 | 66:0345 E8       | add ax,word ptr ss:[ebp-18]             |
   00402D14 | 0F80 F5000000    | jo balls.402E0F                         | error
   00402D1A | 8945 E8          | mov dword ptr ss:[ebp-18],eax           |
   00402D1D | 33DB             | xor ebx,ebx                             |
   00402D1F | E9 07FAFFFF      | jmp balls.40272B                        |
   00402D24 | B9 0A000000      | mov ecx,A                               | ecx:&"S领O", 0A:'\n'
   00402D29 | 66:394D E4       | cmp word ptr ss:[ebp-1C],cx             | 检查2
   00402D2D | 75 6C            | jne <balls.fail>                        |
   00402D2F | B8 04000280      | mov eax,80020004                        | success
   00402D34 | 894D 84          | mov dword ptr ss:[ebp-7C],ecx           |
   00402D37 | 894D 94          | mov dword ptr ss:[ebp-6C],ecx           |
   00402D3A | 894D A4          | mov dword ptr ss:[ebp-5C],ecx           | [ebp-5C]:_PeekMessageA@20
   00402D3D | 8D95 74FFFFFF    | lea edx,dword ptr ss:[ebp-8C]           |
   00402D43 | 8D4D B4          | lea ecx,dword ptr ss:[ebp-4C]           | [ebp-4C]:rtcGetCurrentCalendar+2F5
   00402D46 | 8945 8C          | mov dword ptr ss:[ebp-74],eax           |
   00402D49 | 8945 9C          | mov dword ptr ss:[ebp-64],eax           | [ebp-64]:rtcIsMissing+11A
   00402D4C | 8945 AC          | mov dword ptr ss:[ebp-54],eax           |
   00402D4F | C785 7CFFFFFF 4C | mov dword ptr ss:[ebp-84],balls.401B4C  | 401B4C:L"CONGRATULATIONS"
   00402D59 | C785 74FFFFFF 08 | mov dword ptr ss:[ebp-8C],8             |
   ```

   能看到成功之前是要检查[ebp-1C]是否等于10

   翻找了一下，只有这里涉及到[ebp-1C]

   ```assembly
   00402CF0 | F7DB             | neg ebx                                 |
   00402CF2 | 83C4 24          | add esp,24                              |
   00402CF5 | 66:85DB          | test bx,bx                              |
   00402CF8 | 74 11            | je balls.402D0B                         |
   00402CFA | 66:8B4D E4       | mov cx,word ptr ss:[ebp-1C]             |
   00402CFE | 66:83C1 01       | add cx,1                                |
   00402D02 | 0F80 07010000    | jo <balls.ErrOverflow>                  |
   00402D08 | 894D E4          | mov dword ptr ss:[ebp-1C],ecx           |
   00402D0B | B8 01000000      | mov eax,1                               |
   00402D10 | 66:0345 E8       | add ax,word ptr ss:[ebp-18]             |
   00402D14 | 0F80 F5000000    | jo <balls.ErrOverflow>                  | error
   00402D1A | 8945 E8          | mov dword ptr ss:[ebp-18],eax           |
   00402D1D | 33DB             | xor ebx,ebx                             |
   00402D1F | E9 07FAFFFF      | jmp balls.40272B                        |
   ```

   所以

   ```
   00402CFE | 66:83C1 01       | add cx,1                                |
   ```

   在这里设置条件断点,cx==1

   然后去程序窗口中随便找个球拖拽，只要对了就会触发断点

   触发后把条件+1，然后继续找

   最后你会得到正确答案

   ![2024-09-29_101703](img\2024-09-29_101703.png)

   （顺带一提，观察球和盒的index，可见一对一关系）
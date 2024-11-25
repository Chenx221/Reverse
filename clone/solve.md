算serial

先上一组可用密钥：

```
Name: chenx221
Serial: 6237A3FB
```

检查逻辑（c#实现）：

未给出的自定义函数见keygen源码

```c#
public static bool Check(string user, string serial)
{
    if (!(CheckStrLen(serial, 8, 8) && CheckStrLen(user, 5, 0)))
        return false;

    uint eax, ebx, ecx;
    ushort cx;
    byte cl, ch, dl = 0, bl;
    byte[] userBytes, serialBytes;

    foreach (char c in user[4..])
    {
        dl += (byte)c;
    }
    ecx = (uint)(dl << 24) | (uint)(dl << 16) | (uint)(dl << 8) | dl;
    userBytes = Encoding.ASCII.GetBytes(user[..4]);
    eax = BitConverter.ToUInt32(userBytes, 0);
    ecx ^= eax;
    ecx = BSwap(ecx);
    ecx += 0x3022006;
    ecx = BSwap(ecx);
    ecx -= 0xDEADC0DE;
    ecx = BSwap(ecx);
    cl = (byte)((ecx & 0xFF) + 1);
    ecx = (ecx & 0xFFFFFF00) | cl;
    ch = (byte)(((ecx >> 8) & 0xFF) + 1);
    ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
    ecx = BSwap(ecx);
    cl = (byte)((ecx & 0xFF) - 1);
    ecx = (ecx & 0xFFFFFF00) | cl;
    ch = (byte)(((ecx >> 8) & 0xFF) - 1);
    ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
    ecx = BSwap(ecx);
    ecx ^= 0xEDB88320;
    ecx = BSwap(ecx);
    ecx += 0xD76AA478;
    ecx = BSwap(ecx);
    ecx -= 0xB00BFACE;
    ecx = BSwap(ecx);
    ecx += 0xBADBEEF;
    ecx = BSwap(ecx);
    ecx++;
    ecx = BSwap(ecx);
    ecx--;
    ecx = BSwap(ecx);
    ecx += eax;
    ecx = BSwap(ecx);
    cx = (ushort)((ecx & 0xFFFF) + 1);
    ecx = (ecx & 0xFFFF0000) | cx;
    ecx = BSwap(ecx);
    cx = (ushort)((ecx & 0xFFFF) + 1);
    ecx = (ecx & 0xFFFF0000) | cx;
    ecx = BSwap(ecx); //true serial result

    serialBytes = ParseHstr(serial);
    ebx = (uint)(serialBytes[0] * 0x10 + serialBytes[1]);
    bl = (byte)(ebx & 0xFF);
    bl = (byte)((bl ^ 0x12) + 0x34);
    ebx = bl;
    eax = ebx;
    eax <<= 8;
    ebx = (uint)(serialBytes[2] * 0x10 + serialBytes[3]);
    bl = (byte)(ebx & 0xFF);
    bl = (byte)((bl ^ 0x56) + 0x78);
    ebx = bl;
    eax += ebx;
    eax <<= 8;
    ebx = (uint)(serialBytes[4] * 0x10 + serialBytes[5]);
    bl = (byte)(ebx & 0xFF);
    bl = (byte)((bl ^ 0x90) + 0xAB);
    ebx = bl;
    eax += ebx;
    eax <<= 8;
    ebx = (uint)(serialBytes[6] * 0x10 + serialBytes[7]);
    bl = (byte)(ebx & 0xFF);
    bl = (byte)((bl ^ 0xCD) + 0xEF);
    ebx = bl;
    eax += ebx;
    eax = BSwap(eax); //user input serial result
    return eax == ecx; //ecx<->ebx(crackme)
}
```

反向计算（c#实现）：

```c#
public static void CalcSerial(string user)
{
    ...
    ... //这部分与Check相同，故省略
    ...
    ecx = BSwap(ecx); //true serial result

    eax = BSwap(ecx);
    int[] key = [0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF];
    int keyIndex = key.Length - 1;
    byte[] byteArray = new byte[4];
    for (int i = 3; i >= 0; i--)
    {
        byte temp = (byte)(eax & 0xFF);
        temp = (byte)(temp - key[keyIndex--]);
        temp = (byte)(temp ^ key[keyIndex--]);
        byteArray[i] = temp;
        eax >>= 8;
    }
    Console.WriteLine($"Serial: {BitConverter.ToString(byteArray).Replace("-", "")}");
}
```

细节：

```assembly
004011DB | 6A 19            | push 19                       | event
004011DD | 68 9A304000      | push clone.40309A             | 40309A:"123456" (serial)
004011E2 | 6A 66            | push 66                       |
004011E4 | FF75 08          | push dword ptr ss:[ebp+8]     |
004011E7 | E8 F4010000      | call <JMP.&GetDlgItemTextA>   |
004011EC | 83F8 08          | cmp eax,8                     | serial length == 8
004011EF | 0F85 A9010000    | jne <clone.Fail>              |
004011F5 | 6A 1E            | push 1E                       |
004011F7 | 68 7C304000      | push clone.40307C             | 40307C:"chenx221" (user)
004011FC | 6A 65            | push 65                       |
004011FE | FF75 08          | push dword ptr ss:[ebp+8]     |
00401201 | E8 DA010000      | call <JMP.&GetDlgItemTextA>   |
00401206 | 83F8 05          | cmp eax,5                     | user length >=5
00401209 | 0F82 8F010000    | jb <clone.Fail>               |
0040120F | 8D05 7C304000    | lea eax,dword ptr ds:[40307C] | user
00401215 | 8D48 04          | lea ecx,dword ptr ds:[eax+4]  | user去前四位
00401218 | 33D2             | xor edx,edx                   |
0040121A | 0211             | add dl,byte ptr ds:[ecx]      | 累加user去前四位后每位asc(注意:dl)
0040121C | 41               | inc ecx                       |
0040121D | 8039 00          | cmp byte ptr ds:[ecx],0       |
00401220 | 75 F8            | jne clone.40121A              |
00401222 | 33C9             | xor ecx,ecx                   |
00401224 | 8ACA             | mov cl,dl                     | 上一步的结果(ex:ab)
00401226 | 8AEA             | mov ch,dl                     | 上一步的结果
00401228 | 0FC9             | bswap ecx                     | 1
0040122A | 8ACA             | mov cl,dl                     |
0040122C | 8AEA             | mov ch,dl                     | (ex: ecx=abababab)
0040122E | 8B00             | mov eax,dword ptr ds:[eax]    | user
00401230 | 33C8             | xor ecx,eax                   | abababab ^ user前四位(小端序
00401232 | 0FC9             | bswap ecx                     | 2
00401234 | 81C1 06200203    | add ecx,3022006               |
0040123A | 0FC9             | bswap ecx                     | 3
0040123C | 81E9 DEC0ADDE    | sub ecx,DEADC0DE              |
00401242 | 0FC9             | bswap ecx                     | 4
00401244 | FEC1             | inc cl                        |
00401246 | FEC5             | inc ch                        |
00401248 | 0FC9             | bswap ecx                     | 5
0040124A | FEC9             | dec cl                        |
0040124C | FECD             | dec ch                        |
0040124E | 0FC9             | bswap ecx                     | 6
00401250 | 81F1 2083B8ED    | xor ecx,EDB88320              |
00401256 | 0FC9             | bswap ecx                     | ...
00401258 | 81C1 78A46AD7    | add ecx,D76AA478              |
0040125E | 0FC9             | bswap ecx                     |
00401260 | 81E9 CEFA0BB0    | sub ecx,B00BFACE              |
00401266 | 0FC9             | bswap ecx                     |
00401268 | 81C1 EFBEAD0B    | add ecx,BADBEEF               |
0040126E | 0FC9             | bswap ecx                     |
00401270 | 41               | inc ecx                       |
00401271 | 0FC9             | bswap ecx                     |
00401273 | 49               | dec ecx                       |
00401274 | 0FC9             | bswap ecx                     |
00401276 | 03C8             | add ecx,eax                   | 还是user
00401278 | 0FC9             | bswap ecx                     |
0040127A | 66:41            | inc cx                        |
0040127C | 0FC9             | bswap ecx                     |
0040127E | 66:41            | inc cx                        |
00401280 | 0FC9             | bswap ecx                     |
00401282 | 890D C8304000    | mov dword ptr ds:[4030C8],ecx |
00401288 | 33C9             | xor ecx,ecx                   |
0040128A | 8D05 9A304000    | lea eax,dword ptr ds:[40309A] | serial
00401290 | 33DB             | xor ebx,ebx                   | 十六进制字符转实际数值
00401292 | 8A18             | mov bl,byte ptr ds:[eax]      |
00401294 | 80FB 00          | cmp bl,0                      |
00401297 | 74 3A            | je clone.4012D3               | serial读完了?
00401299 | 80FB 30          | cmp bl,30                     | 30:'0'
0040129C | 0F82 FC000000    | jb <clone.Fail>               |
004012A2 | 80FB 39          | cmp bl,39                     | 39:'9'
004012A5 | 77 0D            | ja clone.4012B4               |
004012A7 | 80EB 30          | sub bl,30                     | 0~9数字处理部分(str2int)
004012AA | 8899 B8304000    | mov byte ptr ds:[ecx+4030B8], |
004012B0 | 40               | inc eax                       |
004012B1 | 41               | inc ecx                       |
004012B2 | EB DC            | jmp clone.401290              |
004012B4 | 80FB 41          | cmp bl,41                     | 41:'A'
004012B7 | 0F82 E1000000    | jb <clone.Fail>               |
004012BD | 80FB 46          | cmp bl,46                     | 46:'F'
004012C0 | 0F87 D8000000    | ja <clone.Fail>               |
004012C6 | 80EB 37          | sub bl,37                     | 只接受A~F
004012C9 | 8899 B8304000    | mov byte ptr ds:[ecx+4030B8], | A~F处理部分(str2int)
004012CF | 40               | inc eax                       |
004012D0 | 41               | inc ecx                       |
004012D1 | EB BD            | jmp clone.401290              |
004012D3 | 33C0             | xor eax,eax                   |
004012D5 | 33DB             | xor ebx,ebx                   |
004012D7 | 33C9             | xor ecx,ecx                   | 0
004012D9 | 33D2             | xor edx,edx                   |
004012DB | 0FB699 B8304000  | movzx ebx,byte ptr ds:[ecx+40 | 读取第一位
004012E2 | C1E3 04          | shl ebx,4                     | <<4
004012E5 | 41               | inc ecx                       | 1
004012E6 | 0FB691 B8304000  | movzx edx,byte ptr ds:[ecx+40 |
004012ED | 03DA             | add ebx,edx                   | +=第二位
004012EF | 80F3 12          | xor bl,12                     |
004012F2 | 80C3 34          | add bl,34                     |
004012F5 | 81E3 FF000000    | and ebx,FF                    |
004012FB | 41               | inc ecx                       | 2
004012FC | 03C3             | add eax,ebx                   |
004012FE | C1E0 08          | shl eax,8                     |
00401301 | 0FB699 B8304000  | movzx ebx,byte ptr ds:[ecx+40 |
00401308 | C1E3 04          | shl ebx,4                     |
0040130B | 41               | inc ecx                       | 3
0040130C | 0FB691 B8304000  | movzx edx,byte ptr ds:[ecx+40 |
00401313 | 03DA             | add ebx,edx                   |
00401315 | 80F3 56          | xor bl,56                     |
00401318 | 80C3 78          | add bl,78                     |
0040131B | 81E3 FF000000    | and ebx,FF                    |
00401321 | 41               | inc ecx                       | 4
00401322 | 03C3             | add eax,ebx                   |
00401324 | C1E0 08          | shl eax,8                     |
00401327 | 0FB699 B8304000  | movzx ebx,byte ptr ds:[ecx+40 |
0040132E | C1E3 04          | shl ebx,4                     |
00401331 | 41               | inc ecx                       | 5
00401332 | 0FB691 B8304000  | movzx edx,byte ptr ds:[ecx+40 |
00401339 | 03DA             | add ebx,edx                   |
0040133B | 80F3 90          | xor bl,90                     |
0040133E | 80C3 AB          | add bl,AB                     |
00401341 | 81E3 FF000000    | and ebx,FF                    |
00401347 | 41               | inc ecx                       | 6
00401348 | 03C3             | add eax,ebx                   |
0040134A | C1E0 08          | shl eax,8                     |
0040134D | 0FB699 B8304000  | movzx ebx,byte ptr ds:[ecx+40 |
00401354 | C1E3 04          | shl ebx,4                     |
00401357 | 41               | inc ecx                       | 7
00401358 | 0FB691 B8304000  | movzx edx,byte ptr ds:[ecx+40 |
0040135F | 03DA             | add ebx,edx                   |
00401361 | 80F3 CD          | xor bl,CD                     |
00401364 | 80C3 EF          | add bl,EF                     |
00401367 | 81E3 FF000000    | and ebx,FF                    |
0040136D | 41               | inc ecx                       | 8
0040136E | 03C3             | add eax,ebx                   |
00401370 | 0FC8             | bswap eax                     |
00401372 | 8B1D C8304000    | mov ebx,dword ptr ds:[4030C8] |
00401378 | 3BD8             | cmp ebx,eax                   | eax是用户输入serial计算结果,ebx是正确的结果
0040137A | 75 22            | jne <clone.Fail>              |
0040137C | 6A 40            | push 40                       | Success
0040137E | 68 50304000      | push clone.403050             | 403050:"Bravo!"
00401383 | 68 19304000      | push clone.403019             | 403019:"Well done! Now make good tutorial :)"
00401388 | 6A 00            | push 0                        |
0040138A | E8 75000000      | call clone.401404             |
0040138F | 68 3E304000      | push clone.40303E             | 40303E:"clone - defeated!"
00401394 | FF75 08          | push dword ptr ss:[ebp+8]     |
00401397 | E8 86000000      | call <JMP.&SetWindowTextA>    |
0040139C | EB 00            | jmp <clone.Fail>              |
0040139E | EB 15            | jmp clone.4013B5              |
```
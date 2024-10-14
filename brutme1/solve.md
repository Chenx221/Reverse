这次还是寻找正确的serial

先放正确的serial: `54935769`

```cmd
D:\sources\keygen\FindKey\bin\x64\Release\net8.0>FindKey.exe
Found match: 5493-5769
```

首先定位到按钮事件：

```assembly
00405C30 | 55                   | push ebp                                  | Check
00405C31 | 8BEC                 | mov ebp,esp                               |
00405C33 | B9 09000000          | mov ecx,9                                 | 09:'\t'
00405C38 | 6A 00                | push 0                                    |
00405C3A | 6A 00                | push 0                                    |
00405C3C | 49                   | dec ecx                                   |
00405C3D | 75 F9                | jne brutme1.405C38                        |
00405C3F | 53                   | push ebx                                  |
00405C40 | 56                   | push esi                                  | esi:"U嬱兡鸶鬪@"
00405C41 | 57                   | push edi                                  | edi:"U嬱兡鸶鬪@"
00405C42 | 8BD8                 | mov ebx,eax                               |
00405C44 | 33C0                 | xor eax,eax                               |
00405C46 | 55                   | push ebp                                  |
00405C47 | 68 6A5E4000          | push brutme1.405E6A                       |
00405C4C | 64:FF30              | push dword ptr fs:[eax]                   |
00405C4F | 64:8920              | mov dword ptr fs:[eax],esp                |
00405C52 | 8D55 F0              | lea edx,dword ptr ss:[ebp-10]             |
00405C55 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             |
00405C58 | E8 77F1FFFF          | call <brutme1.GetSerial>                  |
00405C5D | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]             | [ebp-10]: Serial
00405C60 | E8 DBD4FFFF          | call brutme1.403140                       | GetLength
00405C65 | 83F8 08              | cmp eax,8                                 | 长度需要8位
00405C68 | 0F85 BC010000        | jne <brutme1.Fail>                        |
00405C6E | 8D55 E8              | lea edx,dword ptr ss:[ebp-18]             |
```

一上来先检查输入的Serial长度是否为8位

在这之后是将8位serial对半分开转成数值（意味着8位都要是0-9数字）

```assembly
00405C71 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405C74 | E8 5BF1FFFF          | call <brutme1.GetSerial>                  |
00405C79 | 8B45 E8              | mov eax,dword ptr ss:[ebp-18]             | [ebp-18]: Serial
00405C7C | 8A10                 | mov dl,byte ptr ds:[eax]                  | 取第一位
00405C7E | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]             | eax:_TppWorkerThread@4
00405C81 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405C84 | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405C87 | 8D55 EC              | lea edx,dword ptr ss:[ebp-14]             |
00405C8A | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]             | eax:_TppWorkerThread@4
00405C8D | E8 72C9FFFF          | call <brutme1.PStrCpy>                    |
00405C92 | 8D55 DC              | lea edx,dword ptr ss:[ebp-24]             |
00405C95 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405C98 | E8 37F1FFFF          | call <brutme1.GetSerial>                  |
00405C9D | 8B45 DC              | mov eax,dword ptr ss:[ebp-24]             | [ebp-24]: Serial
00405CA0 | 8A50 01              | mov dl,byte ptr ds:[eax+1]                | 取第二位
00405CA3 | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405CA6 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405CA9 | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405CAC | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             | 2
00405CAF | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]             | 1
00405CB2 | B1 02                | mov cl,2                                  |
00405CB4 | E8 1BC9FFFF          | call <brutme1.PStrNCat>                   | 拼接12
00405CB9 | 8D55 E4              | lea edx,dword ptr ss:[ebp-1C]             |
00405CBC | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]             | eax:_TppWorkerThread@4
00405CBF | E8 40C9FFFF          | call <brutme1.PStrCpy>                    |
00405CC4 | 8D55 D4              | lea edx,dword ptr ss:[ebp-2C]             | [ebp-2C]:"P羘"
00405CC7 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405CCA | E8 05F1FFFF          | call <brutme1.GetSerial>                  |
00405CCF | 8B45 D4              | mov eax,dword ptr ss:[ebp-2C]             | eax:_TppWorkerThread@4, [ebp-2C]:"P羘"
00405CD2 | 8A50 02              | mov dl,byte ptr ds:[eax+2]                | 3
00405CD5 | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405CD8 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405CDB | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405CDE | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             |
00405CE1 | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]             | eax:_TppWorkerThread@4
00405CE4 | B1 03                | mov cl,3                                  |
00405CE6 | E8 E9C8FFFF          | call <brutme1.PStrNCat>                   |
00405CEB | 8D55 D8              | lea edx,dword ptr ss:[ebp-28]             |
00405CEE | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]             | eax:_TppWorkerThread@4
00405CF1 | E8 0EC9FFFF          | call <brutme1.PStrCpy>                    |
00405CF6 | 8D55 C8              | lea edx,dword ptr ss:[ebp-38]             |
00405CF9 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405CFC | E8 D3F0FFFF          | call <brutme1.GetSerial>                  |
00405D01 | 8B45 C8              | mov eax,dword ptr ss:[ebp-38]             | eax:_TppWorkerThread@4
00405D04 | 8A50 03              | mov dl,byte ptr ds:[eax+3]                | eax+03:_TppWorkerThread@4+3
00405D07 | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405D0A | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405D0D | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405D10 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             |
00405D13 | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]             | eax:_TppWorkerThread@4
00405D16 | B1 04                | mov cl,4                                  |
00405D18 | E8 B7C8FFFF          | call <brutme1.PStrNCat>                   |
00405D1D | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]             | 前四位
00405D20 | 8D45 F8              | lea eax,dword ptr ss:[ebp-8]              | eax:_TppWorkerThread@4
00405D23 | E8 F4D3FFFF          | call brutme1.40311C                       | StrConvert
00405D28 | 8D55 C4              | lea edx,dword ptr ss:[ebp-3C]             |
00405D2B | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405D2E | E8 A1F0FFFF          | call <brutme1.GetSerial>                  |
00405D33 | 8B45 C4              | mov eax,dword ptr ss:[ebp-3C]             | eax:_TppWorkerThread@4
00405D36 | 8A50 04              | mov dl,byte ptr ds:[eax+4]                | eax+04:_TppWorkerThread@4+4
00405D39 | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]             | eax:_TppWorkerThread@4
00405D3C | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405D3F | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405D42 | 8D55 EC              | lea edx,dword ptr ss:[ebp-14]             |
00405D45 | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]             | eax:_TppWorkerThread@4
00405D48 | E8 B7C8FFFF          | call <brutme1.PStrCpy>                    | 5
00405D4D | 8D55 C0              | lea edx,dword ptr ss:[ebp-40]             |
00405D50 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405D53 | E8 7CF0FFFF          | call <brutme1.GetSerial>                  |
00405D58 | 8B45 C0              | mov eax,dword ptr ss:[ebp-40]             | eax:_TppWorkerThread@4
00405D5B | 8A50 05              | mov dl,byte ptr ds:[eax+5]                | eax+05:_TppWorkerThread@4+5
00405D5E | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405D61 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405D64 | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405D67 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             |
00405D6A | 8D45 E4              | lea eax,dword ptr ss:[ebp-1C]             | eax:_TppWorkerThread@4
00405D6D | B1 02                | mov cl,2                                  |
00405D6F | E8 60C8FFFF          | call <brutme1.PStrNCat>                   | 56
00405D74 | 8D55 E4              | lea edx,dword ptr ss:[ebp-1C]             |
00405D77 | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]             | eax:_TppWorkerThread@4
00405D7A | E8 85C8FFFF          | call <brutme1.PStrCpy>                    |
00405D7F | 8D55 BC              | lea edx,dword ptr ss:[ebp-44]             |
00405D82 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405D85 | E8 4AF0FFFF          | call <brutme1.GetSerial>                  |
00405D8A | 8B45 BC              | mov eax,dword ptr ss:[ebp-44]             | eax:_TppWorkerThread@4
00405D8D | 8A50 06              | mov dl,byte ptr ds:[eax+6]                | eax+06:_TppWorkerThread@4+6
00405D90 | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405D93 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405D96 | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405D99 | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             |
00405D9C | 8D45 D8              | lea eax,dword ptr ss:[ebp-28]             | eax:_TppWorkerThread@4
00405D9F | B1 03                | mov cl,3                                  |
00405DA1 | E8 2EC8FFFF          | call <brutme1.PStrNCat>                   | 567
00405DA6 | 8D55 D8              | lea edx,dword ptr ss:[ebp-28]             |
00405DA9 | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]             | eax:_TppWorkerThread@4
00405DAC | E8 53C8FFFF          | call <brutme1.PStrCpy>                    |
00405DB1 | 8D55 B8              | lea edx,dword ptr ss:[ebp-48]             |
00405DB4 | 8B43 24              | mov eax,dword ptr ds:[ebx+24]             | eax:_TppWorkerThread@4
00405DB7 | E8 18F0FFFF          | call <brutme1.GetSerial>                  |
00405DBC | 8B45 B8              | mov eax,dword ptr ss:[ebp-48]             | eax:_TppWorkerThread@4
00405DBF | 8A50 07              | mov dl,byte ptr ds:[eax+7]                | eax+07:_TppWorkerThread@4+7
00405DC2 | 8D45 E0              | lea eax,dword ptr ss:[ebp-20]             | eax:_TppWorkerThread@4
00405DC5 | 8850 01              | mov byte ptr ds:[eax+1],dl                | eax+01:_TppWorkerThread@4+1
00405DC8 | C600 01              | mov byte ptr ds:[eax],1                   | eax:_TppWorkerThread@4
00405DCB | 8D55 E0              | lea edx,dword ptr ss:[ebp-20]             |
00405DCE | 8D45 CC              | lea eax,dword ptr ss:[ebp-34]             | eax:_TppWorkerThread@4
00405DD1 | B1 04                | mov cl,4                                  |
00405DD3 | E8 FCC7FFFF          | call <brutme1.PStrNCat>                   | 5678
00405DD8 | 8D55 CC              | lea edx,dword ptr ss:[ebp-34]             |
00405DDB | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]              | eax:_TppWorkerThread@4, [ebp-0C]:__except_handler4
00405DDE | E8 39D3FFFF          | call brutme1.40311C                       | StrConvert
00405DE3 | 8D55 FC              | lea edx,dword ptr ss:[ebp-4]              |
00405DE6 | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]              | 1234
00405DE9 | E8 42C8FFFF          | call <brutme1.ValLong>                    |
00405DEE | 8BF0                 | mov esi,eax                               | eax:_TppWorkerThread@4
00405DF0 | 837D FC 00           | cmp dword ptr ss:[ebp-4],0                |
00405DF4 | 75 34                | jne <brutme1.Fail>                        |
00405DF6 | 8D55 FC              | lea edx,dword ptr ss:[ebp-4]              |
00405DF9 | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]              | 5678
00405DFC | E8 2FC8FFFF          | call <brutme1.ValLong>                    |
00405E01 | 8BF8                 | mov edi,eax                               | eax:_TppWorkerThread@4
00405E03 | 837D FC 00           | cmp dword ptr ss:[ebp-4],0                |
00405E07 | 75 21                | jne <brutme1.Fail>                        |
```

转换后将两个数值送进另一个函数中计算

```assembly
00405E09 | 8BC3                 | mov eax,ebx                               | ebx:"4Z@"
00405E0B | 8BCF                 | mov ecx,edi                               | 5678
00405E0D | 8BD6                 | mov edx,esi                               | 1234
00405E0F | E8 F0FDFFFF          | call <brutme1.CheckSerial>                |
```

在这里经过0xD00B次循环处理后将结果与设定值`0x6F3AAD5A`进行比较

```assembly
00405C04 | 53                   | push ebx                                  | ebx:"4Z@"
00405C05 | B8 0BD00000          | mov eax,D00B                              | 0xD00B
00405C0A | 33D1                 | xor edx,ecx                               | 1234 xor 5678
00405C0C | 8BD9                 | mov ebx,ecx                               |
00405C0E | C1E3 02              | shl ebx,2                                 |
00405C11 | 8BCA                 | mov ecx,edx                               |
00405C13 | C1E9 02              | shr ecx,2                                 |
00405C16 | 03D9                 | add ebx,ecx                               |
00405C18 | 8BCB                 | mov ecx,ebx                               |
00405C1A | 33CA                 | xor ecx,edx                               |
00405C1C | 48                   | dec eax                                   | eax:_TppWorkerThread@4
00405C1D | 75 EB                | jne brutme1.405C0A                        |
00405C1F | 81FB 5AAD3A6F        | cmp ebx,6F3AAD5A                          |
00405C25 | 75 04                | jne brutme1.405C2B                        |
00405C27 | B0 01                | mov al,1                                  |
00405C29 | 5B                   | pop ebx                                   |
00405C2A | C3                   | ret                                       |
00405C2B | 33C0                 | xor eax,eax                               | eax:_TppWorkerThread@4
00405C2D | 5B                   | pop ebx                                   |
00405C2E | C3                   | ret                                       |
```

怎么看都不像是能直接逆的，所以直接准备暴力破解：

(大概需要几分钟~十几分钟时间，喝杯水稍作等待)

```c#
static void Main()
{
    var parallelOptions = new ParallelOptions
    {
        MaxDegreeOfParallelism = 6 //根据设备自行调整
    };
    Parallel.For(0, 10000, parallelOptions, (i, state) =>
                 {
                     for (int j = 0; j <= 9999; j++)
                     {
                         if (CheckIfMatchesCondition(i, j))
                         {
                             Console.WriteLine($"Found match: {i:D4}-{j:D4}");
                             state.Stop();
                             break;
                         }
                     }
                 });
}
static bool CheckIfMatchesCondition(int p1, int p2)
{
    uint edx = (uint)p1;
    uint ecx = (uint)p2;
    uint ebx;
    int eax = 0xD00B;
    do
    {
        edx ^= ecx;
        ebx = (ecx << 2) + (edx >> 2);
        ecx = ebx ^ edx;
    }
    while (--eax > 0);
    return ebx == 0x6F3AAD5A;

}
```


这次是寻找serial

先放答案：

```
yyyy*0x7D2 + (dd<<5-dd) + (mm<<2)*3 + 0xFF3C
```

```c#
DateTime now = DateTime.Now;
int year = now.Year;
int month = now.Month;
int day = now.Day;
Console.WriteLine($"Serial: {(year * 0x7D2) + ((day << 5) - day) + ((month << 2) * 3) + 0xFF3C}");
```

细节：

```assembly
0046726 | 55                   | push ebp                           |
0046726 | 8BEC                 | mov ebp,esp                        |
0046726 | 33C9                 | xor ecx,ecx                        |
0046727 | 51                   | push ecx                           |
0046727 | 51                   | push ecx                           |
0046727 | 51                   | push ecx                           |
0046727 | 51                   | push ecx                           |
0046727 | 51                   | push ecx                           |
0046727 | 51                   | push ecx                           |
0046727 | 53                   | push ebx                           |
0046727 | 56                   | push esi                           | esi:"U嬱兡鸶huF"
0046727 | 57                   | push edi                           | edi:"U嬱兡鸶huF"
0046727 | 8BD8                 | mov ebx,eax                        |
0046727 | 33C0                 | xor eax,eax                        |
0046727 | 55                   | push ebp                           |
0046727 | 68 E3734600          | push crackme_easy_no4.4673E3       |
0046728 | 64:FF30              | push dword ptr fs:[eax]            |
0046728 | 64:8920              | mov dword ptr fs:[eax],esp         |
0046728 | E8 1926FAFF          | call <crackme_easy_no4.Date>       |
0046728 | 83C4 F8              | add esp,FFFFFFF8                   |
0046729 | DD1C24               | fstp qword ptr ss:[esp]            |
0046729 | 9B                   | fwait                              |
0046729 | 8D55 F8              | lea edx,dword ptr ss:[ebp-8]       |
0046729 | B8 FC734600          | mov eax,crackme_easy_no4.4673FC    | 4673FC:"\"\"d"
0046729 | E8 E131FAFF          | call <crackme_easy_no4.Date2Str>   |
004672A | 8B45 F8              | mov eax,dword ptr ss:[ebp-8]       | Day
004672A | E8 D511FAFF          | call <crackme_easy_no4.Str2Int>    |
004672A | 8BF0                 | mov esi,eax                        | esi:"U嬱兡鸶huF"
004672A | E8 F625FAFF          | call <crackme_easy_no4.Date>       |
004672B | 83C4 F8              | add esp,FFFFFFF8                   |
004672B | DD1C24               | fstp qword ptr ss:[esp]            |
004672B | 9B                   | fwait                              |
004672B | 8D55 F4              | lea edx,dword ptr ss:[ebp-C]       |
004672B | B8 08744600          | mov eax,crackme_easy_no4.467408    | 467408:"\"\"m"
004672C | E8 BE31FAFF          | call <crackme_easy_no4.Date2Str>   |
004672C | 8B45 F4              | mov eax,dword ptr ss:[ebp-C]       | Month
004672C | E8 B211FAFF          | call <crackme_easy_no4.Str2Int>    |
004672C | 8BF8                 | mov edi,eax                        | edi:"U嬱兡鸶huF"
004672D | E8 D325FAFF          | call <crackme_easy_no4.Date>       |
004672D | 83C4 F8              | add esp,FFFFFFF8                   |
004672D | DD1C24               | fstp qword ptr ss:[esp]            |
004672D | 9B                   | fwait                              |
004672D | 8D55 F0              | lea edx,dword ptr ss:[ebp-10]      |
004672D | B8 14744600          | mov eax,crackme_easy_no4.467414    | 467414:"\"\"yyyy"
004672E | E8 9B31FAFF          | call <crackme_easy_no4.Date2Str>   |
004672E | 8B45 F0              | mov eax,dword ptr ss:[ebp-10]      | Year
004672E | E8 8F11FAFF          | call <crackme_easy_no4.Str2Int>    |
004672F | 69C0 D2070000        | imul eax,eax,7D2                   | yyyy*0x7D2
004672F | 8BD6                 | mov edx,esi                        | dd
004672F | C1E2 05              | shl edx,5                          | dd<<5
004672F | 2BD6                 | sub edx,esi                        | dd<<5-dd
004672F | 03C2                 | add eax,edx                        | yyyy*0x7D2 + (dd<<5-dd)
0046730 | 8BD7                 | mov edx,edi                        | mm
0046730 | C1E2 02              | shl edx,2                          | mm<<2
0046730 | 8D1452               | lea edx,dword ptr ds:[edx+edx*2]   | (mm<<2)*3
0046730 | 03C2                 | add eax,edx                        | yyyy*0x7D2 + (dd<<5-dd) + (mm<<2)*3
0046730 | 05 3CFF0000          | add eax,FF3C                       | yyyy*0x7D2 + (dd<<5-dd) + (mm<<2)*3 + 0xFF3C
0046730 | 8BF0                 | mov esi,eax                        | esi:"U嬱兡鸶huF"
0046731 | 8D55 EC              | lea edx,dword ptr ss:[ebp-14]      | [ebp-14]:L"馨D"
0046731 | 8B83 F0020000        | mov eax,dword ptr ds:[ebx+2F0]     |
0046731 | E8 D5BFFCFF          | call <crackme_easy_no4.GetText>    |
0046731 | 8B45 EC              | mov eax,dword ptr ss:[ebp-14]      | [ebp-14]:Input Serial
0046732 | 50                   | push eax                           |
0046732 | 8D55 E8              | lea edx,dword ptr ss:[ebp-18]      | [ebp-18]:"U嬱兡鸶huF"
0046732 | 8BC6                 | mov eax,esi                        | esi:"U嬱兡鸶huF"
0046732 | E8 EF10FAFF          | call <crackme_easy_no4.IntToStr>   |
0046732 | 8B55 E8              | mov edx,dword ptr ss:[ebp-18]      | [ebp-18]:"U嬱兡鸶huF"
0046733 | 58                   | pop eax                            |
0046733 | E8 2AD3F9FF          | call <crackme_easy_no4.LStrCmp1>   | 前面的计算结果转十进制字符串后与输入serial比较
0046733 | 75 41                | jne <crackme_easy_no4.ErrorSerial> |
0046733 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]       | Success
0046733 | BA 24744600          | mov edx,crackme_easy_no4.467424    |
0046734 | E8 B7CFF9FF          | call <crackme_easy_no4.LStrLAsg>   |
0046734 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]       | [ebp-04]:L"馨D"
0046734 | E8 BFFEFFFF          | call crackme_easy_no4.46720C       |
0046734 | 6A 00                | push 0                             |
0046734 | 68 38744600          | push crackme_easy_no4.467438       | 467438:"Fehler"
0046735 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]       | [ebp-04]:L"馨D"
0046735 | E8 B8D3F9FF          | call <crackme_easy_no4.LStrToPChar |
0046735 | 50                   | push eax                           |
0046735 | A1 30AC4600          | mov eax,dword ptr ds:[46AC30]      |
0046736 | E8 0126FDFF          | call <crackme_easy_no4.GetHandle>  |
0046736 | 50                   | push eax                           |
0046736 | E8 CFFAF9FF          | call <JMP.&_MessageBoxA@16>        |
0046736 | A1 30AC4600          | mov eax,dword ptr ds:[46AC30]      |
0046737 | E8 4581FEFF          | call <crackme_easy_no4.Close>      |
0046737 | EB 0D                | jmp crackme_easy_no4.467386        |
0046737 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]       | [ebp-04]:L"馨D"
0046737 | BA 48744600          | mov edx,crackme_easy_no4.467448    |
0046738 | E8 76CFF9FF          | call <crackme_easy_no4.LStrLAsg>   |
0046738 | 8D45 FC              | lea eax,dword ptr ss:[ebp-4]       | [ebp-04]:L"馨D"
0046738 | E8 7EFEFFFF          | call crackme_easy_no4.46720C       |
0046738 | 6A 00                | push 0                             |
0046739 | 68 38744600          | push crackme_easy_no4.467438       | 467438:"Fehler"
0046739 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]       | [ebp-04]:L"馨D"
0046739 | E8 77D3F9FF          | call <crackme_easy_no4.LStrToPChar |
0046739 | 50                   | push eax                           |
0046739 | A1 30AC4600          | mov eax,dword ptr ds:[46AC30]      |
004673A | E8 C025FDFF          | call <crackme_easy_no4.GetHandle>  |
004673A | 50                   | push eax                           |
004673A | E8 8EFAF9FF          | call <JMP.&_MessageBoxA@16>        |
004673A | A1 30AC4600          | mov eax,dword ptr ds:[46AC30]      |
004673B | E8 0481FEFF          | call <crackme_easy_no4.Close>      |
004673B | 33C0                 | xor eax,eax                        |
004673B | 5A                   | pop edx                            |
004673B | 59                   | pop ecx                            |
004673B | 59                   | pop ecx                            |
004673B | 64:8910              | mov dword ptr fs:[eax],edx         |
004673C | 68 EA734600          | push crackme_easy_no4.4673EA       |
004673C | 8D45 E8              | lea eax,dword ptr ss:[ebp-18]      | [ebp-18]:"U嬱兡鸶huF"
004673C | E8 97CEF9FF          | call <crackme_easy_no4.LStrClr>    |
004673C | 8D45 EC              | lea eax,dword ptr ss:[ebp-14]      | [ebp-14]:L"馨D"
004673D | E8 8FCEF9FF          | call <crackme_easy_no4.LStrClr>    |
004673D | 8D45 F0              | lea eax,dword ptr ss:[ebp-10]      |
004673D | BA 04000000          | mov edx,4                          |
004673D | E8 A6CEF9FF          | call crackme_easy_no4.404288       |
004673E | C3                   | ret                                |
004673E | E9 A4C8F9FF          | jmp crackme_easy_no4.403C8C        |
004673E | EB DB                | jmp crackme_easy_no4.4673C5        |
004673E | 5F                   | pop edi                            | edi:"U嬱兡鸶huF"
004673E | 5E                   | pop esi                            | esi:"U嬱兡鸶huF"
004673E | 5B                   | pop ebx                            |
004673E | 8BE5                 | mov esp,ebp                        |
004673E | 5D                   | pop ebp                            |
004673F | C3                   | ret                                |
```


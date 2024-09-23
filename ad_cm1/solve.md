easy难度

密码：`qWeRtZ`

```assembly
004010AB | 6A 07                | push 7                                       |
004010AD | 68 5C304000          | push ad_cm#1.40305C                          | 40305C:"123"
004010B2 | 68 B80B0000          | push BB8                                     |
004010B7 | FF75 08              | push dword ptr ss:[ebp+8]                    |
004010BA | E8 6F000000          | call <JMP.&_GetDlgItemTextA@16>              |
004010BF | B8 5C304000          | mov eax,ad_cm#1.40305C                       | eax:"123", 40305C:"123"
004010C4 | BB 1E304000          | mov ebx,ad_cm#1.40301E                       | ebx:"qWeRtZ", 40301E:"qWeRtZ"
004010C9 | B9 07000000          | mov ecx,7                                    |
004010CE | 8A13                 | mov dl,byte ptr ds:[ebx]                     | ebx:"qWeRtZ"
004010D0 | 3810                 | cmp byte ptr ds:[eax],dl                     | eax:"123"
004010D2 | 75 18                | jne ad_cm#1.4010EC                           | 判断
004010D4 | 40                   | inc eax                                      | eax:"123"
004010D5 | 43                   | inc ebx                                      | ebx:"qWeRtZ"
004010D6 | E2 F6                | loop ad_cm#1.4010CE                          |
004010D8 | 6A 40                | push 40                                      |
004010DA | 68 09304000          | push ad_cm#1.403009                          | 403009:"ArturDents CrackMe#1"
004010DF | 68 36304000          | push ad_cm#1.403036                          | 403036:"Yeah, you did it!"
004010E4 | FF75 08              | push dword ptr ss:[ebp+8]                    |
004010E7 | E8 48000000          | call <JMP.&_MessageBoxA@16>                  |
```


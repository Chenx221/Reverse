vb6(native)

Serial: `Serial must be at least one characters long!`

细节

```assembly
004035C0 | 55                   | push ebp                                    | SerialCheck
...
...
00403658 | 8B45 E8              | mov eax,dword ptr ss:[ebp-18]               |
0040365B | 68 DC214000          | push bjcm10a.4021DC                         | 4021DC:L"Serial must be at least one characters long!"
00403660 | 50                   | push eax                                    |
00403661 | FF15 44104000        | call dword ptr ds:[<__vbaStrCmp>]           |
00403667 | 8BF0                 | mov esi,eax                                 |
00403669 | 8D4D E8              | lea ecx,dword ptr ss:[ebp-18]               | ecx:__vbaVarTextCmpLt+DF0
0040366C | F7DE                 | neg esi                                     |
0040366E | 1BF6                 | sbb esi,esi                                 |
00403670 | 46                   | inc esi                                     |
00403671 | F7DE                 | neg esi                                     |
00403673 | FF15 98104000        | call dword ptr ds:[<__vbaFreeStr>]          |
00403679 | 8D4D E4              | lea ecx,dword ptr ss:[ebp-1C]               | ecx:__vbaVarTextCmpLt+DF0
0040367C | FF15 9C104000        | call dword ptr ds:[<__vbaFreeObj>]          |
00403682 | 8B1D 20104000        | mov ebx,dword ptr ds:[<__vbaBoolStr>]       |
00403688 | 68 4C224000          | push bjcm10a.40224C                         | 40224C:L"False"
0040368D | FFD3                 | call ebx                                    |
0040368F | 66:3BF0              | cmp si,ax                                   |
00403692 | 75 6E                | jne bjcm10a.403702                          |
...
...(FAIL)
00403702 | 68 A4224000          | push bjcm10a.4022A4                         | 4022A4:L"True"
00403707 | FFD3                 | call ebx                                    |
00403709 | 66:3BF0              | cmp si,ax                                   |
0040370C | 75 77                | jne bjcm10a.403785                          |
0040370E | 8B35 84104000        | mov esi,dword ptr ds:[<__vbaVarDup>]        | 00401084:"阛鶲B<頞鶛鵒h 鶲愗隣斀颫浇颫"
00403714 | B9 04000280          | mov ecx,80020004                            | ecx:__vbaVarTextCmpLt+DF0
00403719 | 894D AC              | mov dword ptr ss:[ebp-54],ecx               | ecx:__vbaVarTextCmpLt+DF0
0040371C | B8 0A000000          | mov eax,A                                   | 0A:'\n'
00403721 | 894D BC              | mov dword ptr ss:[ebp-44],ecx               | ecx:__vbaVarTextCmpLt+DF0
00403724 | BB 08000000          | mov ebx,8                                   |
00403729 | 8D55 84              | lea edx,dword ptr ss:[ebp-7C]               |
0040372C | 8D4D C4              | lea ecx,dword ptr ss:[ebp-3C]               | ecx:__vbaVarTextCmpLt+DF0, [ebp-3C]:_NtUserWaitMessage@0+C
0040372F | 8945 A4              | mov dword ptr ss:[ebp-5C],eax               | [ebp-5C]:_PeekMessageA@20
00403732 | 8945 B4              | mov dword ptr ss:[ebp-4C],eax               | [ebp-4C]:rtcGetCurrentCalendar+2F5
00403735 | C745 8C 00234000     | mov dword ptr ss:[ebp-74],bjcm10a.402300    | 402300:L"Correct serial!"
0040373C | 895D 84              | mov dword ptr ss:[ebp-7C],ebx               |
0040373F | FFD6                 | call esi                                    |
00403741 | 8D55 94              | lea edx,dword ptr ss:[ebp-6C]               |
00403744 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               | ecx:__vbaVarTextCmpLt+DF0
00403747 | C745 9C B4224000     | mov dword ptr ss:[ebp-64],bjcm10a.4022B4    | [ebp-64]:rtcIsMissing+11A, 4022B4:L"Good job, tell me how you do that!"
0040374E | 895D 94              | mov dword ptr ss:[ebp-6C],ebx               |
00403751 | FFD6                 | call esi                                    |
00403753 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               | [ebp-5C]:_PeekMessageA@20
00403756 | 8D4D B4              | lea ecx,dword ptr ss:[ebp-4C]               | ecx:__vbaVarTextCmpLt+DF0, [ebp-4C]:rtcGetCurrentCalendar+2F5
00403759 | 50                   | push eax                                    |
0040375A | 8D55 C4              | lea edx,dword ptr ss:[ebp-3C]               | [ebp-3C]:_NtUserWaitMessage@0+C
0040375D | 51                   | push ecx                                    | ecx:__vbaVarTextCmpLt+DF0
0040375E | 52                   | push edx                                    |
0040375F | 8D45 D4              | lea eax,dword ptr ss:[ebp-2C]               |
00403762 | 57                   | push edi                                    | edi:_PeekMessageA@20
00403763 | 50                   | push eax                                    |
00403764 | FF15 28104000        | call dword ptr ds:[<Ordinal#595>]           | Msgbox
```


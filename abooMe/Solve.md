| 00404670 | 55                     | push ebp                                  |                                                              |
| -------- | ---------------------- | ----------------------------------------- | ------------------------------------------------------------ |
| 00404671 | 8BEC                   | mov ebp,esp                               |                                                              |
| 00404673 | 81EC F4000000          | sub esp,F4                                |                                                              |
| 00404679 | 8B45 0C                | mov eax,dword ptr ss:\[ebp+C\]            |                                                              |
| 0040467C | 8985 10FFFFFF          | mov dword ptr ss:\[ebp-F0\],eax           |                                                              |
| 00404682 | 81BD 10FFFFFF 10010000 | cmp dword ptr ss:\[ebp-F0\],110           |                                                              |
| 0040468C | 77 2B                  | ja aboome2.4046B9                         |                                                              |
| 0040468E | 81BD 10FFFFFF 10010000 | cmp dword ptr ss:\[ebp-F0\],110           |                                                              |
| 00404698 | 74 40                  | je aboome2.4046DA                         |                                                              |
| 0040469A | 83BD 10FFFFFF 0F       | cmp dword ptr ss:\[ebp-F0\],F             |                                                              |
| 004046A1 | 0F84 A4000000          | je aboome2.40474B                         |                                                              |
| 004046A7 | 83BD 10FFFFFF 10       | cmp dword ptr ss:\[ebp-F0\],10            |                                                              |
| 004046AE | 0F84 B8020000          | je aboome2.40496C                         |                                                              |
| 004046B4 | E9 DC020000            | jmp \<aboome2.Useless\>                   |                                                              |
| 004046B9 | 81BD 10FFFFFF 11010000 | cmp dword ptr ss:\[ebp-F0\],111           |                                                              |
| 004046C3 | 0F84 89000000          | je aboome2.404752                         |                                                              |
| 004046C9 | 81BD 10FFFFFF 01020000 | cmp dword ptr ss:\[ebp-F0\],201           |                                                              |
| 004046D3 | 74 58                  | je aboome2.40472D                         |                                                              |
| 004046D5 | E9 BB020000            | jmp \<aboome2.Useless\>                   |                                                              |
| 004046DA | 833D 7CC04100 00       | cmp dword ptr ds:\[41C07C\],0             |                                                              |
| 004046E1 | 75 40                  | jne aboome2.404723                        |                                                              |
| 004046E3 | E8 59FFFFFF            | call \<aboome2.sub_404641\>               |                                                              |
| 004046E8 | 6A 00                  | push 0                                    |                                                              |
| 004046EA | 68 44AC0000            | push AC44                                 |                                                              |
| 004046EF | E8 ACCEFFFF            | call \<aboome2.sub_4015A0\>               |                                                              |
| 004046F4 | 83C4 08                | add esp,8                                 |                                                              |
| 004046F7 | 0FBEC8                 | movsx ecx,al                              |                                                              |
| 004046FA | 85C9                   | test ecx,ecx                              |                                                              |
| 004046FC | 75 05                  | jne aboome2.404703                        |                                                              |
| 004046FE | E9 92020000            | jmp \<aboome2.Useless\>                   |                                                              |
| 00404703 | 6A 00                  | push 0                                    |                                                              |
| 00404705 | 6A 00                  | push 0                                    |                                                              |
| 00404707 | E8 38C9FFFF            | call \<aboome2.sub_401044\>               |                                                              |
| 0040470C | 83C4 08                | add esp,8                                 |                                                              |
| 0040470F | A3 7CC04100            | mov dword ptr ds:\[41C07C\],eax           |                                                              |
| 00404714 | 8B15 7CC04100          | mov edx,dword ptr ds:\[41C07C\]           |                                                              |
| 0040471A | 52                     | push edx                                  |                                                              |
| 0040471B | E8 F1CAFFFF            | call \<aboome2.sub_401211\>               |                                                              |
| 00404720 | 83C4 04                | add esp,4                                 |                                                              |
| 00404723 | B8 01000000            | mov eax,1                                 |                                                              |
| 00404728 | E9 6A020000            | jmp aboome2.404997                        |                                                              |
| 0040472D | FF15 E8204100          | call dword ptr ds:\[\<ReleaseCapture\>\]  |                                                              |
| 00404733 | 6A 00                  | push 0                                    |                                                              |
| 00404735 | 6A 02                  | push 2                                    |                                                              |
| 00404737 | 68 A1000000            | push A1                                   |                                                              |
| 0040473C | 8B45 08                | mov eax,dword ptr ss:\[ebp+8\]            |                                                              |
| 0040473F | 50                     | push eax                                  |                                                              |
| 00404740 | FF15 E4204100          | call dword ptr ds:\[\<SendMessageA\>\]    |                                                              |
| 00404746 | E9 4A020000            | jmp \<aboome2.Useless\>                   |                                                              |
| 0040474B | 33C0                   | xor eax,eax                               |                                                              |
| 0040474D | E9 45020000            | jmp aboome2.404997                        |                                                              |
| 00404752 | 8B4D 10                | mov ecx,dword ptr ss:\[ebp+10\]           |                                                              |
| 00404755 | 81E1 FFFF0000          | and ecx,FFFF                              |                                                              |
| 0040475B | 898D 0CFFFFFF          | mov dword ptr ss:\[ebp-F4\],ecx           |                                                              |
| 00404761 | 8B95 0CFFFFFF          | mov edx,dword ptr ss:\[ebp-F4\]           |                                                              |
| 00404767 | 81EA EA030000          | sub edx,3EA                               |                                                              |
| 0040476D | 8995 0CFFFFFF          | mov dword ptr ss:\[ebp-F4\],edx           |                                                              |
| 00404773 | 83BD 0CFFFFFF 04       | cmp dword ptr ss:\[ebp-F4\],4             |                                                              |
| 0040477A | 0F87 EA010000          | ja \<aboome2.Jump2Useless\>               |                                                              |
| 00404780 | 8B85 0CFFFFFF          | mov eax,dword ptr ss:\[ebp-F4\]           |                                                              |
| 00404786 | FF2485 9D494000        | jmp dword ptr ds:\[eax\*4+40499D\]        |                                                              |
| 0040478D | C745 FC 00000000       | mov dword ptr ss:\[ebp-4\],0              |                                                              |
| 00404794 | C745 F8 00000000       | mov dword ptr ss:\[ebp-8\],0              |                                                              |
| 0040479B | 6A 0F                  | push F                                    |                                                              |
| 0040479D | 8D8D 18FFFFFF          | lea ecx,dword ptr ss:\[ebp-E8\]           |                                                              |
| 004047A3 | 51                     | push ecx                                  |                                                              |
| 004047A4 | 68 E8030000            | push 3E8                                  | Name Edit Control ID                                         |
| 004047A9 | 8B55 08                | mov edx,dword ptr ss:\[ebp+8\]            |                                                              |
| 004047AC | 52                     | push edx                                  |                                                              |
| 004047AD | FF15 E0204100          | call dword ptr ds:\[\<GetDlgItemTextA\>\] | get name to ecx                                              |
| 004047B3 | 8D85 18FFFFFF          | lea eax,dword ptr ss:\[ebp-E8\]           |                                                              |
| 004047B9 | 50                     | push eax                                  |                                                              |
| 004047BA | E8 110C0000            | call \<aboome2.Lib_strlen\>               | get name length                                              |
| 004047BF | 83C4 04                | add esp,4                                 |                                                              |
| 004047C2 | 8945 8C                | mov dword ptr ss:\[ebp-74\],eax           |                                                              |
| 004047C5 | 837D 8C 03             | cmp dword ptr ss:\[ebp-74\],3             | Name.length\<=3时验证不通过                                  |
| 004047C9 | 0F8E 10010000          | jle \<aboome2.Jump22Useless\>             |                                                              |
| 004047CF | C745 90 00000000       | mov dword ptr ss:\[ebp-70\],0             | sub_4047CF                                                   |
| 004047D6 | EB 09                  | jmp aboome2.4047E1                        | 开始整活                                                     |
| 004047D8 | 8B4D 90                | mov ecx,dword ptr ss:\[ebp-70\]           |                                                              |
| 004047DB | 83C1 01                | add ecx,1                                 |                                                              |
| 004047DE | 894D 90                | mov dword ptr ss:\[ebp-70\],ecx           |                                                              |
| 004047E1 | 8B55 90                | mov edx,dword ptr ss:\[ebp-70\]           |                                                              |
| 004047E4 | 3B55 8C                | cmp edx,dword ptr ss:\[ebp-74\]           | edx与length比较                                              |
| 004047E7 | 7D 36                  | jge aboome2.40481F                        | edx\>=length时跳下一个整活                                   |
| 004047E9 | 8B45 90                | mov eax,dword ptr ss:\[ebp-70\]           |                                                              |
| 004047EC | 0FBE8C05 18FFFFFF      | movsx ecx,byte ptr ss:\[ebp+eax-E8\]      |                                                              |
| 004047F4 | 8B55 90                | mov edx,dword ptr ss:\[ebp-70\]           |                                                              |
| 004047F7 | 33C0                   | xor eax,eax                               |                                                              |
| 004047F9 | 8A82 C1AC4100          | mov al,byte ptr ds:\[edx+41ACC1\]         |                                                              |
| 004047FF | 0345 FC                | add eax,dword ptr ss:\[ebp-4\]            | part1                                                        |
| 00404802 | 03C1                   | add eax,ecx                               |                                                              |
| 00404804 | 8945 FC                | mov dword ptr ss:\[ebp-4\],eax            |                                                              |
| 00404807 | 8B4D 90                | mov ecx,dword ptr ss:\[ebp-70\]           |                                                              |
| 0040480A | 0FBE940D 18FFFFFF      | movsx edx,byte ptr ss:\[ebp+ecx-E8\]      |                                                              |
| 00404812 | 6BD2 0A                | imul edx,edx,A                            | edx = edx\*A                                                 |
| 00404815 | 8B45 FC                | mov eax,dword ptr ss:\[ebp-4\]            |                                                              |
| 00404818 | 03C2                   | add eax,edx                               |                                                              |
| 0040481A | 8945 FC                | mov dword ptr ss:\[ebp-4\],eax            |                                                              |
| 0040481D | EB B9                  | jmp aboome2.4047D8                        |                                                              |
| 0040481F | C745 90 00000000       | mov dword ptr ss:\[ebp-70\],0             |                                                              |
| 00404826 | EB 09                  | jmp aboome2.404831                        |                                                              |
| 00404828 | 8B4D 90                | mov ecx,dword ptr ss:\[ebp-70\]           |                                                              |
| 0040482B | 83C1 01                | add ecx,1                                 |                                                              |
| 0040482E | 894D 90                | mov dword ptr ss:\[ebp-70\],ecx           |                                                              |
| 00404831 | 8B55 90                | mov edx,dword ptr ss:\[ebp-70\]           |                                                              |
| 00404834 | 3B55 8C                | cmp edx,dword ptr ss:\[ebp-74\]           |                                                              |
| 00404837 | 7D 3D                  | jge aboome2.404876                        |                                                              |
| 00404839 | 8B45 90                | mov eax,dword ptr ss:\[ebp-70\]           |                                                              |
| 0040483C | 33C9                   | xor ecx,ecx                               |                                                              |
| 0040483E | 8A88 C0AC4100          | mov cl,byte ptr ds:\[eax+41ACC0\]         |                                                              |
| 00404844 | 6BC9 0A                | imul ecx,ecx,A                            |                                                              |
| 00404847 | 8B55 F8                | mov edx,dword ptr ss:\[ebp-8\]            | part2                                                        |
| 0040484A | 03D1                   | add edx,ecx                               |                                                              |
| 0040484C | 8955 F8                | mov dword ptr ss:\[ebp-8\],edx            |                                                              |
| 0040484F | 0FBE85 1AFFFFFF        | movsx eax,byte ptr ss:\[ebp-E6\]          |                                                              |
| 00404856 | 8B4D 90                | mov ecx,dword ptr ss:\[ebp-70\]           |                                                              |
| 00404859 | 33D2                   | xor edx,edx                               |                                                              |
| 0040485B | 8A91 C0AC4100          | mov dl,byte ptr ds:\[ecx+41ACC0\]         |                                                              |
| 00404861 | 0355 F8                | add edx,dword ptr ss:\[ebp-8\]            |                                                              |
| 00404864 | 03D0                   | add edx,eax                               |                                                              |
| 00404866 | 8955 F8                | mov dword ptr ss:\[ebp-8\],edx            |                                                              |
| 00404869 | 8B45 F8                | mov eax,dword ptr ss:\[ebp-8\]            |                                                              |
| 0040486C | 05 37130300            | add eax,31337                             |                                                              |
| 00404871 | 8945 F8                | mov dword ptr ss:\[ebp-8\],eax            |                                                              |
| 00404874 | EB B2                  | jmp aboome2.404828                        |                                                              |
| 00404876 | 8B4D F8                | mov ecx,dword ptr ss:\[ebp-8\]            |                                                              |
| 00404879 | 51                     | push ecx                                  | p2                                                           |
| 0040487A | 8B55 F8                | mov edx,dword ptr ss:\[ebp-8\]            |                                                              |
| 0040487D | 52                     | push edx                                  | p2                                                           |
| 0040487E | 8B45 FC                | mov eax,dword ptr ss:\[ebp-4\]            |                                                              |
| 00404881 | 50                     | push eax                                  | p1                                                           |
| 00404882 | 68 D0AC4100            | push aboome2.41ACD0                       | 41ACD0:"%X-aboo-me-%X%i-SCA"                                 |
| 00404887 | 8D4D 94                | lea ecx,dword ptr ss:\[ebp-6C\]           |                                                              |
| 0040488A | 51                     | push ecx                                  | serial store here                                            |
| 0040488B | E8 E90A0000            | call \<aboome2.LibFun_sprintf\>           | sprintf                                                      |
| 00404890 | 83C4 14                | add esp,14                                |                                                              |
| 00404893 | 6A 64                  | push 64                                   |                                                              |
| 00404895 | 8D95 28FFFFFF          | lea edx,dword ptr ss:\[ebp-D8\]           |                                                              |
| 0040489B | 52                     | push edx                                  |                                                              |
| 0040489C | 68 ED030000            | push 3ED                                  |                                                              |
| 004048A1 | 8B45 08                | mov eax,dword ptr ss:\[ebp+8\]            |                                                              |
| 004048A4 | 50                     | push eax                                  |                                                              |
| 004048A5 | FF15 E0204100          | call dword ptr ds:\[\<GetDlgItemTextA\>\] |                                                              |
| 004048AB | 8D8D 28FFFFFF          | lea ecx,dword ptr ss:\[ebp-D8\]           | 用户输入的serial                                             |
| 004048B1 | 51                     | push ecx                                  |                                                              |
| 004048B2 | 8D55 94                | lea edx,dword ptr ss:\[ebp-6C\]           | 正确的serial                                                 |
| 004048B5 | 52                     | push edx                                  |                                                              |
| 004048B6 | FF15 0C204100          | call dword ptr ds:\[\<lstrcmpA\>\]        |                                                              |
| 004048BC | 8985 14FFFFFF          | mov dword ptr ss:\[ebp-EC\],eax           |                                                              |
| 004048C2 | 83BD 14FFFFFF 00       | cmp dword ptr ss:\[ebp-EC\],0             |                                                              |
| 004048C9 | 75 14                  | jne \<aboome2.Jump22Useless\>             | 主逻辑判断，Patch的话Patch这                                 |
| 004048CB | 68 E4AC4100            | push aboome2.41ACE4                       | 41ACE4:"Good Work! now make a keygen! "                      |
| 004048D0 | 68 ED030000            | push 3ED                                  |                                                              |
| 004048D5 | 8B45 08                | mov eax,dword ptr ss:\[ebp+8\]            |                                                              |
| 004048D8 | 50                     | push eax                                  |                                                              |
| 004048D9 | FF15 DC204100          | call dword ptr ds:\[\<SetDlgItemTextA\>\] |                                                              |
| 004048DF | E9 86000000            | jmp \<aboome2.Jump2Useless\>              | Jump22Useless                                                |
| 004048E4 | 6A 00                  | push 0                                    |                                                              |
| 004048E6 | 68 08AD4100            | push aboome2.41AD08                       | 41AD08:"Aboo Me!"                                            |
| 004048EB | 68 14AD4100            | push aboome2.41AD14                       | 41AD14:"Aboo Me - kiTo / SCA\nChiptune: svenzzon\nGFX: kiTo ;)\nGreetz: All members in SCA" |
| 004048F0 | 8B4D 08                | mov ecx,dword ptr ss:\[ebp+8\]            |                                                              |
| 004048F3 | 51                     | push ecx                                  |                                                              |
| 004048F4 | FF15 D8204100          | call dword ptr ds:\[\<MessageBoxA\>\]     |                                                              |
| 004048FA | EB 6E                  | jmp \<aboome2.Jump2Useless\>              |                                                              |
| 004048FC | 833D 7CC04100 00       | cmp dword ptr ds:\[41C07C\],0             |                                                              |
| 00404903 | 75 3C                  | jne \<aboome2.sub_404941\>                |                                                              |
| 00404905 | E8 37FDFFFF            | call \<aboome2.sub_404641\>               |                                                              |
| 0040490A | 6A 00                  | push 0                                    |                                                              |
| 0040490C | 68 44AC0000            | push AC44                                 |                                                              |
| 00404911 | E8 8ACCFFFF            | call \<aboome2.sub_4015A0\>               |                                                              |
| 00404916 | 83C4 08                | add esp,8                                 |                                                              |
| 00404919 | 0FBED0                 | movsx edx,al                              |                                                              |
| 0040491C | 85D2                   | test edx,edx                              |                                                              |
| 0040491E | 75 02                  | jne aboome2.404922                        |                                                              |
| 00404920 | EB 48                  | jmp \<aboome2.Jump2Useless\>              |                                                              |
| 00404922 | 6A 00                  | push 0                                    |                                                              |
| 00404924 | 6A 00                  | push 0                                    |                                                              |
| 00404926 | E8 19C7FFFF            | call \<aboome2.sub_401044\>               |                                                              |
| 0040492B | 83C4 08                | add esp,8                                 |                                                              |
| 0040492E | A3 7CC04100            | mov dword ptr ds:\[41C07C\],eax           |                                                              |
| 00404933 | A1 7CC04100            | mov eax,dword ptr ds:\[41C07C\]           |                                                              |
| 00404938 | 50                     | push eax                                  |                                                              |
| 00404939 | E8 D3C8FFFF            | call \<aboome2.sub_401211\>               |                                                              |
| 0040493E | 83C4 04                | add esp,4                                 |                                                              |
| 00404941 | EB 27                  | jmp \<aboome2.Jump2Useless\>              | sub_404941                                                   |
| 00404943 | 833D 7CC04100 00       | cmp dword ptr ds:\[41C07C\],0             |                                                              |
| 0040494A | 74 1E                  | je \<aboome2.Jump2Useless\>               |                                                              |
| 0040494C | 8B0D 7CC04100          | mov ecx,dword ptr ds:\[41C07C\]           | sub_40494C                                                   |
| 00404952 | 51                     | push ecx                                  |                                                              |
| 00404953 | E8 6FC7FFFF            | call \<aboome2.sub_4010C7\>               |                                                              |
| 00404958 | 83C4 04                | add esp,4                                 |                                                              |
| 0040495B | E8 D9CDFFFF            | call \<aboome2.sub_401739\>               |                                                              |
| 00404960 | C705 7CC04100 00000000 | mov dword ptr ds:\[41C07C\],0             |                                                              |
| 0040496A | EB 29                  | jmp \<aboome2.Useless\>                   | Jump2Useless                                                 |
| 0040496C | 833D 7CC04100 00       | cmp dword ptr ds:\[41C07C\],0             |                                                              |
| 00404973 | 74 14                  | je aboome2.404989                         |                                                              |
| 00404975 | 8B15 7CC04100          | mov edx,dword ptr ds:\[41C07C\]           |                                                              |
| 0040497B | 52                     | push edx                                  |                                                              |
| 0040497C | E8 46C7FFFF            | call \<aboome2.sub_4010C7\>               |                                                              |
| 00404981 | 83C4 04                | add esp,4                                 |                                                              |
| 00404984 | E8 B0CDFFFF            | call \<aboome2.sub_401739\>               |                                                              |
| 00404989 | 6A 00                  | push 0                                    |                                                              |
| 0040498B | 8B45 08                | mov eax,dword ptr ss:\[ebp+8\]            |                                                              |
| 0040498E | 50                     | push eax                                  |                                                              |
| 0040498F | FF15 D4204100          | call dword ptr ds:\[\<EndDialog\>\]       |                                                              |
| 00404995 | 33C0                   | xor eax,eax                               | Useless                                                      |
| 00404997 | 8BE5                   | mov esp,ebp                               |                                                              |
| 00404999 | 5D                     | pop ebp                                   |                                                              |
| 0040499A | C2 1000                | ret 10                                    |                                                              |
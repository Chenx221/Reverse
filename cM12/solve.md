计算serial，确实简单

先上一组可用SN

```
chenx221
1c2h2e-easy
```

Serial计算方法:

`Name[^1]+Name[0]+Name[^2]+Name[1]+Name[^3]+Name[2]+"-easy"`

Name长度至少3位，否则生成的serial无法通过"Serial长度需>6"的输入值检查

细节：

```assembly
00450A44 | 55               | push ebp                       | Check
00450A45 | 8BEC             | mov ebp,esp                    |
00450A47 | B9 08000000      | mov ecx,8                      |
00450A4C | 6A 00            | push 0                         |
00450A4E | 6A 00            | push 0                         |
00450A50 | 49               | dec ecx                        |
00450A51 | 75 F9            | jne 00401c.450A4C              |
00450A53 | 53               | push ebx                       |
00450A54 | 56               | push esi                       | esi:"U嬱兡鸶€\rE"
00450A55 | 8BD8             | mov ebx,eax                    |
00450A57 | 33C0             | xor eax,eax                    |
00450A59 | 55               | push ebp                       |
00450A5A | 68 C90C4500      | push 00401c.450CC9             |
00450A5F | 64:FF30          | push dword ptr fs:[eax]        |
00450A62 | 64:8920          | mov dword ptr fs:[eax],esp     |
00450A65 | 8D55 F8          | lea edx,dword ptr ss:[ebp-8]   |
00450A68 | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450A6E | E8 5DF0FDFF      | call <00401c.TControl::GetText |
00450A73 | 8B45 F8          | mov eax,dword ptr ss:[ebp-8]   |
00450A76 | E8 253AFBFF      | call 00401c.4044A0             |
00450A7B | 8BF0             | mov esi,eax                    | esi:"U嬱兡鸶€\rE"
00450A7D | 8D55 F4          | lea edx,dword ptr ss:[ebp-C]   |
00450A80 | 8B83 FC020000    | mov eax,dword ptr ds:[ebx+2FC] |
00450A86 | E8 45F0FDFF      | call <00401c.TControl::GetText |
00450A8B | 8B45 F4          | mov eax,dword ptr ss:[ebp-C]   |
00450A8E | E8 0D3AFBFF      | call 00401c.4044A0             |
00450A93 | 83F8 06          | cmp eax,6                      |
00450A96 | 0F8E A5010000    | jle 00401c.450C41              | Serial长度需>6 (Name实际需要至少3位
00450A9C | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450A9F | E8 3C37FBFF      | call <00401c.LStrClr>          |
00450AA4 | 8D55 EC          | lea edx,dword ptr ss:[ebp-14]  |
00450AA7 | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450AAD | E8 1EF0FDFF      | call <00401c.TControl::GetText |
00450AB2 | 8B45 EC          | mov eax,dword ptr ss:[ebp-14]  |
00450AB5 | 8A5430 FF        | mov dl,byte ptr ds:[eax+esi-1] | Name[^1]
00450AB9 | 8D45 F0          | lea eax,dword ptr ss:[ebp-10]  |
00450ABC | E8 0739FBFF      | call 00401c.4043C8             |
00450AC1 | 8B55 F0          | mov edx,dword ptr ss:[ebp-10]  |
00450AC4 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450AC7 | E8 DC39FBFF      | call <00401c.lib_LStrCat>      |
00450ACC | 8D55 E4          | lea edx,dword ptr ss:[ebp-1C]  | [ebp-1C]:"U嬱兡鸶€\rE"
00450ACF | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450AD5 | E8 F6EFFDFF      | call <00401c.TControl::GetText |
00450ADA | 8B45 E4          | mov eax,dword ptr ss:[ebp-1C]  | [ebp-1C]:"U嬱兡鸶€\rE"
00450ADD | 8A10             | mov dl,byte ptr ds:[eax]       |
00450ADF | 8D45 E8          | lea eax,dword ptr ss:[ebp-18]  | [ebp-18]:"U嬱兡鸶€\rE"
00450AE2 | E8 E138FBFF      | call 00401c.4043C8             |
00450AE7 | 8B55 E8          | mov edx,dword ptr ss:[ebp-18]  | Name[0]
00450AEA | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450AED | E8 B639FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]
00450AF2 | 8D55 DC          | lea edx,dword ptr ss:[ebp-24]  |
00450AF5 | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450AFB | E8 D0EFFDFF      | call <00401c.TControl::GetText |
00450B00 | 8B45 DC          | mov eax,dword ptr ss:[ebp-24]  |
00450B03 | 8A5430 FE        | mov dl,byte ptr ds:[eax+esi-2] | Name[^2]
00450B07 | 8D45 E0          | lea eax,dword ptr ss:[ebp-20]  |
00450B0A | E8 B938FBFF      | call 00401c.4043C8             |
00450B0F | 8B55 E0          | mov edx,dword ptr ss:[ebp-20]  |
00450B12 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450B15 | E8 8E39FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]+Name[^2]
00450B1A | 8D55 D4          | lea edx,dword ptr ss:[ebp-2C]  |
00450B1D | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450B23 | E8 A8EFFDFF      | call <00401c.TControl::GetText |
00450B28 | 8B45 D4          | mov eax,dword ptr ss:[ebp-2C]  |
00450B2B | 8A50 01          | mov dl,byte ptr ds:[eax+1]     |
00450B2E | 8D45 D8          | lea eax,dword ptr ss:[ebp-28]  |
00450B31 | E8 9238FBFF      | call 00401c.4043C8             |
00450B36 | 8B55 D8          | mov edx,dword ptr ss:[ebp-28]  |
00450B39 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450B3C | E8 6739FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]+Name[^2]+Name[1]
00450B41 | 8D55 CC          | lea edx,dword ptr ss:[ebp-34]  |
00450B44 | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450B4A | E8 81EFFDFF      | call <00401c.TControl::GetText |
00450B4F | 8B45 CC          | mov eax,dword ptr ss:[ebp-34]  |
00450B52 | 8A5430 FD        | mov dl,byte ptr ds:[eax+esi-3] |
00450B56 | 8D45 D0          | lea eax,dword ptr ss:[ebp-30]  | [ebp-30]:"脥I"
00450B59 | E8 6A38FBFF      | call 00401c.4043C8             |
00450B5E | 8B55 D0          | mov edx,dword ptr ss:[ebp-30]  | [ebp-30]:"脥I"
00450B61 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450B64 | E8 3F39FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]+Name[^2]+Name[1]+Name[^3]
00450B69 | 8D55 C4          | lea edx,dword ptr ss:[ebp-3C]  |
00450B6C | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450B72 | E8 59EFFDFF      | call <00401c.TControl::GetText |
00450B77 | 8B45 C4          | mov eax,dword ptr ss:[ebp-3C]  |
00450B7A | 8A50 02          | mov dl,byte ptr ds:[eax+2]     |
00450B7D | 8D45 C8          | lea eax,dword ptr ss:[ebp-38]  |
00450B80 | E8 4338FBFF      | call 00401c.4043C8             |
00450B85 | 8B55 C8          | mov edx,dword ptr ss:[ebp-38]  |
00450B88 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450B8B | E8 1839FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]+Name[^2]+Name[1]+Name[^3]+Name[2]
00450B90 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450B93 | BA E40C4500      | mov edx,00401c.450CE4          | 450CE4:"-easy"
00450B98 | E8 0B39FBFF      | call <00401c.lib_LStrCat>      | Name[^1]+Name[0]+Name[^2]+Name[1]+Name[^3]+Name[2]+"-easy"
00450B9D | 8D55 C0          | lea edx,dword ptr ss:[ebp-40]  | [ebp-40]:"U嬱兡鸶€\rE"
00450BA0 | 8B83 FC020000    | mov eax,dword ptr ds:[ebx+2FC] |
00450BA6 | E8 25EFFDFF      | call <00401c.TControl::GetText |
00450BAB | 8B55 C0          | mov edx,dword ptr ss:[ebp-40]  | User input serial
00450BAE | 8B45 FC          | mov eax,dword ptr ss:[ebp-4]   | true serial
00450BB1 | E8 363AFBFF      | call <00401c.lib_LStrCmp>      |
00450BB6 | 74 32            | je 00401c.450BEA               |
00450BB8 | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450BBD | 8B00             | mov eax,dword ptr ds:[eax]     |
00450BBF | E8 80D7FFFF      | call 00401c.44E344             |
00450BC4 | 6A 40            | push 40                        |
00450BC6 | B9 EC0C4500      | mov ecx,00401c.450CEC          | 450CEC:" Wrong Code!"
00450BCB | BA FC0C4500      | mov edx,00401c.450CFC          | 450CFC:"Try again"
00450BD0 | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450BD5 | 8B00             | mov eax,dword ptr ds:[eax]     |
00450BD7 | E8 4CE8FFFF      | call 00401c.44F428             |
00450BDC | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450BE1 | 8B00             | mov eax,dword ptr ds:[eax]     |
00450BE3 | E8 6CD7FFFF      | call 00401c.44E354             |
00450BE8 | EB 57            | jmp 00401c.450C41              |
00450BEA | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450BEF | 8B00             | mov eax,dword ptr ds:[eax]     |
00450BF1 | E8 4ED7FFFF      | call 00401c.44E344             |
00450BF6 | 6A 40            | push 40                        |
00450BF8 | B9 080D4500      | mov ecx,00401c.450D08          | 450D08:" Well Done!"
00450BFD | BA 140D4500      | mov edx,00401c.450D14          | 450D14:"Success!"
00450C02 | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450C07 | 8B00             | mov eax,dword ptr ds:[eax]     |
00450C09 | E8 1AE8FFFF      | call 00401c.44F428             |
00450C0E | A1 04204500      | mov eax,dword ptr ds:[452004]  |
00450C13 | 8B00             | mov eax,dword ptr ds:[eax]     |
00450C15 | E8 3AD7FFFF      | call 00401c.44E354             |
00450C1A | 33D2             | xor edx,edx                    |
00450C1C | 8B83 FC020000    | mov eax,dword ptr ds:[ebx+2FC] |
00450C22 | 8B08             | mov ecx,dword ptr ds:[eax]     |
00450C24 | FF51 64          | call dword ptr ds:[ecx+64]     |
00450C27 | 33D2             | xor edx,edx                    |
00450C29 | 8B83 F8020000    | mov eax,dword ptr ds:[ebx+2F8] |
00450C2F | 8B08             | mov ecx,dword ptr ds:[eax]     |
00450C31 | FF51 64          | call dword ptr ds:[ecx+64]     |
00450C34 | 33D2             | xor edx,edx                    |
00450C36 | 8B83 00030000    | mov eax,dword ptr ds:[ebx+300] |
00450C3C | 8B08             | mov ecx,dword ptr ds:[eax]     |
00450C3E | FF51 64          | call dword ptr ds:[ecx+64]     |
00450C41 | 33C0             | xor eax,eax                    |
00450C43 | 5A               | pop edx                        |
00450C44 | 59               | pop ecx                        |
00450C45 | 59               | pop ecx                        |
00450C46 | 64:8910          | mov dword ptr fs:[eax],edx     |
00450C49 | 68 D30C4500      | push 00401c.450CD3             |
00450C4E | 8D45 C0          | lea eax,dword ptr ss:[ebp-40]  | [ebp-40]:"U嬱兡鸶€\rE"
00450C51 | BA 02000000      | mov edx,2                      |
00450C56 | E8 A935FBFF      | call 00401c.404204             |
00450C5B | 8D45 C8          | lea eax,dword ptr ss:[ebp-38]  |
00450C5E | E8 7D35FBFF      | call <00401c.LStrClr>          |
00450C63 | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]  |
00450C66 | E8 7535FBFF      | call <00401c.LStrClr>          |
00450C6B | 8D45 D0          | lea eax,dword ptr ss:[ebp-30]  | [ebp-30]:"脥I"
00450C6E | E8 6D35FBFF      | call <00401c.LStrClr>          |
00450C73 | 8D45 D4          | lea eax,dword ptr ss:[ebp-2C]  |
00450C76 | E8 6535FBFF      | call <00401c.LStrClr>          |
00450C7B | 8D45 D8          | lea eax,dword ptr ss:[ebp-28]  |
00450C7E | E8 5D35FBFF      | call <00401c.LStrClr>          |
00450C83 | 8D45 DC          | lea eax,dword ptr ss:[ebp-24]  |
00450C86 | E8 5535FBFF      | call <00401c.LStrClr>          |
00450C8B | 8D45 E0          | lea eax,dword ptr ss:[ebp-20]  |
00450C8E | E8 4D35FBFF      | call <00401c.LStrClr>          |
00450C93 | 8D45 E4          | lea eax,dword ptr ss:[ebp-1C]  | [ebp-1C]:"U嬱兡鸶€\rE"
00450C96 | E8 4535FBFF      | call <00401c.LStrClr>          |
00450C9B | 8D45 E8          | lea eax,dword ptr ss:[ebp-18]  | [ebp-18]:"U嬱兡鸶€\rE"
00450C9E | E8 3D35FBFF      | call <00401c.LStrClr>          |
00450CA3 | 8D45 EC          | lea eax,dword ptr ss:[ebp-14]  |
00450CA6 | E8 3535FBFF      | call <00401c.LStrClr>          |
00450CAB | 8D45 F0          | lea eax,dword ptr ss:[ebp-10]  |
00450CAE | E8 2D35FBFF      | call <00401c.LStrClr>          |
00450CB3 | 8D45 F4          | lea eax,dword ptr ss:[ebp-C]   |
00450CB6 | BA 02000000      | mov edx,2                      |
00450CBB | E8 4435FBFF      | call 00401c.404204             |
00450CC0 | 8D45 FC          | lea eax,dword ptr ss:[ebp-4]   |
00450CC3 | E8 1835FBFF      | call <00401c.LStrClr>          |
00450CC8 | C3               | ret                            |
00450CC9 | E9 EE2EFBFF      | jmp 00401c.403BBC              |
00450CCE | E9 7BFFFFFF      | jmp 00401c.450C4E              |
00450CD3 | 5E               | pop esi                        | esi:"U嬱兡鸶€\rE"
00450CD4 | 5B               | pop ebx                        |
00450CD5 | 8BE5             | mov esp,ebp                    |
00450CD7 | 5D               | pop ebp                        |
00450CD8 | C3               | ret                            |
```


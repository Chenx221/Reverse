计算serial，这次的是c++

先上总体思路：

```c#
string name = Console.ReadLine(); 
//用户输入的Name
string guid = hwProfile.szHwProfileGuid.Replace("-", "").Replace("{", "").Replace("}", ""); 
//硬件GUID
string base64 = name + Convert.ToBase64String(Encoding.UTF8.GetBytes(guid)); 
//将GUID base64编码，在开头加上name
byte[] hash = System.Security.Cryptography.SHA1.HashData(Encoding.UTF8.GetBytes(base64));
//将上面的base64组合计算sha1
byte[] halfHash = new byte[10];
Array.Copy(hash, 10, halfHash, 0, 10);
string hexHash = BitConverter.ToString(halfHash).Replace("-", "");
//只要后半段sha1校验值 <-v1
int sum = 0;
foreach (char c in name)
    sum += c;
//计算Name各位的Ascii值之和 <-v2
Console.WriteLine($"Serial: {hexHash}{sum * (name.Length + 5)}");
//Serial为v1加上(v2*name长度+5)
```

细节：(该加的注释我都加好了，我就不细讲了)

```assembly
01001E5 | 55                   | push ebp                                   |
01001E5 | 8BEC                 | mov ebp,esp                                |
01001E5 | 6A FF                | push FFFFFFFF                              |
01001E5 | 68 BC540001          | push <c++_crackme1.sub_10054BC>            |
01001E5 | 64:A1 00000000       | mov eax,dword ptr fs:[0]                   |
01001E6 | 50                   | push eax                                   |
01001E6 | 81EC 24040000        | sub esp,424                                |
01001E6 | A1 28800001          | mov eax,dword ptr ds:[1008028]             | 01008028:"笔*扤5誱pd"
01001E6 | 33C5                 | xor eax,ebp                                |
01001E6 | 8945 F0              | mov dword ptr ss:[ebp-10],eax              |
01001E7 | 53                   | push ebx                                   |
01001E7 | 56                   | push esi                                   |
01001E7 | 57                   | push edi                                   |
01001E7 | 50                   | push eax                                   |
01001E7 | 8D45 F4              | lea eax,dword ptr ss:[ebp-C]               | [ebp-0C]:__except_handler4
01001E7 | 64:A3 00000000       | mov dword ptr fs:[0],eax                   |
01001E7 | 8BF1                 | mov esi,ecx                                |
01001E8 | 8D8D F4FBFFFF        | lea ecx,dword ptr ss:[ebp-40C]             |
01001E8 | 89B5 D8FBFFFF        | mov dword ptr ss:[ebp-428],esi             |
01001E8 | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
01001E9 | 33DB                 | xor ebx,ebx                                |
01001E9 | 8D8D ECFBFFFF        | lea ecx,dword ptr ss:[ebp-414]             |
01001E9 | 895D FC              | mov dword ptr ss:[ebp-4],ebx               |
01001E9 | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
01001EA | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             |
01001EA | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
01001EA | 8D8D D0FBFFFF        | lea ecx,dword ptr ss:[ebp-430]             |
01001EB | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
01001EB | 8D85 F4FBFFFF        | lea eax,dword ptr ss:[ebp-40C]             |
01001EC | 50                   | push eax                                   |
01001EC | C645 FC 03           | mov byte ptr ss:[ebp-4],3                  |
01001EC | 8B3D 34610001        | mov edi,dword ptr ds:[<Ordinal#4810>]      |
01001EC | 68 EB030000          | push 3EB                                   |
01001ED | 8BCE                 | mov ecx,esi                                |
01001ED | FFD7                 | call edi                                   |
01001ED | 8D8D F4FBFFFF        | lea ecx,dword ptr ss:[ebp-40C]             | [ebp-40C]: Name
01001ED | FF15 4C610001        | call dword ptr ds:[<Ordinal#5264>]         |
01001EE | 50                   | push eax                                   |
01001EE | 68 70680001          | push c++_crackme1.1006870                  |
01001EE | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
01001EE | FF15 88620001        | call dword ptr ds:[<Ordinal#293>]          |
01001EF | C645 FC 04           | mov byte ptr ss:[ebp-4],4                  |
01001EF | 8B8D E4FBFFFF        | mov ecx,dword ptr ss:[ebp-41C]             |
01001EF | 51                   | push ecx                                   |
01001EF | 8D8D F4FBFFFF        | lea ecx,dword ptr ss:[ebp-40C]             |
01001F0 | FF15 50610001        | call dword ptr ds:[<Ordinal#2614>]         |
01001F0 | 85C0                 | test eax,eax                               |
01001F0 | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
01001F1 | 0F9585 F3FBFFFF      | setne byte ptr ss:[ebp-40D]                |
01001F1 | C645 FC 03           | mov byte ptr ss:[ebp-4],3                  |
01001F1 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
01001F2 | 8BCE                 | mov ecx,esi                                |
01001F2 | 389D F3FBFFFF        | cmp byte ptr ss:[ebp-40D],bl               |
01001F2 | 0F84 D5050000        | je <c++_crackme1.Error1>                   |
01001F3 | 8D95 ECFBFFFF        | lea edx,dword ptr ss:[ebp-414]             | [ebp-414]: Serial
01001F3 | 52                   | push edx                                   |
01001F3 | 68 EC030000          | push 3EC                                   |
01001F3 | FFD7                 | call edi                                   |
01001F3 | 8D85 FCFBFFFF        | lea eax,dword ptr ss:[ebp-404]             |
01001F4 | 50                   | push eax                                   |
01001F4 | FF15 00600001        | call dword ptr ds:[<GetCurrentHwProfileW>] |
01001F4 | 85C0                 | test eax,eax                               |
01001F4 | 74 13                | je c++_crackme1.1001F63                    |
01001F5 | 8D8D 00FCFFFF        | lea ecx,dword ptr ss:[ebp-400]             |
01001F5 | 51                   | push ecx                                   |
01001F5 | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             | [ebp-418]:L"{21abf477-80...-806e6f6e6963}"
01001F5 | FF15 38610001        | call dword ptr ds:[<Ordinal#1312>]         |
01001F6 | 68 CC6A0001          | push c++_crackme1.1006ACC                  |
01001F6 | 68 74680001          | push c++_crackme1.1006874                  |
01001F6 | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             |
01001F7 | FF15 8C620001        | call dword ptr ds:[<Ordinal#11683>]        | 替换{为空
01001F7 | 68 CC6A0001          | push c++_crackme1.1006ACC                  |
01001F7 | 68 78680001          | push c++_crackme1.1006878                  |
01001F8 | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             |
01001F8 | FF15 8C620001        | call dword ptr ds:[<Ordinal#11683>]        | 替换}为空
01001F8 | 68 CC6A0001          | push c++_crackme1.1006ACC                  |
01001F9 | 68 7C680001          | push c++_crackme1.100687C                  |
01001F9 | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             |
01001F9 | FF15 8C620001        | call dword ptr ds:[<Ordinal#11683>]        | 替换-为空
01001FA | 8B85 E8FBFFFF        | mov eax,dword ptr ss:[ebp-418]             |
01001FA | 6A 03                | push 3                                     |
01001FA | 50                   | push eax                                   |
01001FA | 8D8D F0FCFFFF        | lea ecx,dword ptr ss:[ebp-310]             |
01001FB | 8D95 F4FCFFFF        | lea edx,dword ptr ss:[ebp-30C]             |
01001FB | 51                   | push ecx                                   |
01001FB | 8995 F0FCFFFF        | mov dword ptr ss:[ebp-310],edx             |
01001FC | E8 FA050000          | call <c++_crackme1.sub_10025C0>            |
01001FC | C645 FC 05           | mov byte ptr ss:[ebp-4],5                  |
01001FC | 8B85 F0FCFFFF        | mov eax,dword ptr ss:[ebp-310]             |
01001FD | 8BC8                 | mov ecx,eax                                |
01001FD | C785 40FFFFFF 0F0000 | mov dword ptr ss:[ebp-C0],F                |
01001FD | 899D 3CFFFFFF        | mov dword ptr ss:[ebp-C4],ebx              |
01001FE | 889D 2CFFFFFF        | mov byte ptr ss:[ebp-D4],bl                |
01001FE | 8D71 01              | lea esi,dword ptr ds:[ecx+1]               |
01001FE | EB 03                | jmp c++_crackme1.1001FF0                   |
01001FE | 8D49 00              | lea ecx,dword ptr ds:[ecx]                 |
01001FF | 8A11                 | mov dl,byte ptr ds:[ecx]                   |
01001FF | 41                   | inc ecx                                    |
01001FF | 3AD3                 | cmp dl,bl                                  |
01001FF | 75 F9                | jne c++_crackme1.1001FF0                   |
01001FF | 2BCE                 | sub ecx,esi                                |
01001FF | 8BF9                 | mov edi,ecx                                | 0x20
01001FF | 8DB5 2CFFFFFF        | lea esi,dword ptr ss:[ebp-D4]              |
0100200 | E8 8AF3FFFF          | call <c++_crackme1.sub_1001390>            |
0100200 | C645 FC 06           | mov byte ptr ss:[ebp-4],6                  |
0100200 | 83BD 40FFFFFF 10     | cmp dword ptr ss:[ebp-C0],10               |
0100201 | 8B85 2CFFFFFF        | mov eax,dword ptr ss:[ebp-D4]              |
0100201 | 73 02                | jae c++_crackme1.100201B                   |
0100201 | 8BC6                 | mov eax,esi                                |
0100201 | 8B95 3CFFFFFF        | mov edx,dword ptr ss:[ebp-C4]              |
0100202 | 52                   | push edx                                   |
0100202 | 50                   | push eax                                   |
0100202 | 8D85 D8FEFFFF        | lea eax,dword ptr ss:[ebp-128]             |
0100202 | 50                   | push eax                                   |
0100202 | E8 11F0FFFF          | call <c++_crackme1.EncodeBase64>           |
0100202 | 83C4 0C              | add esp,C                                  |
0100203 | C645 FC 07           | mov byte ptr ss:[ebp-4],7                  |
0100203 | 8B95 F4FBFFFF        | mov edx,dword ptr ss:[ebp-40C]             |
0100203 | 6A 03                | push 3                                     |
0100203 | 52                   | push edx                                   |
0100203 | 8D85 74FDFFFF        | lea eax,dword ptr ss:[ebp-28C]             |
0100204 | 8D8D 78FDFFFF        | lea ecx,dword ptr ss:[ebp-288]             |
0100204 | 50                   | push eax                                   |
0100204 | 898D 74FDFFFF        | mov dword ptr ss:[ebp-28C],ecx             | [ebp-28C]: Name
0100205 | E8 69050000          | call <c++_crackme1.sub_10025C0>            |
0100205 | C645 FC 08           | mov byte ptr ss:[ebp-4],8                  |
0100205 | 8B85 74FDFFFF        | mov eax,dword ptr ss:[ebp-28C]             |
0100206 | 8BC8                 | mov ecx,eax                                |
0100206 | C785 08FFFFFF 0F0000 | mov dword ptr ss:[ebp-F8],F                | [ebp-F8]:&"T傓"
0100206 | 899D 04FFFFFF        | mov dword ptr ss:[ebp-FC],ebx              |
0100207 | 889D F4FEFFFF        | mov byte ptr ss:[ebp-10C],bl               |
0100207 | 8D71 01              | lea esi,dword ptr ds:[ecx+1]               |
0100207 | 8D6424 00            | lea esp,dword ptr ss:[esp]                 | [esp]:_TppWorkerThread@4+347
0100208 | 8A11                 | mov dl,byte ptr ds:[ecx]                   |
0100208 | 41                   | inc ecx                                    |
0100208 | 3AD3                 | cmp dl,bl                                  |
0100208 | 75 F9                | jne c++_crackme1.1002080                   |
0100208 | 2BCE                 | sub ecx,esi                                |
0100208 | 8BF9                 | mov edi,ecx                                |
0100208 | 8DB5 F4FEFFFF        | lea esi,dword ptr ss:[ebp-10C]             |
0100209 | E8 FAF2FFFF          | call <c++_crackme1.sub_1001390>            |
0100209 | C645 FC 09           | mov byte ptr ss:[ebp-4],9                  | 09:'\t'
0100209 | 83BD ECFEFFFF 10     | cmp dword ptr ss:[ebp-114],10              |
010020A | 8B85 D8FEFFFF        | mov eax,dword ptr ss:[ebp-128]             |
010020A | 73 06                | jae c++_crackme1.10020AF                   |
010020A | 8D85 D8FEFFFF        | lea eax,dword ptr ss:[ebp-128]             |
010020A | 50                   | push eax                                   |
010020B | 8D8D D0FBFFFF        | lea ecx,dword ptr ss:[ebp-430]             |
010020B | FF15 3C610001        | call dword ptr ds:[<Ordinal#1313>]         |
010020B | 8D8D D8FEFFFF        | lea ecx,dword ptr ss:[ebp-128]             |
010020C | 51                   | push ecx                                   | ecx: BASE64后的GUID
010020C | 8D95 F4FEFFFF        | lea edx,dword ptr ss:[ebp-10C]             |
010020C | 52                   | push edx                                   | edx: Name
010020C | 8D85 BCFEFFFF        | lea eax,dword ptr ss:[ebp-144]             | [ebp-144]:&"T傓"
010020D | 50                   | push eax                                   |
010020D | E8 9A060000          | call <c++_crackme1.sub_1002770>            | 拼接Name与GUID
010020D | 83C4 0C              | add esp,C                                  |
010020D | 8B8D D0FEFFFF        | mov ecx,dword ptr ss:[ebp-130]             |
010020D | 8B85 BCFEFFFF        | mov eax,dword ptr ss:[ebp-144]             | [ebp-144]:&"T傓"
010020E | 8BD0                 | mov edx,eax                                |
010020E | 83F9 10              | cmp ecx,10                                 |
010020E | 73 08                | jae c++_crackme1.10020F4                   |
010020E | 8D95 BCFEFFFF        | lea edx,dword ptr ss:[ebp-144]             | [ebp-144]:&"T傓"
010020F | 8BC2                 | mov eax,edx                                |
010020F | 8D8D 78FEFFFF        | lea ecx,dword ptr ss:[ebp-188]             |
010020F | 898D B8FEFFFF        | mov dword ptr ss:[ebp-148],ecx             |
0100210 | C785 F8FDFFFF 012345 | mov dword ptr ss:[ebp-208],67452301        |
0100210 | C785 FCFDFFFF 89ABCD | mov dword ptr ss:[ebp-204],EFCDAB89        |
0100211 | C785 00FEFFFF FEDCBA | mov dword ptr ss:[ebp-200],98BADCFE        | [ebp-200]:"锰烫烫虄9"
0100211 | C785 04FEFFFF 765432 | mov dword ptr ss:[ebp-1FC],10325476        | [ebp-1FC]:class wil::shutdown_aware_object<class wil::details::FeatureStateManager> wil::details::g_featureStateManager+14
0100212 | C785 08FEFFFF F0E1D2 | mov dword ptr ss:[ebp-1F8],C3D2E1F0        | [ebp-1F8]:public: void __thiscall wil::details::FeatureStateManager::OnSRUMTimer(void)+40
0100213 | 899D 0CFEFFFF        | mov dword ptr ss:[ebp-1F4],ebx             |
0100213 | 899D 10FEFFFF        | mov dword ptr ss:[ebp-1F0],ebx             |
0100213 | 8D70 01              | lea esi,dword ptr ds:[eax+1]               |
0100214 | 8A08                 | mov cl,byte ptr ds:[eax]                   |
0100214 | 40                   | inc eax                                    |
0100214 | 3ACB                 | cmp cl,bl                                  |
0100214 | 75 F9                | jne c++_crackme1.1002141                   |
0100214 | 2BC6                 | sub eax,esi                                |
0100214 | 52                   | push edx                                   |
0100214 | 8DB5 F8FDFFFF        | lea esi,dword ptr ss:[ebp-208]             |
0100215 | E8 AA1C0000          | call <c++_crackme1.sub_1003E00>            |
0100215 | 8BCE                 | mov ecx,esi                                |
0100215 | E8 631D0000          | call <c++_crackme1.sub_1003EC0>            |
0100215 | 33D2                 | xor edx,edx                                |
0100215 | C785 24FFFFFF 070000 | mov dword ptr ss:[ebp-DC],7                |
0100216 | 899D 20FFFFFF        | mov dword ptr ss:[ebp-E0],ebx              |
0100216 | 66:8995 10FFFFFF     | mov word ptr ss:[ebp-F0],dx                |
0100217 | 8D95 48FFFFFF        | lea edx,dword ptr ss:[ebp-B8]              |
0100217 | 8BCE                 | mov ecx,esi                                |
0100217 | C645 FC 0C           | mov byte ptr ss:[ebp-4],C                  | 0C:'\f'
0100218 | E8 491E0000          | call <c++_crackme1.sub_1003FD0>            |
0100218 | 3AC3                 | cmp al,bl                                  |
0100218 | 74 2A                | je c++_crackme1.10021B5                    |
0100218 | 8D85 48FFFFFF        | lea eax,dword ptr ss:[ebp-B8]              |
0100219 | 8D50 02              | lea edx,dword ptr ds:[eax+2]               | SHA1(前面组合字符串)
0100219 | 66:8B08              | mov cx,word ptr ds:[eax]                   |
0100219 | 83C0 02              | add eax,2                                  |
0100219 | 66:3BCB              | cmp cx,bx                                  |
0100219 | 75 F5                | jne c++_crackme1.1002194                   |
0100219 | 2BC2                 | sub eax,edx                                |
010021A | D1F8                 | sar eax,1                                  |
010021A | 50                   | push eax                                   |
010021A | 8D85 48FFFFFF        | lea eax,dword ptr ss:[ebp-B8]              |
010021A | 8D8D 10FFFFFF        | lea ecx,dword ptr ss:[ebp-F0]              |
010021B | E8 BB1F0000          | call <c++_crackme1.sub_1004170>            |
010021B | 83BD 24FFFFFF 08     | cmp dword ptr ss:[ebp-DC],8                |
010021B | 8B85 10FFFFFF        | mov eax,dword ptr ss:[ebp-F0]              |
010021C | 73 06                | jae c++_crackme1.10021CA                   |
010021C | 8D85 10FFFFFF        | lea eax,dword ptr ss:[ebp-F0]              |
010021C | 50                   | push eax                                   |
010021C | 8D8D DCFBFFFF        | lea ecx,dword ptr ss:[ebp-424]             |
010021D | FF15 40610001        | call dword ptr ds:[<Ordinal#286>]          |
010021D | C645 FC 0D           | mov byte ptr ss:[ebp-4],D                  | 0D:'\r'
010021D | 8B85 DCFBFFFF        | mov eax,dword ptr ss:[ebp-424]             |
010021E | 8B40 F4              | mov eax,dword ptr ds:[eax-C]               |
010021E | 50                   | push eax                                   |
010021E | 99                   | cdq                                        |
010021E | 2BC2                 | sub eax,edx                                |
010021E | D1F8                 | sar eax,1                                  |
010021E | 50                   | push eax                                   |
010021E | 8D8D E0FBFFFF        | lea ecx,dword ptr ss:[ebp-420]             |
010021F | 51                   | push ecx                                   |
010021F | 8D8D DCFBFFFF        | lea ecx,dword ptr ss:[ebp-424]             |
010021F | FF15 48610001        | call dword ptr ds:[<Ordinal#7914>]         | 截取后半部分
010021F | 50                   | push eax                                   |
010021F | 8D8D DCFBFFFF        | lea ecx,dword ptr ss:[ebp-424]             |
0100220 | C645 FC 0E           | mov byte ptr ss:[ebp-4],E                  |
0100220 | FF15 44610001        | call dword ptr ds:[<Ordinal#1310>]         |
0100220 | 8D8D E0FBFFFF        | lea ecx,dword ptr ss:[ebp-420]             |
0100221 | C645 FC 0D           | mov byte ptr ss:[ebp-4],D                  | 0D:'\r'
0100221 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100221 | 8B95 F4FBFFFF        | mov edx,dword ptr ss:[ebp-40C]             |
0100222 | 8B7A F4              | mov edi,dword ptr ds:[edx-C]               |
0100222 | 33F6                 | xor esi,esi                                |
0100222 | 899D E0FBFFFF        | mov dword ptr ss:[ebp-420],ebx             |
0100223 | 3BFB                 | cmp edi,ebx                                |
0100223 | 7E 27                | jle c++_crackme1.100225B                   |
0100223 | EB 0A                | jmp c++_crackme1.1002240                   |
0100223 | 8DA424 00000000      | lea esp,dword ptr ss:[esp]                 | [esp]:_TppWorkerThread@4+347
0100223 | 8D49 00              | lea ecx,dword ptr ds:[ecx]                 |
0100224 | 56                   | push esi                                   | 累加Name各位ascii
0100224 | 8D8D F4FBFFFF        | lea ecx,dword ptr ss:[ebp-40C]             |
0100224 | FF15 90620001        | call dword ptr ds:[<Ordinal#1440>]         |
0100224 | 0FB7C0               | movzx eax,ax                               |
0100225 | 0185 E0FBFFFF        | add dword ptr ss:[ebp-420],eax             |
0100225 | 46                   | inc esi                                    |
0100225 | 3BF7                 | cmp esi,edi                                |
0100225 | 7C E5                | jl c++_crackme1.1002240                    |
0100225 | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
0100226 | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
0100226 | 8D8D D4FBFFFF        | lea ecx,dword ptr ss:[ebp-42C]             |
0100226 | FF15 78630001        | call dword ptr ds:[<Ordinal#296>]          |
0100227 | 6A 14                | push 14                                    |
0100227 | 53                   | push ebx                                   |
0100227 | 8D8D F8FBFFFF        | lea ecx,dword ptr ss:[ebp-408]             |
0100227 | 51                   | push ecx                                   |
0100227 | 8D8D ECFBFFFF        | lea ecx,dword ptr ss:[ebp-414]             | [ebp-414]: Serial
0100228 | C645 FC 10           | mov byte ptr ss:[ebp-4],10                 |
0100228 | FF15 48610001        | call dword ptr ds:[<Ordinal#7914>]         |
0100228 | 50                   | push eax                                   |
0100228 | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
0100229 | C645 FC 11           | mov byte ptr ss:[ebp-4],11                 |
0100229 | FF15 44610001        | call dword ptr ds:[<Ordinal#1310>]         |
0100229 | 8D8D F8FBFFFF        | lea ecx,dword ptr ss:[ebp-408]             |
010022A | C645 FC 10           | mov byte ptr ss:[ebp-4],10                 |
010022A | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
010022A | 68 CC6A0001          | push c++_crackme1.1006ACC                  |
010022B | 68 7C680001          | push c++_crackme1.100687C                  |
010022B | 8D8D ECFBFFFF        | lea ecx,dword ptr ss:[ebp-414]             |
010022B | FF15 8C620001        | call dword ptr ds:[<Ordinal#11683>]        | 替换serial中的-
010022C | 8B95 ECFBFFFF        | mov edx,dword ptr ss:[ebp-414]             |
010022C | 8B42 F4              | mov eax,dword ptr ds:[edx-C]               |
010022C | 83C0 EC              | add eax,FFFFFFEC                           |
010022D | 50                   | push eax                                   |
010022D | 6A 14                | push 14                                    |
010022D | 8D85 F8FBFFFF        | lea eax,dword ptr ss:[ebp-408]             |
010022D | 50                   | push eax                                   |
010022D | 8D8D ECFBFFFF        | lea ecx,dword ptr ss:[ebp-414]             |
010022E | FF15 48610001        | call dword ptr ds:[<Ordinal#7914>]         |
010022E | 50                   | push eax                                   |
010022E | 8D8D D4FBFFFF        | lea ecx,dword ptr ss:[ebp-42C]             |
010022E | C645 FC 12           | mov byte ptr ss:[ebp-4],12                 |
010022F | FF15 44610001        | call dword ptr ds:[<Ordinal#1310>]         |
010022F | 8D8D F8FBFFFF        | lea ecx,dword ptr ss:[ebp-408]             |
010022F | C645 FC 10           | mov byte ptr ss:[ebp-4],10                 |
0100230 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100230 | 8B8D D4FBFFFF        | mov ecx,dword ptr ss:[ebp-42C]             |
0100230 | 51                   | push ecx                                   |
0100230 | FF15 E8600001        | call dword ptr ds:[<_wtoi>]                | Serial额外部分会被提取
0100231 | 8B95 DCFBFFFF        | mov edx,dword ptr ss:[ebp-424]             |
0100231 | 8985 F8FBFFFF        | mov dword ptr ss:[ebp-408],eax             |
0100232 | DB85 F8FBFFFF        | fild dword ptr ss:[ebp-408]                |
0100232 | 83C4 04              | add esp,4                                  |
0100232 | 52                   | push edx                                   |
0100232 | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
0100233 | D99D F8FBFFFF        | fstp dword ptr ss:[ebp-408]                |
0100233 | FF15 50610001        | call dword ptr ds:[<Ordinal#2614>]         |
0100233 | 8B8D D8FBFFFF        | mov ecx,dword ptr ss:[ebp-428]             |
0100234 | 85C0                 | test eax,eax                               |
0100234 | 75 32                | jne <c++_crackme1.Error2>                  |
0100234 | D985 F8FBFFFF        | fld dword ptr ss:[ebp-408]                 | 某个数（serial额外部分）
0100234 | 83C7 05              | add edi,5                                  |
0100234 | DAB5 E0FBFFFF        | fidiv dword ptr ss:[ebp-420]               | / sum(asc(name))
0100235 | 89BD F8FBFFFF        | mov dword ptr ss:[ebp-408],edi             |
0100235 | DB85 F8FBFFFF        | fild dword ptr ss:[ebp-408]                | name.length+5
0100236 | DAE9                 | fucompp                                    |
0100236 | DFE0                 | fnstsw ax                                  |
0100236 | F6C4 44              | test ah,44                                 |
0100236 | 7A 0E                | jp <c++_crackme1.Error2>                   | 数需要满足: sum(asc(name))*(name.length+5)
0100236 | 6A 40                | push 40                                    | Success
0100236 | 68 80680001          | push c++_crackme1.1006880                  | 1006880:L"Good boy!"
0100237 | 68 98680001          | push c++_crackme1.1006898                  | 1006898:L"Correct!\nNow write your keygen. :P"
0100237 | EB 0C                | jmp c++_crackme1.1002384                   |
0100237 | 6A 30                | push 30                                    |
0100237 | 68 E0680001          | push c++_crackme1.10068E0                  | 10068E0:L"Bad boy"
0100237 | 68 F0680001          | push c++_crackme1.10068F0                  | 10068F0:L"Wrong serial!"
0100238 | FF15 54610001        | call dword ptr ds:[<Ordinal#7911>]         |
0100238 | 8D8D D4FBFFFF        | lea ecx,dword ptr ss:[ebp-42C]             |
0100239 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100239 | 8D8D E4FBFFFF        | lea ecx,dword ptr ss:[ebp-41C]             |
0100239 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
010023A | 8D8D DCFBFFFF        | lea ecx,dword ptr ss:[ebp-424]             |
010023A | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
010023A | 83BD 24FFFFFF 08     | cmp dword ptr ss:[ebp-DC],8                |
010023B | 72 10                | jb c++_crackme1.10023C7                    |
010023B | 8B85 10FFFFFF        | mov eax,dword ptr ss:[ebp-F0]              |
010023B | 50                   | push eax                                   |
010023B | FF15 DC630001        | call dword ptr ds:[<Ordinal#1300>]         |
010023C | 83C4 04              | add esp,4                                  |
010023C | 33C9                 | xor ecx,ecx                                |
010023C | BF 10000000          | mov edi,10                                 |
010023C | C785 24FFFFFF 070000 | mov dword ptr ss:[ebp-DC],7                |
010023D | 899D 20FFFFFF        | mov dword ptr ss:[ebp-E0],ebx              |
010023D | 66:898D 10FFFFFF     | mov word ptr ss:[ebp-F0],cx                |
010023E | C785 F8FDFFFF 012345 | mov dword ptr ss:[ebp-208],67452301        |
010023E | C785 FCFDFFFF 89ABCD | mov dword ptr ss:[ebp-204],EFCDAB89        |
010023F | C785 00FEFFFF FEDCBA | mov dword ptr ss:[ebp-200],98BADCFE        | [ebp-200]:"锰烫烫虄9"
0100240 | C785 04FEFFFF 765432 | mov dword ptr ss:[ebp-1FC],10325476        | [ebp-1FC]:class wil::shutdown_aware_object<class wil::details::FeatureStateManager> wil::details::g_featureStateManager+14
0100240 | C785 08FEFFFF F0E1D2 | mov dword ptr ss:[ebp-1F8],C3D2E1F0        | [ebp-1F8]:public: void __thiscall wil::details::FeatureStateManager::OnSRUMTimer(void)+40
0100241 | 899D 0CFEFFFF        | mov dword ptr ss:[ebp-1F4],ebx             |
0100241 | 899D 10FEFFFF        | mov dword ptr ss:[ebp-1F0],ebx             |
0100242 | 39BD D0FEFFFF        | cmp dword ptr ss:[ebp-130],edi             |
0100242 | 72 10                | jb c++_crackme1.100243B                    |
0100242 | 8B95 BCFEFFFF        | mov edx,dword ptr ss:[ebp-144]             | [ebp-144]:&"T傓"
0100243 | 52                   | push edx                                   |
0100243 | FF15 DC630001        | call dword ptr ds:[<Ordinal#1300>]         |
0100243 | 83C4 04              | add esp,4                                  |
0100243 | BE 0F000000          | mov esi,F                                  |
0100244 | 89B5 D0FEFFFF        | mov dword ptr ss:[ebp-130],esi             |
0100244 | 899D CCFEFFFF        | mov dword ptr ss:[ebp-134],ebx             |
0100244 | 889D BCFEFFFF        | mov byte ptr ss:[ebp-144],bl               |
0100245 | 39BD 08FFFFFF        | cmp dword ptr ss:[ebp-F8],edi              | [ebp-F8]:&"T傓"
0100245 | 72 10                | jb c++_crackme1.100246A                    |
0100245 | 8B85 F4FEFFFF        | mov eax,dword ptr ss:[ebp-10C]             |
0100246 | 50                   | push eax                                   |
0100246 | FF15 DC630001        | call dword ptr ds:[<Ordinal#1300>]         |
0100246 | 83C4 04              | add esp,4                                  |
0100246 | 8B85 74FDFFFF        | mov eax,dword ptr ss:[ebp-28C]             |
0100247 | 8D8D 78FDFFFF        | lea ecx,dword ptr ss:[ebp-288]             |
0100247 | 89B5 08FFFFFF        | mov dword ptr ss:[ebp-F8],esi              | [ebp-F8]:&"T傓"
0100247 | 899D 04FFFFFF        | mov dword ptr ss:[ebp-FC],ebx              |
0100248 | 889D F4FEFFFF        | mov byte ptr ss:[ebp-10C],bl               |
0100248 | 3BC1                 | cmp eax,ecx                                |
0100248 | 74 0A                | je c++_crackme1.1002496                    |
0100248 | 50                   | push eax                                   |
0100248 | FF15 EC600001        | call dword ptr ds:[<free>]                 |
0100249 | 83C4 04              | add esp,4                                  |
0100249 | 39BD ECFEFFFF        | cmp dword ptr ss:[ebp-114],edi             |
0100249 | 72 10                | jb c++_crackme1.10024AE                    |
0100249 | 8B95 D8FEFFFF        | mov edx,dword ptr ss:[ebp-128]             |
010024A | 52                   | push edx                                   |
010024A | FF15 DC630001        | call dword ptr ds:[<Ordinal#1300>]         |
010024A | 83C4 04              | add esp,4                                  |
010024A | 89B5 ECFEFFFF        | mov dword ptr ss:[ebp-114],esi             |
010024B | 899D E8FEFFFF        | mov dword ptr ss:[ebp-118],ebx             |
010024B | 889D D8FEFFFF        | mov byte ptr ss:[ebp-128],bl               |
010024C | 39BD 40FFFFFF        | cmp dword ptr ss:[ebp-C0],edi              |
010024C | 72 10                | jb c++_crackme1.10024D8                    |
010024C | 8B85 2CFFFFFF        | mov eax,dword ptr ss:[ebp-D4]              |
010024C | 50                   | push eax                                   |
010024C | FF15 DC630001        | call dword ptr ds:[<Ordinal#1300>]         |
010024D | 83C4 04              | add esp,4                                  |
010024D | 8B85 F0FCFFFF        | mov eax,dword ptr ss:[ebp-310]             |
010024D | 8D8D F4FCFFFF        | lea ecx,dword ptr ss:[ebp-30C]             |
010024E | 89B5 40FFFFFF        | mov dword ptr ss:[ebp-C0],esi              |
010024E | 899D 3CFFFFFF        | mov dword ptr ss:[ebp-C4],ebx              |
010024F | 889D 2CFFFFFF        | mov byte ptr ss:[ebp-D4],bl                |
010024F | 3BC1                 | cmp eax,ecx                                |
010024F | 74 1E                | je c++_crackme1.1002518                    |
010024F | 50                   | push eax                                   |
010024F | FF15 EC600001        | call dword ptr ds:[<free>]                 |
0100250 | 83C4 04              | add esp,4                                  |
0100250 | EB 12                | jmp c++_crackme1.1002518                   |
0100250 | 6A 30                | push 30                                    |
0100250 | 68 0C690001          | push c++_crackme1.100690C                  | 100690C:L"Error"
0100250 | 68 18690001          | push c++_crackme1.1006918                  | 1006918:L"Enter a name."
0100251 | FF15 54610001        | call dword ptr ds:[<Ordinal#7911>]         |
0100251 | 8D8D D0FBFFFF        | lea ecx,dword ptr ss:[ebp-430]             |
0100251 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100252 | 8D8D E8FBFFFF        | lea ecx,dword ptr ss:[ebp-418]             |
0100252 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100253 | 8D8D ECFBFFFF        | lea ecx,dword ptr ss:[ebp-414]             |
0100253 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100253 | 8D8D F4FBFFFF        | lea ecx,dword ptr ss:[ebp-40C]             |
0100254 | FF15 7C630001        | call dword ptr ds:[<Ordinal#902>]          |
0100254 | 8B4D F4              | mov ecx,dword ptr ss:[ebp-C]               | [ebp-0C]:__except_handler4
0100254 | 64:890D 00000000     | mov dword ptr fs:[0],ecx                   |
0100255 | 59                   | pop ecx                                    |
0100255 | 5F                   | pop edi                                    |
0100255 | 5E                   | pop esi                                    |
0100255 | 5B                   | pop ebx                                    |
0100255 | 8B4D F0              | mov ecx,dword ptr ss:[ebp-10]              |
0100255 | 33CD                 | xor ecx,ebp                                |
0100255 | E8 E4250000          | call c++_crackme1.1004B44                  |
0100256 | 8BE5                 | mov esp,ebp                                |
0100256 | 5D                   | pop ebp                                    |
0100256 | C3                   | ret                                        |
```


计算serial

因为serial是根据name、当前计算机名和用户名计算，这里就不提供示例name,serial了

相关算法：

```c#
public static void CalcSerial(string name, string computerName, string userName)
{
    int v8 = 0x1791117;
    int v18 = name.Length;
    int v4 = 0;
    int vc = 0;
    int esi;
    int eax = 0x20;
    string cuu = ReverseString(computerName + userName).ToUpper();
    if (v18 < 4)
        return;
    foreach (char c in name)
    {
        v4 += c + v8++;
    }
    esi = v18 * v4 + v8;
    foreach (char c in cuu)
    {
        vc += (c ^ eax++) * v18;
    }
    // long serial; 这块是原先的比较逻辑
    // serial ^=vc;
    // vc+=esi;
    // if(vc == serial)
    Console.WriteLine((vc + esi) ^ vc);
}
```

细节:

```assembly
0040112E | 55                | push ebp                              | Check
0040112F | 8BEC              | mov ebp,esp                           |
00401131 | 81EC 30010000     | sub esp,130                           |
00401137 | 8A15 14314000     | mov dl,byte ptr ds:[403114]           |
0040113D | 53                | push ebx                              |
0040113E | 56                | push esi                              |
0040113F | 57                | push edi                              |
00401140 | 33C0              | xor eax,eax                           |
00401142 | 8D7D D1           | lea edi,dword ptr ss:[ebp-2F]         |
00401145 | 8855 D0           | mov byte ptr ss:[ebp-30],dl           |
00401148 | 6A 07             | push 7                                |
0040114A | AB                | stosd                                 |
0040114B | AB                | stosd                                 |
0040114C | AB                | stosd                                 |
0040114D | AB                | stosd                                 |
0040114E | AB                | stosd                                 |
0040114F | 66:AB             | stosw                                 |
00401151 | AA                | stosb                                 |
00401152 | 5B                | pop ebx                               |
00401153 | 33C0              | xor eax,eax                           |
00401155 | 8BCB              | mov ecx,ebx                           |
00401157 | 8D7D B1           | lea edi,dword ptr ss:[ebp-4F]         |
0040115A | 8855 B0           | mov byte ptr ss:[ebp-50],dl           |
0040115D | 6A 0F             | push F                                |
0040115F | F3:AB             | rep stosd                             |
00401161 | 66:AB             | stosw                                 |
00401163 | AA                | stosb                                 |
00401164 | 59                | pop ecx                               |
00401165 | 33C0              | xor eax,eax                           |
00401167 | 8DBD D1FEFFFF     | lea edi,dword ptr ss:[ebp-12F]        |
0040116D | 8895 D0FEFFFF     | mov byte ptr ss:[ebp-130],dl          |
00401173 | F3:AB             | rep stosd                             |
00401175 | 66:AB             | stosw                                 |
00401177 | AA                | stosb                                 |
00401178 | 6A 0F             | push F                                |
0040117A | 33C0              | xor eax,eax                           |
0040117C | 59                | pop ecx                               |
0040117D | 8DBD 11FFFFFF     | lea edi,dword ptr ss:[ebp-EF]         |
00401183 | 8895 10FFFFFF     | mov byte ptr ss:[ebp-F0],dl           |
00401189 | 8855 90           | mov byte ptr ss:[ebp-70],dl           |
0040118C | F3:AB             | rep stosd                             |
0040118E | 66:AB             | stosw                                 |
00401190 | AA                | stosb                                 |
00401191 | 8BCB              | mov ecx,ebx                           |
00401193 | 33C0              | xor eax,eax                           |
00401195 | 8D7D 91           | lea edi,dword ptr ss:[ebp-6F]         |
00401198 | 8895 50FFFFFF     | mov byte ptr ss:[ebp-B0],dl           |
0040119E | F3:AB             | rep stosd                             |
004011A0 | 66:AB             | stosw                                 |
004011A2 | AA                | stosb                                 |
004011A3 | 8BCB              | mov ecx,ebx                           |
004011A5 | 33C0              | xor eax,eax                           |
004011A7 | 8DBD 51FFFFFF     | lea edi,dword ptr ss:[ebp-AF]         |
004011AD | 8895 70FFFFFF     | mov byte ptr ss:[ebp-90],dl           |
004011B3 | F3:AB             | rep stosd                             |
004011B5 | 66:AB             | stosw                                 |
004011B7 | AA                | stosb                                 |
004011B8 | 8BCB              | mov ecx,ebx                           |
004011BA | 33C0              | xor eax,eax                           |
004011BC | 8DBD 71FFFFFF     | lea edi,dword ptr ss:[ebp-8F]         |
004011C2 | 8365 FC 00        | and dword ptr ss:[ebp-4],0            |
004011C6 | F3:AB             | rep stosd                             |
004011C8 | 66:AB             | stosw                                 |
004011CA | 8365 F4 00        | and dword ptr ss:[ebp-C],0            | [ebp-0C]:__except_handler4
004011CE | 6A 18             | push 18                               |
004011D0 | AA                | stosb                                 |
004011D1 | 8D45 D0           | lea eax,dword ptr ss:[ebp-30]         |
004011D4 | BF E8030000       | mov edi,3E8                           |
004011D9 | 50                | push eax                              |
004011DA | 57                | push edi                              |
004011DB | FF75 08           | push dword ptr ss:[ebp+8]             |
004011DE | BE FF000000       | mov esi,FF                            |
004011E3 | C745 F8 17117901  | mov dword ptr ss:[ebp-8],1791117      | val
004011EA | 8975 EC           | mov dword ptr ss:[ebp-14],esi         |
004011ED | 8975 F0           | mov dword ptr ss:[ebp-10],esi         |
004011F0 | FF15 74204000     | call dword ptr ds:[<GetDlgItemTextA>] |
004011F6 | 8D45 D0           | lea eax,dword ptr ss:[ebp-30]         | Name
004011F9 | 50                | push eax                              |
004011FA | E8 83010000       | call <JMP.&_strlen>                   |
004011FF | 83F8 04           | cmp eax,4                             |
00401202 | 59                | pop ecx                               |
00401203 | 8945 E8           | mov dword ptr ss:[ebp-18],eax         | name.length
00401206 | 73 22             | jae keygenme2.cerebellum.xyzero.40122 | n.l >=4
00401208 | 8B35 78204000     | mov esi,dword ptr ds:[<SetDlgItemText | Fail
0040120E | 6A 00             | push 0                                |
00401210 | 68 E9030000       | push 3E9                              |
00401215 | FF75 08           | push dword ptr ss:[ebp+8]             |
00401218 | FFD6              | call esi                              |
0040121A | 68 14314000       | push keygenme2.cerebellum.xyzero.4031 |
0040121F | 57                | push edi                              |
00401220 | FF75 08           | push dword ptr ss:[ebp+8]             |
00401223 | FFD6              | call esi                              |
00401225 | E9 50010000       | jmp keygenme2.cerebellum.xyzero.40137 |
0040122A | 33C9              | xor ecx,ecx                           |
0040122C | 85C0              | test eax,eax                          |
0040122E | 76 13             | jbe keygenme2.cerebellum.xyzero.40124 |
00401230 | 0FBE540D D0       | movsx edx,byte ptr ss:[ebp+ecx-30]    |
00401235 | 0355 F8           | add edx,dword ptr ss:[ebp-8]          | ebp-8 初始值0x1791117
00401238 | 0155 FC           | add dword ptr ss:[ebp-4],edx          | 累加
0040123B | FF45 F8           | inc dword ptr ss:[ebp-8]              |
0040123E | 41                | inc ecx                               |
0040123F | 3BC8              | cmp ecx,eax                           |
00401241 | 72 ED             | jb keygenme2.cerebellum.xyzero.401230 |
00401243 | 8BF0              | mov esi,eax                           |
00401245 | 8D45 EC           | lea eax,dword ptr ss:[ebp-14]         |
00401248 | 0FAF75 FC         | imul esi,dword ptr ss:[ebp-4]         | n.l * 累加值
0040124C | 0375 F8           | add esi,dword ptr ss:[ebp-8]          | +=前面的0x17911*** (v1
0040124F | 50                | push eax                              |
00401250 | 8D85 50FFFFFF     | lea eax,dword ptr ss:[ebp-B0]         | pc name
00401256 | 50                | push eax                              |
00401257 | FF15 18204000     | call dword ptr ds:[<GetComputerNameA> |
0040125D | 8B1D 7C204000     | mov ebx,dword ptr ds:[<wsprintfA>]    |
00401263 | 8D85 50FFFFFF     | lea eax,dword ptr ss:[ebp-B0]         |
00401269 | 50                | push eax                              | eax:"CHENX221-VMDBG" (ex)
0040126A | BF 00314000       | mov edi,keygenme2.cerebellum.xyzero.4 | 403100:"%s"
0040126F | 8D85 D0FEFFFF     | lea eax,dword ptr ss:[ebp-130]        | pc name
00401275 | 57                | push edi                              |
00401276 | 50                | push eax                              |
00401277 | FFD3              | call ebx                              |
00401279 | 83C4 0C           | add esp,C                             |
0040127C | 8D45 F0           | lea eax,dword ptr ss:[ebp-10]         |
0040127F | 50                | push eax                              |
00401280 | 8D85 70FFFFFF     | lea eax,dword ptr ss:[ebp-90]         | username
00401286 | 50                | push eax                              |
00401287 | FF15 00204000     | call dword ptr ds:[<GetUserNameA>]    |
0040128D | 8D85 70FFFFFF     | lea eax,dword ptr ss:[ebp-90]         |
00401293 | 50                | push eax                              | eax:"x221" (ex)
00401294 | 8D85 10FFFFFF     | lea eax,dword ptr ss:[ebp-F0]         | ↑
0040129A | 57                | push edi                              |
0040129B | 50                | push eax                              |
0040129C | FFD3              | call ebx                              |
0040129E | 83C4 0C           | add esp,C                             |
004012A1 | 8D85 10FFFFFF     | lea eax,dword ptr ss:[ebp-F0]         |
004012A7 | 50                | push eax                              |
004012A8 | 8D85 D0FEFFFF     | lea eax,dword ptr ss:[ebp-130]        |
004012AE | 50                | push eax                              |
004012AF | FF15 10204000     | call dword ptr ds:[<lstrcatA>]        | 拼接 pcname+username
004012B5 | 8D85 D0FEFFFF     | lea eax,dword ptr ss:[ebp-130]        |
004012BB | 50                | push eax                              |
004012BC | E8 C1000000       | call <JMP.&_strlen>                   |
004012C1 | 8BF8              | mov edi,eax                           |
004012C3 | 33C0              | xor eax,eax                           |
004012C5 | 85FF              | test edi,edi                          |
004012C7 | 59                | pop ecx                               |
004012C8 | 76 16             | jbe keygenme2.cerebellum.xyzero.4012E |
004012CA | 8D8C3D CFFEFFFF   | lea ecx,dword ptr ss:[ebp+edi-131]    | 翻转合并结果
004012D1 | 8A11              | mov dl,byte ptr ds:[ecx]              |
004012D3 | 889405 10FFFFFF   | mov byte ptr ss:[ebp+eax-F0],dl       |
004012DA | 40                | inc eax                               |
004012DB | 49                | dec ecx                               |
004012DC | 3BC7              | cmp eax,edi                           |
004012DE | 72 F1             | jb keygenme2.cerebellum.xyzero.4012D1 |
004012E0 | 8D85 10FFFFFF     | lea eax,dword ptr ss:[ebp-F0]         |
004012E6 | 50                | push eax                              |
004012E7 | FF15 80204000     | call dword ptr ds:[<CharUpperA>]      | 转大写
004012ED | 85FF              | test edi,edi                          |
004012EF | 76 28             | jbe keygenme2.cerebellum.xyzero.40131 |
004012F1 | 6A 20             | push 20                               |
004012F3 | 8D8D 10FFFFFF     | lea ecx,dword ptr ss:[ebp-F0]         |
004012F9 | 58                | pop eax                               |
004012FA | 2BC8              | sub ecx,eax                           |
004012FC | 894D FC           | mov dword ptr ss:[ebp-4],ecx          |
004012FF | EB 03             | jmp keygenme2.cerebellum.xyzero.40130 |
00401301 | 8B4D FC           | mov ecx,dword ptr ss:[ebp-4]          |
00401304 | 0FBE0C01          | movsx ecx,byte ptr ds:[ecx+eax]       | 取出每位
00401308 | 33C8              | xor ecx,eax                           | 每位 xor eax(初始0x20)
0040130A | 0FAF4D E8         | imul ecx,dword ptr ss:[ebp-18]        | *=n.l
0040130E | 014D F4           | add dword ptr ss:[ebp-C],ecx          | 存储结果
00401311 | 40                | inc eax                               |
00401312 | 8D48 E0           | lea ecx,dword ptr ds:[eax-20]         |
00401315 | 3BCF              | cmp ecx,edi                           |
00401317 | 72 E8             | jb keygenme2.cerebellum.xyzero.401301 |
00401319 | 8D45 B0           | lea eax,dword ptr ss:[ebp-50]         |
0040131C | 6A 24             | push 24                               |
0040131E | BF E9030000       | mov edi,3E9                           |
00401323 | 50                | push eax                              |
00401324 | 57                | push edi                              |
00401325 | FF75 08           | push dword ptr ss:[ebp+8]             |
00401328 | FF15 74204000     | call dword ptr ds:[<GetDlgItemTextA>] |
0040132E | 8D45 B0           | lea eax,dword ptr ss:[ebp-50]         | Serial
00401331 | 50                | push eax                              |
00401332 | FF15 48204000     | call dword ptr ds:[<atol>]            | str2long
00401338 | 59                | pop ecx                               |
00401339 | 8BC8              | mov ecx,eax                           | long result
0040133B | 8B45 F4           | mov eax,dword ptr ss:[ebp-C]          | 上一步的结果
0040133E | 33C8              | xor ecx,eax                           | serial xor 上一步结果
00401340 | 03C6              | add eax,esi                           | eax+=v1
00401342 | 3BC1              | cmp eax,ecx                           | 最后的比较
00401344 | 74 0E             | je <keygenme2.cerebellum.xyzero.Succe |
00401346 | 6A 00             | push 0                                | Fail
00401348 | 57                | push edi                              |
00401349 | FF75 08           | push dword ptr ss:[ebp+8]             |
0040134C | FF15 78204000     | call dword ptr ds:[<SetDlgItemTextA>] |
00401352 | EB 26             | jmp keygenme2.cerebellum.xyzero.40137 |
00401354 | 8D45 D0           | lea eax,dword ptr ss:[ebp-30]         |
00401357 | 50                | push eax                              |
00401358 | 8D45 90           | lea eax,dword ptr ss:[ebp-70]         |
0040135B | 68 F0304000       | push keygenme2.cerebellum.xyzero.4030 | 4030F0:"Good job %s!!!"
00401360 | 50                | push eax                              |
00401361 | FFD3              | call ebx                              |
00401363 | 83C4 0C           | add esp,C                             |
00401366 | 8D45 90           | lea eax,dword ptr ss:[ebp-70]         |
00401369 | 6A 00             | push 0                                |
0040136B | 68 EC304000       | push keygenme2.cerebellum.xyzero.4030 | 4030EC:"OK!"
00401370 | 50                | push eax                              |
00401371 | FF75 08           | push dword ptr ss:[ebp+8]             |
00401374 | FF15 84204000     | call dword ptr ds:[<MessageBoxA>]     |
0040137A | 6A 01             | push 1                                |
0040137C | 58                | pop eax                               |
0040137D | 5F                | pop edi                               |
0040137E | 5E                | pop esi                               |
0040137F | 5B                | pop ebx                               |
00401380 | C9                | leave                                 |
00401381 | C3                | ret                                   |
```


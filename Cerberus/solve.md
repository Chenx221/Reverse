算密钥

先上算法：

```c#
//判断空判断长度这里省略
public static string CalcSerial(string name)
{
    int name_length = name.Length;
    int esi = 0x18;
    int ebx = 0x400;
    int ebp = 0x32;
    int ecx = name_length;
    int eax, edx, edi, v10 = 0;
    foreach (char c in name)
    {
        eax = c;
        eax += 0x56B;
        edx = eax;
        edx ^= 0x890428;
        esi += edx;
        edx = name[3];
        if (ecx <= 9)
        {
            edx += ecx;
            edx ^= 0x209;
            edx *= esi;
            ebx += edx;
        }
        else
        {
            edx += ecx;
            edx ^= 0x209;
            edx *= ebx;
            esi += edx;
        }
        edx = eax;
        edx <<= 7;
        edx += eax;
        eax += edx * 8;
        v10 = ebx + eax * 4;
        ebx = v10; //first loop not run
    }
    string temp = name;
    for (edi = 5; edi > 0; edi--)
    {
        eax = temp[edi];
        ebp = eax + ebp + 0x134A;
        temp = Fun_403c80(temp);
    }
    string temp2 = temp; //edx
    edi = 1;
    for (ebx = 5; ebx > 0; ebx--)
    {
        ecx = temp2[0];
        eax = temp2[edi];
        eax += 0x23;
        ebp += ecx + 0x134A;
        ecx = eax * 3;
        ecx *= 5;
        edx = ecx * 5;
        eax += edx * 4;
        esi += eax * 2;
        temp2 = Fun_403c80(temp2);
        edi++;
    }
    edx = temp2[5];
    eax = 0x18;
    ecx = v10;
    eax -= edx;
    ebp += ecx;
    edx = temp2[2];
    eax ^= ebp;
    esi += 0x3c;
    int eax2 = 0x1337;
    eax2 -= edx;
    eax2 ^= esi;
    return $"LNT-{eax2}-{eax}"; //user32._wsprintfA(edx,"%s-%d-%d","LNT",eax2,eax); //esi
}

//反转字符串
public static string Fun_403c80(string name)
{
    return new string(name.Reverse().ToArray());
}
```

测试密钥：

```
chenx221
LNT-72236375--441384786
```

细节：

```assembly
00401130  | 0F85 36020000     | jne keygen.40136C                  |
00401136  | 8B9424 78010000   | mov edx,dword ptr ss:[esp+178]     | edx:EntryPoint
0040113D  | 8D4C24 18         | lea ecx,dword ptr ss:[esp+18]      | ecx:EntryPoint
00401141  | 6A 32             | push 32                            |
00401143  | 51                | push ecx                           | ecx:EntryPoint
00401144  | 68 E8030000       | push 3E8                           |
00401149  | 52                | push edx                           | edx:EntryPoint
0040114A  | FF15 9C404000     | call dword ptr ds:[<GetDlgItemText |
00401150  | 8D7C24 18         | lea edi,dword ptr ss:[esp+18]      | Name
00401154  | 83C9 FF           | or ecx,FFFFFFFF                    | ecx:EntryPoint
00401157  | 33C0              | xor eax,eax                        |
00401159  | F2:AE             | repne scasb                        |
0040115B  | F7D1              | not ecx                            | ecx:EntryPoint
0040115D  | 49                | dec ecx                            | ecx:EntryPoint
0040115E  | 83F9 05           | cmp ecx,5                          | ecx:EntryPoint
00401161  | 7D 29             | jge keygen.40118C                  | name.length > 4
00401163  | 8B8424 78010000   | mov eax,dword ptr ss:[esp+178]     |
0040116A  | 6A 30             | push 30                            |
0040116C  | 68 F0504000       | push keygen.4050F0                 | 4050F0:"Cerberus Keygen"
00401171  | 68 A4504000       | push keygen.4050A4                 | 4050A4:"Sorry name must be over 4 characters"
00401176  | 50                | push eax                           |
00401177  | FF15 98404000     | call dword ptr ds:[<MessageBoxA>]  |
0040117D  | 5F                | pop edi                            | edi:EntryPoint
0040117E  | 5E                | pop esi                            | esi:EntryPoint
0040117F  | 5D                | pop ebp                            |
00401180  | 33C0              | xor eax,eax                        |
00401182  | 5B                | pop ebx                            |
00401183  | 81C4 64010000     | add esp,164                        |
00401189  | C2 1000           | ret 10                             |
0040118C  | 33FF              | xor edi,edi                        | edi:EntryPoint
0040118E  | 85C9              | test ecx,ecx                       | ecx:EntryPoint
00401190  | 7E 56             | jle keygen.4011E8                  |
00401192  | EB 04             | jmp keygen.401198                  |
00401194  | 8B5C24 10         | mov ebx,dword ptr ss:[esp+10]      | loop start
00401198  | 0FBE443C 18       | movsx eax,byte ptr ss:[esp+edi+18] |
0040119D  | 05 6B050000       | add eax,56B                        |
004011A2  | 8BD0              | mov edx,eax                        | edx:EntryPoint
004011A4  | 81F2 28048900     | xor edx,890428                     | edx:EntryPoint
004011AA  | 03F2              | add esi,edx                        | esi:EntryPoint, edx:EntryPoint
004011AC  | 83F9 09           | cmp ecx,9                          | length
004011AF  | 0FBE5424 1B       | movsx edx,byte ptr ss:[esp+1B]     | edx:EntryPoint
004011B4  | 7E 0F             | jle keygen.4011C5                  |
004011B6  | 03D1              | add edx,ecx                        | edx:EntryPoint, ecx:EntryPoint
004011B8  | 81F2 09020000     | xor edx,209                        | edx:EntryPoint
004011BE  | 0FAFD3            | imul edx,ebx                       | edx:EntryPoint
004011C1  | 03F2              | add esi,edx                        | esi:EntryPoint, edx:EntryPoint
004011C3  | EB 0D             | jmp keygen.4011D2                  |
004011C5  | 03D1              | add edx,ecx                        | edx:EntryPoint, ecx:EntryPoint
004011C7  | 81F2 09020000     | xor edx,209                        | edx:EntryPoint
004011CD  | 0FAFD6            | imul edx,esi                       | edx:EntryPoint, esi:EntryPoint
004011D0  | 03DA              | add ebx,edx                        | edx:EntryPoint
004011D2  | 8BD0              | mov edx,eax                        | edx:EntryPoint
004011D4  | C1E2 07           | shl edx,7                          | edx:EntryPoint
004011D7  | 03D0              | add edx,eax                        | edx:EntryPoint
004011D9  | 47                | inc edi                            | edi:EntryPoint
004011DA  | 3BF9              | cmp edi,ecx                        | edi:EntryPoint, ecx:EntryPoint
004011DC  | 8D04D0            | lea eax,dword ptr ds:[eax+edx*8]   |
004011DF  | 8D1483            | lea edx,dword ptr ds:[ebx+eax*4]   | edx:EntryPoint
004011E2  | 895424 10         | mov dword ptr ss:[esp+10],edx      | [esp+10]:___RtlUserThreadStart@8+2B, edx:EntryPoint
004011E6  | 7C AC             | jl keygen.401194                   | next loop
004011E8  | BF 05000000       | mov edi,5                          | edi:EntryPoint
004011ED  | 0FBE443C 18       | movsx eax,byte ptr ss:[esp+edi+18] |
004011F2  | 8D4C24 18         | lea ecx,dword ptr ss:[esp+18]      | ecx:EntryPoint
004011F6  | 51                | push ecx                           | ecx:EntryPoint
004011F7  | 8DAC28 4A130000   | lea ebp,dword ptr ds:[eax+ebp+134A |
004011FE  | E8 7D2A0000       | call keygen.403C80                 |
00401203  | 83C4 04           | add esp,4                          |
00401206  | 4F                | dec edi                            | edi:EntryPoint
00401207  | 85FF              | test edi,edi                       | edi:EntryPoint
00401209  | 7F E2             | jg keygen.4011ED                   |
0040120B  | 8D543C 18         | lea edx,dword ptr ss:[esp+edi+18]  | edx:EntryPoint
0040120F  | 8D7C24 19         | lea edi,dword ptr ss:[esp+19]      | edi:EntryPoint
00401213  | 895424 14         | mov dword ptr ss:[esp+14],edx      | edx:EntryPoint
00401217  | BB 05000000       | mov ebx,5                          |
0040121C  | 8B4424 14         | mov eax,dword ptr ss:[esp+14]      |
00401220  | 0FBE08            | movsx ecx,byte ptr ds:[eax]        | ecx:EntryPoint
00401223  | 0FBE07            | movsx eax,byte ptr ds:[edi]        | edi:EntryPoint
00401226  | 83C0 23           | add eax,23                         |
00401229  | 8DAC29 4A130000   | lea ebp,dword ptr ds:[ecx+ebp+134A |
00401230  | 8D0C40            | lea ecx,dword ptr ds:[eax+eax*2]   | ecx:EntryPoint
00401233  | 8D0C89            | lea ecx,dword ptr ds:[ecx+ecx*4]   | ecx:EntryPoint
00401236  | 8D1489            | lea edx,dword ptr ds:[ecx+ecx*4]   | edx:EntryPoint
00401239  | 8D4C24 18         | lea ecx,dword ptr ss:[esp+18]      | ecx:EntryPoint
0040123D  | 51                | push ecx                           | ecx:EntryPoint
0040123E  | 8D0490            | lea eax,dword ptr ds:[eax+edx*4]   |
00401241  | 8D3446            | lea esi,dword ptr ds:[esi+eax*2]   | esi:EntryPoint
00401244  | E8 372A0000       | call keygen.403C80                 |
00401249  | 83C4 04           | add esp,4                          |
0040124C  | 47                | inc edi                            | edi:EntryPoint
0040124D  | 4B                | dec ebx                            |
0040124E  | 75 CC             | jne keygen.40121C                  |
00401250  | 0FBE5424 1D       | movsx edx,byte ptr ss:[esp+1D]     | edx:EntryPoint
00401255  | 8B4C24 10         | mov ecx,dword ptr ss:[esp+10]      | ecx:EntryPoint, [esp+10]:___RtlUserThreadStart@8+2B
00401259  | B8 18000000       | mov eax,18                         |
0040125E  | 2BC2              | sub eax,edx                        | edx:EntryPoint
00401260  | 03E9              | add ebp,ecx                        | ecx:EntryPoint
00401262  | 0FBE5424 1A       | movsx edx,byte ptr ss:[esp+1A]     | edx:EntryPoint
00401267  | 33C5              | xor eax,ebp                        |
00401269  | 83C6 3C           | add esi,3C                         | esi:EntryPoint
0040126C  | 50                | push eax                           |
0040126D  | B8 37130000       | mov eax,1337                       |
00401272  | 2BC2              | sub eax,edx                        | edx:EntryPoint
00401274  | 8D8C24 B0000000   | lea ecx,dword ptr ss:[esp+B0]      | ecx:EntryPoint
0040127B  | 33C6              | xor eax,esi                        | esi:EntryPoint
0040127D  | 8D9424 14010000   | lea edx,dword ptr ss:[esp+114]     | edx:EntryPoint
00401284  | 50                | push eax                           |
00401285  | 51                | push ecx                           | ecx:EntryPoint
00401286  | 68 98504000       | push keygen.405098                 | 405098:"%s-%d-%d"
0040128B  | 52                | push edx                           | edx:EntryPoint
0040128C  | FF15 A0404000     | call dword ptr ds:[<wsprintfA>]    |
00401292  | 8BBC24 8C010000   | mov edi,dword ptr ss:[esp+18C]     | edi:EntryPoint
00401299  | 8B35 9C404000     | mov esi,dword ptr ds:[<GetDlgItemT | esi:EntryPoint
0040129F  | 83C4 14           | add esp,14                         |
004012A2  | 8D4424 48         | lea eax,dword ptr ss:[esp+48]      |
004012A6  | 6A 64             | push 64                            |
004012A8  | 50                | push eax                           |
004012A9  | 68 EA030000       | push 3EA                           |
004012AE  | 57                | push edi                           | edi:EntryPoint
004012AF  | FFD6              | call esi                           | esi:EntryPoint
004012B1  | 8D4C24 48         | lea ecx,dword ptr ss:[esp+48]      | serial
004012B5  | 6A 64             | push 64                            |
004012B7  | 51                | push ecx                           | ecx:EntryPoint
004012B8  | 68 EA030000       | push 3EA                           |
004012BD  | 57                | push edi                           | edi:EntryPoint
004012BE  | FFD6              | call esi                           | esi:EntryPoint
004012C0  | 85C0              | test eax,eax                       |
004012C2  | 75 22             | jne keygen.4012E6                  | serial不能为空
004012C4  | 6A 30             | push 30                            |
004012C6  | 68 F0504000       | push keygen.4050F0                 | 4050F0:"Cerberus Keygen"
004012CB  | 68 7C504000       | push keygen.40507C                 | 40507C:"You have to enter a serial"
004012D0  | 57                | push edi                           | edi:EntryPoint
004012D1  | FF15 98404000     | call dword ptr ds:[<MessageBoxA>]  |
004012D7  | 5F                | pop edi                            | edi:EntryPoint
004012D8  | 5E                | pop esi                            | esi:EntryPoint
004012D9  | 5D                | pop ebp                            |
004012DA  | 33C0              | xor eax,eax                        |
004012DC  | 5B                | pop ebx                            |
004012DD  | 81C4 64010000     | add esp,164                        |
004012E3  | C2 1000           | ret 10                             |
004012E6  | 8DB424 10010000   | lea esi,dword ptr ss:[esp+110]     | esi:EntryPoint
004012ED  | 8D4424 48         | lea eax,dword ptr ss:[esp+48]      |
004012F1  | 8A10              | mov dl,byte ptr ds:[eax]           |
004012F3  | 8A1E              | mov bl,byte ptr ds:[esi]           | esi:EntryPoint
004012F5  | 8ACA              | mov cl,dl                          |
004012F7  | 3AD3              | cmp dl,bl                          |
004012F9  | 75 1E             | jne keygen.401319                  |
004012FB  | 84C9              | test cl,cl                         |
004012FD  | 74 16             | je keygen.401315                   |
004012FF  | 8A50 01           | mov dl,byte ptr ds:[eax+1]         |
00401302  | 8A5E 01           | mov bl,byte ptr ds:[esi+1]         |
00401305  | 8ACA              | mov cl,dl                          |
00401307  | 3AD3              | cmp dl,bl                          |
00401309  | 75 0E             | jne keygen.401319                  |
0040130B  | 83C0 02           | add eax,2                          |
0040130E  | 83C6 02           | add esi,2                          | esi:EntryPoint
00401311  | 84C9              | test cl,cl                         |
00401313  | 75 DC             | jne keygen.4012F1                  |
00401315  | 33C0              | xor eax,eax                        |
00401317  | EB 05             | jmp keygen.40131E                  |
00401319  | 1BC0              | sbb eax,eax                        |
0040131B  | 83D8 FF           | sbb eax,FFFFFFFF                   |
0040131E  | 85C0              | test eax,eax                       |
00401320  | 75 25             | jne keygen.401347                  |
00401322  | 6A 40             | push 40                            |
00401324  | 68 F0504000       | push keygen.4050F0                 | 4050F0:"Cerberus Keygen"
00401329  | 68 44504000       | push keygen.405044                 | 405044:"Good job cracker!\n\nNow write a keygen and a tutorial."
0040132E  | 57                | push edi                           | edi:EntryPoint
0040132F  | FF15 98404000     | call dword ptr ds:[<MessageBoxA>]  |
00401335  | 5F                | pop edi                            | edi:EntryPoint
00401336  | 5E                | pop esi                            | esi:EntryPoint
00401337  | 5D                | pop ebp                            |
00401338  | B8 01000000       | mov eax,1                          |
0040133D  | 5B                | pop ebx                            |
0040133E  | 81C4 64010000     | add esp,164                        |
00401344  | C2 1000           | ret 10                             |
00401347  | 6A 10             | push 10                            |
00401349  | 68 F0504000       | push keygen.4050F0                 | 4050F0:"Cerberus Keygen"
0040134E  | 68 30504000       | push keygen.405030                 | 405030:"Wrong!\n\nTry again!"
00401353  | 57                | push edi                           | edi:EntryPoint
00401354  | FF15 98404000     | call dword ptr ds:[<MessageBoxA>]  |
0040135A  | 5F                | pop edi                            | edi:EntryPoint
0040135B  | 5E                | pop esi                            | esi:EntryPoint
0040135C  | 5D                | pop ebp                            |
0040135D  | B8 01000000       | mov eax,1                          |
00401362  | 5B                | pop ebx                            |
00401363  | 81C4 64010000     | add esp,164                        |
00401369  | C2 1000           | ret 10                             |
```

(按钮事件 EP往上翻一点就到了)
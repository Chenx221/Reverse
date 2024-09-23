easy难度

判断:

```c#
string name = "user input";
string serial = "user input";
int time = nema.Length;
int p = 0;
if(time<5){
    //FAIL
}
while(time>0){
    char n = name[p];
    n-=time;
    if(serial[p]!==n) break; //FAIL
    p++;
    time--;
}
if(time==0){
    //SUCCESS
}else{
    //FAIL
}
```

```assembly
004010FC | 55                   | push ebp                                     |
004010FD | 8BEC                 | mov ebp,esp                                  |
004010FF | 6A 14                | push 14                                      |
00401101 | 68 80304000          | push ad_cm#2.403080                          |
00401106 | 68 B80B0000          | push BB8                                     |
0040110B | FF75 08              | push dword ptr ss:[ebp+8]                    |
0040110E | E8 77000000          | call <JMP.&_GetDlgItemTextA@16>              | get name
00401113 | 8BF0                 | mov esi,eax                                  | eax:@BaseThreadInitThunk@12
00401115 | 8D01                 | lea eax,dword ptr ds:[ecx]                   | eax:@BaseThreadInitThunk@12
00401117 | 83FE 05              | cmp esi,5                                    |
0040111A | 7D 18                | jge ad_cm#2.401134                           |
0040111C | 6A 40                | push 40                                      | name too short
0040111E | 68 12304000          | push ad_cm#2.403012                          | 403012:"ArturDents CrackMe#2"
00401123 | 68 44304000          | push ad_cm#2.403044                          | 403044:"Your name must be at least five characters long!"
00401128 | FF75 08              | push dword ptr ss:[ebp+8]                    |
0040112B | E8 60000000          | call <JMP.&_MessageBoxA@16>                  |
00401130 | 33C0                 | xor eax,eax                                  | eax:@BaseThreadInitThunk@12
00401132 | EB 40                | jmp <ad_cm#2.Fail>                           |
00401134 | 6A 14                | push 14                                      |
00401136 | 68 80324000          | push ad_cm#2.403280                          |
0040113B | 68 B90B0000          | push BB9                                     |
00401140 | FF75 08              | push dword ptr ss:[ebp+8]                    |
00401143 | E8 42000000          | call <JMP.&_GetDlgItemTextA@16>              | get serial
00401148 | B8 80304000          | mov eax,ad_cm#2.403080                       | eax:@BaseThreadInitThunk@12
0040114D | BB 80324000          | mov ebx,ad_cm#2.403280                       |
00401152 | 8BCE                 | mov ecx,esi                                  | esi=name.length
00401154 | 8A10                 | mov dl,byte ptr ds:[eax]                     | eax:@BaseThreadInitThunk@12
00401156 | 2AD1                 | sub dl,cl                                    |
00401158 | 3813                 | cmp byte ptr ds:[ebx],dl                     |
0040115A | 75 18                | jne <ad_cm#2.Fail>                           |
0040115C | 40                   | inc eax                                      | eax:@BaseThreadInitThunk@12
0040115D | 43                   | inc ebx                                      |
0040115E | E2 F4                | loop ad_cm#2.401154                          |
00401160 | 6A 40                | push 40                                      |
00401162 | 68 12304000          | push ad_cm#2.403012                          | 403012:"ArturDents CrackMe#2"
00401167 | 68 27304000          | push ad_cm#2.403027                          | 403027:"Yeah, you did it!"
0040116C | FF75 08              | push dword ptr ss:[ebp+8]                    |
0040116F | E8 1C000000          | call <JMP.&_MessageBoxA@16>                  |
00401174 | C9                   | leave                                        | fail
00401175 | C2 0400              | ret 4                                        |
```


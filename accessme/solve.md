找用户名和密码

检测od(OLLYDBG.EXE)

| username | password  |
| -------- | --------- |
| Karl     | TWE-3265  |
| Erik     | TWE-2132  |
| admin    | allaccess |

经过整理的判断逻辑

```c#
string name="user input";
string password="user input";
string true_pwd="";
string process_name = "OLLYDBG.EXE";
string[] usernames = {"Karl","Erik","admin"};
string[] passwords = {"TWE-3265","TWE-2132","allaccess"};

if(!checkProcess(process_name)){ //checkProcess检测到时返回True
    int index = Array.IndexOf(usernames, name);
    if(index >= 0 && index+1<=5){
        true_pwd = passwords[index];
        if(true_pwd.Equals(password)){
            //SUCCESS
        }else{
            //FAIL
            //密码不匹配
        }
    }else{
        //FAIL
        //查无此号
    }
}else{
    //FAIL
    //检测到调试器运行中
}
```

Main: 004014E1 (以下为部分内容)

```assembly
004016D1 | 8D85 A8FEFFFF        | lea eax,dword ptr ss:[ebp-158]                  |
004016D7 | 894424 04            | mov dword ptr ss:[esp+4],eax                    | [esp+04]:_scanf+18
004016DB | C70424 55304000      | mov dword ptr ss:[esp],accessme.403055          | [esp]:"OLLYDBG.EXE", 403055:"%s"
004016E2 | E8 09060000          | call <JMP.&_printf>                             |
004016E7 | C74424 04 30414000   | mov dword ptr ss:[esp+4],accessme.404130        | [esp+04]:_scanf+18, 404130:"admin"
004016EF | C70424 55304000      | mov dword ptr ss:[esp],accessme.403055          | [esp]:"OLLYDBG.EXE", 403055:"%s"
004016F6 | E8 E5050000          | call <JMP.&_scanf>                              |
004016FB | C74424 04 30414000   | mov dword ptr ss:[esp+4],accessme.404130        | [esp+04]:_scanf+18, 404130:"admin"
00401703 | C70424 58304000      | mov dword ptr ss:[esp],accessme.403058          | [esp]:"OLLYDBG.EXE", 403058:"Enter password for account '%s': "
0040170A | E8 E1050000          | call <JMP.&_printf>                             |
0040170F | C74424 04 A0404000   | mov dword ptr ss:[esp+4],accessme.4040A0        | [esp+04]:_scanf+18, 4040A0:"admin"
00401717 | C70424 55304000      | mov dword ptr ss:[esp],accessme.403055          | [esp]:"OLLYDBG.EXE", 403055:"%s"
0040171E | E8 BD050000          | call <JMP.&_scanf>                              |
00401723 | 8D85 98FEFFFF        | lea eax,dword ptr ss:[ebp-168]                  |
00401729 | 894424 08            | mov dword ptr ss:[esp+8],eax                    | [esp+08]:__input_l
0040172D | C74424 04 A0404000   | mov dword ptr ss:[esp+4],accessme.4040A0        | [esp+04]:_scanf+18, 4040A0:"admin"
00401735 | C70424 30414000      | mov dword ptr ss:[esp],accessme.404130          | [esp]:"OLLYDBG.EXE", 404130:"admin"
0040173C | E8 CFFCFFFF          | call <accessme.Check>                           |
00401741 | A3 14404000          | mov dword ptr ds:[404014],eax                   |
00401746 | 833D 14404000 05     | cmp dword ptr ds:[404014],5                     |
0040174D | 75 0E                | jne accessme.40175D                             |
0040174F | C70424 7A304000      | mov dword ptr ss:[esp],accessme.40307A          | success
00401756 | E8 95050000          | call <JMP.&_printf>                             |
0040175B | EB 0C                | jmp accessme.401769                             |
0040175D | C70424 94304000      | mov dword ptr ss:[esp],accessme.403094          | fail
00401764 | E8 87050000          | call <JMP.&_printf>                             |
00401769 | E8 62050000          | call <JMP.&__getch>                             |
0040176E | C70424 BB304000      | mov dword ptr ss:[esp],accessme.4030BB          | [esp]:"OLLYDBG.EXE", 4030BB:"cls"
00401775 | E8 46050000          | call <JMP.&_system>                             |
0040177A | E8 62FDFFFF          | call accessme.4014E1                            |
0040177F | B8 00000000          | mov eax,0                                       |
00401784 | C9                   | leave                                           |
00401785 | C3                   | ret                                             |
```

Check: 00401410

```assembly
00401410 | 55                   | push ebp                                        | check username & password
00401411 | 89E5                 | mov ebp,esp                                     |
00401413 | 83EC 18              | sub esp,18                                      |
00401416 | C705 10404000 000000 | mov dword ptr ds:[404010],0                     |
00401420 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:name
00401423 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
00401426 | E8 15090000          | call <JMP.&_strlen>                             |
0040142B | 3905 10404000        | cmp dword ptr ds:[404010],eax                   |
00401431 | 73 22                | jae accessme.401455                             |
00401433 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
00401436 | 0305 10404000        | add eax,dword ptr ds:[404010]                   |
0040143C | 0FBE00               | movsx eax,byte ptr ds:[eax]                     |
0040143F | 0305 70404000        | add eax,dword ptr ds:[404070]                   |
00401445 | 83C0 25              | add eax,25                                      |
00401448 | A3 70404000          | mov dword ptr ds:[404070],eax                   |
0040144D | FF05 10404000        | inc dword ptr ds:[404010]                       |
00401453 | EB CB                | jmp accessme.401420                             | 将name每个字符的ascii相加起来(每一个还要额外+0x25)
00401455 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
00401458 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
0040145B | E8 E0080000          | call <JMP.&_strlen>                             |
00401460 | 89C2                 | mov edx,eax                                     | length
00401462 | A1 70404000          | mov eax,dword ptr ds:[404070]                   | 上一步的运算结果
00401467 | 0FAFC2               | imul eax,edx                                    | *
0040146A | A3 70404000          | mov dword ptr ds:[404070],eax                   |
0040146F | 8B45 10              | mov eax,dword ptr ss:[ebp+10]                   | [ebp+10]:"OLLYDBG.EXE"
00401472 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
00401475 | E8 BBFEFFFF          | call <accessme._checkProcess>                   |
0040147A | 85C0                 | test eax,eax                                    |
0040147C | 75 10                | jne accessme.40148E                             |
0040147E | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
00401481 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
00401484 | E8 07FEFFFF          | call <accessme.IsUsernameExists>                |
00401489 | A3 90404000          | mov dword ptr ds:[404090],eax                   |
0040148E | 833D 90404000 05     | cmp dword ptr ds:[404090],5                     |
00401495 | 7E 10                | jle accessme.4014A7                             |
00401497 | A1 90404000          | mov eax,dword ptr ds:[404090]                   |
0040149C | 0305 70404000        | add eax,dword ptr ds:[404070]                   |
004014A2 | 8945 FC              | mov dword ptr ss:[ebp-4],eax                    |
004014A5 | EB 35                | jmp accessme.4014DC                             |
004014A7 | 8B15 90404000        | mov edx,dword ptr ds:[404090]                   | edx:_KiFastSystemCallRet@0
004014AD | 89D0                 | mov eax,edx                                     | edx:_KiFastSystemCallRet@0
004014AF | C1E0 04              | shl eax,4                                       |
004014B2 | 01D0                 | add eax,edx                                     | edx:_KiFastSystemCallRet@0
004014B4 | C1E0 03              | shl eax,3                                       |
004014B7 | 0305 80404000        | add eax,dword ptr ds:[404080]                   |
004014BD | 83C0 40              | add eax,40                                      | find password
004014C0 | 894424 04            | mov dword ptr ss:[esp+4],eax                    | [esp+04]:_scanf+18
004014C4 | 8B45 0C              | mov eax,dword ptr ss:[ebp+C]                    | [ebp+0C]:"admin"
004014C7 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
004014CA | E8 61080000          | call <JMP.&_strcmp>                             |
004014CF | 85C0                 | test eax,eax                                    |
004014D1 | 75 09                | jne accessme.4014DC                             |
004014D3 | C745 FC 05000000     | mov dword ptr ss:[ebp-4],5                      | 返回5代表成功
004014DA | EB 00                | jmp accessme.4014DC                             |
004014DC | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                    |
004014DF | C9                   | leave                                           |
004014E0 | C3                   | ret                                             |
```

_checkProcess: 00401335 内容就不放了

IsUsernameExists: 00401290

```assembly
00401290 | 55                   | push ebp                                        |
00401291 | 89E5                 | mov ebp,esp                                     |
00401293 | 83EC 18              | sub esp,18                                      |
00401296 | C705 10404000 000000 | mov dword ptr ds:[404010],0                     |
004012A0 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
004012A3 | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
004012A6 | E8 950A0000          | call <JMP.&_strlen>                             |
004012AB | 3905 10404000        | cmp dword ptr ds:[404010],eax                   |
004012B1 | 73 75                | jae accessme.401328                             |
004012B3 | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
004012B6 | 0305 10404000        | add eax,dword ptr ds:[404010]                   |
004012BC | 0FBE00               | movsx eax,byte ptr ds:[eax]                     |
004012BF | 0305 70404000        | add eax,dword ptr ds:[404070]                   |
004012C5 | 05 DE000000          | add eax,DE                                      |
004012CA | A3 70404000          | mov dword ptr ds:[404070],eax                   |
004012CF | C705 B0414000 010000 | mov dword ptr ds:[4041B0],1                     |
004012D9 | 833D B0414000 03     | cmp dword ptr ds:[4041B0],3                     |
004012E0 | 7F 3B                | jg accessme.40131D                              |
004012E2 | 8B15 B0414000        | mov edx,dword ptr ds:[4041B0]                   | edx:_KiFastSystemCallRet@0
004012E8 | 89D0                 | mov eax,edx                                     | edx:_KiFastSystemCallRet@0
004012EA | C1E0 04              | shl eax,4                                       |
004012ED | 01D0                 | add eax,edx                                     | edx:_KiFastSystemCallRet@0
004012EF | C1E0 03              | shl eax,3                                       |
004012F2 | 0305 80404000        | add eax,dword ptr ds:[404080]                   |
004012F8 | 894424 04            | mov dword ptr ss:[esp+4],eax                    | [esp+04]:_scanf+18
004012FC | 8B45 08              | mov eax,dword ptr ss:[ebp+8]                    | [ebp+08]:"admin"
004012FF | 890424               | mov dword ptr ss:[esp],eax                      | [esp]:"OLLYDBG.EXE"
00401302 | E8 290A0000          | call <JMP.&_strcmp>                             | 匹配用户名
00401307 | 85C0                 | test eax,eax                                    |
00401309 | 75 0A                | jne accessme.401315                             |
0040130B | A1 B0414000          | mov eax,dword ptr ds:[4041B0]                   |
00401310 | 8945 FC              | mov dword ptr ss:[ebp-4],eax                    |
00401313 | EB 1B                | jmp accessme.401330                             |
00401315 | FF05 B0414000        | inc dword ptr ds:[4041B0]                       |
0040131B | EB BC                | jmp accessme.4012D9                             |
0040131D | FF05 10404000        | inc dword ptr ds:[404010]                       |
00401323 | E9 78FFFFFF          | jmp accessme.4012A0                             |
00401328 | A1 70404000          | mov eax,dword ptr ds:[404070]                   |
0040132D | 8945 FC              | mov dword ptr ss:[ebp-4],eax                    |
00401330 | 8B45 FC              | mov eax,dword ptr ss:[ebp-4]                    |
00401333 | C9                   | leave                                           |
00401334 | C3                   | ret                                             |
```


去调试器检测（和上一版本系统）：

```
>bjcm40a.exe
0000435A:FF->E9
0000435B:15->B0
0000435C:9C->01
0000435D:10->00
0000435E:40->00
```

计算serial

这里整理了一些检查serial的代码

```c#
string serial = "serial";
int length = serial.Length;
if(serial!="")
{
    if(serial[0]/5==length)
    {
        int v = serial[0]+serial[^1];
        v= ((v<<1) ^ 3)*0x2468AC;
        v= Convert.ToInt32(v.ToString("X")[^3..], 16);
        int v2 = serial[1]+serial[2]+serial[3];
        v2 = v2>>1;
        v2 = (v2^6)*0x20;
        if(v1==v2)
        {
            //SUCCESS
        }
    }
}
```

尝试一下暴力寻找匹配的serial

```c#
namespace GuestSerial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int p1 = 35; p1 <= 126; p1 += 5)
            {
                for (int p2 = 32; p2 <= 126; p2++)
                {
                    for (int p3 = 32; p3 <= 126; p3++)
                    {
                        for (int p4 = 32; p4 <= 126; p4++)
                        {
                            for (int p5 = 32; p5 <= 126; p5++)
                            {
                                int v1 = (((p1 + p2) * 2) ^ 3) * 0x2468AC % 4096;
                                int v2 = (((p3 + p4 + p5) / 2) ^ 6) * 0x20;
                                if (v1 == v2)
                                {
                                    Console.WriteLine("Serial: " + p1 + (char)p3 + (char)p4 + (char)p5 + "..." + (char)p2);
                                    return;
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}

```

去TM的，不存在serial

```

D:\sources\keygen\GuestSerial\bin\Release\net8.0\GuestSerial.exe (进程 21136)已退出，代码为 0 (0x0)。
按任意键关闭此窗口. . .
```

> I don't know is this crackme even crackable, if not mail me
> and I'll make the new one, witch will be easier then this!

只能patch了，作为报复，这次直接在检测失败上开刀

```
>bjcm40a.exe
00004DB0:8B->E9
00004DB1:1D->4E
00004DB2:D4->FF
00004DB3:10->FF
00004DB4:40->FF
```

细节：(已删去一处没用的判断 serial[0]>0x64 && serial[0]<0x1E )

```assembly
004046DD | 8985 60FEFFFF        | mov dword ptr ss:[ebp-1A0],eax              | serial.Length a
004046E3 | 898D 54FEFFFF        | mov dword ptr ss:[ebp-1AC],ecx              | serial[0] b
004046E9 | DB85 60FEFFFF        | fild dword ptr ss:[ebp-1A0]                 |
004046EF | DD9D 58FEFFFF        | fstp qword ptr ss:[ebp-1A8]                 | a
004046F5 | DB85 54FEFFFF        | fild dword ptr ss:[ebp-1AC]                 |
004046FB | DD9D 4CFEFFFF        | fstp qword ptr ss:[ebp-1B4]                 | b
00404701 | DD85 4CFEFFFF        | fld qword ptr ss:[ebp-1B4]                  | hex2dec(b)
00404707 | 833D 00604000 00     | cmp dword ptr ds:[406000],0                 | 0
0040470E | 75 08                | jne bjcm40a.404718                          |
00404710 | DC35 40114000        | fdiv qword ptr ds:[401140]                  | 5
00404716 | EB 11                | jmp bjcm40a.404729                          |
00404718 | FF35 44114000        | push dword ptr ds:[401144]                  |
0040471E | FF35 40114000        | push dword ptr ds:[401140]                  |
00404724 | E8 6BCAFFFF          | call <JMP.&_adj_fdiv_m64>                   |
00404729 | DFE0                 | fnstsw ax                                   |
0040472B | A8 0D                | test al,D                                   |
0040472D | 0F85 0E080000        | jne <bjcm40a.FPException>                   |
00404733 | FF15 50104000        | call dword ptr ds:[<__vbaFPFix>]            |
00404739 | FF15 54104000        | call dword ptr ds:[<__vbaFpR8>]             |
0040473F | DC9D 58FEFFFF        | fcomp qword ptr ss:[ebp-1A8]                |
00404745 | DFE0                 | fnstsw ax                                   |
00404747 | F6C4 40              | test ah,40                                  |
0040474A | 75 07                | jne bjcm40a.404753                          |
0040474C | B8 01000000          | mov eax,1                                   |
00404751 | EB 02                | jmp bjcm40a.404755                          |
00404753 | 33C0                 | xor eax,eax                                 |
00404755 | F7D8                 | neg eax                                     |
00404757 | 66:8BD8              | mov bx,ax                                   |
0040475A | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]               |
0040475D | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404760 | 52                   | push edx                                    |
00404761 | 50                   | push eax                                    |
00404762 | 6A 02                | push 2                                      |
00404764 | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
0040476A | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]               |
0040476D | 8D55 8C              | lea edx,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
00404770 | 51                   | push ecx                                    |
00404771 | 52                   | push edx                                    |
00404772 | 6A 02                | push 2                                      |
00404774 | FF15 24104000        | call dword ptr ds:[<__vbaFreeObjList>]      |
0040477A | 83C4 18              | add esp,18                                  |
0040477D | 66:3BDF              | cmp bx,di                                   | check serial[0] ascii / 5 == serial.Length
00404780 | 0F85 2A060000        | jne <bjcm40a.FAIL>                          |
00404786 | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404788 | 56                   | push esi                                    |
00404789 | FF90 08030000        | call dword ptr ds:[eax+308]                 |
0040478F | 8B1D 40104000        | mov ebx,dword ptr ds:[<__vbaObjSet>]        |
00404795 | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
00404798 | 50                   | push eax                                    |
00404799 | 51                   | push ecx                                    |
0040479A | FFD3                 | call ebx                                    |
0040479C | 8B16                 | mov edx,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
0040479E | 56                   | push esi                                    |
0040479F | FF92 08030000        | call dword ptr ds:[edx+308]                 |
004047A5 | 50                   | push eax                                    |
004047A6 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
004047A9 | 50                   | push eax                                    |
004047AA | FFD3                 | call ebx                                    |
004047AC | 8B45 8C              | mov eax,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
004047AF | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
004047B5 | 6A 01                | push 1                                      |
004047B7 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]               |
004047BD | BB 09000000          | mov ebx,9                                   | 09:'\t'
004047C2 | 51                   | push ecx                                    |
004047C3 | 52                   | push edx                                    |
004047C4 | 897D 8C              | mov dword ptr ss:[ebp-74],edi               | [ebp-74]:_PeekMessageA@20, edi:_PeekMessageA@20
004047C7 | 8985 7CFFFFFF        | mov dword ptr ss:[ebp-84],eax               |
004047CD | 899D 74FFFFFF        | mov dword ptr ss:[ebp-8C],ebx               | [ebp-8C]:_PeekMessageA@20+1F1
004047D3 | FF15 DC104000        | call dword ptr ds:[<Ordinal#617>]           | left(serial,1)
004047D9 | 8B45 88              | mov eax,dword ptr ss:[ebp-78]               |
004047DC | 6A 01                | push 1                                      |
004047DE | 8985 5CFFFFFF        | mov dword ptr ss:[ebp-A4],eax               |
004047E4 | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]               |
004047EA | 8D8D 44FFFFFF        | lea ecx,dword ptr ss:[ebp-BC]               |
004047F0 | 50                   | push eax                                    |
004047F1 | 51                   | push ecx                                    |
004047F2 | 897D 88              | mov dword ptr ss:[ebp-78],edi               | edi:_PeekMessageA@20
004047F5 | 899D 54FFFFFF        | mov dword ptr ss:[ebp-AC],ebx               |
004047FB | FF15 EC104000        | call dword ptr ds:[<Ordinal#619>]           | right(serial,1)
00404801 | 8B1D 98104000        | mov ebx,dword ptr ds:[<__vbaStrVarVal>]     |
00404807 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]               |
0040480D | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               |
00404810 | 52                   | push edx                                    |
00404811 | 50                   | push eax                                    |
00404812 | FFD3                 | call ebx                                    |
00404814 | 50                   | push eax                                    |
00404815 | FF15 28104000        | call dword ptr ds:[<Ordinal#516>]           | int(right...)
0040481B | 66:8BD0              | mov dx,ax                                   |
0040481E | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]               |
00404824 | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404827 | 51                   | push ecx                                    |
00404828 | 50                   | push eax                                    |
00404829 | 66:8995 4AFEFFFF     | mov word ptr ss:[ebp-1B6],dx                |
00404830 | FFD3                 | call ebx                                    |
00404832 | 50                   | push eax                                    |
00404833 | FF15 28104000        | call dword ptr ds:[<Ordinal#516>]           | int(left...)
00404839 | 66:8B8D 4AFEFFFF     | mov cx,word ptr ss:[ebp-1B6]                |
00404840 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]               |
00404846 | 66:03C8              | add cx,ax                                   | sum(int,int)
00404849 | 52                   | push edx                                    |
0040484A | 0F80 F6060000        | jo <bjcm40a.ErrOverflow>                    |
00404850 | 66:898D 3CFFFFFF     | mov word ptr ss:[ebp-C4],cx                 |
00404857 | C785 34FFFFFF 020000 | mov dword ptr ss:[ebp-CC],2                 |
00404861 | FF15 B0104000        | call dword ptr ds:[<Ordinal#572>]           | hex(sum...)
00404867 | 8B1D E4104000        | mov ebx,dword ptr ds:[<__vbaStrMove>]       |
0040486D | 8BD0                 | mov edx,eax                                 |
0040486F | 8D4D 90              | lea ecx,dword ptr ss:[ebp-70]               | move
00404872 | FFD3                 | call ebx                                    |
00404874 | BA 78294000          | mov edx,bjcm40a.402978                      | 402978:L"SHL"
00404879 | 8D4D 98              | lea ecx,dword ptr ss:[ebp-68]               |
0040487C | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404882 | BA 682B4000          | mov edx,bjcm40a.402B68                      |
00404887 | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               |
0040488A | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404890 | 8B55 90              | mov edx,dword ptr ss:[ebp-70]               |
00404893 | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]               |
00404896 | 897D 90              | mov dword ptr ss:[ebp-70],edi               | edi:_PeekMessageA@20
00404899 | FFD3                 | call ebx                                    |
0040489B | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
0040489D | 8D4D 94              | lea ecx,dword ptr ss:[ebp-6C]               |
004048A0 | 8D55 98              | lea edx,dword ptr ss:[ebp-68]               |
004048A3 | 51                   | push ecx                                    |
004048A4 | 52                   | push edx                                    |
004048A5 | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               |
004048A8 | 8D55 A0              | lea edx,dword ptr ss:[ebp-60]               |
004048AB | 51                   | push ecx                                    |
004048AC | 52                   | push edx                                    |
004048AD | 56                   | push esi                                    |
004048AE | FF90 F8060000        | call dword ptr ds:[eax+6F8]                 | fun(hex(...),3,"SHL") CHECK HERE
004048B4 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
004048B6 | 7D 12                | jge bjcm40a.4048CA                          |
004048B8 | 68 F8060000          | push 6F8                                    |
004048BD | 68 DC274000          | push bjcm40a.4027DC                         |
004048C2 | 56                   | push esi                                    |
004048C3 | 50                   | push eax                                    |
004048C4 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004048CA | 8B55 94              | mov edx,dword ptr ss:[ebp-6C]               |
004048CD | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
004048D0 | 897D 94              | mov dword ptr ss:[ebp-6C],edi               | edi:_PeekMessageA@20
004048D3 | FFD3                 | call ebx                                    |
004048D5 | 8D45 90              | lea eax,dword ptr ss:[ebp-70]               |
004048D8 | 8D4D 98              | lea ecx,dword ptr ss:[ebp-68]               |
004048DB | 50                   | push eax                                    |
004048DC | 8D55 9C              | lea edx,dword ptr ss:[ebp-64]               |
004048DF | 51                   | push ecx                                    |
004048E0 | 8D45 A0              | lea eax,dword ptr ss:[ebp-60]               |
004048E3 | 52                   | push edx                                    |
004048E4 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
004048E7 | 50                   | push eax                                    |
004048E8 | 8D55 A8              | lea edx,dword ptr ss:[ebp-58]               |
004048EB | 51                   | push ecx                                    |
004048EC | 52                   | push edx                                    |
004048ED | 6A 06                | push 6                                      |
004048EF | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
004048F5 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
004048F8 | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
004048FB | 50                   | push eax                                    |
004048FC | 51                   | push ecx                                    |
004048FD | 6A 02                | push 2                                      |
004048FF | FF15 24104000        | call dword ptr ds:[<__vbaFreeObjList>]      |
00404905 | 8D95 34FFFFFF        | lea edx,dword ptr ss:[ebp-CC]               |
0040490B | 8D85 44FFFFFF        | lea eax,dword ptr ss:[ebp-BC]               |
00404911 | 52                   | push edx                                    |
00404912 | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]               |
00404918 | 50                   | push eax                                    |
00404919 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]               |
0040491F | 51                   | push ecx                                    |
00404920 | 8D85 74FFFFFF        | lea eax,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404926 | 52                   | push edx                                    |
00404927 | 50                   | push eax                                    |
00404928 | 6A 05                | push 5                                      |
0040492A | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
00404930 | 83C4 40              | add esp,40                                  |
00404933 | BA 94294000          | mov edx,bjcm40a.402994                      | *
00404938 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
0040493B | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404941 | BA 702B4000          | mov edx,bjcm40a.402B70                      | 402B70:L"2468AC"
00404946 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404949 | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
0040494F | 8B0E                 | mov ecx,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404951 | 8D55 A0              | lea edx,dword ptr ss:[ebp-60]               |
00404954 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               |
00404957 | 52                   | push edx                                    |
00404958 | 50                   | push eax                                    |
00404959 | 8D55 A8              | lea edx,dword ptr ss:[ebp-58]               |
0040495C | 8D45 D4              | lea eax,dword ptr ss:[ebp-2C]               |
0040495F | 52                   | push edx                                    |
00404960 | 50                   | push eax                                    |
00404961 | 56                   | push esi                                    |
00404962 | FF91 F8060000        | call dword ptr ds:[ecx+6F8]                 | pre_result * 0x2468AC = v1
00404968 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
0040496A | 7D 12                | jge bjcm40a.40497E                          |
0040496C | 68 F8060000          | push 6F8                                    |
00404971 | 68 DC274000          | push bjcm40a.4027DC                         |
00404976 | 56                   | push esi                                    |
00404977 | 50                   | push eax                                    |
00404978 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
0040497E | 8B55 A0              | mov edx,dword ptr ss:[ebp-60]               |
00404981 | 8D4D D4              | lea ecx,dword ptr ss:[ebp-2C]               |
00404984 | 897D A0              | mov dword ptr ss:[ebp-60],edi               | edi:_PeekMessageA@20
00404987 | FFD3                 | call ebx                                    |
00404989 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
0040498C | 8D55 A8              | lea edx,dword ptr ss:[ebp-58]               |
0040498F | 51                   | push ecx                                    |
00404990 | 52                   | push edx                                    |
00404991 | 6A 02                | push 2                                      |
00404993 | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
00404999 | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
0040499B | 83C4 0C              | add esp,C                                   |
0040499E | 56                   | push esi                                    |
0040499F | FF90 08030000        | call dword ptr ds:[eax+308]                 |
004049A5 | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
004049A8 | 50                   | push eax                                    |
004049A9 | 51                   | push ecx                                    |
004049AA | FF15 40104000        | call dword ptr ds:[<__vbaObjSet>]           |
004049B0 | 8B10                 | mov edx,dword ptr ds:[eax]                  |
004049B2 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
004049B5 | 51                   | push ecx                                    |
004049B6 | 50                   | push eax                                    |
004049B7 | 8985 ECFEFFFF        | mov dword ptr ss:[ebp-114],eax              |
004049BD | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
004049C3 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
004049C5 | DBE2                 | fnclex                                      |
004049C7 | 7D 18                | jge bjcm40a.4049E1                          |
004049C9 | 8B95 ECFEFFFF        | mov edx,dword ptr ss:[ebp-114]              |
004049CF | 68 A0000000          | push A0                                     |
004049D4 | 68 542B4000          | push bjcm40a.402B54                         |
004049D9 | 52                   | push edx                                    |
004049DA | 50                   | push eax                                    |
004049DB | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
004049E1 | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
004049E3 | 56                   | push esi                                    |
004049E4 | FF90 08030000        | call dword ptr ds:[eax+308]                 |
004049EA | 8D4D 88              | lea ecx,dword ptr ss:[ebp-78]               |
004049ED | 50                   | push eax                                    |
004049EE | 51                   | push ecx                                    |
004049EF | FF15 40104000        | call dword ptr ds:[<__vbaObjSet>]           |
004049F5 | 8B10                 | mov edx,dword ptr ds:[eax]                  |
004049F7 | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]               |
004049FA | 51                   | push ecx                                    |
004049FB | 50                   | push eax                                    |
004049FC | 8985 E4FEFFFF        | mov dword ptr ss:[ebp-11C],eax              | [ebp-11C]:_PeekMessageA@20+1F1
00404A02 | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
00404A08 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
00404A0A | DBE2                 | fnclex                                      |
00404A0C | 7D 18                | jge bjcm40a.404A26                          |
00404A0E | 8B95 E4FEFFFF        | mov edx,dword ptr ss:[ebp-11C]              | [ebp-11C]:_PeekMessageA@20+1F1
00404A14 | 68 A0000000          | push A0                                     |
00404A19 | 68 542B4000          | push bjcm40a.402B54                         |
00404A1E | 52                   | push edx                                    |
00404A1F | 50                   | push eax                                    |
00404A20 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404A26 | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404A28 | 56                   | push esi                                    |
00404A29 | FF90 08030000        | call dword ptr ds:[eax+308]                 |
00404A2F | 8D4D 84              | lea ecx,dword ptr ss:[ebp-7C]               |
00404A32 | 50                   | push eax                                    |
00404A33 | 51                   | push ecx                                    |
00404A34 | FF15 40104000        | call dword ptr ds:[<__vbaObjSet>]           |
00404A3A | 8B10                 | mov edx,dword ptr ds:[eax]                  |
00404A3C | 8D4D 98              | lea ecx,dword ptr ss:[ebp-68]               |
00404A3F | 51                   | push ecx                                    |
00404A40 | 50                   | push eax                                    |
00404A41 | 8985 DCFEFFFF        | mov dword ptr ss:[ebp-124],eax              |
00404A47 | FF92 A0000000        | call dword ptr ds:[edx+A0]                  |
00404A4D | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
00404A4F | DBE2                 | fnclex                                      |
00404A51 | 7D 18                | jge bjcm40a.404A6B                          |
00404A53 | 8B95 DCFEFFFF        | mov edx,dword ptr ss:[ebp-124]              |
00404A59 | 68 A0000000          | push A0                                     |
00404A5E | 68 542B4000          | push bjcm40a.402B54                         |
00404A63 | 52                   | push edx                                    |
00404A64 | 50                   | push eax                                    |
00404A65 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404A6B | 8B3D 5C104000        | mov edi,dword ptr ds:[<Ordinal#631>]        | edi:_PeekMessageA@20
00404A71 | B8 02000000          | mov eax,2                                   |
00404A76 | B9 01000000          | mov ecx,1                                   |
00404A7B | 8985 74FFFFFF        | mov dword ptr ss:[ebp-8C],eax               | [ebp-8C]:_PeekMessageA@20+1F1
00404A81 | 8985 64FFFFFF        | mov dword ptr ss:[ebp-9C],eax               |
00404A87 | 8985 54FFFFFF        | mov dword ptr ss:[ebp-AC],eax               |
00404A8D | 8D85 64FFFFFF        | lea eax,dword ptr ss:[ebp-9C]               |
00404A93 | 898D 7CFFFFFF        | mov dword ptr ss:[ebp-84],ecx               |
00404A99 | 898D 6CFFFFFF        | mov dword ptr ss:[ebp-94],ecx               |
00404A9F | 898D 5CFFFFFF        | mov dword ptr ss:[ebp-A4],ecx               |
00404AA5 | 8B4D A0              | mov ecx,dword ptr ss:[ebp-60]               |
00404AA8 | 50                   | push eax                                    |
00404AA9 | 6A 03                | push 3                                      |
00404AAB | 51                   | push ecx                                    |
00404AAC | FFD7                 | call edi                                    | serial[2]
00404AAE | 8BD0                 | mov edx,eax                                 |
00404AB0 | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               |
00404AB3 | FFD3                 | call ebx                                    |
00404AB5 | 50                   | push eax                                    |
00404AB6 | FF15 28104000        | call dword ptr ds:[<Ordinal#516>]           | asc(serial...)
00404ABC | 8B4D A8              | mov ecx,dword ptr ss:[ebp-58]               |
00404ABF | 66:8BD0              | mov dx,ax                                   |
00404AC2 | 8D85 74FFFFFF        | lea eax,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404AC8 | 66:8995 48FEFFFF     | mov word ptr ss:[ebp-1B8],dx                |
00404ACF | 50                   | push eax                                    |
00404AD0 | 6A 02                | push 2                                      |
00404AD2 | 51                   | push ecx                                    |
00404AD3 | FFD7                 | call edi                                    | serial[1]
00404AD5 | 8BD0                 | mov edx,eax                                 |
00404AD7 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404ADA | FFD3                 | call ebx                                    |
00404ADC | 50                   | push eax                                    |
00404ADD | FF15 28104000        | call dword ptr ds:[<Ordinal#516>]           | asc(serial...)
00404AE3 | 66:8BBD 48FEFFFF     | mov di,word ptr ss:[ebp-1B8]                |
00404AEA | 8D95 54FFFFFF        | lea edx,dword ptr ss:[ebp-AC]               |
00404AF0 | 66:03F8              | add di,ax                                   | serial[1]+serial[2]
00404AF3 | 8B45 98              | mov eax,dword ptr ss:[ebp-68]               |
00404AF6 | 52                   | push edx                                    |
00404AF7 | 6A 04                | push 4                                      |
00404AF9 | 50                   | push eax                                    |
00404AFA | 0F80 46040000        | jo <bjcm40a.ErrOverflow>                    |
00404B00 | FF15 5C104000        | call dword ptr ds:[<Ordinal#631>]           | serial[3]
00404B06 | 8BD0                 | mov edx,eax                                 |
00404B08 | 8D4D 94              | lea ecx,dword ptr ss:[ebp-6C]               |
00404B0B | FFD3                 | call ebx                                    |
00404B0D | 50                   | push eax                                    |
00404B0E | FF15 28104000        | call dword ptr ds:[<Ordinal#516>]           | asc(serial...)
00404B14 | 66:03F8              | add di,ax                                   | serial[1]+2+3
00404B17 | 8D8D 44FFFFFF        | lea ecx,dword ptr ss:[ebp-BC]               |
00404B1D | 0F80 23040000        | jo <bjcm40a.ErrOverflow>                    |
00404B23 | 51                   | push ecx                                    |
00404B24 | 66:89BD 4CFFFFFF     | mov word ptr ss:[ebp-B4],di                 |
00404B2B | C785 44FFFFFF 020000 | mov dword ptr ss:[ebp-BC],2                 |
00404B35 | FF15 B0104000        | call dword ptr ds:[<Ordinal#572>]           | hex(s...)
00404B3B | 8BD0                 | mov edx,eax                                 |
00404B3D | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404B40 | FFD3                 | call ebx                                    |
00404B42 | 8D55 94              | lea edx,dword ptr ss:[ebp-6C]               |
00404B45 | 8D45 98              | lea eax,dword ptr ss:[ebp-68]               |
00404B48 | 52                   | push edx                                    |
00404B49 | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               |
00404B4C | 50                   | push eax                                    |
00404B4D | 8D55 A0              | lea edx,dword ptr ss:[ebp-60]               |
00404B50 | 51                   | push ecx                                    |
00404B51 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               |
00404B54 | 52                   | push edx                                    |
00404B55 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404B58 | 50                   | push eax                                    |
00404B59 | 51                   | push ecx                                    |
00404B5A | 6A 06                | push 6                                      |
00404B5C | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
00404B62 | 8D55 84              | lea edx,dword ptr ss:[ebp-7C]               |
00404B65 | 8D45 88              | lea eax,dword ptr ss:[ebp-78]               |
00404B68 | 52                   | push edx                                    |
00404B69 | 8D4D 8C              | lea ecx,dword ptr ss:[ebp-74]               | [ebp-74]:_PeekMessageA@20
00404B6C | 50                   | push eax                                    |
00404B6D | 51                   | push ecx                                    |
00404B6E | 6A 03                | push 3                                      |
00404B70 | FF15 24104000        | call dword ptr ds:[<__vbaFreeObjList>]      |
00404B76 | 8D95 44FFFFFF        | lea edx,dword ptr ss:[ebp-BC]               |
00404B7C | 52                   | push edx                                    |
00404B7D | 8D85 54FFFFFF        | lea eax,dword ptr ss:[ebp-AC]               |
00404B83 | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]               |
00404B89 | 50                   | push eax                                    |
00404B8A | 8D95 74FFFFFF        | lea edx,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404B90 | 51                   | push ecx                                    |
00404B91 | 52                   | push edx                                    |
00404B92 | 6A 04                | push 4                                      |
00404B94 | FF15 18104000        | call dword ptr ds:[<__vbaFreeVarList>]      |
00404B9A | 8B3D B8104000        | mov edi,dword ptr ds:[<__vbaStrCopy>]       | edi:_PeekMessageA@20
00404BA0 | 83C4 40              | add esp,40                                  |
00404BA3 | BA 6C294000          | mov edx,bjcm40a.40296C                      | 40296C:L"SHR"
00404BA8 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404BAB | FFD7                 | call edi                                    | edi:_PeekMessageA@20
00404BAD | BA 842B4000          | mov edx,bjcm40a.402B84                      | 6
00404BB2 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404BB5 | FFD7                 | call edi                                    | edi:_PeekMessageA@20
00404BB7 | 8B06                 | mov eax,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404BB9 | 8D4D A0              | lea ecx,dword ptr ss:[ebp-60]               |
00404BBC | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]               |
00404BBF | 51                   | push ecx                                    |
00404BC0 | 52                   | push edx                                    |
00404BC1 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404BC4 | 8D55 D8              | lea edx,dword ptr ss:[ebp-28]               |
00404BC7 | 51                   | push ecx                                    |
00404BC8 | 52                   | push edx                                    |
00404BC9 | 56                   | push esi                                    |
00404BCA | FF90 F8060000        | call dword ptr ds:[eax+6F8]                 | pre_result SHR 6
00404BD0 | 33FF                 | xor edi,edi                                 | edi:_PeekMessageA@20
00404BD2 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
00404BD4 | 7D 12                | jge bjcm40a.404BE8                          |
00404BD6 | 68 F8060000          | push 6F8                                    |
00404BDB | 68 DC274000          | push bjcm40a.4027DC                         |
00404BE0 | 56                   | push esi                                    |
00404BE1 | 50                   | push eax                                    |
00404BE2 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404BE8 | 8B55 A0              | mov edx,dword ptr ss:[ebp-60]               |
00404BEB | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404BEE | 897D A0              | mov dword ptr ss:[ebp-60],edi               | edi:_PeekMessageA@20
00404BF1 | FFD3                 | call ebx                                    |
00404BF3 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               |
00404BF6 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404BF9 | 50                   | push eax                                    |
00404BFA | 51                   | push ecx                                    |
00404BFB | 6A 02                | push 2                                      |
00404BFD | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
00404C03 | 83C4 0C              | add esp,C                                   |
00404C06 | BA 94294000          | mov edx,bjcm40a.402994                      | *
00404C0B | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404C0E | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404C14 | BA 8C2B4000          | mov edx,bjcm40a.402B8C                      | 20
00404C19 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404C1C | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404C22 | 8B16                 | mov edx,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404C24 | 8D45 A0              | lea eax,dword ptr ss:[ebp-60]               |
00404C27 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404C2A | 50                   | push eax                                    |
00404C2B | 51                   | push ecx                                    |
00404C2C | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404C2F | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404C32 | 50                   | push eax                                    |
00404C33 | 51                   | push ecx                                    |
00404C34 | 56                   | push esi                                    |
00404C35 | FF92 F8060000        | call dword ptr ds:[edx+6F8]                 | pre_result * 20 = v2
00404C3B | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
00404C3D | 7D 12                | jge bjcm40a.404C51                          |
00404C3F | 68 F8060000          | push 6F8                                    |
00404C44 | 68 DC274000          | push bjcm40a.4027DC                         |
00404C49 | 56                   | push esi                                    |
00404C4A | 50                   | push eax                                    |
00404C4B | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404C51 | 8B55 A0              | mov edx,dword ptr ss:[ebp-60]               |
00404C54 | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404C57 | 897D A0              | mov dword ptr ss:[ebp-60],edi               | edi:_PeekMessageA@20
00404C5A | FFD3                 | call ebx                                    |
00404C5C | 8D55 A4              | lea edx,dword ptr ss:[ebp-5C]               |
00404C5F | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404C62 | 52                   | push edx                                    |
00404C63 | 50                   | push eax                                    |
00404C64 | 6A 02                | push 2                                      |
00404C66 | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
00404C6C | 8B4D D4              | mov ecx,dword ptr ss:[ebp-2C]               |
00404C6F | 83C4 0C              | add esp,C                                   |
00404C72 | 6A 03                | push 3                                      |
00404C74 | 51                   | push ecx                                    |
00404C75 | FF15 E8104000        | call dword ptr ds:[<Ordinal#618>]           | right(v1,3)
00404C7B | 8BD0                 | mov edx,eax                                 |
00404C7D | 8D4D 9C              | lea ecx,dword ptr ss:[ebp-64]               |
00404C80 | FFD3                 | call ebx                                    |
00404C82 | BA 34294000          | mov edx,bjcm40a.402934                      | =
00404C87 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404C8A | FF15 B8104000        | call dword ptr ds:[<__vbaStrCopy>]          |
00404C90 | 8B55 9C              | mov edx,dword ptr ss:[ebp-64]               |
00404C93 | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404C96 | 897D 9C              | mov dword ptr ss:[ebp-64],edi               | edi:_PeekMessageA@20
00404C99 | FFD3                 | call ebx                                    |
00404C9B | 8B16                 | mov edx,dword ptr ds:[esi]                  | [esi]:rtcCommandBstr+78
00404C9D | 8D45 A0              | lea eax,dword ptr ss:[ebp-60]               |
00404CA0 | 8D4D A4              | lea ecx,dword ptr ss:[ebp-5C]               |
00404CA3 | 50                   | push eax                                    |
00404CA4 | 51                   | push ecx                                    |
00404CA5 | 8D45 A8              | lea eax,dword ptr ss:[ebp-58]               |
00404CA8 | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404CAB | 50                   | push eax                                    |
00404CAC | 51                   | push ecx                                    |
00404CAD | 56                   | push esi                                    |
00404CAE | FF92 F8060000        | call dword ptr ds:[edx+6F8]                 | v1 == v2?
00404CB4 | 3BC7                 | cmp eax,edi                                 | edi:_PeekMessageA@20
00404CB6 | 7D 12                | jge bjcm40a.404CCA                          |
00404CB8 | 68 F8060000          | push 6F8                                    |
00404CBD | 68 DC274000          | push bjcm40a.4027DC                         |
00404CC2 | 56                   | push esi                                    |
00404CC3 | 50                   | push eax                                    |
00404CC4 | FF15 30104000        | call dword ptr ds:[<__vbaHresultCheckObj>]  |
00404CCA | 8B55 A0              | mov edx,dword ptr ss:[ebp-60]               |
00404CCD | 8D4D D8              | lea ecx,dword ptr ss:[ebp-28]               |
00404CD0 | 897D A0              | mov dword ptr ss:[ebp-60],edi               | edi:_PeekMessageA@20
00404CD3 | FFD3                 | call ebx                                    |
00404CD5 | 8D55 9C              | lea edx,dword ptr ss:[ebp-64]               |
00404CD8 | 8D45 A4              | lea eax,dword ptr ss:[ebp-5C]               |
00404CDB | 52                   | push edx                                    |
00404CDC | 8D4D A8              | lea ecx,dword ptr ss:[ebp-58]               |
00404CDF | 50                   | push eax                                    |
00404CE0 | 51                   | push ecx                                    |
00404CE1 | 6A 03                | push 3                                      |
00404CE3 | FF15 BC104000        | call dword ptr ds:[<__vbaFreeStrList>]      |
00404CE9 | 8B55 D8              | mov edx,dword ptr ss:[ebp-28]               |
00404CEC | 83C4 10              | add esp,10                                  |
00404CEF | 52                   | push edx                                    | 前面比较的结果（0或FFFF）
00404CF0 | 68 982B4000          | push bjcm40a.402B98                         | 402B98:L"FFFF"
00404CF5 | FF15 70104000        | call dword ptr ds:[<__vbaStrCmp>]           |
00404CFB | 85C0                 | test eax,eax                                |
00404CFD | 0F85 AD000000        | jne <bjcm40a.FAIL>                          |
00404D03 | 8B1D D4104000        | mov ebx,dword ptr ds:[<__vbaVarDup>]        | Success
00404D09 | B9 04000280          | mov ecx,80020004                            |
00404D0E | 898D 4CFFFFFF        | mov dword ptr ss:[ebp-B4],ecx               |
00404D14 | B8 0A000000          | mov eax,A                                   | 0A:'\n'
00404D19 | 898D 5CFFFFFF        | mov dword ptr ss:[ebp-A4],ecx               |
00404D1F | BE 08000000          | mov esi,8                                   |
00404D24 | 8D95 14FFFFFF        | lea edx,dword ptr ss:[ebp-EC]               |
00404D2A | 8D8D 64FFFFFF        | lea ecx,dword ptr ss:[ebp-9C]               |
00404D30 | 8985 44FFFFFF        | mov dword ptr ss:[ebp-BC],eax               |
00404D36 | 8985 54FFFFFF        | mov dword ptr ss:[ebp-AC],eax               |
00404D3C | C785 1CFFFFFF F42B40 | mov dword ptr ss:[ebp-E4],bjcm40a.402BF4    | 402BF4:L"Correct serial!"
00404D46 | 89B5 14FFFFFF        | mov dword ptr ss:[ebp-EC],esi               |
00404D4C | FFD3                 | call ebx                                    |
00404D4E | 8D95 24FFFFFF        | lea edx,dword ptr ss:[ebp-DC]               | [ebp-DC]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00404D54 | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404D5A | C785 2CFFFFFF A82B40 | mov dword ptr ss:[ebp-D4],bjcm40a.402BA8    | 402BA8:L"Good job, tell me how you do that!"
00404D64 | 89B5 24FFFFFF        | mov dword ptr ss:[ebp-DC],esi               | [ebp-DC]:int __stdcall _PeekMessage(struct tagMSG *, struct HWND__*, unsigned int, unsigned int, unsigned int, unsigned int, int)+EB
00404D6A | FFD3                 | call ebx                                    |
00404D6C | 8D85 44FFFFFF        | lea eax,dword ptr ss:[ebp-BC]               |
00404D72 | 8D8D 54FFFFFF        | lea ecx,dword ptr ss:[ebp-AC]               |
00404D78 | 50                   | push eax                                    |
00404D79 | 8D95 64FFFFFF        | lea edx,dword ptr ss:[ebp-9C]               |
00404D7F | 51                   | push ecx                                    |
00404D80 | 52                   | push edx                                    |
00404D81 | 8D85 74FFFFFF        | lea eax,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404D87 | 57                   | push edi                                    | edi:_PeekMessageA@20
00404D88 | 50                   | push eax                                    |
00404D89 | FF15 3C104000        | call dword ptr ds:[<Ordinal#595>]           |
00404D8F | 8D8D 44FFFFFF        | lea ecx,dword ptr ss:[ebp-BC]               |
00404D95 | 8D95 54FFFFFF        | lea edx,dword ptr ss:[ebp-AC]               |
00404D9B | 51                   | push ecx                                    |
00404D9C | 8D85 64FFFFFF        | lea eax,dword ptr ss:[ebp-9C]               |
00404DA2 | 52                   | push edx                                    |
00404DA3 | 8D8D 74FFFFFF        | lea ecx,dword ptr ss:[ebp-8C]               | [ebp-8C]:_PeekMessageA@20+1F1
00404DA9 | 50                   | push eax                                    |
00404DAA | 51                   | push ecx                                    |
00404DAB | E9 A8000000          | jmp bjcm40a.404E58                          |
00404DB0 | 8B1D D4104000        | mov ebx,dword ptr ds:[<__vbaVarDup>]        |
```


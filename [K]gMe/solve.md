累死，算个serial真tm麻烦

没有什么示例的serial组合，因为是根据分区名/文件系统/分区序列号计算的

没有整理好的计算思路，看下面乱的一批的笔记或者keygen源码（没有彻底测试，应该没有bug）

```
BOOL GetVolumeInformationA(
  [in, optional]  LPCSTR  lpRootPathName,               程序所在盘符
  [out, optional] LPSTR   lpVolumeNameBuffer,
  [in]            DWORD   nVolumeNameSize,
  [out, optional] LPDWORD lpVolumeSerialNumber,
  [out, optional] LPDWORD lpMaximumComponentLength,
  [out, optional] LPDWORD lpFileSystemFlags,
  [out, optional] LPSTR   lpFileSystemNameBuffer,
  [in]            DWORD   nFileSystemNameSize
);

0019F1A8  006BE44C    "C:\\"
0019F1AC  006BD14C    out VolumeName                system
0019F1B0  000000FF    
0019F1B4  0019F354    out VolumeSerialNumber        6E762381
0019F1B8  0019F250    out MaximumComponentLength    FF
0019F1BC  0019F24C    out FileSystemFlags           FF2EE703/ 03E72EFF
0019F1C0  006BD36C    out FileSystemName            NTFS
0019F1C4  000000FF    

void GetSystemInfo(
  [out] LPSYSTEM_INFO lpSystemInfo
);

typedef struct _SYSTEM_INFO {
  union {
    DWORD dwOemId;
    struct {
      WORD wProcessorArchitecture;
      WORD wReserved;
    } DUMMYSTRUCTNAME;
  } DUMMYUNIONNAME;
  DWORD     dwPageSize;                   
  LPVOID    lpMinimumApplicationAddress;
  LPVOID    lpMaximumApplicationAddress;
  DWORD_PTR dwActiveProcessorMask;
  DWORD     dwNumberOfProcessors;
  DWORD     dwProcessorType;
  DWORD     dwAllocationGranularity;
  WORD      wProcessorLevel;
  WORD      wProcessorRevision;
} SYSTEM_INFO, *LPSYSTEM_INFO;

C:\work>testC.exe
wProcessorArchitecture: 0
wReserved: 0
dwPageSize: 4096
lpMinimumApplicationAddress: 00010000
lpMaximumApplicationAddress: 7FFEFFFF
dwActiveProcessorMask: 0000000F
dwNumberOfProcessors: 4
dwProcessorType: 586
dwAllocationGranularity: 65536
wProcessorLevel: 6
wProcessorRevision: 42242

Raw byte data of SYSTEM_INFO structure:
00 00 00 00 [00 10 00 00] [00 00 01 00] [FF FF FE 7F]
[0F 00 00 00] [04 00 00 00] [4A 02 00 00] [00 00 01 00]
[06 00] [02 A5]


Form1.CalcSerial(name, serial, VolumeName, FileSystemName, VolumeSerialNumber, dwProcessorType, lpMinimumApplicationAddress, lpMaximumApplicationAddress / 0xFF, dwAllocationGranularity, wProcessorLevel + wProcessorRevision, dwNumberOfProcessors)


0019F19C  0019F2F8    &L"chenx221"  name
0019F1A0  0019F2F4    &L"123456"    serial
0019F1A4  0019F308    &L"system"    VolumeName
0019F1A8  0019F30C    &L"NTFS"      FileSystemName
0019F1AC  0019F354    6E762381      VolumeSerialNumber
0019F1B0  0019F350    &L"24A"       dwProcessorType
0019F1B4  0019F34C    &L"10000"     lpMinimumApplicationAddress
0019F1B8  0019F348    &L"807F7F"    lpMaximumApplicationAddress / 0xFF
0019F1BC  0019F344    &L"10000"     dwAllocationGranularity
0019F1C0  0019F340    &L"A5020006"  wProcessorLevel + wProcessorRevision
-         -           4             dwNumberOfProcessors

EB or VolumeName

24A->A42 dwProcessorType

Hex2Dec(VolumeSerialNumber) 1853236097

1+7
8+7
5+7
3+7
2+7

42 + hex((1+7) * dwNumberOfProcessors)  // a+b,a依次下来 5个，b始终是末尾，42是固定开头

42 [20 3C 30 28 24]

42 20 3C 30 28 24 + A42

[c][h]enx2[2][1] // Name

hex(asc('c')+asc('1')) + hex(asc('h')+asc('2')) 94 9A

94 9A + 42 20 3C 30 28 24 A42

"[10]000", "[80]7F7F" //lpMinimumApplicationAddress, lpMaximumApplicationAddress / 0xFF

&H + 10 => 0x10

&H + 80 => 0x80

0x80 - 0x10 = 0x70

94 9A + 42 20 3C 30 28 24 + A42 + 70

hex(asc('E')+asc('B')) 69+66 = 0x87 //或者system前两位

94 9A + 42 20 3C 30 28 24 + A42 + 70 + 87

[N]TF[S]

hex(asc('N')+asc('S')) 0xA1 * dwNumberOfProcessors = 0x284

serial: UpperCase(284 + 94 9A + 42 20 3C 30 28 24 + A42 + 70 + 87)

```

细节：

```assembly
0040E8B0 | 55               | push ebp                                 |
0040E8B1 | 8BEC             | mov ebp,esp                              |
0040E8B3 | 83EC 18          | sub esp,18                               |
0040E8B6 | 68 C6134000      | push <JMP.&__vbaExceptHandler>           |
0040E8BB | 64:A1 00000000   | mov eax,dword ptr fs:[0]                 |
0040E8C1 | 50               | push eax                                 |
0040E8C2 | 64:8925 00000000 | mov dword ptr fs:[0],esp                 |
0040E8C9 | B8 84010000      | mov eax,184                              |
0040E8CE | E8 ED2AFFFF      | call <JMP.&__vbaChkstk>                  |
0040E8D3 | 53               | push ebx                                 |
0040E8D4 | 56               | push esi                                 |
0040E8D5 | 57               | push edi                                 | edi:&L"MM d dddd"
0040E8D6 | 8965 E8          | mov dword ptr ss:[ebp-18],esp            |
0040E8D9 | C745 EC 30114000 | mov dword ptr ss:[ebp-14],[k]gme.401130  |
0040E8E0 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040E8E3 | 83E0 01          | and eax,1                                |
0040E8E6 | 8945 F0          | mov dword ptr ss:[ebp-10],eax            | [ebp-10]:&L"MM d dddd"
0040E8E9 | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040E8EC | 83E1 FE          | and ecx,FFFFFFFE                         |
0040E8EF | 894D 08          | mov dword ptr ss:[ebp+8],ecx             |
0040E8F2 | C745 F4 00000000 | mov dword ptr ss:[ebp-C],0               |
0040E8F9 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040E8FC | 8B02             | mov eax,dword ptr ds:[edx]               |
0040E8FE | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040E901 | 51               | push ecx                                 |
0040E902 | FF50 04          | call dword ptr ds:[eax+4]                |
0040E905 | C745 FC 01000000 | mov dword ptr ss:[ebp-4],1               |
0040E90C | C745 FC 02000000 | mov dword ptr ss:[ebp-4],2               |
0040E913 | 6A FF            | push FFFFFFFF                            |
0040E915 | FF15 40104000    | call dword ptr ds:[<__vbaOnError>]       |
0040E91B | C745 FC 03000000 | mov dword ptr ss:[ebp-4],3               |
0040E922 | C785 64FFFFFF 04 | mov dword ptr ss:[ebp-9C],80020004       | [ebp-9C]:&L"MM d dddd"
0040E92C | C785 5CFFFFFF 0A | mov dword ptr ss:[ebp-A4],A              | 0A:'\n'
0040E936 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040E93C | 52               | push edx                                 |
0040E93D | FF15 3C104000    | call dword ptr ds:[<Ordinal#593>]        |
0040E943 | D99D E4FEFFFF    | fstp dword ptr ss:[ebp-11C]              |
0040E949 | C785 DCFEFFFF 04 | mov dword ptr ss:[ebp-124],4             |
0040E953 | C785 54FFFFFF 04 | mov dword ptr ss:[ebp-AC],80020004       |
0040E95D | C785 4CFFFFFF 0A | mov dword ptr ss:[ebp-B4],A              | [ebp-B4]:SendDlgItemMessageW+F94, 0A:'\n'
0040E967 | 8D85 4CFFFFFF    | lea eax,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040E96D | 50               | push eax                                 |
0040E96E | FF15 3C104000    | call dword ptr ds:[<Ordinal#593>]        |
0040E974 | D99D 44FFFFFF    | fstp dword ptr ss:[ebp-BC]               |
0040E97A | C785 3CFFFFFF 04 | mov dword ptr ss:[ebp-C4],4              |
0040E984 | 8D8D 3CFFFFFF    | lea ecx,dword ptr ss:[ebp-C4]            |
0040E98A | 51               | push ecx                                 |
0040E98B | 8D95 2CFFFFFF    | lea edx,dword ptr ss:[ebp-D4]            |
0040E991 | 52               | push edx                                 |
0040E992 | FF15 D0104000    | call dword ptr ds:[<Ordinal#573>]        |
0040E998 | 8D85 DCFEFFFF    | lea eax,dword ptr ss:[ebp-124]           |
0040E99E | 50               | push eax                                 |
0040E99F | 8D8D 2CFFFFFF    | lea ecx,dword ptr ss:[ebp-D4]            |
0040E9A5 | 51               | push ecx                                 |
0040E9A6 | 8D95 1CFFFFFF    | lea edx,dword ptr ss:[ebp-E4]            | [ebp-E4]:PeekMessageW+2BB
0040E9AC | 52               | push edx                                 |
0040E9AD | FF15 EC104000    | call dword ptr ds:[<__vbaVarAdd>]        |
0040E9B3 | 50               | push eax                                 |
0040E9B4 | FF15 E8104000    | call dword ptr ds:[<__vbaI4Var>]         |
0040E9BA | 8945 C0          | mov dword ptr ss:[ebp-40],eax            |
0040E9BD | 8D85 1CFFFFFF    | lea eax,dword ptr ss:[ebp-E4]            | [ebp-E4]:PeekMessageW+2BB
0040E9C3 | 50               | push eax                                 |
0040E9C4 | 8D8D 2CFFFFFF    | lea ecx,dword ptr ss:[ebp-D4]            |
0040E9CA | 51               | push ecx                                 |
0040E9CB | 8D95 3CFFFFFF    | lea edx,dword ptr ss:[ebp-C4]            |
0040E9D1 | 52               | push edx                                 |
0040E9D2 | 8D85 4CFFFFFF    | lea eax,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040E9D8 | 50               | push eax                                 |
0040E9D9 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040E9DF | 51               | push ecx                                 |
0040E9E0 | 6A 05            | push 5                                   |
0040E9E2 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040E9E8 | 83C4 18          | add esp,18                               |
0040E9EB | C745 FC 04000000 | mov dword ptr ss:[ebp-4],4               |
0040E9F2 | C785 64FFFFFF 04 | mov dword ptr ss:[ebp-9C],80020004       | [ebp-9C]:&L"MM d dddd"
0040E9FC | C785 5CFFFFFF 0A | mov dword ptr ss:[ebp-A4],A              | 0A:'\n'
0040EA06 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040EA0C | 52               | push edx                                 |
0040EA0D | FF15 3C104000    | call dword ptr ds:[<Ordinal#593>]        |
0040EA13 | D99D D8FEFFFF    | fstp dword ptr ss:[ebp-128]              |
0040EA19 | DB45 C0          | fild dword ptr ss:[ebp-40]               |
0040EA1C | DD9D 98FEFFFF    | fstp qword ptr ss:[ebp-168]              |
0040EA22 | D985 D8FEFFFF    | fld dword ptr ss:[ebp-128]               |
0040EA28 | DCAD 98FEFFFF    | fsubr qword ptr ss:[ebp-168]             |
0040EA2E | DFE0             | fnstsw ax                                |
0040EA30 | A8 0D            | test al,D                                |
0040EA32 | 0F85 C90E0000    | jne [k]gme.40F901                        |
0040EA38 | FF15 F8104000    | call dword ptr ds:[<__vbaFpI4>]          |
0040EA3E | 8945 BC          | mov dword ptr ss:[ebp-44],eax            | [ebp-44]:EndDialog+439
0040EA41 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040EA47 | FF15 08104000    | call dword ptr ds:[<__vbaFreeVar>]       |
0040EA4D | C745 FC 05000000 | mov dword ptr ss:[ebp-4],5               |
0040EA54 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040EA57 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040EA59 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040EA5C | 52               | push edx                                 |
0040EA5D | FF91 14030000    | call dword ptr ds:[ecx+314]              |
0040EA63 | 50               | push eax                                 |
0040EA64 | 8D85 70FFFFFF    | lea eax,dword ptr ss:[ebp-90]            |
0040EA6A | 50               | push eax                                 |
0040EA6B | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040EA71 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040EA77 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040EA7A | 51               | push ecx                                 |
0040EA7B | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EA81 | 8B02             | mov eax,dword ptr ds:[edx]               |
0040EA83 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040EA89 | 51               | push ecx                                 |
0040EA8A | FF90 A0000000    | call dword ptr ds:[eax+A0]               |
0040EA90 | DBE2             | fnclex                                   |
0040EA92 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040EA98 | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040EA9F | 7D 26            | jge [k]gme.40EAC7                        |
0040EAA1 | 68 A0000000      | push A0                                  |
0040EAA6 | 68 F8D64000      | push [k]gme.40D6F8                       |
0040EAAB | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EAB1 | 52               | push edx                                 |
0040EAB2 | 8B85 CCFEFFFF    | mov eax,dword ptr ss:[ebp-134]           |
0040EAB8 | 50               | push eax                                 |
0040EAB9 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040EABF | 8985 94FEFFFF    | mov dword ptr ss:[ebp-16C],eax           | [ebp-16C]:rtcExp+16DE
0040EAC5 | EB 0A            | jmp [k]gme.40EAD1                        |
0040EAC7 | C785 94FEFFFF 00 | mov dword ptr ss:[ebp-16C],0             | [ebp-16C]:rtcExp+16DE
0040EAD1 | 8B4D 8C          | mov ecx,dword ptr ss:[ebp-74]            | name
0040EAD4 | 51               | push ecx                                 |
0040EAD5 | FF15 28104000    | call dword ptr ds:[<Ordinal#519>]        | Trim
0040EADB | 8BD0             | mov edx,eax                              |
0040EADD | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040EAE0 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040EAE6 | 50               | push eax                                 |
0040EAE7 | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
0040EAED | 8945 C0          | mov dword ptr ss:[ebp-40],eax            | len(name)
0040EAF0 | 8D55 88          | lea edx,dword ptr ss:[ebp-78]            |
0040EAF3 | 52               | push edx                                 |
0040EAF4 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040EAF7 | 50               | push eax                                 |
0040EAF8 | 6A 02            | push 2                                   |
0040EAFA | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040EB00 | 83C4 0C          | add esp,C                                |
0040EB03 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EB09 | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040EB0F | C745 FC 06000000 | mov dword ptr ss:[ebp-4],6               |
0040EB16 | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040EB19 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040EB1B | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040EB1E | 50               | push eax                                 |
0040EB1F | FF92 10030000    | call dword ptr ds:[edx+310]              |
0040EB25 | 50               | push eax                                 |
0040EB26 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EB2C | 51               | push ecx                                 |
0040EB2D | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040EB33 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040EB39 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040EB3C | 52               | push edx                                 |
0040EB3D | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040EB43 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040EB45 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EB4B | 52               | push edx                                 |
0040EB4C | FF91 A0000000    | call dword ptr ds:[ecx+A0]               |
0040EB52 | DBE2             | fnclex                                   |
0040EB54 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040EB5A | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040EB61 | 7D 26            | jge [k]gme.40EB89                        |
0040EB63 | 68 A0000000      | push A0                                  |
0040EB68 | 68 F8D64000      | push [k]gme.40D6F8                       |
0040EB6D | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040EB73 | 50               | push eax                                 |
0040EB74 | 8B8D CCFEFFFF    | mov ecx,dword ptr ss:[ebp-134]           |
0040EB7A | 51               | push ecx                                 |
0040EB7B | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040EB81 | 8985 90FEFFFF    | mov dword ptr ss:[ebp-170],eax           |
0040EB87 | EB 0A            | jmp [k]gme.40EB93                        |
0040EB89 | C785 90FEFFFF 00 | mov dword ptr ss:[ebp-170],0             |
0040EB93 | 8B55 8C          | mov edx,dword ptr ss:[ebp-74]            | serial
0040EB96 | 52               | push edx                                 |
0040EB97 | FF15 28104000    | call dword ptr ds:[<Ordinal#519>]        | trim
0040EB9D | 8BD0             | mov edx,eax                              |
0040EB9F | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040EBA2 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040EBA8 | 50               | push eax                                 |
0040EBA9 | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
0040EBAF | 8945 BC          | mov dword ptr ss:[ebp-44],eax            | len(serial)
0040EBB2 | 8D45 88          | lea eax,dword ptr ss:[ebp-78]            |
0040EBB5 | 50               | push eax                                 |
0040EBB6 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040EBB9 | 51               | push ecx                                 |
0040EBBA | 6A 02            | push 2                                   |
0040EBBC | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040EBC2 | 83C4 0C          | add esp,C                                |
0040EBC5 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EBCB | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040EBD1 | C745 FC 07000000 | mov dword ptr ss:[ebp-4],7               |
0040EBD8 | 837D C0 00       | cmp dword ptr ss:[ebp-40],0              | 检查name是否为空
0040EBDC | 0F85 5C010000    | jne [k]gme.40ED3E                        |
0040EBE2 | C745 FC 08000000 | mov dword ptr ss:[ebp-4],8               |
0040EBE9 | C785 34FFFFFF 04 | mov dword ptr ss:[ebp-CC],80020004       | [ebp-CC]:&L"MM d dddd"
0040EBF3 | C785 2CFFFFFF 0A | mov dword ptr ss:[ebp-D4],A              | 0A:'\n'
0040EBFD | C785 44FFFFFF 04 | mov dword ptr ss:[ebp-BC],80020004       |
0040EC07 | C785 3CFFFFFF 0A | mov dword ptr ss:[ebp-C4],A              | 0A:'\n'
0040EC11 | C785 04FFFFFF 3C | mov dword ptr ss:[ebp-FC],[k]gme.40D73C  | 40D73C:L"Error"
0040EC1B | C785 FCFEFFFF 08 | mov dword ptr ss:[ebp-104],8             |
0040EC25 | 8D95 FCFEFFFF    | lea edx,dword ptr ss:[ebp-104]           |
0040EC2B | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040EC31 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040EC37 | C785 14FFFFFF 0C | mov dword ptr ss:[ebp-EC],[k]gme.40D70C  | 40D70C:L"Enter username first!"
0040EC41 | C785 0CFFFFFF 08 | mov dword ptr ss:[ebp-F4],8              |
0040EC4B | 8D95 0CFFFFFF    | lea edx,dword ptr ss:[ebp-F4]            |
0040EC51 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040EC57 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040EC5D | 8D95 2CFFFFFF    | lea edx,dword ptr ss:[ebp-D4]            |
0040EC63 | 52               | push edx                                 |
0040EC64 | 8D85 3CFFFFFF    | lea eax,dword ptr ss:[ebp-C4]            |
0040EC6A | 50               | push eax                                 |
0040EC6B | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040EC71 | 51               | push ecx                                 |
0040EC72 | 6A 30            | push 30                                  |
0040EC74 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040EC7A | 52               | push edx                                 |
0040EC7B | FF15 48104000    | call dword ptr ds:[<Ordinal#595>]        |
0040EC81 | 8D85 2CFFFFFF    | lea eax,dword ptr ss:[ebp-D4]            |
0040EC87 | 50               | push eax                                 |
0040EC88 | 8D8D 3CFFFFFF    | lea ecx,dword ptr ss:[ebp-C4]            |
0040EC8E | 51               | push ecx                                 |
0040EC8F | 8D95 4CFFFFFF    | lea edx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040EC95 | 52               | push edx                                 |
0040EC96 | 8D85 5CFFFFFF    | lea eax,dword ptr ss:[ebp-A4]            |
0040EC9C | 50               | push eax                                 |
0040EC9D | 6A 04            | push 4                                   |
0040EC9F | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040ECA5 | 83C4 14          | add esp,14                               |
0040ECA8 | C745 FC 09000000 | mov dword ptr ss:[ebp-4],9               | 09:'\t'
0040ECAF | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040ECB2 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040ECB4 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040ECB7 | 50               | push eax                                 |
0040ECB8 | FF92 14030000    | call dword ptr ds:[edx+314]              |
0040ECBE | 50               | push eax                                 |
0040ECBF | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040ECC5 | 51               | push ecx                                 |
0040ECC6 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040ECCC | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040ECD2 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040ECD8 | 8B02             | mov eax,dword ptr ds:[edx]               |
0040ECDA | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040ECE0 | 51               | push ecx                                 |
0040ECE1 | FF90 04020000    | call dword ptr ds:[eax+204]              |
0040ECE7 | DBE2             | fnclex                                   |
0040ECE9 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040ECEF | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040ECF6 | 7D 26            | jge [k]gme.40ED1E                        |
0040ECF8 | 68 04020000      | push 204                                 |
0040ECFD | 68 F8D64000      | push [k]gme.40D6F8                       |
0040ED02 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040ED08 | 52               | push edx                                 |
0040ED09 | 8B85 CCFEFFFF    | mov eax,dword ptr ss:[ebp-134]           |
0040ED0F | 50               | push eax                                 |
0040ED10 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040ED16 | 8985 8CFEFFFF    | mov dword ptr ss:[ebp-174],eax           |
0040ED1C | EB 0A            | jmp [k]gme.40ED28                        |
0040ED1E | C785 8CFEFFFF 00 | mov dword ptr ss:[ebp-174],0             |
0040ED28 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040ED2E | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040ED34 | E9 DF0A0000      | jmp [k]gme.40F818                        |
0040ED39 | E9 86020000      | jmp [k]gme.40EFC4                        |
0040ED3E | C745 FC 0B000000 | mov dword ptr ss:[ebp-4],B               | 0B:'\v'
0040ED45 | 837D C0 04       | cmp dword ptr ss:[ebp-40],4              | 检查name是否满足最低4位长度要求
0040ED49 | 0F8D 75020000    | jge [k]gme.40EFC4                        |
0040ED4F | C745 FC 0C000000 | mov dword ptr ss:[ebp-4],C               | 0C:'\f'
0040ED56 | C785 34FFFFFF 04 | mov dword ptr ss:[ebp-CC],80020004       | [ebp-CC]:&L"MM d dddd"
0040ED60 | C785 2CFFFFFF 0A | mov dword ptr ss:[ebp-D4],A              | 0A:'\n'
0040ED6A | C785 44FFFFFF 04 | mov dword ptr ss:[ebp-BC],80020004       |
0040ED74 | C785 3CFFFFFF 0A | mov dword ptr ss:[ebp-C4],A              | 0A:'\n'
0040ED7E | C785 04FFFFFF 3C | mov dword ptr ss:[ebp-FC],[k]gme.40D73C  | 40D73C:L"Error"
0040ED88 | C785 FCFEFFFF 08 | mov dword ptr ss:[ebp-104],8             |
0040ED92 | 8D95 FCFEFFFF    | lea edx,dword ptr ss:[ebp-104]           |
0040ED98 | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040ED9E | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040EDA4 | C785 14FFFFFF 4C | mov dword ptr ss:[ebp-EC],[k]gme.40D74C  | 40D74C:L"User name must be atleast 4 characters long"
0040EDAE | C785 0CFFFFFF 08 | mov dword ptr ss:[ebp-F4],8              |
0040EDB8 | 8D95 0CFFFFFF    | lea edx,dword ptr ss:[ebp-F4]            |
0040EDBE | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040EDC4 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040EDCA | 8D8D 2CFFFFFF    | lea ecx,dword ptr ss:[ebp-D4]            |
0040EDD0 | 51               | push ecx                                 |
0040EDD1 | 8D95 3CFFFFFF    | lea edx,dword ptr ss:[ebp-C4]            |
0040EDD7 | 52               | push edx                                 |
0040EDD8 | 8D85 4CFFFFFF    | lea eax,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040EDDE | 50               | push eax                                 |
0040EDDF | 6A 30            | push 30                                  |
0040EDE1 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040EDE7 | 51               | push ecx                                 |
0040EDE8 | FF15 48104000    | call dword ptr ds:[<Ordinal#595>]        |
0040EDEE | 8D95 2CFFFFFF    | lea edx,dword ptr ss:[ebp-D4]            |
0040EDF4 | 52               | push edx                                 |
0040EDF5 | 8D85 3CFFFFFF    | lea eax,dword ptr ss:[ebp-C4]            |
0040EDFB | 50               | push eax                                 |
0040EDFC | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040EE02 | 51               | push ecx                                 |
0040EE03 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040EE09 | 52               | push edx                                 |
0040EE0A | 6A 04            | push 4                                   |
0040EE0C | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040EE12 | 83C4 14          | add esp,14                               |
0040EE15 | C745 FC 0D000000 | mov dword ptr ss:[ebp-4],D               | 0D:'\r'
0040EE1C | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040EE1F | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040EE21 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040EE24 | 52               | push edx                                 |
0040EE25 | FF91 14030000    | call dword ptr ds:[ecx+314]              |
0040EE2B | 50               | push eax                                 |
0040EE2C | 8D85 70FFFFFF    | lea eax,dword ptr ss:[ebp-90]            |
0040EE32 | 50               | push eax                                 |
0040EE33 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040EE39 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040EE3F | 6A 00            | push 0                                   |
0040EE41 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040EE47 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040EE49 | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040EE4F | 50               | push eax                                 |
0040EE50 | FF92 14010000    | call dword ptr ds:[edx+114]              |
0040EE56 | DBE2             | fnclex                                   |
0040EE58 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040EE5E | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040EE65 | 7D 26            | jge [k]gme.40EE8D                        |
0040EE67 | 68 14010000      | push 114                                 |
0040EE6C | 68 F8D64000      | push [k]gme.40D6F8                       |
0040EE71 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040EE77 | 51               | push ecx                                 |
0040EE78 | 8B95 CCFEFFFF    | mov edx,dword ptr ss:[ebp-134]           |
0040EE7E | 52               | push edx                                 |
0040EE7F | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040EE85 | 8985 88FEFFFF    | mov dword ptr ss:[ebp-178],eax           |
0040EE8B | EB 0A            | jmp [k]gme.40EE97                        |
0040EE8D | C785 88FEFFFF 00 | mov dword ptr ss:[ebp-178],0             |
0040EE97 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EE9D | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040EEA3 | C745 FC 0E000000 | mov dword ptr ss:[ebp-4],E               |
0040EEAA | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040EEAD | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040EEAF | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040EEB2 | 52               | push edx                                 |
0040EEB3 | FF91 14030000    | call dword ptr ds:[ecx+314]              |
0040EEB9 | 50               | push eax                                 |
0040EEBA | 8D85 70FFFFFF    | lea eax,dword ptr ss:[ebp-90]            |
0040EEC0 | 50               | push eax                                 |
0040EEC1 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040EEC7 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040EECD | 8B4D C0          | mov ecx,dword ptr ss:[ebp-40]            |
0040EED0 | 51               | push ecx                                 |
0040EED1 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EED7 | 8B02             | mov eax,dword ptr ds:[edx]               |
0040EED9 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040EEDF | 51               | push ecx                                 |
0040EEE0 | FF90 1C010000    | call dword ptr ds:[eax+11C]              |
0040EEE6 | DBE2             | fnclex                                   |
0040EEE8 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040EEEE | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040EEF5 | 7D 26            | jge [k]gme.40EF1D                        |
0040EEF7 | 68 1C010000      | push 11C                                 |
0040EEFC | 68 F8D64000      | push [k]gme.40D6F8                       |
0040EF01 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EF07 | 52               | push edx                                 |
0040EF08 | 8B85 CCFEFFFF    | mov eax,dword ptr ss:[ebp-134]           |
0040EF0E | 50               | push eax                                 |
0040EF0F | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040EF15 | 8985 84FEFFFF    | mov dword ptr ss:[ebp-17C],eax           | [ebp-17C]:MsgWaitForMultipleObjectsEx+8CF
0040EF1B | EB 0A            | jmp [k]gme.40EF27                        |
0040EF1D | C785 84FEFFFF 00 | mov dword ptr ss:[ebp-17C],0             | [ebp-17C]:MsgWaitForMultipleObjectsEx+8CF
0040EF27 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EF2D | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040EF33 | C745 FC 0F000000 | mov dword ptr ss:[ebp-4],F               |
0040EF3A | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040EF3D | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040EF3F | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040EF42 | 50               | push eax                                 |
0040EF43 | FF92 14030000    | call dword ptr ds:[edx+314]              |
0040EF49 | 50               | push eax                                 |
0040EF4A | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EF50 | 51               | push ecx                                 |
0040EF51 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040EF57 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040EF5D | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EF63 | 8B02             | mov eax,dword ptr ds:[edx]               |
0040EF65 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040EF6B | 51               | push ecx                                 |
0040EF6C | FF90 04020000    | call dword ptr ds:[eax+204]              |
0040EF72 | DBE2             | fnclex                                   |
0040EF74 | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040EF7A | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040EF81 | 7D 26            | jge [k]gme.40EFA9                        |
0040EF83 | 68 04020000      | push 204                                 |
0040EF88 | 68 F8D64000      | push [k]gme.40D6F8                       |
0040EF8D | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040EF93 | 52               | push edx                                 |
0040EF94 | 8B85 CCFEFFFF    | mov eax,dword ptr ss:[ebp-134]           |
0040EF9A | 50               | push eax                                 |
0040EF9B | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040EFA1 | 8985 80FEFFFF    | mov dword ptr ss:[ebp-180],eax           | [ebp-180]:MsgWaitForMultipleObjectsEx+4B9
0040EFA7 | EB 0A            | jmp [k]gme.40EFB3                        |
0040EFA9 | C785 80FEFFFF 00 | mov dword ptr ss:[ebp-180],0             | [ebp-180]:MsgWaitForMultipleObjectsEx+4B9
0040EFB3 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040EFB9 | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040EFBF | E9 54080000      | jmp [k]gme.40F818                        |
0040EFC4 | C745 FC 12000000 | mov dword ptr ss:[ebp-4],12              |
0040EFCB | 837D BC 00       | cmp dword ptr ss:[ebp-44],0              | 检查Serial是否为空
0040EFCF | 0F85 57010000    | jne [k]gme.40F12C                        |
0040EFD5 | C745 FC 13000000 | mov dword ptr ss:[ebp-4],13              |
0040EFDC | C785 34FFFFFF 04 | mov dword ptr ss:[ebp-CC],80020004       | [ebp-CC]:&L"MM d dddd"
0040EFE6 | C785 2CFFFFFF 0A | mov dword ptr ss:[ebp-D4],A              | 0A:'\n'
0040EFF0 | C785 44FFFFFF 04 | mov dword ptr ss:[ebp-BC],80020004       |
0040EFFA | C785 3CFFFFFF 0A | mov dword ptr ss:[ebp-C4],A              | 0A:'\n'
0040F004 | C785 04FFFFFF 3C | mov dword ptr ss:[ebp-FC],[k]gme.40D73C  | 40D73C:L"Error"
0040F00E | C785 FCFEFFFF 08 | mov dword ptr ss:[ebp-104],8             |
0040F018 | 8D95 FCFEFFFF    | lea edx,dword ptr ss:[ebp-104]           |
0040F01E | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040F024 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040F02A | C785 14FFFFFF A8 | mov dword ptr ss:[ebp-EC],[k]gme.40D7A8  | 40D7A8:L"Enter serial number first!"
0040F034 | C785 0CFFFFFF 08 | mov dword ptr ss:[ebp-F4],8              |
0040F03E | 8D95 0CFFFFFF    | lea edx,dword ptr ss:[ebp-F4]            |
0040F044 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F04A | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0040F050 | 8D8D 2CFFFFFF    | lea ecx,dword ptr ss:[ebp-D4]            |
0040F056 | 51               | push ecx                                 |
0040F057 | 8D95 3CFFFFFF    | lea edx,dword ptr ss:[ebp-C4]            |
0040F05D | 52               | push edx                                 |
0040F05E | 8D85 4CFFFFFF    | lea eax,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040F064 | 50               | push eax                                 |
0040F065 | 6A 30            | push 30                                  |
0040F067 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F06D | 51               | push ecx                                 |
0040F06E | FF15 48104000    | call dword ptr ds:[<Ordinal#595>]        |
0040F074 | 8D95 2CFFFFFF    | lea edx,dword ptr ss:[ebp-D4]            |
0040F07A | 52               | push edx                                 |
0040F07B | 8D85 3CFFFFFF    | lea eax,dword ptr ss:[ebp-C4]            |
0040F081 | 50               | push eax                                 |
0040F082 | 8D8D 4CFFFFFF    | lea ecx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040F088 | 51               | push ecx                                 |
0040F089 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040F08F | 52               | push edx                                 |
0040F090 | 6A 04            | push 4                                   |
0040F092 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040F098 | 83C4 14          | add esp,14                               |
0040F09B | C745 FC 14000000 | mov dword ptr ss:[ebp-4],14              |
0040F0A2 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F0A5 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F0A7 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040F0AA | 52               | push edx                                 |
0040F0AB | FF91 10030000    | call dword ptr ds:[ecx+310]              |
0040F0B1 | 50               | push eax                                 |
0040F0B2 | 8D85 70FFFFFF    | lea eax,dword ptr ss:[ebp-90]            |
0040F0B8 | 50               | push eax                                 |
0040F0B9 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040F0BF | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040F0C5 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040F0CB | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040F0CD | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040F0D3 | 50               | push eax                                 |
0040F0D4 | FF92 04020000    | call dword ptr ds:[edx+204]              |
0040F0DA | DBE2             | fnclex                                   |
0040F0DC | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040F0E2 | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040F0E9 | 7D 26            | jge [k]gme.40F111                        |
0040F0EB | 68 04020000      | push 204                                 |
0040F0F0 | 68 F8D64000      | push [k]gme.40D6F8                       |
0040F0F5 | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040F0FB | 51               | push ecx                                 |
0040F0FC | 8B95 CCFEFFFF    | mov edx,dword ptr ss:[ebp-134]           |
0040F102 | 52               | push edx                                 |
0040F103 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F109 | 8985 7CFEFFFF    | mov dword ptr ss:[ebp-184],eax           |
0040F10F | EB 0A            | jmp [k]gme.40F11B                        |
0040F111 | C785 7CFEFFFF 00 | mov dword ptr ss:[ebp-184],0             |
0040F11B | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F121 | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040F127 | E9 EC060000      | jmp [k]gme.40F818                        |
0040F12C | C745 FC 17000000 | mov dword ptr ss:[ebp-4],17              |
0040F133 | 6A 00            | push 0                                   |
0040F135 | FF15 B0104000    | call dword ptr ds:[<Ordinal#537>]        |
0040F13B | 8985 64FFFFFF    | mov dword ptr ss:[ebp-9C],eax            | [ebp-9C]:&L"MM d dddd"
0040F141 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],8              |
0040F14B | 8D85 5CFFFFFF    | lea eax,dword ptr ss:[ebp-A4]            |
0040F151 | 50               | push eax                                 |
0040F152 | 68 FF000000      | push FF                                  |
0040F157 | FF15 9C104000    | call dword ptr ds:[<Ordinal#606>]        |
0040F15D | 8BD0             | mov edx,eax                              |
0040F15F | 8D4D 90          | lea ecx,dword ptr ss:[ebp-70]            |
0040F162 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F168 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F16E | FF15 08104000    | call dword ptr ds:[<__vbaFreeVar>]       |
0040F174 | C745 FC 18000000 | mov dword ptr ss:[ebp-4],18              |
0040F17B | 6A 00            | push 0                                   |
0040F17D | FF15 B0104000    | call dword ptr ds:[<Ordinal#537>]        |
0040F183 | 8985 64FFFFFF    | mov dword ptr ss:[ebp-9C],eax            | [ebp-9C]:&L"MM d dddd"
0040F189 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],8              |
0040F193 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F199 | 51               | push ecx                                 |
0040F19A | 68 FF000000      | push FF                                  |
0040F19F | FF15 9C104000    | call dword ptr ds:[<Ordinal#606>]        |
0040F1A5 | 8BD0             | mov edx,eax                              |
0040F1A7 | 8D4D 94          | lea ecx,dword ptr ss:[ebp-6C]            |
0040F1AA | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F1B0 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F1B6 | FF15 08104000    | call dword ptr ds:[<__vbaFreeVar>]       |
0040F1BC | C745 FC 19000000 | mov dword ptr ss:[ebp-4],19              |
0040F1C3 | 833D EC224100 00 | cmp dword ptr ds:[4122EC],0              |
0040F1CA | 75 1C            | jne [k]gme.40F1E8                        |
0040F1CC | 68 EC224100      | push [k]gme.4122EC                       |
0040F1D1 | 68 00D84000      | push [k]gme.40D800                       |
0040F1D6 | FF15 BC104000    | call dword ptr ds:[<__vbaNew2>]          |
0040F1DC | C785 78FEFFFF EC | mov dword ptr ss:[ebp-188],[k]gme.4122EC |
0040F1E6 | EB 0A            | jmp [k]gme.40F1F2                        |
0040F1E8 | C785 78FEFFFF EC | mov dword ptr ss:[ebp-188],[k]gme.4122EC |
0040F1F2 | 8B95 78FEFFFF    | mov edx,dword ptr ss:[ebp-188]           |
0040F1F8 | 8B02             | mov eax,dword ptr ds:[edx]               |
0040F1FA | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040F200 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F206 | 51               | push ecx                                 |
0040F207 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040F20D | 8B02             | mov eax,dword ptr ds:[edx]               |
0040F20F | 8B8D D0FEFFFF    | mov ecx,dword ptr ss:[ebp-130]           |
0040F215 | 51               | push ecx                                 |
0040F216 | FF50 14          | call dword ptr ds:[eax+14]               |
0040F219 | DBE2             | fnclex                                   |
0040F21B | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040F221 | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040F228 | 7D 23            | jge [k]gme.40F24D                        |
0040F22A | 6A 14            | push 14                                  |
0040F22C | 68 F0D74000      | push [k]gme.40D7F0                       |
0040F231 | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040F237 | 52               | push edx                                 |
0040F238 | 8B85 CCFEFFFF    | mov eax,dword ptr ss:[ebp-134]           |
0040F23E | 50               | push eax                                 |
0040F23F | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F245 | 8985 74FEFFFF    | mov dword ptr ss:[ebp-18C],eax           |
0040F24B | EB 0A            | jmp [k]gme.40F257                        |
0040F24D | C785 74FEFFFF 00 | mov dword ptr ss:[ebp-18C],0             |
0040F257 | 8B8D 70FFFFFF    | mov ecx,dword ptr ss:[ebp-90]            |
0040F25D | 898D C8FEFFFF    | mov dword ptr ss:[ebp-138],ecx           |
0040F263 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040F266 | 52               | push edx                                 |
0040F267 | 8B85 C8FEFFFF    | mov eax,dword ptr ss:[ebp-138]           |
0040F26D | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F26F | 8B95 C8FEFFFF    | mov edx,dword ptr ss:[ebp-138]           |
0040F275 | 52               | push edx                                 |
0040F276 | FF51 50          | call dword ptr ds:[ecx+50]               |
0040F279 | DBE2             | fnclex                                   |
0040F27B | 8985 C4FEFFFF    | mov dword ptr ss:[ebp-13C],eax           |
0040F281 | 83BD C4FEFFFF 00 | cmp dword ptr ss:[ebp-13C],0             |
0040F288 | 7D 23            | jge [k]gme.40F2AD                        |
0040F28A | 6A 50            | push 50                                  |
0040F28C | 68 10D84000      | push [k]gme.40D810                       |
0040F291 | 8B85 C8FEFFFF    | mov eax,dword ptr ss:[ebp-138]           |
0040F297 | 50               | push eax                                 |
0040F298 | 8B8D C4FEFFFF    | mov ecx,dword ptr ss:[ebp-13C]           |
0040F29E | 51               | push ecx                                 |
0040F29F | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F2A5 | 8985 70FEFFFF    | mov dword ptr ss:[ebp-190],eax           |
0040F2AB | EB 0A            | jmp [k]gme.40F2B7                        |
0040F2AD | C785 70FEFFFF 00 | mov dword ptr ss:[ebp-190],0             |
0040F2B7 | 6A 03            | push 3                                   |
0040F2B9 | 8B55 8C          | mov edx,dword ptr ss:[ebp-74]            |
0040F2BC | 52               | push edx                                 |
0040F2BD | FF15 FC104000    | call dword ptr ds:[<Ordinal#616>]        |
0040F2C3 | 8BD0             | mov edx,eax                              |
0040F2C5 | 8D8D 78FFFFFF    | lea ecx,dword ptr ss:[ebp-88]            |
0040F2CB | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F2D1 | C785 D4FEFFFF 00 | mov dword ptr ss:[ebp-12C],0             |
0040F2DB | C785 D8FEFFFF 00 | mov dword ptr ss:[ebp-128],0             |
0040F2E5 | 8B85 78FFFFFF    | mov eax,dword ptr ss:[ebp-88]            |
0040F2EB | 8985 A8FEFFFF    | mov dword ptr ss:[ebp-158],eax           |
0040F2F1 | C785 78FFFFFF 00 | mov dword ptr ss:[ebp-88],0              |
0040F2FB | 68 FF000000      | push FF                                  |
0040F300 | 8B4D 94          | mov ecx,dword ptr ss:[ebp-6C]            |
0040F303 | 51               | push ecx                                 |
0040F304 | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F30A | 52               | push edx                                 |
0040F30B | FF15 F4104000    | call dword ptr ds:[<__vbaStrToAnsi>]     |
0040F311 | 50               | push eax                                 |
0040F312 | 8D85 D4FEFFFF    | lea eax,dword ptr ss:[ebp-12C]           |
0040F318 | 50               | push eax                                 |
0040F319 | 8D8D D8FEFFFF    | lea ecx,dword ptr ss:[ebp-128]           |
0040F31F | 51               | push ecx                                 |
0040F320 | 8D55 DC          | lea edx,dword ptr ss:[ebp-24]            |
0040F323 | 52               | push edx                                 |
0040F324 | 68 FF000000      | push FF                                  |
0040F329 | 8B45 90          | mov eax,dword ptr ss:[ebp-70]            |
0040F32C | 50               | push eax                                 |
0040F32D | 8D4D 80          | lea ecx,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F330 | 51               | push ecx                                 |
0040F331 | FF15 F4104000    | call dword ptr ds:[<__vbaStrToAnsi>]     |
0040F337 | 50               | push eax                                 |
0040F338 | 8B95 A8FEFFFF    | mov edx,dword ptr ss:[ebp-158]           |
0040F33E | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040F341 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F347 | 50               | push eax                                 |
0040F348 | 8D55 84          | lea edx,dword ptr ss:[ebp-7C]            |
0040F34B | 52               | push edx                                 |
0040F34C | FF15 F4104000    | call dword ptr ds:[<__vbaStrToAnsi>]     |
0040F352 | 50               | push eax                                 |
0040F353 | E8 6CE3FFFF      | call [k]gme.40D6C4                       | GetVolumeInformationA
0040F358 | FF15 30104000    | call dword ptr ds:[<__vbaSetSystemError> |
0040F35E | 8B45 80          | mov eax,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F361 | 50               | push eax                                 |
0040F362 | 8D4D 90          | lea ecx,dword ptr ss:[ebp-70]            |
0040F365 | 51               | push ecx                                 |
0040F366 | FF15 94104000    | call dword ptr ds:[<__vbaStrToUnicode>]  |
0040F36C | 8B95 7CFFFFFF    | mov edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F372 | 52               | push edx                                 |
0040F373 | 8D45 94          | lea eax,dword ptr ss:[ebp-6C]            |
0040F376 | 50               | push eax                                 |
0040F377 | FF15 94104000    | call dword ptr ds:[<__vbaStrToUnicode>]  |
0040F37D | 8D8D 78FFFFFF    | lea ecx,dword ptr ss:[ebp-88]            |
0040F383 | 51               | push ecx                                 |
0040F384 | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F38A | 52               | push edx                                 |
0040F38B | 8D45 80          | lea eax,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F38E | 50               | push eax                                 |
0040F38F | 8D4D 84          | lea ecx,dword ptr ss:[ebp-7C]            |
0040F392 | 51               | push ecx                                 |
0040F393 | 8D55 88          | lea edx,dword ptr ss:[ebp-78]            |
0040F396 | 52               | push edx                                 |
0040F397 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040F39A | 50               | push eax                                 |
0040F39B | 6A 06            | push 6                                   |
0040F39D | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040F3A3 | 83C4 1C          | add esp,1C                               |
0040F3A6 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F3AC | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0040F3B2 | C745 FC 1A000000 | mov dword ptr ss:[ebp-4],1A              |
0040F3B9 | 6A 01            | push 1                                   |
0040F3BB | 8B4D 90          | mov ecx,dword ptr ss:[ebp-70]            |
0040F3BE | 51               | push ecx                                 |
0040F3BF | 6A 00            | push 0                                   |
0040F3C1 | FF15 B0104000    | call dword ptr ds:[<Ordinal#537>]        |
0040F3C7 | 8BD0             | mov edx,eax                              |
0040F3C9 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040F3CC | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F3D2 | 50               | push eax                                 |
0040F3D3 | 6A 00            | push 0                                   |
0040F3D5 | FF15 C0104000    | call dword ptr ds:[<__vbaInStr>]         |
0040F3DB | 83E8 01          | sub eax,1                                |
0040F3DE | 0F80 22050000    | jo [k]gme.40F906                         |
0040F3E4 | 50               | push eax                                 |
0040F3E5 | 8B55 90          | mov edx,dword ptr ss:[ebp-70]            |
0040F3E8 | 52               | push edx                                 |
0040F3E9 | FF15 FC104000    | call dword ptr ds:[<Ordinal#616>]        |
0040F3EF | 8BD0             | mov edx,eax                              |
0040F3F1 | 8D4D 90          | lea ecx,dword ptr ss:[ebp-70]            |
0040F3F4 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F3FA | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040F3FD | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F403 | C745 FC 1B000000 | mov dword ptr ss:[ebp-4],1B              |
0040F40A | 6A 01            | push 1                                   |
0040F40C | 8B45 94          | mov eax,dword ptr ss:[ebp-6C]            |
0040F40F | 50               | push eax                                 |
0040F410 | 6A 00            | push 0                                   |
0040F412 | FF15 B0104000    | call dword ptr ds:[<Ordinal#537>]        |
0040F418 | 8BD0             | mov edx,eax                              |
0040F41A | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040F41D | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F423 | 50               | push eax                                 |
0040F424 | 6A 00            | push 0                                   |
0040F426 | FF15 C0104000    | call dword ptr ds:[<__vbaInStr>]         |
0040F42C | 83E8 01          | sub eax,1                                |
0040F42F | 0F80 D1040000    | jo [k]gme.40F906                         |
0040F435 | 50               | push eax                                 |
0040F436 | 8B4D 94          | mov ecx,dword ptr ss:[ebp-6C]            |
0040F439 | 51               | push ecx                                 |
0040F43A | FF15 FC104000    | call dword ptr ds:[<Ordinal#616>]        |
0040F440 | 8BD0             | mov edx,eax                              |
0040F442 | 8D4D 94          | lea ecx,dword ptr ss:[ebp-6C]            |
0040F445 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F44B | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040F44E | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F454 | C745 FC 1C000000 | mov dword ptr ss:[ebp-4],1C              |
0040F45B | 8D55 98          | lea edx,dword ptr ss:[ebp-68]            | [ebp-68]:&"脥I"
0040F45E | 52               | push edx                                 |
0040F45F | E8 10E2FFFF      | call [k]gme.40D674                       | GetSystemInfo
0040F464 | FF15 30104000    | call dword ptr ds:[<__vbaSetSystemError> |
0040F46A | C745 FC 1D000000 | mov dword ptr ss:[ebp-4],1D              |
0040F471 | 8B45 AC          | mov eax,dword ptr ss:[ebp-54]            |
0040F474 | 8945 C4          | mov dword ptr ss:[ebp-3C],eax            | [ebp-3C]:&L"MM d dddd"
0040F477 | C745 FC 1E000000 | mov dword ptr ss:[ebp-4],1E              |
0040F47E | 8D4D B0          | lea ecx,dword ptr ss:[ebp-50]            |
0040F481 | 898D 14FFFFFF    | mov dword ptr ss:[ebp-EC],ecx            |
0040F487 | C785 0CFFFFFF 03 | mov dword ptr ss:[ebp-F4],4003           |
0040F491 | 8D95 0CFFFFFF    | lea edx,dword ptr ss:[ebp-F4]            |
0040F497 | 52               | push edx                                 |
0040F498 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040F49E | 8BD0             | mov edx,eax                              |
0040F4A0 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040F4A3 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F4A9 | C745 FC 1F000000 | mov dword ptr ss:[ebp-4],1F              |
0040F4B0 | 8D45 A0          | lea eax,dword ptr ss:[ebp-60]            | [ebp-60]:&L"MM d dddd"
0040F4B3 | 8985 14FFFFFF    | mov dword ptr ss:[ebp-EC],eax            |
0040F4B9 | C785 0CFFFFFF 03 | mov dword ptr ss:[ebp-F4],4003           |
0040F4C3 | 8D8D 0CFFFFFF    | lea ecx,dword ptr ss:[ebp-F4]            |
0040F4C9 | 51               | push ecx                                 |
0040F4CA | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040F4D0 | 8BD0             | mov edx,eax                              |
0040F4D2 | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040F4D5 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F4DB | C745 FC 20000000 | mov dword ptr ss:[ebp-4],20              | 20:' '
0040F4E2 | DB45 A4          | fild dword ptr ss:[ebp-5C]               |
0040F4E5 | DD9D 68FEFFFF    | fstp qword ptr ss:[ebp-198]              |
0040F4EB | DD85 68FEFFFF    | fld qword ptr ss:[ebp-198]               |
0040F4F1 | 833D 00204100 00 | cmp dword ptr ds:[412000],0              |
0040F4F8 | 75 08            | jne [k]gme.40F502                        |
0040F4FA | DC35 E0114000    | fdiv qword ptr ds:[4011E0]               | 0xFF
0040F500 | EB 11            | jmp [k]gme.40F513                        |
0040F502 | FF35 E4114000    | push dword ptr ds:[4011E4]               |
0040F508 | FF35 E0114000    | push dword ptr ds:[4011E0]               |
0040F50E | E8 D11EFFFF      | call <JMP.&_adj_fdiv_m64>                |
0040F513 | DD9D 64FFFFFF    | fstp qword ptr ss:[ebp-9C]               |
0040F519 | DFE0             | fnstsw ax                                |
0040F51B | A8 0D            | test al,D                                |
0040F51D | 0F85 DE030000    | jne [k]gme.40F901                        |
0040F523 | C785 5CFFFFFF 05 | mov dword ptr ss:[ebp-A4],5              |
0040F52D | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
0040F533 | 52               | push edx                                 |
0040F534 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040F53A | 8BD0             | mov edx,eax                              | MaximumApplicationAddress / 0xFF
0040F53C | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040F53F | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F545 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040F54B | FF15 08104000    | call dword ptr ds:[<__vbaFreeVar>]       |
0040F551 | C745 FC 21000000 | mov dword ptr ss:[ebp-4],21              | 21:'!'
0040F558 | 8D45 B4          | lea eax,dword ptr ss:[ebp-4C]            |
0040F55B | 8985 14FFFFFF    | mov dword ptr ss:[ebp-EC],eax            |
0040F561 | C785 0CFFFFFF 03 | mov dword ptr ss:[ebp-F4],4003           |
0040F56B | 8D8D 0CFFFFFF    | lea ecx,dword ptr ss:[ebp-F4]            |
0040F571 | 51               | push ecx                                 |
0040F572 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040F578 | 8BD0             | mov edx,eax                              |
0040F57A | 8D4D CC          | lea ecx,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040F57D | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F583 | C745 FC 22000000 | mov dword ptr ss:[ebp-4],22              | 22:'\"'
0040F58A | 8D55 B8          | lea edx,dword ptr ss:[ebp-48]            | [ebp-48]:"脥I"
0040F58D | 8995 14FFFFFF    | mov dword ptr ss:[ebp-EC],edx            |
0040F593 | C785 0CFFFFFF 03 | mov dword ptr ss:[ebp-F4],4003           |
0040F59D | 8D85 0CFFFFFF    | lea eax,dword ptr ss:[ebp-F4]            |
0040F5A3 | 50               | push eax                                 |
0040F5A4 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040F5AA | 8BD0             | mov edx,eax                              |
0040F5AC | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
0040F5AF | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F5B5 | C745 FC 23000000 | mov dword ptr ss:[ebp-4],23              | 23:'#'
0040F5BC | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040F5BF | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040F5C1 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F5C4 | 50               | push eax                                 |
0040F5C5 | FF92 14030000    | call dword ptr ds:[edx+314]              |
0040F5CB | 50               | push eax                                 |
0040F5CC | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F5D2 | 51               | push ecx                                 |
0040F5D3 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040F5D9 | 8985 D0FEFFFF    | mov dword ptr ss:[ebp-130],eax           |
0040F5DF | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040F5E2 | 52               | push edx                                 |
0040F5E3 | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040F5E9 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F5EB | 8B95 D0FEFFFF    | mov edx,dword ptr ss:[ebp-130]           |
0040F5F1 | 52               | push edx                                 |
0040F5F2 | FF91 A0000000    | call dword ptr ds:[ecx+A0]               |
0040F5F8 | DBE2             | fnclex                                   |
0040F5FA | 8985 CCFEFFFF    | mov dword ptr ss:[ebp-134],eax           |
0040F600 | 83BD CCFEFFFF 00 | cmp dword ptr ss:[ebp-134],0             |
0040F607 | 7D 26            | jge [k]gme.40F62F                        |
0040F609 | 68 A0000000      | push A0                                  |
0040F60E | 68 F8D64000      | push [k]gme.40D6F8                       |
0040F613 | 8B85 D0FEFFFF    | mov eax,dword ptr ss:[ebp-130]           |
0040F619 | 50               | push eax                                 |
0040F61A | 8B8D CCFEFFFF    | mov ecx,dword ptr ss:[ebp-134]           |
0040F620 | 51               | push ecx                                 |
0040F621 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F627 | 8985 64FEFFFF    | mov dword ptr ss:[ebp-19C],eax           |
0040F62D | EB 0A            | jmp [k]gme.40F639                        |
0040F62F | C785 64FEFFFF 00 | mov dword ptr ss:[ebp-19C],0             |
0040F639 | 8B55 8C          | mov edx,dword ptr ss:[ebp-74]            |
0040F63C | 52               | push edx                                 |
0040F63D | FF15 28104000    | call dword ptr ds:[<Ordinal#519>]        |
0040F643 | 8BD0             | mov edx,eax                              |
0040F645 | 8D8D 78FFFFFF    | lea ecx,dword ptr ss:[ebp-88]            |
0040F64B | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F651 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F654 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F656 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040F659 | 52               | push edx                                 |
0040F65A | FF91 10030000    | call dword ptr ds:[ecx+310]              |
0040F660 | 50               | push eax                                 |
0040F661 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040F667 | 50               | push eax                                 |
0040F668 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0040F66E | 8985 C8FEFFFF    | mov dword ptr ss:[ebp-138],eax           |
0040F674 | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040F677 | 51               | push ecx                                 |
0040F678 | 8B95 C8FEFFFF    | mov edx,dword ptr ss:[ebp-138]           |
0040F67E | 8B02             | mov eax,dword ptr ds:[edx]               |
0040F680 | 8B8D C8FEFFFF    | mov ecx,dword ptr ss:[ebp-138]           |
0040F686 | 51               | push ecx                                 |
0040F687 | FF90 A0000000    | call dword ptr ds:[eax+A0]               |
0040F68D | DBE2             | fnclex                                   |
0040F68F | 8985 C4FEFFFF    | mov dword ptr ss:[ebp-13C],eax           |
0040F695 | 83BD C4FEFFFF 00 | cmp dword ptr ss:[ebp-13C],0             |
0040F69C | 7D 26            | jge [k]gme.40F6C4                        |
0040F69E | 68 A0000000      | push A0                                  |
0040F6A3 | 68 F8D64000      | push [k]gme.40D6F8                       |
0040F6A8 | 8B95 C8FEFFFF    | mov edx,dword ptr ss:[ebp-138]           |
0040F6AE | 52               | push edx                                 |
0040F6AF | 8B85 C4FEFFFF    | mov eax,dword ptr ss:[ebp-13C]           |
0040F6B5 | 50               | push eax                                 |
0040F6B6 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F6BC | 8985 60FEFFFF    | mov dword ptr ss:[ebp-1A0],eax           |
0040F6C2 | EB 0A            | jmp [k]gme.40F6CE                        |
0040F6C4 | C785 60FEFFFF 00 | mov dword ptr ss:[ebp-1A0],0             |
0040F6CE | 6A 00            | push 0                                   |
0040F6D0 | 6A FF            | push FFFFFFFF                            |
0040F6D2 | 6A 01            | push 1                                   |
0040F6D4 | 68 2CD84000      | push [k]gme.40D82C                       | null
0040F6D9 | 68 24D84000      | push [k]gme.40D824                       | -
0040F6DE | 8B4D 88          | mov ecx,dword ptr ss:[ebp-78]            |
0040F6E1 | 51               | push ecx                                 |
0040F6E2 | FF15 98104000    | call dword ptr ds:[<Ordinal#712>]        | 去除serial中的-
0040F6E8 | 8BD0             | mov edx,eax                              |
0040F6EA | 8D4D 84          | lea ecx,dword ptr ss:[ebp-7C]            |
0040F6ED | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F6F3 | 50               | push eax                                 |
0040F6F4 | FF15 28104000    | call dword ptr ds:[<Ordinal#519>]        |
0040F6FA | 8BD0             | mov edx,eax                              |
0040F6FC | 8D8D 74FFFFFF    | lea ecx,dword ptr ss:[ebp-8C]            | [ebp-8C]:IsWindow+6E
0040F702 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F708 | 8B95 74FFFFFF    | mov edx,dword ptr ss:[ebp-8C]            | [ebp-8C]:IsWindow+6E
0040F70E | 8995 A4FEFFFF    | mov dword ptr ss:[ebp-15C],edx           |
0040F714 | C785 74FFFFFF 00 | mov dword ptr ss:[ebp-8C],0              | [ebp-8C]:IsWindow+6E
0040F71E | 8B95 A4FEFFFF    | mov edx,dword ptr ss:[ebp-15C]           |
0040F724 | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F72A | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F730 | 8B85 78FFFFFF    | mov eax,dword ptr ss:[ebp-88]            |
0040F736 | 8985 A0FEFFFF    | mov dword ptr ss:[ebp-160],eax           |
0040F73C | C785 78FFFFFF 00 | mov dword ptr ss:[ebp-88],0              |
0040F746 | 8B95 A0FEFFFF    | mov edx,dword ptr ss:[ebp-160]           |
0040F74C | 8D4D 80          | lea ecx,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F74F | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040F755 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
0040F758 | 51               | push ecx                                 |
0040F759 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
0040F75C | 52               | push edx                                 |
0040F75D | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040F760 | 50               | push eax                                 |
0040F761 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040F764 | 51               | push ecx                                 |
0040F765 | 8D55 D4          | lea edx,dword ptr ss:[ebp-2C]            |
0040F768 | 52               | push edx                                 |
0040F769 | 8D45 D8          | lea eax,dword ptr ss:[ebp-28]            |
0040F76C | 50               | push eax                                 |
0040F76D | 8D4D DC          | lea ecx,dword ptr ss:[ebp-24]            |
0040F770 | 51               | push ecx                                 |
0040F771 | 8D55 94          | lea edx,dword ptr ss:[ebp-6C]            |
0040F774 | 52               | push edx                                 |
0040F775 | 8D45 90          | lea eax,dword ptr ss:[ebp-70]            |
0040F778 | 50               | push eax                                 |
0040F779 | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F77F | 51               | push ecx                                 |
0040F780 | 8D55 80          | lea edx,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F783 | 52               | push edx                                 |
0040F784 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F787 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F789 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040F78C | 52               | push edx                                 |
0040F78D | FF91 F8060000    | call dword ptr ds:[ecx+6F8]              | 0040F910
0040F793 | 8985 C0FEFFFF    | mov dword ptr ss:[ebp-140],eax           |
0040F799 | 83BD C0FEFFFF 00 | cmp dword ptr ss:[ebp-140],0             |
0040F7A0 | 7D 23            | jge [k]gme.40F7C5                        |
0040F7A2 | 68 F8060000      | push 6F8                                 |
0040F7A7 | 68 70D44000      | push [k]gme.40D470                       |
0040F7AC | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F7AF | 50               | push eax                                 |
0040F7B0 | 8B8D C0FEFFFF    | mov ecx,dword ptr ss:[ebp-140]           |
0040F7B6 | 51               | push ecx                                 |
0040F7B7 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0040F7BD | 8985 5CFEFFFF    | mov dword ptr ss:[ebp-1A4],eax           |
0040F7C3 | EB 0A            | jmp [k]gme.40F7CF                        |
0040F7C5 | C785 5CFEFFFF 00 | mov dword ptr ss:[ebp-1A4],0             |
0040F7CF | 8D95 74FFFFFF    | lea edx,dword ptr ss:[ebp-8C]            | [ebp-8C]:IsWindow+6E
0040F7D5 | 52               | push edx                                 |
0040F7D6 | 8D85 78FFFFFF    | lea eax,dword ptr ss:[ebp-88]            |
0040F7DC | 50               | push eax                                 |
0040F7DD | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F7E3 | 51               | push ecx                                 |
0040F7E4 | 8D55 80          | lea edx,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F7E7 | 52               | push edx                                 |
0040F7E8 | 8D45 84          | lea eax,dword ptr ss:[ebp-7C]            |
0040F7EB | 50               | push eax                                 |
0040F7EC | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040F7EF | 51               | push ecx                                 |
0040F7F0 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040F7F3 | 52               | push edx                                 |
0040F7F4 | 6A 07            | push 7                                   |
0040F7F6 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040F7FC | 83C4 20          | add esp,20                               |
0040F7FF | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040F805 | 50               | push eax                                 |
0040F806 | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F80C | 51               | push ecx                                 |
0040F80D | 6A 02            | push 2                                   |
0040F80F | FF15 1C104000    | call dword ptr ds:[<__vbaFreeObjList>]   |
0040F815 | 83C4 0C          | add esp,C                                |
0040F818 | C745 F0 00000000 | mov dword ptr ss:[ebp-10],0              | [ebp-10]:&L"MM d dddd"
0040F81F | 9B               | fwait                                    |
0040F820 | 68 DFF84000      | push [k]gme.40F8DF                       |
0040F825 | EB 78            | jmp [k]gme.40F89F                        |
0040F827 | 8D95 74FFFFFF    | lea edx,dword ptr ss:[ebp-8C]            | [ebp-8C]:IsWindow+6E
0040F82D | 52               | push edx                                 |
0040F82E | 8D85 78FFFFFF    | lea eax,dword ptr ss:[ebp-88]            |
0040F834 | 50               | push eax                                 |
0040F835 | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040F83B | 51               | push ecx                                 |
0040F83C | 8D55 80          | lea edx,dword ptr ss:[ebp-80]            | [ebp-80]:&L"MM d dddd"
0040F83F | 52               | push edx                                 |
0040F840 | 8D45 84          | lea eax,dword ptr ss:[ebp-7C]            |
0040F843 | 50               | push eax                                 |
0040F844 | 8D4D 88          | lea ecx,dword ptr ss:[ebp-78]            |
0040F847 | 51               | push ecx                                 |
0040F848 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040F84B | 52               | push edx                                 |
0040F84C | 6A 07            | push 7                                   |
0040F84E | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040F854 | 83C4 20          | add esp,20                               |
0040F857 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040F85D | 50               | push eax                                 |
0040F85E | 8D8D 70FFFFFF    | lea ecx,dword ptr ss:[ebp-90]            |
0040F864 | 51               | push ecx                                 |
0040F865 | 6A 02            | push 2                                   |
0040F867 | FF15 1C104000    | call dword ptr ds:[<__vbaFreeObjList>]   |
0040F86D | 83C4 0C          | add esp,C                                |
0040F870 | 8D95 1CFFFFFF    | lea edx,dword ptr ss:[ebp-E4]            | [ebp-E4]:PeekMessageW+2BB
0040F876 | 52               | push edx                                 |
0040F877 | 8D85 2CFFFFFF    | lea eax,dword ptr ss:[ebp-D4]            |
0040F87D | 50               | push eax                                 |
0040F87E | 8D8D 3CFFFFFF    | lea ecx,dword ptr ss:[ebp-C4]            |
0040F884 | 51               | push ecx                                 |
0040F885 | 8D95 4CFFFFFF    | lea edx,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040F88B | 52               | push edx                                 |
0040F88C | 8D85 5CFFFFFF    | lea eax,dword ptr ss:[ebp-A4]            |
0040F892 | 50               | push eax                                 |
0040F893 | 6A 05            | push 5                                   |
0040F895 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040F89B | 83C4 18          | add esp,18                               |
0040F89E | C3               | ret                                      |
0040F89F | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040F8A2 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8A8 | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040F8AB | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8B1 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040F8B4 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8BA | 8D4D CC          | lea ecx,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040F8BD | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8C3 | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
0040F8C6 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8CC | 8D4D 94          | lea ecx,dword ptr ss:[ebp-6C]            |
0040F8CF | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8D5 | 8D4D 90          | lea ecx,dword ptr ss:[ebp-70]            |
0040F8D8 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040F8DE | C3               | ret                                      |
0040F8DF | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0040F8E2 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040F8E4 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F8E7 | 50               | push eax                                 |
0040F8E8 | FF52 08          | call dword ptr ds:[edx+8]                |
0040F8EB | 8B45 F0          | mov eax,dword ptr ss:[ebp-10]            | [ebp-10]:&L"MM d dddd"
0040F8EE | 8B4D E0          | mov ecx,dword ptr ss:[ebp-20]            |
0040F8F1 | 64:890D 00000000 | mov dword ptr fs:[0],ecx                 |
0040F8F8 | 5F               | pop edi                                  | edi:&L"MM d dddd"
0040F8F9 | 5E               | pop esi                                  |
0040F8FA | 5B               | pop ebx                                  |
0040F8FB | 8BE5             | mov esp,ebp                              |
0040F8FD | 5D               | pop ebp                                  |
0040F8FE | C2 0400          | ret 4                                    |
0040F901 | E9 C61AFFFF      | jmp <JMP.&__vbaFPException>              |
0040F906 | FF15 B8104000    | call dword ptr ds:[<__vbaErrorOverflow>] |
```

```assembly
0040F910 | 55               | push ebp                                 |
0040F911 | 8BEC             | mov ebp,esp                              |
0040F913 | 83EC 18          | sub esp,18                               |
0040F916 | 68 C6134000      | push <JMP.&__vbaExceptHandler>           |
0040F91B | 64:A1 00000000   | mov eax,dword ptr fs:[0]                 |
0040F921 | 50               | push eax                                 |
0040F922 | 64:8925 00000000 | mov dword ptr fs:[0],esp                 |
0040F929 | B8 F0000000      | mov eax,F0                               |
0040F92E | E8 8D1AFFFF      | call <JMP.&__vbaChkstk>                  |
0040F933 | 53               | push ebx                                 |
0040F934 | 56               | push esi                                 |
0040F935 | 57               | push edi                                 | edi:&L"MM d dddd"
0040F936 | 8965 E8          | mov dword ptr ss:[ebp-18],esp            |
0040F939 | C745 EC E8114000 | mov dword ptr ss:[ebp-14],[k]gme.4011E8  |
0040F940 | C745 F0 00000000 | mov dword ptr ss:[ebp-10],0              | [ebp-10]:&L"MM d dddd"
0040F947 | C745 F4 00000000 | mov dword ptr ss:[ebp-C],0               |
0040F94E | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0040F951 | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F953 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
0040F956 | 52               | push edx                                 |
0040F957 | FF51 04          | call dword ptr ds:[ecx+4]                |
0040F95A | C745 FC 01000000 | mov dword ptr ss:[ebp-4],1               |
0040F961 | C745 FC 02000000 | mov dword ptr ss:[ebp-4],2               |
0040F968 | 6A FF            | push FFFFFFFF                            |
0040F96A | FF15 40104000    | call dword ptr ds:[<__vbaOnError>]       |
0040F970 | C745 FC 03000000 | mov dword ptr ss:[ebp-4],3               |
0040F977 | 8B45 14          | mov eax,dword ptr ss:[ebp+14]            |
0040F97A | 8B08             | mov ecx,dword ptr ds:[eax]               |
0040F97C | 51               | push ecx                                 |
0040F97D | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
0040F983 | 85C0             | test eax,eax                             |
0040F985 | 75 15            | jne [k]gme.40F99C                        |
0040F987 | C745 FC 04000000 | mov dword ptr ss:[ebp-4],4               |
0040F98E | BA 34D84000      | mov edx,[k]gme.40D834                    | 40D834:L"EB"
0040F993 | 8B4D 14          | mov ecx,dword ptr ss:[ebp+14]            |
0040F996 | FF15 D4104000    | call dword ptr ds:[<__vbaStrCopy>]       | EB
0040F99C | C745 FC 06000000 | mov dword ptr ss:[ebp-4],6               |
0040F9A3 | C785 1CFFFFFF 01 | mov dword ptr ss:[ebp-E4],1              | [ebp-E4]:PeekMessageW+2BB
0040F9AD | C785 20FFFFFF FF | mov dword ptr ss:[ebp-E0],FFFFFFFF       |
0040F9B7 | 8B55 20          | mov edx,dword ptr ss:[ebp+20]            |
0040F9BA | 8B02             | mov eax,dword ptr ds:[edx]               |
0040F9BC | 50               | push eax                                 |
0040F9BD | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
0040F9C3 | 8945 DC          | mov dword ptr ss:[ebp-24],eax            |
0040F9C6 | EB 12            | jmp [k]gme.40F9DA                        |
0040F9C8 | 8B4D DC          | mov ecx,dword ptr ss:[ebp-24]            | 翻转 ex:24A A42
0040F9CB | 038D 20FFFFFF    | add ecx,dword ptr ss:[ebp-E0]            |
0040F9D1 | 0F80 840B0000    | jo [k]gme.41055B                         |
0040F9D7 | 894D DC          | mov dword ptr ss:[ebp-24],ecx            |
0040F9DA | 8B55 DC          | mov edx,dword ptr ss:[ebp-24]            |
0040F9DD | 3B95 1CFFFFFF    | cmp edx,dword ptr ss:[ebp-E4]            | [ebp-E4]:PeekMessageW+2BB
0040F9E3 | 0F8C 95000000    | jl [k]gme.40FA7E                         |
0040F9E9 | C745 FC 07000000 | mov dword ptr ss:[ebp-4],7               |
0040F9F0 | C745 B4 01000000 | mov dword ptr ss:[ebp-4C],1              |
0040F9F7 | C745 AC 02000000 | mov dword ptr ss:[ebp-54],2              |
0040F9FE | 8B45 20          | mov eax,dword ptr ss:[ebp+20]            |
0040FA01 | 8985 74FFFFFF    | mov dword ptr ss:[ebp-8C],eax            | [ebp-8C]:IsWindow+6E
0040FA07 | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],4008           |
0040FA11 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0040FA14 | 51               | push ecx                                 |
0040FA15 | 8B55 DC          | mov edx,dword ptr ss:[ebp-24]            |
0040FA18 | 52               | push edx                                 |
0040FA19 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040FA1F | 50               | push eax                                 |
0040FA20 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FA23 | 51               | push ecx                                 |
0040FA24 | FF15 64104000    | call dword ptr ds:[<Ordinal#632>]        |
0040FA2A | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FA2D | 52               | push edx                                 |
0040FA2E | FF15 10104000    | call dword ptr ds:[<__vbaStrVarMove>]    |
0040FA34 | 8BD0             | mov edx,eax                              |
0040FA36 | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FA39 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FA3F | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FA42 | 50               | push eax                                 |
0040FA43 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0040FA46 | 51               | push ecx                                 |
0040FA47 | 6A 02            | push 2                                   |
0040FA49 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040FA4F | 83C4 0C          | add esp,C                                |
0040FA52 | C745 FC 08000000 | mov dword ptr ss:[ebp-4],8               |
0040FA59 | 8B55 D8          | mov edx,dword ptr ss:[ebp-28]            |
0040FA5C | 52               | push edx                                 |
0040FA5D | 8B45 D4          | mov eax,dword ptr ss:[ebp-2C]            |
0040FA60 | 50               | push eax                                 |
0040FA61 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FA67 | 8BD0             | mov edx,eax                              |
0040FA69 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040FA6C | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FA72 | C745 FC 09000000 | mov dword ptr ss:[ebp-4],9               | 09:'\t'
0040FA79 | E9 4AFFFFFF      | jmp [k]gme.40F9C8                        |
0040FA7E | C745 FC 0A000000 | mov dword ptr ss:[ebp-4],A               | 0A:'\n'
0040FA85 | BA 40D84000      | mov edx,[k]gme.40D840                    | 40D840:L"42"
0040FA8A | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FA8D | FF15 D4104000    | call dword ptr ds:[<__vbaStrCopy>]       |
0040FA93 | C745 FC 0B000000 | mov dword ptr ss:[ebp-4],B               | 0B:'\v'
0040FA9A | C785 14FFFFFF 05 | mov dword ptr ss:[ebp-EC],5              |
0040FAA4 | C785 18FFFFFF 01 | mov dword ptr ss:[ebp-E8],1              | [ebp-E8]:_NtUserPeekMessage@24+C
0040FAAE | C745 DC 01000000 | mov dword ptr ss:[ebp-24],1              |
0040FAB5 | EB 12            | jmp [k]gme.40FAC9                        |
0040FAB7 | 8B4D DC          | mov ecx,dword ptr ss:[ebp-24]            | l
0040FABA | 038D 18FFFFFF    | add ecx,dword ptr ss:[ebp-E8]            | [ebp-E8]:_NtUserPeekMessage@24+C
0040FAC0 | 0F80 950A0000    | jo [k]gme.41055B                         |
0040FAC6 | 894D DC          | mov dword ptr ss:[ebp-24],ecx            |
0040FAC9 | 8B55 DC          | mov edx,dword ptr ss:[ebp-24]            |
0040FACC | 3B95 14FFFFFF    | cmp edx,dword ptr ss:[ebp-EC]            |
0040FAD2 | 0F8F 07010000    | jg [k]gme.40FBDF                         |
0040FAD8 | C745 FC 0C000000 | mov dword ptr ss:[ebp-4],C               | 0C:'\f'
0040FADF | C745 B4 01000000 | mov dword ptr ss:[ebp-4C],1              |
0040FAE6 | C745 AC 02000000 | mov dword ptr ss:[ebp-54],2              |
0040FAED | 8B45 1C          | mov eax,dword ptr ss:[ebp+1C]            | [ebp+1C]:"h@€"
0040FAF0 | 8985 74FFFFFF    | mov dword ptr ss:[ebp-8C],eax            | [ebp-8C]:IsWindow+6E
0040FAF6 | C785 6CFFFFFF 03 | mov dword ptr ss:[ebp-94],4003           |
0040FB00 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0040FB03 | 51               | push ecx                                 |
0040FB04 | 8B55 DC          | mov edx,dword ptr ss:[ebp-24]            |
0040FB07 | 52               | push edx                                 |
0040FB08 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040FB0E | 50               | push eax                                 |
0040FB0F | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FB12 | 51               | push ecx                                 |
0040FB13 | FF15 64104000    | call dword ptr ds:[<Ordinal#632>]        |
0040FB19 | 8B55 1C          | mov edx,dword ptr ss:[ebp+1C]            | [ebp+1C]:"h@€"
0040FB1C | 8995 54FFFFFF    | mov dword ptr ss:[ebp-AC],edx            |
0040FB22 | C785 4CFFFFFF 03 | mov dword ptr ss:[ebp-B4],4003           | [ebp-B4]:SendDlgItemMessageW+F94
0040FB2C | 6A 01            | push 1                                   |
0040FB2E | 8D85 4CFFFFFF    | lea eax,dword ptr ss:[ebp-B4]            | [ebp-B4]:SendDlgItemMessageW+F94
0040FB34 | 50               | push eax                                 |
0040FB35 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040FB38 | 51               | push ecx                                 |
0040FB39 | FF15 0C114000    | call dword ptr ds:[<Ordinal#619>]        |
0040FB3F | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FB42 | 52               | push edx                                 |
0040FB43 | FF15 24114000    | call dword ptr ds:[<__vbaI4ErrVar>]      |
0040FB49 | 8BF0             | mov esi,eax                              |
0040FB4B | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040FB4E | 50               | push eax                                 |
0040FB4F | FF15 24114000    | call dword ptr ds:[<__vbaI4ErrVar>]      |
0040FB55 | 03F0             | add esi,eax                              |
0040FB57 | 0F80 FE090000    | jo [k]gme.41055B                         |
0040FB5D | 8B4D 34          | mov ecx,dword ptr ss:[ebp+34]            | [ebp+34]:SoftModalMessageBox+6F3
0040FB60 | 0FAF31           | imul esi,dword ptr ds:[ecx]              |
0040FB63 | 0F80 F2090000    | jo [k]gme.41055B                         |
0040FB69 | 8975 84          | mov dword ptr ss:[ebp-7C],esi            |
0040FB6C | C785 7CFFFFFF 03 | mov dword ptr ss:[ebp-84],3              | [ebp-84]:&L"MM d dddd"
0040FB76 | 8B55 D4          | mov edx,dword ptr ss:[ebp-2C]            |
0040FB79 | 52               | push edx                                 |
0040FB7A | 8D85 7CFFFFFF    | lea eax,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FB80 | 50               | push eax                                 |
0040FB81 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040FB87 | 8BD0             | mov edx,eax                              |
0040FB89 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FB8C | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FB92 | 50               | push eax                                 |
0040FB93 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FB99 | 8BD0             | mov edx,eax                              |
0040FB9B | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FB9E | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FBA4 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FBA7 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0040FBAD | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FBB3 | 51               | push ecx                                 |
0040FBB4 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040FBB7 | 52               | push edx                                 |
0040FBB8 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040FBBB | 50               | push eax                                 |
0040FBBC | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FBBF | 51               | push ecx                                 |
0040FBC0 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FBC3 | 52               | push edx                                 |
0040FBC4 | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
0040FBC7 | 50               | push eax                                 |
0040FBC8 | 6A 06            | push 6                                   |
0040FBCA | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040FBD0 | 83C4 1C          | add esp,1C                               |
0040FBD3 | C745 FC 0D000000 | mov dword ptr ss:[ebp-4],D               | 0D:'\r'
0040FBDA | E9 D8FEFFFF      | jmp [k]gme.40FAB7                        |
0040FBDF | C745 FC 0E000000 | mov dword ptr ss:[ebp-4],E               |
0040FBE6 | 8B4D D4          | mov ecx,dword ptr ss:[ebp-2C]            |
0040FBE9 | 51               | push ecx                                 |
0040FBEA | 8B55 D8          | mov edx,dword ptr ss:[ebp-28]            |
0040FBED | 52               | push edx                                 |
0040FBEE | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FBF4 | 8BD0             | mov edx,eax                              |
0040FBF6 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040FBF9 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FBFF | C745 FC 0F000000 | mov dword ptr ss:[ebp-4],F               |
0040FC06 | 8B45 0C          | mov eax,dword ptr ss:[ebp+C]             |
0040FC09 | 8985 74FFFFFF    | mov dword ptr ss:[ebp-8C],eax            | [ebp-8C]:IsWindow+6E
0040FC0F | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],4008           |
0040FC19 | 6A 01            | push 1                                   |
0040FC1B | 8D8D 6CFFFFFF    | lea ecx,dword ptr ss:[ebp-94]            |
0040FC21 | 51               | push ecx                                 |
0040FC22 | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
0040FC25 | 52               | push edx                                 |
0040FC26 | FF15 0C114000    | call dword ptr ds:[<Ordinal#619>]        |
0040FC2C | 8B45 0C          | mov eax,dword ptr ss:[ebp+C]             |
0040FC2F | 8985 64FFFFFF    | mov dword ptr ss:[ebp-9C],eax            | [ebp-9C]:&L"MM d dddd"
0040FC35 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],4008           |
0040FC3F | 6A 01            | push 1                                   |
0040FC41 | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040FC47 | 51               | push ecx                                 |
0040FC48 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FC4B | 52               | push edx                                 |
0040FC4C | FF15 00114000    | call dword ptr ds:[<Ordinal#617>]        |
0040FC52 | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
0040FC55 | 50               | push eax                                 |
0040FC56 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FC59 | 51               | push ecx                                 |
0040FC5A | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0040FC60 | 50               | push eax                                 |
0040FC61 | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FC67 | 66:8BF0          | mov si,ax                                |
0040FC6A | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FC6D | 52               | push edx                                 |
0040FC6E | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FC71 | 50               | push eax                                 |
0040FC72 | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0040FC78 | 50               | push eax                                 |
0040FC79 | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FC7F | 66:03F0          | add si,ax                                |
0040FC82 | 0F80 D3080000    | jo [k]gme.41055B                         |
0040FC88 | 66:8975 94       | mov word ptr ss:[ebp-6C],si              |
0040FC8C | C745 8C 02000000 | mov dword ptr ss:[ebp-74],2              |
0040FC93 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040FC96 | 51               | push ecx                                 |
0040FC97 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040FC9D | 8BD0             | mov edx,eax                              |
0040FC9F | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FCA2 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FCA8 | 8D55 CC          | lea edx,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FCAB | 52               | push edx                                 |
0040FCAC | 8D45 D0          | lea eax,dword ptr ss:[ebp-30]            |
0040FCAF | 50               | push eax                                 |
0040FCB0 | 6A 02            | push 2                                   |
0040FCB2 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040FCB8 | 83C4 0C          | add esp,C                                |
0040FCBB | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040FCBE | 51               | push ecx                                 |
0040FCBF | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FCC2 | 52               | push edx                                 |
0040FCC3 | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
0040FCC6 | 50               | push eax                                 |
0040FCC7 | 6A 03            | push 3                                   |
0040FCC9 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040FCCF | 83C4 10          | add esp,10                               |
0040FCD2 | C745 FC 10000000 | mov dword ptr ss:[ebp-4],10              |
0040FCD9 | C745 B4 01000000 | mov dword ptr ss:[ebp-4C],1              |
0040FCE0 | C745 AC 02000000 | mov dword ptr ss:[ebp-54],2              |
0040FCE7 | 8B4D 0C          | mov ecx,dword ptr ss:[ebp+C]             |
0040FCEA | 898D 74FFFFFF    | mov dword ptr ss:[ebp-8C],ecx            | [ebp-8C]:IsWindow+6E
0040FCF0 | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],4008           |
0040FCFA | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
0040FCFD | 52               | push edx                                 |
0040FCFE | 6A 02            | push 2                                   |
0040FD00 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040FD06 | 50               | push eax                                 |
0040FD07 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FD0A | 51               | push ecx                                 |
0040FD0B | FF15 64104000    | call dword ptr ds:[<Ordinal#632>]        |
0040FD11 | C745 94 01000000 | mov dword ptr ss:[ebp-6C],1              |
0040FD18 | C745 8C 02000000 | mov dword ptr ss:[ebp-74],2              |
0040FD1F | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FD22 | 52               | push edx                                 |
0040FD23 | 8D45 D0          | lea eax,dword ptr ss:[ebp-30]            |
0040FD26 | 50               | push eax                                 |
0040FD27 | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0040FD2D | 50               | push eax                                 |
0040FD2E | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FD34 | 66:8BF0          | mov si,ax                                |
0040FD37 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
0040FD3A | 51               | push ecx                                 |
0040FD3B | 8B55 0C          | mov edx,dword ptr ss:[ebp+C]             |
0040FD3E | 8B02             | mov eax,dword ptr ds:[edx]               |
0040FD40 | 50               | push eax                                 |
0040FD41 | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
0040FD47 | 83E8 01          | sub eax,1                                |
0040FD4A | 0F80 0B080000    | jo [k]gme.41055B                         |
0040FD50 | 50               | push eax                                 |
0040FD51 | 8B4D 0C          | mov ecx,dword ptr ss:[ebp+C]             |
0040FD54 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040FD56 | 52               | push edx                                 |
0040FD57 | FF15 60104000    | call dword ptr ds:[<Ordinal#631>]        |
0040FD5D | 8BD0             | mov edx,eax                              |
0040FD5F | 8D4D CC          | lea ecx,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FD62 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FD68 | 50               | push eax                                 |
0040FD69 | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FD6F | 66:03F0          | add si,ax                                |
0040FD72 | 0F80 E3070000    | jo [k]gme.41055B                         |
0040FD78 | 66:8975 84       | mov word ptr ss:[ebp-7C],si              |
0040FD7C | C785 7CFFFFFF 02 | mov dword ptr ss:[ebp-84],2              | [ebp-84]:&L"MM d dddd"
0040FD86 | 8B45 D4          | mov eax,dword ptr ss:[ebp-2C]            |
0040FD89 | 50               | push eax                                 |
0040FD8A | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FD90 | 51               | push ecx                                 |
0040FD91 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040FD97 | 8BD0             | mov edx,eax                              |
0040FD99 | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
0040FD9C | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FDA2 | 50               | push eax                                 |
0040FDA3 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FDA9 | 8BD0             | mov edx,eax                              |
0040FDAB | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FDAE | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FDB4 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
0040FDB7 | 52               | push edx                                 |
0040FDB8 | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FDBB | 50               | push eax                                 |
0040FDBC | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FDBF | 51               | push ecx                                 |
0040FDC0 | 6A 03            | push 3                                   |
0040FDC2 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040FDC8 | 83C4 10          | add esp,10                               |
0040FDCB | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FDD1 | 52               | push edx                                 |
0040FDD2 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040FDD5 | 50               | push eax                                 |
0040FDD6 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FDD9 | 51               | push ecx                                 |
0040FDDA | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
0040FDDD | 52               | push edx                                 |
0040FDDE | 6A 04            | push 4                                   |
0040FDE0 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040FDE6 | 83C4 14          | add esp,14                               |
0040FDE9 | C745 FC 11000000 | mov dword ptr ss:[ebp-4],11              |
0040FDF0 | 8B45 D4          | mov eax,dword ptr ss:[ebp-2C]            |
0040FDF3 | 50               | push eax                                 |
0040FDF4 | 8B4D D8          | mov ecx,dword ptr ss:[ebp-28]            |
0040FDF7 | 51               | push ecx                                 |
0040FDF8 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FDFE | 8BD0             | mov edx,eax                              |
0040FE00 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040FE03 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FE09 | C745 FC 12000000 | mov dword ptr ss:[ebp-4],12              |
0040FE10 | 68 4CD84000      | push [k]gme.40D84C                       | 40D84C:L"&H"
0040FE15 | 6A 02            | push 2                                   |
0040FE17 | 8B55 24          | mov edx,dword ptr ss:[ebp+24]            |
0040FE1A | 8B02             | mov eax,dword ptr ds:[edx]               |
0040FE1C | 50               | push eax                                 |
0040FE1D | FF15 FC104000    | call dword ptr ds:[<Ordinal#616>]        |
0040FE23 | 8BD0             | mov edx,eax                              |
0040FE25 | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
0040FE28 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FE2E | 50               | push eax                                 |
0040FE2F | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FE35 | 8BD0             | mov edx,eax                              |
0040FE37 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
0040FE3A | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FE40 | 50               | push eax                                 |
0040FE41 | FF15 28114000    | call dword ptr ds:[<Ordinal#581>]        |
0040FE47 | DD9D 34FFFFFF    | fstp qword ptr ss:[ebp-CC]               |
0040FE4D | 68 4CD84000      | push [k]gme.40D84C                       | 40D84C:L"&H"
0040FE52 | 6A 02            | push 2                                   |
0040FE54 | 8B4D 28          | mov ecx,dword ptr ss:[ebp+28]            |
0040FE57 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0040FE59 | 52               | push edx                                 |
0040FE5A | FF15 FC104000    | call dword ptr ds:[<Ordinal#616>]        |
0040FE60 | 8BD0             | mov edx,eax                              |
0040FE62 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FE65 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FE6B | 50               | push eax                                 |
0040FE6C | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FE72 | 8BD0             | mov edx,eax                              |
0040FE74 | 8D4D CC          | lea ecx,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FE77 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FE7D | 50               | push eax                                 |
0040FE7E | FF15 28114000    | call dword ptr ds:[<Ordinal#581>]        |
0040FE84 | DCA5 34FFFFFF    | fsub qword ptr ss:[ebp-CC]               |
0040FE8A | DD5D B4          | fstp qword ptr ss:[ebp-4C]               |
0040FE8D | DFE0             | fnstsw ax                                |
0040FE8F | A8 0D            | test al,D                                |
0040FE91 | 0F85 BF060000    | jne [k]gme.410556                        |
0040FE97 | C745 AC 05000000 | mov dword ptr ss:[ebp-54],5              |
0040FE9E | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
0040FEA1 | 50               | push eax                                 |
0040FEA2 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040FEA8 | 8BD0             | mov edx,eax                              |
0040FEAA | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0040FEAD | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FEB3 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
0040FEB6 | 51               | push ecx                                 |
0040FEB7 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
0040FEBA | 52               | push edx                                 |
0040FEBB | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FEBE | 50               | push eax                                 |
0040FEBF | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FEC2 | 51               | push ecx                                 |
0040FEC3 | 6A 04            | push 4                                   |
0040FEC5 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040FECB | 83C4 14          | add esp,14                               |
0040FECE | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0040FED1 | FF15 08104000    | call dword ptr ds:[<__vbaFreeVar>]       |
0040FED7 | C745 FC 13000000 | mov dword ptr ss:[ebp-4],13              |
0040FEDE | 8B55 14          | mov edx,dword ptr ss:[ebp+14]            |
0040FEE1 | 8995 74FFFFFF    | mov dword ptr ss:[ebp-8C],edx            | [ebp-8C]:IsWindow+6E
0040FEE7 | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],4008           |
0040FEF1 | 6A 01            | push 1                                   |
0040FEF3 | 8D85 6CFFFFFF    | lea eax,dword ptr ss:[ebp-94]            |
0040FEF9 | 50               | push eax                                 |
0040FEFA | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0040FEFD | 51               | push ecx                                 |
0040FEFE | FF15 00114000    | call dword ptr ds:[<Ordinal#617>]        |
0040FF04 | C745 A4 01000000 | mov dword ptr ss:[ebp-5C],1              |
0040FF0B | C745 9C 02000000 | mov dword ptr ss:[ebp-64],2              | [ebp-64]:RegisterClassW+ED
0040FF12 | 8B55 14          | mov edx,dword ptr ss:[ebp+14]            |
0040FF15 | 8995 64FFFFFF    | mov dword ptr ss:[ebp-9C],edx            | [ebp-9C]:&L"MM d dddd"
0040FF1B | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],4008           |
0040FF25 | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FF28 | 50               | push eax                                 |
0040FF29 | 6A 02            | push 2                                   |
0040FF2B | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
0040FF31 | 51               | push ecx                                 |
0040FF32 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040FF35 | 52               | push edx                                 |
0040FF36 | FF15 64104000    | call dword ptr ds:[<Ordinal#632>]        |
0040FF3C | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
0040FF3F | 50               | push eax                                 |
0040FF40 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FF43 | 51               | push ecx                                 |
0040FF44 | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0040FF4A | 50               | push eax                                 |
0040FF4B | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FF51 | 66:8BF0          | mov si,ax                                |
0040FF54 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0040FF57 | 52               | push edx                                 |
0040FF58 | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FF5B | 50               | push eax                                 |
0040FF5C | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0040FF62 | 50               | push eax                                 |
0040FF63 | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0040FF69 | 66:03F0          | add si,ax                                |
0040FF6C | 0F80 E9050000    | jo [k]gme.41055B                         |
0040FF72 | 66:8975 84       | mov word ptr ss:[ebp-7C],si              |
0040FF76 | C785 7CFFFFFF 02 | mov dword ptr ss:[ebp-84],2              | [ebp-84]:&L"MM d dddd"
0040FF80 | 8B4D D8          | mov ecx,dword ptr ss:[ebp-28]            |
0040FF83 | 51               | push ecx                                 |
0040FF84 | 8B55 D4          | mov edx,dword ptr ss:[ebp-2C]            |
0040FF87 | 52               | push edx                                 |
0040FF88 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FF8E | 8BD0             | mov edx,eax                              |
0040FF90 | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
0040FF93 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FF99 | 50               | push eax                                 |
0040FF9A | 8D85 7CFFFFFF    | lea eax,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FFA0 | 50               | push eax                                 |
0040FFA1 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
0040FFA7 | 8BD0             | mov edx,eax                              |
0040FFA9 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
0040FFAC | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FFB2 | 50               | push eax                                 |
0040FFB3 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
0040FFB9 | 8BD0             | mov edx,eax                              |
0040FFBB | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0040FFBE | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
0040FFC4 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
0040FFC7 | 51               | push ecx                                 |
0040FFC8 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
0040FFCB | 52               | push edx                                 |
0040FFCC | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0040FFCF | 50               | push eax                                 |
0040FFD0 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
0040FFD3 | 51               | push ecx                                 |
0040FFD4 | 6A 04            | push 4                                   |
0040FFD6 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
0040FFDC | 83C4 14          | add esp,14                               |
0040FFDF | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
0040FFE5 | 52               | push edx                                 |
0040FFE6 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0040FFE9 | 50               | push eax                                 |
0040FFEA | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0040FFED | 51               | push ecx                                 |
0040FFEE | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
0040FFF1 | 52               | push edx                                 |
0040FFF2 | 6A 04            | push 4                                   |
0040FFF4 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0040FFFA | 83C4 14          | add esp,14                               |
0040FFFD | C745 FC 14000000 | mov dword ptr ss:[ebp-4],14              |
00410004 | 8B45 18          | mov eax,dword ptr ss:[ebp+18]            |
00410007 | 8985 74FFFFFF    | mov dword ptr ss:[ebp-8C],eax            | [ebp-8C]:IsWindow+6E
0041000D | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],4008           |
00410017 | 6A 01            | push 1                                   |
00410019 | 8D8D 6CFFFFFF    | lea ecx,dword ptr ss:[ebp-94]            |
0041001F | 51               | push ecx                                 |
00410020 | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
00410023 | 52               | push edx                                 |
00410024 | FF15 00114000    | call dword ptr ds:[<Ordinal#617>]        |
0041002A | 8B45 18          | mov eax,dword ptr ss:[ebp+18]            |
0041002D | 8985 64FFFFFF    | mov dword ptr ss:[ebp-9C],eax            | [ebp-9C]:&L"MM d dddd"
00410033 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],4008           |
0041003D | 6A 01            | push 1                                   |
0041003F | 8D8D 5CFFFFFF    | lea ecx,dword ptr ss:[ebp-A4]            |
00410045 | 51               | push ecx                                 |
00410046 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
00410049 | 52               | push edx                                 |
0041004A | FF15 0C114000    | call dword ptr ds:[<Ordinal#619>]        |
00410050 | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
00410053 | 50               | push eax                                 |
00410054 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
00410057 | 51               | push ecx                                 |
00410058 | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
0041005E | 50               | push eax                                 |
0041005F | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
00410065 | 66:8BF0          | mov si,ax                                |
00410068 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0041006B | 52               | push edx                                 |
0041006C | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
0041006F | 50               | push eax                                 |
00410070 | FF15 AC104000    | call dword ptr ds:[<__vbaStrVarVal>]     |
00410076 | 50               | push eax                                 |
00410077 | FF15 20104000    | call dword ptr ds:[<Ordinal#516>]        |
0041007D | 66:03F0          | add si,ax                                |
00410080 | 0F80 D5040000    | jo [k]gme.41055B                         |
00410086 | 0FBFCE           | movsx ecx,si                             |
00410089 | 8B55 34          | mov edx,dword ptr ss:[ebp+34]            | [ebp+34]:SoftModalMessageBox+6F3
0041008C | 0FAF0A           | imul ecx,dword ptr ds:[edx]              |
0041008F | 0F80 C6040000    | jo [k]gme.41055B                         |
00410095 | 894D 94          | mov dword ptr ss:[ebp-6C],ecx            |
00410098 | C745 8C 03000000 | mov dword ptr ss:[ebp-74],3              |
0041009F | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
004100A2 | 50               | push eax                                 |
004100A3 | FF15 C8104000    | call dword ptr ds:[<Ordinal#572>]        |
004100A9 | 8BD0             | mov edx,eax                              |
004100AB | 8D4D C8          | lea ecx,dword ptr ss:[ebp-38]            |
004100AE | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
004100B4 | 50               | push eax                                 |
004100B5 | 8B4D D8          | mov ecx,dword ptr ss:[ebp-28]            |
004100B8 | 51               | push ecx                                 |
004100B9 | FF15 2C104000    | call dword ptr ds:[<__vbaStrCat>]        |
004100BF | 8BD0             | mov edx,eax                              |
004100C1 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
004100C4 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
004100CA | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
004100CD | 52               | push edx                                 |
004100CE | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
004100D1 | 50               | push eax                                 |
004100D2 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
004100D5 | 51               | push ecx                                 |
004100D6 | 6A 03            | push 3                                   |
004100D8 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
004100DE | 83C4 10          | add esp,10                               |
004100E1 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
004100E4 | 52               | push edx                                 |
004100E5 | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
004100E8 | 50               | push eax                                 |
004100E9 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
004100EC | 51               | push ecx                                 |
004100ED | 6A 03            | push 3                                   |
004100EF | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
004100F5 | 83C4 10          | add esp,10                               |
004100F8 | C745 FC 15000000 | mov dword ptr ss:[ebp-4],15              |
004100FF | 8B55 10          | mov edx,dword ptr ss:[ebp+10]            |
00410102 | 8B02             | mov eax,dword ptr ds:[edx]               |
00410104 | 50               | push eax                                 |
00410105 | FF15 70104000    | call dword ptr ds:[<Ordinal#527>]        |
0041010B | 8BD0             | mov edx,eax                              |
0041010D | 8B4D 10          | mov ecx,dword ptr ss:[ebp+10]            |
00410110 | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
00410116 | C745 FC 16000000 | mov dword ptr ss:[ebp-4],16              |
0041011D | 8B4D D8          | mov ecx,dword ptr ss:[ebp-28]            |
00410120 | 51               | push ecx                                 |
00410121 | FF15 70104000    | call dword ptr ds:[<Ordinal#527>]        |
00410127 | 8BD0             | mov edx,eax                              |
00410129 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
0041012C | FF15 08114000    | call dword ptr ds:[<__vbaStrMove>]       |
00410132 | C745 FC 17000000 | mov dword ptr ss:[ebp-4],17              |
00410139 | 8B55 10          | mov edx,dword ptr ss:[ebp+10]            |
0041013C | 8B02             | mov eax,dword ptr ds:[edx]               |
0041013E | 50               | push eax                                 |
0041013F | 8B4D D8          | mov ecx,dword ptr ss:[ebp-28]            |
00410142 | 51               | push ecx                                 |
00410143 | FF15 74104000    | call dword ptr ds:[<__vbaStrCmp>]        | 最后比较
00410149 | 85C0             | test eax,eax                             |
0041014B | 0F84 D6020000    | je <[k]gme.Success>                      |
00410151 | C745 FC 18000000 | mov dword ptr ss:[ebp-4],18              | Fail
00410158 | C745 84 04000280 | mov dword ptr ss:[ebp-7C],80020004       |
0041015F | C785 7CFFFFFF 0A | mov dword ptr ss:[ebp-84],A              | [ebp-84]:&L"MM d dddd", 0A:'\n'
00410169 | C745 94 04000280 | mov dword ptr ss:[ebp-6C],80020004       |
00410170 | C745 8C 0A000000 | mov dword ptr ss:[ebp-74],A              | 0A:'\n'
00410177 | C785 64FFFFFF A8 | mov dword ptr ss:[ebp-9C],[k]gme.40D8A8  | [ebp-9C]:&L"MM d dddd", 40D8A8:L"[K]eygenMe"
00410181 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],8              |
0041018B | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
00410191 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
00410194 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
0041019A | C785 74FFFFFF 58 | mov dword ptr ss:[ebp-8C],[k]gme.40D858  | [ebp-8C]:IsWindow+6E, 40D858:L"Noup, wrong serial number. Try again."
004101A4 | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],8              |
004101AE | 8D95 6CFFFFFF    | lea edx,dword ptr ss:[ebp-94]            |
004101B4 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
004101B7 | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
004101BD | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
004101C3 | 52               | push edx                                 |
004101C4 | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
004101C7 | 50               | push eax                                 |
004101C8 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
004101CB | 51               | push ecx                                 |
004101CC | 6A 40            | push 40                                  |
004101CE | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
004101D1 | 52               | push edx                                 |
004101D2 | FF15 48104000    | call dword ptr ds:[<Ordinal#595>]        |
004101D8 | 8D85 7CFFFFFF    | lea eax,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
004101DE | 50               | push eax                                 |
004101DF | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
004101E2 | 51               | push ecx                                 |
004101E3 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
004101E6 | 52               | push edx                                 |
004101E7 | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
004101EA | 50               | push eax                                 |
004101EB | 6A 04            | push 4                                   |
004101ED | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
004101F3 | 83C4 14          | add esp,14                               |
004101F6 | C745 FC 19000000 | mov dword ptr ss:[ebp-4],19              |
004101FD | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
00410200 | 8B11             | mov edx,dword ptr ds:[ecx]               |
00410202 | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
00410205 | 50               | push eax                                 |
00410206 | FF92 10030000    | call dword ptr ds:[edx+310]              |
0041020C | 50               | push eax                                 |
0041020D | 8D4D C0          | lea ecx,dword ptr ss:[ebp-40]            |
00410210 | 51               | push ecx                                 |
00410211 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
00410217 | 8985 30FFFFFF    | mov dword ptr ss:[ebp-D0],eax            |
0041021D | 6A 00            | push 0                                   |
0041021F | 8B95 30FFFFFF    | mov edx,dword ptr ss:[ebp-D0]            |
00410225 | 8B02             | mov eax,dword ptr ds:[edx]               |
00410227 | 8B8D 30FFFFFF    | mov ecx,dword ptr ss:[ebp-D0]            |
0041022D | 51               | push ecx                                 |
0041022E | FF90 14010000    | call dword ptr ds:[eax+114]              |
00410234 | DBE2             | fnclex                                   |
00410236 | 8985 2CFFFFFF    | mov dword ptr ss:[ebp-D4],eax            |
0041023C | 83BD 2CFFFFFF 00 | cmp dword ptr ss:[ebp-D4],0              |
00410243 | 7D 26            | jge [k]gme.41026B                        |
00410245 | 68 14010000      | push 114                                 |
0041024A | 68 F8D64000      | push [k]gme.40D6F8                       |
0041024F | 8B95 30FFFFFF    | mov edx,dword ptr ss:[ebp-D0]            |
00410255 | 52               | push edx                                 |
00410256 | 8B85 2CFFFFFF    | mov eax,dword ptr ss:[ebp-D4]            |
0041025C | 50               | push eax                                 |
0041025D | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
00410263 | 8985 FCFEFFFF    | mov dword ptr ss:[ebp-104],eax           |
00410269 | EB 0A            | jmp [k]gme.410275                        |
0041026B | C785 FCFEFFFF 00 | mov dword ptr ss:[ebp-104],0             |
00410275 | 8D4D C0          | lea ecx,dword ptr ss:[ebp-40]            |
00410278 | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
0041027E | C745 FC 1A000000 | mov dword ptr ss:[ebp-4],1A              |
00410285 | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
00410288 | 8B11             | mov edx,dword ptr ds:[ecx]               |
0041028A | 8B45 08          | mov eax,dword ptr ss:[ebp+8]             |
0041028D | 50               | push eax                                 |
0041028E | FF92 10030000    | call dword ptr ds:[edx+310]              |
00410294 | 50               | push eax                                 |
00410295 | 8D4D BC          | lea ecx,dword ptr ss:[ebp-44]            | [ebp-44]:EndDialog+439
00410298 | 51               | push ecx                                 |
00410299 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
0041029F | 8985 28FFFFFF    | mov dword ptr ss:[ebp-D8],eax            |
004102A5 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
004102A8 | 8B02             | mov eax,dword ptr ds:[edx]               |
004102AA | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
004102AD | 51               | push ecx                                 |
004102AE | FF90 10030000    | call dword ptr ds:[eax+310]              |
004102B4 | 50               | push eax                                 |
004102B5 | 8D55 C0          | lea edx,dword ptr ss:[ebp-40]            |
004102B8 | 52               | push edx                                 |
004102B9 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
004102BF | 8985 30FFFFFF    | mov dword ptr ss:[ebp-D0],eax            |
004102C5 | 8D45 D0          | lea eax,dword ptr ss:[ebp-30]            |
004102C8 | 50               | push eax                                 |
004102C9 | 8B8D 30FFFFFF    | mov ecx,dword ptr ss:[ebp-D0]            |
004102CF | 8B11             | mov edx,dword ptr ds:[ecx]               |
004102D1 | 8B85 30FFFFFF    | mov eax,dword ptr ss:[ebp-D0]            |
004102D7 | 50               | push eax                                 |
004102D8 | FF92 A0000000    | call dword ptr ds:[edx+A0]               |
004102DE | DBE2             | fnclex                                   |
004102E0 | 8985 2CFFFFFF    | mov dword ptr ss:[ebp-D4],eax            |
004102E6 | 83BD 2CFFFFFF 00 | cmp dword ptr ss:[ebp-D4],0              |
004102ED | 7D 26            | jge [k]gme.410315                        |
004102EF | 68 A0000000      | push A0                                  |
004102F4 | 68 F8D64000      | push [k]gme.40D6F8                       |
004102F9 | 8B8D 30FFFFFF    | mov ecx,dword ptr ss:[ebp-D0]            |
004102FF | 51               | push ecx                                 |
00410300 | 8B95 2CFFFFFF    | mov edx,dword ptr ss:[ebp-D4]            |
00410306 | 52               | push edx                                 |
00410307 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0041030D | 8985 F8FEFFFF    | mov dword ptr ss:[ebp-108],eax           |
00410313 | EB 0A            | jmp [k]gme.41031F                        |
00410315 | C785 F8FEFFFF 00 | mov dword ptr ss:[ebp-108],0             |
0041031F | 8B45 D0          | mov eax,dword ptr ss:[ebp-30]            |
00410322 | 50               | push eax                                 |
00410323 | FF15 0C104000    | call dword ptr ds:[<__vbaLenBstr>]       |
00410329 | 50               | push eax                                 |
0041032A | 8B8D 28FFFFFF    | mov ecx,dword ptr ss:[ebp-D8]            |
00410330 | 8B11             | mov edx,dword ptr ds:[ecx]               |
00410332 | 8B85 28FFFFFF    | mov eax,dword ptr ss:[ebp-D8]            |
00410338 | 50               | push eax                                 |
00410339 | FF92 1C010000    | call dword ptr ds:[edx+11C]              |
0041033F | DBE2             | fnclex                                   |
00410341 | 8985 24FFFFFF    | mov dword ptr ss:[ebp-DC],eax            |
00410347 | 83BD 24FFFFFF 00 | cmp dword ptr ss:[ebp-DC],0              |
0041034E | 7D 26            | jge [k]gme.410376                        |
00410350 | 68 1C010000      | push 11C                                 |
00410355 | 68 F8D64000      | push [k]gme.40D6F8                       |
0041035A | 8B8D 28FFFFFF    | mov ecx,dword ptr ss:[ebp-D8]            |
00410360 | 51               | push ecx                                 |
00410361 | 8B95 24FFFFFF    | mov edx,dword ptr ss:[ebp-DC]            |
00410367 | 52               | push edx                                 |
00410368 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
0041036E | 8985 F4FEFFFF    | mov dword ptr ss:[ebp-10C],eax           |
00410374 | EB 0A            | jmp [k]gme.410380                        |
00410376 | C785 F4FEFFFF 00 | mov dword ptr ss:[ebp-10C],0             |
00410380 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
00410383 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
00410389 | 8D45 BC          | lea eax,dword ptr ss:[ebp-44]            | [ebp-44]:EndDialog+439
0041038C | 50               | push eax                                 |
0041038D | 8D4D C0          | lea ecx,dword ptr ss:[ebp-40]            |
00410390 | 51               | push ecx                                 |
00410391 | 6A 02            | push 2                                   |
00410393 | FF15 1C104000    | call dword ptr ds:[<__vbaFreeObjList>]   |
00410399 | 83C4 0C          | add esp,C                                |
0041039C | C745 FC 1B000000 | mov dword ptr ss:[ebp-4],1B              |
004103A3 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
004103A6 | 8B02             | mov eax,dword ptr ds:[edx]               |
004103A8 | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
004103AB | 51               | push ecx                                 |
004103AC | FF90 10030000    | call dword ptr ds:[eax+310]              |
004103B2 | 50               | push eax                                 |
004103B3 | 8D55 C0          | lea edx,dword ptr ss:[ebp-40]            |
004103B6 | 52               | push edx                                 |
004103B7 | FF15 44104000    | call dword ptr ds:[<__vbaObjSet>]        |
004103BD | 8985 30FFFFFF    | mov dword ptr ss:[ebp-D0],eax            |
004103C3 | 8B85 30FFFFFF    | mov eax,dword ptr ss:[ebp-D0]            |
004103C9 | 8B08             | mov ecx,dword ptr ds:[eax]               |
004103CB | 8B95 30FFFFFF    | mov edx,dword ptr ss:[ebp-D0]            |
004103D1 | 52               | push edx                                 |
004103D2 | FF91 04020000    | call dword ptr ds:[ecx+204]              |
004103D8 | DBE2             | fnclex                                   |
004103DA | 8985 2CFFFFFF    | mov dword ptr ss:[ebp-D4],eax            |
004103E0 | 83BD 2CFFFFFF 00 | cmp dword ptr ss:[ebp-D4],0              |
004103E7 | 7D 26            | jge [k]gme.41040F                        |
004103E9 | 68 04020000      | push 204                                 |
004103EE | 68 F8D64000      | push [k]gme.40D6F8                       |
004103F3 | 8B85 30FFFFFF    | mov eax,dword ptr ss:[ebp-D0]            |
004103F9 | 50               | push eax                                 |
004103FA | 8B8D 2CFFFFFF    | mov ecx,dword ptr ss:[ebp-D4]            |
00410400 | 51               | push ecx                                 |
00410401 | FF15 34104000    | call dword ptr ds:[<__vbaHresultCheckObj |
00410407 | 8985 F0FEFFFF    | mov dword ptr ss:[ebp-110],eax           | [ebp-110]:__vbaVarCat+1780
0041040D | EB 0A            | jmp [k]gme.410419                        |
0041040F | C785 F0FEFFFF 00 | mov dword ptr ss:[ebp-110],0             | [ebp-110]:__vbaVarCat+1780
00410419 | 8D4D C0          | lea ecx,dword ptr ss:[ebp-40]            |
0041041C | FF15 1C114000    | call dword ptr ds:[<__vbaFreeObj>]       |
00410422 | E9 A5000000      | jmp [k]gme.4104CC                        |
00410427 | C745 FC 1D000000 | mov dword ptr ss:[ebp-4],1D              |
0041042E | C745 84 04000280 | mov dword ptr ss:[ebp-7C],80020004       |
00410435 | C785 7CFFFFFF 0A | mov dword ptr ss:[ebp-84],A              | [ebp-84]:&L"MM d dddd", 0A:'\n'
0041043F | C745 94 04000280 | mov dword ptr ss:[ebp-6C],80020004       |
00410446 | C745 8C 0A000000 | mov dword ptr ss:[ebp-74],A              | 0A:'\n'
0041044D | C785 64FFFFFF A8 | mov dword ptr ss:[ebp-9C],[k]gme.40D8A8  | [ebp-9C]:&L"MM d dddd", 40D8A8:L"[K]eygenMe"
00410457 | C785 5CFFFFFF 08 | mov dword ptr ss:[ebp-A4],8              |
00410461 | 8D95 5CFFFFFF    | lea edx,dword ptr ss:[ebp-A4]            |
00410467 | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
0041046A | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
00410470 | C785 74FFFFFF C4 | mov dword ptr ss:[ebp-8C],[k]gme.40D8C4  | [ebp-8C]:IsWindow+6E, 40D8C4:L"Yeap! You did it!"
0041047A | C785 6CFFFFFF 08 | mov dword ptr ss:[ebp-94],8              |
00410484 | 8D95 6CFFFFFF    | lea edx,dword ptr ss:[ebp-94]            |
0041048A | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
0041048D | FF15 F0104000    | call dword ptr ds:[<__vbaVarDup>]        |
00410493 | 8D95 7CFFFFFF    | lea edx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
00410499 | 52               | push edx                                 |
0041049A | 8D45 8C          | lea eax,dword ptr ss:[ebp-74]            |
0041049D | 50               | push eax                                 |
0041049E | 8D4D 9C          | lea ecx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
004104A1 | 51               | push ecx                                 |
004104A2 | 6A 40            | push 40                                  |
004104A4 | 8D55 AC          | lea edx,dword ptr ss:[ebp-54]            |
004104A7 | 52               | push edx                                 |
004104A8 | FF15 48104000    | call dword ptr ds:[<Ordinal#595>]        |
004104AE | 8D85 7CFFFFFF    | lea eax,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
004104B4 | 50               | push eax                                 |
004104B5 | 8D4D 8C          | lea ecx,dword ptr ss:[ebp-74]            |
004104B8 | 51               | push ecx                                 |
004104B9 | 8D55 9C          | lea edx,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
004104BC | 52               | push edx                                 |
004104BD | 8D45 AC          | lea eax,dword ptr ss:[ebp-54]            |
004104C0 | 50               | push eax                                 |
004104C1 | 6A 04            | push 4                                   |
004104C3 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
004104C9 | 83C4 14          | add esp,14                               |
004104CC | 9B               | fwait                                    |
004104CD | 68 34054100      | push [k]gme.410534                       |
004104D2 | EB 4D            | jmp [k]gme.410521                        |
004104D4 | 8D4D C4          | lea ecx,dword ptr ss:[ebp-3C]            | [ebp-3C]:&L"MM d dddd"
004104D7 | 51               | push ecx                                 |
004104D8 | 8D55 C8          | lea edx,dword ptr ss:[ebp-38]            |
004104DB | 52               | push edx                                 |
004104DC | 8D45 CC          | lea eax,dword ptr ss:[ebp-34]            | [ebp-34]:"Pj"
004104DF | 50               | push eax                                 |
004104E0 | 8D4D D0          | lea ecx,dword ptr ss:[ebp-30]            |
004104E3 | 51               | push ecx                                 |
004104E4 | 6A 04            | push 4                                   |
004104E6 | FF15 D8104000    | call dword ptr ds:[<__vbaFreeStrList>]   |
004104EC | 83C4 14          | add esp,14                               |
004104EF | 8D55 BC          | lea edx,dword ptr ss:[ebp-44]            | [ebp-44]:EndDialog+439
004104F2 | 52               | push edx                                 |
004104F3 | 8D45 C0          | lea eax,dword ptr ss:[ebp-40]            |
004104F6 | 50               | push eax                                 |
004104F7 | 6A 02            | push 2                                   |
004104F9 | FF15 1C104000    | call dword ptr ds:[<__vbaFreeObjList>]   |
004104FF | 83C4 0C          | add esp,C                                |
00410502 | 8D8D 7CFFFFFF    | lea ecx,dword ptr ss:[ebp-84]            | [ebp-84]:&L"MM d dddd"
00410508 | 51               | push ecx                                 |
00410509 | 8D55 8C          | lea edx,dword ptr ss:[ebp-74]            |
0041050C | 52               | push edx                                 |
0041050D | 8D45 9C          | lea eax,dword ptr ss:[ebp-64]            | [ebp-64]:RegisterClassW+ED
00410510 | 50               | push eax                                 |
00410511 | 8D4D AC          | lea ecx,dword ptr ss:[ebp-54]            |
00410514 | 51               | push ecx                                 |
00410515 | 6A 04            | push 4                                   |
00410517 | FF15 14104000    | call dword ptr ds:[<__vbaFreeVarList>]   |
0041051D | 83C4 14          | add esp,14                               |
00410520 | C3               | ret                                      |
00410521 | 8D4D D8          | lea ecx,dword ptr ss:[ebp-28]            |
00410524 | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
0041052A | 8D4D D4          | lea ecx,dword ptr ss:[ebp-2C]            |
0041052D | FF15 20114000    | call dword ptr ds:[<__vbaFreeStr>]       |
00410533 | C3               | ret                                      |
00410534 | 8B55 08          | mov edx,dword ptr ss:[ebp+8]             |
00410537 | 8B02             | mov eax,dword ptr ds:[edx]               |
00410539 | 8B4D 08          | mov ecx,dword ptr ss:[ebp+8]             |
0041053C | 51               | push ecx                                 |
0041053D | FF50 08          | call dword ptr ds:[eax+8]                |
00410540 | 8B45 F0          | mov eax,dword ptr ss:[ebp-10]            | [ebp-10]:&L"MM d dddd"
00410543 | 8B4D E0          | mov ecx,dword ptr ss:[ebp-20]            |
00410546 | 64:890D 00000000 | mov dword ptr fs:[0],ecx                 |
0041054D | 5F               | pop edi                                  | edi:&L"MM d dddd"
0041054E | 5E               | pop esi                                  |
0041054F | 5B               | pop ebx                                  |
00410550 | 8BE5             | mov esp,ebp                              |
00410552 | 5D               | pop ebp                                  |
00410553 | C2 3000          | ret 30                                   |
00410556 | E9 710EFFFF      | jmp <JMP.&__vbaFPException>              |
0041055B | FF15 B8104000    | call dword ptr ds:[<__vbaErrorOverflow>] |
```


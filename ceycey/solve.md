密码: `ULTRADMA............................................................`

```assembly
004575FF | 6A 00             | push 0                                |
00457601 | 53                | push ebx                              |
00457602 | 8BD8              | mov ebx,eax                           |
00457604 | 33C0              | xor eax,eax                           |
00457606 | 55                | push ebp                              |
00457607 | 68 58764500       | push ceycey_dump_scy.457658           |
0045760C | 64:FF30           | push dword ptr fs:[eax]               |
0045760F | 64:8920           | mov dword ptr fs:[eax],esp            |
00457612 | 8D55 FC           | lea edx,dword ptr ss:[ebp-4]          |
00457615 | 8B83 D4020000     | mov eax,dword ptr ds:[ebx+2D4]        |
0045761B | E8 E8C9FCFF       | call <ceycey_dump_scy.GetText>        |
00457620 | 8B45 FC           | mov eax,dword ptr ss:[ebp-4]          | [ebp-04]:password
00457623 | BA 6C764500       | mov edx,ceycey_dump_scy.45766C        | 45766C:"ULTRADMA............................................................"
00457628 | E8 5FC6FAFF       | call <ceycey_dump_scy._LStrCmp>       |
0045762D | 75 13             | jne ceycey_dump_scy.457642            |
0045762F | 6A 00             | push 0                                |
00457631 | 68 B4764500       | push ceycey_dump_scy.4576B4           | 4576B4:"Do not think u r good"
00457636 | 68 CC764500       | push ceycey_dump_scy.4576CC           | 4576CC:"Easy huh?"
0045763B | 6A 00             | push 0                                |
0045763D | E8 52F0FAFF       | call <JMP.&MessageBoxA>               |
00457642 | 33C0              | xor eax,eax                           |
```


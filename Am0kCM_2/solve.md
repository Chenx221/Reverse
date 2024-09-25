1. 直接看Main函数

   ```assembly
   00401000 | 68 FFE77648          | push 4876E7FF                                |
   00401005 | E8 56000000          | call am0kcm_2.401060                         |
   0040100A | 83C4 04              | add esp,4                                    |
   0040100D | E8 58000000          | call <am0kcm_2.random>                       |
   00401012 | 99                   | cdq                                          |
   00401013 | 3D FFE77648          | cmp eax,4876E7FF                             | <--
   00401018 | 75 1E                | jne <am0kcm_2.Fail>                          |
   0040101A | 83FA 17              | cmp edx,17                                   | <--
   0040101D | 75 19                | jne <am0kcm_2.Fail>                          |
   0040101F | 6A 00                | push 0                                       |
   00401021 | 68 98504000          | push am0kcm_2.405098                         | 405098:"Good Job"
   00401026 | 68 68504000          | push am0kcm_2.405068                         | 405068:"You re one the good way to become a cracker ;)"
   0040102B | 6A 00                | push 0                                       |
   0040102D | FF15 94404000        | call dword ptr ds:[<MessageBoxA>]            |
   00401033 | 33C0                 | xor eax,eax                                  |
   00401035 | C2 1000              | ret 10                                       |
   00401038 | 6A 00                | push 0                                       |
   0040103A | 68 60504000          | push am0kcm_2.405060                         | 405060:"Bad Job"
   0040103F | 68 30504000          | push am0kcm_2.405030                         | 405030:"Hehe, don't stop trying :P\nStill not cracked.."
   00401044 | 6A 00                | push 0                                       |
   00401046 | FF15 94404000        | call dword ptr ds:[<MessageBoxA>]            |
   0040104C | 33C0                 | xor eax,eax                                  |
   0040104E | C2 1000              | ret 10                                       |
   ```

   `00401013`和`0040101A` 这两个的比较结果永远不会相等

2. 要么NOP掉两个jne跳转，要么jne改je

   这里我选择后者

   ```assembly
   00401018 | 74 1E                | je <am0kcm_2.Fail>                           |
   0040101A | 83FA 17              | cmp edx,17                                   |
   0040101D | 74 19                | je <am0kcm_2.Fail>                           |
   ```
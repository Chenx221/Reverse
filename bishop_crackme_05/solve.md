跳过了这个作者好几个vb4的crackme，因为太老了没工具...

1. 去NAG

   ```
   102BC:00->01
   1030C:01->00
   ```

2. Serial算法

   ```c#
   long result = 0;
   foreach (char c in name)
   {
       int ci = c;
       result += ci * ci;
   }
   result += result * result;
   ```

   


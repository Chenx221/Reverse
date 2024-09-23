p-code

VB Decompiler

1. 调整启动顺序

   ```
   A154: 00->04 //Form1(NAG)
   A1A4: 01->03 //Frmregister
   A244: 03->00 //frmcrack(MAIN)
   A294: 04->01 //frmAbout
   ```

2. 恢复frmcrack

   窗体Visible: False (op:2E)
   通过搜索可寻得

   ```
   22A12: 00->01 //Visible: True
   ```

3. 计算Serial（以下代码已精简处理）

   ```c#
           private static long[] GenerateKey(string name, string fname, string company)
           {
               int day = DateTime.Now.Day;
               int month = DateTime.Now.Month;
   
               long nameAsc = Convert.ToInt32(name[0]);
               long fnameAsc = Convert.ToInt32(fname[0]);
               long companyAsc = Convert.ToInt32(company[0]);
   
               int length1 = name.Length;
               int length2 = fname.Length;
               int length3 = company.Length;
   
               string part1 = (length1 < 6) ? "444" : ((length1 < 11) ? "555" : "666");
               string part2 = (length2 < 6) ? "777" : ((length2 < 11) ? "888" : "999");
               string part3 = (length3 < 6) ? "111" : ((length3 < 11) ? "222" : "333");
   
               long s1 = nameAsc * Convert.ToInt64(part1) * day / month;
               long s2 = fnameAsc * Convert.ToInt64(part2) * day / month;
               long s3 = companyAsc * Convert.ToInt64(part3) * day / month;
   
               return [s1, s2, s3];
           }
   ```

   


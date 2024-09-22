p-code

vbdec

1. 找key

   ```
   401D50    0F 0003               VCallAd Crackme.Text1
   401D53    19 70FF               FStAdFunc var_90
   401D56    08 70FF               FLdPr var_90
   401D59    0D A0000000           VCallHresult _TextBox Get _Default
   401D5E    3E 6CFF               FLdZeroAd var_94
   401D61    31 74FF               FStStr var_8C
   401D64    1A 70FF               FFree1Ad var_90
   401D67    1B 0100               LitStr str_4017F4='ExDec_Roxx' 
   401D6A    43 78FF               FStStrCopy var_88
   401D6D    6C 78FF               ILdRf [var_88]
   401D70    6C 74FF               ILdRf [var_8C]
   401D73    FB30                  EqStr
   ```

   显然，key="ExDec_Roxx"
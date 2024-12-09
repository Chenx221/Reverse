用java写的crackme

*最高只支持到Java 8，高版本上可能弹不了消息框

计算sn比解密1.gif还简单:)

先上一组可用Name&SN

```
chenx221
Y2hlbngyMjE=
```

反编译jar工具有很多，各位随意挑选即可

```java
public final void actionPerformed(ActionEvent var1) {
    String var2 = var1.getActionCommand();
    if (var2.equals(I.I(1))) { //Exit
        this.setVisible(false);
        System.exit(0);
    } else if (var2.equals(I.I(6))) { //Check
        String var3 = addActionListener.getText(); //Name
        String var4 = encode.getText(); //SN
        if (var3 == null) {
            return;
        }
        if (var4 == null) {
            return;
        }

        if (var3.length() <= 3) { //Name length needs to be greater than 3
            JOptionPane.showMessageDialog(this, I.I(12));
            return;
        }

        BASE64Encoder var5 = new BASE64Encoder();
        String var6 = var5.encode(var3.getBytes());
        byte[] var7 = var6.getBytes(); // Base64 encoded name
        byte[] var8 = var4.getBytes(); // SN
        boolean var9 = false;
        if (var7.length != var8.length) {
            JOptionPane.showMessageDialog(this, I.I(47));
            return;
        }

        for(int var10 = 0; var10 < var7.length; ++var10) { //Simply compare the two byte arrays
            var9 = var7[var10] == var8[var10];
            if (!var9) {
                JOptionPane.showMessageDialog(this, I.I(47));
                return;
            }
        }

        if (!var9) { //Uselss check
            JOptionPane.showMessageDialog(this, I.I(47));
            return;
        }

        JOptionPane.showMessageDialog(this, I.I(63));
        return;
    }

}
```

可见本crackme的SN就是Name的base64编码，Name有长度要求(>3)

随意搓一个bash64转换器或[在线工具](https://it-tools.tech/base64-string-converter)就可以解决问题



另外提一下，程序的文本都存放在I.gif里（这不是图像）

```
0000  00 01 03 07 46 7B 6A 57 06 40 6B 46 40 68 21 4D  ....F{jW.@kF@h!M 
0010  62 6E 66 23 6F 66 6D 64 77 6B 23 6E 76 70 77 23  bnf#ofmdwk#nvpw# 
0020  6E 6C 71 66 23 77 6B 62 6D 23 30 23 60 6B 62 71  nlqf#wkbm#0#`kbq 
0030  70 0C 50 66 71 6A 62 6F 23 6A 70 23 54 71 6C 6D  p.Pfqjbo#jp#Tqlm 
0040  64 19 50 66 71 6A 62 6F 23 6A 70 23 51 6A 64 6B  d.Pfqjbo#jp#Qjdk 
0050  77 22 23 5A 4C 76 51 23 4C 48 22 22 06 4D 62 6E  w"#ZLvQ#LH"".Mbn 
0060  46 39 07 50 2C 4D 39 18 49 62 75 62 23 40 71 62  F9.P,M9.Ibub#@qb 
0070  60 68 4E 66 23 20 32 23 61 7A 23 75 6B 6F 7A 58  `hNf# 2#az#ukozX 
0080  45 51 5E 6F 67 66 65 62 76 6F 77 40 6F 6C 70 66  EQ^ogfebvow@olpf 
0090  4C 73 66 71 62 77 6A 6C 6D 23 6E 76 70 77 23 61  Lsfqbwjlm#nvpw#a 
00A0  66 23 6C 6D 66 23 6C 65 39 23 47 4C 5C 4D 4C 57  f#lmf#le9#GL\MLW 
00B0  4B 4A 4D 44 5C 4C 4D 5C 40 4F 4C 50 46 2F 23 4B  KJMD\LM\@OLPF/#K 
00C0  4A 47 46 5C 4C 4D 5C 40 4F 4C 50 46 2F 23 47 4A  JGF\LM\@OLPF/#GJ 
00D0  50 53 4C 50 46 5C 4C 4D 5C 40 4F 4C 50 46 2F 23  PSLPF\LM\@OLPF/# 
00E0  6C 71 23 46 5B 4A 57 5C 4C 4D 5C 40 4F 4C 50 46  lq#F[JW\LM\@OLPF 
00F0  16 67 66 65 62 76 6F 77 40 6F 6C 70 66 4C 73 66  .gfebvow@olpfLsf 
0100  71 62 77 6A 6C 6D                                qbwjlm
```

但看起来文件被加密了，反编译寻找解密方法：

```java
InputStream var0 = (new I()).getClass().getResourceAsStream("" + 'I' + '.' + 'g' + 'i' + 'f');
if (var0 != null) {
    int var1 = var0.read() << 16 | var0.read() << 8 | var0.read();
    IYOE = new byte[var1];
    int var2 = 0;
    byte var3 = (byte)var1;
    byte[] var4 = IYOE;

    while(var1 != 0) {
        int var5 = var0.read(var4, var2, var1);
        if (var5 == -1) {
            break;
        }

        var1 -= var5;

        for(int var7 = var5 + var2; var2 < var7; ++var2) {
            var4[var2] ^= var3;
        }
    }

    var0.close();
}
```

文件结构

1. 3B 数据大小：`00 01 03` , `259`, 其中`3`加入数据部分xor运算
2. 259B 数据: 这部分数据需要XOR 3处理
   1. 1B 文本长度
   2. *B 文本数据

解密后的数据部分：

```
0000  04 45 78 69 54 05 43 68 45 43 6B 22 4E 61 6D 65  .ExiT.ChECk"Name 
0010  20 6C 65 6E 67 74 68 20 6D 75 73 74 20 6D 6F 72   length must mor 
0020  65 20 74 68 61 6E 20 33 20 63 68 61 72 73 0F 53  e than 3 chars.S 
0030  65 72 69 61 6C 20 69 73 20 57 72 6F 6E 67 1A 53  erial is Wrong.S 
0040  65 72 69 61 6C 20 69 73 20 52 69 67 68 74 21 20  erial is Right!  
0050  59 4F 75 52 20 4F 4B 21 21 05 4E 61 6D 45 3A 04  YOuR OK!!.NamE:. 
0060  53 2F 4E 3A 1B 4A 61 76 61 20 43 72 61 63 6B 4D  S/N:.Java CrackM 
0070  65 20 23 31 20 62 79 20 76 68 6C 79 5B 46 52 5D  e #1 by vhly[FR] 
0080  6C 64 65 66 61 75 6C 74 43 6C 6F 73 65 4F 70 65  ldefaultCloseOpe 
0090  72 61 74 69 6F 6E 20 6D 75 73 74 20 62 65 20 6F  ration must be o 
00A0  6E 65 20 6F 66 3A 20 44 4F 5F 4E 4F 54 48 49 4E  ne of: DO_NOTHIN 
00B0  47 5F 4F 4E 5F 43 4C 4F 53 45 2C 20 48 49 44 45  G_ON_CLOSE, HIDE 
00C0  5F 4F 4E 5F 43 4C 4F 53 45 2C 20 44 49 53 50 4F  _ON_CLOSE, DISPO 
00D0  53 45 5F 4F 4E 5F 43 4C 4F 53 45 2C 20 6F 72 20  SE_ON_CLOSE, or  
00E0  45 58 49 54 5F 4F 4E 5F 43 4C 4F 53 45 15 64 65  EXIT_ON_CLOSE.de 
00F0  66 61 75 6C 74 43 6C 6F 73 65 4F 70 65 72 61 74  faultCloseOperat 
0100  69 6F 6E                                         ion
```


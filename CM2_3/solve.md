根据name算SN

先来一组可用的name&SN:

```
Name: chenx221
SN: 14wact6
```

思路：

```
SN: CRC32(MD5("vhly[FR]" + name))结果的36进制形式

MessageDigest var6 = MessageDigest.getInstance(I.I(47)); // MD5
var6.update(I.I(51).getBytes()); // vhly[FR]
这两处字符串被藏在了I.gif里，I.gif处理方法和该crackme上一代相同
使用上次准备的读取器可以获取这些字符串：
ExiTChECk"Name length must more than 3 charsMD5vhly[FR]Serial is WrongSerial is Right! YOuR OK
```

细节：

```java
String var3 = addActionListener.getText(); // Name
String var4 = digest.getText(); // SN
if (var3 == null) {
    return;
}

if (var4 == null) {
    return;
}

if (var3.length() <= 3) {
    JOptionPane.showMessageDialog(this, I.I(12));
    return;
}

byte[] var5 = null;

try {
    MessageDigest var6 = MessageDigest.getInstance(I.I(47)); // MD5
    var6.update(I.I(51).getBytes()); // vhly[FR]
    var6.update(var3.getBytes()); // Name
    var5 = var6.digest(); // Convert vhly[FR]+Name to MD5
} catch (Exception var12) {
}

CRC32 var13 = new CRC32();
var13.update(var5);
long var7 = var13.getValue();
long var9 = Long.parseLong(var4, 36); // 36进制
boolean var11 = false;
if (var7 / var9 == 1L) {
    var11 = true;
} else {
    var11 = false;
}

if (!var11) {
    JOptionPane.showMessageDialog(this, I.I(60));
    return;
}

JOptionPane.showMessageDialog(this, I.I(76));
return;
```


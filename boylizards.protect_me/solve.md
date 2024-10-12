反向crackme?

一看是vb.net 内心狂喜

先来看一下serial是怎么生成的：

```c#
// TextBox1:Name, TextBox2:Serial
private void Button1_Click(object sender, EventArgs e)
{
    if (this.TextBox1.Text.Length <= 7)
    {
        Interaction.MsgBox("Name must be at least 8 chars!!!", MsgBoxStyle.OkOnly, null);
    }
    else
    {
        this.lengthofbox = StringType.FromInteger(this.TextBox1.Text.Length);
        this.TextBox2.Text = this.lengthofbox + this.TextBox1.Text.Remove(3, 5);
        this.TextBox3.Text = this.TextBox2.Text;
        this.string2 = StringType.FromDouble(DoubleType.FromString(this.lengthofbox) / (double)this.TextBox3.Text.Length);
        this.TextBox2.Text = this.string2 + this.TextBox3.Text;
    }
}
```

这样一来就能写出以下检查代码

```c#
static bool ValidateNameSerial(string name, string serial, ref string ErrReason)
{
    int length = name.Length;
    if (length <= 7)
    {
        ErrReason = "Name length must be greater than 7";
        return false;
    }
    else
    {
        string temp2 = length + name.Remove(3, 5);
        if ((length / (double)temp2.Length) + temp2 == serial)
            return true;
        ErrReason = "Wrong Serial";
        return false;

    }
}
```


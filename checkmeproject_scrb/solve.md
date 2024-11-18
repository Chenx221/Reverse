用nmp的smartcheck

Part1 serial:

```
SsCcAaRrAaBbEeEe
```

Part2 serial:

```
SCRB2003
```

```
Data:   SC0B3R20
Index:  12748356
Serial: SCRB2003
```

Part3 serial:

```
示例serial:
chenx221
10944392
```

```python
name = "22"
total_ascii = sum(ord(char) for char in name)
serial = total_ascii * 2003 * len(name)
print(serial)
```
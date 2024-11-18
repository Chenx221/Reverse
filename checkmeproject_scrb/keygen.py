name = "chenx221"
total_ascii = sum(ord(char) for char in name)
serial = total_ascii * 2003 * len(name)
print(serial)
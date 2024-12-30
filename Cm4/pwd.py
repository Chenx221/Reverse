def xor_and_to_ascii(hex1, hex2):
    result = [b1 ^ b2 for b1, b2 in zip(hex1, hex2)]
    result_ascii = ''.join(chr(byte) for byte in result)
    return result_ascii

hex1 = [0x4D, 0x11, 0x62, 0x8E, 0xBE, 0x1D]
hex2 = [0x34, 0x78, 0x12, 0xFE, 0xDB, 0x78]

ascii_result = xor_and_to_ascii(hex1, hex2)
print(ascii_result)

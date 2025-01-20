def generate_serial(name):
    ascii_sum = sum(ord(c) for c in name)
    return ascii_sum ^ 0x7D0 ^ 0x7BF

name = input("Enter name: ")
serial = generate_serial(name)
print(f"serial: {serial}")

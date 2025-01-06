name = input("Enter name (at least 3 characters): ")

if len(name) < 3:
    print("Error: Name must be at least 3 characters!")
else:
    result = (
        name[-1] + 
        name[0] +
        name[-2] +
        name[1] +
        name[-3] +
        name[2] +
        "-easy"
    )
    print("Serial: ", result)

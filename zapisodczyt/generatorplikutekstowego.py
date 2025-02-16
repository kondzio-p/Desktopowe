import random
#32-126

with open("duzyplik1.txt", "wt") as f:
    for i in range(500000):
        linia = f"Linia {i} "
        linia = "".join([chr(random.randint(32, 126)) for _ in range(500)])
        linia += "\n"
        f.write(linia)

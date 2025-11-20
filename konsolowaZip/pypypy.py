# lista1 = ["a", "b", "c", "d"]
# lista2 = [6, 552, 33, 444]

# if len(lista1) == len(lista2):
#     slownik = dict(zip(lista1, lista2))
#     print(slownik)

# print(type(zip(lista1, lista2)))
# krowa = zip(lista1, lista2)
# print(krowa)

# krowa2= list(zip(lista1, lista2))
# print(krowa2)

#----

# lista = [5,32,54,7,76,68,3,65]

# for idx, val in enumerate(lista, start=1):
#     print(f"Element pod indeksem {idx} to {val}")

# for idx, val in enumerate(lista, start=1):
#     if val == 68:
#         print(f"Element znajduje sie na pozycji {idx}")

#----

import json
with open("dane.json", "+r") as file:
    dane = json.load(file)

for idx, osoba in enumerate(dane, start=1):
    print(idx, ' : ', osoba["imie"], ' ', osoba['wiek'])

print(dane)
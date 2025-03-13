from random import randint

def losuj(ilosc: int):
    rzuty=[randint(1,6) for i in range(ilosc)]
    return rzuty

def sumuj(rzuty):
    suma=sum([i for i in rzuty if rzuty.count(i) >= 2])
    return suma

def wyswietl(rzuty):
    for i, rzut in enumerate(rzuty):
        print(f"Kostka {i+1}: {rzut}")

    
def main():
    ilosc = 0
    while ilosc<3 or ilosc>10:
        ilosc = int(input("Ile kości chcesz rzucić?(3-10)"))

    jeszcze_raz = True

    while jeszcze_raz:
        rzuty = losuj(ilosc)
        suma = sumuj(rzuty)
        wyswietl(rzuty)
        print("Liczba uzyskanych pinktów: ", str(suma))

        odp = input("Jeszcze raz? (t/n): ")
        if odp.lower() != 't':
            jeszcze_raz = False
        
    
if __name__ == '__main__':
    main()

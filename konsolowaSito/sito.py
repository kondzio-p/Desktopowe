from typing import List

class Czytnik:
    def __init__(self, sciezka: str):
        self.sciezka = sciezka
        self.liczby: List[int] = []

    def odczytaj_liczby(self) -> List[int]:
        self.liczby = []
        try: 
            with open(self.sciezka, 'r', encoding='utf-8') as plik:
                for linia in plik:
                    linia = linia.strip()
                    if not linia:
                        continue
                    try:
                        self.liczby.append(int(linia))
                    except ValueError:
                        pass
        except FileNotFoundError:
            pass
        return self.liczby.copy()
    
class sitoE:
    def __init__(self, maks: int):
        self.maks = maks
        self.czy_pierwsza = [True] * (maks+1)
        self.czy_pierwsza[0] = self.czy_pierwsza[1] = False
        p = 2
        while p*p <= maks:
            if self.czy_pierwsza[p]:
                for i in range(p*p, maks+1, p):
                    self.czy_pierwsza[i] = False
            p+=1
        
    def jest_prime(self, n:int) -> bool:
        return self.czy_pierwsza[n]
    
class SortowanieWybor:
    def sortuj(self, lista: List[int]) -> List[int]:
        n = len(lista)
        for i in range(n):
            min_idx = i
            for j in range(i+1, n):
                if lista[j] < lista[min_idx]:
                    min_idx = j
            lista[i], lista[min_idx] = lista[min_idx], lista[i]
        return lista
    
class zapis:
    def __init__(self, lista: List[int], sciezka:str):
        with open(sciezka, 'w', encoding='utf-8') as plik:
            for liczba in lista:
                plik.write(str(liczba) + ', ')
    
czytnik = Czytnik("dane.txt")
liczby = czytnik.odczytaj_liczby()

maks = 0
for x in liczby:
    if x > maks:
        maks = x

liczby_pierwsze = []
if maks>0:
    sito = sitoE(maks)
    for x in liczby:
        if sito.jest_prime(x):
            liczby_pierwsze.append(x)
        
sortowanie = SortowanieWybor()
posortowane_pierwsze = sortowanie.sortuj(liczby_pierwsze)

zapis = zapis(posortowane_pierwsze, 'wynik.txt')


print(f"Liczba znalezionych liczb pierwszych: {len(posortowane_pierwsze)}")
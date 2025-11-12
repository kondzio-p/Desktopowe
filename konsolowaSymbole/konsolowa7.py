def wczytaj_plik(sciezka: str):
    """Wczytuje plik i zwraca listę niepustych linii (bez znaków końca linii)."""
    with open(sciezka, 'r', encoding='utf-8') as f:
        return [linia.rstrip('\n') for linia in f if linia.strip()]

#  Słowniki do konwersji trójkowej
sym_to_digit = {'o': 0, '+': 1, '*': 2}
digit_to_sym = {0: 'o', 1: '+', 2: '*'}

#  Z 1
def zad1(linie):
    print("Zadanie_1")
    for linia in linie:
        if linia == linia[::-1]:
            print(linia)
    print("\n")

#  Z 2 –  3×3 
def zad2(linie):
    H = len(linie)
    if H < 3:
        print("Zadanie_2\n0\n")
        return

    kwadraty = []                     # lista (wiersz_środka, kolumna_środka)
    for i in range(1, H - 1):         # wiersz środka
        W = len(linie[i])
        if W < 3:
            continue
        for j in range(1, W - 1):     # kolumna środka
            s = linie[i][j]           # symbol w środku
            # sprawdzamy 3×3
            ok = True
            for di in (-1, 0, 1):
                for dj in (-1, 0, 1):
                    if linie[i + di][j + dj] != s:
                        ok = False
                        break
                if not ok:
                    break
            if ok:
                kwadraty.append((i + 1, j + 1))   # numeracja od 1

    print("Zadanie_2")
    if not kwadraty:
        print("0")
    else:
        # format:  3 399 5 546 2 630 11  (liczba_kwadratów + pary (w,k))
        parts = [str(len(kwadraty))]
        for w, k in kwadraty:
            parts.extend([str(w), str(k)])
        print(' '.join(parts))
    print("\n")

#  Z 3 – największa
def zad3(linie):
    max_val = -1
    max_napis = ""

    for napis in linie:
        val = 0
        for znak in napis:
            val = val * 3 + sym_to_digit[znak]
        if val > max_val:
            max_val = val
            max_napis = napis

    print("Zadanie_3")
    print(max_val, max_napis)
    print("\n")

#  Z 4 – suma 
def dziesietne_na_trojkowe(liczba: int) -> str:
    """Konwertuje liczbę dziesiętną na zapis trójkowy z symbolami o,+,*."""
    if liczba == 0:
        return 'o'
    digits = []
    while liczba:
        digits.append(digit_to_sym[liczba % 3])
        liczba //= 3
    return ''.join(reversed(digits))

def zad4(linie):
    suma = 0
    for napis in linie:
        val = 0
        for znak in napis:
            val = val * 3 + sym_to_digit[znak]
        suma += val

    ternary = dziesietne_na_trojkowe(suma)

    print("Zadanie_4")
    print(suma, ternary)

# --- --- ---
def main():
    plik = "symbole.txt"
    linie = wczytaj_plik(plik)

    zad1(linie)
    zad2(linie)
    zad3(linie)
    zad4(linie)

if __name__ == '__main__':
    main()
class Klub:
    def __init__(self, kraj, nazwa, rok_zalozenia, liczba_mistrzostw):
        self.kraj = kraj
        self.nazwa = nazwa
        self.rok_zalozenia = rok_zalozenia
        self.liczba_mistrzostw = liczba_mistrzostw

    def __str__(self):
        return f"{self.kraj};{self.nazwa};{self.rok_zalozenia};{self.liczba_mistrzostw}"

    # Wczytywanie z pliku
    def wczytaj_z_pliku(self, nazwa_pliku):
        kluby = []
        with open(nazwa_pliku, "r", encoding="utf-8") as f:
            for linia in f:
                linia = linia.strip()
                if linia == "":
                    continue
                dane = linia.split(";")
                kraj = dane[0]
                nazwa = dane[1]
                rok = int(dane[2])
                mistrz = int(dane[3])
                klub = Klub(kraj, nazwa, rok, mistrz)
                kluby.append(klub)
        return kluby

    # Quicksort po roku (rosnąco)
    def quicksort_po_roku(self, lista):
        if len(lista) <= 1:
            return lista

        pivot = lista[0]  # bierzemy pierwszy element jako pivot
        mniejsze = []
        rowne = []
        wieksze = []

        for klub in lista:
            if klub.rok_zalozenia < pivot.rok_zalozenia:
                mniejsze.append(klub)
            elif klub.rok_zalozenia == pivot.rok_zalozenia:
                rowne.append(klub)
            else:
                wieksze.append(klub)

        # Rekurencyjnie sortujemy mniejsze i większe
        posortowane = self.quicksort_po_roku(mniejsze) + rowne + self.quicksort_po_roku(wieksze)
        return posortowane

    # Quicksort po mistrzostwach (malejąco)
    def quicksort_po_mistrzostwach(self, lista):
        if len(lista) <= 1:
            return lista

        pivot = lista[0]
        wieksze = []
        rowne = []
        mniejsze = []

        for klub in lista:
            if klub.liczba_mistrzostw > pivot.liczba_mistrzostw:
                wieksze.append(klub)
            elif klub.liczba_mistrzostw == pivot.liczba_mistrzostw:
                rowne.append(klub)
            else:
                mniejsze.append(klub)

        posortowane = self.quicksort_po_mistrzostwach(wieksze) + rowne + self.quicksort_po_mistrzostwach(mniejsze)
        return posortowane

    # Wyszukiwanie binarne po roku
    def wyszukiwanie_binarne_po_roku(self, lista, rok):
        lewy = 0
        prawy = len(lista) - 1
        znalezione = []

        while lewy <= prawy:
            srodek = (lewy + prawy) // 2
            if lista[srodek].rok_zalozenia == rok:
                # szukamy wszystkich klubów o tym samym roku
                i = srodek
                while i >= 0 and lista[i].rok_zalozenia == rok:
                    znalezione.append(lista[i])
                    i -= 1

                i = srodek + 1
                while i < len(lista) and lista[i].rok_zalozenia == rok:
                    znalezione.append(lista[i])
                    i += 1
                break
            elif lista[srodek].rok_zalozenia < rok:
                lewy = srodek + 1
            else:
                prawy = srodek - 1

        return znalezione



def main():
    pomoc = Klub("", "", 0, 0) 
    kluby = pomoc.wczytaj_z_pliku("kluby.txt")

    print("Wczytano kluby:")
    for k in kluby:
        print(k)

    # sortowanie po roku (rosnąco)
    kluby_po_roku = pomoc.quicksort_po_roku(kluby)
    print("\nSortowanie po roku (rosnąco):")
    for k in kluby_po_roku:
        print(k)

    # sortowanie po liczbie mistrzostw (malejąco)
    kluby_po_mistrzostwach = pomoc.quicksort_po_mistrzostwach(kluby)
    print("\nSortowanie po liczbie mistrzostw (malejąco):")
    for k in kluby_po_mistrzostwach:
        print(k)

    # najstarszy klub i ten z największą liczbą mistrzostw
    najstarszy = kluby_po_roku[0]
    najlepszy = kluby_po_mistrzostwach[0]

    print(f"\nNajstarszy klub: {najstarszy}")
    print(f"Najwięcej mistrzostw: {najlepszy}")

    # wyszukiwanie binarne
    rok_szukany = int(input("\nPodaj rok do wyszukania: "))
    znalezione = pomoc.wyszukiwanie_binarne_po_roku(kluby_po_roku, rok_szukany)

    if znalezione:
        print("Znaleziono kluby:")
        for k in znalezione:
            print(k)
    else:
        print("Nie znaleziono klubów o takim roku założenia.")

    # zapis do pliku wynikowego
    with open("wyniki.txt", "w", encoding="utf-8") as f:
        for k in kluby_po_roku:
            f.write(str(k) + "\n")

    print("\nDane zapisano do pliku wyniki.txt")


if __name__ == "__main__":
    main()

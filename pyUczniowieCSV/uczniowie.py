import csv

class Uczen:
    def __init__(self, imie, nazwisko, klasa, oceny):
        self.imie = imie
        self.nazwisko = nazwisko
        self.klasa = klasa
        self.oceny = oceny
        self.srednia = sum(self.oceny) / len(self.oceny) if self.oceny else 0

    def __str__(self):
        return f"{self.imie} {self.nazwisko} ({self.klasa}) - oceny: {self.oceny} - średnia: {self.srednia:.2f}"

class BazaUczniow:
    def __init__(self):
        self.uczniowie = []

    def wczytaj_uczniow(self, sciezka):
        try:
            with open(sciezka, mode='r', encoding='utf-8') as plikCSV:
                csvreader = csv.reader(plikCSV, delimiter=';')
                print("Dane wczytano poprawnie.")
                for row in csvreader:
                    if len(row) >= 4:
                        imie, nazwisko, klasa = row[0], row[1], row[2]
                        oceny_str = row[3]
                        oceny = list(map(int, oceny_str.split(',')))
                        self.uczniowie.append(Uczen(imie, nazwisko, klasa, oceny))
        except FileNotFoundError:
            print(f"Błąd: Plik nie został znaleziony.")
            exit()

    def wyswietl_uczniow(self):
        print("\nLista uczniów:")
        for uczen in self.uczniowie:
            print(uczen)

    def sortuj_wg_nazwiska(self):
        n = len(self.uczniowie)
        for i in range(n):
            for j in range(0, n - i - 1):
                if self.uczniowie[j].nazwisko > self.uczniowie[j + 1].nazwisko:
                    self.uczniowie[j], self.uczniowie[j + 1] = self.uczniowie[j + 1], self.uczniowie[j]

    def sortuj_wg_sredniej(self):
        # (malejąco)
        n = len(self.uczniowie)
        for i in range(n):
            for j in range(0, n - i - 1):
                if self.uczniowie[j].srednia < self.uczniowie[j + 1].srednia:
                    self.uczniowie[j], self.uczniowie[j + 1] = self.uczniowie[j + 1], self.uczniowie[j]

    def wyszukaj_ucznia_binarne(self, nazwisko):
        self.sortuj_wg_nazwiska()
        low, high = 0, len(self.uczniowie) - 1

        while low <= high:
            srodek = (low + high) // 2
            mid_nazwisko = self.uczniowie[srodek].nazwisko
            if mid_nazwisko == nazwisko:
                return self.uczniowie[srodek]
            elif mid_nazwisko < nazwisko:
                low = srodek + 1
            else:
                high = srodek - 1
        return None

    def zapisz_statystyki_do_pliku(self, sciezka):
        try:
            with open(sciezka, mode='w', encoding='utf-8', newline='') as plikCSV:
                writer = csv.writer(plikCSV, delimiter=';')
                writer.writerow(['Imię', 'Nazwisko', 'Klasa', 'Średnia ocen'])
                for uczen in self.uczniowie:
                    writer.writerow([uczen.imie, uczen.nazwisko, uczen.klasa, f"{uczen.srednia:.2f}"])
            print(f"Statystyki zapisano do pliku: {sciezka}")
        except Exception as e:
            print(f"Błąd podczas zapisu do pliku: {e}")

def menu():
    print("\n--- MENU ---")
    print("1. Wyświetl wszystkich uczniów")
    print("2. Posortuj wg. nazwiska")
    print("3. Posortuj wg. średniej ocen")
    print("4. Wyszukaj ucznia (binarne wyszukiwanie)")
    print("5. Zapisz statystyki do pliku")
    print("0. Zakończ")

def main():
    baza = BazaUczniow()
    baza.wczytaj_uczniow('uczniowie.csv')

    while True:
        menu()
        wybor = input("Wybierz opcję: ")

        if wybor == "1":
            baza.wyswietl_uczniow()
        elif wybor == "2":
            baza.sortuj_wg_nazwiska()
            baza.wyswietl_uczniow()
        elif wybor == "3":
            baza.sortuj_wg_sredniej()
            baza.wyswietl_uczniow()
        elif wybor == "4":
            nazwisko = input("Podaj nazwisko do wyszukania: ")
            wynik = baza.wyszukaj_ucznia_binarne(nazwisko)
            if wynik:
                print("Znaleziono ucznia:")
                print(wynik)
            else:
                print("Nie znaleziono ucznia o podanym nazwisku.")
        elif wybor == "5":
            baza.zapisz_statystyki_do_pliku('statystyki.csv')
        elif wybor == "0":
            print("Koniec programu")
            break
        else:
            print("Nieznana opcja.")

if __name__ == "__main__":
    main()

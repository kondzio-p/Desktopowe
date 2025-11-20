class CzytaczPliku:
    def __init__(self, nazwa_pliku):
        self.nazwa_pliku = nazwa_pliku

    def wczytaj_dane(self):
        dane = []
        try:
            with open(self.nazwa_pliku, "r", encoding="utf-8") as plik:
                for numer_linii, linia in enumerate(plik, 1):
                    linia = linia.strip()
                    if not linia:
                        continue
                    try:
                        liczba = int(linia)
                        dane.append(liczba)
                    except ValueError:
                        print(f"Nieprawidłowa wartość w linii {numer_linii}: '{linia}'")
        except FileNotFoundError:
            print(f"Błąd: plik '{self.nazwa_pliku}' nie istnieje.")
        return dane


class Histogram:
    def __init__(self, dane, liczba_przedzialow):
        self.dane = dane
        self.liczba_przedzialow = liczba_przedzialow

    def oblicz(self):
        if not self.dane:
            print("Brak danych do analizy.")
            return

        min_wart = min(self.dane)
        max_wart = max(self.dane)

        # === Znajdź przedział startowy: najmniejsza wielokrotność 9 <= min_wart
        start_bin = (min_wart // 9) * 9
        if min_wart < 0 and min_wart % 9 != 0:
            start_bin -= 9  # np. -1 -> -9, -10 -> -18

        # === Znajdź przedział końcowy: największa wielokrotność 9 >= max_wart
        end_bin = (max_wart // 9) * 9
        if max_wart % 9 != 0:
            end_bin += 9

        # Całkowity zakres w jednostkach 9
        aktualny_zakres = (end_bin - start_bin) // 9

        # === DOPASUJ DO ŻĄDANEJ LICZBY PRZEDZIAŁÓW ===
        if self.liczba_przedzialow == 1:
            # Jeden przedział: ten, który zawiera najwięcej danych? Nie — jeden wielki po 9
            # Ale według spec: "jeden wielki od min do max (w granicach 9)" → bierzemy przedział obejmujący wszystko
            # Najprościej: przedział, który zawiera min i max → rozszerz do pełnych 9
            self.poczatki = [start_bin]
            self.ile = [0]
            for x in self.dane:
                if start_bin <= x <= start_bin + 8:
                    self.ile[0] += 1
        else:
            # Potrzebujemy dokładnie `liczba_przedzialow` przedziałów po 9
            if aktualny_zakres < self.liczba_przedzialow:
                # Rozszerz symetrycznie
                brak = self.liczba_przedzialow - aktualny_zakres
                na_lewo = brak // 2
                na_prawo = brak - na_lewo
                start_bin -= na_lewo * 9
                end_bin += na_prawo * 9
            elif aktualny_zakres > self.liczba_przedzialow:
                # Za dużo — ale nie możemy zmniejszyć, bo dane się nie mieszczą
                # Więc zostawiamy minimalny zakres i ignorujemy nadmiar? Nie — musimy objąć dane!
                # Zawsze przynajmniej tyle, ile potrzeba
                pass  # już OK

            # Teraz budujemy dokładnie `liczba_przedzialow` przedziałów
            self.poczatki = []
            self.ile = [0] * self.liczba_przedzialow
            current = start_bin
            for i in range(self.liczba_przedzialow):
                self.poczatki.append(current)
                current += 9

            # Zliczanie
            for x in self.dane:
                # Znajdź przedział: (x // 9)*9
                bin_start = (x // 9) * 9
                if x < 0 and x % 9 != 0:
                    bin_start -= 9
                # Sprawdź, czy mieści się w naszych przedziałach
                for j, p in enumerate(self.poczatki):
                    if p <= x <= p + 8:
                        self.ile[j] += 1
                        break

        self.wyswietl()

    def wyswietl(self):
        print("\nHistogram:\n")
        for i, start in enumerate(self.poczatki):
            koniec = start + 8
            ile = self.ile[i]

            # Formatowanie: 00-08, -09--01, 09-17, itd.
            def fmt(n):
                if n >= 0:
                    return f"{n:02d}"
                else:
                    return f"{n}"  # np. -9 → -9, -10 → -10

            start_str = fmt(start)
            end_str = fmt(koniec)

            kreski = "#" * ile
            kreski = kreski.ljust(30)  # do 30 znaków

            print(f"{start_str}-{end_str} | {kreski}| {ile}")


class Aplikacja:
    def uruchom(self):
        czytnik = CzytaczPliku("histogram_data.txt")
        dane = czytnik.wczytaj_dane()

        if not dane:
            print("Brak poprawnych danych – zakończenie programu.")
            return

        while True:
            try:
                wejscie = input("Podaj liczbę przedziałów histogramu: ")
                liczba = int(wejscie)
                if liczba > 0:
                    break
                else:
                    print("Liczba musi być większa od 0!")
            except:
                print("Podaj poprawną liczbę całkowitą!")

        hist = Histogram(dane, liczba)
        hist.oblicz()


# === URUCHOM ===
app = Aplikacja()
app.uruchom()
import random

class apka:
    def __init__(self, plik: str):
        self.plik = plik
        self.osoby = self.wczytaj_osoby()
        self.druzyny = {
            "Piłka nożna": [],
            "Siatkówka": [],
        }

    def wczytaj_osoby(self) -> list[str]:
        """Wczytywanie z txt"""
        osoby = []
        try:
            with open(self.plik, "r", encoding="utf-8") as f:
                for linia in f:
                    linia = linia.strip()
                    if linia:
                        osoby.append(linia)
        except FileNotFoundError:
            print(f"Plik '{self.plik}' nie istnieje. Lista osób pusta.")
        return osoby
    
    def losuj_druzyne(self, nazwa: str, liczba_osob: int=5):
        if liczba_osob>len(self.osoby):
            raise ValueError("Za mało osób w pliku")
        self.druzyny[nazwa] = random.sample(self.osoby, liczba_osob)

    def uruchom(self):
        try:
            liczba_pilka = int(input("Podaj liczebność drużyny piłki nożnej: "))
            liczba_siatkowka = int(input("Podaj liczebność drużyny siatkówki: "))
            self.losuj_druzyne("Piłka nożna", liczba_pilka)
            self.losuj_druzyne("Siatkówka", liczba_siatkowka)
        except ValueError:
            print("Podano nieprawidłową liczbę.")

    def pokaz_druzyny(self):
        for nazwa, czlonkowie in self.druzyny.items():
            print(f"{nazwa} ({len(czlonkowie)} osob):")
            for osoba in czlonkowie:
                print(" -", osoba)
            print()

if __name__ == "__main__":
    apka = apka("uczniowie.txt")
    apka.uruchom()
    apka.pokaz_druzyny()

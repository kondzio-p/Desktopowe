import random

class DiceGame:
    def __init__(self):
        self.dice_rolls = []

    # Funkcja do losowania rzutów kostkami
    def roll_dice(self, num_dice):
        self.dice_rolls = [random.randint(1, 6) for _ in range(num_dice)]
    
    # Funkcja do liczenia punktów
    def calculate_points(self):
        count = {}
        # Liczymy ile razy każda liczba została wyrzucona
        for roll in self.dice_rolls:
            if roll in count:
                count[roll] += 1
            else:
                count[roll] = 1
        
        points = 0
        # Dodajemy punkty tylko dla liczb, które zostały wyrzucone dwa razy lub więcej
        for number, frequency in count.items():
            if frequency >= 2:
                points += number * frequency
        
        return points

    # Funkcja do wypisania wyników rzutów
    def display_rolls(self):
        for index, roll in enumerate(self.dice_rolls, 1):
            print(f"Kostka {index}: {roll}")
    
    # Funkcja do wykonania gry
    def play(self):
        while True:
            # Pobranie liczby kostek do rzucenia
            while True:
                try:
                    num_dice = int(input("Podaj liczbę kostek (od 3 do 10): "))
                    if 3 <= num_dice <= 10:
                        break
                    else:
                        print("Liczba kostek musi być z przedziału 3-10.")
                except ValueError:
                    print("Wpisz poprawną liczbę.")

            # Losowanie rzutów kostkami
            self.roll_dice(num_dice)

            # Wyświetlanie wyników
            self.display_rolls()

            # Obliczanie punktów
            points = self.calculate_points()
            print(f"Zdobyte punkty: {points}")

            # Zapytanie o kontynuację gry
            play_again = input("Czy chcesz zagrać ponownie? (t/n): ").lower()
            if play_again != 't':
                print("Dziękujemy za grę!")
                break

# Główna funkcja uruchamiająca grę
if __name__ == "__main__":
    game = DiceGame()
    game.play()

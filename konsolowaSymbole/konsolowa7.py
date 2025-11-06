def zadanie1(lista):
    palindromy = []
    for wyraz in lista:
        for i in range(len(wyraz) // 2):
            if wyraz[i] == wyraz[len(wyraz) - 1 - i]:
                palindrom = True
                continue

            else:
                palindrom = False
                break
        if palindrom:
            palindromy.append(wyraz)

    print(palindromy)

def wczytaj():
    with open("symbole.txt", "rt", encoding="utf-8") as f:
        lista = f.read().split()
    return lista

def main():
    lista = wczytaj()
    zadanie1(lista)

if __name__ == '__main__':
    main()
def create():
    note = input("Введите текст заметки: ")
    with open("notes.txt", "a") as file:
        file.write(note + "\n")
    print("Заметка сохранена")

def delete():
    notes = []
    try:
        with open("notes.txt", "r") as file:
            notes = file.readlines()
    except:
        print("Заметок нет")
        return
    
    if not notes:
        print("Заметок нет")
        return
    
    i = 1
    for note in notes:
        print(f"{i}. {note.strip()}")
        i += 1
    
    try:
        num = int(input("Введите номер заметки для удаления: "))
        if 1 <= num <= len(notes):
            notes.pop(num - 1)
            with open("notes.txt", "w") as file:
                file.writelines(notes)
            print("Заметка удалена")
        else:
            print("Неверный номер")
    except ValueError:
        print("Введите число")

def search():
    word = input("Какое слово ищем? ")
    
    try:
        with open("notes.txt", "r") as file:
            all_notes = file.read().splitlines()
    except:
        print("Пока нет заметок")
        return
    
    results = []
    for note in all_notes:
        if word.lower() in note.lower():
            results.append(note)
    
    if results:
        print(f"Нашёл {len(results)} заметок:")
        for note in results:
            print(f"• {note}")
    else:
        print("Ничего не нашёл")

def close():
    print("Выход из программы")
    exit()

def show():
    try:
        with open("notes.txt", "r") as file:
            notes = file.readlines()
        
        if not notes:
            print("Заметок нет")
            return
            
        print("\nМои заметки:")
        for note in notes:
            print(f"• {note.strip()}")
    except:
        print("Заметок нет")

def interface():
    while True:
        print('''
Добро пожаловать в Manager Of Projects
Что Вы хотите сделать:
1 - Создать заметку
2 - Удалить заметку
3 - Найти заметку
4 - Закрыть программу
5 - Показать все заметки
Для выбора команды напишите номер команды.''')
        answer = input()
        match answer:
            case "1":
               create() 
            case "2":
                delete()
            case "3":
                search()
            case "4":
                close()
            case "5":
                show()
            case _:
                print("Такой команды нет, введите число от 1 до 5")
                continue

interface()
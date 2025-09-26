def create():
    pass
def delete():
    pass
def search():
    pass
def close():
    pass
def show():
    pass
def interface():
    print("Добро пожаловать в manager of projects. Тут ты можешь работать со своими заметками")
    while True:
        print('''Что ты хочешь сделать?
        1 - добавить заметку
        2 - удалить заметку
        3 - найти заметку
        4 - закрыть заметку
        5 - показать заметку
        Для выбора команды напишите номер команды''')
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
                print("Команды только 1 - 5. Попробуйте еще раз")
                continue
                
        
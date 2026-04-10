using System;

public class Location
{
    public string Name { get; set; }          // название локации
    public string Description { get; set; }   // описание

    public IInteractable[] Objects;           // объекты в локации
    public Location[] Exits;                  // переходы

    public GameEventBase[] Events;            // события 

    public Location(string name, string description)
    {
        Name = name;
        Description = description;

        Objects = new IInteractable[0];
        Exits = new Location[0];
        Events = new GameEventBase[0];
    }

    // ================= OBJECTS =================

    public IInteractable FindObject(string id)
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].Id == id)
                return Objects[i];
        }
        return null;
    }

    // ================= EVENTS =================

    public void TriggerEvents(GameState state)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            Events[i].Execute(state);
        }
    }

    // ================= INFO =================

    public void Describe()
    {
        Console.WriteLine("== " + Name + " ==");
        Console.WriteLine(Description);

        Console.WriteLine("\nОбъекты:");
        for (int i = 0; i < Objects.Length; i++)
        {
            Console.WriteLine("- " + Objects[i].Id);
        }

        Console.WriteLine("\nВыходы:");
        for (int i = 0; i < Exits.Length; i++)
        {
            Console.WriteLine("- " + Exits[i].Name);
        }
    }
}
using System;

public class Location
{
    public string Name { get; set; }
    public string Description { get; set; }

    public IInteractable[] Objects = new IInteractable[0];
    public Location[] Exits = new Location[0];
    public GameEventBase[] Events = new GameEventBase[0];

    public Location(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public IInteractable? FindObject(string id)
    {
        foreach (var obj in Objects)
            if (obj.Id == id) return obj;
        return null;
    }

    public void TriggerEvents(GameState state)
    {
        foreach (var e in Events)
            e.Execute(state);
    }

    public void Describe()
    {
        Console.WriteLine("\n== " + Name + " ==");
        Console.WriteLine(Description);

        Console.WriteLine("\nОбъекты:");
        foreach (var obj in Objects)
            Console.WriteLine("- " + obj.Id);

        Console.WriteLine("\nВыходы:");
        foreach (var exit in Exits)
            Console.WriteLine("- " + exit.Name);
    }
}
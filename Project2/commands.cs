using System;
using System.Linq;

public class LookCommand : CommandBase
{
    public LookCommand() : base("look") { }
    public override void Execute(GameState state, string[] args)
        => state.CurrentLocation.Describe();
}

public class GoCommand : CommandBase
{
    public GoCommand() : base("go") { }

    public override void Execute(GameState state, string[] args)
    {
        var loc = state.CurrentLocation.Exits
            .FirstOrDefault(l => l.Name.ToLower() == args[0].ToLower());

        if (loc == null)
        {
            Console.WriteLine("Нет пути");
            return;
        }

        state.CurrentLocation = loc;
        state.CurrentLocation.TriggerEvents(state);
        state.CurrentLocation.Describe();
    }
    
}

public class InteractCommand : CommandBase
{
    public InteractCommand() : base("interact") { }

    public override void Execute(GameState state, string[] args)
    {
        var obj = state.CurrentLocation.FindObject(args[0]);

        if (obj == null)
        {
            Console.WriteLine("Нет такого объекта");
            return;
        }

        obj.Interact(state);
    }
}

public class InventoryCommand : CommandBase
{
    public InventoryCommand() : base("inv") { }
    public override void Execute(GameState state, string[] args)
        => state.ShowInventory();
}

public class StatusCommand : CommandBase
{
    public StatusCommand() : base("status") { }
    public override void Execute(GameState state, string[] args)
        => state.ShowStatus();
}

public class HelpCommand : CommandBase
{
    public HelpCommand() : base("help") { }

    public override void Execute(GameState state, string[] args)
    {
        Console.WriteLine("look, go, interact, inv, status");
    }
}

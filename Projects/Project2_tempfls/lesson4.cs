using System;
using System.Linq;

public class AddExitEffect : EffectBase
{
    private Location from;
    private Location to;

    public AddExitEffect(Location from, Location to)
    {
        this.from = from;
        this.to = to;
    }

    public override void Apply(GameState state)
    {
        var exits = from.Exits.ToList();

        if (!exits.Contains(to))
        {
            exits.Add(to);
            from.Exits = exits.ToArray();
            state.AddLog("Открылся новый путь: " + to.Name);
        }
    }
}

public class ChangeLocationEffect : EffectBase
{
    private Location location;

    public ChangeLocationEffect(Location location)
    {
        this.location = location;
    }

    public override void Apply(GameState state)
    {
        state.CurrentLocation = location;
        state.AddLog("Вы переместились в " + location.Name);
    }
}
public class OnEnterLocationEvent : GameEventBase
{
    public OnEnterLocationEvent(ICondition condition, IEffect[] effects)
        : base(condition, effects)
    {
    }
}

public class OnTurnEvent : GameEventBase
{
    public OnTurnEvent(ICondition condition, IEffect[] effects)
        : base(condition, effects)
    {
    }
}

public class OneTimeEvent : GameEventBase
{
    private bool triggered = false;

    public OneTimeEvent(ICondition condition, IEffect[] effects)
        : base(condition, effects)
    {
    }

    public new void Execute(GameState state)
    {
        if (triggered)
            return;

        if (condition.Check(state))
        {
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].Apply(state);
            }

            triggered = true;
        }
    }
}public class WorldBuilder
{
    public static Location CreateWorld(GameState state)
    {
        // ===== ЛОКАЦИИ =====
        var hall = new Location("Hall", "Стартовая зона");
        var storage = new Location("Storage", "Склад с припасами");
        var darkCorridor = new Location("DarkCorridor", "Очень темный коридор");
        var generatorRoom = new Location("GeneratorRoom", "Комната генератора");
        var exit = new Location("Exit", "Выход наружу");

        // ===== СВЯЗИ =====
        hall.Exits = new Location[] { darkCorridor };
        darkCorridor.Exits = new Location[] { hall };
        storage.Exits = new Location[] { generatorRoom };
        generatorRoom.Exits = new Location[] { storage };

        // ===== ОБЪЕКТЫ =====

        // сундук с ключом
        var chest = new Chest("chest", new IEffect[]
        {
            new AddItemEffect("Key"),
            new LogEffect("Вы нашли ключ!")
        });

        // дверь (открывает путь в storage)
        var door = new Door("door",
            new HasItemCondition("Key"),
            new IEffect[]
            {
                new AddExitEffect(hall, storage),
                new LogEffect("Дверь открыта!")
            });

        // ловушка
        var trap = new Trap("trap", new IEffect[]
        {
            new DamageEffect(20),
            new LogEffect("Вы попали в ловушку!")
        });

        // генератор (NPC/терминал)
        var generator = new NPC("generator",
            new AndCondition(
                new HasItemCondition("Fuse"),
                new HasItemCondition("Wrench")
            ),
            new IEffect[]
            {
                new SetFlagEffect("GeneratorOn", true),
                new LogEffect("Генератор включен!")
            });

        // ===== РАСПРЕДЕЛЕНИЕ =====
        hall.Objects = new IInteractable[] { chest, door };
        darkCorridor.Objects = new IInteractable[] { trap };
        generatorRoom.Objects = new IInteractable[] { generator };

        // ===== СОБЫТИЯ =====

        // урон в темноте без факела
        darkCorridor.Events = new GameEventBase[]
        {
            new OnEnterLocationEvent(
                new NotCondition(new HasItemCondition("Torch")),
                new IEffect[]
                {
                    new DamageEffect(10),
                    new LogEffect("Слишком темно! Вы получили урон.")
                })
        };

        // открытие выхода после генератора
        generatorRoom.Events = new GameEventBase[]
        {
            new OneTimeEvent(
                new FlagCondition("GeneratorOn"),
                new IEffect[]
                {
                    new AddExitEffect(generatorRoom, exit),
                    new LogEffect("Открылся путь к выходу!")
                })
        };

        // ===== СТАРТ =====
        state.CurrentLocation = hall;

        return hall;
    }
}   
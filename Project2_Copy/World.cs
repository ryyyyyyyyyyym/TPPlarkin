public class WorldBuilder
{
    public static void CreateWorld(GameState state)
    {
        // ЛОКАЦИИ
        var hall = new Location("Hall", "Стартовая комната");
        var storage = new Location("Storage", "Склад");
        var dark = new Location("DarkCorridor", "Темный коридор");
        var generatorRoom = new Location("GeneratorRoom", "Комната генератора");
        var exit = new Location("Exit", "Выход");

        // СВЯЗИ
        hall.Exits = new[] { storage, dark };
        storage.Exits = new[] { hall, generatorRoom };
        dark.Exits = new[] { hall, exit };
        generatorRoom.Exits = new[] { storage };
        exit.Exits = new Location[0];

        // ПРЕДМЕТЫ ЧЕРЕЗ СУНДУКИ
        var chestKey = new Chest("chest"); // дает Key

        var fuseChest = new Chest("fusebox");
        var wrenchChest = new Chest("toolbox");

        // ХАК: разные предметы
        // (просто добавим через эффекты при входе)
        storage.Events = new GameEventBase[]
        {
            new SimpleEvent(
                new NotCondition(new FlagCondition("FuseTaken")),
                new IEffect[]
                {
                    new AddItemEffect("Fuse"),
                    new SetFlagEffect("FuseTaken", true)
                }
            ),
            new SimpleEvent(
                new NotCondition(new FlagCondition("WrenchTaken")),
                new IEffect[]
                {
                    new AddItemEffect("Wrench"),
                    new SetFlagEffect("WrenchTaken", true)
                }
            )
        };

        // ДВЕРЬ ВЫХОДА (как было)
        var door = new Door("door",
            new HasItemCondition("Key"),
            new AddExitEffect(hall, exit));

        // ГЕНЕРАТОР
        var generator = new Generator(
            "generator",
            new HasItemCondition("Fuse"),
            new SetFlagEffect("GeneratorOn", true)
        );

        // ОБЪЕКТЫ
        hall.Objects = new IInteractable[] { chestKey, door };
        storage.Objects = new IInteractable[] { fuseChest, wrenchChest };
        generatorRoom.Objects = new IInteractable[] { generator };
        dark.Objects = new IInteractable[] { new Trap("trap") };

        // СОБЫТИЯ (ТЕМНОТА)
        dark.Events = new GameEventBase[]
        {
            new SimpleEvent(
                new NotCondition(new HasItemCondition("Torch")),
                new IEffect[]
                {
                    new DamageEffect(10)
                }
            )
        };

        // СТАРТ
        state.CurrentLocation = hall;
    }
}
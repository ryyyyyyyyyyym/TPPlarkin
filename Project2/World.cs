public class WorldBuilder
{
    public static void CreateWorld(GameState state)
    {
        var hall = new Location("Hall", "Стартовая комната");
        var storage = new Location("Storage", "Склад");
        var dark = new Location("DarkCorridor", "Темный коридор");
        var generatorRoom = new Location("GeneratorRoom", "Комната генератора");
        var exit = new Location("Exit", "Выход");

        // связи
        hall.Exits = new[] { storage, dark };
        storage.Exits = new[] { hall, generatorRoom };
        dark.Exits = new[] { hall, exit };
        generatorRoom.Exits = new[] { storage };

        // сундуки
        var chestKey = new Chest("chest", "Key");
        var fuseChest = new Chest("fusebox", "Fuse");
        var wrenchChest = new Chest("toolbox", "Wrench");
        var torchChest = new Chest("torchbox", "Torch");

        // дверь
        var door = new Door("door",
            new HasItemCondition("Key"),
            new AddExitEffect(hall, exit));

        // генератор
        var generator = new Generator(
            "generator",
            new HasItemCondition("Fuse"),
            new SetFlagEffect("GeneratorOn", true)
        );

        // объекты
        hall.Objects = new IInteractable[] { chestKey, torchChest, door };
        storage.Objects = new IInteractable[] { fuseChest, wrenchChest };
        generatorRoom.Objects = new IInteractable[] { generator };
        dark.Objects = new IInteractable[] { new Trap("trap") };

        // темнота
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

        state.CurrentLocation = hall;
    }
}
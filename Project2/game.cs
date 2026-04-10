using System;
using System.Collections.Generic;
using System.Linq;

public class Game
{
    private GameState state;
    private Dictionary<string, ICommand> commands;

    public Game()
    {
        state = new GameState();
        WorldBuilder.CreateWorld(state);

        commands = new List<ICommand>
        {
            new LookCommand(),
            new GoCommand(),
            new InteractCommand(),
            new InventoryCommand(),
            new StatusCommand(),
            new HelpCommand()
        }.ToDictionary(c => c.Name, c => c);
        
    }

    public void Run()
    {
        Console.WriteLine("Игра началась!");
        state.CurrentLocation.Describe();

        while (true)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            var parts = input.Split(' ');
            var commandName = parts[0].ToLower();
            var args = parts.Skip(1).ToArray();

            if (commands.ContainsKey(commandName))
            {
                commands[commandName].Execute(state, args);
            }
            else
            {
                Console.WriteLine("Неизвестная команда.");
            }

            state.CurrentLocation.TriggerEvents(state);

            if (state.CurrentLocation.Name == "Exit")
            {
                Console.WriteLine("Вы победили!");
                break;
            }

            if (state.Health <= 0)
            {
                Console.WriteLine("Игра окончена.");
                break;
            }
        }
    }
}
using System;
using System.Collections.Generic;

public class GameState
{
    public int Health { get; private set; } = 100;
    public Location CurrentLocation { get; set; } = null!;

    private List<string> inventory = new List<string>();
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public void AddItem(string item)
    {
        inventory.Add(item);
        Console.WriteLine("Получен предмет: " + item);
    }

    public void RemoveItem(string item)
    {
        inventory.Remove(item);
    }

    public bool HasItem(string item)
    {
        return inventory.Contains(item);
    }

    public void ShowInventory()
    {
        Console.WriteLine("Инвентарь:");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var item in inventory)
            Console.WriteLine("- " + item);
    }

    public void SetFlag(string flag, bool value)
    {
        flags[flag] = value;
    }

    public bool GetFlag(string flag)
    {
        return flags.ContainsKey(flag) && flags[flag];
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine("Урон: " + damage);
    }

    public void Heal(int value)
    {
        Health += value;
    }

    public void ShowStatus()
    {
        Console.WriteLine("Здоровье: " + Health);
    }
}
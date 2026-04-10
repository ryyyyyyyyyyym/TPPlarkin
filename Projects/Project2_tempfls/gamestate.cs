using System;
using System.Collections.Generic;

public class GameState
{
    // ===== ОСНОВНЫЕ ДАННЫЕ =====

    public int Health { get; private set; } = 100;

    public Location CurrentLocation { get; set; }

    private List<string> inventory = new List<string>();
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();
    private List<string> log = new List<string>();

    // ===== ИНВЕНТАРЬ =====

    public void AddItem(string item)
    {
        inventory.Add(item);
        AddLog("Получен предмет: " + item);
    }

    public void RemoveItem(string item)
    {
        inventory.Remove(item);
        AddLog("Удалён предмет: " + item);
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
        {
            Console.WriteLine("- " + item);
        }
    }

    // ===== ФЛАГИ =====

    public void SetFlag(string flag, bool value)
    {
        flags[flag] = value;
    }

    public bool GetFlag(string flag)
    {
        return flags.ContainsKey(flag) && flags[flag];
    }

    // ===== ЗДОРОВЬЕ =====

    public void TakeDamage(int damage)
    {
        Health -= damage;
        AddLog("Получен урон: " + damage);

        if (Health <= 0)
        {
            Console.WriteLine("Вы погибли!");
        }
    }

    public void Heal(int value)
    {
        Health += value;
        AddLog("Восстановлено здоровье: " + value);
    }

    // ===== ЛОГ =====

    public void AddLog(string message)
    {
        log.Add(message);
        Console.WriteLine(message);
    }

    public void ShowStatus()
    {
        Console.WriteLine("Здоровье: " + Health);
    }
}
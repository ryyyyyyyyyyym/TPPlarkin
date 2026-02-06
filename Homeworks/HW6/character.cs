using System;

interface IDamageable
{
    void TakeDamage(int damage);
}

abstract class Character : IDamageable
{
    protected string Name;
    protected int Health;

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public abstract void Attack();

    public void Move()
    {
        Console.WriteLine($"{Name} moves");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} takes {damage} damage");
    }
}

class Warrior : Character
{
    public Warrior(string name, int health) : base(name, health) { }

    public override void Attack()
    {
        Console.WriteLine($"{Name} attacks with a sword");
    }
}

class Mage : Character
{
    public Mage(string name, int health) : base(name, health) { }

    public override void Attack()
    {
        Console.WriteLine($"{Name} casts a spell");
    }
}

interface IHealable
{
    void Heal(int amount);
}

class MageHealer : Mage, IHealable
{
    public MageHealer(string name, int health) : base(name, health) { }

    public void Heal(int amount)
    {
        Health += amount;
        Console.WriteLine($"{Name} heals for {amount}");
    }
}

class Program
{
    static void Main()
    {
        Character[] characters =
        {
            new Warrior("Warrior", 100),
            new Mage("Mage", 70)
        };

        foreach (Character c in characters)
        {
            c.Attack();
        }
    }
}

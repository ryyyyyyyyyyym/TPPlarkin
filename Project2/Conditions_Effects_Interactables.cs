// CONDITIONS

public class HasItemCondition : ConditionBase
{
    private string item;
    public HasItemCondition(string item) => this.item = item;
    public override bool Check(GameState state) => state.HasItem(item);
}

public class FlagCondition : ConditionBase
{
    private string flag;
    public FlagCondition(string flag) => this.flag = flag;
    public override bool Check(GameState state) => state.GetFlag(flag);
}

public class NotCondition : ConditionBase
{
    private ICondition cond;
    public NotCondition(ICondition c) => cond = c;
    public override bool Check(GameState state) => !cond.Check(state);
}

// EFFECTS

public class AddItemEffect : EffectBase
{
    private string item;
    public AddItemEffect(string item) => this.item = item;
    public override void Apply(GameState state) => state.AddItem(item);
}

public class DamageEffect : EffectBase
{
    private int dmg;
    public DamageEffect(int dmg) => this.dmg = dmg;
    public override void Apply(GameState state) => state.TakeDamage(dmg);
}

public class SetFlagEffect : EffectBase
{
    private string flag;
    private bool value;
    public SetFlagEffect(string f, bool v) { flag = f; value = v; }
    public override void Apply(GameState state) => state.SetFlag(flag, value);
}

public class AddExitEffect : EffectBase
{
    private Location from;
    private Location to;

    public AddExitEffect(Location f, Location t)
    {
        from = f;
        to = t;
    }

    public override void Apply(GameState state)
    {
        var list = from.Exits.ToList();
        if (!list.Contains(to))
        {
            list.Add(to);
            from.Exits = list.ToArray();
        }
    }
}

// OBJECTS

public class Chest : IInteractable
{
    public string Id { get; private set; }

    private string item;
    private bool opened = false;

    public Chest(string id, string item)
    {
        Id = id;
        this.item = item;
    }

    public void Interact(GameState state)
    {
        if (opened)
        {
            Console.WriteLine("Пусто");
            return;
        }

        state.AddItem(item);
        opened = true;
    }
}

public class Door : IInteractable
{
    public string Id { get; private set; }
    private ICondition condition;
    private IEffect effect;

    public Door(string id, ICondition c, IEffect e)
    {
        Id = id;
        condition = c;
        effect = e;
    }

    public void Interact(GameState state)
    {
        if (condition.Check(state))
            effect.Apply(state);
        else
            Console.WriteLine("Дверь закрыта");
    }
}

public class Trap : IInteractable
{
    public string Id { get; private set; }
    private bool triggered = false;

    public Trap(string id) => Id = id;

    public void Interact(GameState state)
    {
        if (triggered) return;
        state.TakeDamage(20);
        triggered = true;
    }
}
public class Generator : IInteractable
{
    public string Id { get; private set; }

    private ICondition condition;
    private IEffect effect;

    public Generator(string id, ICondition condition, IEffect effect)
    {
        Id = id;
        this.condition = condition;
        this.effect = effect;
    }

    public void Interact(GameState state)
    {
        if (condition.Check(state))
        {
            Console.WriteLine("Генератор включен!");
            effect.Apply(state);
        }
        else
        {
            Console.WriteLine("Нужны Fuse и Wrench");
        }
    }
}

public class SimpleEvent : GameEventBase
{
    public SimpleEvent(ICondition condition, IEffect[] effects)
        : base(condition, effects) { }
}
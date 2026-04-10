public interface ICommand
{
    string Name { get; }
    void Execute(GameState state, string[] args);
}

public interface IInteractable
{
    string Id { get; }
    void Interact(GameState state);
}

public interface ICondition
{
    bool Check(GameState state);
}

public interface IEffect
{
    void Apply(GameState state);
}

public abstract class CommandBase : ICommand
{
    public string Name { get; protected set; }

    public CommandBase(string name)
    {
        Name = name;
    }

    public abstract void Execute(GameState state, string[] args);
}

public abstract class ConditionBase : ICondition
{
    public abstract bool Check(GameState state);
}

public abstract class EffectBase : IEffect
{
    public abstract void Apply(GameState state);
}

public abstract class GameEventBase
{
    protected ICondition condition;
    protected IEffect[] effects;

    public GameEventBase(ICondition condition, IEffect[] effects)
    {
        this.condition = condition;
        this.effects = effects;
    }

    public virtual void Execute(GameState state)
    {
        if (condition.Check(state))
        {
            foreach (var e in effects)
                e.Apply(state);
        }
    }
}
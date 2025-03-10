using voidsccut.scripts.shared;

namespace voidsccut.scripts.abilities;

public abstract class Ability : IAbility
{
    public bool IsFinished { get; protected set; } = false;
    public string Name { get; }
    public string Description { get; }
    public string Id { get; }
    public int ActionCost { get; }
    public int MovementCost { get; }
    public int OrderCost { get; private set; }
    
    
    protected IEntity Entity{get; private set;}
    protected ITile Tile{get; private set;}
    protected float DeltaTime{get; private set;}
    
    
    protected Ability(string name, string description, string id, int actionCost, int movementCost, int orderCost)
    {
        this.Name = name;
        this.Description = description;
        this.Id = id;
        this.ActionCost = actionCost;
        this.MovementCost = movementCost;
        this.OrderCost = orderCost;
    }
    

    public void Process(float deltaTime)
    {
        if (IsFinished) return;
        DeltaTime = deltaTime;
        OnProcess();
        if (IsFinished) Reset();
    }
    protected abstract void OnProcess();
    
    
    public virtual bool IsUsableBy(IEntity entity, AbilityUseCase useCase)
    {
        if (useCase == AbilityUseCase.Ordered)
        {
            return OrderCost > Game.Player.Orders;
        }
        return ActionCost > entity.ActionLeft || MovementCost > entity.MovementLeft;
    }

    
    public void Use(IEntity entity, ITile tile)
    {
        IsFinished = false;
        Entity = entity;
        Tile = tile;
        entity.Manipulator.SetAbility(this);
        OnUse();
        if (IsFinished) Reset();
    }
    protected abstract void OnUse();

    
    private void Reset()
    {
        OnBeforeReset();
        Entity.Manipulator.SetAbility(null);
        Entity = null;
        Tile = null;
    }
    protected abstract void OnBeforeReset();
}
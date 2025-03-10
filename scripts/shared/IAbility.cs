namespace voidsccut.scripts.shared;

public interface IAbility : IProcessable
{
    public string Name { get; }
    public string Description { get; }
    public string Id { get; }
    public bool IsUsableBy(IEntity entity, AbilityUseCase useCase);
    public int ActionCost { get; }
    public int MovementCost { get; }
    public int OrderCost { get; }
    public void Use(IEntity entity, ITile tile);
}
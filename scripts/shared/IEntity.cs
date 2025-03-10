namespace voidsccut.scripts.shared;

public interface IEntity : IProcessable
{
    public IEntityManipulator Manipulator { get; }
    public string Name { get; }
    public ITile Tile { get; }
    public IAbility Ability { get; }
    public int MovementLeft { get; }
    public int ActionLeft { get; }
    public Team Team { get; }
}
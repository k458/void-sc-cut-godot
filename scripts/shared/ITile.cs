namespace voidsccut.scripts.shared;

public interface ITile
{
    public int X { get; }
    public int Y{ get; }
    public IEntity Entity { get; }
}
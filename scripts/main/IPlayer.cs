namespace voidsccut.scripts.main;

public interface IPlayer
{
    public IPlayerManipulator Manipulator { get; }
    public int Orders { get; }
    public bool ControlAllowed { get; }
}
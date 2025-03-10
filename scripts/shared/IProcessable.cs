namespace voidsccut.scripts.shared;

public interface IProcessable
{
    public void Process(float deltaTime);
    public bool IsFinished { get; }
}
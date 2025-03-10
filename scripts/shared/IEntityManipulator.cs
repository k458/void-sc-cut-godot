namespace voidsccut.scripts.shared;

public interface IEntityManipulator
{
    public void SetTile(ITile tile);
    public void SetAbility(IAbility ability);
    public bool TranslateTowards(ITile tile, float distance);
    public void SetMovementLeft(int movementLeft);
    public void SetActionLeft(int action);
}
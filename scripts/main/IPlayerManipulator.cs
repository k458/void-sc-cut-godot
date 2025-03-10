namespace voidsccut.scripts.main;

public interface IPlayerManipulator
{
    public void SetOrdersMax(int i);
    public void ReplenishOrders();
    public void ReduceOrders(int i = 1);
    public void IncreaseOrders(int i = 1);
    public void SetControlAllowed(bool allowed);
}
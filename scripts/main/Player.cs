namespace voidsccut.scripts.main;

public class Player : IPlayer, IPlayerManipulator
{
    public IPlayerManipulator Manipulator => this;
    public bool ControlAllowed { get; private set; }
    public int Orders { get; private set; }
    
    private int _ordersMax = 0;
    
    
    public void SetOrdersMax(int i) => _ordersMax = i >= 0 ? i : 0;
    
    public void ReplenishOrders() => Orders = (Orders > 0 ? 1 : 0) + _ordersMax;
    
    public void ReduceOrders(int i = 1) => Orders = Orders - i >= 0 ? Orders - i : 0;
    
    public void IncreaseOrders(int i = 1) => Orders += i;
    
    public void SetControlAllowed(bool allowed) => ControlAllowed = allowed;
}
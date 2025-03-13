namespace voidsccut.scripts.messageService;

public interface IMessageManager : IMessageTransmitter
{
    void AddMessageReceiver(IMessageReceiver receiver);
}
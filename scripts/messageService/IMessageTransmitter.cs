namespace voidsccut.scripts.messageService;

public interface IMessageTransmitter
{
    void TransmitMessage(MessageType type);
    void TransmitMessage(MessageType type, string log);
}
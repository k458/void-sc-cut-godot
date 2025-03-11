using System.Collections.Generic;
using voidsccut.scripts.client.model;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.messageService;

public class MessageManager
{
    private List<IMessageReceiver> _receivers = new List<IMessageReceiver>();
    
    public void AddMessageReceiver(IMessageReceiver receiver)
    {
        _receivers.Add(receiver);
    }
    public void TransmitMessage(MessageType type)
    {
        foreach (var receiver in _receivers)
        {
            if (receiver != null) receiver.Message(type);
        }
    }
}

// public class MessageManager
// {
//     private List<IMessageReceiver> _receivers = new List<IMessageReceiver>();
//     private List<MessageType> _messageTypes = new List<MessageType>();
//     private List<IMessageReceiver> _receiversNext = new List<IMessageReceiver>();
//     private List<MessageType> _messageTypesNext = new List<MessageType>();
//     
//     public void AddMessageReceiver(IMessageReceiver receiver, MessageType type)
//     {
//         _receivers.Add(receiver);
//         _messageTypes.Add(type);
//     }
//     public void TransmitMessage(MessageType type)
//     {
//         _receiversNext.Clear();
//         _messageTypesNext.Clear();
//         for (int i = 0; i < _messageTypes.Count; i++)
//         {
//             if (_messageTypes[i] == type)
//             {
//                 if (_receivers[i] != null)_receivers[i].Message(type);
//             }
//             else
//             {
//                 _receiversNext.Add(_receivers[i]);
//                 _messageTypesNext.Add(type);
//             }
//         }
//         SwapLists();
//     }
//
//     private void SwapLists()
//     {
//         (_receivers, _receiversNext) = (_receiversNext, _receivers);
//         (_messageTypes, _messageTypesNext) = (_messageTypesNext, _messageTypes);
//     }
// }
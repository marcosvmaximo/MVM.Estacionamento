using System;
namespace MVM.Estacionamento.Core.Messages;

public abstract class Message
{
    public Guid MessageId { get; protected set; }
    public string MessageType { get; protected set; }

    public Message()
    {
        MessageType = GetType().Name;
    }
}


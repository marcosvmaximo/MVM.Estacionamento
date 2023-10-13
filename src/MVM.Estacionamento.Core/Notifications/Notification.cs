using System;
using MVM.Estacionamento.Core.Messages;

namespace MVM.Estacionamento.Core;

public class Notification : Message
{
    public string Key { get; private set; }
    public string Value { get; private set; }

    public Notification(string key, string value)
    {
        MessageId = Guid.NewGuid();
        Key = key;
        Value = value;
    }
}


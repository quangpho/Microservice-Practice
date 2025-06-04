using Application.Events;
using Application.Interfaces;
using Azure.Messaging.ServiceBus;

namespace Infrastructure.Publisher;

public class MessagePublisher : IMessagePublisher
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;
    
    public Task PublishMemberCreatedAsync(MemberCreatedEvent memberEvent)
    {
        throw new NotImplementedException();
    }
}
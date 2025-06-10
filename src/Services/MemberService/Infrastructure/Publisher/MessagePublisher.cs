using Application.Events;
using Application.Interfaces;
using RabbitMQ.Client;

namespace Infrastructure.Publisher;

public class MessagePublisher : IMessagePublisher
{
    private readonly ConnectionFactory _connectionFactory;

    public MessagePublisher(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task PublishMemberCreatedAsync(MemberCreatedEvent memberEvent)
    {
        throw new NotImplementedException();
    }
}
using Application.Events;

namespace Application.Interfaces;

public interface IMessagePublisher
{
    Task PublishMemberCreatedAsync(MemberCreatedEvent memberEvent);
}
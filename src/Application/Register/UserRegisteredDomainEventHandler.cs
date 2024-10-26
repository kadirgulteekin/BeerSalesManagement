using Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Register;

internal sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

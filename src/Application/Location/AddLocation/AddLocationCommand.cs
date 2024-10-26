using Application.Abstractions.Messaging;

namespace Application.Location.AddLocation;

public sealed class AddLocationCommand : ICommand<Guid>
{
    public string LocationName { get; set; }
}

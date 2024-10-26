using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using MediatR;
using SharedKernel;

namespace Application.Location.AddLocation;

public sealed class AddLocationCommandHandler(IApplicationDbContext context)
    : ICommandHandler<AddLocationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddLocationCommand command, CancellationToken cancellationToken)
    {
        var location = new Domain.Beers.Location
        {
            Name = command.LocationName
        };
        context.Locations.Add(location);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(location.LocationId);
    }
}

using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Sales.GenerateSales;

public sealed record GenerateRandomSalesCommand(Guid LocationId, DateTime StartDate, DateTime EndDate, int Count) : ICommand<Result>;
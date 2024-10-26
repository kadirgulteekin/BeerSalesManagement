using Domain.Beers;
using MongoDB.Driver;

namespace Application.Abstractions.Data;

public interface INoSqlDbContext
{
    IMongoCollection<SalesRecord> SalesRecords { get; }
}

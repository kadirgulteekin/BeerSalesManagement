using Domain.Beers;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Application.Abstractions.Data;

internal class NoSqlDbContext : INoSqlDbContext
{
    private readonly IMongoDatabase _database;
    public NoSqlDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("NoSqlDatabase"));
        _database = client.GetDatabase(configuration["NoSqlDatabaseName"]);
    }
    public IMongoCollection<SalesRecord> SalesRecords => _database.GetCollection<SalesRecord>("SalesRecords");
}

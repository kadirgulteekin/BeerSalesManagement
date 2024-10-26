var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Web_Api>("web-api");

builder.Build().Run();

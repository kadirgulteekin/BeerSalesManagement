using Polly;
using Web.Api.Infrastructure;

namespace Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        // REMARK: If you want to use Controllers, you'll need this.
        services.AddControllers();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddResiliencePipeline("retry", builder =>
        {
            builder.AddRetry(new Polly.Retry.RetryStrategyOptions
            {
                Delay = TimeSpan.FromSeconds(1),
                MaxRetryAttempts = 2,
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true
            });
        });
        return services;
    }
}
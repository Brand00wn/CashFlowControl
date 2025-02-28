using MassTransit;
using Quartz;
using Domain.Models.Consolidation;
using MediatR;
using Application.Launch.Launch.Query.GetAllDailyLaunches;

public class DailyConsolidationJob : IJob
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;
    private readonly ILogger<DailyConsolidationJob> _logger;

    public DailyConsolidationJob(IPublishEndpoint publishEndpoint, IMediator mediator, ILogger<DailyConsolidationJob> logger)
    {
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Executing Daily Consolidation...");

        try
        {
            var dailyLaunches = await _mediator.Send(new GetAllPendingDailyLaunchesQuery());

            if (dailyLaunches.Count > 0)
            {
                var message = new DailyConsolidationEvent
                {
                    Launches = dailyLaunches
                };

                await _publishEndpoint.Publish(message);
                _logger.LogInformation($"Consolidation sended: {DateTime.UtcNow}, Total Launches: {message.Launches.Count}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on sending daily consolidation event.");
        }
    }
}

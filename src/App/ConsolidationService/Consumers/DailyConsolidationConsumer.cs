using Application.Consolidation.Consolidation.Command.CreateConsolidation;
using Domain.Models.Consolidation;
using MassTransit;
using MediatR;

public class DailyConsolidationConsumer : IConsumer<DailyConsolidationEvent>
{
    private readonly ILogger<DailyConsolidationConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;

    public DailyConsolidationConsumer(
        IMediator mediator,
        IPublishEndpoint publishEndpoint,
        ILogger<DailyConsolidationConsumer> logger)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DailyConsolidationEvent> context)
    {
        var message = context.Message;
        _logger.LogDebug($"Received daily consolidation message for {message.Launches.Count} launches.");

        if(message.Launches.Count > 0)
        {
            var consolidationCommand = new CreateConsolidationCommand()
            {
                Launches = message.Launches
            };

            var launches = await _mediator.Send(consolidationCommand);

            var consolidationCompletedEvent = new ConsolidationCompletedEvent
            {
                Launches = launches
            };

            await _publishEndpoint.Publish(consolidationCompletedEvent);

            _logger.LogDebug($"Consolidation completed for {message.Launches.Count} launches");
        }
    }
}

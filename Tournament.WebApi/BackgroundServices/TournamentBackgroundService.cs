using Tournament.Application.Interfaces;

namespace Tournament.BackgroundServices;

public class TournamentBackgroundService : BackgroundService
{
    private readonly ILogger<TournamentBackgroundService> _logger;
    private readonly ICompetitionService _competitionService;
    private const int DelayServiceInSecond = 15;

    public TournamentBackgroundService(ILogger<TournamentBackgroundService> logger, 
        ICompetitionService competitionService)
    {
        _logger = logger;
        _competitionService = competitionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background service started at {time}", DateTime.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(DelayServiceInSecond), stoppingToken);

            _logger.LogInformation($"Competition Guid ={_competitionService.CurrentCompetitionId}");
            _logger.LogInformation("Произошло событие в фоновой службе");

        }
    }
    
}
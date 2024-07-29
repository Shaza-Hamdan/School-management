using System.Net;
using System.Net.Mail;
using TRIAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Trial.DTO;
using TRIAL.Persistence.Repository;
using TRIAL.Persistence.entity;

public class HomeworkCleanupService : BackgroundService
{
    private readonly AppDBContext _appDbContext;
    private readonly ILogger<HomeworkCleanupService> _logger;

    public HomeworkCleanupService(AppDBContext appDbContext, ILogger<HomeworkCleanupService> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanupExpiredHomeworksAsync();
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Run every hour
        }
    }

    private async Task CleanupExpiredHomeworksAsync()
    {
        var now = DateTime.UtcNow;
        var expiredHomeworks = _appDbContext.HwT
            .Where(h => h.Deadline < now)
            .ToList();

        if (expiredHomeworks.Any())
        {
            _appDbContext.HwT.RemoveRange(expiredHomeworks);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Expired homeworks cleaned up.");
        }
    }
}

using Ardalis.Result;

namespace Tournament.Application.Interfaces;

public interface ICompetitionService
{
    Guid CurrentCompetitionId { get; set; }

    Task<Result> ExecuteAsync();
}
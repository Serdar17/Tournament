namespace Tournament.Application.Interfaces;

public interface ICompetitionService
{
    public Guid CurrentCompetitionId { get; set; }
}
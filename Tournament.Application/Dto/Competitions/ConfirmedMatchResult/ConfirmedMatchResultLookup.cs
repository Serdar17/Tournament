using Tournament.Application.Dto.Account;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Competitions.ConfirmedMatchResult;

public class ConfirmedMatchResultLookup
{
    public int ScheduleId { get; set; }
    
    public Guid FirstPlayerId { get; set; }

    public PlayerModel? FirstPlayerModel { get; set; }
    
    public Score? FirstPlayerScore { get; set; }
    
    public Guid SecondPlayerId { get; set; }

    public PlayerModel? SecondPlayerModel { get; set; }
    
    public Score? SecondPlayerScore { get; set; }
}
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Account;

public class MatchResultModel
{
    public Guid FirstPlayerId { get; set; }

    public PlayerModel FirstPlayerDto { get; set; }

    public Guid SecondPlayerId { get; set; }

    public PlayerModel SecondPlayerDto { get; set; }
    
    public Score Score { get; set; }
}
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.ConfirmedMatchResult;

namespace Tournament.Application.Tournament.Commands.SaveSchedule;

public class SaveScheduleCommand : ICommand
{
    public Guid CompetitionId { get; set; }

    public ConfirmedMatchResultLookup ConfirmedMatchResultLookup { get; set; }
}
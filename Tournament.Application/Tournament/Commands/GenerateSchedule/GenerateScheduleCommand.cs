using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Tournament.Commands.GenerateSchedule;

public class GenerateScheduleCommand : ICommand
{
    public Guid CompetitionId { get; set; }
}
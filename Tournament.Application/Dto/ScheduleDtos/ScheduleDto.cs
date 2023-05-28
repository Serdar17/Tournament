namespace Tournament.Application.Dto.ScheduleDtos;

public class ScheduleDto
{
    public ScheduleDto(int scheduleId, PlayerDto firstPlayer, PlayerDto secondPlayer, int tableNumber)
    {
        ScheduleId = scheduleId;
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
        TableNumber = tableNumber;
    }

    public int ScheduleId { get; set; }
    
    public PlayerDto FirstPlayer { get; set; }

    public PlayerDto SecondPlayer { get; set; }

    public int TableNumber { get; set; }
    
    public bool HasPlayed { get; set; }
    
    public bool IsConfirmed { get; set; }

    public int FirstPlayerScore { get; set; }

    public int SecondPlayerScore { get; set; }
}
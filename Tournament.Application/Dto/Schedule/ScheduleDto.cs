namespace Tournament.Application.Dto.Schedule;

public class ScheduleDto
{
    public ScheduleDto(PlayerDto firstPlayer, PlayerDto secondPlayer, int tableNumber)
    {
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
        TableNumber = tableNumber;
    }
    
    public PlayerDto FirstPlayer { get; set; }

    public PlayerDto SecondPlayer { get; set; }

    public int TableNumber { get; set; }
}
namespace Tournament.Domain.Models.Competitions;

public class Score
{
    public Score(int firstPlayerScored, int secondPlayerScored, bool isConfirmed)
    {
        FirstPlayerScored = firstPlayerScored;
        SecondPlayerScored = secondPlayerScored;
        IsConfirmed = isConfirmed;
    }
    
    public int FirstPlayerScored { get; set; }

    public int SecondPlayerScored { get; set; }

    public bool IsConfirmed { get; set; }
}
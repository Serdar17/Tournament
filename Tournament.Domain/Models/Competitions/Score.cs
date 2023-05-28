namespace Tournament.Domain.Models.Competitions;

public class Score
{
    public Score(int scored, int missed, bool isConfirmed)
    {
        Scored = scored;
        Missed = missed;
        IsConfirmed = isConfirmed;
    }
    
    public int Scored { get; set; }

    public int Missed { get; set; }

    public bool IsConfirmed { get; set; }
}
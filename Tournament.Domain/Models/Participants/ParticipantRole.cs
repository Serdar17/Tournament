namespace Tournament.Domain.Models.Participants;

public static class ParticipantRole
{
    public const string Participant = "Participant";

    public const string Referee = "Referee";

    public const string Admin = "Admin";
    
    public const string AdminAndReferee = $"{Admin},{Referee}";
}
using Tournament.Dto;
using Tournament.Models;

namespace Tournament.Services;

public interface IParticipantService
{
    public List<Participant> GetAll();

    public Participant Create(Participant participant);
    
    public Participant? GetParticipantByUserName(string userName);

    public bool IsValidParticipantInformation(LoginModel loginModel);

    
}
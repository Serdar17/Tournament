// using Tournament.Dto;
// using Tournament.Helpers;
// using Tournament.Models;
// using Tournament.Services;
//
// namespace Tournament.Implementation;
//
// public class ParticipantService : IParticipantService
// {
//     private readonly IParticipantRepository<Participant> _repository;
//     
//     public ParticipantService(IParticipantRepository<Participant> repository)
//     {
//         _repository = repository;
//     }
//     public List<Participant> GetAll()
//     {
//         return _repository.GetAll().ToList();
//     }
//
//     public Participant Create(Participant participant)
//     {
//         var entity = _repository.Create(participant);
//         
//         return entity;
//     }
//
//     public bool IsValidParticipantInformation(LoginModel loginModel)
//     {
//         var participant = _repository
//             .GetAll()
//             .FirstOrDefault(x => x.Email.Equals(loginModel) || x.Phone.Equals(loginModel));
//
//         if (participant is null)
//         {
//             return false;
//         }
//
//         if (participant.Password != HashPasswordHelper.HashPassword(loginModel.Password))
//         {
//             return false;
//         }
//
//         return true;
//     }
//
//     public Participant? GetParticipantByUserName(string userName)
//     {
//         var participant = _repository
//             .GetAll()
//             .FirstOrDefault(x => x.Email.Equals(userName) || x.Phone.Equals(userName));
//
//         return participant;
//     }
// }
using Ardalis.Result;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Account;
using Tournament.Domain.Models.Participants;

namespace Tournament.Application.Interfaces;

public interface IParticipantService
{
    public List<ApplicationUser> GetAll();

    public Task<Result<ParticipantInfoModel>> GetParticipantByIdAsync(Guid guid);

    public Task<Result> ConfirmMatchResult(MatchResultModel matchResult, Guid id);

    public Task<Result<ParticipantInfoModel>> PatchParticipantAsync(Guid guid, 
        JsonPatchDocument<ParticipantInfoModel> patch);

    public Task<Result<UserDto>> UpdateParticipant(UserDto user);

    public Task<Result> DeleteParticipantByIdAsync(Guid guid);

}
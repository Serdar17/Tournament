using Ardalis.Result;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Dto;
using Tournament.Models;

namespace Tournament.Services;

public interface IParticipantService
{
    public List<Participant> GetAll();

    public Task<Result<Participant>> GetParticipantByIdAsync(Guid guid);

    public Task<Result<ParticipantInfoModel>> PatchParticipantAsync(Guid guid, 
        JsonPatchDocument<ParticipantInfoModel> patch);

    public Task<Result> DeleteParticipantByIdAsync(Guid guid);

}
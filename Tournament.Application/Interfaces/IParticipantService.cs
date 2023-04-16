using Ardalis.Result;
using Tournament.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Application.Dto;

namespace Tournament.Application.Interfaces;

public interface IParticipantService
{
    public List<Participant> GetAll();

    public Task<Result<Participant>> GetParticipantByIdAsync(Guid guid);

    public Task<Result<ParticipantInfoModel>> PatchParticipantAsync(Guid guid, 
        JsonPatchDocument<ParticipantInfoModel> patch);

    public Task<Result> DeleteParticipantByIdAsync(Guid guid);

}
﻿using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Application.Dto;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models;
using Tournament.Domain.Models.Participants;

namespace Tournament.Services;

public sealed class ParticipantService : IParticipantService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    
    public ParticipantService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public List<ApplicationUser> GetAll()
    {
        return _userManager.Users.ToList();
    }

    public async Task<Result<ApplicationUser>> GetParticipantByIdAsync(Guid guid)
    {
        var participant = await _userManager.FindByIdAsync(guid.ToString());

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }

        return Result.Success(participant);
    }

    public async Task<Result<ParticipantInfoModel>> PatchParticipantAsync(Guid guid, 
        JsonPatchDocument<ParticipantInfoModel> patch)
    {
        var participant = await _userManager.FindByIdAsync(guid.ToString());

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }
        
        var participantPatch = _mapper.Map<ParticipantInfoModel>(participant);
        
        patch.ApplyTo(participantPatch);

        _mapper.Map(participantPatch, participant);
        
        participant.SetRating();

        var result = await _userManager.UpdateAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {guid.ToString()} could not be updated");
        }

        return Result.Success(participantPatch);
    }

    public async Task<Result<UserDto>> UpdateParticipant(UserDto user)
    {
        var participant = await _userManager.FindByIdAsync(user.Id);

        if (participant is null)
        {
            return Result.Error($"Participant with id: {user.Id} doesn't exist in the database.");
        }
        
        _mapper.Map(user, participant);
        
        participant.SetRating();

        var result = await _userManager.UpdateAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {user.Id.ToString()} could not be updated");
        }

        return Result.Success(user);
    }

    public async Task<Result> DeleteParticipantByIdAsync(Guid guid)
    {
        var participant = await _userManager.FindByIdAsync(guid.ToString());

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }

        var result = await _userManager.DeleteAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {guid.ToString()} could not be deleted");
        }

        return Result.Success();
    }
}
﻿using Ardalis.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Create;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Competitions.Commands.CreateCompetition;

public class CreateCompetitionInfoHandler : 
    ICommandHandler<CreateCompetitionCommand, CompetitionListVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCompetitionInfoHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<CompetitionListVm>> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competitionInfo = new Competition()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            TableCount = request.TableCount,
            RoundsCount = request.RoundsCount,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            CreationDateTime = DateTime.Now,
            PlaceDescription = request.PlaceDescription
        };

        await _dbContext.Competitions.AddAsync(competitionInfo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var entities = await _dbContext.Competitions
            .ProjectTo<CompetitionLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result.Success(new CompetitionListVm() { Competition = entities });
    }
}
﻿using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.ScheduleDtos;

namespace Tournament.Application.Competitions.Queries.GetScheduleByCompetitionId;

public class GetScheduleByCompetitionIdQuery : IQuery<List<ScheduleDto>>
{
    public Guid CompetitionId { get; set; }
    
}
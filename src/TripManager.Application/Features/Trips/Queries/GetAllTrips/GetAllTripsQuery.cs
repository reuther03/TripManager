﻿using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Features.Trips.Queries.GetTrip;
using TripManager.Common.Abstractions;
using TripManager.Common.Primitives.Pagination;

namespace TripManager.Application.Features.Trips.Queries.GetAllTrips;

public record GetAllTripsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TripDto>>
{
    internal sealed class Handler : IQueryHandler<GetAllTripsQuery, PaginatedList<TripDto>>
    {
        private readonly ITripDbContext _dbContext;
        private readonly IUserContext _userContext;

        public Handler(ITripDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<PaginatedList<TripDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            var trips = await _dbContext.Trips
                .Include(x => x.Activities)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Where(x => x.UserId == _userContext.UserId) // xd?
                .Select(x => TripDto.AsDto(x))
                .ToListAsync(cancellationToken);

            var totalTrips = await _dbContext.Trips.CountAsync(cancellationToken);

            return PaginatedList<TripDto>.Create(request.Page, request.PageSize, totalTrips, trips);
        }
    }
}
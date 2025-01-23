using AutoMapper;
using DownNotifier.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Features.TargetAppFeatures.Query
{
    public class GetTargetAppsForUserResponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; set; }
        public DateTime LastCheckDate { get; set; }
    }
    public class GetTargetAppsForUserRequest : IRequest<List<GetTargetAppsForUserResponse>>
    {
        public int UserId { get; set; }

        public GetTargetAppsForUserRequest(int userID)
        {
            UserId = userID;
        }
    }

    public class GetTargetAppsForUserRequestHandler : IRequestHandler<GetTargetAppsForUserRequest, List<GetTargetAppsForUserResponse>>
    {
        private readonly ITargetAppRepository targetAppRepository;
        private readonly IMapper mapper;

        public GetTargetAppsForUserRequestHandler(ITargetAppRepository targetAppRepository, IMapper mapper)
        {
            this.targetAppRepository = targetAppRepository;
            this.mapper = mapper;
        }
        public Task<List<GetTargetAppsForUserResponse>> Handle(GetTargetAppsForUserRequest request, CancellationToken cancellationToken)
        {
            var targetApps = targetAppRepository.GetAll(s => s.UserId == request.UserId);
            var mappedResult = mapper.Map<List<GetTargetAppsForUserResponse>>(targetApps);
            return Task.FromResult(mappedResult);
        }
    }
}

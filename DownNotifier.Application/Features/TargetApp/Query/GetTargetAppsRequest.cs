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
    public class GetTargetAppsResponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; set; }
        public DateTime LastCheckDate { get; set; }
    }
    public class GetTargetAppsRequest : IRequest<List<GetTargetAppsResponse>>
    {
    }

    public class GetTargetAppsRequestHandler : IRequestHandler<GetTargetAppsRequest, List<GetTargetAppsResponse>>
    {
        private readonly ITargetAppRepository targetAppRepository;
        private readonly IMapper mapper;

        public GetTargetAppsRequestHandler(ITargetAppRepository targetAppRepository, IMapper mapper)
        {
            this.targetAppRepository = targetAppRepository;
            this.mapper = mapper;
        }
        public Task<List<GetTargetAppsResponse>> Handle(GetTargetAppsRequest request, CancellationToken cancellationToken)
        {
            var targetApps = targetAppRepository.GetAll();
            var mappedResult = mapper.Map<List<GetTargetAppsResponse>>(targetApps);
            return Task.FromResult(mappedResult);
        }
    }
}

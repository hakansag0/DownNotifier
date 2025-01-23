using AutoMapper;
using DownNotifier.Application.Repositories;
using MediatR;

namespace DownNotifier.Application.Features.TargetAppFeatures.Query
{
    public class GetTargetAppByIdForUserResponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; set; }
        public DateTime LastCheckDate { get; set; }
    }
    public class GetTargetAppByIdForUserRequest : IRequest<GetTargetAppByIdForUserResponse>
    {
        public int UserId { get; set; }
        public int TargetAppId { get; set; }

        public GetTargetAppByIdForUserRequest(int userID, int targetAppId)
        {
            UserId = userID;
            TargetAppId = targetAppId;
        }
    }
    public class GetTargetAppByIdForUserRequestHandler : IRequestHandler<GetTargetAppByIdForUserRequest, GetTargetAppByIdForUserResponse>
    {
        private readonly ITargetAppRepository targetAppRepository;
        private readonly IMapper mapper;

        public GetTargetAppByIdForUserRequestHandler(ITargetAppRepository targetAppRepository, IMapper mapper)
        {
            this.targetAppRepository = targetAppRepository;
            this.mapper = mapper;
        }
        public Task<GetTargetAppByIdForUserResponse> Handle(GetTargetAppByIdForUserRequest request, CancellationToken cancellationToken)
        {
            var targetApp = targetAppRepository.Get(s => s.UserId == request.UserId && s.Id == request.TargetAppId);
            var mappedResult = mapper.Map<GetTargetAppByIdForUserResponse>(targetApp);
            return Task.FromResult(mappedResult);
        }
    }
}

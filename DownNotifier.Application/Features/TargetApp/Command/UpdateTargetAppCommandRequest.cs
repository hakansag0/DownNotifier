using DownNotifier.Application.CustomExceptions.TargetApp;
using DownNotifier.Application.Repositories;
using DownNotifier.Domain.Entities;
using MediatR;

namespace DownNotifier.Application.Features.TargetAppFeatures.Command
{
    public class UpdateTargetAppCommandResponse
    {
        public int Id { get; set; }
    }
    public class UpdateTargetAppCommandRequest : IRequest<UpdateTargetAppCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; set; }
    }

    internal class UpdateTargetAppCommandRequestHandler : IRequestHandler<UpdateTargetAppCommandRequest, UpdateTargetAppCommandResponse>
    {
        private readonly ITargetAppRepository targetAppRepository;

        public UpdateTargetAppCommandRequestHandler(ITargetAppRepository targetAppRepository)
        {
            this.targetAppRepository = targetAppRepository;
        }

        public Task<UpdateTargetAppCommandResponse> Handle(UpdateTargetAppCommandRequest request, CancellationToken cancellationToken)
        {
            TargetApp? targetAppToUpdate = targetAppRepository.GetById(request.Id);
            if(targetAppToUpdate == null)
            {
                throw new TargetAppNotFoundException("Application not found.");
            }

            targetAppToUpdate.SetName(request.Name);
            targetAppToUpdate.SetURL(request.URL);
            targetAppToUpdate.SetMonitoringIntervalInSeconds(request.MonitoringIntervalInSeconds);


            if (targetAppRepository.Get(s => s.Name == targetAppToUpdate.Name && s.UserId == targetAppToUpdate.UserId && s.Id != targetAppToUpdate.Id) != null)
            {
                throw new TargetAppAlreadyExistsException("You have an application with same name. Try different name.");
            }

            targetAppRepository.Update(targetAppToUpdate);
            targetAppRepository.SaveChanges();
            return Task.FromResult(new UpdateTargetAppCommandResponse()
            {
                Id = targetAppToUpdate.Id,
            });
        }
    }
}

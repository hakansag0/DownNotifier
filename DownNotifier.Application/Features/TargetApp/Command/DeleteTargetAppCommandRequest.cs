using DownNotifier.Application.CustomExceptions.TargetApp;
using DownNotifier.Application.Repositories;
using DownNotifier.Domain.Entities;
using MediatR;

namespace DownNotifier.Application.Features.TargetAppFeatures.Command
{
    public class DeleteTargetAppCommandRequest : IRequest
    {
        public int AppId { get; set; }
        public int UserId { get; set; }

        public DeleteTargetAppCommandRequest(int appId, int userId)
        {
            AppId = appId;
            UserId = userId;
        }
    }

    internal class DeleteTargetAppCommandRequestHandler : IRequestHandler<DeleteTargetAppCommandRequest>
    {
        private readonly ITargetAppRepository targetAppRepository;

        public DeleteTargetAppCommandRequestHandler(ITargetAppRepository targetAppRepository) {
            this.targetAppRepository = targetAppRepository;
        }
        public Task Handle(DeleteTargetAppCommandRequest request, CancellationToken cancellationToken)
        {
            TargetApp? targetApp = targetAppRepository.Get(s => s.Id == request.AppId && s.UserId == request.UserId);
            if(targetApp == null)
            {
                throw new TargetAppNotFoundException("App not found.");
            }
            targetAppRepository.Delete(targetApp.Id);
            targetAppRepository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}

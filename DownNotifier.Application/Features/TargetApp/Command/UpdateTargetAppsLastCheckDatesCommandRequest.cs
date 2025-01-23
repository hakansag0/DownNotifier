using DownNotifier.Application.Repositories;
using DownNotifier.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Features.TargetAppFeatures.Command
{
    public class UpdateTargetAppsLastCheckDatesCommandRequest : IRequest
    {
        public Dictionary<int, DateTime> AppChecks { get; set; } = default!;
    }

    internal class UpdateTargetAppsLastCheckDatesCommandRequestHandler : IRequestHandler<UpdateTargetAppsLastCheckDatesCommandRequest>
    {
        private readonly ITargetAppRepository targetAppRepository;

        public UpdateTargetAppsLastCheckDatesCommandRequestHandler(ITargetAppRepository targetAppRepository)
        {
            this.targetAppRepository = targetAppRepository;
        }

        public Task Handle(UpdateTargetAppsLastCheckDatesCommandRequest request, CancellationToken cancellationToken)
        {

            List<TargetApp>? targetAppsToUpdate = targetAppRepository.GetAll(s => request.AppChecks.Keys.Contains(s.Id));
            if (targetAppsToUpdate == null || targetAppsToUpdate.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var targetApp in targetAppsToUpdate)
            {
                targetApp.UpdateCheckDate(request.AppChecks[targetApp.Id]);
                targetAppRepository.Update(targetApp);
            }

            targetAppRepository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}

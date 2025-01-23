using DownNotifier.Application.CustomExceptions.TargetApp;
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
    public class CreateTargetAppCommandResponse
    {
        public int Id { get; set; }
    }
    public class CreateTargetAppCommandRequest : IRequest<CreateTargetAppCommandResponse>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; set; }
    }

    internal class CreateTargetAppCommandRequestHandler : IRequestHandler<CreateTargetAppCommandRequest, CreateTargetAppCommandResponse>
    {
        private readonly ITargetAppRepository targetAppRepository;

        public CreateTargetAppCommandRequestHandler(ITargetAppRepository targetAppRepository)
        {
            this.targetAppRepository = targetAppRepository;
        }

        public Task<CreateTargetAppCommandResponse> Handle(CreateTargetAppCommandRequest request, CancellationToken cancellationToken)
        {
            TargetApp targetAppToCreate = TargetApp.Create(request.UserId, request.Name, request.URL, request.MonitoringIntervalInSeconds);

            if (targetAppRepository.Get(s => s.Name == targetAppToCreate.Name && s.UserId == targetAppToCreate.UserId) != null)
            {
                throw new TargetAppAlreadyExistsException("You have an application with same name. Try different name.");
            }

            TargetApp createdTargetApp = targetAppRepository.Add(targetAppToCreate);
            targetAppRepository.SaveChanges();
            return Task.FromResult(new CreateTargetAppCommandResponse()
            {
                Id = createdTargetApp.Id,
            });
        }
    }
}

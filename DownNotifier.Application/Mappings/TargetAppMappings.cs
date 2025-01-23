using AutoMapper;
using DownNotifier.Application.Features.TargetAppFeatures.Query;
using DownNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Mappings
{
    internal class TargetAppMappings : Profile
    {
        public TargetAppMappings()
        {
            CreateMap<TargetApp, GetTargetAppsForUserResponse>();
            CreateMap<TargetApp, GetTargetAppByIdForUserResponse>();
            CreateMap<TargetApp, GetTargetAppsResponse>();
        }
    }
}

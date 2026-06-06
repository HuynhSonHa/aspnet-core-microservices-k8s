using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using PlatformService.Interfaces;
using PlatformService.Mappers;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepository _platformRepository;

        public GrpcPlatformService(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var platforms = _platformRepository.GetAllPlatforms();
            var grpcPlatforms = platforms.Select(p => p.ToGrpcPlatformModel());
            return Task.FromResult(new PlatformResponse { Platforms = { grpcPlatforms } });
        }
    }
}
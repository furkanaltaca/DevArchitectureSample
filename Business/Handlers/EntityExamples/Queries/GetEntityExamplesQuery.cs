
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.EntityExamples.Queries
{
    [SecuredOperation]
    public class GetEntityExamplesQuery : IRequest<IDataResult<IEnumerable<EntityExample>>>
    {
        public class GetEntityExamplesQueryHandler : IRequestHandler<GetEntityExamplesQuery, IDataResult<IEnumerable<EntityExample>>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMediator _mediator;

            public GetEntityExamplesQueryHandler(IEntityExampleRepository entityExampleRepository, IMediator mediator)
            {
                _entityExampleRepository = entityExampleRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EntityExample>>> Handle(GetEntityExamplesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EntityExample>>(await _entityExampleRepository.GetListAsync());
            }
        }
    }
}
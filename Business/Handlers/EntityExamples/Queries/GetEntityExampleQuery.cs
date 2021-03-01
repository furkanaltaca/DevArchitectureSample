
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.EntityExamples.Queries
{
    [SecuredOperation]
    public class GetEntityExampleQuery : IRequest<IDataResult<EntityExample>>
    {
        public int Id { get; set; }

        public class GetEntityExampleQueryHandler : IRequestHandler<GetEntityExampleQuery, IDataResult<EntityExample>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMediator _mediator;

            public GetEntityExampleQueryHandler(IEntityExampleRepository entityExampleRepository, IMediator mediator)
            {
                _entityExampleRepository = entityExampleRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<EntityExample>> Handle(GetEntityExampleQuery request, CancellationToken cancellationToken)
            {
                var entityExample = await _entityExampleRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<EntityExample>(entityExample);
            }
        }
    }
}

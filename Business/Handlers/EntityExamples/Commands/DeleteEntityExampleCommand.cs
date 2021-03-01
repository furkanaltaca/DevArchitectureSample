
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.EntityExamples.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [SecuredOperation]
    public class DeleteEntityExampleCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteEntityExampleCommandHandler : IRequestHandler<DeleteEntityExampleCommand, IResult>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMediator _mediator;

            public DeleteEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMediator mediator)
            {
                _entityExampleRepository = entityExampleRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entityExampleToDelete = _entityExampleRepository.Get(p => p.Id == request.Id);

                _entityExampleRepository.Delete(entityExampleToDelete);
                await _entityExampleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}


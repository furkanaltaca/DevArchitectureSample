
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.EntityExamples.ValidationRules;

namespace Business.Handlers.EntityExamples.Commands
{
    
    [SecuredOperation]
    public class CreateEntityExampleCommand : IRequest<IResult>
    {

        public string Name { get; set; }


        public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IResult>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMediator _mediator;
            public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMediator mediator)
            {
                _entityExampleRepository = entityExampleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEntityExampleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var isThereEntityExampleRecord = _entityExampleRepository.Query().Any(u => u.Name == request.Name);

                if (isThereEntityExampleRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEntityExample = new EntityExample
                {
                    Name = request.Name,

                };

                _entityExampleRepository.Add(addedEntityExample);
                await _entityExampleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
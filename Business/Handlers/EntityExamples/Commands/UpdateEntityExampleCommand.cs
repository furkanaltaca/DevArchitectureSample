
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.EntityExamples.ValidationRules;


namespace Business.Handlers.EntityExamples.Commands
{

    [SecuredOperation]
    public class UpdateEntityExampleCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateEntityExampleCommandHandler : IRequestHandler<UpdateEntityExampleCommand, IResult>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMediator _mediator;

            public UpdateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMediator mediator)
            {
                _entityExampleRepository = entityExampleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEntityExampleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var isThereEntityExampleRecord = await _entityExampleRepository.GetAsync(u => u.Id == request.Id);


                isThereEntityExampleRecord.Name = request.Name;


                _entityExampleRepository.Update(isThereEntityExampleRecord);
                await _entityExampleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}


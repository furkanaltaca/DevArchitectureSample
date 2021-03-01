
using Business.Handlers.EntityExamples.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.EntityExamples.Queries.GetEntityExampleQuery;
using Entities.Concrete;
using static Business.Handlers.EntityExamples.Queries.GetEntityExamplesQuery;
using static Business.Handlers.EntityExamples.Commands.CreateEntityExampleCommand;
using Business.Handlers.EntityExamples.Commands;
using Business.Constants;
using static Business.Handlers.EntityExamples.Commands.UpdateEntityExampleCommand;
using static Business.Handlers.EntityExamples.Commands.DeleteEntityExampleCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EntityExampleHandlerTests
    {
        Mock<IEntityExampleRepository> _entityExampleRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _entityExampleRepository = new Mock<IEntityExampleRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task EntityExample_GetQuery_Success()
        {
            //Arrange
            var query = new GetEntityExampleQuery();

            _entityExampleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EntityExample, bool>>>())).ReturnsAsync(new EntityExample()
//propertyler buraya yazılacak
//{																		
//EntityExampleId = 1,
//EntityExampleName = "Test"
//}
);

            var handler = new GetEntityExampleQueryHandler(_entityExampleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EntityExampleId.Should().Be(1);

        }

        [Test]
        public async Task EntityExample_GetQueries_Success()
        {
            //Arrange
            var query = new GetEntityExamplesQuery();

            _entityExampleRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<EntityExample, bool>>>()))
                        .ReturnsAsync(new List<EntityExample> { new EntityExample() { /*TODO:propertyler buraya yazılacak EntityExampleId = 1, EntityExampleName = "test"*/ } });

            var handler = new GetEntityExamplesQueryHandler(_entityExampleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<EntityExample>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task EntityExample_CreateCommand_Success()
        {
            EntityExample rt = null;
            //Arrange
            var command = new CreateEntityExampleCommand();
            //propertyler buraya yazılacak
            //command.EntityExampleName = "deneme";

            _entityExampleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EntityExample, bool>>>()))
                        .ReturnsAsync(rt);

            _entityExampleRepository.Setup(x => x.Add(It.IsAny<EntityExample>())).Returns(new EntityExample());

            var handler = new CreateEntityExampleCommandHandler(_entityExampleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _entityExampleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task EntityExample_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEntityExampleCommand();
            //propertyler buraya yazılacak 
            //command.EntityExampleName = "test";

            _entityExampleRepository.Setup(x => x.Query())
                                           .Returns(new List<EntityExample> { new EntityExample() { /*TODO:propertyler buraya yazılacak EntityExampleId = 1, EntityExampleName = "test"*/ } }.AsQueryable());

            _entityExampleRepository.Setup(x => x.Add(It.IsAny<EntityExample>())).Returns(new EntityExample());

            var handler = new CreateEntityExampleCommandHandler(_entityExampleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task EntityExample_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEntityExampleCommand();
            //command.EntityExampleName = "test";

            _entityExampleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EntityExample, bool>>>()))
                        .ReturnsAsync(new EntityExample() { /*TODO:propertyler buraya yazılacak EntityExampleId = 1, EntityExampleName = "deneme"*/ });

            _entityExampleRepository.Setup(x => x.Update(It.IsAny<EntityExample>())).Returns(new EntityExample());

            var handler = new UpdateEntityExampleCommandHandler(_entityExampleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _entityExampleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task EntityExample_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEntityExampleCommand();

            _entityExampleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<EntityExample, bool>>>()))
                        .ReturnsAsync(new EntityExample() { /*TODO:propertyler buraya yazılacak EntityExampleId = 1, EntityExampleName = "deneme"*/});

            _entityExampleRepository.Setup(x => x.Delete(It.IsAny<EntityExample>()));

            var handler = new DeleteEntityExampleCommandHandler(_entityExampleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _entityExampleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}


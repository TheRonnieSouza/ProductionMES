using FluentAssertions;
using MediatR;
using Moq;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.UnitTest.FakeRepository;

namespace ProductionMES.UnitTest.Application.AddProductionCommandTest
{
    public class ValidateOPAvaliableBehaviorTest
    {
        [Fact]
        public async Task GivenExistOpAvaliable_WhenValidated_ThenAllowAddingToProduction()
        {
            //Arrange
            var fakeRepository = new FakeProductionRepository();
          
            var command = new AddProductionCommand("402314925ASKR2525",
                                                    "line", "station", "user", "ModelA",
                                                    DateTime.Now);

            var behavior = new AddProduction_ValidateOpAvaliableBehavior(fakeRepository);

            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next())
                    .Returns(Task.FromResult(true));

            //Act

            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            //Assert

            //Assert.True(result);
            result.Should().Be(true);          
            mockNext.Verify(next => next(), Times.Once);

        }        

        [Fact]
        public async Task GivenNotExistOpAvaliable_When_Validated_ThenDoesNotAllowAddingToProduction()
        {
            // Arrange
            var fakeRepository = new FakeProductionRepository();

            var command = new AddProductionCommand("402314925ASKR2525", "line", "station", "user", "ModelC", DateTime.Now);

            var behavior = new AddProduction_ValidateOpAvaliableBehavior(fakeRepository);

            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next()).Returns(Task.FromResult(false));

            // Act
            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            // Assert
            Assert.False(result);
            mockNext.Verify(next => next(), Times.Never());

        }
        
    }
}

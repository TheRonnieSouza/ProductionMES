using MediatR;
using Moq;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Test.Application.Commands
{
    public class AddProductionCaseTests
    {
        //Cenario OK
        [Fact]
        public async Task GivenValidTraceabilityCodeMask_WhenValidated_ThenAllowsAddingToProduction()
        {
            //Arrange
            var mockRepository = new Mock<IProductionRepository>();
            mockRepository.Setup(r => r.MaskValidate("402314925ASKR2525", "currentModel"))
                          .Returns("SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]");

            var behavior = new AddProduction_ValidateMaskBehavior(mockRepository.Object);

            var command = new AddProductionCommand()
            {
                traceabilityCode = "402314925ASKR2525",
                currentModel = "currentModel",
                line = "line",
                station = "station",
                user = "user",
                dataOccurrence = DateTime.Now
            };


            var nextCalled = false;
            var next = new RequestHandlerDelegate<bool>(() =>
            {
                nextCalled = true;
                return Task.FromResult(true);
            });

            //Act

            var result = await behavior.Handle(command, next, CancellationToken.None);

            //Assert

            Assert.True(result); // Deve permitir adicionar à produção
            Assert.True(nextCalled); // Deve continuar para o próximo handler
            mockRepository.Verify(r => r.MaskValidate("402314925ASKR2525", "currentModel"), Times.Once);


        }

        [Fact]
        public async Task GivenInvalidTraceabilityCodeMask_WhenValidated_ThenDoesNotAllowAddingToProduction()
        {
            //Arrange

        }
    }
}

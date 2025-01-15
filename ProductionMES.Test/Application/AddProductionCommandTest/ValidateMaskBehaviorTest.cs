using FluentAssertions;
using MediatR;
using Moq;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.Core.Interfaces;
using ProductionMES.UnitTest.Application.AddProductionCommandTest;
using ProductionMES.UnitTest.FakeRepository;

namespace ProductionMES.Test.Application.Commands
{
    public class ValidateMaskBehaviorTest
    {        
        [Fact]
        public async Task GivenValidTraceabilityCodeMask_WhenValidated_ThenAllowsAddingToProduction()
        {
            //Arrange
            var mockRepository = new Mock<IProductionRepository>();
            mockRepository.Setup(r => r.MaskValidate("ModelA"))
                          .Returns("SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]");

            var behavior = new AddProduction_ValidateMaskBehavior(mockRepository.Object);

            var command = new AddProductionCommand()
            {
                traceabilityCode = "402314925ASKR2525",
                currentModel = "ModelA",
                line = "line",
                station = "station",
                user = "user",
                dataOccurrence = DateTime.Now
            };

            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next())
                    .Returns(Task.FromResult(true));

            //Act
            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            //Assert
           Assert.True(result);           
           mockNext.Verify(next => next(), Times.Once); 
           mockRepository.Verify(r => r.MaskValidate("ModelA"), Times.Once);
        }

        [Fact]
        public async Task GivenInvalidTraceabilityCodeMask_WhenValidated_ThenDoesNotAllowAddingToProduction()
        {
            //Arrange
            var mockRepository = new Mock<IProductionRepository>();
            mockRepository.Setup(m => m.MaskValidate("ModelA"))
                          .Returns("SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]");

            var behavior = new AddProduction_ValidateMaskBehavior(mockRepository.Object);

            var command = new AddProductionCommand()
            {
                traceabilityCode = "402314925ASKR252500",
                currentModel = "ModelA",
                line = "line",
                station = "station",
                user = "user",
                dataOccurrence = DateTime.Now
            };


            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next())
                    .Returns(Task.FromResult(true));

            //Act
            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            //Assert
            Assert.False(result);
            //result.Should().BeFalse();
            mockNext.Verify(next => next(), Times.Never);
            mockRepository.Verify(r => r.MaskValidate("ModelA"), Times.Once);
        }

        [Theory]
        [InlineData("102314925ASKR2525", "ModelA", true)] // mascara valida
        [InlineData("2023B25ASKR2525", "ModelA", false)] // SERIAL_NUMBER tem letras
        [InlineData("302314925ASKR252", "ModelB", true)] // mascara valida para outro modelo
        [InlineData("402314925ASKR2525", "", false)] // mascara inexistente
        public async Task GivenDynamicMasks_WhenValidated_ThenBehaviorAdaptsCorrectly(
                      string traceabilityCode,
                      string currentModel,
                      bool expectedResult)
        {
            // Arrange
            var mockRepository = new Mock<IProductionRepository>();          

              mockRepository.Setup(r => r.MaskValidate(It.IsAny<string>()))
              .Returns((string model) =>
              {
                  switch (model)
                  {
                      case "ModelA":
                          return "SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]";
                      case "ModelB":
                          return "SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[5]";
                      default:
                          return string.Empty; 
                  }
              });

            var behavior = new AddProduction_ValidateMaskBehavior(mockRepository.Object);

            var command = new AddProductionCommand
            {
                traceabilityCode = traceabilityCode,
                currentModel = currentModel,
                line = "line",
                station = "station",
                user = "user",
                dataOccurrence = DateTime.Now
            };

            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next())
                    .Returns(Task.FromResult(true));

            // Act
            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult, result);

            if (expectedResult)
            {
                mockNext.Verify(next => next(), Times.Once); 
            }
            else
            {
                mockNext.Verify(next => next(), Times.Never); 
            }
        }       

        [Theory]
        [InlineData("A02314925ASKR2525","ModelA", false)] //o serial nao pode ter letra
        [InlineData("102314R25ASKR2525", "ModelA", false)] // o dia Juliano nao pode ter letra
        [InlineData("20231492AASKR2525", "ModelA", false)] // o ano nao pode ter letras
        [InlineData("30231492522KR2525", "ModelA", false)] //client nao pode ser numero
        [InlineData("402314925ASKR252500", "ModelA", false)] //codigo de rastreabilidade tem mais caracteres do que a mascara permite
        [InlineData("502314925ASKR252", "ModelA", false)] //codigo de rastreabilidade tem menos caracteres do que a mascara permite      
        public async Task GivenTraceabilityCodeIsInvalid_WhenValidated_ThenDoesNotAllowAddingToProduction(
                            string traceabilityCode, 
                            string model,
                            bool expectedResult)
        {
            //Arrange
            var mockRepository = new FakeProductionRepository();                    

            var behavior = new AddProduction_ValidateMaskBehavior(mockRepository);


            var mockNext = new Mock<RequestHandlerDelegate<bool>>();
            mockNext.Setup(next => next())
                    .Returns(Task.FromResult(true));

            var command = new AddProductionCommand()
            {
                traceabilityCode = traceabilityCode,
                currentModel = model,
                line = "line",
                station = "station",
                user = "user",
                dataOccurrence = DateTime.Now
            };

            //Act

            var result = await behavior.Handle(command, mockNext.Object, CancellationToken.None);

            //Assert
                        
            Assert.False(result);
            //result.Should().BeFalse();
            Assert.Equal(expectedResult, result);
            //result.Should().Be(expectedResult);
            mockNext.Verify(next => next(), Times.Never);
        }
    }
}

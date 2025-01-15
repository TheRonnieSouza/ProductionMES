using FluentAssertions;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.UnitTest.FakeRepository;

namespace ProductionMES.UnitTest.Application.AddProductionCommandTest
{
    public class AddProductionTest
    {
        [Fact]
        public async Task GivenTraceabilityCodeIsOk_WhenValidated_ThenAllowAddingToProduction()
        {
            //Arrange
            var fakeRepository = new FakeProductionRepository();

            var command = new AddProductionCommand("102314925ASKR2525",
                                                    "line",
                                                    "station",
                                                    "user",
                                                    "ModelA",
                                                    DateTime.Now);

            var partProduction = command.ToEntity();

            var handler = new AddProductionCommandHandler(fakeRepository);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert

            Assert.True(result);
            Assert.Equal(command.traceabilityCode, partProduction.TraceabilityCode);
            command.line.Should().Be(partProduction.Line);
            command.station.Should().Be(partProduction.Station);
            command.user.Should().Be(partProduction.User);

        }


































        //Teste usando o repositório real
        //[Fact]
        //public async Task GivenTraceabilityCodeIsOk_WhenValidated_ThenAllowAddingToProduction()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IProductionRepository>();

        //    var command = new AddProductionCommand("102314925ASKR2525",
        //                                            "line",
        //                                            "station",
        //                                            "user",
        //                                            "modelA",
        //                                            DateTime.Now);

        //    var partProduction = command.ToEntity();

        //    mockRepository.Setup(m => m.AddProduction(It.Is<PartProduction>(p =>
        //                                              p.TraceabilityCode == "102314925ASKR2525" &&
        //                                              p.Line == "line" &&
        //                                              p.Station == "station" &&
        //                                              p.User == "user")))
        //                                              .Returns(550);

        //    var handler = new AddProductionCommandHandler(mockRepository.Object);

        //    //Act

        //    var result = await handler.Handle(command, CancellationToken.None);

        //    //Assert

        //    Assert.True(result);
        //    Assert.Equal(command.traceabilityCode, partProduction.TraceabilityCode);
        //    command.line.Should().Be(command.line);
        //    command.station.Should().Be(command.station);
        //    command.user.Should().Be(command.user);

        //    mockRepository.Verify(m => m.AddProduction(It.Is<PartProduction>(p =>
        //     p.TraceabilityCode == "102314925ASKR2525" &&
        //     p.Line == "line" &&
        //     p.Station == "station" &&
        //     p.User == "user")), Times.Once);
        //}
    }
}

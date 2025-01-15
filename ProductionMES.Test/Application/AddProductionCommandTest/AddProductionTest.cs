using FluentAssertions;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.UnitTest.FakeRepository;

namespace ProductionMES.UnitTest.Application.AddProductionCommandTest
{
    public class AddProductionTest
    {
        [Fact]
        public async Task GivenTraceabilityCodeIsOk_WhenAddingToProduction_ThenProductionInsertSucess()
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

        [Fact]

        public async Task GivenInsertFail_WhenAddinToProduction_ThenReturnFalse()
        {

            //Arrange
            var fakeRepository = new FakeProductionRepository();
                 fakeRepository.SimulateInsertFailure(true);

            var command = new AddProductionCommand("102314925ASKR2525",
                                                    "line",
                                                    "station",
                                                    "user",
                                                    "ModelA",
                                                    DateTime.Now);
            var partProduction = command.ToEntity();

            var handle =  new AddProductionCommandHandler(fakeRepository);

            //Act

            var result = await handle.Handle(command, CancellationToken.None);

            //Assert

            Assert.False(result);
        }

    }
}

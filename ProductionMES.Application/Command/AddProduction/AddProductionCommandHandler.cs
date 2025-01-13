using MediatR;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProductionCommandHandler : IRequestHandler<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;

        public AddProductionCommandHandler(IProductionRepository repository)
        {
            _repository = repository;
        }
       
        public Task<bool> Handle(AddProductionCommand request, CancellationToken cancellationToken)
        {
            var partProduction = request.ToEntity();
            var resultIdStatus = _repository.AddProductionEstacaoStatus(partProduction);

            return Task.FromResult(true);
        }
    }
}

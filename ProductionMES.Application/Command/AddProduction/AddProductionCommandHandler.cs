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
       
        public async Task<bool> Handle(AddProductionCommand request, CancellationToken cancellationToken)
        {
            var partProduction = request.ToEntity();
            int resultIdStatus =  _repository.AddProduction(partProduction);

            if(resultIdStatus > 0)
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }
    }
}

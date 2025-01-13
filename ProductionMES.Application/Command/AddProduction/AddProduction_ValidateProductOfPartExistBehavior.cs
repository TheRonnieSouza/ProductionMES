using MediatR;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProduction_ValidateProductOfPartExistBehavior : IPipelineBehavior<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;

        public AddProduction_ValidateProductOfPartExistBehavior(IProductionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddProductionCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            if(_repository.ProductOfPartExist(request.traceabilityCode,request.currentModel));
             return await next();

            return await next(); ;// return await Task.FromResult(false);
        }
    }
}

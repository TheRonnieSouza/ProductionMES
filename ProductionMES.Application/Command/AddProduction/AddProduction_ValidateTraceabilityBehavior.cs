using MediatR;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProduction_ValidateTraceabilityBehavior : IPipelineBehavior<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;
        public AddProduction_ValidateTraceabilityBehavior(IProductionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddProductionCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            if ( _repository.ValidateTraceabilityIsUnique(request.traceabilityCode))
                return await next();

            //return Task.FromResult(false);
            return await next();

        }
    }
}

using MediatR;
using ProductionMES.Core.Entities;
using ProductionMES.Core.Enum;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProduction_ValidateOpAvaliableBehavior : IPipelineBehavior<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;
        public AddProduction_ValidateOpAvaliableBehavior(IProductionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddProductionCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
           ProductionOrder order =  _repository.ExistOpAvaliable(request.line, request.station, request.traceabilityCode,request.currentModel);

            if (order == null)
                return false;

            if (order.Status == ProductionOrderStatus.InProgress)
                return await next();

                return false;
        }
    }
}

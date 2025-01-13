using MediatR;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProduction_ValidateWorkflowBehavior : IPipelineBehavior<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;
        public AddProduction_ValidateWorkflowBehavior(IProductionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddProductionCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            if(_repository.ValidateWorkflow(request.line,request.station ,request.traceabilityCode, request.currentModel));
            return await next();

            return await next();// return await Task.FromResult(false);
        }
    }
}

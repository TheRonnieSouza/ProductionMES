//using MediatR;
//using ProductionMES.Core.Interfaces;

//namespace ProductionMES.Application.Queries.ValidateTraceability
//{
//    public class ValidateTraceabilityQuery : IRequest<bool>
//    {
//        public string traceabilityCode { get; private set; }
//        public string line { get; private set; }
//        public string station { get; private set; }
//        public string user { get; private set; }

//    }

//    public class ValidateTraceabilityQueryHandler : IRequestHandler<ValidateTraceabilityQuery, bool>
//    {
//        private readonly IProductionRepository _repository;

//        public ValidateTraceabilityQueryHandler(IProductionRepository repository)
//        {
//            _repository = repository;
//        }

//        public Task<bool> Handle(ValidateTraceabilityQuery request, CancellationToken cancellationToken)
//        {

//            _repository.AddProduction();


//        }
//    }
//}

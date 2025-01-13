using MediatR;
using ProductionMES.Core.Entities;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProductionCommand : IRequest<bool>
    {
        public AddProductionCommand()
        {
        }
        public AddProductionCommand(string traceabilityCode, string line, string station, string user, string currentModel, DateTime dataOccurrence)
        {
            this.traceabilityCode = traceabilityCode;
            this.line = line;
            this.station = station;
            this.user = user;
            this.currentModel = currentModel;
            this.dataOccurrence = dataOccurrence;
        }
        public string  traceabilityCode { get;  set; }
        public string line { get;  set; }
        public string station { get;  set; }
        public string user { get;  set; }
        public string currentModel { get;  set; }  
        public DateTime dataOccurrence { get;  set; }

        public PartProduction ToEntity() => new(line, station, traceabilityCode, user);
    }
}

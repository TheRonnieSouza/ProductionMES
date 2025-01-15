using ProductionMES.Core.Entities;

namespace ProductionMES.Core.Interfaces
{
    public interface IProductionRepository
    {
        public string MaskValidate(string currentModel);

        public ProductionOrder ExistOpAvaliable(string line, string station, string traceabilityCode, string currentModel);
    
        public bool ProductOfPartExist(string traceabilityCode, string currentModel);

        public bool ValidateTraceabilityIsUnique(string traceabilityCode);
    
        public bool ValidateWorkflow(string line, string station, string traceabilityCode, string currentModel);
        
        public object AddProduction(PartProduction partProduction);

    }
}

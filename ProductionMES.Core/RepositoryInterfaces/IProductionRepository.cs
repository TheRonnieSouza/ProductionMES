using ProductionMES.Core.Entities;

namespace ProductionMES.Core.Interfaces
{
    public interface IProductionRepository
    {
        public string MaskValidate(string traceabilityCode, string currentModel);

        public ProductionOrder ExistOpAvaliable(string line, string station, string traceabilityCode, string currentModel);
    
        public bool ProductOfPartExist(string traceabilityCode, string currentModel);

        public bool ValidateTraceabilityIsUnique(string traceabilityCode);
    
        public bool ValidateWorkflow(string line, string station, string traceabilityCode, string currentModel);
        
        public int AddProductionEstacaoStatus(PartProduction partProduction);

        public void AddProductionEstacaoRastreabilidade(PartProduction partProduction, int idStatus);

    }
}

using ProductionMES.Core.Entities;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Infrastructure.ProductionRepository
{
    public class ProductionRepository : IProductionRepository
    {
        public int AddProductionEstacaoStatus(PartProduction partProduction)
        {
            if(!partProduction.Status)
            {
                return 1;
            }
            return 0;
        }

        public void AddProductionEstacaoRastreabilidade(PartProduction partProduction, int idStatus)
        {
            throw new NotImplementedException();
        }

        public ProductionOrder ExistOpAvaliable(string line, string station, string traceabilityCode, string currentModel)
        {
            throw new NotImplementedException();
        }

        public string MaskValidate(string traceabilityCode, string currentModel)
        {
            string mask = "SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]";
            //mask = string.Empty;
            return mask;

            // string mask = "SERIAL_NUMBER[-4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]";

        }

        public bool ProductOfPartExist(string traceabilityCode, string currentModel)
        {
            throw new NotImplementedException();
        }

        public bool ValidateTraceabilityIsUnique(string traceabilityCode)
        {
            throw new NotImplementedException();
        }

        public bool ValidateWorkflow(string line, string station, string traceabilityCode, string currentModel)
        {
            throw new NotImplementedException();
        }
    }
}

using ProductionMES.Core.Entities;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Infrastructure.ProductionRepository
{
    public class ProductionRepository : IProductionRepository
    {
        public object AddProduction(PartProduction partProduction)
        {
            return 550;            
        }       

        public ProductionOrder ExistOpAvaliable(string line, string station, string traceabilityCode, string currentModel)
        {
            if (currentModel == "ModelA")
            {
                return new ProductionOrder(30,15,"ModelA",Core.Enum.ProductionOrderStatus.InProgress,line,station);
            }
            else if (currentModel == "ModelB")
            {
                return new ProductionOrder(30, 15, "ModelA", Core.Enum.ProductionOrderStatus.Canceled, line, station);
            }
            return null;
        }

        public string MaskValidate(string currentModel)
        {
            if (currentModel == "ModelA")
            {
                return "SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[6]";
            }
            else if (currentModel == "ModelB")
            {
                return "SERIAL_NUMBER[4],DIA_NUMBER[3],ANO_NUMBER[2],CLIENT_TEXT[2],PRODUCT_ALFANUMERICO[5]";
            }
            return string.Empty;
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

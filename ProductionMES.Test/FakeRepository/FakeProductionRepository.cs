using ProductionMES.Core.Entities;
using ProductionMES.Core.Enum;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.UnitTest.FakeRepository
{
    public class FakeProductionRepository : IProductionRepository
    {
        private bool _simulateInsertFailure;

        public void SimulateInsertFailure(bool simulate)
        {
            _simulateInsertFailure = simulate;
        }

        public object AddProduction(PartProduction partProduction)
        {
            if (_simulateInsertFailure)
                return null; 

            return 550; 
        }
        public ProductionOrder ExistOpAvaliable(string line, string station, string traceabilityCode, string currentModel)
        {
            if (currentModel == "ModelA")
                return new ProductionOrder(30, 15, "ModelA", ProductionOrderStatus.InProgress, line, station);
            if (currentModel == "ModelB")
                return new ProductionOrder(30, 15, "ModelB", ProductionOrderStatus.Canceled, line, station);
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

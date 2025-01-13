using ProductionMES.Core.Enum;

namespace ProductionMES.Core.Entities
{
    public class ProductionOrder : EntityBase
    {
        public ProductionOrder() : base()
        {
        }

        public int TotalQuantity { get; private set; }

        public int CurrentQuantity { get; private set; }

        public string Product { get; private set; }

        public ProductionOrderStatus Status { get; private set; }

    }   
}

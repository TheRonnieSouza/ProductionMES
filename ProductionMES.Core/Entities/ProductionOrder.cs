using ProductionMES.Core.Enum;

namespace ProductionMES.Core.Entities
{
    public class ProductionOrder : EntityBase
    {
        public ProductionOrder() : base()
        {
        }

        public ProductionOrder(int totalQuantity,
                                int currentQuantity,
                                string product,
                                ProductionOrderStatus status, 
                                string line, 
                                string station) : base(line,station)
        {           
            TotalQuantity = totalQuantity;
            CurrentQuantity = currentQuantity;
            Product = product;
            Status = status;
        }

        public int TotalQuantity { get; private set; }

        public int CurrentQuantity { get; private set; }

        public string Product { get; private set; }

        public ProductionOrderStatus Status { get; private set; }

    }   
}

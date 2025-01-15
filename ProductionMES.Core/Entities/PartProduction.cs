namespace ProductionMES.Core.Entities
{
    public class PartProduction : EntityBase
    {
        public PartProduction() : base()
        {
        }
        public PartProduction(string line, string station, 
                                string traceabilityCode, string user) : base(line,station)
        {
            TraceabilityCode = traceabilityCode;
            OccurenceDate = DateTime.Now;            
            User = user;
        }
       
        public DateTime OccurenceDate { get; private set; }
        public string TraceabilityCode { get; private set; }
        public bool Status { get; private set; }
        public string User { get; private set;}
        
        public void ReversedStatus()
        {
            Status =  true;
        }
    }
}

namespace ProductionMES.Core.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
        }
        public EntityBase(string line, string station)
        {
            Line = line;
            Station = station;
        }
        public string Line { get; private set; }

        public string Station { get; private set; }

       
    }
}

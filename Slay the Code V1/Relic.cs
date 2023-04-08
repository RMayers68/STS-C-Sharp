namespace STV
{
    public class Relic
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        // constructor
        public Relic(string name, string type,string description)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }
        //Cloning from dictionary constructor
        public Relic(Relic relic)
        {
            this.Name = relic.Name;
            this.Type = relic.Type;
            this.Description = relic.Description;
        }
    }
}
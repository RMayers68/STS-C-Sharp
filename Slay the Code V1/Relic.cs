namespace STV
{
    public class Relic
    {
        public int RelicID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        // constructors
        public Relic(int relicID, string name, string type,string description)
        {
            this.RelicID = relicID;
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }
        public Relic(Relic relic)
        {
            this.RelicID = relic.RelicID;
            this.Name = relic.Name;
            this.Type = relic.Type;
            this.Description = relic.Description;
        }
    }
}

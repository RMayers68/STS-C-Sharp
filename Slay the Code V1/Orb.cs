namespace STV
{
    public class Orb
    {
        //attributes
        public int OrbID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Effect { get; set; }

        //constructor
        public Orb(int orbID, string name,string description,int effect)
        {
            this.OrbID = orbID;
            this.Name = name;
            this.Description = description;
            this.Effect = effect;
        }

        public Orb(Orb orb)
        {
            this.OrbID= orb.OrbID;
            this.Name = orb.Name;
            this.Description = orb.Description;
            this.Effect = orb.Effect;
        }
    }
}

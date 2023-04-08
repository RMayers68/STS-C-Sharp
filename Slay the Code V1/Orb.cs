namespace STV
{
    public class Orb
    {
        //attributes
        public string Name { get; set; }
        public string Description { get; set; }
        public int Effect { get; set; }

        //constructor
        public Orb(string name,string description,int effect)
        {
            this.Name = name;
            this.Description = description;
            this.Effect = effect;
        }

        public Orb(Orb orb)
        {
            this.Name = orb.Name;
            this.Description = orb.Description;
            this.Effect = orb.Effect;
        }
    }
}
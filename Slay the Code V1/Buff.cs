namespace STV
{
    public class Buff
    {
        // attributes
        public int BuffID { get; set; }
        public string Name { get; set; }
        public string BuffDebuff { get; set; }
        public byte Type { get; set; }
        public int? Intensity { get; set; }
        public int? Duration { get; set; }
        public int? Counter { get; set; }


        //constructor

        public Buff(int buffID, string name, string buffDebuff, byte type)
        {
            this.BuffID = buffID;
            this.Name = name;
            this.BuffDebuff = buffDebuff;
            this.Type = type;
        }

        // pulling from dictionary
        public Buff(Buff buff)
        {
            this.BuffID = buff.BuffID;
            this.Name = buff.Name;
            this.BuffDebuff = buff.BuffDebuff;
            this.Type = buff.Type;
        }


        //string method
        public string Description()
        {
            switch (BuffID)
            {
                default:
                    return "";
                case 1:
                    return $"Takes 50% more damage from attacks for {Duration} turns.";
                case 2:
                    return $"Deal 25% less damage with attacks for {Duration} turns.";
                case 3:
                    return $"At the end of its turn, gains {Intensity} Strength.";
                case 4:
                    return $"Increases attack damage by {Intensity}.";
                case 5:
                    return $"Gains {Intensity} Block upon first receiving attack damage.";
                case 6:
                    return $"Gain 25% less Block from cards for {Duration} turns.";
                case 7:
                    return $"Increases the effectiveness of Orbs by {Intensity}.";
            }
        }


        // Duration-related methods
        public void setDuration(int turnDuration)
        {
            if (this.Duration == null)
                this.Duration = turnDuration;
            else this.Duration += turnDuration;
        }

        public void DurationDecrease()
        {
            if (this.Type == 1)
                this.Duration -= 1;
        }

        public bool DurationEnded()
        {
            if (this.Duration == 0)
            {
                return true;
            }
            else return false;
        }

        // Intensity related methods
        public void IntensitySet(int intensity)
        {
            if (this.Intensity == null) this.Intensity = intensity;
            else this.Intensity += intensity;
        }
    }
}

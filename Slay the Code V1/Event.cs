using static Global.Functions;

namespace STV
{
    public abstract class Event
    {
        public string Name { get; set; }
        public string StartOfEncounter { get; set; }
        public List<string> Options { get; set; }
        public static readonly Random EventRNG = new();
        public static void EventDecider(Hero hero, int actModifier)
        {
            hero.Actions.Clear();
            EventRNG.Next(0, hero.Actions.Count);
            actModifier.CompareTo(actModifier);
            return;
        }

        public virtual void EventAction(Hero hero) { }

        public virtual string Result(int result) { return ""; }
    }
}
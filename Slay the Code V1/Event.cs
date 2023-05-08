using STV.Events;
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
            Event e;
            ScreenWipe();
            if (actModifier == 0)
            {
                int act1Events = 22;
                if (hero.Gold < 40)
                    act1Events -= 2;
                e = EventRNG.Next(0, act1Events) switch
                {
                    0 => new BonfireSpirits(),
                    1 => new TheDivineFountain(),
                    2 => new Duplicator(),
                    3 => new GoldenShrine(),
                    4 => new Lab(),
                    5 => new OminousForge(),
                    6 => new Purifier(),
                    7 => new Transmogrifier(),
                    8 => new UpgradeShrine(),
                    9 => new WeMeetAgain(),
                    10 => new WheelOfChange(),
                    11 => new BigFish(),
                    12 => new DeadAdventurer(),
                    13 => new GoldenIdol(),
                    14 => new TheSssserpent(),
                    15 => new WorldOfGoop(),
                    16 => new WingStatue(),
                    17 => new FaceTrader(),
                    18 => new HypnotizingColoredMushrooms(),
                    19 => new LivingWall(),
                    20 => new TheWomanInBlue(),
                    _ => new TheCleric(),
                };
            }
            else if (actModifier == 5)
            {
                int act2Events = 28;
                if (hero.Gold < 75)
                    act2Events -= 2;
                e = EventRNG.Next(0, act2Events) switch
                {
                    0 => new BonfireSpirits(),
                    1 => new TheDivineFountain(),
                    2 => new Duplicator(),
                    3 => new GoldenShrine(),
                    4 => new Lab(),
                    5 => new OminousForge(),
                    6 => new Purifier(),
                    7 => new Transmogrifier(),
                    8 => new UpgradeShrine(),
                    9 => new WeMeetAgain(),
                    10 => new WheelOfChange(),
                    11 => new DesignerInspire(),
                    12 => new FaceTrader(),
                    13 => new AncientWriting(),
                    14 => new Augmenter(),
                    15 => new TheColosseum(),
                    16 => new CouncilOfGhosts(),
                    17 => new CursedTome(),
                    18 => new ForgottenAltar(),
                    19 => new TheLibrary(),
                    20 => new MaskedBandits(),
                    21 => new TheMausoleum(),
                    22 => new TheNest(),
                    24 => new OldBeggar(),
                    25 => new PleadingVagrant(),
                    26 => new Vampires(),
                    _ => new TheWomanInBlue(),
                };
            }
            else
            {
                int act3Events = 18;
                if (hero.Gold < 40)
                    act3Events--;
                e = EventRNG.Next(0, act3Events) switch
                {
                    0 => new BonfireSpirits(),
                    1 => new TheDivineFountain(),
                    2 => new Duplicator(),
                    3 => new GoldenShrine(),
                    4 => new Lab(),
                    5 => new OminousForge(),
                    6 => new Purifier(),
                    7 => new Transmogrifier(),
                    8 => new UpgradeShrine(),
                    9 => new WeMeetAgain(),
                    10 => new WheelOfChange(),
                    11 => new DesignerInspire(),
                    12 => new MindBloom(),
                    13 => new TheMoaiHead(),
                    14 => new MysteriousSphere(),
                    15 => new SensoryStone(),
                    16 => new WindingHalls(),
                    _ => new TheWomanInBlue(),
                };
            }
            e.EventAction(hero);
        }

        public virtual void EventAction(Hero hero) { }

        public virtual string Result(int result) { return ""; }
    }
}
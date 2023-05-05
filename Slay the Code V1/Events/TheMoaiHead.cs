namespace STV
{
    public class TheMoaiHead : Event
    {
        public TheMoaiHead()
        {
            Name = "The Moai Head";
            StartOfEncounter = $"You stumble across something that feels *very* out of place.\nBefore you, an enormous stony head emerges from a large wall segment that does not shift and change like the rest of this area.\nThe head's mouth is wide open, and it reveals large intimidating teeth stained red with blood.\nThe surface of the statue is riddled with pictographs that seem to indicate people throwing themselves into the mouth of this head and being devoured.\nWhy would anyone do that?";
            Options = new() { "[J]ump Inside - Heal to full HP. Lose 12.5% of your Max HP.", "[O]ffer - Receive 333 Gold. Lose Golden Idol.", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "J", "O", "L" };
            if (!hero.HasRelic("Golden Idol"))
            {
                Options[1] = "Locked - Requires Golden Idol";
                choices.Remove("O");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "J")
            {
                Console.WriteLine(Result(0));
                hero.SetMaxHP(Convert.ToInt32(hero.MaxHP * 0.125) * -1);
            }
            else if (playerChoice == "O")
            {
                hero.Relics.Remove(hero.FindRelic("Golden Idol"));
                Console.WriteLine(Result(1));
                hero.GoldChange(333);
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "At first when you step up into the mouth of the statue, nothing happens.\nAs you start to feel more than a little foolish, the huge molars slam down from above, crushing you whole.\n\nDarkness.\n\nSometime later from within the dark, you see a sliver of light, and hear what you now realize is the sound of stony teeth slowly rising upwards.\nYou leave confused.",
                1 => "You jump back a little as the gigantic molars smash down on the idol, smashing it into dust.\nAs the teeth start to rise up again, gold pours forth in a torrent from the opening, flooding you with riches.",
                _ => "You leave, wondering what could have been.",
            };
        }
    }
}

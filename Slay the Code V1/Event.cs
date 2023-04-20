﻿using static Global.Functions;

namespace STV
{
    public class Event
    {
        private static readonly Random EventRNG = new();
        public static void EventDecider(Hero hero, int actModifier)
        {
            hero.Actions.Clear();
            EventRNG.Next(0, hero.Actions.Count);
            actModifier.CompareTo(actModifier);
            return;
        }
    }
}
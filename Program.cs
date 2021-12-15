using System;
using System.Collections.Generic;

namespace AOC2021 {
    class Program {
        static readonly List<BaseDay> days = new List<BaseDay>();

        static void Main() {
            Console.WriteLine(Art.Header);
            Console.WriteLine("");

            days.Add(new Day01());
            days.Add(new Day02());
            days.Add(new Day03());
            days.Add(new Day04());
            days.Add(new Day05());
            days.Add(new Day06());
            days.Add(new Day07());
            days.Add(new Day08());
            days.Add(new Day09());
            days.Add(new Day10());
            days.Add(new Day11());
            days.Add(new Day13());
            days.Add(new Day14());


            BaseDay debugDay = days.Find(x => x.Debug == true);

            if (debugDay != null) {
                debugDay.StartDay();
                debugDay.EndDay();
            } else {
                days.ForEach(x => {
                    x.StartDay();
                    x.EndDay();
                    Console.WriteLine();
                });
            }

            Console.Beep();
            Console.ReadKey();
        }

    }
}

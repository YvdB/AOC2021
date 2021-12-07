using System;
using System.Collections.Generic;
using System.IO;

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

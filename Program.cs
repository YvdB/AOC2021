﻿using System;
using System.Collections.Generic;

namespace AOC2021 {
    class Program {
        static readonly List<BaseDay> days = new List<BaseDay>();

        static void Main() {
            Console.WriteLine("Welcome to the Advent of Code 2021!");
            Console.Beep();

            days.Add(new Day01());
            days.Add(new Day02());
            days.Add(new Day03());
            days.Add(new Day04());

            BaseDay debugDay = days.Find(x => x.Debug == true);

            if (debugDay != null) {
                debugDay.Solve();
            } else {
                days.ForEach(x => x.Solve());
            }

            Console.ReadKey();
        }

    }
}
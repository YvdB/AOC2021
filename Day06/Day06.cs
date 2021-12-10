using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    class Day06 : BaseDay {

        public override bool Debug => false;
        private List<int> originalFish = new List<int>();

        protected override void Solve() {
            originalFish = Day04.ParseBoardRow<int>(GetInput()[0], ',', int.TryParse);

            SolutionPart1 = GetFishPopAfterDays(originalFish, 80);
            SolutionPart2 = GetFishPopAfterDaysBetter(originalFish, 256);
        }

        /// <summary>
        /// Slow method to find part 1
        /// </summary>
        private int GetFishPopAfterDays(List<int> fish, int days) {
            List<int> simFish = new List<int>(fish);
            int dayCount = days;
            while (dayCount > 0) {
                int fishCount = simFish.Count;
                for (int i = 0; i < fishCount; i++) {
                    if (simFish[i] == 0) {
                        simFish[i] = 6;
                        simFish.Add(8);
                    } else {
                        simFish[i]--;
                    }
                }
                dayCount--;
            }
            return simFish.Count;
        }

        /// <summary>
        /// Better method to find part 2, couldn't get this one without some help.
        /// </summary>
        private long GetFishPopAfterDaysBetter(List<int> fish, int days) {
            long[] timers = new long[9];

            fish.ForEach(x => timers[x]++);

            for (int i = 0; i < days; i++) {

                long newFish = timers[0];
                for (int j = 0; j < timers.Length-1; j++) {
                    timers[j] = timers[j + 1];
                }
                timers[timers.Length - 1] = newFish;

                timers[6] += newFish;
            }

            return timers.Sum(x => (long)x);
        }
    }
}

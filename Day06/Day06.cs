using System.Collections.Generic;

namespace AOC2021 {
    class Day06 : BaseDay {

        public override bool Debug => false;

        private List<int> originalFish = new List<int>();

        protected override void Solve() {
            originalFish = Day04.ParseBoardRow<int>(GetInput()[0], ',', int.TryParse);

            int days = 80;

            SolutionPart1 = GetFishPopAfterDays(originalFish, days);
            SolutionPart2 = GetFishPopAfterDaysBetter(originalFish, days);
        }

        /// <summary>
        /// Slow method to find part 1
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
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

        private int GetFishPopAfterDaysBetter(List<int> fish, int days) {
            return 0; // I'm stuck.
        }
    }
}

using System;
using System.Collections.Generic;

namespace AOC2021 {
    class Day07 : BaseDay {

        public override bool Debug => false;

        protected override void Solve() {
            List<int> crabs = Day04.ParseBoardRow<int>(GetInput()[0], ',', int.TryParse);

            int min = int.MaxValue;
            int max = 0;

            crabs.ForEach(x => {
                min = Math.Min(min, x);
                max = Math.Max(max, x);
            });

            SolutionPart1 = CalculateFuel(crabs, min, max, false);
            SolutionPart2 = CalculateFuel(crabs, min, max, true);
        }

        private int CalculateFuel(List<int> crabs, int min, int max, bool part2) {
            int minFuel = int.MaxValue;
            int totalFuel = 0;

            for (int i = min; i <= max; i++) {
                foreach (int crab in crabs) {
                    int steps = Math.Abs(crab - i);
                    totalFuel += part2 ? FuelCost(steps) : steps;
                    if(totalFuel > minFuel) { break; }
                }
                minFuel = Math.Min(minFuel, totalFuel);
                totalFuel = 0;
            }
            return minFuel;
        }

        private int FuelCost(int steps) {
            int cost = 0;
            for (int i = 1; i <= steps; i++) {
                cost += i;
            }
            return cost;
        }
    }
}

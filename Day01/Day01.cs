using System;

namespace AOC2021 {
    class Day01 : BaseDay {

        public override bool Debug => false;

        protected override void Solve() {
            string[] lines = GetInput();

            int[] inputInts = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                if (int.TryParse(lines[i], out int result)) {
                    inputInts[i] = result;
                }
            }

            int higherSingle = 0;
            int higherGroup = 0;
            int prevSingle = inputInts[0];

            GroupValues(inputInts, 0, 3, out int prevGroup);

            for (int i = 0; i < inputInts.Length; i++) {
                int currSingle = inputInts[i];

                if (currSingle > prevSingle) {
                    higherSingle++;
                }

                if (GroupValues(inputInts, i, 3, out int currGroup)) {
                    if (currGroup > prevGroup) {
                        higherGroup++;
                    }
                }

                prevGroup = currGroup;
                prevSingle = currSingle;
            }

            SolutionPart1 = higherSingle;
            SolutionPart2 = higherGroup;
        }

        private static bool GroupValues(int[] values, int start, int amount, out int result) {
            if (start + amount <= values.Length) {
                int add = 0;
                for (int i = start; i < start + amount; i++) {
                    add += values[i];
                }
                result = add;
                return true;
            } else {
                result = 0;
                return false;
            }
        }
    }
}

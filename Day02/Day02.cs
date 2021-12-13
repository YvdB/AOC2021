using System;

namespace AOC2021 {
    class Day02 : BaseDay {

        private enum Dir { forward, down, up };

        public override bool Debug => false;

        protected override void Solve() {
            string[] lines = GetInput();

            int depth1 = 0;
            int depth2 = 0;
            int horizontal1 = 0;
            int horizontal2 = 0;
            int aim = 0;

            for (int i = 0; i < lines.Length; i++) {
                string[] split = lines[i].Split(' ');

                Dir currAction = (Dir)Enum.Parse(typeof(Dir), split[0]);
                int amount = int.Parse(split[1]);

                switch (currAction) {
                    case Dir.forward:
                        horizontal1 += amount;
                        horizontal2 += amount;
                        depth2 += aim * amount;
                        break;
                    case Dir.down:
                        depth1 += amount;
                        aim += amount;
                        break;
                    case Dir.up:
                        depth1 -= amount;
                        aim -= amount;
                        break;
                    default: break;
                }
            }

            SetAnswerPart1(depth1 * horizontal1);
            SetAnswerPart2(depth2 * horizontal2);
        }
    }
}

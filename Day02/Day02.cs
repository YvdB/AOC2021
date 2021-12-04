using System;

namespace AOC2021 {
    class Day02 : BaseDay {

        private enum Dir { forward, down, up };

        public override bool Debug => false;

        public override void Solve() {
            string[] lines = GetInput();

            int depth = 0;
            int horizontal = 0;
            int aim = 0;

            for (int i = 0; i < lines.Length; i++) {
                string[] split = lines[i].Split(' ');

                Dir currAction = (Dir)Enum.Parse(typeof(Dir), split[0]);
                int amount = int.Parse(split[1]);

                switch (currAction) {
                    case Dir.forward:
                        horizontal += amount;
                        depth += aim * amount;
                        break;
                    case Dir.down:
                        aim += amount;
                        break;
                    case Dir.up:
                        aim -= amount;
                        break;
                    default: break;
                }
            }

            Log("horizontal: " + horizontal);
            Log("depth: " + depth);
            Log("multiplied: " + depth * horizontal);
        }
    }
}

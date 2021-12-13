using System;

namespace AOC2021 {
    class Day11 : BaseDay {

        public override bool Debug => false;

        private class DumboOctopus {
            public bool Flashed;
            public int Energy;
        }

        private DumboOctopus[,] dumboOctopi;

        protected override void Solve() {
            string[] lines = GetInput();

            ParseInput(lines);

            LogField(-1,"Start");

            int totalFlashes = 0;

            int step = 1;
            int flashesAfter100Steps = -1;
            int synchronizedFlashStep = -1;

            while (synchronizedFlashStep == -1) {
                IncreaseEnergy();

                int flashesToProcess = GetNewFlashes();
                int stepFlashes = 0;

                while (flashesToProcess > 0) {
                    int flashes = FlashIfEnergyHighEnough();
                    totalFlashes += flashes;
                    stepFlashes += flashes;
                    flashesToProcess = GetNewFlashes(); ;
                }

                if (stepFlashes == dumboOctopi.Length) {
                    synchronizedFlashStep = step;
                }

                if(step == 100) {
                    flashesAfter100Steps = totalFlashes;
                }

                ResetFlashed();

                LogField(step, "Done");

                step++;
            }

            SetAnswerPart1(flashesAfter100Steps != -1 ? flashesAfter100Steps : totalFlashes);
            SetAnswerPart2(synchronizedFlashStep);
        }

        private void LogField(int step, string state) {
            if (!Debug) { return; }

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(string.Format("Step {0} State: {1}", step, state));
            ConsoleColor bg = Console.BackgroundColor;
            ConsoleColor fg = Console.ForegroundColor;
            for (int y = 0; y < dumboOctopi.GetLength(1); y++) {
                for (int x = 0; x < dumboOctopi.GetLength(0); x++) {
                    int energy = dumboOctopi[x, y].Energy;
                    if (energy == 0) {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    } else {
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = fg;
                    }
                    Console.Write(energy > 9 ? "*" : energy.ToString());
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            System.Threading.Thread.Sleep(12);
        }

        private void IncreaseEnergy() {
            for (int x = 0; x < dumboOctopi.GetLength(0); x++) {
                for (int y = 0; y < dumboOctopi.GetLength(1); y++) {
                    dumboOctopi[x, y].Energy++;
                }
            }
        }

        private void IncreaseEnergyNeighbours(int x, int y) {
            int up = Math.Max(y - 1, 0);
            int down = Math.Min(y + 1, dumboOctopi.GetLength(1) - 1);
            int right = Math.Min(x + 1, dumboOctopi.GetLength(0) - 1);
            int left = Math.Max(x - 1, 0);

            for (int xClamped = left; xClamped <= right; xClamped++) {
                for (int yClamped = up; yClamped <= down; yClamped++) {
                    if (yClamped == y && xClamped == x) {
                    } else {
                        if (!dumboOctopi[xClamped, yClamped].Flashed) {
                            dumboOctopi[xClamped, yClamped].Energy++;
                        }
                    }
                }
            }
        }

        private int FlashIfEnergyHighEnough() {
            int flashes = 0;
            for (int x = 0; x < dumboOctopi.GetLength(0); x++) {
                for (int y = 0; y < dumboOctopi.GetLength(1); y++) {
                    DumboOctopus octo = dumboOctopi[x, y];
                    if (!octo.Flashed && octo.Energy > 9) {
                        octo.Flashed = true;
                        flashes++;
                        IncreaseEnergyNeighbours(x, y);
                    }
                }
            }
            return flashes;
        }

        private void ResetFlashed() {
            for (int x = 0; x < dumboOctopi.GetLength(0); x++) {
                for (int y = 0; y < dumboOctopi.GetLength(1); y++) {
                    if (dumboOctopi[x, y].Flashed) {
                        dumboOctopi[x, y].Flashed = false;
                        dumboOctopi[x, y].Energy = 0;
                    }
                }
            }
        }

        private int GetNewFlashes() {
            int flashing = 0;
            for (int x = 0; x < dumboOctopi.GetLength(0); x++) {
                for (int y = 0; y < dumboOctopi.GetLength(1); y++) {
                    if (!dumboOctopi[x, y].Flashed && dumboOctopi[x, y].Energy > 9) {
                        flashing++;
                    }
                }
            }
            return flashing;
        }

        private void ParseInput(string[] lines) {
            int rows = lines.Length;
            int columns = lines[0].Length;

            dumboOctopi = new DumboOctopus[columns, rows];

            for (int x = 0; x < columns; x++) {
                for (int y = 0; y < rows; y++) {
                    dumboOctopi[x, y] = new DumboOctopus() {
                        Energy = int.Parse(lines[y][x].ToString())
                    };
                }
            }
        }
    }
}

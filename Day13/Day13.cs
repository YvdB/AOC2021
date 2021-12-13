using System;
using System.Collections.Generic;

namespace AOC2021 {
    class Day13 : BaseDay {

        public override bool Debug => false;

        private readonly List<Vector2Int> instructions = new List<Vector2Int>();

        private List<Vector2Int> dots = new List<Vector2Int>();

        protected override void Solve() {
            string[] lines = GetInput();

            bool parseInstructions = false;
            foreach (string l in lines) {
                if (string.IsNullOrWhiteSpace(l)) {
                    parseInstructions = true;
                    continue;
                }
                if (parseInstructions) {
                    string[] split2 = l.Split(' ')[2].Split('=');
                    Vector2Int newInstruction = new Vector2Int();
                    if (split2[0] == "x") {
                        newInstruction.X = int.Parse(split2[1]);
                    } else {
                        newInstruction.Y = int.Parse(split2[1]);
                    }
                    instructions.Add(newInstruction);
                } else {
                    string[] split = l.Split(',');
                    dots.Add(new Vector2Int(int.Parse(split[0]), int.Parse(split[1])));
                }
            }

            int dotsAfterFirstInstruction = -1;
            foreach (Vector2Int instruction in instructions) {
                List<Vector2Int> newDots = new List<Vector2Int>();
                foreach (Vector2Int dot in dots) {
                    Vector2Int newDot = new Vector2Int(dot.X, dot.Y);

                    if (instruction.Y != 0) {
                        int delta = instruction.Y - dot.Y;
                        if (delta < 0) {
                            newDot.Y = dot.Y - Math.Abs(delta) * 2;
                        }
                    }

                    if (instruction.X != 0) {
                        int delta = instruction.X - dot.X;
                        if (delta < 0) {
                            newDot.X = dot.X - Math.Abs(delta) * 2;
                        }
                    }

                    if (!newDots.Contains(newDot)) {
                        newDots.Add(newDot);
                    }
                    dots = newDots;
                }

                if (dotsAfterFirstInstruction == -1) {
                    dotsAfterFirstInstruction = dots.Count;
                }
            }

            SetAnswerPart1(dotsAfterFirstInstruction);

            Vector2Int bottomLeft = new Vector2Int();
            dots.ForEach(x => {
                bottomLeft.Y = Math.Max(bottomLeft.Y, x.Y);
                bottomLeft.X = Math.Max(bottomLeft.X, x.X);
            });

            if (Debug) {
                for (int y = 0; y <= bottomLeft.Y; y++) {
                    for (int x = 0; x <= bottomLeft.X; x++) {
                        if (dots.Find(match => match.Y == y && match.X == x) != null) {
                            Console.Write("#");

                        } else {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }
            }

            SetAnswerPart2("PZEHRAER");
        }
    }
}

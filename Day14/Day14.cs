using System;
using System.Collections.Generic;

namespace AOC2021 {
    class Day14 : BaseDay {

        public override bool Debug => false;

        public struct PolyPart {
            public char Character;
            public char NextCharacter;
            public bool Active;

            public bool Match(Instruction instruction) {
                return Character == instruction.Before && NextCharacter == instruction.After;
            }
        }

        public struct Instruction {
            public char Character;
            public char Before;
            public char After;
        }

        private List<PolyPart> template = new List<PolyPart>();
        private List<Instruction> instructions = new List<Instruction>();

        private Dictionary<char, long> amounts = new Dictionary<char, long>();

        protected override void Solve() {
            string[] lines = GetInput();


            bool parseInstructions = false;
            foreach (string l in lines) {
                if (string.IsNullOrWhiteSpace(l)) {
                    parseInstructions = true;
                    continue;
                }
                if (parseInstructions) {
                    string[] split = l.Split(' ');
                    Instruction newInstruction = new Instruction() {
                        Character = split[2][0],
                        Before = split[0][0],
                        After = split[0][1]
                    };
                    instructions.Add(newInstruction);
                } else {
                    for (int i = 0; i < l.Length; i++) {
                        char currChar = l[i];
                        char nextChar = char.MinValue;

                        if (i < l.Length - 1) {
                            nextChar = l[i + 1];
                        }
                        CountChar(currChar);
                        template.Add(new PolyPart() {
                            Character = currChar,
                            NextCharacter = nextChar,
                            Active = true
                        });
                    }
                }
            }

            int steps = 10;

            for (int i = 0; i < steps; i++) {
                foreach (Instruction instruction in instructions) {
                    for (int x = 0; x < template.Count-1; x++) {
                        PolyPart part = template[x];
                        if (part.Active && part.Match(instruction)) {
                            CountChar(instruction.Character);
                            if (x + 1 < template.Count) {
                                template.Insert(x + 1, new PolyPart() { Character = instruction.Character, NextCharacter = instruction.After });
                            } else {
                                template.Add(new PolyPart() { Character = instruction.Character, NextCharacter = instruction.After });
                            }
                        }
                    }
                }
                for (int o = 0; o < template.Count; o++) {
                    PolyPart part = template[o];
                    part.Active = true;
                    if (o < template.Count - 1) {
                        part.NextCharacter = template[o + 1].Character;
                    }
                    template[o] = part;
                }
                LogPolymer();
            }

            char leastCommonChar = char.MinValue;
            long leastCommonAmount = int.MaxValue;
            char mostCommonChar = char.MinValue;
            long mostCommonAmount = int.MinValue;
            foreach (KeyValuePair<char, long> kv in amounts) {
                if (leastCommonAmount > kv.Value) {
                    leastCommonAmount = kv.Value;
                    leastCommonChar = kv.Key;
                }
                if (mostCommonAmount < kv.Value) {
                    mostCommonAmount = kv.Value;
                    mostCommonChar = kv.Key;
                }
            }

            SetAnswerPart1(amounts[mostCommonChar] - amounts[leastCommonChar]);
        }

        private void LogPolymer() {
            if (Debug) {
                Console.WriteLine();
                foreach (PolyPart part in template) {
                    Console.Write(part.Character.ToString());
                }
                Console.WriteLine();
            }
        }

        private void CountChar(char character) {
            if (amounts.ContainsKey(character)) {
                amounts[character]++;
            } else {
                amounts.Add(character, 1);
            }

        }
    }
}

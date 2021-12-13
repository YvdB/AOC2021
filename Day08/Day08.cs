using System.Collections.Generic;

namespace AOC2021 {
    class Day08 : BaseDay {

        public override bool Debug => false;

        readonly char[] defaultDigitMap = new char[7] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

        readonly int[] zero = new int[7]  { 1, 1, 1, 0, 1, 1, 1 };
        readonly int[] one = new int[7]   { 0, 0, 1, 0, 0, 1, 0 };
        readonly int[] two = new int[7]   { 1, 0, 1, 1, 1, 0, 1 };
        readonly int[] three = new int[7] { 1, 0, 1, 1, 0, 1, 1 };
        readonly int[] four = new int[7]  { 0, 1, 1, 1, 0, 1, 0 };
        readonly int[] five = new int[7]  { 1, 1, 0, 1, 0, 1, 1 };
        readonly int[] six = new int[7]   { 1, 1, 0, 1, 1, 1, 1 };
        readonly int[] seven = new int[7] { 1, 0, 1, 0, 0, 1, 0 };
        readonly int[] eight = new int[7] { 1, 1, 1, 1, 1, 1, 1 };
        readonly int[] nine = new int[7]  { 1, 1, 1, 1, 0, 1, 1 };

        readonly List<int[]> allDigitStates = new List<int[]>();
        readonly Dictionary<string, int> defaultDigits = new Dictionary<string, int>() {
            { "cf", 1 },
            { "acdeg", 2 },
            { "acdfg", 3 },
            { "bcdf", 4 },
            { "abdfg", 5 },
            { "abdefg", 6 },
            { "acf", 7 },
            { "abcdefg", 8 },
            { "abcdfg", 9 }
        };

        protected override void Solve() {
            allDigitStates.AddRange(new int[][] { zero, one, two, three, four, five, six, seven, eight, nine });

            string[] lines = GetInput();

            List<int> uniqueDigitLengths = GetUniqueDigits();
            int knownLengthCount = 0;

            int addedSolutions = 0;

            for (int i = 0; i < lines.Length; i++) {
                if (string.IsNullOrEmpty(lines[i])) { break; }
                string[] split = lines[i].Split('|');

                string[] inputDigits = split[0].Trim().Split(' ');
                string[] outputDigits = split[1].Trim().Split(' ');

                char[] scrambledDigitMap = new char[7];

                FindTheMapping(inputDigits, scrambledDigitMap);

                string decoded = "";

                for (int j = 0; j < outputDigits.Length; j++) {
                    string s = Decode(outputDigits[j], scrambledDigitMap);
                    decoded += s;
                    foreach (int length in uniqueDigitLengths) {
                        if (outputDigits[j].Length == length) {
                            knownLengthCount++;
                        }
                    }
                }

                addedSolutions += int.Parse(decoded);
            }

            SetAnswerPart1(knownLengthCount);
            SetAnswerPart2(addedSolutions);
        }

        /// <summary>
        /// This made me feel like a mad man.
        /// </summary>
        /// <param name="inputDigits"></param>
        /// <param name="scrambledDigitMap"></param>
        private void FindTheMapping(string[] inputDigits, char[] scrambledDigitMap) {
            string one = "";
            string seven = "";
            string four = "";

            for (int j = 0; j < inputDigits.Length; j++) {
                string inputDigit = inputDigits[j];
                if (inputDigit.Length == 2) {
                    one = inputDigit;
                } else if (inputDigit.Length == 3) {
                    seven = inputDigit;
                } else if (inputDigit.Length == 4) {
                    four = inputDigit;
                }
            }

            char uniqueFromSeven = FindUnique(seven, one);

            scrambledDigitMap[0] = uniqueFromSeven;

            string nine = FindNumber(inputDigits, seven + four);

            char uniqueFromNine = FindUnique(nine, seven + four);

            scrambledDigitMap[6] = uniqueFromNine;

            string three = FindNumber(inputDigits, seven + uniqueFromNine);

            char uniqueFromThree = FindUnique(three, seven + uniqueFromNine);

            scrambledDigitMap[3] = uniqueFromThree;

            string zero = "";
            foreach (string id in inputDigits) {
                if (id.Length == 6 && !id.Contains(uniqueFromThree.ToString())) {
                    zero = id;
                }
            }

            char uniqueFromZero = FindUnique(zero, nine);

            scrambledDigitMap[4] = uniqueFromZero;

            string two = FindNumber(inputDigits, "" + uniqueFromThree + uniqueFromZero + uniqueFromSeven + uniqueFromNine);

            scrambledDigitMap[2] = FindUnique(two, "" + uniqueFromThree + uniqueFromZero + uniqueFromSeven + uniqueFromNine);

            scrambledDigitMap[5] = FindUnique(one, scrambledDigitMap[2].ToString());

            scrambledDigitMap[1] = FindUnique(zero, two + one);
        }

        private string FindNumber(string[] inputDigits, string check) {
            string combined = "";
            foreach (char c in check) {
                if (combined.Contains(c.ToString())) { continue; } else { combined += c; }
            }
            foreach (string id in inputDigits) {
                if (id.Length == combined.Length + 1) {
                    bool allThere = true;
                    foreach (char c in combined) {
                        if (!id.Contains(c.ToString())) {
                            allThere = false;
                        }
                    }
                    if (allThere) {
                        return id;
                    }
                }
            }
            return "";
        }

        private char FindUnique(string a, string b) {
            foreach(char c in a) {
                if (b.Contains(c.ToString())) { continue; } else { return c; }
            }
            return char.MinValue;
        }

        private string Decode(string input, char[] map) {
            int[] digitState = new int[7];

            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < defaultDigitMap.Length; j++) {
                    if(input[i] == defaultDigitMap[j]) {
                        digitState[Translate(defaultDigitMap[j], map)] = 1;
                    }
                }
            }

            string output = "x";

            for (int i = 0; i < allDigitStates.Count; i++) {
                if (Match(digitState, allDigitStates[i])) {
                    output = (i).ToString();
                }
            }

            return output;
        }

        private bool Match(int[] a, int[] b) {
            if(a.Length != b.Length) { return false; }
            bool match = true;
            for (int i = 0; i < a.Length; i++) {
                if(a[i] != b[i]) {
                    match = false;
                }
            }
            return match;
        }

        private int Translate(char x, char[] map) {
            for (int i = 0; i < map.Length; i++) {
                if(x == map[i]) {
                    return i;
                }
            }
            return 0;
        }

        private List<int> GetUniqueDigits() {
            List<int> uniqueDigitLengths = new List<int>();
            List<string> keys = new List<string>(defaultDigits.Keys);
            foreach (KeyValuePair<string, int> kv in defaultDigits) {
                bool unique = true;
                keys.ForEach(x => {
                    if (x != kv.Key) {
                        if (x.Length == kv.Key.Length) {
                            unique = false;
                        }
                    }
                });

                if (unique) { uniqueDigitLengths.Add(kv.Key.Length); }
            }
            return uniqueDigitLengths;
        }
    }
}

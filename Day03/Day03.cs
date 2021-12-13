using System;

namespace AOC2021 {
    class Day03 : BaseDay {

        public override bool Debug => false;

        protected override void Solve() {
            string[] lines = GetInput();

            int trimmedLength = lines[0].Trim().Length;

            int[] columnAdd = new int[trimmedLength];

            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[i].Length; j++) {
                    if (int.TryParse(lines[i][j].ToString(), out int result)) {
                        columnAdd[j] += result;
                    }
                }
            }

            string gammaRateBinary = "";
            string epsilonRateBinary = "";

            for (int i = 0; i < columnAdd.Length; i++) {
                if (columnAdd[i] > lines.Length / 2) {
                    gammaRateBinary += "1";
                    epsilonRateBinary += "0";
                } else {
                    gammaRateBinary += "0";
                    epsilonRateBinary += "1";
                }
            }

            int gammaRate = Convert.ToInt32(gammaRateBinary, 2);
            int epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

            SetAnswerPart1(gammaRate* epsilonRate);

            int generatorRating = Convert.ToInt32(FindBinary(lines, false), 2);
            int scrubberRating = Convert.ToInt32(FindBinary(lines, true), 2);

            SetAnswerPart2(scrubberRating * generatorRating);
        }

        private string FindBinary(string[] lines, bool lower) {

            int trimmedLength = lines[0].Trim().Length;

            int[] filter = new int[trimmedLength];

            for (int i = 0; i < filter.Length; i++) {
                filter[i] = -1;
            }

            int column = 0;

            for (int x = 0; x < trimmedLength; x++) {

                int added = 0;
                int linesChecked = 0;

                for (int i = 0; i < lines.Length; i++) {
                    string currentLine = lines[i];

                    if (int.TryParse(currentLine[column].ToString(), out int currentBit)) {
                        bool skip = false;
                        for (int j = 0; j < column; j++) {
                            if (int.TryParse(currentLine[j].ToString(), out int prevBit)) {
                                if (filter[j] != -1 && prevBit != filter[j]) { skip = true; }
                            }
                        }
                        if (skip) { continue; }
                        added += currentBit;
                        linesChecked++;
                    }
                }

                if (lower) {
                    if (linesChecked == 1) {
                        filter[column] = added;
                    } else {
                        filter[column] = added >= (double)linesChecked / 2 ? 0 : 1;
                    }
                } else {
                    filter[column] = added >= (double)linesChecked / 2 ? 1 : 0;
                }

                column++;
            }

            string binary = "";

            foreach (int bit in filter) {
                binary += bit.ToString();
            }

            return binary;
        }
    }
}

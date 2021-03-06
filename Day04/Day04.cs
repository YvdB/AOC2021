using System.Collections.Generic;

namespace AOC2021 {
    class Day04 : BaseDay {

        public override bool Debug => false;

        private Queue<int> numbersForDrawing;
        private List<Board> boards;

        public class Board {
            public int[,,] TileNumber;
            public int BoardSize;
            public int WinningInput = -1;

            public Board(int size) {
                BoardSize = size;
            }

            public int SumUnmarkedNumbers {
                get {
                    int sum = 0;
                    for (int x = 0; x < BoardSize; x++) {
                        for (int y = 0; y < BoardSize; y++) {
                            if (TileNumber[x, y, 1] == 0) {
                                sum += TileNumber[x, y, 0];
                            }
                        }
                    }
                    return sum;
                }
            }

            public override string ToString() {
                return string.Format("Last Input: {0} Unmarked Sum: {1} Value: {2}", WinningInput, SumUnmarkedNumbers, WinningInput * SumUnmarkedNumbers);
            }

            public int Answer { get { return WinningInput * SumUnmarkedNumbers; } }
        }

        protected override void Solve() {
            string[] lines = GetInput();

            ParseInput(lines);

            List<Board> completedBoards = new List<Board>();

            while (numbersForDrawing.Count > 0) {
                int nextNumber = numbersForDrawing.Dequeue();

                foreach (Board board in boards) {
                    if (board.WinningInput != -1) { continue; }
                    if (BingoBoard(board, nextNumber)) {
                        completedBoards.Add(board);
                    }
                }
            }

            SetAnswerPart1(completedBoards[0].Answer);
            SetAnswerPart2(completedBoards[completedBoards.Count - 1].Answer);
        }

        private bool BingoBoard(Board board, int nextNumber) {
            for (int x = 0; x < board.BoardSize; x++) {
                for (int y = 0; y < board.BoardSize; y++) {
                    if (board.TileNumber[x, y, 0] == nextNumber) {
                        board.TileNumber[x, y, 1] = 1;
                    }
                }
            }

            for (int x = 0; x < board.BoardSize; x++) {
                for (int y = 0; y < board.BoardSize; y++) {
                    if (CheckRow(board, x) || CheckColumn(board, y)) {
                        board.WinningInput = nextNumber;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckRow(Board board, int row) {
            bool bingo = true;
            for (int x = 0; x < board.BoardSize; x++) {
                if (board.TileNumber[x, row, 1] != 1) {
                    bingo = false;
                }
            }
            return bingo;
        }

        private bool CheckColumn(Board board, int column) {
            bool bingo = true;
            for (int y = 0; y < board.BoardSize; y++) {
                if (board.TileNumber[column, y, 1] != 1) {
                    bingo = false;
                }
            }
            return bingo;
        }

        private void ParseInput(string[] lines) {
            numbersForDrawing = new Queue<int>();
            string[] seperatedNumbers = lines[0].Split(',');
            for (int i = 0; i < seperatedNumbers.Length; i++) {
                if (int.TryParse(seperatedNumbers[i], out int result)) {
                    numbersForDrawing.Enqueue(result);
                }
            }

            Board newBoard = null;
            boards = new List<Board>();
            for (int i = 1; i < lines.Length; i++) {
                if (string.IsNullOrEmpty(lines[i])) {
                    newBoard = null;
                } else {
                    if (newBoard == null) {
                        List<int> rowNumbers = ParseBoardRow<int>(lines[i], ' ', int.TryParse);

                        newBoard = new Board(rowNumbers.Count);
                        newBoard.TileNumber = new int[newBoard.BoardSize, newBoard.BoardSize, 2];

                        for (int y = 0; y < newBoard.BoardSize; y++) {
                            rowNumbers = ParseBoardRow<int>(lines[i+y], ' ', int.TryParse);
                            for (int x = 0; x < newBoard.BoardSize; x++) {
                                newBoard.TileNumber[x, y, 0] = rowNumbers[x];
                            }
                        }
                        boards.Add(newBoard);
                    }
                }
            }
        }

        public static List<T> ParseBoardRow<T>(string row, char seperator, TryParseHandler<T> handler) {
            List<T> numbers = new List<T>();
            string[] seperated = row.Split(seperator);
            for (int i = 0; i < seperated.Length; i++) {
                if (handler(seperated[i], out T result)) {
                    numbers.Add(result);
                }
            }
            return numbers;
        }

        public delegate bool TryParseHandler<T>(string value, out T result);
    }
}

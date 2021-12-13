using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2021 {
    class Day05 : BaseDay {

        public override bool Debug => false;

        public class Cloud {

            public Vector2Int Start => points[0];
            public Vector2Int End => points[points.Count - 1];

            public int MinY => Math.Min(Start.Y, End.Y);
            public int MinX => Math.Min(Start.X, End.X);
            public int MaxY => Math.Max(Start.Y, End.Y);
            public int MaxX => Math.Max(Start.X, End.X);

            private readonly List<Vector2Int> points;

            public Cloud(string text) {
                points = new List<Vector2Int>();
                string[] split = text.Split(' ');
                foreach (string s in split) {
                    if (char.IsDigit(s[0])) {
                        string[] splitPoints = s.Split(',');
                        points.Add(new Vector2Int(int.Parse(splitPoints[0]), int.Parse(splitPoints[1])));
                    }

                }
            }

            public bool Straight => Start.X == End.X || Start.Y == End.Y;
            public bool Diagonal => Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);

            public List<Vector2Int> GetAllPoints() {
                int xDir = Math.Sign(Start.X - End.X);
                int yDir = Math.Sign(Start.Y - End.Y);

                int nextX = Start.X;
                int nextY = Start.Y;

                List<Vector2Int> points = new List<Vector2Int>();

                while (nextX != End.X || nextY != End.Y) {
                    points.Add(new Vector2Int(nextX, nextY));
                    nextX -= xDir;
                    nextY -= yDir;
                }

                points.Add(End);

                return points;
            }
        }

        public List<Cloud> VentClouds;

        protected override void Solve() {
            string[] lines = GetInput();

            VentClouds = new List<Cloud>();
            foreach (string s in lines) {
                VentClouds.Add(new Cloud(s));
            }

            int maxX = 0;
            int maxY = 0;

            VentClouds.ForEach(x => {
                maxX = Math.Max(maxX, x.MaxX);
                maxY = Math.Max(maxY, x.MaxY);
            });

            int[,] grid = new int[maxX + 1, maxY + 1];
            int overlapStraight = 0;

            VentClouds.ForEach(cloud => {
                if (cloud.Straight) {
                    List<Vector2Int> points = cloud.GetAllPoints();
                    foreach (Vector2Int p in points) {
                        grid[p.X, p.Y]++;

                        if (grid[p.X, p.Y] == 2) {
                            overlapStraight++;
                        }
                    }
                }
            }); 
            
            int overlapDiagonal = 0;

            VentClouds.ForEach(cloud => {
                if (cloud.Diagonal) {
                    List<Vector2Int> points = cloud.GetAllPoints();
                    foreach (Vector2Int p in points) {
                        grid[p.X, p.Y]++;

                        if (grid[p.X, p.Y] == 2) {
                            overlapDiagonal++;
                        }
                    }
                }
            });

            if (Debug) {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();

                for (int y = 0; y <= maxY; y++) {
                    for (int x = 0; x <= maxX; x++) {
                        int amount = grid[x, y];
                        if (amount == 0) {
                            stringBuilder.Append(".");
                        } else {
                            stringBuilder.Append(amount.ToString());
                        }
                    }
                    stringBuilder.AppendLine();
                }

                Log(stringBuilder.ToString());
            }

            SetAnswerPart1(overlapStraight);
            SetAnswerPart2(overlapStraight + overlapDiagonal);

        }
    }
}

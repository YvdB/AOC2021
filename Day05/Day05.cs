using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2021 {
    class Day05 : BaseDay {

        public override bool Debug => false;

        public class Point {
            public int X = 0, Y = 0;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public override bool Equals(object other) {
                Point otherPoint = (Point)other;
                if(otherPoint == null) { 
                    return false;
                } else {
                    return X == otherPoint.X && Y == otherPoint.Y;
                }
            }

            public override int GetHashCode() {
                return base.GetHashCode();
            }
        }

        public class Cloud {

            public Point Start => points[0];
            public Point End => points[points.Count-1];

            public int MinY => Math.Min(Start.Y, End.Y);
            public int MinX => Math.Min(Start.X, End.X);
            public int MaxY => Math.Max(Start.Y, End.Y);
            public int MaxX => Math.Max(Start.X, End.X);

            private List<Point> points;

            public Cloud(string text) {
                points = new List<Point>();
                string[] split = text.Split(' ');
                foreach(string s in split) {
                    if (char.IsDigit(s[0])) {
                        string[] splitPoints = s.Split(',');
                        points.Add(new Point(int.Parse(splitPoints[0]), int.Parse(splitPoints[1])));
                    }

                }
            }

            public bool Straight => Horizontal || Vertical;
            public bool Diagonal => Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);
            public bool Vertical => Start.Y == End.Y;
            public bool Horizontal => Start.X == End.X;

            public bool PointOnLine(int X, int Y) {
                bool xMatch = X >= MinX && X <= MaxX;
                bool yMatch = Y >= MinY && Y <= MaxY;
                bool diagonal = Math.Abs(Y - End.Y) == Math.Abs(X - End.X);

                if (Straight) {
                    return xMatch && yMatch;
                } else if (Diagonal) {
                    return diagonal && xMatch && yMatch;
                } else {
                    return false;
                }
            }

            public List<Point> GetAllPoints() {
                List<Point> points = new List<Point>();
                for (int x = MinX; x <= MaxX; x++) {
                    for (int y = MinY; y <= MaxY; y++) {
                        if(PointOnLine(x, y)) {
                            points.Add(new Point(x, y));
                        }
                    }
                }
                return points;
            }
        }

        public List<Cloud> VentClouds;

        public override void Solve() {
            string[] lines = GetInput();

            VentClouds = new List<Cloud>();
            foreach(string s in lines) {
                VentClouds.Add(new Cloud(s));
            }

            int maxX = 0;
            int maxY = 0;

            VentClouds.ForEach(x => {
                maxX = Math.Max(maxX, x.MaxX);
                maxY = Math.Max(maxY, x.MaxY);
            });

            int[,] grid = new int[maxX+1, maxY+1];
            int overlapPointCount = 0;

            VentClouds.ForEach(cloud => {
                if (cloud.Straight || cloud.Diagonal) {
                    List<Point> points = cloud.GetAllPoints();
                    foreach (Point p in points) {
                        grid[p.X, p.Y]++;

                        if(grid[p.X, p.Y] == 2) {
                            overlapPointCount++;
                        }
                    }
                }
            });

            if (Debug) {
                StringBuilder stringBuilder = new StringBuilder();
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

            Log("Overlapping Points: " + overlapPointCount);
        }
    }
}
